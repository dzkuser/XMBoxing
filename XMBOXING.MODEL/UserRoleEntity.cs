using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMBOXING.MODEL
{

    /// <summary>
    /// 作者：邓镇康
    /// 创建时间:2019-6-5
    /// 修改时间：
    /// 功能：用户角色关联实体类
    /// </summary>
    public class UserRoleEntity
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID { get; set; }

    }
}
