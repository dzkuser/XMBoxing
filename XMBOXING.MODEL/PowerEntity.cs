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
    /// 功能：权限实体类
    /// </summary>
    public class PowerEntity
    {
        /// <summary>
        /// 主键编号
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// 权限名
        /// </summary>
        public string PowerName { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        public int ParentID { get; set; } = 0;

        /// <summary>
        /// 节点ID
        /// </summary>
        public int NodeID { get; set; }
    }
}
