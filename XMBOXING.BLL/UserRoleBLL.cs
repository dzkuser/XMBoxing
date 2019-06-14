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
    /// 创建时间:2019-6-6
    /// 修改时间：
    /// 功能：用户角色关联逻辑处理层
    /// </summary>
    public class UserRoleBLL
    {
        /// <summary>
        /// 用户角色关联数据访问对象
        /// </summary>
        private IUserRoleDAL mobjUserRole = new UserRoleDAL();


        /// <summary>
        /// 根据用户账号得到角色ID
        /// </summary>
        /// <param name="astrAccountName">账号</param>
        /// <returns></returns>
        public List<int> GetRoleIDByAccount(string astrAccountName)
        {
            IQueryable<UserRoleEntity> objUserRole = mobjUserRole.GetRoleByAccount(astrAccountName);
            List<int> objRoleIDs = new List<int>();
            foreach (var item in objUserRole)
            {
                objRoleIDs.Add(item.RoleID);
            }

            return objRoleIDs;
        }

        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="aobjUserRole">用户角色实体类</param>
        /// <returns></returns>
        public bool InsertUserRole(UserRoleEntity aobjUserRole) {
           return  mobjUserRole.Insert(aobjUserRole);
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="aobjUserRoles">用户角色信息实体类集合</param>
        /// <returns></returns>
        public bool InsertUserRoleMore(List<UserRoleEntity> aobjUserRoles) {
            return mobjUserRole.InsertMore(aobjUserRoles);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="aintID">编号</param>
        /// <returns></returns>
        public bool DeleteUserRole(int aintID) {
            return mobjUserRole.Delete(aintID);
        }

        /// <summary>
        /// 删除该角色ID的记录
        /// </summary>
        /// <param name="aintRoleID"></param>
        /// <returns></returns>
        public bool DeleteUserRoleByRoleID(int aintRoleID) {

            return mobjUserRole.DeleteUserRoleByRoleID(aintRoleID);
        }

        /// <summary>
        /// 删除该角色ID集合的记录
        /// </summary>
        /// <param name="aobjRoleIDs">ID集合</param>
        /// <returns></returns>
        public bool DeleteUserRoleByRoleIDs(List<int> aobjRoleIDs) {
            return mobjUserRole.DeleteUserRoleByRoleIDs(aobjRoleIDs);
        }
    }
}
