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
    /// 功能：参赛人员实体信息
    /// </summary>
    public class ParticipantEntity
    {

        /// <summary>
        /// 编号ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 赛事ID
        /// </summary>
        public int CompetitionID { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string Picture { get; set; }

    }
}
