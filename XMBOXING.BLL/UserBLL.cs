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
    /// 创建时间:2019-5-28
    /// 修改时间
    /// 功能：账号信息逻辑处理类
    /// </summary>
    public class UserBLL
    {
        /// 账号信息数据访问对象
        /// </summary>
        private IUserDAL mobjUserDAL = new UserDAL();

        /// <summary>
        /// 添加一条账号信息记录
        /// </summary>
        /// <param name="aAddUser">账号实体记录</param>
        /// <returns></returns>
        public bool InsertUser(UserEntity aAddUser)
        {
            return mobjUserDAL.Insert(aAddUser);
        }


        /// <summary>
        /// 修改账号信息
        /// </summary>
        /// <param name="aEditUser">账号实体记录</param>
        /// <returns></returns>
        public bool UpdateUser(UserEntity aEditUser)
        {

            return mobjUserDAL.Update(aEditUser);
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="aintId">编号</param>
        /// <returns></returns>
        public bool DeleteUser(int aintId)
        {
            return mobjUserDAL.Delete(aintId);
        }

        /// <summary>
        /// 根据编号查询
        /// </summary>
        /// <param name="aintId">编号</param>
        /// <returns></returns>
        public UserEntity GetExTypeByID(int aintId)
        {
            return mobjUserDAL.GetEntityByID(aintId);
        }

        /// <summary>
        /// 查询所有记录
        /// </summary>
        /// <returns></returns>
        public IQueryable<UserEntity> GetAll()
        {
            return mobjUserDAL.GetAll();
        }


        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="astrAccountName">登录名</param>
        /// <param name="astrUserPassWord">账号</param>
        /// <returns></returns>
        public UserEntity Login(string astrAccountName, string astrUserPassWord)
        {
            bool isSuccess= mobjUserDAL.Login(astrAccountName,astrUserPassWord);
            if (isSuccess) {
                return GetUserByAccountName(astrAccountName);
            }
            return null;
        }

        /// <summary>
        /// 修改积分
        /// </summary>
        /// <param name="astrAccountName">用户账号</param>
        /// <param name="aintIntegral">积分</param>
        /// <returns></returns>
        public bool UpdateIntegral(string astrAccountName, int aintIntegral)
        {
            return mobjUserDAL.UpdateIntegral(astrAccountName,aintIntegral);
        }

        /// <summary>
        /// 根据用户账号查询用户信息
        /// </summary>
        /// <param name="astrAccountName">用户账号</param>
        /// <returns></returns>
        public UserEntity GetUserByAccountName(string astrAccountName) {

            return mobjUserDAL.GetUserByAccountName(astrAccountName);
        }
    }
}
