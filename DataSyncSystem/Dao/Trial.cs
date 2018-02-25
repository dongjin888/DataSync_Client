using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncSystem.Dao
{
    public class Trial
    {
        private int id;
        private string trPltfmName;
        private string trPdctName;
        private string trUserId;
        private string trDate;
        private string trSummaryPath;
        private string trDebugPath;
        private string trInfo;
        private string trOperator;

        #region Trial property 
        public int Id
        {
            set { id = value; }
            get { return id; }
        }
        public string TrPltfmName
        {
            set { trPltfmName = value; }
            get { return trPltfmName; }
        }
        public string TrPdctName
        {
            set { trPdctName = value; }
            get { return trPdctName; }
        }
        public string TrDate
        {
            set { trDate = value; }
            get { return trDate; }
        }
        public string TrSummaryPath
        {
            set { trSummaryPath = value; }
            get { return trSummaryPath; }
        }
        public string TrDebugPath
        {
            set { trDebugPath = value; }
            get { return trDebugPath; }
        }
        public string TrUserId
        {
            set { trUserId = value; }
            get { return trUserId; }
        }
        public string TrInfo
        {
            set { trInfo = value; }
            get { return trInfo; }
        }
        public string TrOperator
        {
            set { trOperator = value; }
            get
            {
                return trOperator;
            }
        }
        #endregion

        public Trial()
        {
            id = Id;
            trPltfmName = TrPltfmName;
            trPdctName = TrPdctName;
            trDate = TrDate;
            trSummaryPath = TrSummaryPath;
            trDebugPath = TrDebugPath;
            trUserId = TrUserId;
            trInfo = TrInfo;
            trOperator = TrOperator;
        }
        public override string ToString()
        {
            string res = "";
            res += "Trial " + id;
            res += "| " + trPltfmName;
            res += "| " + trPdctName;
            res += "| " + trDate;
            res += "| [" + trSummaryPath + "]";
            res += "| [" + trDebugPath + "]";
            res += "| " + trUserId;
            res += "| " + trInfo;
            res += " | " + trOperator;
            res += "\n";
            return res;
        }
    }
}
