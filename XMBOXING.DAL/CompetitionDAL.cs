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
    /// 功能：赛事设定数据访问实现类
    /// </summary>
    public class CompetitionDAL:BaseDAL<CompetitionEntity>,ICompetitionDAL
    {
        public CompetitionDAL() {
            this.ToTable("tbCompetition");
            this.ToKey("ID");
        }

        /// <summary>
        /// 根据条件查询赛事信息
        /// </summary>
        /// <param name="adicParam">条件</param>
        /// <returns></returns>
        public IQueryable<CompetitionEntity> GetCompetitionByWhere(Dictionary<string ,object> adicParam) {
            WhereBuilder objWhereBuilder = new WhereBuilder();
            objWhereBuilder.FromSql = "tbCompetition";
            objWhereBuilder.AddWhereAndParameter(adicParam,"GameName","GameName","like","'%'+@GameName+'%'","and");
            objWhereBuilder.AddWhereAndParameter(adicParam,"StartDate","StartDate","=","@StartDate","and");
            objWhereBuilder.AddWhereAndParameter(adicParam, "Participant", "ParticipantOne","like", "'%'+@Participant+'%'","and");
            objWhereBuilder.AddWhereAndParameter(adicParam, "Participant", "ParticipantTwo", "like", "'%'+@Participant+'%'","or");
            string strSql = objWhereBuilder.GetSql();
         
            return QueryList(strSql,objWhereBuilder.Parameters).AsQueryable();
        }

        /// <summary>
        /// 调用存储过程
        /// 查询要进行图片轮播的赛事
        /// </summary>
        /// <returns></returns>
        public IQueryable<CompetitionEntity> GetCompetitionByCarousel() {
            return QueryList("P_Competition_GetCompetitionByCarousel",null,System.Data.CommandType.StoredProcedure).AsQueryable();
        }


        /// <summary>
        /// 查询要在首页展示的赛事
        /// </summary>
        /// <returns></returns>
        public IQueryable<CompetitionEntity> GetCompetitionByIndex()
        {
            return QueryList("P_Competition_GetCompetitionByIndex",null,System.Data.CommandType.StoredProcedure).AsQueryable();
        }

        /// <summary>
        /// 查询在日期内的赛事
        /// </summary>
        /// <param name="aDate">时间</param>
        /// <returns></returns>
        public IQueryable<CompetitionEntity> GetComprtitionByDate(DateTime aDate)
        {
            Dictionary<string, object> objParam = new Dictionary<string, object>();
            objParam.Add("@Date",aDate);
            return QueryList("P_Competition_GetComprtitionByDate",objParam,System.Data.CommandType.StoredProcedure).AsQueryable();
        }

        /// <summary>
        /// 得到赛事结果
        /// </summary>
        /// <param name="adicParam">条件参数</param>
        /// <returns></returns>
        public IQueryable<CompetitionDTO> GetComprtitionResult(Dictionary<string,object> adicParam) {
            WhereBuilder objWhereBuilder = new WhereBuilder();
            objWhereBuilder.AddWhereAndParameter(adicParam,"CompetitionID", "CompetitionID", "=", "@CompetitionID","and");
            objWhereBuilder.AddWhereAndParameter(adicParam,"StartDate", "StartDate","=", "@StartDate","and");
            objWhereBuilder.AddWhereAndParameter(adicParam, "EndDate", "EndDate","=","@EndDate","and");

            string strSql =String.Format("select a.ID as CompetitionID,a.GameTheme,a.GameName,a.StartDate,a.EndDate,A.ParticipantOne,A.ParticipantTwo,b.BetState,Sum(B.Integral) AS BetIntegral,COUNT(B.ID) as BetTotal from " +
                "tbCompetition  a left join tbBet b on a.ID=b.CompetitionID where a.GameType=1 {0} " +
                " GROUP BY a.ID,a.GameTheme,a.GameName,a.StartDate,a.EndDate,A.ParticipantOne,A.ParticipantTwo,b.BetState",objWhereBuilder.mobjSql.ToString());
            return QueryList<CompetitionDTO>(strSql,objWhereBuilder.Parameters).AsQueryable();
        }

        /// <summary>
        /// 得到赛事结果详细
        /// </summary>
        /// <param name="adicParam">条件参数</param>
        /// <returns></returns>
        public IQueryable<CompetitionDTO> GetComportitionResultInfo(Dictionary<string,object> adicParam) {
            WhereBuilder objWhereBuilder = new WhereBuilder();
            objWhereBuilder.AddWhereAndParameter(adicParam, "AccountName", "c.AccountName","=", "@AccountName","and");
            objWhereBuilder.AddWhereAndParameter(adicParam, "CompetitionID", "CompetitionID", "=", "@CompetitionID", "and");
            objWhereBuilder.AddWhereAndParameter(adicParam, "StartDate", "StartDate", "=", "@StartDate", "and");
            objWhereBuilder.AddWhereAndParameter(adicParam, "EndDate", "EndDate", "=", "@EndDate", "and");

            string strSql = String.Format("select b.ID as BetID,a.ID as CompetitionID,a.GameTheme,a.GameName,a.StartDate,a.EndDate,A.ParticipantOne,A.ParticipantTwo,b.BetState,b.WinIntegral,c.Integral " +
                "from  tbCompetition  a left join tbBet b on a.ID = b.CompetitionID join tbUser c on b.AccountName = c.AccountName where a.GameType = 1  {0}",objWhereBuilder.mobjSql);
            return QueryList<CompetitionDTO>(strSql,objWhereBuilder.Parameters).AsQueryable();
        }


        /// <summary>
        /// 查询未开始
        /// </summary>
        /// <returns></returns>
        public IQueryable<CompetitionEntity> GetComprtitionByNotStart()
        {
            string strSql = "select * from tbCompetition where GameType=0";
            return QueryList(strSql).AsQueryable();
        }
    }
}
