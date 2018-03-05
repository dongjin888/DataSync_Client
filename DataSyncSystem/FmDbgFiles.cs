using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataSyncSystem.Utils;

namespace DataSyncSystem
{
    public partial class FmDbgFiles : Form
    {
        private string dbgFilesDict;
        private Dictionary<string, string> fileid2NameDict = new Dictionary<string, string>();
        private List<string> dictLine = new List<string>();

        private string userid;
        private string date;

        public static string dnldFolder = null;

        static FmDbgFiles()
        {
            dnldFolder = Environment.CurrentDirectory;
            if (!FileHandle.checkDirCanWrite(dnldFolder))
            {
                dnldFolder = "";
            }
        }

        public FmDbgFiles()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        public FmDbgFiles(string usid,string dt)
        {
            InitializeComponent();

            rdoStrict.Checked = true; // 默认为严格搜索语法

            StartPosition = FormStartPosition.CenterScreen;

            userid = usid;
            date = dt;
            dbgFilesDict = Environment.CurrentDirectory + "\\" + usid + "_" +
                          dt + ".dict";

            listView1.Columns.Add("Downlaod", -2, HorizontalAlignment.Center);
            listView1.Columns.Add("File-Names", -2, HorizontalAlignment.Left);

            txtDnldFolder.Text = dnldFolder;

            //用线程去加载dbgFilesDict文件
            //Thread th = new Thread(loadDict);
            //th.IsBackground = true;
            //th.Start();
            loadDict();
        }

        //加载dbgFilesDict [线程函数]
        private void loadDict()
        {
            FileInfo dictFile = new FileInfo(dbgFilesDict);
            MyLogger.WriteLine("fm show file name:" + dictFile.FullName);
            int waitTimes = 0; // 等待dict 文件次数
            
            if (!dictFile.Exists)
            {
                Thread.Sleep(500);
                waitTimes++;
                
                while (File.Exists(dictFile.FullName))
                {
                    MyLogger.WriteLine("wait dict file...");
                    MyLogger.WriteLine(dictFile.FullName);
                    Thread.Sleep(500);
                    waitTimes++;
                    if(waitTimes>= 3)
                    {
                        MyLogger.WriteLine("wait dict file timeout!");
                        break;
                    }
                }
            }
            if(waitTimes < 3) //dict 文件已经得到
            {
                //读取dict 文件到 listview 中
                using (FileStream fs = new FileStream(dbgFilesDict, FileMode.Open))
                {
                    using(StreamReader sr = new StreamReader(fs))
                    {
                        string line = null;
                        string[] splits = new string[2];
                        while((line = sr.ReadLine()) != null && !line.Equals(""))
                        {
                            splits = line.Split('*');
                            fileid2NameDict.Add(splits[0], line); // id - name
                            dictLine.Add(splits[0]+"*"+ splits[1]);
                        }
                    }
                }
                MyLogger.WriteLine(dbgFilesDict+" 文件load 完成!");

                //用代理通知listview 更新
                updateList(dictLine);
            }
        }

        private delegate void UpdateList(List<string> list);
        public void updateList(List<string> list)
        {
            if (this.InvokeRequired)
            {
                UpdateList update = new UpdateList(updateList);
                this.Invoke(update, new object[] { list });
            }
            else
            {
                //清除
                listView1.Items.Clear();

                //重新装填
                string[] splits = new string[] { "null", "null" };
                for(int i=0; i<list.Count; i++)
                {
                    splits = list[i].Split('*');
                    ListViewItem item = new ListViewItem(splits[0],i);
                    item.SubItems.Add(splits[1]);
                    listView1.Items.Add(item);
                }
            }
        }

        // 下载按钮，点击后遍历listview 中的选中项，然后下载
        private void picboxDnld_Click(object sender, EventArgs e)
        {
            picboxDnld.Image = Properties.Resources.dnldon;

            Thread picboxImgTh = new Thread(imgChg);
            picboxImgTh.IsBackground = true;
            picboxImgTh.Start();

            MyLogger.WriteLine("download list:");
            List<string> dnldFileIdList = new List<string>();
            foreach (ListViewItem item in listView1.CheckedItems)
            {
                dnldFileIdList.Add(item.SubItems[0].Text); // subitems[0]:fileid  [1]:filename
                MyLogger.WriteLine(item.SubItems[0].Text + " +++ " + item.SubItems[1].Text);
            }

            //先判断txtDnldFolder是否有目录
            if (!txtDnldFolder.Text.Trim().Equals(""))
            {
                if(dnldFileIdList.Count >= 1)
                {
                    //开始下载
                    GetCsvSock.dnldFiles(userid, date, dnldFolder, dnldFileIdList, true);
                }
                else
                {
                    MessageBox.Show("Please at least 1 file to download!", "operator error!");
                }

                //重置list选中状态
                for(int i=0; i<listView1.Items.Count; i++)
                {
                    listView1.Items[i].Checked = false;
                }
                labSelectNum.Text = listView1.CheckedItems.Count + "";


            }
            else
            {
                MessageBox.Show("Please choose a download folder!", "download deny");
            }
        }
        #region 改变下载按钮特效
        private void imgChg()
        {
            Thread.Sleep(300);
            updateFreshImg();
        }
        private delegate void UpdateDnldImg();
        private void updateFreshImg()
        {
            if (this.InvokeRequired)
            {
                UpdateDnldImg update = new UpdateDnldImg(updateFreshImg);
                this.Invoke(update, new object[] { });
            }
            else
            {
                picboxDnld.Image = Properties.Resources.dnldlev;
            }
        }
        #endregion

        //回车事件的处理
        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            bool isStrict = true;
            if (rdoNotStrict.Checked)
            {
                isStrict = false;
                MyLogger.WriteLine("不严格搜索!");
            }
            if(e.KeyCode == Keys.Enter)
            {
                string filter = txtFilter.Text.Trim();
                List<string> showLine = new List<string>();

                MyLogger.WriteLine("search event!"+filter);

                foreach (string line in dictLine)
                {
                    if (isStrict)
                    {
                        if (line.Contains(filter))
                        {
                            showLine.Add(line);
                            MyLogger.WriteLine("[+filter] " + line);
                        }
                    }
                    else
                    {
                        if (line.ToLower().Contains(filter.ToLower()))
                        {
                            showLine.Add(line);
                            MyLogger.WriteLine("[+filter] " + line);
                        }
                    }
                }
                updateList(showLine);
            }
        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dia = new FolderBrowserDialog();
            if(dia.ShowDialog() == DialogResult.OK)
            {
                if (!FileHandle.checkDirCanWrite(dia.SelectedPath))
                {
                    MessageBox.Show("Please choose another folder!\n You can save file to this folder", "operator error");
                }
                else
                {
                    txtDnldFolder.Text = dia.SelectedPath;
                    dnldFolder = dia.SelectedPath;
                }
            }
        }

        private void listView1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int count = 0;
            count = listView1.CheckedItems.Count;
            if (e.CurrentValue == CheckState.Unchecked) //
            {
                count += 1;
            }
            else //之前选中
            {
                count -= 1;
            }
            labSelectNum.Text = count + "";
        }
    }
}
