using System;
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

namespace DataSyncSystem.Utils
{
    public static class GetCsvSock
    {
        public static Socket sock = null;//静态成员，所有的GetCsvSock 实例都只是使用这个sock
        public static string userId;
        public static string trialDate;
        public static FmMain parent;

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
            byte[] msgBuf = new byte[128];
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
                    continue;
                }

                //正常响应reqcsv:
                else if (msg.StartsWith("resreqcsv:"))
                {
                    MyLogger.WriteLine("<下载csv>服务端回应！" + msg);

                    //开始接收 csv 数据包
                    //> 第一种方法
                    #region 接收csv 数据
                    bool ifDataEnd = false;

                    int count = 0;
                    int maxFileLen = 1024 * 512;//512 k
                    byte[] fileBuf = new byte[maxFileLen];

                    string csvFileName = userId + "_" + trialDate + ".csv";
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
                            if (count > 64)
                            {
                                fs.Write(fileBuf, 0, count);
                                fs.Flush();//------add 
                            }
                            else //count < 64
                            {
                                msg = Encoding.UTF8.GetString(fileBuf);

                                //文件结束标志  end:# file_name # file_left #
                                if (msg.StartsWith("endcsv:"))
                                {
                                    msg = "resendcsv:#" + name + "#"; // end:# unique #
                                    sock.Send(Encoding.UTF8.GetBytes(msg.ToCharArray()));//response
                                    MyLogger.WriteLine("客户端回应:" + msg);

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
                                    }
                                    catch { }

                                    MyLogger.WriteLine("csv 文件关闭");
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
                    byte[] dbgFileBuf = new byte[1024 * 32]; //32k
                    try
                    {
                        sock.Receive(dbgFileBuf);
                    }
                    catch
                    {
                        MyLogger.WriteLine("接收dbgfiles 出错!");
                    }
                    msg = Encoding.UTF8.GetString(dbgFileBuf);
                    string[] dbgFileArr = msg.Split('#')[0].Split(','); // msg.Split('#')[0] => 文件列表
                    FileStream fs = new FileStream(Environment.CurrentDirectory + "\\" + trialUnique + ".dict",
                                                   FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);
                    foreach (string s in dbgFileArr)
                        sw.WriteLine(s);
                    sw.Close();
                    fs.Close();
                    MyLogger.WriteLine(Environment.CurrentDirectory + "\\" + trialUnique + ".dict 保存完成!");
                }
            }
        }

        // 读取csv 文件并装填到DataGridView 中
        public static void csv3(object obj)
        {
            string pCsvPath = obj as string;//文件路径
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
        public static void dnldCsvFile(string id, string date)
        {
            userId = id;
            trialDate = date;

            //先发送请求头: reqcsv:#userId_trialDate#
            string reqHead = "reqcsv:#" + userId + "_" + trialDate + "#";

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

        public static void init(Form fm)
        {
            parent = (FmMain)fm;
        }

    } // public static class GetCsvSock
} // namespace
