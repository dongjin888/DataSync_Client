using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataSyncSystem.SelfView;
using DataSyncSystem.Utils;

namespace DataSyncSystem
{
    public partial class FmAnalyzer : Form
    {
        private string userid;
        private string date;

        private Control parent;

        // 显示每种tool 的控件
        private List<Control> controls = new List<Control>();

        public FmAnalyzer(Control par)
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;

            parent = par;
        }

        public FmAnalyzer(string uid,string dt,Control par)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            parent = par;

            userid = uid;
            date = dt;

            //读取Analyze 中的tool dict 
            if(AnalyzerCfg.toolDict.Count <= 0)
            {
                labNoAnalyze.Visible = true;
            }
            else
            {
                Panel toolPan = new Panel();
                toolPan.Location = new Point(65, 38);
                toolPan.Size = new Size(510, 276);
                toolPan.BackColor = Color.LightGray;
                toolPan.ForeColor = Color.Black;
                toolPan.AutoScroll = true;

                int i = 0;
                int x = 55;
                int y = 30;
                //添加每种分析tool
                foreach(string toolName in AnalyzerCfg.toolDict.Keys)
                {
                    MyLable label = new MyLable();
                    label.Text = toolName;
                    label.BackColor = Color.FromArgb(0, 192, 192);
                    label.ForeColor = Color.Black;
                    label.Location = new Point(x, y);
                    label.Size = new Size(400, 50);
                    label.TextAlign = ContentAlignment.MiddleCenter;
                    label.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Bold);
                    label.MouseEnter += new EventHandler(lab_MouseEnter);
                    label.MouseLeave += new EventHandler(lab_MouseLeave);
                    label.MouseClick += new MouseEventHandler(lab_MouseClick);
                    label.TabIndex = i;
                    i++;

                    //添加到panel中
                    toolPan.Controls.Add(label);
                    y += label.Height + 30;
                }
                Controls.Add(toolPan);
            }
        }

        private void lab_MouseEnter(object sender, EventArgs e)
        {
            MyLable lab = sender as MyLable;
            lab.BackColor = Color.Black;
            lab.ForeColor = Color.White;
        }

        private void lab_MouseLeave(object sender, EventArgs e)
        {
            MyLable lab = sender as MyLable;
            lab.BackColor = Color.FromArgb(0,192,192);
            lab.ForeColor = Color.Black;
        }

        private void lab_MouseClick(object sender, MouseEventArgs e)
        {
            MyLable lab = sender as MyLable;

            //得到配置文件信息
            List<string> cfg = AnalyzerCfg.toolDict[lab.Text.Trim()];

            List<string> fileNameList = new List<string>();

            string dictName = Environment.CurrentDirectory + "\\" + userid + "_" + date + ".dict";
            if(!File.Exists(dictName))
            {
                MessageBox.Show("This trial didn't have the .dict file!", "operator!");
                return;
            }
            FileStream fs = new FileStream(dictName, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string line = null;
            while((line = sr.ReadLine()) != null &&!line.Equals(""))
            {
                fileNameList.Add(line);
            }
            sr.Close();
            fs.Close();

            string[] filekeywords = (cfg[2] + ",").Split(',');
            List<string> findFileIds = new List<string>();
            foreach(string str in filekeywords)
            {
                //分割keywords[] 去find文件
                findAnalyzeFileId(str.Split(':').ToList(),fileNameList, findFileIds);
            }
            
            if(findFileIds.Count == 0)
            {
                MessageBox.Show("Didn't find any file for tfc analyze!", "operator error");
                return;
            }

            if(findFileIds.Count > 0)
            {
                foreach (string id in findFileIds)
                    MyLogger.WriteLine("find id:"+id);

                //GetCsvSock 获取要下载的文件
                ImpAnalyzeCsvDnlded callback = new ImpAnalyzeCsvDnlded();
                callback.AnalyzeName = lab.Text.Trim();
                callback.Parent = parent;
                GetCsvSock.dnldFiles(userid, date,null, findFileIds, false,callback);
            }
        }

        private void findAnalyzeFileId(List<string> oneFilekeys,List<string> names,List<string> res)
        {
            string[] splits = null;
            foreach (string name in names)
            {
                splits = name.Split('*');
                int ct = 0;
                foreach (string key in oneFilekeys)
                {
                    string tmp = key.ToLower();
                    if (!tmp.Equals(""))
                    {
                        if (splits[1].ToLower().Contains(tmp))
                        {
                            ct++;
                        }
                    }
                }
                if (ct == oneFilekeys.Count)
                    res.Add(splits[0]);
            }
        }
    }
}
