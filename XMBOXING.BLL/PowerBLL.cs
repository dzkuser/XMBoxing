using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMBOXING.DAL;
using XMBOXING.IDAL;
using XMBOXING.MODEL;

namespace XMBOXING.BLL
{

    /// <summary>
    /// 作者：邓镇康
    /// 创建时间:2019-6-5
    /// 修改时间：
    /// 功能：权限功能逻辑处理类
    /// </summary>
    public class PowerBLL
    {
        /// <summary>
        /// 权限控制数据访问对象
        /// </summary>
        private IPowerDAL mobjPowerDAL = new PowerDAL();

        /// <summary>
        /// 根据角色获得权限信息
        /// </summary>
        /// <param name="aobjPowerIDs">角色ID</param>
        /// <returns></returns>
        public IQueryable<PowerEntity> GetPowerByIDs(List<int> aobjPowerIDs)
        {
            return mobjPowerDAL.GetPowerByIDs(aobjPowerIDs);
        }

        /// 删除该角色ID的记录
        /// </summary>
        /// <param name="aintRoleID">角色ID</param>
        /// <returns></returns>
        public bool DeletePowerByRoleID(int aintRoleID)
        {
            return mobjPowerDAL.DeletePowerByRoleID(aintRoleID);
        }

        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="aintRoleID">角色ID</param>
        /// <param name="aobjPowerName">权限</param>
        /// <returns></returns>
        public bool InsertPowerMore(int aintRoleID,List<PowerEntity> aobjPowerName) {

            List<PowerEntity> objPowers = new List<PowerEntity>();
            foreach (var item in aobjPowerName)
            {           
                item.RoleID = aintRoleID;
                objPowers.Add(item);
            }
            return mobjPowerDAL.InsertMore(objPowers);

        }

        /// <summary>
        /// 删除该角色ID集合的记录
        /// </summary>
        /// <param name="aobjRoleIDs">ID集合</param>
        /// <returns></returns>
        public bool DeletePowerByRoleIDs(List<int> aobjRoleIDs) {
            return mobjPowerDAL.DeletePowerByRoleIDs(aobjRoleIDs);
        }

    }
}
