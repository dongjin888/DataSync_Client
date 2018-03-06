using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnalysisTool;
using DataSyncSystem.Utils;
using DataSyncSystem.SelfView;
using System.Threading;
using System.IO;

namespace DataSyncSystem.Utils
{
    public interface IAnalyzCsvDnlded
    {
        void dnldOkCallBack(List<string> fileNameList);
    }

    public class ImpAnalyzeCsvDnlded:IAnalyzCsvDnlded
    {
        private string anlyzeName;
        private Control parent;

        public Control Parent
        {
            set { parent = value; }
        }

        public string AnalyzeName
        {
            set { anlyzeName = value; }
        }

        FmWaitAnlyzFile fm = null;

        public ImpAnalyzeCsvDnlded()
        {
            fm = new FmWaitAnlyzFile();
            fm.Show(); //弹出等待框
        }

        [STAThread]
        public void dnldOkCallBack(List<string> fileNameList)
        {
            List<string> cfgs = AnalyzerCfg.toolDict[anlyzeName];

            //消失等待框
            fm.Dispose();

            if (anlyzeName.Equals("tfc"))
            {
                //string newFileName;
                List<string> alterNameList = new List<string>();
                /*
                newFileName = Environment.CurrentDirectory + "\\DIH_step.csv";
                try
                {
                    File.Copy(fileNameList[0], newFileName);
                    alterNameList.Add(newFileName);
                    newFileName = Environment.CurrentDirectory + "\\DIH_Result.csv";
                    File.Copy(fileNameList[1], newFileName);
                    alterNameList.Add(newFileName);
                }
                catch
                {
                    MessageBox.Show("Rename analyze file exception!", "operat error");
                    return;
                }

                //
                try
                {
                    File.Delete(fileNameList[0]);
                    File.Delete(fileNameList[1]);
                }
                catch { MyLogger.WriteLine("删除临时下载的分析文件异常！"); }
                */
                //alterNameList.Add("c:\\data\\DIH_step.csv");
                //alterNameList.Add("c:\\data\\DIH_Result.csv");

                var thread = new Thread(new ParameterizedThreadStart(param => {
                    a_MainForm main = new a_MainForm();//(fileNameList)
                    string stepFile2 = @"c:\data\DIH_step.csv";
                    string resFile2 = @"c:\data\DIH_Result.csv";

                    Files_Base.StepFileName = stepFile2;
                    Files_Base.ResultFileName = resFile2;

                    List<string> curve2Plot = new List<string> { };
                    curve2Plot.Add("DefaultTdpCurvePlot");
                    curve2Plot.Add("DefaultCC0");
                    curve2Plot.Add("DefaultMisTest");
                    curve2Plot.Add("DefaultDeltaPT");
                    curve2Plot.Add("DefaultRWTdpCDF");
                    a_CurveForm newForm = new a_CurveForm();
                    newForm.FormInitialize(curve2Plot);
                    for (int i = 0; i < curve2Plot.Count; i++)
                    {
                        main.dispatchPlot(curve2Plot[i], newForm.panelList[i]);
                    }
                    newForm.StartPosition = FormStartPosition.CenterScreen;
                    parent.Invoke((MethodInvoker)(() => newForm.Show()));

                }));
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
            else
            {
                foreach(string file in fileNameList)
                {
                    Console.WriteLine("dnldok:\n" + file + "\n");
                }
            }
        }
    }
}
