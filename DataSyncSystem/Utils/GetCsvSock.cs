﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSyncSystem.Dao;
using DataSyncSystem.Utils;
using System.Threading;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.IO.Compression;
using System.Windows.Forms;
using System.Data;
using DataSyncSystem.SelfView;

namespace DataSyncSystem.Utils
{
    public class GetCsvSock
    {
        public static Socket sock = null;//静态成员，所有的GetCsvSock 实例都只是使用这个sock
        public static string userId;
        public static string trialDate;
        public static FmMain parent;

        public static string dnldDir = Environment.CurrentDirectory;
        public static bool bchDnldProg = false;

        private static IAnalyzCsvDnlded callback;

        //初始化socket
        static GetCsvSock()
        {
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(ContantInfo.SockServ.ip);

            try
            {
                sock.Connect(ip, Int32.Parse(ContantInfo.SockServ.port));
                MyLogger.WriteLine("<下载csv> 连接服务端成功!");

                //开启监听线程
                Thread recvMsgTh = new Thread(recvMsg);
                recvMsgTh.IsBackground = true;
                recvMsgTh.Start();

            }
            catch(Exception err)
            {
                MyLogger.WriteLine("连接服务端失败(csv sock)! \n详细信息:\n"+err.Message);
            }
        }
        private static void recvMsg()
        {
            string msg = null;
            byte[] msgBuf = new byte[128]; //128 字节
            int byteCount = 0;

            while (sock != null)
            {
                //接收服务端数据
                MyLogger.WriteLine("GetCsvSock listen next csv msg.....");
                try { 
                    byteCount = sock.Receive(msgBuf);
                }
                catch
                {
                    MyLogger.WriteLine("close socket exception!");
                    return;
                }
                if(byteCount == -1)
                {
                    MyLogger.WriteLine("GetCsvSock exit!");
                    break;
                }

                msg = Encoding.UTF8.GetString(msgBuf);

                //错误响应reqcsv:
                if (msg.StartsWith("errreqcsv:"))
                {
                    MyLogger.WriteLine("<下载csv>:服务端回应请求错误! " + msg);
                    MessageBox.Show(msg.Split('#')[1], "csv文件缺失");

                    //设置CombxSummaryFile 没有summary 文件
                    parent.setSumFileComb(-1);

                    //清空datagridview 中的内容
                    parent.clearGridView();

                    //csv响应错误，也要请求文件dict
                    if (!File.Exists(Environment.CurrentDirectory +"\\"+ userId + "_" + trialDate + ".dict"))
                    {
                        queryDdgFiles(userId, trialDate);
                    }

                    continue;
                }

                //正常响应reqcsv:  msg = "resreqcsv:#" + csvFiles.Count + "#" + sumId + "#";
                else if (msg.StartsWith("resreqcsv:"))
                {
                    MyLogger.WriteLine("<下载csv>服务端回应！" + msg);
                    //获取服务端summary数量
                    string[] splits = msg.Split('#');
                    int sumFileNum = Int32.Parse(splits[1]);

                    //更新FmMain 中的summary.csv下拉框
                    parent.setSumFileComb(sumFileNum);

                    //开始接收 csv 数据包
                    //> 第一种方法
                    #region 接收csv 数据
                    bool ifDataEnd = false;

                    int count = 0;
                    int maxFileLen = 1024 * 512;//512 k
                    byte[] fileBuf = new byte[maxFileLen];

                    string csvFileName = userId + "_" + trialDate+ "_" + splits[2] + ".csv";
                    MyLogger.WriteLine("recv Data run:" + csvFileName);

                    //保存的csv 文件路径
                    //保存在当前软件运行的目录下
                    //该summary.csv 文件命名形式为 userId_date.csv
                    //最后退出软件时，会删除所有临时文件

                    string name = Environment.CurrentDirectory + "\\" + csvFileName;
                    using (FileStream fs = new FileStream(name, FileMode.Create))
                    {
                        while (!ifDataEnd)
                        {
                            try
                            {
                                count = sock.Receive(fileBuf);
                            }
                            catch
                            {
                                MyLogger.WriteLine("close recv sock exception!");
                            }

                            //正常数据
                            if (count > 128) // 64
                            {
                                try
                                {
                                    fs.Write(fileBuf, 0, count);
                                    fs.Flush();//------add 
                                }
                                catch { MyLogger.WriteLine("接收csv文件时，写入文件错误！"); }
                            }
                            else //count < 64
                            {
                                msg = Encoding.UTF8.GetString(fileBuf);

                                //文件结束标志  end:# file_name # file_left #
                                if (msg.StartsWith("endcsv:"))
                                {
                                    msg = "resendcsv:#" + name + "#"; // end:# unique #
                                    try
                                    {
                                        sock.Send(Encoding.UTF8.GetBytes(msg.ToCharArray()));//response
                                        MyLogger.WriteLine("客户端回应:" + msg);
                                    }
                                    catch { MyLogger.WriteLine("发送csv接收完成的回应时,socket错误！"); }

                                    //结束
                                    ifDataEnd = true;

                                    //防止接收到结束符，文件流还没关闭的情况
                                    if (fs.CanWrite)
                                    {
                                        fs.Close();
                                        MyLogger.WriteLine(139 + "csv文件下载成功，保存在:\n" + name);
                                    }
                                }
                                //不能整段发送的剩余数据
                                else
                                {
                                    MyLogger.WriteLine("段数据 写入csv 文件!");
                                    try { 
                                        fs.Write(fileBuf, 0, count);
                                        fs.Close();
                                        MyLogger.WriteLine("csv 文件关闭");
                                    }
                                    catch { MyLogger.WriteLine("csv段数据接收并关闭文件时错误!"); }
                                }
                            }
                        } // while (!ifDataEnd)
                    } // using(FileStream fs
                    #endregion

                    //> 使用线程去读取csv文件 并装填到DataGridView 中
                    Thread th = new Thread(csv3);
                    th.IsBackground = true;
                    th.Start(name);
                }

                // 错误响应 reqdbgfile:
                else if (msg.StartsWith("errreqdbgfile"))
                {
                    MyLogger.WriteLine("<下载dbgfiles>:服务端回应请求错误! " + msg);
                    MessageBox.Show(msg.Split('#')[1], "搜索错误!");
                    continue;
                }

                //正常响应 reqdbgfile:
                else if (msg.StartsWith("resreqdbgfile:"))
                {
                    string trialUnique = msg.Split('#')[1];
                    MyLogger.WriteLine("接收服务端resreqdbgfile:\n"+msg);

                    //接收dbgfiles 信息
                    byte[] dbgFileBuf = new byte[1024 * 300]; //300k
                    try
                    {
                        sock.Receive(dbgFileBuf);
                    }
                    catch
                    {
                        MyLogger.WriteLine("接收resreqdbgfiles响应 出错!");
                    }
                    msg = Encoding.UTF8.GetString(dbgFileBuf);
                    string[] dbgFileArr = msg.Split('#')[0].Split(','); // msg.Split('#')[0] => 文件列表
                    try
                    {
                        FileStream fs = new FileStream(Environment.CurrentDirectory + "\\" + trialUnique + ".dict",
                                                   FileMode.Create);
                        StreamWriter sw = new StreamWriter(fs);
                        foreach (string s in dbgFileArr)
                        {
                            sw.WriteLine(s);
                        }
                        sw.Close();
                        fs.Close();
                        MyLogger.WriteLine(Environment.CurrentDirectory + "\\" + trialUnique + ".dict 保存完成!");

                        //显示界面中点击按钮
                        parent.enablePic();
                    }
                    catch(Exception ex) 
                    {
                        MyLogger.WriteLine("保存dbgfiles dict 文件时错误！" + ex.Message);
                    }
                }

                //错误响应 reqbunchfile
                else if (msg.StartsWith("errreqbunchfile:"))
                {
                    MyLogger.WriteLine("<下载bunchfiles>:服务端回应请求错误! " + msg);
                    MessageBox.Show(msg.Split('#')[1], "下载错误!");
                    continue;
                }

                //正常响应 reqbunchfile
                else if (msg.StartsWith("resreqbunchfile:"))
                {
                    //直接开始接收bunch files 文件信息
                    int maxFileLen = 1024 * 512;//512 k
                    byte[] fileBuf = new byte[maxFileLen];
                    string fileName = null;
                    int waitRecvFileNum = Int32.Parse(msg.Split('#')[1]); //要下载的文件个数
                    long allFileLength = long.Parse(msg.Split('#')[2]);
                    long sent = 0;

                    FmBchDnldProgrs prog = null;
                    if (bchDnldProg) //如果要显示bunch files 下载进度条
                    {
                        prog = new FmBchDnldProgrs(waitRecvFileNum,allFileLength);
                        prog.Show(parent);
                    }

                    //监听每个文件上传请求
                    DateTime now = DateTime.Now;
                    string tm = now.ToLongTimeString();
                    tm = tm.Substring(0, tm.Length - 3).Replace(':', '.'); //09:34:23 > 09.34.23

                    //另外的这种时间格式是用来进行数据分析的时候
                    //方便文件操作
                    if (!bchDnldProg)
                    {
                        tm = userId + "_" + trialDate; //1000248501_trial日期
                    }

                    List<string> dnldFiles = new List<string>();
                    int taskFileId = 1;
                    while (waitRecvFileNum >= 1)
                    {
                        //监听文件头 信息
                        int count = 0;
                        try
                        {
                            sock.Receive(msgBuf);
                            msg = Encoding.UTF8.GetString(msgBuf);
                        }
                        catch
                        {
                            MyLogger.WriteLine("文件批量下载时，监听单个文件信息错误！");
                            break;
                        }

                        //file: # file_len # file_name #  
                        if (msg.StartsWith("singleinfo:"))
                        {
                            fileName = msg.Split('#')[2];
                            MyLogger.WriteLine("singleinfo:\n" + msg);

                            bool ifFileEnd = false;
                            string fmTmp = dnldDir + "\\" + tm + "-" + fileName;
                            using (FileStream fs = new FileStream(fmTmp, FileMode.Create))
                            {
                                //监听文件数据 loop
                                while (!ifFileEnd)
                                {
                                    try
                                    {
                                        count = sock.Receive(fileBuf);
                                    }
                                    catch {
                                        MyLogger.WriteLine("bunch file 下载时,接收文件信息时socket 错误！");
                                        break;
                                    }
                                    //正常数据
                                    if (count > 128)
                                    {
                                        try
                                        {
                                            fs.Write(fileBuf, 0, count);
                                            sent += count;
                                        }
                                        catch(Exception ex)
                                        {
                                            MyLogger.WriteLine("exceptioin:\n" + ex.Message);
                                            MyLogger.WriteLine("bunch file 下载时,写入文件信息时错误！");
                                        }
                                    }
                                    else
                                    {
                                        msg = Encoding.UTF8.GetString(fileBuf);

                                        //文件结束标志  singleend:# file_name # file_left #
                                        if (msg.StartsWith("singleend:"))
                                        {
                                            try
                                            {
                                                waitRecvFileNum = Int32.Parse(msg.Split('#')[2]);
                                                MyLogger.WriteLine("还剩余文件：" + (waitRecvFileNum - 1) + " 待传输\n");
                                            }
                                            catch { MyLogger.WriteLine("parse 文件待传输数量时异常！"); }

                                            //结束
                                            ifFileEnd = true;

                                            //[add]
                                            if (fs.CanWrite)
                                            {
                                                try
                                                {
                                                    fs.Close();
                                                    MyLogger.WriteLine("保存文件:" + fileName + " 成功!\n");
                                                }
                                                catch { MyLogger.WriteLine("bunfile 关闭接收文件时异常！"); }
                                            }
                                        }

                                        //不能整段发送的剩余数据
                                        else
                                        {
                                            try
                                            {
                                                fs.Write(fileBuf, 0, count);
                                                sent += count;
                                                fs.Close();
                                                MyLogger.WriteLine("保存文件:" + fileName + " 成功!\n");
                                            }
                                            catch { MyLogger.WriteLine("bunfile 关闭接收文件时异常！"); }
                                        }
                                    }
                                    if (prog != null)
                                    {
                                        prog.updtProg(waitRecvFileNum, fileName,sent);
                                    }
                                }// while(!iffileEnd)

                                //if (prog != null)
                                //{
                                //   prog.updtProg(waitRecvFileNum,fileName);
                                //}

                            }// using file()
                            dnldFiles.Add(fmTmp);
                            taskFileId++;
                        }// if (msg.StartsWith("singleinfo:"))
                    }// while(waitRecvNum > 1)
                    
                    if(prog != null) // 不是后台下载
                    {
                        //bunch files 传输完成!
                        MessageBox.Show("下载完成！", "message");
                    }
                    else //prog等于null，表示文件是为analyze后台下载的
                    {
                        //下载完待分析的文件后，callback通知分析
                        callback.dnldOkCallBack(dnldFiles);
                    }
                }
            }
        }

