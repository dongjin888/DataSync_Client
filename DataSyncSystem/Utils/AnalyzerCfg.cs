using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncSystem.Utils
{
    public class AnalyzerCfg
    {

        public static Dictionary<string, List<string>> toolDict = new Dictionary<string, List<string>>();

        static AnalyzerCfg()
        {
            toolDict.Add("tfc",                 
                new List<string>(){                
                "2",                                
                "step file,result file",            
                "tfc:step:dih,tfc:summary:amber"
            });

            // [add] a new analyze 
            /*
            toolDict.Add("mcw", new List<string>(){
                "1",
                "mcw result file",
                "mcw:amber:result"
            });

            toolDict.Add("mjog", new List<string>(){
                "2",
                "mjog result file,mjog file 2",
                "mcw:amber:result,mjog:amber:keyword"
            });

            toolDict.Add("ocw", new List<string>(){
                "1",
                "ocw result file",
                "ocw:ar8:test"
            });

            toolDict.Add("tdp", new List<string>(){
                "1",
                "mcw result file",
                "mcw:amber:result"
            });
            */
        }

    }
}
