using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataSyncSystem.Dao;
using DataSyncSystem.Utils;
using DataSyncSystem.SelfView;
using System.Threading;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.IO.Compression;

namespace DataSyncSystem
{
    public partial class FmMain : Form
    {
        public User curUser = null; 
        DataService service = new DataService();
        public static Dictionary<string, string> userDict = null; //这个用来通过userid 友好的显示名字

        #region panMain 中需要的功能部件
        //显示相关的控制辅助
        private Label[] pmLabs = new Label[7]; // 0root 1>> 2pltfm 3>> 4pdct  5>> 6trial 
        public string[] pmLabTexts = new string[7];
        private Panel[] pmPans = new Panel[4]; // 0pltfm 1pdct 2trials 3trial 
        public int pmCurLab = 1; //显示到 1>>
        public int pmCurPan = 0; //显示 0pltfm

        //pmPanPltfms panel中所需的功能部件
        public List<Platform> pmPltfmList = null;
        public int pmPltfmPgSize = 9; //每页显示9条数据
        public int pmPltfmPgNow = 1;
        public int pmPltfmPgAll = 0;
        /****控制画图相关部件 ***/
        public int pmPltfmStartX = 150;
        public int pmPltfmStartY = 30;
        public int pmPltfmWoffset = 60;
        public int pmPltfmHoffset = 25;
        public int pmPltfmLineSize = 3; //每行显示3个
        //页脚开始位置信息
        public int pmPltfmPgStartX = 210;
        public int pmPltfmPgStartY = 465;
        public int pmPltfmPgShow = 3;  // 页脚显示多少页

        //pmPanPdcts panel 中所需的功能部件
        public List<Product> pmPdctList = null;
        public int pmPdctPgSize = 9; //每页显示9条数据
        public int pmPdctPgNow = 1;
        public int pmPdctPgAll = 0;
        /****控制画图相关部件 ***/
        public int pmPdctStartX = 150;
        public int pmPdctStartY = 30;
        public int pmPdctWoffset = 60;
        public int pmPdctHoffset = 25;
        public int pmPdctLineSize = 3;//每行显示3个
        //页脚开始位置信息
        public int pmPdctPgStartX = 210;
        public int pmPdctPgStartY = 465;
        public int pmPdctPgShow = 3;  // 页脚显示多少页

        //pmPanTrials panel 中所需的功能部件
        public List<Trial> pmTrialsList = null;
        public int pmTrialPgSize = 9; //每页显示9条数据
        public int pmTrialPgNow = 1;
        public int pmTrialPgAll = 0;
        /****控制画图相关部件 ***/
        public int pmTrialStartX = 120;
        public int pmTrialStartY = 35;
        public int pmTrialWoffset = 60;
        public int pmTrialHoffset = 35;
        public int pmTrialLineSize = 3; //控制换行相关
        //页脚开始位置信息
        public int pmTrialPgStartX = 130;
        public int pmTrialPgStartY = 430;
        public int pmTrialPgShow = 3; //页脚显示4个页码

        //pmPanHeads panel 中所需的功能部件
        public Trial pmHeadShowTrial = null;

        #endregion

        #region panMyUpload 中需要的功能部件
        //显示相关的控制辅助
        public List<Trial> plUploadList = null;
        public string plUploaderId;
        /** 分页相关 **/
        public int plUploadStartX = 90;
        public int plUploadStartY = 26;
        public int plUploadHoffset = 25;
        public int plUploadPgAll = 0;
        public int plUploadPgNow = 1;
        public int plUploadPgSize = 8; //每页显示多少条数据
        public int plUploadPgStartX = 125;
        public int plUploadPgStartY = 550;
        public int plUploadPgShow = 4; //页脚显示多少页码

        #endregion

        #region panSetting 中需要的功能部件
        List<Control> psControlList = new List<Control>();
        bool ifPsSettingCan = true; //button setting 可用
        bool ifPsOkCan = false; //button ok 不可用
        List<string> psPltfmNames = null;
        List<string> psPdctNames = null;
        #endregion

        #region 文件传输部件
        //文件上传
        Socket upldSock = null;
        volatile bool ifRecv = false; 
        string upldHead = null; // upld请求头
        string upldPath = "";  // upld 的路径
        string zipFileName = "";  // upld的临时文件压缩的目录 
        TrialInfo trialInfo = null;
        volatile bool isNewUpld = true;  // 表示是否是新的上传
        string upldHistStr = ""; // .upldhist.hist 中存储的字符串
        string pltfmpdctStr = ""; // .upldhist.hist 中存储的pltfm , pdct 

        //文件下载
        FolderBrowserDialog dnldDialog = new FolderBrowserDialog();
        public string dnldPath = "";
        Socket dnldSock = null;
        string dnldHead = null;
        volatile bool dnldOk ;

        public volatile bool dnldRunFlg;
        volatile bool upldRunFlg;
        volatile int compressCode = ContantInfo.Compress.WAIT;

        #endregion

        public FmMain()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            #region 初始化用于设置显示控制的panMain 中的labels 数组及 Pannels 数组
            pmLabs[0] = pmLabRoot;
            pmLabTexts[0] = "Root";
            pmLabs[1] = pmLabNext0;
            pmLabTexts[1] = ">>";
            pmLabs[2] = pmLabPlatform;
            pmLabTexts[2] = "";
            pmLabs[3] = pmLabNext1;
            pmLabTexts[3] = ">>";
            pmLabs[4] = pmLabProduct;
            pmLabTexts[4] = "";
            pmLabs[5] = pmLabNext2;
            pmLabTexts[5] = ">>";
            pmLabs[6] = pmLabTrial;
            pmLabTexts[6] = "";

            pmPans[0] = pmPanPltfms;
            pmPans[1] = pmPanPdcts;
            pmPans[2] = pmPanTrials;
            pmPans[3] = pmPanHeads;
            #endregion

            #region 初始化用于设置显示控制的panMyUpload 中的labels 数组及 Pannels 数组
            #endregion

            #region 初始化设置显示控制的 panSettting 中的control 数组
            psControlList.Add(psCombPltfm);
            psControlList.Add(psCombPdct);
            psControlList.Add(psTxtDnldPath);
            psControlList.Add(psBtChseDnldPath);
            #endregion

            //设置panSetting 中界面的显示
            setPsPanel();
        }

        #region form 的初始化和消亡设置
        private void FmMain_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;

            GetCsvSock.init(this);
            initDnldSock();
            initUpldSock();

            //界面打开先显示panel1 
            panMain.Visible = true;
            btMain.BackColor = Color.Red;

            //获取要panMain 中要显示的初始信息
            pmPltfmList = service.getPltfmPageList(pmPltfmPgNow, pmPltfmPgSize,ref pmPltfmPgAll);

