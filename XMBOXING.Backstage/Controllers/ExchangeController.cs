using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XMBOXING.BLL;
using XMBOXING.MODEL;

namespace XMBOXING.Backstage.Controllers
{

    /// <summary>
    /// 作者：邓镇康
    /// 创建时间:2019-5-31
    /// 修改时间：
    /// 功能：积分兑换控制类
    /// </summary>
    public class ExchangeController : BaseController
    {

        /// <summary>
        /// 积分兑换逻辑处理对象
        /// </summary>
        private ExchangeBLL mobjExchangeBLL = new ExchangeBLL();

        /// <summary>
        /// 积分类型
        /// </summary>
        private ExTypeBLL mobjExType = new ExTypeBLL();

       /// <summary>
       /// 积分兑换视图
       /// </summary>
       /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 兑换积分
        /// </summary>
        /// <param name="aobjAddExchange">兑换积分记录实体信息</param>
        /// <returns></returns>
        public ActionResult Conversion(ExchangeEntity aobjAddExchange) {
          bool isSucess=mobjExchangeBLL.InsertExchange(aobjAddExchange);
          return Content(isSucess.ToString());
        }

        /// <summary>
        /// 兑换信息汇总视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ConversionRecordGather() {
            return View();
        }

        /// <summary>
        /// 获得积分兑换记录积分汇总
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRecordGather(int? aintOutType,int? aintInType,DateTime? StartDate,DateTime? EndDate) {
            if (StartDate == null) {
                StartDate = DateTime.Now;
            }
            if (EndDate == null) {
                EndDate = DateTime.Now;
            }
          IQueryable<ExchangeDTO> objExchangeDTOs=mobjExchangeBLL.GetExchangeGroup(aintOutType,aintInType,StartDate,EndDate);
          return Content(JsonConvert.SerializeObject(objExchangeDTOs));
        }


        /// <summary>
        /// 兑换信息详细
        /// </summary>
        /// <returns></returns>
        public ActionResult ConversionRecordInfo() {
            return View();
        }


        /// <summary>
        /// 获得积分兑换记录详细
        /// </summary>
        /// <returns></returns>
        public ActionResult GetExchangeInfo() {
            IQueryable<ExchangeDTO> objExchangeInfos = mobjExchangeBLL.GetExchangesRecord();
            return Content(JsonConvert.SerializeObject(objExchangeInfos));
        }


        /// <summary>
        /// 获得积分类型
        /// </summary>
        /// <returns></returns>
        public ActionResult GetExType() {
            return Content(JsonConvert.SerializeObject(mobjExType.GetAll()));
        }
    }
}