        // 读取csv 文件并装填到DataGridView 中
        public static void csv3(object obj)
        {
            string pCsvPath = obj as string;//文件路径
            if(!File.Exists(pCsvPath))
            {
                MyLogger.WriteLine("线程加载csv文件时错误！文件不存在");
                return;
            }
            try
            {
                String line;
                String[] split = null;
                DataTable table = new DataTable();
                DataRow row = null;

                StreamReader sr = new StreamReader(pCsvPath, Encoding.Default);
                //创建与数据源对应的数据列 
                line = sr.ReadLine();
                split = line.Split(',');
                foreach (String colname in split)
                {
                    table.Columns.Add(colname, System.Type.GetType("System.String"));
                }
                //将数据填入数据表 
                int j = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    j = 0;
                    row = table.NewRow();
                    split = line.Split(',');
                    foreach (String colname in split)
                    {
                        row[j] = colname;
                        j++;
                    }
                    table.Rows.Add(row);
                }
                sr.Close();
                //使用代理更新FmMain 中的DataGridView 
                parent.showDataview(table.DefaultView);

                //下载完summary csv 文件，接着街下载dbgfiles
                if (!File.Exists(Environment.CurrentDirectory+"\\" + userId + "_" + trialDate + ".dict"))
                {
                    queryDdgFiles(userId, trialDate);
                }
            }
            catch (Exception vErr)
            {
                MessageBox.Show(vErr.Message);
            }
            finally
            {
                GC.Collect();
            }
        }