            //显示用户信息
            curUser = service.getUserByUserId(Cache.userId);
            labUserName.Text = curUser.UserName;
            labUserTeam.Text = curUser.TeamName;
            labUserTel.Text = curUser.UserTel;

            //我的上传界面中默认显示当前用户的
            plUploaderId = curUser.UserId;
            plUploadList = service.getTrialPageList(plUploaderId, plUploadPgNow, plUploadPgSize, ref plUploadPgAll);

            #region 根据用户配置预先显示panel main 中的内容
            FileInfo cfg = new FileInfo(Environment.CurrentDirectory + "\\" + ".datasync.cfg");
            if (cfg.Exists)
            {
                FileStream fs = new FileStream(cfg.FullName, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string tmp = null;
                while((tmp=sr.ReadLine()) != null)
                {
                    if (tmp.StartsWith("pltfm"))
                    {
                        tmp = tmp.Split('=')[1];
                        psCombPltfm.Text = tmp;
                        Cache.pltfm = tmp;
                    }
                    if (tmp.StartsWith("pdct"))
                    {
                        tmp = tmp.Split('=')[1];
                        psCombPdct.Text = tmp;
                        Cache.pdct = tmp;
                    }
                    if (tmp.StartsWith("path"))
                    {
                        tmp = tmp.Split('=')[1];
                        psTxtDnldPath.Text = tmp;
                        Cache.dnldPath = tmp;
                    }
                }
                sr.Close();
                fs.Close();
            }
            if(!Cache.pltfm.Equals(""))
            {
                pmPdctList = service.getPdctPageList(pmPdctPgNow, pmPdctPgSize, Cache.pltfm, ref pmPdctPgAll);
                pmCurLab += 2;
                pmCurPan += 1;
                pmLabTexts[pmCurLab - 1] = Cache.pltfm;
                if (!Cache.pdct.Equals(""))
                {
                    pmTrialsList = service.getTrPgByPdct(Cache.pltfm, Cache.pdct, pmTrialPgNow, pmTrialPgSize,
                                                         ref pmTrialPgAll);
                    pmCurLab += 2;
                    pmCurPan += 1;
                    pmLabTexts[pmCurLab - 1] = Cache.pdct;
                }
            }
            setCurPmLab();
            setCurPmPan();
            #endregion

            userDict = service.getAllUserDict();
        }

        private void FmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            FileStream fs = new FileStream(Environment.CurrentDirectory + "\\client.log", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(MyLogger.buf);
            sw.WriteLine("==========" + DateTime.Now.ToLocalTime() + "==================");
            sw.Flush();
            sw.Close();
            fs.Close();
        }

        private void FmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            service.closeCon();

            //关闭下载 csv 的socket
            try
            {
                GetCsvSock.sock.Shutdown(SocketShutdown.Both);
                GetCsvSock.sock.Close();
            }
            catch
            {
                MyLogger.WriteLine("关闭csvsock 遇到异常!");
            }

            // 关闭负责下载的socket
            if(dnldSock != null)
            {
                try
                {
                    dnldSock.Shutdown(SocketShutdown.Both);
                    dnldSock.Close();
                }
                catch
                {
                    MyLogger.WriteLine("关闭dnldSock 遇到异常!");
                }
            }

            //关闭负责上传的socket
            if(upldSock != null)
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
        #endregion

        #region 侧边栏的三个按钮的点击事件
        //切换到MainPanel
        private void btMain_Click(object sender, EventArgs e)
        {
            if (!panMain.Visible) { panMain.Visible = true; btMain.BackColor = Color.Red; }
            if (panMyLoad.Visible) { panMyLoad.Visible = false; btMyLoad.BackColor = Color.White; }
            if (panSetting.Visible) { panSetting.Visible = false; btSetting.BackColor = Color.White; }

            if (!btMyLoad.Text.Equals("my upload")) { btMyLoad.Text = "my upload"; }
        }

        //切换到MyUploadPanel
        private void btMyLoad_Click(object sender, EventArgs e)
        {
            if (!panMyLoad.Visible) { panMyLoad.Visible = true; btMyLoad.BackColor = Color.Red; }
            if (panMain.Visible) { panMain.Visible = false; btMain.BackColor = Color.White;  }
            if (panSetting.Visible) { panSetting.Visible = false;btSetting.BackColor = Color.White; }

            //使用DataService 获取plUploader 的上传记录列表
            if (!plUploaderId.Equals(curUser.UserId))
            {
                plUploaderId = curUser.UserId;
                plUploadPgNow = 1;
                plUploadList = service.getTrialPageList(plUploaderId, plUploadPgNow, plUploadPgSize, ref plUploadPgAll);
                panMyLoad.Controls.Clear();
                panMyLoad.Refresh();
            }
        }

        //切换到setting panel 
        private void btSetting_Click(object sender, EventArgs e)
        {
            if (!panSetting.Visible) { panSetting.Visible = true; btSetting.BackColor = Color.Red; }
            if (panMain.Visible) { panMain.Visible = false; btMain.BackColor = Color.White; }
            if (panMyLoad.Visible) { panMyLoad.Visible = false; btMyLoad.BackColor = Color.White; }

            if (!btMyLoad.Text.Equals("my upload")) { btMyLoad.Text = "my upload"; }
        }
        #endregion

