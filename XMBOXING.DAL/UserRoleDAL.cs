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
    /// 创建时间:2019-6-5
    /// 修改时间：
    /// 功能：用户角色数据访问实现类
    /// </summary>
    public class UserRoleDAL:BaseDAL<UserRoleEntity>,IUserRoleDAL
    {

        public UserRoleDAL() {
            this.ToTable("tbUserRole");
            this.ToKey("ID");
        }

        /// <summary>
        /// 根据用户账号得到角色ID
        /// </summary>
        /// <param name="astrAccountName">账号</param>
        /// <returns></returns>
        public IQueryable<UserRoleEntity> GetRoleByAccount(string astrAccountName) {
            Dictionary<string, object> objParam = new Dictionary<string, object>();
            objParam.Add("@AccountName",astrAccountName);
            string strSql = "select * from tbUserRole where AccountName=@AccountName and RoleStatus=0";
            return QueryList(strSql,objParam).AsQueryable();
        }

        /// <summary>
        /// 删除该角色ID的记录
        /// </summary>
        /// <param name="aintRoleID">角色ID</param>
        /// <returns></returns>
        public bool DeleteUserRoleByRoleID(int aintRoleID)
        {
            Dictionary<string, object> objParam = new Dictionary<string, object>();
            objParam.Add("@RoleID", aintRoleID);
            string strSql = "delete tbUserRole where RoleID=@RoleID";
            return Execute(strSql,objParam)>0?true:false;
        }

        /// <summary>
        /// 删除该角色ID集合的记录
        /// </summary>
        /// <param name="aobjRoleIDs">ID集合</param>
        /// <returns></returns>
        public bool DeleteUserRoleByRoleIDs(List<int> aobjRoleIDs) {
            string strSql = "delete tbUserRole where RoleID in @RoleID";
            return Execute(strSql, new { RoleID = aobjRoleIDs }) > 0 ? true:false ;
        }

    }
}
