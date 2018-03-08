using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataSyncSystem.SelfView;
using DataSyncSystem.Dao;
using DataSyncSystem.Utils;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;
using System.IO.Compression;

namespace DataSyncSystem
{
    public partial class FmFactory : Form
    {
        private User curUser;
        private DataService service = new DataService();

        #region 上传部件
        Socket upldSock = null;
        volatile bool ifRecv = false; 
        string upldHead = null; // upld请求头
        string upldPath = "";  // upld 的路径
        string zipFileName = "";  // upld的临时文件压缩的目录 
        TrialInfo trialInfo = new TrialInfo(); // 记录上传过的trial信息
        volatile bool isNewUpld = true;  // 表示是否是新的上传
        string upldHistStr = ""; // .upldhist.hist 中存储的字符串
        string pltfmpdctStr = ""; // .upldhist.hist 中存储的pltfm , pdct 
        volatile bool upldRunFlg ;
        volatile int compressCode = ContantInfo.Compress.WAIT;
        #endregion

        public FmFactory()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void FmFactory_Load(object sender, EventArgs e)
        {
            // 获取当前用户信息
            curUser = service.getUserByUserId(Cache.userId);

            labName.Text = curUser.UserName;
            labTeam.Text = curUser.TeamName;
            labUserid.Text = curUser.UserId;

            // 初始化socket
            initUpldSock();
        }

        private void FmFactory_FormClosing(object sender, FormClosingEventArgs e)
        {
            service.closeCon();

            //关闭负责上传的socket
            if (upldSock != null)
            {
                try
                {
                    upldSock.Shutdown(SocketShutdown.Both);
                    upldSock.Close();
                }
                catch
                {
                    MyLogger.WriteLine("关闭upldSock 遇到异常！");
                }
            }
        }

        //初始化传输端口
        private void initUpldSock()
        {
            //判断upldSock 是否准备好
            if (upldSock == null)
            {
                //先连接服务端,获得一个socket
                upldSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Parse(ContantInfo.SockServ.ip);
                try
                {
                    upldSock.Connect(ip, Int32.Parse(ContantInfo.SockServ.port));

                    MyLogger.WriteLine("=========== start thread! ==========");
                    upldRunFlg = true;

                    //开启线程监听server 的 响应
                    Thread recvTh = new Thread(recv);
                    recvTh.IsBackground = true;
                    recvTh.Start();

                    labStatus.Text = "connected!";
                }
                catch (Exception err)
                {
                    MessageBox.Show("socket连接失败", "warning");
                    MyLogger.WriteLine(err.Message);
                }
            }
        }

