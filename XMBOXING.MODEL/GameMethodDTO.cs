using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMBOXING.MODEL
{

    /// <summary>
    /// 作者：邓镇康
    /// 创建时间:2019-5-30
    /// 修改时间：
    /// 功能：玩法设定数据传输类
    /// </summary>
    public class GameMethodDTO
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        public int CompanyID { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 玩法ID
        /// </summary>
        public int MethodID { get; set; }

        /// <summary>
        /// 玩法
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// 玩法类型
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 赛事名字
        /// </summary>
        public string GameName { get; set; }

        /// <summary>
        /// 赛事主题
        /// </summary>
        public string GameTheme { get; set; }  

        /// <summary>
        /// 赛事ID
        /// </summary>
        public int CompetitionID { get; set; }

        /// <summary>
        /// 玩法总积分
        /// </summary>
        public int TotalIntegral { get; set; }

        /// <summary>
        /// 是否胜利
        /// </summary>
        public int IsWin { get; set; }
    }
}
