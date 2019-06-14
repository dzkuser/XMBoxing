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
    /// 功能：角色数据访问实体类
    /// </summary>  
    public class RoleDAL:BaseDAL<RoleEntity>,IRoleDAL
    {
        public RoleDAL() {
            this.ToKey("ID");
            this.ToTable("tbRole");
        }

        /// <summary>
        /// 根据用户账号得到角色
        /// </summary>
        /// <param name="astrAccountName">用户账号</param>
        /// <returns></returns>
        public IQueryable<RoleEntity> GetRoleByAccountName(string astrAccountName)
        {
            string strSql = "select * from tbBet where AccountName=@AccountName";
            Dictionary<string, object> objParam = new Dictionary<string, object>();
            objParam.Add("@AccountName",astrAccountName);
            return QueryList(strSql,objParam).AsQueryable();
        }



    }
}
