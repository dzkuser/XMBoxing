using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XMBOXING.BLL;
using XMBOXING.Comm;
using XMBOXING.MODEL;

namespace XMBOXING.Backstage.Controllers
{

    /// <summary>
    /// 作者：邓镇康
    /// 创建时间:2019-5-31
    /// 修改时间：
    /// 功能：赛事设定控制器
    /// </summary>
    public class CompetitionController:BaseController
    {

        /// <summary>
        /// 赛事设定逻辑处理对象
        /// </summary>
        private CompetitionBLL mobjCompetitionBLL = new CompetitionBLL();

        /// <summary>
        /// 投注逻辑处理对象
        /// </summary>
        private BetBLL mobjBetBLL = new BetBLL();

       /// <summary>
       /// 赛事设定视图
       /// </summary>
       /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 根据条件得到所有的赛事设定
        /// </summary>
        /// <param name="astrGameName">游戏名字</param>
        /// <param name="astrParticipant">参赛人名</param>
        /// <param name="aobjStartDate">时间</param>
        /// <returns></returns>
        public ActionResult GetComprtitions(string astrGameName, string astrParticipant, DateTime? aobjStartDate) {
            IQueryable<CompetitionEntity> objComprtitions=mobjCompetitionBLL.GetCompetitionByWhere(astrGameName,astrParticipant,aobjStartDate);
            return Content(JsonConvert.SerializeObject(objComprtitions));
        }

        /// <summary>
        /// 添加一条赛事记录
        /// </summary>
        /// <param name="aobjAddCompetition">赛事设定实体信息</param>
        /// <returns></returns>
        public ActionResult InsertComprtition(CompetitionEntity aobjAddCompetition) {
            aobjAddCompetition.StartDate = DateTime.Now;
            bool isSuccess = mobjCompetitionBLL.InsertCompetition(aobjAddCompetition);           
            return Content(isSuccess.ToString());
        }

        /// <summary>
        /// 修改一条赛事记录
        /// </summary>
        /// <param name="aobjUpdateComprtition">修改赛事设定信息</param>
        /// <returns></returns>
        public ActionResult UpdateComprtition(CompetitionEntity aobjUpdateComprtition) {
            bool isSuccess = mobjCompetitionBLL.UpdateCompetition(aobjUpdateComprtition);
            return Content(isSuccess.ToString());
        }


        /// <summary>
        /// 删除一场赛事
        /// </summary>
        /// <param name="aintID">编号</param>
        /// <returns></returns>
        public ActionResult DeleteComprtition(int aintID) {
           bool isSuccess=mobjCompetitionBLL.DeleteCompetition(aintID);
           return Content(isSuccess.ToString());
        }

        /// <summary>
        /// 得到首页展示的赛事
        /// </summary>
        /// <returns></returns>
        public ActionResult GetIndex() {
            return Content(JsonConvert.SerializeObject(mobjCompetitionBLL.GetCompetitionByIndex()));
        }


        /// <summary>
        /// 得到赛事结果汇总
        /// </summary>
        /// <param name="adicParam">条件参数</param>
        /// <returns></returns>
        public ActionResult GetComprtitionResult(string aintID, DateTime? aobjStartDate, DateTime? aobjEndDate)
        {          

            return Content(JsonConvert.SerializeObject(mobjCompetitionBLL.GetComprtitionResult(aintID,aobjStartDate,aobjEndDate)));

        }



        /// <summary>
        /// 得到赛事结果详细
        /// </summary>
        /// <param name="adicParam">条件参数</param>
        /// <returns></returns>
        public ActionResult GetComportitionResultInfo(string aintID,string astrAccountName, DateTime? aobjStartDate, DateTime? aobjEndDate)
        {
            Dictionary<string, object> objParam = new Dictionary<string, object>();
            objParam.Add("CompetitionID",aintID);
            objParam.Add("AccountName",astrAccountName);
            objParam.Add("StartDate",aobjStartDate);
            objParam.Add("EndDate",aobjEndDate);
            return Content(JsonConvert.SerializeObject(mobjCompetitionBLL.GetComportitionResultInfo(objParam)));

        }


        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="aintBetID">投注编号</param>
        /// <param name="astrAccountName">用户名称</param>
        /// <returns></returns>
        public ActionResult DeleteComportitionBet(int aintBetID,string astrAccountName) {
            bool isSuccess = mobjBetBLL.DeleteBet(aintBetID,astrAccountName);
            return Content(isSuccess.ToString());
        }

        

    }
}