        private void panMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panMyLoad_Paint(object sender, PaintEventArgs e)
        {
            int newX = plUploadStartX;
            int newY = plUploadStartY;
            if(plUploadList != null)
            {
                //页面信息
                for (int i = 0; i < plUploadList.Count; i++)
                {
                    UploadRecord record = new UploadRecord(plUploadList[i], panMyLoad.BackColor
                                              ,this,dnldDialog,dnldSock);
                    record.Location = new Point(newX, newY);
                    newY += (record.Height + plUploadHoffset);
                    panMyLoad.Controls.Add(record);
                }

                //页脚信息
                int lastX = plUploadPgStartX;
                ShowPage.showPg(plUploadPgNow, plUploadPgAll, plUploadPgShow, panMyLoad, lastX, 
                                plUploadPgStartY, new EventHandler(uploadPage_Click));

                /*
                #region 画出页脚信息
                #region "<<"
                if (plUploadPgNow > 1)
                {
                    Button btPre = new Button();
                    btPre.Text = "<<";
                    btPre.Location = new Point(plUploadPgStartX, plUploadPgStartY);
                    btPre.Size = new Size(60, 25);
                    btPre.BackColor = Color.White;
                    btPre.ForeColor = Color.Black;
                    btPre.UseVisualStyleBackColor = true;
                    btPre.Click += new System.EventHandler(this.uploadPage_Click);
                    panMyLoad.Controls.Add(btPre);
                    lastX = plUploadPgStartX + 60 + offset;
                }
                #endregion

                #region 页中 
                int tmp = lastX;
                for (int i = 1; i <= plUploadPgAll; i++)
                {
                    Button bt = new Button();
                    bt.Text = i + "";
                    bt.Location = new Point(lastX + (i - 1) * (60 + offset), plUploadPgStartY);
                    bt.Size = new Size(60, 25);
                    if (plUploadPgNow == i)
                        bt.BackColor = Color.Red;
                    else
                        bt.BackColor = Color.White;
                    bt.ForeColor = Color.Black;
                    bt.Click += new System.EventHandler(this.uploadPage_Click);
                    panMyLoad.Controls.Add(bt);
                    MyLogger.WriteLine("button :" + bt.Location.X + "," + bt.Location.Y);

                    tmp = bt.Location.X;
                }
                lastX = tmp + 60 + offset;
                #endregion

                #region ">>"
                if (plUploadPgNow < plUploadPgAll)
                {
                    Button btNext = new Button();
                    btNext.Text = ">>";
                    btNext.Location = new Point(lastX, plUploadPgStartY);
                    btNext.Size = new Size(60, 25);
                    btNext.BackColor = Color.White;
                    btNext.ForeColor = Color.Black;
                    btNext.UseVisualStyleBackColor = true;
                    btNext.Click += new System.EventHandler(this.uploadPage_Click);
                    panMyLoad.Controls.Add(btNext);
                    MyLogger.WriteLine("button :" + btNext.Location.X + "," + btNext.Location.Y);
                }
                #endregion
                #endregion
                */
            }
            else
            {
                MyLogger.WriteLine("no my upload to show !");
            }
        }
        #region panMyLoad 中的页点击事件
        private void uploadPage_Click(object sender,EventArgs e)
        {
            Button bt = (Button)sender;
            string btText = bt.Text.Trim();
            bool isNewGet = true;

            if (btText.Equals("<<"))
            {
                plUploadPgNow -= 1;
            }
            else if (btText.Equals(">>"))
            {
                plUploadPgNow += 1;
            }
            else
            {
                //如果点击的不是当前页，才重新获取数据
                if (plUploadPgNow != Int32.Parse(btText))
                {
                    plUploadPgNow = Int32.Parse(btText);
                }
                else
                {
                    isNewGet = false;
                }
            }

            if (isNewGet)
            {
                //使用DataService 获取页面数据
                plUploadList = service.getTrialPageList(plUploaderId, plUploadPgNow, plUploadPgSize, ref plUploadPgAll);
                panMyLoad.Controls.Clear();
                panMyLoad.Refresh();
            }
        }
        #endregion

