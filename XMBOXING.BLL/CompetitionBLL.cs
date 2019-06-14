using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMBOXING.DAL;
using XMBOXING.IDAL;
using XMBOXING.MODEL;

namespace XMBOXING.BLL
{

    /// <summary>
    /// 作者：邓镇康
    /// 创建时间:2019-5-28
    /// 修改时间：
    /// 功能：赛事设定逻辑处理类
    /// </summary>
    public class CompetitionBLL
    {
        /// <summary>
        /// 赛事设定数据访问对象
        /// </summary>
        private ICompetitionDAL mobjCompetitionDAL = new CompetitionDAL();

        /// <summary>
        /// 添加一条赛事设定记录
        /// </summary>
        /// <param name="aAddCompetition">赛事信息实体记录</param>
        /// <returns></returns>
        public bool InsertCompetition(CompetitionEntity aAddCompetition) {

            return mobjCompetitionDAL.Insert(aAddCompetition);
        }


        /// <summary>
        /// 修改赛事设定
        /// </summary>
        /// <param name="aEditComprtition">赛事信息实体记录</param>
        /// <returns></returns>
        public bool UpdateCompetition(CompetitionEntity aEditComprtition) {

            return mobjCompetitionDAL.Update(aEditComprtition);
        }

        /// <summary>
        /// 删除一条赛事设定记录
        /// </summary>
        /// <param name="aintId">赛事设定编号</param>
        /// <returns></returns>
        public bool DeleteCompetition(int aintId) {

            return mobjCompetitionDAL.Delete(aintId);
        }

        /// <summary>
        /// 根据编号查询
        /// </summary>
        /// <param name="aintId">赛事设定编号</param>
        /// <returns></returns>
        public CompetitionEntity GetComprtitionByID(int aintId) {
            return mobjCompetitionDAL.GetEntityByID(aintId);
        }

        /// <summary>
        /// 查询所有记录
        /// </summary>
        /// <returns></returns>
        public IQueryable<CompetitionEntity> GetAll() {

            return mobjCompetitionDAL.GetAll();

        }

        /// <summary>
        /// 根据条件查询赛事信息
        /// </summary>
        /// <param name="adicParam">条件</param>
        /// <returns></returns>
        public IQueryable<CompetitionEntity> GetCompetitionByWhere(string astrGameName,string astrParticipant,DateTime? aobjStartDate) {

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("GameName",astrGameName);
            param.Add("Participant",astrParticipant);
            param.Add("StartDate",aobjStartDate);
            return mobjCompetitionDAL.GetCompetitionByWhere(param);
        }

        /// <summary>
        /// 查询要进行图片轮播的赛事
        /// </summary>
        /// <returns></returns>
        public IQueryable<CompetitionEntity> GetCompetitionByCarousel() {
            return mobjCompetitionDAL.GetCompetitionByCarousel();
        }


        /// <summary>
        /// 查询要在首页展示的赛事
        /// </summary>
        /// <returns></returns>
        public IQueryable<CompetitionEntity> GetCompetitionByIndex()
        {
            return mobjCompetitionDAL.GetCompetitionByIndex();
        }

        /// <summary>
        /// 查询在日期内的赛事
        /// </summary>
        /// <param name="aDate">时间</param>
        /// <returns></returns>
        public IQueryable<CompetitionEntity> GetComprtitionByDate(DateTime? aDate)
        {
            if (aDate == null) {
                aDate = DateTime.Now;
            }
            return mobjCompetitionDAL.GetComprtitionByDate(aDate.Value);
        }

        /// <summary>
        /// 得到赛事结果汇总
        /// </summary>
        /// <param name="adicParam">条件参数</param>
        /// <returns></returns>
        public IQueryable<CompetitionDTO> GetComprtitionResult(string aintID,DateTime? aobjStartDate,DateTime? aobjEndDate) {

            Dictionary<string, object> objParam = new Dictionary<string, object>();
            objParam.Add("CompetitionID",aintID);
            objParam.Add("StartDate",aobjStartDate);
            objParam.Add("EndDate",aobjEndDate);

            return mobjCompetitionDAL.GetComprtitionResult(objParam);

        }

        /// <summary>
        /// 得到赛事结果详细
        /// </summary>
        /// <param name="adicParam">条件参数</param>
        /// <returns></returns>
        public IQueryable<CompetitionDTO> GetComportitionResultInfo(Dictionary<string, object> adicParam)
        {          
            return mobjCompetitionDAL.GetComportitionResultInfo(adicParam);
        }

        /// <summary>
        /// 查询未开始
        /// </summary>
        /// <returns></returns>
        public IQueryable<CompetitionEntity> GetComprtitionByNotStart() {
            return mobjCompetitionDAL.GetComprtitionByNotStart();
        }
    }
}
