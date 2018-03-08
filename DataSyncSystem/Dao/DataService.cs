using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSyncSystem.Utils;
using DataSyncSystem.Dao;
using MySql.Data.MySqlClient;
using System.Data;


namespace DataSyncSystem.Dao
{
    public class DataService
    {
        private MySqlConnection  con = null;

        public DataService()
        {
            if(con == null)
            {
                con = new MySqlConnection(ContantInfo.Database.CONSQLSTR);
            }
        }

        public bool closeCon()
        {
            bool ret = false;
            if (con != null && con.State == ConnectionState.Open)
            {
                try
                {
                    con.Close();
                    ret = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    ret = false;
                }
            }
            return false;
        }

        #region 验证用户
        public bool checkUser(string userId,string md5EncryptedStr,ref string level)
        {
            bool ret = false;
            if (con != null)
            {
                try
                {
                    if (con.State != ConnectionState.Open) { con.Open(); }

                    string query = @"select userpass,userlevel from tabUsers where userId=@userId;";
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = query;
                    cmd.Parameters.AddRange(new MySqlParameter[] { new MySqlParameter("@userId", userId) });

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return ret;
                        }
                        else
                        {
                            string pass = reader.GetString(0);
                            if (pass.Equals(md5EncryptedStr))
                            {
                                ret = true;
                                level = reader.GetString(1);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return ret;
        }
        #endregion

        public List<string> getTeamList()
        {
            List<string> ret = null;
            if (con != null)
            {
                try
                {
                    if (con.State != ConnectionState.Open) { con.Open(); }

                    string queryStr = @"select teamname from tabTeams;";
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = queryStr;
                    ret = new List<string>();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ret.Add(reader.GetString(0));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MyLogger.WriteLine("dataservice-getTeamList() get error!\n   " + ex.Message);
                }
            }
            return ret;
        }
        public Dictionary<string, string> getUserNmDictByTeam(string team)
        {
            Dictionary<string,string> ret = null;
            if (con != null)
            {
                try
                {
                    if (con.State != ConnectionState.Open) { con.Open(); }

                    string queryStr = @"select userid,username from tabUsers where
                                        teamname=@team;";
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@team", team);
                    cmd.CommandText = queryStr;
                    ret = new Dictionary<string, string>();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ret.Add(reader.GetString(0),reader.GetString(1)); //userid == username
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    MyLogger.WriteLine("dataservice-getTeamList() get error!\n   " + ex.Message);
                }
            }
            return ret;
        }

        #region 根据userId 用户工号获取用户对象
        public User getUserByUserId(string userId)
        {
            User ret = null;
            if(con != null)
            {
                try
                {
                    if(con.State != ConnectionState.Open) { con.Open();  }

                    string query = @"select * from tabUsers where userId=@userId;";
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = query;
                    cmd.Parameters.AddRange(new MySqlParameter[] { new MySqlParameter("@userId", userId) });
                    
                    using(MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ret = new User();
                        }
                        ret.Id = reader.GetInt32(0);
                        ret.UserId = reader.GetString(1);
                        ret.UserName = reader.GetString(2);
                        ret.TeamName = reader.GetString(3);
                        ret.UserTel = reader.GetString(4);
                        ret.UserLevel = reader.GetString(5);
                        ret.UserImgPath = reader.GetString(6);
                        ret.UserInfo = reader.GetString(7);
                    }
                    
                }
                catch(Exception ex)
                {
                    MyLogger.WriteLine(ex.Message);
                }
            }
            return ret;
        }
        #endregion

        #region 根据测试Platform , product,userId , Date 代号获取一个Trial 
        public Trial getTrialById(string pltfm,string pdct,string userId,string date)
        {
            Trial ret = null;

            if (con != null)
            {
                try
                {
                    if (con.State != ConnectionState.Open) { con.Open(); }

                    string query = @"select * from tabTrials where 
                                    trialPltfmName=@pltfm and
                                    trialPdctName=@pdct and
                                    trialUserId=@userId and
                                    trialDate=@date;";
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@pltfm",pltfm);
                    cmd.Parameters.AddWithValue("@pdct", pdct);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@date", date);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ret = new Trial();
                        }
                        ret.Id = reader.GetInt32(0);
                        ret.TrPltfmName = reader.GetString(1);
                        ret.TrPdctName = reader.GetString(2);
                        ret.TrUserId = reader.GetString(3);
                        ret.TrDate = reader.GetString(4);
                        ret.TrSummaryPath = reader.GetString(5);
                        ret.TrDebugPath = reader.GetString(6);
                        ret.TrInfo = reader.GetString(7);
                        ret.TrOperator = reader.GetString(8);
                        Console.WriteLine(ret);
                    }

                }
                catch (Exception ex)
                {
                    MyLogger.WriteLine(ex.Message);
                }
            }
            return ret;
        }
        #endregion

