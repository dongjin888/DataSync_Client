using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncSystem.Utils
{
    public static class AnalyzerCfg
    {

        public static Dictionary<string, List<string>> toolDict = new Dictionary<string, List<string>>();

        static AnalyzerCfg()
        {
            toolDict.Add("tfc",                     // analyze name
                new List<string>(){                 //parameter
                "2",                                //need 2 files
                "step file,result file",            // file1 info:step file , file2 info:result file
                "tfc:dih:amber,tfc:dih:result:abmer"// file1 contains tfc,dih,amber  ==> tfcAmberDIH.csv 
                                                    // file2 contains tfc,dih,result ==> tfcAmberDihResult.csv
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
