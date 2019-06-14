using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMBOXING.MODEL
{

    /// <summary>
    /// 作者：邓镇康
    /// 创建时间:2019-6-4
    /// 修改时间：
    /// 功能：赛事数据传输类
    /// </summary>
    public class CompetitionDTO
    {

        /// <summary>
        /// 赛事ID
        /// </summary>
        public int CompetitionID { get; set; }


        /// <summary>
        /// 投注编号
        /// </summary>
        public int BetID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 比赛主题
        /// </summary>
        public string GameTheme { get; set; }

        /// <summary>
        /// 比赛名称
        /// </summary>
        public string GameName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 游戏类型
        /// </summary>
        public int? GameType { get; set; }

        /// <summary>
        /// 第一个参赛者
        /// </summary>
        public string ParticipantOne { get; set; }
   
        /// <summary>
        /// 第二个参赛者
        /// </summary>
        public string ParticipantTwo { get; set; }
  
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 投注积分
        /// </summary>
        public int BetIntegral { get; set; }

        /// <summary>
        /// 投注总数
        /// </summary>
        public int BetTotal { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int BetState { get; set; }


        /// <summary>
        /// 赢得的积分
        /// </summary>
        public decimal WinIntegral { get; set; }


        /// <summary>
        /// 状态名字
        /// </summary>
        public string BetStateName { get {
                return BetState > 0 ? "以结算" : "未结算";
        } }


    }
}
