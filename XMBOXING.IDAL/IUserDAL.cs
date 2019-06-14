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
    /// 创建时间:2019-5-28
    /// 修改时间：
    /// 功能：用户信息数据访问接口
    /// </summary>
    public interface IUserDAL:IBaseDAL<UserEntity>
    {

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="astrAccountName">登录名</param>
        /// <param name="astrUserPassWord">账号</param>
        /// <returns></returns>
        bool Login(string astrAccountName,string astrUserPassWord);

        /// <summary>
        /// 修改积分
        /// </summary>
        /// <param name="astrAccountName">用户账号</param>
        /// <param name="aintIntegral">积分</param>
        /// <returns></returns>
         bool UpdateIntegral(string astrAccountName, object aintIntegral);

        /// <summary>
        /// 根据用户账号查询用户信息
        /// </summary>
        /// <param name="astrAccountName">用户账号</param>
        /// <returns></returns>
        UserEntity GetUserByAccountName(string astrAccountName);
    }
}
