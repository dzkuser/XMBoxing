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
    /// 创建时间:2019-6-5
    /// 修改时间：
    /// 功能：用户角色数据访问接口
    /// </summary>
    public interface IUserRoleDAL:IBaseDAL<UserRoleEntity>
    {

        /// <summary>
        /// 根据用户账号得到角色ID
        /// </summary>
        /// <param name="astrAccountName">账号</param>
        /// <returns></returns>
        IQueryable<UserRoleEntity> GetRoleByAccount(string astrAccountName);


        /// <summary>
        /// 删除该角色ID的记录
        /// </summary>
        /// <param name="aintRoleID">角色ID</param>
        /// <returns></returns>
        bool DeleteUserRoleByRoleID(int aintRoleID);


        /// <summary>
        /// 删除该角色ID集合的记录
        /// </summary>
        /// <param name="aobjRoleIDs">ID集合</param>
        /// <returns></returns>
        bool DeleteUserRoleByRoleIDs(List<int> aobjRoleIDs);

    }
}
