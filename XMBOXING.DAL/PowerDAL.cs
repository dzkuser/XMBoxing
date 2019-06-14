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
    /// 功能：权限数据访问实现类
    /// </summary>
    public class PowerDAL:BaseDAL<PowerEntity>,IPowerDAL
    {

        public PowerDAL() {
            this.ToKey("ID");
            this.ToTable("tbPower");
        }

        /// <summary>
        /// 根据角色获得权限信息
        /// </summary>
        /// <param name="aobjPowerIDs">角色ID</param>
        /// <returns></returns>
        public IQueryable<PowerEntity> GetPowerByIDs(List<int> aobjPowerIDs) {
            string strSql = "select * from tbPower where RoleID in @aobjPowerIDs";
            return QueryList(strSql,new { aobjPowerIDs=aobjPowerIDs.ToArray()}).AsQueryable();
        }


        /// 删除该角色ID的记录
        /// </summary>
        /// <param name="aintRoleID">角色ID</param>
        /// <returns></returns>
        public bool DeletePowerByRoleID(int aintRoleID)
        {
            Dictionary<string, object> objParam = new Dictionary<string, object>();
            objParam.Add("@RoleID", aintRoleID);
            string strSql = "delete tbPower where RoleID=@RoleID";
            return Execute(strSql, objParam) > 0 ? true : false;
        }

        /// <summary>
        /// 删除该角色ID集合的记录
        /// </summary>
        /// <param name="aobjRoleIDs">ID集合</param>
        /// <returns></returns>
        public bool DeletePowerByRoleIDs(List<int> aobjRoleIDs)
        {
            string strSql = "delete tbPower where RoleID in @RoleIDs";
            return Execute(strSql,new { RoleIDs=aobjRoleIDs})>0?true:false;
        }

    }
}
