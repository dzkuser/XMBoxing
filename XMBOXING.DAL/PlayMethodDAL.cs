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
    /// 功能：玩法数据访问对象
    /// </summary>
    public class PlayMethodDAL:BaseDAL<PlayMethodEntity>,IPlayMethodDAL
    {
        public PlayMethodDAL() {

            this.ToTable("tbPlayMethod");
            this.ToKey("ID");
        }
    }
}
