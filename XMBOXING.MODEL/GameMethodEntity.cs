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
    /// 功能：赛事玩法关联实体类
    /// </summary>
    public class GameMethodEntity
    {

        /// <summary>
        /// 主键编号
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 赛事ID
        /// </summary>
        public int CompetitionID { get; set; }

        /// <summary>
        /// 玩法ID
        /// </summary>
        public int PlayMethodID { get; set; }

        /// <summary>
        /// 公司ID
        /// </summary>
        public int CompanyID { get; set; }

        /// <summary>
        /// 是否是胜者
        /// </summary>
        public int IsWin { get; set; }

    }
}
