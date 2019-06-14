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
    /// 功能：比赛设定实体信息
    /// </summary>
    public class CompetitionEntity
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { get; set; }

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
       [DateTime]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 游戏类型
        /// </summary>
        public int? GameType { get; set; }

        /// <summary>
        /// 是否轮播
        /// </summary>
        public int? IsCarousel { get; set; }

        /// <summary>
        /// 是否主页显示
        /// </summary>
        public int? IsIndex { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        ///链接线路 
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 第一个参赛者
        /// </summary>
        public string ParticipantOne { get; set; }

        /// <summary>
        /// 第一个参赛者的图片
        /// </summary>
        public string OnePicture { get; set; }


        /// <summary>
        /// 第二个参赛者
        /// </summary>
        public string ParticipantTwo { get; set; }

        /// <summary>
        /// 第二个参赛者的图片
        /// </summary>
        public string TwoPicture { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDate { get; set; }

    }
}