        //外部调用接口 [用来发送csv头部:reqcsv:#userId_trialDate#]
        public static void dnldCsvFile(string id, string date,int sumId)
        {
            userId = id;
            trialDate = date;

            //先发送请求头: reqcsv:#userId_trialDate#
            string reqHead = "reqcsv:#" + userId + "_" + trialDate + "#" + sumId + "#";

            try
            {
                sock.Send(Encoding.UTF8.GetBytes(reqHead.ToCharArray()));
            }
            catch
            {
                MyLogger.WriteLine("发送reqcsv失败！");
                MessageBox.Show("与服务端断开连接!", "message");
            }
            MyLogger.WriteLine("发送请求头:" + reqHead);
        }

        //外部调用接口 [发送该trail 中的所有bebug文件],包括bebug中的所有文件结构
        public static void queryDdgFiles(string id,string date)
        {
            userId = id;
            trialDate = date;
            string reqHead = "reqdbgfile:#" + userId + "_" + trialDate + "#";
            try
            {
                sock.Send(Encoding.UTF8.GetBytes(reqHead.ToCharArray()));
            }catch(Exception ex)
            {
                MyLogger.WriteLine("发送reqdbgfile请求失败!"+ex.Message);
                MessageBox.Show("与服务端断开连接!", "搜索失败");
            }
            MyLogger.WriteLine("发送请求头:" + reqHead);
        }

