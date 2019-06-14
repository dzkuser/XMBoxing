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
    /// 功能：角色实体类
    /// </summary>
    public class RoleEntity
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { get; set; }

        
        /// <summary>
        /// 角色名
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 角色状态
        /// </summary>
        public int RoleStatus { get; set; }
    }
}
