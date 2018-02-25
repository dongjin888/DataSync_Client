using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncSystem.Utils
{
    public class PanViewTool
    {
        public static string[] labs = {"labRoot","labNext0>>","labPltfmName","labNext1>>",
                                       "labPdctName","labNext2>>","labTrial"};
        public static string[] pans = { "pmPanPltfms", "pmPanPdcts", "pmPanTrials", "pmPanHeads" };
        public static string getPanNameById(int curPan)
        {
            return pans[curPan];
        }

        public static string getLabNameById(int curLab)
        {
            return labs[curLab]; 
        }
    }
}
