using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMBOXING.IDAL;
using XMBOXING.MODEL;

namespace XMBOXING.DAL
{

    /// <summary>
    /// 作者：邓镇康
    /// 创建时间:2019-5-28
    /// 修改时间：
    /// 功能：账号信息数据访问实现类
    /// </summary>
    public class UserDAL:BaseDAL<UserEntity>,IUserDAL
    {
        public UserDAL() {
            this.ToKey("ID");
            this.ToTable("tbUser");
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="astrAccountName">登录名</param>
        /// <param name="astrUserPassWord">账号</param>
        /// <returns></returns>
        public bool Login(string astrAccountName, string astrUserPassWord)
        {
            Dictionary<string, object> objParam = new Dictionary<string, object>();
            objParam.Add("@AccountName",astrAccountName);
            objParam.Add("@UserPassWord",astrUserPassWord);
            return QuerySingle<int>("P_User_Login",objParam,System.Data.CommandType.StoredProcedure)>0?true:false;
        }

        /// <summary>
        /// 修改积分
        /// </summary>
        /// <param name="astrAccountName">用户账号</param>
        /// <param name="aintIntegral">积分</param>
        /// <returns></returns>
        public bool UpdateIntegral(string astrAccountName,object aintIntegral) {
            Dictionary<string, object> objParam = new Dictionary<string, object>();
            objParam.Add("@AccountName", astrAccountName);
            objParam.Add("@Integral",aintIntegral);
            string strSql = "update tbUser Set Integral+=@Integral where AccountName=@AccountName";
            return Execute(strSql,objParam)>0?true:false;
        }


        /// <summary>
        /// 根据用户账号查询用户信息
        /// </summary>
        /// <param name="astrAccountName">用户账号</param>
        /// <returns></returns>
        public UserEntity GetUserByAccountName(string astrAccountName) {
            string strSql = "select * from tbUser where AccountName=@AccountName";
            return QuerySingle(strSql,new { AccountName=astrAccountName });

        }
    }
}
