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
    /// 功能：积分兑换实体信息
    /// </summary>
    public class ExchangeEntity
    {

        /// <summary>
        /// 主键
        /// </summary>
        public int ID { get; set;}

        /// <summary>
        /// 账号
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 转出类型
        /// </summary>
        public int OutType { get; set; }

        /// <summary>
        /// 转入类型
        /// </summary>
        public int Intype { get; set; }

        /// <summary>
        ///创建时间 
        /// </summary>
        public DateTime CreateDate{ get; set; } = DateTime.Now;

        /// <summary>
        /// 兑换积分
        /// </summary>
        public int Integral { get; set; }

    }
}
