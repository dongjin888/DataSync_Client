using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncSystem.Dao
{
    public class Product
    {
        private int id;
        private string pltfmName;
        private string pdctName;
        private string pdctInfo;

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
        public string PdctName
        {
            set { pdctName = value; }
            get { return pdctName; }
        }
        public string PdctInfo
        {
            set { pdctInfo = value; }
            get { return pdctInfo; }
        }
        #endregion

        public Product()
        {
            id = Id;
            pltfmName = PltfmName;
            pdctName = PdctName;
            pdctInfo = PdctInfo;
        }
    }
}
