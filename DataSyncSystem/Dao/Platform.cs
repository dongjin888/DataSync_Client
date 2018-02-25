using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncSystem.Dao
{
    public class Platform
    {
        private int id;
        private string pltfmName;
        private string pltfmInfo;

        #region property 
        public int Id
        {
            set { id = value; }
            get { return id; }
        }
        public string PltfmName
        {
            set { pltfmName = value; }
            get { return pltfmName; }
        }
        public string PltfmInfo
        {
            set { pltfmInfo = value; }
            get { return pltfmInfo; }
        }
        #endregion

        public Platform()
        {
            id = Id;
            pltfmName = PltfmName;
            pltfmInfo = PltfmInfo;
        }
    }
}
