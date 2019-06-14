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
    /// 功能：角色数据访问接口
    /// </summary>
    public interface IRoleDAL:IBaseDAL<RoleEntity>
    {

        /// <summary>
        /// 根据用户账号得到角色
        /// </summary>
        /// <param name="astrAccountName">用户账号</param>
        /// <returns></returns>
        IQueryable<RoleEntity> GetRoleByAccountName(string astrAccountName);
    }
}