        //上传.zip 文件到服务器===============================================
        private void initUpldSock()
        {
            //先连接服务端,获得一个socket
            upldSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(ContantInfo.SockServ.ip);
            try
            {
                upldSock.Connect(ip, Int32.Parse(ContantInfo.SockServ.port));
                upldRunFlg = true;

                //开启线程监听server 的 响应
                Thread recvTh = new Thread(recv);
                recvTh.IsBackground = true;
                recvTh.Start();
            }
            catch (Exception err)
            {
                MessageBox.Show("upload socket连接失败！", "warning");
                MyLogger.WriteLine(err.Message);
            }
        }
        private void btUpload_Click(object sender, EventArgs e)
        {
            isNewUpld = true;
            trialInfo = new TrialInfo();

            //首先选择Data folder
            FolderBrowserDialog browser = new FolderBrowserDialog();
            if(browser.ShowDialog() == DialogResult.OK)
            {
                upldPath = browser.SelectedPath;

                //检查路径的合法性
                int upldDirChkCode = 0;
                if(FileHandle.checkUpldDir(upldPath, ref upldDirChkCode))
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
                    FmWriteInfo fm = new FmWriteInfo(false, service, ref trialInfo, isNewUpld);
                    if (fm.ShowDialog() == DialogResult.OK)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("Activator:" + trialInfo.Activator + "\n");
                        sb.Append("Operator:" + trialInfo.Operator + "\n");
                        sb.Append("Platform:" + trialInfo.Pltfm + "\n");
                        sb.Append("Product:" + trialInfo.Pdct + "\n");
                        sb.Append("Info:" + trialInfo.Info + "\n");
                        sb.Append("Other:" + trialInfo.Other + "\n");

                        if(MessageBox.Show(sb.ToString(), "trial info check",MessageBoxButtons.OKCancel) != DialogResult.Cancel)
                        {
                            //组装上传请求头
                            upldHead = "upld:#";
                            upldHead += trialInfo.Activator + "#" + trialInfo.Operator + "#" +
                                        trialInfo.Unique + "#" + trialInfo.Pltfm + "#" +
                                        trialInfo.Pdct + "#" + trialInfo.Info + "#" + trialInfo.Other + "#";
                            MyLogger.WriteLine("upldhead:" + upldHead);

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
                        else
                        {
                            MyLogger.WriteLine("upload cancel !");
                        }
                    }
                    else // trial info not complete
                    {
                        MessageBox.Show("Trial info not complete !", "upload cancel");
                    }
                }
                else
                {
                    MessageBox.Show("upload dir error:\r\n" + ContantInfo.UpldDir.upldDirErrDict[upldDirChkCode], "message");
                }
            }
            else
            {
                MyLogger.WriteLine("用户取消了上传!");
                return;
            }
        }
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
                    }
                }//while(!endFlg)
                MyLogger.WriteLine("客户端监听任务结束!");
            } //while()
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

            if(sonFolder.Count>1 || (sonFolder.Count==1 && !sonFolder[0].Name.Equals(root.Name))) // 目录中有子目录
            {
                foreach (DirectoryInfo d in sonFolder)
                {
                    if (d.GetFiles().Length != 0) //压缩有文件的目录 到临时上传文件中
                    {
                        // C:\data\DNData\20160817_Tag117_Slot3_DN\LHC_08-16-2016\Bin
                        // LHC_08-16-2016_Bin.zip ==> 到上传目录中
                        try
                        {
                            ZipFile.CreateFromDirectory(d.FullName, upldDirStr + d.Parent.Name + "_" + d.Name + ".zip");
                        }
                        catch { MyLogger.WriteLine("compress error !\n" + (upldDirStr + d.Parent.Name + "_" + d.Name + ".zip")); }
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
            FmProgress upldProg = new FmProgress(this,file.Length,"upld");
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
                    upldProg.updateProgValue(fileBuf.Length,"");

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
                        upldProg.updateProgValue(transed,"");
                    }

                    //发送剩余的字节数
                    fileBuf = new byte[leftLen];
                    fs.Read(fileBuf, 0, leftLen);
                    transed += fileBuf.Length;
                    upldSock.Send(fileBuf);

                    upldProg.updateProgValue(transed,"");

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
                    using(FileStream fs1 = new FileStream(upldPath+"\\.upldhist.hist", FileMode.Create))
                    {
                        using(StreamWriter sr1 = new StreamWriter(fs1))
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

        #region 主页panMain 中最上方 lab 的点击事件
        private void pmLabRoot_Click(object sender, EventArgs e)
        {
            //初始化pmPltfm 的page显示
            //初始化pmTrials 的page显示
            //>> 直接从pmTrial调到这里
            if(pmCurLab >= 4)
            {
                pmTrialPgNow = 1;
                pmPdctPgNow = 1;

            }
            else //从平台过来的
            {
                pmPdctPgNow = 0;
            }

            pmCurLab = 1;
            pmCurPan = 0;
            setCurPmLab();
            setCurPmPan();
        }

        private void pmLabPlatform_Click(object sender, EventArgs e)
        {
            //初始化pmTrial 的page显示
            pmTrialPgNow = 1;

            pmCurLab = 2 + 1;
            pmCurPan = 1;
            setCurPmLab();
            setCurPmPan();
        }

        private void pmLabProduct_Click(object sender, EventArgs e)
        {
            pmCurLab = 4 + 1;
            pmCurPan = 2;
            setCurPmLab();
            setCurPmPan();
        }
        #endregion

        #region 设置panMain 中上方的labels ，和下面的各个子panel的显示隐藏
        public void setCurPmLab()
        {
            for(int i=0; i<pmLabs.Length; i++)
            {
                if(i<= pmCurLab)
                {
                    pmLabs[i].Visible = true;

                    //设置lab 的位置
                    if (i != 0)
                    {
                        pmLabs[i].Location = new Point(pmLabs[i - 1].Location.X + pmLabs[i - 1].Width + 15, pmLabs[i - 1].Location.Y);
                    }

                    //这里判断是不是6 主要是因为不使用Cache 的话，点击Trial时的 userId_trialDate 保存不下来
                    if(i != 6) { pmLabs[i].Text = pmLabTexts[i]; }
                    else { pmLabs[i].Text = Cache.trialUniqueStr; }
                }
                else
                {
                    pmLabs[i].Visible = false;
                }
            }
        }
        public void setCurPmPan()
        {
            for(int i=0; i<pmPans.Length; i++)
            {
                if(pmCurPan == i)
                {
                    pmPans[i].Visible = true;
                }
                else
                {
                    pmPans[i].Visible = false;
                }
            }
        }
        #endregion

        #region panel main 中的pltfm , pdct , trials , trial 的paint事件

        private void pmPanPltfms_Paint(object sender, PaintEventArgs e)
        {
            int judge = 0;
            int newX = 0;
            int newY = 0;
            if(pmPltfmList != null)
            {
                //画出页面中的内容
                #region 页面中platform Item
                for (int i = 0; i < pmPltfmList.Count; i++)
                {
                    MyPlatform pltfm = new MyPlatform(pmPltfmList[i].PltfmName, pmPanPltfms.BackColor, this);
                    newX = pmPltfmStartX + (judge % pmPltfmLineSize) * (pmPltfmWoffset + pltfm.Width);
                    if (judge % pmPltfmLineSize == 0)
                    {
                        newY = pmPltfmStartY + (judge / pmPltfmLineSize) * (pmPltfmHoffset + pltfm.Height);
                    }
                    pltfm.Location = new Point(newX, newY);
                    pmPanPltfms.Controls.Add(pltfm);

                    judge++;
                }
                #endregion

                //画出页脚
                int lastX = pmPltfmPgStartX;
                ShowPage.showPg(pmPltfmPgNow, pmPltfmPgAll, pmPltfmPgShow, pmPanPltfms, lastX, 
                                 pmPltfmPgStartY, new EventHandler(pltfmsPage_Click));
                /*
                #region 画出分页信息

                #region 上一页 "<<"
                //"<<"
                if (pmPltfmPgNow > 1)
                {
                    Button btPre = new Button();
                    btPre.Text = "<<";
                    btPre.Location = new Point(pmPltfmPgStartX, pmPltfmPgStartY);
                    btPre.Size = new Size(60, 25);
                    btPre.BackColor = Color.White;
                    btPre.ForeColor = Color.Black;
                    btPre.UseVisualStyleBackColor = true;
                    btPre.Click += new System.EventHandler(this.pltfmsPage_Click);
                    pmPanPltfms.Controls.Add(btPre);
                    MyLogger.WriteLine("button :" + btPre.Location.X + "," + btPre.Location.Y);

                    lastX = pmPltfmPgStartX + 60 + offset;
                }
                #endregion

                #region 页中
                int tmp = lastX;
                for (int i = 1; i <= pmPltfmPgAll; i++)
                {
                    Button bt = new Button();
                    bt.Text = i+"";
                    bt.Location = new Point(lastX+(i-1)*(60+offset), pmPltfmPgStartY);
                    bt.Size = new Size(60, 25);
                    if (pmPltfmPgNow == i)
                        bt.BackColor = Color.Red;
                    else
                        bt.BackColor = Color.White;
                    bt.ForeColor = Color.Black;
                    bt.Click += new System.EventHandler(this.pltfmsPage_Click);
                    pmPanPltfms.Controls.Add(bt);
                    MyLogger.WriteLine("button :" + bt.Location.X + "," + bt.Location.Y);


                    tmp = bt.Location.X;
                }
                lastX = tmp+60+offset;
                #endregion

                #region 下一页 ">>"
                if (pmPltfmPgNow < pmPltfmPgAll)
                {
                    Button btNext = new Button();
                    btNext.Text = ">>";
                    btNext.Location = new Point(lastX, pmPltfmPgStartY);
                    btNext.Size = new Size(60, 25);
                    btNext.BackColor = Color.White;
                    btNext.ForeColor = Color.Black;
                    btNext.UseVisualStyleBackColor = true;
                    btNext.Click += new System.EventHandler(this.pltfmsPage_Click);
                    pmPanPltfms.Controls.Add(btNext);
                    MyLogger.WriteLine("button :" + btNext.Location.X + "," + btNext.Location.Y);
                }
                #endregion

                #endregion
                */
            }
            else
            {
                //Panel 设置相应的操作，告诉用户没有数据
            }
        }
        #region pmPanPltfms 中页点击事件
        private void pltfmsPage_Click(object sender , EventArgs e)
        {
            Button bt = (Button)sender;
            string btText = bt.Text.Trim();
            bool isNewGet = true;

            if (btText.Equals("<<"))
            {
                pmPltfmPgNow -= 1;
            }
            else if (btText.Equals(">>"))
            {
                pmPltfmPgNow += 1;
            }
            else
            {
                //如果点击的不是当前页，才重新获取数据
                if(pmPltfmPgNow != Int32.Parse(btText))
                {
                    pmPltfmPgNow = Int32.Parse(btText);
                }
                else
                {
                    isNewGet = false;
                }
            }

            if (isNewGet)
            {
                //使用DataService 获取页面数据
                pmPltfmList = service.getPltfmPageList(pmPltfmPgNow, pmPltfmPgSize, ref pmPltfmPgAll);
                pmPanPltfms.Controls.Clear();
                pmPanPltfms.Refresh();
            }
        }
        #endregion

        private void pmPanPdcts_Paint(object sender, PaintEventArgs e)
        {
            int judge = 0;
            int newX = 0;
            int newY = 0;

            if (pmPdctList != null)
            {
                #region 画出页面中 Product Item
                for (int i = 0; i < pmPdctList.Count; i++)
                {
                    MyProduct pdct = new MyProduct(pmPdctList[i].PdctName, pmPanPdcts.BackColor, this);
                    newX = pmPdctStartX + (judge % pmPdctLineSize) * (pmPdctWoffset + pdct.Width);
                    if (judge % pmPdctLineSize == 0)
                    {
                        newY = pmPdctStartY + (judge / pmPdctLineSize) * (pmPdctHoffset + pdct.Height);
                    }
                    pdct.Location = new Point(newX, newY);
                    pmPanPdcts.Controls.Add(pdct);
                    judge++;
                }
                #endregion

                //画出页脚
                int lastX = pmPdctPgStartX;
                ShowPage.showPg(pmPdctPgNow, pmPdctPgAll, pmPdctPgShow, pmPanPdcts, lastX, 
                                pmPdctPgStartY, new EventHandler(pdctsPage_Click));
                /*
                if (pmPdctPgNow > 1)
                {
                    Button btPre = new Button();
                    btPre.Text = "<<";
                    btPre.Location = new Point(pmPdctPgStartX, pmPdctPgStartY);
                    btPre.Size = new Size(60, 25);
                    btPre.BackColor = Color.White;
                    btPre.ForeColor = Color.Black;
                    btPre.UseVisualStyleBackColor = true;
                    btPre.Click += new System.EventHandler(this.pdctsPage_Click);
                    pmPanPdcts.Controls.Add(btPre);
                    MyLogger.WriteLine("button :" + btPre.Location.X + "," + btPre.Location.Y);

                    lastX = pmPdctPgStartX + 60 + offset;
                }
                int tmp = lastX;
                for (int i = 1; i <= pmPdctPgAll; i++)
                {
                    Button bt = new Button();
                    bt.Text = i + "";
                    bt.Location = new Point(lastX + (i - 1) * (60 + offset), pmPdctPgStartY);
                    bt.Size = new Size(60, 25);
                    if (pmPdctPgNow == i)
                        bt.BackColor = Color.Red;
                    else
                        bt.BackColor = Color.White;
                    bt.ForeColor = Color.Black;
                    bt.Click += new System.EventHandler(this.pdctsPage_Click);
                    pmPanPdcts.Controls.Add(bt);
                    MyLogger.WriteLine("button :" + bt.Location.X + "," + bt.Location.Y);

                    tmp = bt.Location.X;
                }
                lastX = tmp + 60 + offset;
                if (pmPdctPgNow < pmPdctPgAll)
                {
                    Button btNext = new Button();
                    btNext.Text = ">>";
                    btNext.Location = new Point(lastX, pmPdctPgStartY);
                    btNext.Size = new Size(60, 25);
                    btNext.BackColor = Color.White;
                    btNext.ForeColor = Color.Black;
                    btNext.UseVisualStyleBackColor = true;
                    btNext.Click += new System.EventHandler(this.pdctsPage_Click);
                    pmPanPdcts.Controls.Add(btNext);
                    MyLogger.WriteLine("button :" + btNext.Location.X + "," + btNext.Location.Y);
                }
                */
            }
            else
            {
                //Panel 设置相应的操作，告诉用户没有数据
                MyLogger.WriteLine("获取到的Product List 为空!");
            }
        }
        #region pmPanPdcts 页点击事件
        private void pdctsPage_Click(object sender,EventArgs e)
        {
            Button bt = (Button)sender;
            string btText = bt.Text.Trim();
            bool isNewGet = true;

            if (btText.Equals("<<"))
            {
                pmPdctPgNow -= 1;
            }
            else if (btText.Equals(">>"))
            {
                pmPdctPgNow += 1;
            }
            else
            {
                //如果点击的不是当前页，才重新获取数据
                if (pmPdctPgNow != Int32.Parse(btText))
                {
                    pmPdctPgNow = Int32.Parse(btText);
                }
                else
                {
                    isNewGet = false;
                }
            }

            if (isNewGet)
            {
                //使用DataService 获取页面数据
                pmPdctList = service.getPdctPageList(pmPdctPgNow, pmPdctPgNow, pmLabTexts[2], ref pmPdctPgAll);
                pmPanPdcts.Controls.Clear();
                pmPanPdcts.Refresh();
            }
        }
        #endregion

        private void pmPanTrials_Paint(object sender, PaintEventArgs e)
        {
            int judge = 0;
            int newX = 0;
            int newY = 0;
            if (pmTrialsList != null)
            {
                #region 画出页面中的 Trail Item
                for (int i = 0; i < pmTrialsList.Count; i++)
                {
                    MyTrial trial = new MyTrial(pmTrialsList[i], this);
                    newX = pmTrialStartX + (judge % pmTrialLineSize) * (pmTrialWoffset + trial.Width);
                    if (judge % pmTrialLineSize == 0)
                    {
                        newY = pmTrialStartY + (judge / pmTrialLineSize) * (pmTrialHoffset + trial.Height);
                    }
                    trial.Location = new Point(newX, newY);
                    pmPanTrials.Controls.Add(trial);

                    judge++;
                }
                #endregion

                //画出页脚
                int lastX = pmTrialPgStartX;
                ShowPage.showPg(pmTrialPgNow, pmTrialPgAll, pmTrialPgShow, pmPanTrials, lastX,
                               pmTrialPgStartY, new EventHandler(trialsPage_Click));
                /*
                #region 画出页信息

                #region "<<"
                if (pmTrialPgNow > 1)
                {
                    Button btPre = new Button();
                    btPre.Text = "<<";
                    btPre.Location = new Point(pmTrialPgStartX, pmTrialPgStartY);
                    btPre.Size = new Size(60, 25);
                    btPre.BackColor = Color.White;
                    btPre.ForeColor = Color.Black;
                    btPre.UseVisualStyleBackColor = true;
                    btPre.Click += new System.EventHandler(this.trialsPage_Click);
                    pmPanTrials.Controls.Add(btPre);
                    MyLogger.WriteLine("button :" + btPre.Location.X + "," + btPre.Location.Y);

                    lastX = pmTrialPgStartX + 60 + offset;
                }
                #endregion

                #region 页中
                int tmp = lastX;
                for (int i = 1; i <= pmTrialPgAll; i++)
                {
                    Button bt = new Button();
                    bt.Text = i + "";
                    bt.Location = new Point(lastX + (i - 1) * (60 + offset), pmTrialPgStartY);
                    bt.Size = new Size(60, 25);
                    if (pmTrialPgNow == i)
                        bt.BackColor = Color.Red;
                    else
                        bt.BackColor = Color.White;
                    bt.ForeColor = Color.Black;
                    bt.Click += new System.EventHandler(this.trialsPage_Click);
                    pmPanTrials.Controls.Add(bt);
                    MyLogger.WriteLine("button :" + bt.Location.X + "," + bt.Location.Y);

                    tmp = bt.Location.X;
                }
                lastX = tmp + 60 + offset;
                #endregion

                #region ">>"
                if (pmTrialPgNow < pmTrialPgAll)
                {
                    Button btNext = new Button();
                    btNext.Text = ">>";
                    btNext.Location = new Point(lastX, pmTrialPgStartY);
                    btNext.Size = new Size(60, 25);
                    btNext.BackColor = Color.White;
                    btNext.ForeColor = Color.Black;
                    btNext.UseVisualStyleBackColor = true;
                    btNext.Click += new System.EventHandler(this.trialsPage_Click);
                    pmPanTrials.Controls.Add(btNext);
                    MyLogger.WriteLine("button :" + btNext.Location.X + "," + btNext.Location.Y);
                }
                #endregion

                #endregion
                */
            }
            else
            {
                //Panel 设置相应的操作，告诉用户没有数据
                MyLogger.WriteLine("获取到的Product List 为空!");
            }
        }
        #region pmPanTrials 页点击事件
        private void trialsPage_Click(object sender , EventArgs e)
        {
            Button bt = (Button)sender;
            string btText = bt.Text.Trim();
            bool isNewGet = true;

            if (btText.Equals("<<"))
            {
                pmTrialPgNow -= 1;
            }
            else if (btText.Equals(">>"))
            {
                pmTrialPgNow += 1;
            }
            else
            {
                //如果点击的不是当前页，才重新获取数据
                if (pmTrialPgNow != Int32.Parse(btText))
                {
                    pmTrialPgNow = Int32.Parse(btText);
                }
                else
                {
                    isNewGet = false;
                }
            }

            if (isNewGet)
            {
                //使用DataService 获取页面数据
                pmTrialsList = service.getTrPgByPdct(pmLabTexts[2], pmLabTexts[4], pmTrialPgNow, pmTrialPgSize, ref pmTrialPgAll);
                pmPanTrials.Controls.Clear();
                pmPanTrials.Refresh();
            }
        }
        #endregion

        private void pmPanHeads_Paint(object sender, PaintEventArgs e)
        {
            if(pmHeadShowTrial != null)
            {
                if(userDict == null && userDict.Count==0)
                    pmHeadLabUserId.Text = pmHeadShowTrial.TrUserId;
                else
                    pmHeadLabUserId.Text = userDict[pmHeadShowTrial.TrUserId];
                if (userDict == null && userDict.Count == 0)
                    pmHeadLabOperator.Text = pmHeadShowTrial.TrOperator;
                else
                    pmHeadLabOperator.Text = userDict[pmHeadShowTrial.TrOperator];
                pmHeadLabDate.Text = ""+TimeHandle.milSecondsToDatetime(long.Parse(pmHeadShowTrial.TrDate));
                pmHeadLabInfo.Text = pmHeadShowTrial.TrInfo;
            }
            else
            {
                Console.WriteLine("获取的Trial记录暂时为空！");
            }
        }
        
        #endregion

        //下载某次Trial的数据( 这里目前下载的是 data.zip )=======================
        public void initDnldSock()
        {
            dnldSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(ContantInfo.SockServ.ip);
            try
            {
                dnldSock.Connect(ip, Int32.Parse(ContantInfo.SockServ.port));
                MyLogger.WriteLine("download request connected to server!");

                dnldRunFlg = true;

                //开启线程监听server 的 响应
                Thread dnldTh = new Thread(dnldRecv);
                dnldTh.IsBackground = true;
                dnldTh.Start();
            }
            catch (Exception err)
            {
                MyLogger.WriteLine(err.Message);
            }
        }
        private void pmTrialBtDnld_Click(object sender, EventArgs e)
        {
            
            dnldPath = CfgTool.getDnldPath(dnldDialog);

            if(dnldPath == null)
            {
                MyLogger.WriteLine("下载取消!");
                return;
            }

            //先判断要下载的文件是否已经存在
            FileInfo existFile = new FileInfo(dnldPath + "\\" + pmHeadShowTrial.TrUserId + "_" + pmHeadShowTrial.TrDate + ".zip");
            if (existFile.Exists)
            {
                MessageBox.Show("you have download the trial data!", "message");
                return;
            }

            //先获取socket 连接
            if(dnldSock == null)
            {
                MessageBox.Show("you have download the trial data!", "message");
                dnldRunFlg = false;
                return;
            }
            dnldHead = "dnld:#";
            dnldHead += pmHeadShowTrial.TrUserId + "_" + pmHeadShowTrial.TrDate + "#";

            try
            {
                //传输head 
                dnldSock.Send(Encoding.UTF8.GetBytes(dnldHead.ToCharArray()));
                dnldRunFlg = true;
            }
            catch
            {
                MessageBox.Show("与服务端断开连接!", "message");
                dnldRunFlg = false;
                MyLogger.WriteLine("发送下载请求头时 遇到异常！");
            }
        }
        private void dnldRecv()
        {
            while (dnldRunFlg)
            {
                MyLogger.WriteLine("dnldSock 等待服务端的下一次可下载回应！");
                dnldOk = false;

                byte[] msgBuf = new byte[64];
                string msg = null;

                int maxFileLen = 1024 * 512;//512 k
                byte[] fileBuf = new byte[maxFileLen];

                int count = 0;

                string dnldFileName = null;

                bool dnldErr = false; //检测下载过程中是否出错

                while (!dnldOk)
                {
                    try
                    {
                        dnldSock.Receive(msgBuf);
                    }
                    catch
                    {
                        dnldRunFlg = false;
                        dnldOk = true;
                       
                        return;
                    }
                    msg = Encoding.UTF8.GetString(msgBuf);
                    string fileLength = "";
                    if (msg.StartsWith("resdnld:"))
                    {
                        MyLogger.WriteLine("服务端同意发送:" + msg.Split('#')[1] + " 文件 ");
                        fileLength = msg.Split('#')[2];
                        MyLogger.WriteLine("文件大小为:" + fileLength);
                    }

                    //显示进度条
                    FmProgress prog = new FmProgress(this, long.Parse(fileLength), "dnld");
                    prog.Show();

                    if (msg.StartsWith("errdnld:"))
                    {
                        MessageBox.Show("下载出错!\n数据库中不存在这条数据记录！", "message");
                        dnldOk = true;
                        dnldErr = true;
                        break;
                    }

                    bool ifFileEnd = false;

                    dnldFileName = dnldPath + "\\" + msg.Split('#')[1] + ".zip";

                    long curLeng = 0;

                    using (FileStream fs = new FileStream(dnldFileName, FileMode.Create))
                    {
                        //监听文件数据 loop
                        while (!ifFileEnd)
                        {
                            count = dnldSock.Receive(fileBuf);
                            curLeng += count;

                            //更新进度条
                            prog.updateProgValue(curLeng, "");

                            //正常数据
                            if (count > 128)
                            {
                                fs.Write(fileBuf, 0, count);
                            }
                            else //count < 128
                            {
                                msg = Encoding.UTF8.GetString(fileBuf);

                                //文件结束标志  end:# file_name # file_left #
                                if (msg.StartsWith("end:"))
                                {
                                    msg = "resend:#" + dnldFileName + "#"; // end:# unique #
                                    dnldSock.Send(Encoding.UTF8.GetBytes(msg.ToCharArray()));//response

                                    //结束
                                    ifFileEnd = true;
                                    dnldOk = true;

                                    //防止接收到结束符，文件流还没关闭的情况
                                    if (fs.CanWrite)
                                    {
                                        fs.Close();
                                    }
                                }

                                //不能整段发送的剩余数据
                                else
                                {
                                    fs.Write(fileBuf, 0, count);
                                    fs.Close();

                                    MessageBox.Show("文件下载成功，保存在:\n" + dnldFileName, "message");
                                }
                            }
                        } //while(!iffileEnd)
                    }//using file()
                }//while(!dnldOk)
                MyLogger.WriteLine("客户端下载消息监听线程结束\n");
                if (!dnldErr)
                {
                    MessageBox.Show("文件下载成功! 保存在:\n" + dnldFileName, "message");
                }
            }
        }

        //设置settting panel 中控件的显示
        private void setPsPanel()
        {
            //如果settting可点 ， 当前controls 不可用
            if (ifPsSettingCan)
            {
                setPsPanControl(false); //
            }
            else //setting 不可点,当前controls 可用
            {
                setPsPanControl(true);
            }

            //设置按钮
            setPsPanBts();
        }
        private void setPsPanControl(bool ifEnable)
        {
            foreach (Control c in psControlList)
            {
                c.Enabled = ifEnable;
            }
        }
        private void setPsPanBts()
        {
            psBtSetting.Enabled = ifPsSettingCan;
            psBtSure.Enabled = ifPsOkCan;

            if (psBtSetting.Enabled) { psBtSetting.BackColor = Color.Black; }
            else { psBtSetting.BackColor = Color.Silver; }

            if (psBtSure.Enabled) { psBtSure.BackColor = Color.Black; }
            else { psBtSure.BackColor = Color.Silver; }
        }

        #region panel setting 中的控件事件函数
        private void psBtSetting_Click(object sender, EventArgs e)
        {
            ifPsSettingCan = false;
            ifPsOkCan = true;
            setPsPanel();

            //从数据库中获取combox 中可以显示的pltfm , pdct 
            if(psPltfmNames == null)
            {
                psPltfmNames = new List<string>();
                psPltfmNames = service.getPltfmNames();
                if(psPltfmNames == null)
                {
                    MyLogger.WriteLine("Get pltfm names error!");
                    return;
                }
            }

            //设置combox pltfms
            psCombPltfm.Items.Clear();
            foreach(string name in psPltfmNames)
            {
                psCombPltfm.Items.Add(name);
            }
            if (psCombPltfm.Text.Equals(""))
            {
                psCombPltfm.SelectedIndex = 0;
            }
            psCombPltfm.Refresh();

            //设置combox pdcts
            string selectPltfm = (string)psCombPltfm.SelectedValue;
            if (selectPltfm != null)
            {
                psPdctNames = service.getPdctNamesByPltfm(selectPltfm);
                if(psPdctNames.Count > 0)
                {
                    psCombPdct.Items.Clear();
                    foreach(string n in psPdctNames)
                    {
                        psCombPdct.Items.Add(n);
                    }
                    //psCombPdct.SelectedIndex = 0;
                    psCombPdct.Refresh();
                }
            }
        }

        private void psBtSure_Click(object sender, EventArgs e)
        {
            ifPsOkCan = false;
            ifPsSettingCan = true;
            setPsPanel();

            string pltfm;
            string pdct;
            string path;
            if (psCombPltfm.SelectedItem != null)
            {
                pltfm = psCombPltfm.SelectedItem.ToString();
            }
            else
            {
                pltfm = psCombPltfm.Text;
            }
            if (psCombPdct.SelectedItem != null)
            {
                pdct = psCombPdct.SelectedItem.ToString();
            }
            else
            {
                pdct = psCombPdct.Text;
            }
            
            path = psTxtDnldPath.Text.Trim();

            psCombPltfm.Text = pltfm;
            psCombPdct.Text = pdct;
            psTxtDnldPath.Text = path;

            //更新配置文件
            FileInfo cfgFile = new FileInfo(Environment.CurrentDirectory + "\\" + ".datasync.cfg");
            using (StreamWriter sw = new StreamWriter(new FileStream(cfgFile.FullName, FileMode.Create, FileAccess.Write)))
            {
                sw.WriteLine("#pltfm: what platform you are focus recently ?");
                sw.WriteLine("pltfm=" + pltfm);
                sw.WriteLine("#pdct: what you product you are focus recently ?");
                sw.WriteLine("pdct=" + pdct);
                sw.WriteLine("#path: which path you would like to save the download file ?");
                sw.WriteLine("path=" + path);
            }
        }

        private void psCombPltfm_SelectedIndexChanged(object sender, EventArgs e)
        {
            //设置combox pdcts
            string selectPltfm = (string)psCombPltfm.SelectedItem;

            if (selectPltfm != null)
            {
                psPdctNames = service.getPdctNamesByPltfm(selectPltfm);
                if (psPdctNames.Count > 0)
                {
                    psCombPdct.Items.Clear();
                    foreach (string n in psPdctNames)
                    {
                        psCombPdct.Items.Add(n);
                    }
                    psCombPdct.SelectedIndex = 0;
                    psCombPdct.Refresh();
                }
                else
                {
                    psCombPdct.Items.Clear();
                    psCombPdct.Text = "";
                }
            }
        }

        private void psBtChseDnldPath_Click(object sender, EventArgs e)
        {
            if(psDnldFolderBrsDlg.ShowDialog() == DialogResult.OK)
            {
                psTxtDnldPath.Text = psDnldFolderBrsDlg.SelectedPath;
            }
        }
        #endregion

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        //在getCsvSock 中下载好csv数据后进行更新的代理
        private delegate void ShowDatagridView(DataView dataView);
        public void showDataview(DataView dataView)
        {
            if (this.InvokeRequired)
            {
                ShowDatagridView show = new ShowDatagridView(showDataview);
                this.Invoke(show, new object[] { dataView });
            }
            else
            {
                pmGridview.DataSource = dataView;
            }
        }

        #region form dis-enable 代理
        private delegate void Enable();
        public void enable()
        {
            if (this.InvokeRequired)
            {
                Enable en = new Enable(enable);
                this.Invoke(en, new object[] {  });
            }
            else
            {
                this.Enabled = true;
            }
        }
        private delegate void Disable();
        public void disable()
        {
            if (this.InvokeRequired)
            {
                Disable dis = new Disable(disable);
                this.Invoke(dis, new object[] { });
            }
            else
            {
                this.Enabled = false;
            }
        }
        #endregion

        //分析按钮的点击事件
        private void pmTrialBtAnalyze_Click(object sender, EventArgs e)
        {
            //弹出分析选择框
            FmAnalyzer analyze = new FmAnalyzer(pmHeadShowTrial.TrUserId, pmHeadShowTrial.TrDate,this);
            analyze.Show();
        }

        //dbgFiles 图片按钮的点击事件[获取dbgfiles 文件列表]
        private void picboxDbgFiles_Click(object sender, EventArgs e)
        {
            //下载文件列表
            //在下载csv后面接接着下载dbgfiles 了，所以不用再下了
            //GetCsvSock.queryDdgFiles(pmHeadShowTrial.TrUserId, pmHeadShowTrial.TrDate);

            string file = Environment.CurrentDirectory + "\\" + pmHeadShowTrial.TrUserId + "_" + 
                          pmHeadShowTrial.TrDate + ".dict";
            MyLogger.WriteLine("dict " + file);

            //弹出下载选项框
            FmDbgFiles fm = new FmDbgFiles(pmHeadShowTrial.TrUserId, pmHeadShowTrial.TrDate);
            fm.ShowDialog(this);
        }

        #region picboxDbgFiles 代理
        // picboxDbgFiles 不可用的更新代理
        private delegate void DisablePic();
        public void disablePic()
        {
            if (this.InvokeRequired)
            {
                DisablePic dis = new DisablePic(disablePic);
                this.Invoke(dis, new object[] { });
            }
            else
            {
                picboxDbgFiles.Visible = false;
            }
        }
        private delegate void EnablePic();
        public void enablePic()
        {
            if (this.InvokeRequired)
            {
                EnablePic dis = new EnablePic(enablePic);
                this.Invoke(dis, new object[] { });
            }
            else
            {
                picboxDbgFiles.Visible = true;
            }
        }
        #endregion

        private void picboxAnalyz_Click(object sender, EventArgs e)
        {
            //弹出分析选择框
            FmAnalyzer analyze = new FmAnalyzer(pmHeadShowTrial.TrUserId, pmHeadShowTrial.TrDate, this);
            analyze.Show();
        }

        private void picboxDnld_Click(object sender, EventArgs e)
        {
            dnldPath = CfgTool.getDnldPath(dnldDialog);

            if (dnldPath == null)
            {
                MyLogger.WriteLine("下载取消!");
                return;
            }

            //先判断要下载的文件是否已经存在
            FileInfo existFile = new FileInfo(dnldPath + "\\" + pmHeadShowTrial.TrUserId + "_" + pmHeadShowTrial.TrDate + ".zip");
            if (existFile.Exists)
            {
                MessageBox.Show("you have download the trial data!", "message");
                return;
            }

            //先获取socket 连接
            if (dnldSock == null)
            {
                MessageBox.Show("you have download the trial data!", "message");
                dnldRunFlg = false;
                return;
            }
            dnldHead = "dnld:#";
            dnldHead += pmHeadShowTrial.TrUserId + "_" + pmHeadShowTrial.TrDate + "#";

            try
            {
                //传输head 
                dnldSock.Send(Encoding.UTF8.GetBytes(dnldHead.ToCharArray()));
                dnldRunFlg = true;
            }
            catch
            {
                MessageBox.Show("与服务端断开连接!", "message");
                dnldRunFlg = false;
                MyLogger.WriteLine("发送下载请求头时 遇到异常！");
            }
        }

        #region 按钮点击图片变化事件
        private void picChgPswd_MouseLeave(object sender, EventArgs e)
        {
            picChgPswd.Image = Properties.Resources.stLv;
        }

        private void picChgPswd_Click(object sender, EventArgs e)
        {
            FmChgPswd fm = new FmChgPswd(service, curUser);
            fm.ShowDialog();
        }

        private void picChgPswd_MouseEnter(object sender, EventArgs e)
        {
            picChgPswd.Image = Properties.Resources.stHv;
        }

        private void picboxDbgFiles_MouseEnter(object sender, EventArgs e)
        {
            picboxDbgFiles.Image = Properties.Resources.schHv;
        }

        private void picboxDbgFiles_MouseLeave(object sender, EventArgs e)
        {
            picboxDbgFiles.Image = Properties.Resources.schLv;
        }

        private void picboxAnalyz_MouseEnter(object sender, EventArgs e)
        {
            picboxAnalyz.Image = Properties.Resources.analyHv;
        }

        private void picboxAnalyz_MouseLeave(object sender, EventArgs e)
        {
            picboxAnalyz.Image = Properties.Resources.analyLv;
        }

        private void picboxDnld_MouseEnter(object sender, EventArgs e)
        {
            picboxDnld.Image = Properties.Resources.dnldlev;
        }

        private void picboxDnld_MouseLeave(object sender, EventArgs e)
        {
            picboxDnld.Image = Properties.Resources.dnldHv;
        }

        private void pmLabProduct_MouseEnter(object sender, EventArgs e)
        {
            pmLabProduct.BackColor = Color.White;
            pmLabProduct.ForeColor = Color.Black;
        }

        private void pmLabProduct_MouseLeave(object sender, EventArgs e)
        {
            pmLabProduct.BackColor = Color.Black;
            pmLabProduct.ForeColor = Color.White;
        }

        private void pmLabPlatform_MouseEnter(object sender, EventArgs e)
        {
            pmLabPlatform.BackColor = Color.White;
            pmLabPlatform.ForeColor = Color.Black;
        }

        private void pmLabPlatform_MouseLeave(object sender, EventArgs e)
        {
            pmLabPlatform.BackColor = Color.Black;
            pmLabPlatform.ForeColor = Color.White;
        }

        private void pmLabRoot_MouseEnter(object sender, EventArgs e)
        {
            pmLabRoot.BackColor = Color.White;
            pmLabRoot.ForeColor = Color.Black;
        }

        private void pmLabRoot_MouseLeave(object sender, EventArgs e)
        {
            pmLabRoot.BackColor = Color.Black;
            pmLabRoot.ForeColor = Color.White;
        }

        #endregion
    }
}
