using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMBOXING.MODEL
{

    /// <summary>
    /// 作者：邓镇康
    /// 创建时间:2019-5-29
    /// 修改时间：
    /// 功能：积分兑换数据传输对象
    /// </summary>
    public class ExchangeDTO
    {

        /// <summary>
        /// 转入类型
        /// </summary>
        public int OutType { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 转出类型
        /// </summary>
        public int InType { get; set; }

        /// <summary>
        /// 转入类型名
        /// </summary>
        public string OutName { get; set; }

        /// <summary>
        /// 转出类型名
        /// </summary>
        public string InName { get; set; }
        
        /// <summary>
        /// 积分
        /// </summary>
        public int Integral { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束时间时间
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 转账类型
        /// </summary>
        public string TypeName { get {
                return Integral > 0 ? "转入" : "转出";
            } }

    }
}