        //监听消息的线程函数
        private void recv()
        {
            while (upldRunFlg)
            {
                MyLogger.WriteLine("upldSock 等待服务端下一次同意上传回应....");
                bool endFlg = false;
                byte[] msgBuf = new byte[64];
                string msg = null;
                while (!endFlg)
                {
                    try
                    {
                        int count = upldSock.Receive(msgBuf);
                    }
                    catch
                    {
                        MyLogger.WriteLine("upldSock Receive() 接收出错！");
                        return;
                    }
                    msg = Encoding.UTF8.GetString(msgBuf);
                    MyLogger.WriteLine(msg);

                    //接收到服务端 接收文件的响应
                    if (msg.StartsWith("resupld:"))
                    {
                        MyLogger.WriteLine("服务端同意接收:" + msg.Split('#')[1]);

                        //先压缩文件
                        Thread compressTh = new Thread(compress);
                        compressTh.IsBackground = true;
                        compressTh.Start();
                        MyLogger.WriteLine("开始压缩文件...");

                        //弹出一个不可取消的等待框,或者进度条
                        FmCompress fm = new FmCompress(this);
                        fm.Show();

                        //然后用循环检测文件压缩是否完成
                        int index = 0;
                        if (compressCode == ContantInfo.Compress.WAIT)
                        {
                            while (compressCode == ContantInfo.Compress.WAIT)
                            {
                                fm.updateInfo(index);
                                index++;
                                MyLogger.WriteLine("wait compress...");
                                Thread.Sleep(1000);
                            }
                            MyLogger.WriteLine("compress ok!");
                        }
                        fm.updateInfo(-1); // 关闭等待压缩进度条

                        if (compressCode == ContantInfo.Compress.PRESSOK) //压缩过程无错
                        {
                            //开始传输 zipFileName 的 .zip 文件
                            Thread upldTh = new Thread(upload);
                            upldTh.IsBackground = true;
                            upldTh.Start();
                            MyLogger.WriteLine("开始文件上传线程....");
                        }
                        else // 压缩出错
                        {
                            MessageBox.Show("压缩出错!", "上传中断");
                            MyLogger.WriteLine("压缩出错!");
                        }

                    }//msg.StartWith("head:");

                    //服务端同意接收文件
                    if (msg.StartsWith("resfile:"))
                    {
                        ifRecv = true;
                        MyLogger.WriteLine("服务端同意接收:" + msg.Split('#')[1]);
                    }

                    //接收到服务端 结束文件接收的响应
                    if (msg.StartsWith("resend:"))
                    {
                        endFlg = true;
                        MyLogger.WriteLine("服务端返回接收结束响应!");
                        MessageBox.Show("上传成功!", "message");

                        //上传完成后，重新设置
                        txtFolder.Text = "";
                        trialInfo = new TrialInfo();
                        groupInfo.Visible = false;
                    }

                    //接收服务端返回的错误信息
                    if (msg.StartsWith("errupld:"))
                    {
                        endFlg = true;
                        MessageBox.Show(msg.Split('#')[1], "message");

                        //删除upld 目录中的.upldhist.hist
                        string histNmae = upldPath + "\\.upldhist.hist";
                        if (File.Exists(histNmae))
                            File.Delete(histNmae);

                        txtFolder.Text = "";
                        trialInfo = new TrialInfo();
                        groupInfo.Visible = false;
                    }
                }//while(!endFlg)
                MyLogger.WriteLine("客户端监听任务结束!");
            } //while()
        }
        private void btBrowser_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dia = new FolderBrowserDialog();
            if(dia.ShowDialog() == DialogResult.OK)
            {
                upldPath = dia.SelectedPath;
                txtFolder.Text = upldPath;

                int checkUpldDirCode = 0;
                if(FileHandle.checkUpldDir(upldPath,ref checkUpldDirCode))
                {
                    #region 检查upldhist.hist
                    FileInfo histFile = new FileInfo(upldPath + "\\.upldhist.hist");
                    if (histFile.Exists)
                    {
                        isNewUpld = false; //不是新的上传
                        using (FileStream fs = new FileStream(histFile.FullName, FileMode.Open))
                        {
                            using (StreamReader sr = new StreamReader(fs))
                            {
                                string line = "";
                                while ((line = sr.ReadLine()) != null)
                                {
                                    if (line.StartsWith("token="))
                                    {
                                        upldHistStr = line.Split('=')[1];
                                    }
                                    if (line.StartsWith("pltfmpdct="))
                                    {
                                        pltfmpdctStr = line.Split('=')[1];
                                    }
                                }
                            }
                        }
                        //处理upldHistStr 还原出真实信息
                        if (!upldHistStr.Equals("")) // userid_datestring
                        {
                            try
                            {
                                upldHistStr = EnDeCode.decode(upldHistStr);
                                trialInfo.Activator = upldHistStr.Split('_')[0];
                                trialInfo.Unique = upldHistStr;
                            }
                            catch (Exception ex)
                            {
                                isNewUpld = true;
                                MyLogger.WriteLine(ex.Message);
                            }
                        }
                        else // 
                        {
                            isNewUpld = true;
                        }

                        if (!pltfmpdctStr.Equals(""))
                        {
                            trialInfo.Pltfm = pltfmpdctStr.Split('_')[0];
                            trialInfo.Pdct = pltfmpdctStr.Split('_')[1];
                        }
                        else
                        {
                            isNewUpld = true;
                        }

                    }
                    #endregion

                    //填写Trial info
                    FmWriteInfo fm = new FmWriteInfo(false, service, ref trialInfo,isNewUpld);
                    if (fm.ShowDialog() == DialogResult.OK)
                    {
                        labActivator.Text = trialInfo.Activator;
                        labOperator.Text = trialInfo.Operator;
                        labPltfm.Text = trialInfo.Pltfm;
                        labPdct.Text = trialInfo.Pdct;
                        labInfo.Text = trialInfo.Info;
                        labOther.Text = trialInfo.Other;
                        groupInfo.Visible = true;
                    }
                    else
                    {
                        txtFolder.Text = "";
                        groupInfo.Visible = false;
                    }
                }
                else
                {
                    txtFolder.Text = "";
                    MessageBox.Show("upload dir error:\r\n" + ContantInfo.UpldDir.upldDirErrDict[checkUpldDirCode], "message");
                }
            }
            else
            {
                MyLogger.WriteLine("Upload Canceld !");
            }
        }
        private void btUpld_Click(object sender, EventArgs e)
        {
            if (!groupInfo.Visible)
            {
                MessageBox.Show("Trial info not exist!", "error");
                return;
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("activator:" + trialInfo.Activator + "\n");
                sb.Append("operator:" + trialInfo.Operator + "\n");
                sb.Append("pltfm:" + trialInfo.Pltfm + "\n");
                sb.Append("pdct:" + trialInfo.Pdct + "\n");
                sb.Append("unique:"+trialInfo.Unique+" "+
                    TimeHandle.milSecondsToDatetime(long.Parse(trialInfo.Unique.Split('_')[1]))+"\n");
                sb.Append("info:" + trialInfo.Info+"\n");
                sb.Append("other:" + trialInfo.Other+"\n");

                //组装上传请求头
                upldHead = "upld:#";
                upldHead += trialInfo.Activator + "#" + trialInfo.Operator + "#" +
                            trialInfo.Unique + "#" + trialInfo.Pltfm + "#" + 
                            trialInfo.Pdct + "#" + trialInfo.Info + "#" + trialInfo.Other + "#";
                MyLogger.WriteLine("upldhead:"+upldHead);
                //return;
                //发送上传请求头
                try
                {
                    upldSock.Send(Encoding.UTF8.GetBytes(upldHead.ToCharArray()));
                    MyLogger.WriteLine("client upldSock 发送了请求头：" + upldHead);
                }
                catch
                {
                    upldRunFlg = false; // 中断上面的recvTh 线程
                    MessageBox.Show("与服务端断开连接！", "message");
                    MyLogger.WriteLine("-------upldSock 发送消息头时 遇到异常--------");
                }
            }
        }
        private void compress()
        {
            // root 是用户选择的要上传的目录
            DirectoryInfo root = new DirectoryInfo(upldPath);
            DirectoryInfo parent = root.Parent;

            //先遍历上传目录中所有的子目录
            List<DirectoryInfo> sonFolder = new List<DirectoryInfo>();
            FileHandle.traceFolder(root, sonFolder);

            // 在上传目录中创建一个上传文件夹
            string upldDirStr = root.FullName + "\\never_same_with_this\\";
            DirectoryInfo upldDir = new DirectoryInfo(upldDirStr);
            if (!upldDir.Exists)
            {
                Directory.CreateDirectory(upldDirStr);
                MyLogger.WriteLine("uploadDir:" + upldPath);
            }
            //先把root 中的子文件拷贝到临时目录
            foreach (FileInfo f in root.GetFiles())
            {
                //过滤掉重要的配置文件.upldhist.hist 及 info.txt
                if ((!f.Name.Equals(".upldhist.hist")) && (!f.Name.Equals("info.txt")))
                {
                    File.Copy(f.FullName, upldDirStr + f.Name);
                }
            }

            zipFileName = root.FullName + "\\never_same_with_this.zip";
            if (File.Exists(zipFileName))
            {
                try
                {
                    File.Delete(zipFileName);
                }
                catch { MyLogger.WriteLine("删除已有的zip文件错误!"); }
            }

            if (sonFolder.Count > 1 || (sonFolder.Count == 1 && !sonFolder[0].Name.Equals(root.Name))) // 目录中有子目录
            {
                foreach (DirectoryInfo d in sonFolder)
                {
                    if (d.GetFiles().Length != 0) //压缩有文件的目录 到临时上传文件中
                    {
                        // C:\data\DNData\20160817_Tag117_Slot3_DN\LHC_08-16-2016\Bin
                        // LHC_08-16-2016_Bin.zip ==> 到上传目录中
                        ZipFile.CreateFromDirectory(d.FullName, upldDirStr + d.Parent.Name + "_" + d.Name + ".zip");
                    }
                }
            }

            try
            {
                //然后压缩整个 临时文件到parent 目录下
                ZipFile.CreateFromDirectory(upldDir.FullName, zipFileName);
                compressCode = ContantInfo.Compress.PRESSOK;
                MyLogger.WriteLine("[compressed]:" + zipFileName);
            }
            catch (Exception ex)
            {
                compressCode = ContantInfo.Compress.ERROR; // 0 表示压缩出错
                MyLogger.WriteLine("[compressed error]:" + ex.Message);
            }

            //删除 DataDir_upload 这个临时目录
            List<FileInfo> fileList = new List<FileInfo>();
            FileHandle.traceAllFile(upldDir, fileList);
            //> 先删除 DataDir_upload 中的子文件
            foreach (FileInfo f in fileList)
            {
                File.Delete(f.FullName);
            }
            //> 再删除 DataDir_upload 这个目录
            Directory.Delete(upldDir.FullName);
            MyLogger.WriteLine("临时文件夹删除完成!");
        }
        private void upload()
        {
            int transMaxLen = 1024 * 512; //512k
            byte[] msgBuf = new byte[200];
            byte[] fileBuf = null;

            string msg = null;

            FileInfo file = new FileInfo(zipFileName);

            //首先传输文件头 file: # file_len # file_name #
            //接收response  elif: # file_name #
            msg = "file:#" + file.Length + "#" + file.Name + "#";
            msgBuf = Encoding.UTF8.GetBytes(msg.ToCharArray());
            upldSock.Send(msgBuf);

            ///等待服务端接收该文件的 response
            while (!ifRecv)
            {
                try { Thread.Sleep(500); }
                catch { }
                //进行一些超时处理
                //如果超时,continue
                MyLogger.WriteLine("wait recv response .....");
            }

            //然后是数据
            MyLogger.WriteLine("开始传输:" + file.Name + " 数据 \n");

            //上传进度条
            FmProgress upldProg = new FmProgress(this, file.Length, "upld");
            upldProg.Show();

            using (FileStream fs = new FileStream(file.FullName, FileMode.Open))
            {
                //文件一次传输可以完成
                if (file.Length < transMaxLen)
                {
                    fileBuf = new byte[file.Length];
                    fs.Read(fileBuf, 0, (int)file.Length);

                    //设置立即发送
                    upldSock.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);
                    upldSock.Send(fileBuf);
                    MyLogger.WriteLine(file.Name + "一次 文件传输完成!" + "\n");

                    //medial progress
                    upldProg.updateProgValue(fileBuf.Length, "");

                    try { Thread.Sleep(500); }
                    catch { Console.WriteLine("sleep error!"); }

                    //发送文件结束标志
                    msg = "end:#" + file.Name + "#";
                    msgBuf = Encoding.UTF8.GetBytes(msg.ToCharArray());
                    upldSock.Send(msgBuf);

                }
                //文件过大，需要分段传输
                else
                {
                    long transed = 0;
                    fileBuf = new byte[transMaxLen];
                    long fileLen = file.Length;
                    int times = (int)(fileLen / transMaxLen); //整数次
                    int leftLen = (int)(fileLen % transMaxLen);//剩下的字节数

                    //发送整数次
                    for (int i = 1; i <= times; i++)
                    {
                        fs.Read(fileBuf, 0, transMaxLen);
                        upldSock.Send(fileBuf);
                        transed += fileBuf.Length;

                        //进度条更新
                        upldProg.updateProgValue(transed, "");
                    }

                    //发送剩余的字节数
                    fileBuf = new byte[leftLen];
                    fs.Read(fileBuf, 0, leftLen);
                    transed += fileBuf.Length;
                    upldSock.Send(fileBuf);

                    upldProg.updateProgValue(transed, "");

                    //设置延时，使剩余文件信息和 文件结束标志分开发送
                    try { Thread.Sleep(500); }
                    catch { Console.WriteLine("sleep error!"); }

                    MyLogger.WriteLine(file.Name + " 数据传输完成!\n");

                    //最后是 end:# file_name # file_left #  
                    //接收response dne: # file_name #       
                    msg = "end:#" + file.Name + "#";
                    msgBuf = Encoding.UTF8.GetBytes(msg.ToCharArray());
                    upldSock.Send(msgBuf);
                    MyLogger.WriteLine("发送文件结束符:" + msg);

                    //结束
                    fs.Close();
                }
                //MessageBox.Show("文件上传成功!","message");

                if (isNewUpld)
                {
                    using (FileStream fs1 = new FileStream(upldPath + "\\.upldhist.hist", FileMode.Create))
                    {
                        using (StreamWriter sr1 = new StreamWriter(fs1))
                        {
                            sr1.WriteLine("pltfmpdct=" + trialInfo.Pltfm + "_" + trialInfo.Pdct);
                            sr1.WriteLine("token=" + EnDeCode.encode(trialInfo.Unique));
                        }
                    }
                }
                compressCode = ContantInfo.Compress.WAIT; // -1 是原始状态, 0 表示出错, 1 表示正常
            } // using(FileStreaam fs = new FileStream())

            try
            {
                File.Delete(zipFileName);
                MyLogger.WriteLine("zip文件删除成功!\n");
            }
            catch { MyLogger.WriteLine("zip文件删除异常!\n"); }
        }

        //重新设置 trial info 信息
        private void btInfoModify_Click(object sender, EventArgs e)
        {
            FmWriteInfo fm = new FmWriteInfo(false, service, ref trialInfo,isNewUpld);
            if(fm.ShowDialog() == DialogResult.OK)
            {
                labActivator.Text = trialInfo.Activator;
                labOperator.Text = trialInfo.Operator;
                labPltfm.Text = trialInfo.Pltfm;
                labPdct.Text = trialInfo.Pdct;
                labInfo.Text = trialInfo.Info;
                labOther.Text = trialInfo.Other;
                groupInfo.Visible = true;
            }
            else
            {
                groupInfo.Visible = false;
            }
        }

        //更改密码的按钮事件
        private void btChgPswd_Click(object sender, EventArgs e)
        {
            FmChgPswd fm = new FmChgPswd(service, curUser);
            fm.ShowDialog();
        }
    }
}
