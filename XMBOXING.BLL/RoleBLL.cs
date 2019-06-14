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
    /// 功能：角色
    /// </summary>
    public class RoleBLL
    {

        /// <summary>
        /// 角色数据访问对象
        /// </summary>
        private IRoleDAL mobjRoleDAL = new RoleDAL();


        /// <summary>
        /// 得到所有角色
        /// </summary>
        /// <returns></returns>
        public IQueryable<RoleEntity> GetRoles() {

            return mobjRoleDAL.GetAll();
        }

        /// <summary>
        /// 添加一个角色
        /// </summary>
        /// <param name="aobjAddRole">角色信息实体类</param>
        /// <returns></returns>
        public bool InsertRole(RoleEntity aobjAddRole) {
            return mobjRoleDAL.Insert(aobjAddRole);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="aintID">角色编号</param>
        /// <returns></returns>
        public bool DeleteRole(int aintID) {
            return mobjRoleDAL.Delete(aintID);
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="aEditRole">角色实体类</param>
        /// <returns></returns>
        public bool UpdateRole(RoleEntity aEditRole) {
            return mobjRoleDAL.Update(aEditRole);
        }

        /// <summary>
        /// 根据用户账号得到角色
        /// </summary>
        /// <param name="astrAccountName">用户账号</param>
        /// <returns></returns>
        public IQueryable<RoleEntity> GetRoleByAccountName(string astrAccountName) {
           return  mobjRoleDAL.GetRoleByAccountName(astrAccountName);
        }


        /// <summary>
        /// 删除多条角色记录
        /// </summary>
        /// <param name="aobjRoleIDs"></param>
        /// <returns></returns>
        public bool DeleteRoleMore(List<int> aobjRoleIDs) {

            return mobjRoleDAL.DeleteMore(aobjRoleIDs);

        }

    }
}
