using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncSystem.Dao
{
    public class User
    {
        private int id;
        private string userId;
        private string userName;
        private string teamName;
        private string userTel;
        private string userLevel;
        private string userImgPath;
        private string userInfo;

        #region user Property
        public int Id
        {
            set { id = value; }
            get { return id; }
        }
        public string UserId
        {
            set { userId = value; }
            get { return userId; }
        }
        public string UserName
        {
            set {  userName = value; }
            get { return userName; }
        }
        public string TeamName
        {
            set {  teamName= value; }
            get { return teamName; }
        }
        public string UserTel
        {
            set { userTel = value; }
            get { return userTel; }
        }
        public string UserLevel
        {
            set { userLevel = value; }
            get { return userLevel; }
        }
        public string UserImgPath
        {
            set { userImgPath = value; }
            get { return userImgPath; }
        }
        public string UserInfo
        {
            set { userInfo = value; }
            get { return userInfo; }
        }
        #endregion

        public User()
        {
            id = Id;
            userId = UserId;
            userName = UserName;
            teamName = TeamName;
            userTel = UserTel;
            userLevel = UserLevel;
            userImgPath = UserImgPath;
            userInfo = UserInfo;
        }

        public override string ToString()
        {
            string res = "";
            res += "User " + id;
            res += "| " + userId;
            res += "| " + userName;
            res += "| " + teamName;
            res += "| " + userTel;
            res += "| " + userLevel;
            res += "| [" + userImgPath+"]";
            res += "| " + userInfo;
            res += "\n";
            return res;
        }
    }
}
