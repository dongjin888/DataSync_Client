using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataSyncSystem.Dao;
using DataSyncSystem.Utils;
using System.Threading;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.IO.Compression;

namespace DataSyncSystem.SelfView
{
    public partial class MyTrial : UserControl
    {
        public Trial trial;
        private FmMain fmMain;
        private string trialUniqStr;
        private string fileName;

        public MyTrial(Trial trial,FmMain fm)
        {
            InitializeComponent();
            fmMain = fm;

            this.trial = trial;
            btUser.Text = trial.TrUserId;
            labTrialInfo.Text = trial.TrInfo;
            labTrialDate.Text = TimeHandle.milSecondsToDatetime(long.Parse(trial.TrDate)).ToString();
            trialUniqStr = trial.TrUserId + "_" + trial.TrDate;

            fileName = trialUniqStr + ".csv";
        }

        //查看某个用户
        private void btUser_Click(object sender, EventArgs e)
        {
            string userId = btUser.Text.Trim();
            FmUser fmUser = new FmUser(fmMain,userId);
            fmUser.ShowDialog();
        }

        //查看该trial
        private void MyTrial_Click(object sender, EventArgs e)
        {
            fmMain.pmCurPan += 1;
            fmMain.pmCurLab += 1;
            Cache.trialUniqueStr = trialUniqStr;
            fmMain.setCurPmLab();
            fmMain.setCurPmPan();

            //还要为下面的pmHeads 中的数据显示做准备
            fmMain.pmHeadShowTrial = trial;
            fmMain.Refresh();

            //连接socket 获取该trial的summary.csv 文件 先
            //获取到的summary.csv 文件会先保存在 当前软件目录下并以 userid_datestr.csv 的形式保存

            GetCsvSock.dnldCsvFile(trial.TrUserId, trial.TrDate);

            #region 行不通的方法
            //try 第二种方法
            /* 
            {
                Socket csvSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Parse(ContantInfo.SockServ.ip);
                csvSock.Connect(ip, Int32.Parse(ContantInfo.SockServ.port));

                Thread csvRecvMsgTh = new Thread(csvRecv);
                csvRecvMsgTh.IsBackground = false;
                csvRecvMsgTh.Start(csvSock);
                MyLogger.WriteLine("csv 监听消息线程:" + csvRecvMsgTh.ToString());
            }
            catch
            {

            }*/
            #endregion

        }

        /* 行不通的第二种方法
        private void csvRecv(object sock)
        {
            Socket socket = sock as Socket;

            string msg = null;
            byte[] msgBuf = new byte[128];

            socket.Receive(msgBuf);

            msg = Encoding.UTF8.GetString(msgBuf);

            if (msg.StartsWith("resreqcsv:"))
            {
                //开启数据接收线程
                Thread recvCsvDataTh = new Thread(recvCsvData);
                recvCsvDataTh.IsBackground = false;
                recvCsvDataTh.Start(socket);

                MyLogger.WriteLine("接收数据线程:" + recvCsvDataTh.ToString());

            }
            else if (msg.StartsWith("errreqcsv:"))
            {
                //请求的csv 出错
                MyLogger.WriteLine("请求出错! 服务端回应：" + msg);
            }
        }
        private void recvCsvData(object sock)
        {
            Socket socket = sock as Socket;

            bool ifDataEnd = false;
            string msg = null;

            int count = 0;
            int maxFileLen = 1024 * 32;//512 k
            byte[] fileBuf = new byte[maxFileLen];

            string csvFileName = fileName as string;
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
                    count = socket.Receive(fileBuf);

                    //正常数据
                    if (count > 128)
                    {
                        fs.Write(fileBuf, 0, count);
                    }
                    else //count < 128
                    {
                        msg = Encoding.UTF8.GetString(fileBuf);

                        //文件结束标志  end:# file_name # file_left #
                        if (msg.StartsWith("endcsv:"))
                        {
                            MyLogger.WriteLine("服务端回应:" + msg);
                            msg = "resendcsv:#" + fileName + "#"; // end:# unique #
                            socket.Send(Encoding.UTF8.GetBytes(msg.ToCharArray()));//response
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
                            fs.Write(fileBuf, 0, count);
                            fs.Close();

                            MyLogger.WriteLine(158 + "csv文件下载成功，保存在:\n" + name);
                        }
                    }
                } // while (!ifDataEnd)
            } // using(FileStream fs
        }
        */

        private void MyTrial_MouseEnter(object sender, EventArgs e)
        {
            BackColor = Color.Silver;
        }

        private void MyTrial_MouseLeave(object sender, EventArgs e)
        {
            BackColor = Color.Beige;
        }
    }
}
