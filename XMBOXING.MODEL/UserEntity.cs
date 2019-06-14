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
    /// 功能：账号信息实体
    /// </summary>
    public class UserEntity
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 账号名称
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string UserPassWord { get; set; }

        /// <summary>
        /// 代理
        /// </summary>
        public string Agent { get; set; }

        /// <summary>
        /// 用户积分
        /// </summary>
        public decimal Integral { get; set; } = 0;

    }
}
