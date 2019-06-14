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
    /// 修改时间
    /// 功能：玩法设定控制类
    /// </summary>
    public class GameMethodController : BaseController
    {

        /// <summary>
        /// 玩法逻辑处理对象
        /// </summary>
        private GameMethodBLL mobjGameMethodBLL = new GameMethodBLL();

        /// <summary>
        /// 公司逻辑处理对象
        /// </summary>
        private CompanyBLL mobjComPanyBLL = new CompanyBLL();

        /// <summary>
        /// 玩法逻辑处理对象
        /// </summary>
        private PlayMethodBLL mobjPlayMethodBLL = new PlayMethodBLL();

        /// <summary>
        /// 赛事设定逻辑处理对象
        /// </summary>
        private CompetitionBLL mobjCompetitionBLL = new CompetitionBLL();


        /// <summary>
        /// 玩法设定视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获得玩法设定数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetGameMethodSetting() {
            IQueryable<CompanyEntity> objCompanys = mobjComPanyBLL.GetAll();
            IQueryable<PlayMethodEntity> objPlayMethod = mobjPlayMethodBLL.GetAll();
            IQueryable<GameMethodDTO> objGameMethodDTOs=mobjGameMethodBLL.GetGameMethodData();
            IQueryable<CompetitionEntity> objCompetitions = mobjCompetitionBLL.GetComprtitionByNotStart();
            var objData = new
            {
                Company = objCompanys,
                PlayMethod = ClassifyPlayMethod(objPlayMethod),
                GameMethod = objGameMethodDTOs,
                Competition=objCompetitions
            };
            return Content(JsonConvert.SerializeObject(objData));  
        }

        /// <summary>
        /// 添加/修改一条游戏玩法记录
        /// </summary>
        /// <param name="aobjGameMethod">游戏玩法实体类</param>
        /// <returns></returns>
        public ActionResult InsertOrUpdateGameMethod(GameMethodEntity aobjGameMethod) {
            bool isSuccess = false;
            if (aobjGameMethod.ID != 0)
            {
                isSuccess = mobjGameMethodBLL.UpdateGameMethod(aobjGameMethod);
            }
            else {
                isSuccess = mobjGameMethodBLL.InsertGameMethod(aobjGameMethod);
            }
          
            return Content(isSuccess.ToString());
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="aobjPlayMethiod">玩法类型集合</param>
        /// <returns></returns>
        public ActionResult InsertMethod(string astrPlayMethodJson) {
            bool isSuccess = mobjPlayMethodBLL.InserMore(JsonConvert.DeserializeObject<List<PlayMethodEntity>>(astrPlayMethodJson));
            return Content(isSuccess.ToString());
        }


        /// <summary>
        /// 修改玩法
        /// </summary>
        /// <param name="aEditEntity">玩法实体类</param>
        /// <returns></returns>
        public ActionResult UpdatePlayMethod(PlayMethodEntity aEditEntity) {

            bool isSuccess=mobjPlayMethodBLL.UpdatePlayMethod(aEditEntity);
            return Content(isSuccess.ToString());

        }

        /// <summary>
        /// 删除玩法
        /// </summary>
        /// <param name="aintID">编号</param>
        /// <returns></returns>
        public ActionResult DeltePlayMethod(int aintID) {

            bool isSuccess = mobjPlayMethodBLL.DeleteExType(aintID);
            return Content(isSuccess.ToString());
        }

        /// <summary>
        /// 在结算时或得改赛事的玩法
        /// </summary>
        /// <param name="aintComprtitionID">赛事id</param>
        /// <returns></returns>
        public ActionResult GetGameMethodByResult(int aintComprtitionID) {

           return Content(JsonConvert.SerializeObject(mobjGameMethodBLL.GetGameMethodByResult(aintComprtitionID)));
        }

        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="aobjResult">前台传来参数</param>
        /// <returns></returns>
        public ActionResult UpdateWinner(string astrResult,int aintBetID) {

            Dictionary<string, List<GameMethodDTO>> objResult = JsonConvert.DeserializeObject<Dictionary<string, List<GameMethodDTO>>>(astrResult);

            return Content(JsonConvert.SerializeObject(mobjGameMethodBLL.UpdateWinner(objResult,aintBetID)));
        }

        /// <summary>
        /// 删除多条玩法记录
        /// </summary>
        /// <param name="astrGameMethodID"></param>
        /// <returns></returns>
        public ActionResult DeleteGameMethodMore(string astrGameMethodIDs) {
            List<int> objGameMethodIDs = JsonConvert.DeserializeObject<List<int>>(astrGameMethodIDs);
            bool isSuccess = mobjGameMethodBLL.DeleteGameMethodMore(objGameMethodIDs);
            isSuccess = mobjPlayMethodBLL.DeleteMore(objGameMethodIDs);
            return Content(isSuccess.ToString());
        }

        /// <summary>
        /// 得到所有玩法记录
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPlayMethods() {
            IQueryable<PlayMethodEntity> objPlayMethods = mobjPlayMethodBLL.GetAll();
            return Content(JsonConvert.SerializeObject(objPlayMethods));
        }

        /// <summary>
        /// 添加多条赛事玩法记录
        /// </summary>
        /// <param name="aintCompetitionID">赛事ID</param>
        /// <param name="aintCompanyID">公司ID</param>
        /// <param name="astrPlayMethodIDs">玩法集合</param>
        /// <returns></returns>
        public ActionResult InsertGameMethod(int aintCompetitionID,int aintCompanyID,string astrPlayMethodIDs) {
            bool isSuccess= mobjGameMethodBLL.InsertGameMethodMore(aintCompetitionID,aintCompanyID,JsonConvert.DeserializeObject<List<int>>(astrPlayMethodIDs));
            return Content(isSuccess.ToString());
        }

        /// <summary>
        /// 把玩法按类型分类
        /// </summary>
        /// <param name="aobjPlayMethod"></param>
        /// <returns></returns>
        private object ClassifyPlayMethod(IQueryable<PlayMethodEntity> aobjPlayMethod)
        {
            List<string> objMethodType = new List<string>();
            Dictionary<string, List<PlayMethodEntity>> objPlayMethod = new Dictionary<string, List<PlayMethodEntity>>();
            foreach (var item in aobjPlayMethod)
            {
                if (!objMethodType.Contains(item.TypeName)) {
                    objMethodType.Add(item.TypeName);
                    objPlayMethod.Add(item.TypeName,aobjPlayMethod.Where(t=>t.TypeName.Equals(item.TypeName)).ToList());
                }
            }
            return objPlayMethod;
        }



    }
}