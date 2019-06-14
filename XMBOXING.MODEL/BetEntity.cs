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
    /// 修改时间：2019-
    /// 功能：用户投注信息实体
    /// </summary>
    public class BetEntity
    {

        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 用户登录名
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 玩法ID
        /// </summary>
        public int MethodID { get; set; }

        /// <summary>
        /// 赛事ID
        /// </summary>
        public int CompetitionID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; } = DateTime.Now;


        /// <summary>
        /// 投注状态
        /// </summary>
        public int BetState { get; set; }

        /// <summary>
        /// 投注的积分
        /// </summary>
        public int Integral { get; set; } = 20;

        /// <summary>
        /// 不是数据库字段
        /// 玩法计数
        /// </summary>
        /// 
        [NotEntityFiled]
        public int MethodCount { get; set; }

        /// <summary>
        /// 赢取积分
        /// </summary>
      //  [NotEntityFiled]
        public decimal WinIntegral { get; set; } = 0;
    }
}
