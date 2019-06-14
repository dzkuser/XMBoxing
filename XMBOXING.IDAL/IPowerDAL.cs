using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMBOXING.MODEL;

namespace XMBOXING.IDAL
{

    /// <summary>
    /// 作者：邓镇康
    /// 创建时间:2019-
    /// 修改时间：
    /// 功能：权限数据访问对象
    /// </summary>
    public interface IPowerDAL:IBaseDAL<PowerEntity>
    {
        /// <summary>
        /// 根据角色获得权限信息
        /// </summary>
        /// <param name="aobjPowerIDs">角色ID</param>
        /// <returns></returns>
        IQueryable<PowerEntity> GetPowerByIDs(List<int> aobjPowerIDs);


        /// 删除该角色ID的记录
        /// </summary>
        /// <param name="aintRoleID">角色ID</param>
        /// <returns></returns>
        bool DeletePowerByRoleID(int aintRoleID);

        /// <summary>
        /// 删除该角色ID集合的记录
        /// </summary>
        /// <param name="aobjRoleIDs">ID集合</param>
        /// <returns></returns>
        bool DeletePowerByRoleIDs(List<int> aobjRoleIDs);
    }
}
