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
    /// 功能：赛事玩法关联数据访问实现类
    /// </summary>
    public class GameMethodDAL:BaseDAL<GameMethodEntity>,IGameMethodDAL
    {
        public GameMethodDAL() {
            this.ToTable("tbGameMethod");
            this.ToKey("ID");
        }

        public IQueryable<GameMethodDTO> GetGameMethodData() {

            string sql = "  select b.ID AS CompanyID,b.CompanyName,d.ID as MethodID,d.MethodName,d.TypeName,c.GameName,c.GameTheme,c.ID as CompetitionID " +
                "from tbGameMethod  a  " +
                "join tbCompany b on a.CompanyID=b.ID  " +
                "join tbPlayMethod d on a.PlayMethodID=d.ID  " +
                "right join tbCompetition c on a.CompetitionID=c.ID  where c.GameType=0";
           return  QueryList<GameMethodDTO>(sql).AsQueryable();
        }

        /// <summary>
        /// 查询改赛事的玩法
        /// </summary>
        /// <param name="aintComprtitionID">赛事ID</param>
        /// <returns></returns>
        public IQueryable<GameMethodDTO> GetPlayMethodByComprtition(int aintComprtitionID)
        {
            Dictionary<string, object> objParam = new Dictionary<string, object>();
            objParam.Add("@ComprtitionID",aintComprtitionID);
            return QueryList<GameMethodDTO>("P_GameMethod_GetPlayMethodByComprtition",objParam,System.Data.CommandType.StoredProcedure).AsQueryable();
        }


        /// <summary>
        /// 在结算时或得改赛事的玩法
        /// </summary>
        /// <param name="aintComprtitionID">赛事id</param>
        /// <returns></returns>
        public IQueryable<GameMethodDTO> GetGameMethodByResult(int aintComprtitionID) {
            string strSql = "select TypeName, MethodName, a.ID as MethodID, Sum(c.Integral) as TotalIntegral from tbGameMethod a join tbPlayMethod b on a.PlayMethodID = b.ID left join tbBet c on c.MethodID = a.ID where a.CompetitionID = @CompetitionID  and MethodStatus = 0 group by  TypeName,MethodName,a.ID";
            Dictionary<string, object> objParam = new Dictionary<string, object>();
            objParam.Add("@CompetitionID",aintComprtitionID);
            return QueryList<GameMethodDTO>(strSql,objParam).AsQueryable();
        }

        /// <summary>
        /// 修改赢的玩法
        /// </summary>
        /// <param name="aintMethodID">玩法ID集合</param>
        /// <returns></returns>
        public bool UpdateWinMethod(List<int> aintMethodID) {
            string strSql = "update tbGameMethod Set IsWin=1 where ID in @Ids";
            return Execute(strSql, new { Ids = aintMethodID.ToArray() }) >0?true:false;
        }


        /// <summary>
        /// 删除多条游戏设定记录
        /// </summary>
        /// <param name="aobjGameMethodIDs">游戏设定ID集合</param>
        /// <returns></returns>
        public bool DeleteGameMethodMore(List<int> aobjGameMethodIDs)
        {
            string strSql = "Delete tbGameMethod where PlayMethodID in @aobjGameMethodIDs";
            return Execute(strSql,new { aobjGameMethodIDs =aobjGameMethodIDs})>0?true:false;
        }
    }
}
