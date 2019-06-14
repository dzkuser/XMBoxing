using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMBOXING.MODEL
{

    /// <summary>
    /// 作者：邓镇康
    /// 创建时间:2019-5-28
    /// 修改时间：
    /// 功能：玩法信息实体
    /// </summary>
    public class PlayMethodEntity
    {
        /// <summary>
        /// 编号ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 玩法名称
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// 玩法状态
        /// </summary>
        public int MethodStatus { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        public int ParentID { get; set; }

    }
}
