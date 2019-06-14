using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using XMBOXING.BLL;
using XMBOXING.MODEL;

namespace XMBOXING.Backstage.Controllers
{

    /// <summary>
    /// 作者：邓镇康
    /// 创建时间:2019-6-6
    /// 修改时间：
    /// 功能：权限过滤器
    /// </summary>
    public class PowerFilter : IResultFilter
    {

        /// <summary>
        /// 权限逻辑处理对象
        /// </summary>
        private PowerBLL mobjPowerBLL = new PowerBLL();


        /// <summary>
        ///结果处理了之后
        /// </summary>
        /// <param name="filterContext">过滤器上下文对象</param>
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {

            string path = filterContext.HttpContext.Request.Url.AbsolutePath;
            string filePath = filterContext.HttpContext.Server.MapPath("/");
            ControllerContext controllerContext = filterContext.Controller.ControllerContext;
            string viewName = filterContext.Controller.ControllerContext.RouteData.GetRequiredString("action");

            HttpSessionStateBase objSesson= filterContext.HttpContext.Session;
            StringWriter sw = new StringWriter();
            List<PowerItem> objItemPower = new List<PowerItem>();
            PowerItem objPowerItem = GetPowerByPath(filePath,path,ref objItemPower);
            if (objPowerItem == null) {
                return;
            }
            string strPower=objPowerItem.text;                   
            List<string> keys=GetPowerName(GetDbPower(strPower,objSesson),objItemPower);
            if (keys == null)
            {
                return;
            }
            IView view = ViewEngines.Engines.FindPartialView(controllerContext, viewName).View;

            if (view == null)
            {
                return;
            }
            ViewContext viewContext = new ViewContext(controllerContext, view, controllerContext.Controller.ViewData, controllerContext.Controller.TempData, sw);
            viewContext.View.Render(viewContext, sw);
            StringBuilder st = ExcludeHtml(sw, keys);
            controllerContext.HttpContext.Response.Clear();
            controllerContext.HttpContext.Response.Write(st);
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {

        }

        /// <summary>
        /// 根据权限处理html
        /// </summary>
        /// <param name="stringWriter">写入流</param>
        /// <param name="keys">没有的权限信息</param>
        /// <returns></returns>
        private StringBuilder ExcludeHtml(StringWriter stringWriter, List<string> keys)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder = stringWriter.GetStringBuilder();
            foreach (var item in keys)
            {
                string button = String.Format("<.*class={1}>.*{0}.*</.{2}>", item, "\".*power.*\"", "{1,5}");
                Regex regex = new Regex(@button);
                Match math = regex.Match(stringBuilder.ToString());
                string value = math.Value;
                if (value != "")
                    stringBuilder.Replace(value, "");
            }

            return stringBuilder;
        }

     
        /// <summary>
        /// 根据URL 得到相对应的权限
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="path">请求URL</param>
        /// <param name="objItemPower">返回出去的权限集合</param>
        /// <returns></returns>
        private PowerItem GetPowerByPath(string filePath, string path,ref List<PowerItem> objItemPower)
        {
            string josnString = File.ReadAllText(filePath + "\\Content\\authority.json", Encoding.Default);
            JObject jObject = JObject.Parse(josnString);
            JToken zh = jObject["zh_cn"];
            List<PowerItem> pairs = JsonConvert.DeserializeObject<List<PowerItem>>(zh.ToString());
            PowerItem powerItem = pairs.Where(t => t.path.Equals(path)).FirstOrDefault();
            if (powerItem == null) {
                return null;
            }
            if (powerItem.action == null)
            {
                objItemPower = pairs.Where(t => t.parentID == powerItem.id).ToList();
            }
            else {
                objItemPower = powerItem.action;
            }
            return powerItem;
        }

        /// <summary>
        /// 获得
        /// </summary>
        /// <param name="powerName">权限名</param>
        /// <returns></returns>
        private List<PowerEntity> GetDbPower(string powerName,HttpSessionStateBase aobjSession)
        {
            List<PowerEntity> entities = new List<PowerEntity>();
            //   Dictionary<int, PowerEntity> powers = mobjPowerBLL.GetPowerByIDs(roleIDs).ToDictionary(t => t.ID);
            Dictionary<int, PowerEntity> powers = (Dictionary<int, PowerEntity>)aobjSession["power"];
            if (powers == null) {
                return entities;
            }
            PowerEntity powerItem = powers.Where(t => t.Value.PowerName.Equals(powerName)).FirstOrDefault().Value;
       
            if (powerItem == null)
            {
                return entities;
            }
             entities = powers.Values.Where(t => t.ParentID == powerItem.NodeID).ToList();
            return entities;
        }

        /// <summary>
        /// 获得没权限的值
        /// </summary>
        /// <param name="aobjPowers">该用户在数据库拥有的值</param>
        /// <param name="aobjPower">该功能的所有权限</param>
        /// <returns></returns>
        private List<string> GetPowerName(List<PowerEntity> aobjPowers,List<PowerItem> aobjPower) {
            List<string> objPowerKeys = new List<string>();
            List<PowerItem> objFilePower = new List<PowerItem>();
           
            foreach (var item in aobjPower)
            {
                if (aobjPowers.Where(t => t.PowerName.Equals(item.text)).FirstOrDefault() == null) {
                    objPowerKeys.Add(item.text);
                }
            }
            return objPowerKeys;

        }
       
    }
}