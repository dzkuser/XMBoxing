using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMBOXING.IDAL;
using XMBOXING.MODEL;

namespace XMBOXING.DAL
{

    /// <summary>
    /// 作者：邓镇康
    /// 创建时间:2019-5-28
    /// 修改时间：
    /// 功能：参赛人员数据访问实现类
    /// </summary>
    public class ParticipantDAL:BaseDAL<ParticipantEntity>,IParticipantDAL
    {

        public ParticipantDAL() {
            this.ToTable("tbParticipant");
            this.ToKey("ID");
        }

    }
}