        #region 按页 根据userId获取测试分页列表  (用于他的上传功能)
        public List<Trial> getTrialPageList(string userId,int pageNow,int pageSize,ref int pageCount)
        {
            int rowCount = 0;
            List<Trial> trialList = null;

            //防止不正确的页数
            if (pageNow < 1)
                pageNow = 1;

            //开始分页操作
            if (con != null)
            {
                try
                {
                    if (con.State != ConnectionState.Open) { con.Open(); }

                    string query = @"select count(*) from tabTrials where 
                                          trialUserId=@userId;";
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@userId", userId);

                    //查询记录数
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            rowCount = reader.GetInt32(0);
                        }
                        MyLogger.WriteLine("tabTrials has " + rowCount + " lines");
                    }
                    if(rowCount == 0) { return trialList; }
                    else { trialList = new List<Trial>(); }
                }
                catch (Exception ex)
                {
                    MyLogger.WriteLine(ex.Message);
                }

                try
                {
                    //配置要查询的行数，页数
                    pageCount = (rowCount - 1) / pageSize + 1;
                    int rowStart = (pageNow - 1) * pageSize;

                    //查询记录
                    string queryPage = @"select * from tabTrials where
                                                trialUserId=@userId 
                                                limit @start,@size";
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = queryPage;
                    cmd.Parameters.AddRange(new MySqlParameter[] {
                        new MySqlParameter("@userId",userId),
                        new MySqlParameter("@start", rowStart) ,
                        new MySqlParameter("@size",pageSize),
                    });

                    //查询Trial记录，并封装到类
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Trial ret = new Trial();
                            ret.Id = reader.GetInt32(0);
                            ret.TrPltfmName = reader.GetString(1);
                            ret.TrPdctName = reader.GetString(2);
                            ret.TrUserId = reader.GetString(3);
                            ret.TrDate = reader.GetString(4);
                            ret.TrSummaryPath = reader.GetString(5);
                            ret.TrDebugPath = reader.GetString(6);
                            ret.TrInfo = reader.GetString(7);
                            ret.TrOperator = reader.GetString(8);
                            trialList.Add(ret);
                        }
                    }
                }
                catch
                {
                    MyLogger.WriteLine("when query page trial list error!");
                }
            }
            return trialList;
        }
        #endregion

        #region 按页 根据pltfm , pdct , 不管userid 获取Trials列表
        public List<Trial> getTrPgByPdct(string pltfm, string pdct, int pageNow, int pageSize,
                                          ref int pageCount)
        {
            int rowCount = 0;
            List<Trial> trialList = null;

            //防止不正确的页数
            if (pageNow < 1)
                pageNow = 1;

            //开始分页操作
            if (con != null)
            {
                try
                {
                    if (con.State != ConnectionState.Open) { con.Open(); }

                    string query = @"select count(*) from tabTrials where 
                                            trialPltfmName=@pltfm and trialPdctName=@pdct;";
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@pltfm", pltfm);
                    cmd.Parameters.AddWithValue("@pdct", pdct);

                    //查询记录数
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            rowCount = reader.GetInt32(0);
                        }
                        MyLogger.WriteLine("tabTrials has " + rowCount + " lines");
                    }
                    if (rowCount == 0) { return trialList; }
                    else { trialList = new List<Trial>(); }
                }
                catch (Exception ex)
                {
                    MyLogger.WriteLine(ex.Message);
                }

                try
                {
                    //配置要查询的行数，页数
                    pageCount = (rowCount - 1) / pageSize + 1;
                    int rowStart = (pageNow - 1) * pageSize;

                    //查询记录
                    string queryPage = @"select * from tabTrials where 
                                            trialPltfmName=@pltfm and trialPdctName=@pdct 
                                            limit @start,@size;";
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = queryPage;
                    cmd.Parameters.AddRange(new MySqlParameter[] {
                        new MySqlParameter("@pltfm",pltfm),
                        new MySqlParameter("@pdct",pdct),
                        new MySqlParameter("@start", rowStart) ,
                        new MySqlParameter("@size",pageSize),
                    });

                    //查询Trial记录，并封装到类
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Trial ret = new Trial();
                            ret.Id = reader.GetInt32(0);
                            ret.TrPltfmName = reader.GetString(1);
                            ret.TrPdctName = reader.GetString(2);
                            ret.TrUserId = reader.GetString(3);
                            ret.TrDate = reader.GetString(4);
                            ret.TrSummaryPath = reader.GetString(5);
                            ret.TrDebugPath = reader.GetString(6);
                            ret.TrInfo = reader.GetString(7);
                            ret.TrOperator = reader.GetString(8);
                            trialList.Add(ret);
                            MyLogger.WriteLine(ret.ToString());
                        }
                    }
                }
                catch
                {
                    MyLogger.WriteLine("when query page trial list error!");
                }
            }
            return trialList;
        }
        #endregion

        #region 根据用户工号userId获取Trail分页数据
        public List<Trial> getPageListByuid(int pageNow, int pageSize,string userId,ref int pageCount)
        {
            int rowCount = 0;
            List<Trial> trialList = null;

            //防止不正确的页数
            if (pageNow < 1)
                pageNow = 1;

            //开始分页操作
            if (con != null)
            {
                try
                {
                    if (con.State != ConnectionState.Open) { con.Open(); }

                    string query = @"select count(*) from tabTrials where trialUserId=@userId;";
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@userId", userId);

                    //查询记录数
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            rowCount = reader.GetInt32(0);
                        }
                        MyLogger.WriteLine("tabTrials has " + rowCount + " lines");
                    }

                    if (rowCount == 0)
                    {
                        return trialList;
                    }
                    else
                    {
                        trialList = new List<Trial>();
                    }
                }
                catch (Exception ex)
                {
                    MyLogger.WriteLine(ex.Message);
                }

                try
                {
                    //配置要查询的行数，页数
                    pageCount = (rowCount - 1) / pageSize + 1;

                    int rowStart = (pageNow - 1) * pageSize;
                    //查询记录
                    string queryPage = @"select * from tabTrials where trialUserId=@userId limit @start,@size";
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = queryPage;
                    cmd.Parameters.AddRange(new MySqlParameter[] {
                        new MySqlParameter("@userId",userId),
                        new MySqlParameter("@start", rowStart) ,
                        new MySqlParameter("@size",pageSize),
                    });

                    //查询Trial记录，并封装到类
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Trial ret = new Trial();
                            ret.Id = reader.GetInt32(0);
                            ret.TrPltfmName = reader.GetString(1);
                            ret.TrPdctName = reader.GetString(2);
                            ret.TrUserId = reader.GetString(3);
                            ret.TrDate = reader.GetString(4);
                            ret.TrSummaryPath = reader.GetString(5);
                            ret.TrDebugPath = reader.GetString(6);
                            ret.TrInfo = reader.GetString(7);
                            ret.TrOperator = reader.GetString(8);
                            trialList.Add(ret);
                        }
                    }
                }
                catch
                {
                    MyLogger.WriteLine("when query page trial list error!");
                }
            }
            return trialList;
        }
        #endregion

        #region 获取所有的platform
        public List<Platform> getAllPlatforms()
        {
            List<Platform> ret = null;

            if(con != null)
            {
                try
                {
                    if (con.State != ConnectionState.Open) { con.Open(); }

                    string queryStr = @"select * from tabPlatforms;";
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = queryStr;
                    ret = new List<Platform>();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Platform p = new Platform();
                            p.Id = reader.GetInt32(0);
                            p.PltfmName = reader.GetString(1);
                            p.PltfmInfo = reader.GetString(2);
                            ret.Add(p);
                        }
                    }
                }catch(Exception ex)
                {
                    MyLogger.WriteLine("getAllPlatforms() get error!\n   " + ex.Message);
                }
            }
            return ret;
        }
        #endregion

        #region 获取platfom 名字List<string>
        public List<string> getPltfmNames()
        {
            List<string> ret = null;
            if (con != null)
            {
                try
                {
                    if (con.State != ConnectionState.Open) { con.Open(); }

                    string queryStr = @"select pltfmName from tabPlatforms;";
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = queryStr;
                    ret = new List<string>();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ret.Add(reader.GetString(0));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MyLogger.WriteLine("dataservice-getPltfmNames() get error!\n   " + ex.Message);
                }
            }
            return ret;
        }
        #endregion
        
        #region 获取某个plafm 下的pdct 名字List<string>
        public List<string> getPdctNamesByPltfm(string pltfmName)
        {
            List<string> ret = null;
            if (con != null)
            {
                try
                {
                    if (con.State != ConnectionState.Open) { con.Open(); }

                    string queryStr = @"select pdctName from tabProducts where pltfmName=@pltfm;";
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@pltfm", pltfmName);
                    cmd.CommandText = queryStr;
                    ret = new List<string>();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ret.Add(reader.GetString(0));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MyLogger.WriteLine("dataservice-getPdctNamesByPltfm() get error!\n   " + ex.Message);
                }
            }
            return ret;
        }
        #endregion

        #region 按页 获取所有的platform
        public List<Platform> getPltfmPageList(int pageNow, int pageSize,ref int pageCount)
        {
            int rowCount = 0;
            List<Platform> ret = null;

            //防止不正确的页数
            if (pageNow < 1)
                pageNow = 1;

            //开始分页操作
            if (con != null)
            {
                try
                {
                    if (con.State != ConnectionState.Open) { con.Open(); }

                    string query = @"select count(*) from tabPlatforms;";
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = query;

                    //查询记录数
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            rowCount = reader.GetInt32(0);
                        }
                        MyLogger.WriteLine("tabPlatforms has " + rowCount + " lines");
                    }
                    if (rowCount == 0) { return ret; }
                    else { ret = new List<Platform>(); }
                }
                catch (Exception ex)
                {
                    MyLogger.WriteLine(ex.Message);
                }

                try
                {
                    //配置要查询的行数，页数
                    pageCount = (rowCount - 1) / pageSize + 1;
                    int rowStart = (pageNow - 1) * pageSize;

                    //查询记录
                    string queryPage = @"select * from tabPlatforms limit @start,@size";
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = queryPage;
                    cmd.Parameters.AddRange(new MySqlParameter[] {
                        new MySqlParameter("@start", rowStart) ,
                        new MySqlParameter("@size",pageSize),
                    });

                    //查询Trial记录，并封装到类
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Platform p = new Platform();
                            p.Id = reader.GetInt32(0);
                            p.PltfmName = reader.GetString(1);
                            p.PltfmInfo = reader.GetString(2);
                            ret.Add(p);
                        }
                    }
                }
                catch
                {
                    MyLogger.WriteLine("when query page trial list error!");
                }
            }
            return ret;
        }
        #endregion

        #region 获取某个platform 下所有的Product
        public List<Product> getProductsByPltfm(string pltfmName)
        {
            List<Product> ret = null;
            if(con != null)
            {
                try
                {
                    if (con.State != ConnectionState.Open) { con.Open(); }

                    string queryStr = @"select * from tabProducts where pltfmName=@pltfmName;";
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = queryStr;
                    cmd.Parameters.AddWithValue("@pltfmName", pltfmName);

                    ret = new List<Product>();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product p = new Product();
                            p.Id = reader.GetInt32(0);
                            p.PltfmName = reader.GetString(1);
                            p.PdctName = reader.GetString(2);
                            p.PdctInfo = reader.GetString(3);
                            ret.Add(p);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MyLogger.WriteLine("getAllPlatforms() get error!\n   " + ex.Message);
                }
            }
            return ret;
        }
        #endregion

        #region 按页 获取某个platform 下的Product
        public List<Product> getPdctPageList(int pageNow, int pageSize,string pltfm,ref int pageCount)
        {
            int rowCount = 0;
            List<Product> ret = null;

            //防止不正确的页数
            if (pageNow < 1)
                pageNow = 1;

            //开始分页操作
            if (con != null)
            {
                try
                {
                    if (con.State != ConnectionState.Open) { con.Open(); }

                    string query = @"select count(*) from tabProducts where pltfmName=@pltfm;";
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@pltfm", pltfm);

                    //查询记录数
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            rowCount = reader.GetInt32(0);
                        }
                        MyLogger.WriteLine(pltfm+" products has " + rowCount + " lines");
                    }
                    if (rowCount == 0) { return ret; }
                    else { ret = new List<Product>(); }
                }
                catch (Exception ex)
                {
                    MyLogger.WriteLine(ex.Message);
                }

                try
                {
                    //配置要查询的行数，页数
                    pageCount = (rowCount - 1) / pageSize + 1;
                    int rowStart = (pageNow - 1) * pageSize;

                    //查询记录
                    string queryPage = @"select * from tabProducts where pltfmName=@pltfm limit @start,@size";
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = queryPage;
                    cmd.Parameters.AddRange(new MySqlParameter[] {
                        new MySqlParameter("@pltfm",pltfm),
                        new MySqlParameter("@start", rowStart) ,
                        new MySqlParameter("@size",pageSize),
                    });

                    //查询Trial记录，并封装到类
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product p = new Product();
                            p.Id = reader.GetInt32(0);
                            p.PltfmName = reader.GetString(1);
                            p.PdctName = reader.GetString(2);
                            p.PdctInfo = reader.GetString(3);
                            ret.Add(p);
                        }
                    }
                }
                catch
                {
                    MyLogger.WriteLine("when query page product list error!");
                }
            }
            return ret;
        }
        #endregion

        #region 插入一条trial 记录
        /*
        public void insertTrial(TrialInfo trialInfo)
        {
            if (con != null)
            {
                try
                {
                    if (con.State != ConnectionState.Open) { con.Open(); }

                    string queryStr = @"insert into tabTrials values(null,@pltfm,@pdct,@actor,@date,
                                        @path,@bugPath,@info,@operat);";
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = queryStr;
                    cmd.Parameters.AddWithValue("@pltfm", trialInfo.Pltfm);
                    cmd.Parameters.AddWithValue("@pdct", trialInfo.Pdct);
                    cmd.Parameters.AddWithValue("@actor", trialInfo.Activator);
                    cmd.Parameters.AddWithValue("@date", trialInfo.Unique.Split('_')[1]);
                    string path = ContantInfo.Fs.path + trialInfo.Pltfm + "\\" + trialInfo.Pdct + "\\" +
                                  trialInfo.Unique + "\\";
                    cmd.Parameters.AddWithValue("@path", path);
                    cmd.Parameters.AddWithValue("@bugPath", path + "debug\\");
                    cmd.Parameters.AddWithValue("@info", trialInfo.Info);
                    cmd.Parameters.AddWithValue("@operat", trialInfo.Operator);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                        MyLogger.WriteLine("insert into tabtrials error!");
                    }
                    MyLogger.WriteLine("insert into tabtrials ok!");
                }
                catch
                {
                    MyLogger.WriteLine("insert tabtrials error!");
                }
            }
        }*/
        #endregion

        public void chgPswd(string userId,string md5Str)
        {
            if (con != null)
            {
                try
                {
                    if (con.State != ConnectionState.Open) { con.Open(); }

                    string queryStr = @"update tabusers set userpass=@pswd where userid=@uid ;";
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = queryStr;
                    cmd.Parameters.AddWithValue("@pswd", md5Str);
                    cmd.Parameters.AddWithValue("@uid", userId);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }catch(Exception ex)
                    {
                        throw new Exception("change pswd error!"+ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