        //外部调用接口 [发送dbgfiles下载请求]
        //function(dnldFolder,userid+"_"+date, List<string> fileIds,bool progress);
        public static void dnldFiles(string id, string date, string dnldFolder, List<string> fileIds, bool progress
            ,IAnalyzCsvDnlded callbk)
        {
            userId = id;
            trialDate = date;
            if(dnldFolder != null)
            {
                dnldDir = dnldFolder;
            }
            callback = callbk;
            bchDnldProg = progress;

            string req = "reqbunchfiles:#" + userId + "_" + trialDate + "#";
            try
            {
                sock.Send(Encoding.UTF8.GetBytes(req.ToString().ToCharArray()));
                MyLogger.WriteLine("发送请求头:" + req);

                Thread.Sleep(400);

                //加入要下载的文件ids
                StringBuilder reqHead = new StringBuilder("bunchfileids:#");
                foreach (string fileid in fileIds)
                {
                    reqHead.Append(fileid + ",");
                }
                reqHead.Append("#");

                try
                {
                    sock.Send(Encoding.UTF8.GetBytes(reqHead.ToString().ToCharArray()));
                    MyLogger.WriteLine("发送bunchfileids 成功!");

                }catch(Exception x)
                {
                    MyLogger.WriteLine("发送bunchfileids 失败！"+x.Message);
                }
            }
            catch (Exception ex)
            {
                MyLogger.WriteLine("发送reqdbgfile请求失败!" + ex.Message);
                MessageBox.Show("与服务端断开连接!", "搜索失败");
            }
        }

        public static void init(Form fm)
        {
            parent = (FmMain)fm;
        }

    } // public static class GetCsvSock
} // namespace
