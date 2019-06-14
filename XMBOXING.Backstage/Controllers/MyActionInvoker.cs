using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using XMBOXING.Comm;
using XMBOXING.MODEL;

namespace XMBOXING.Backstage.Controllers
{

    /// <summary>
    /// 作者：邓镇康
    /// 创建时间:2019-5-30
    /// 修改时间：
    /// 功能：自定义处理执行控制器方法类
    /// </summary>
    public class MyActionInvoker:ControllerActionInvoker
    {

        /// <summary>
        /// 重写执行控制器方法
        /// </summary>
        /// <param name="controllerContext">控制器上下文对象</param>
        /// <param name="actionDescriptor">方法切面</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        protected override ActionResult InvokeActionMethod(ControllerContext controllerContext, ActionDescriptor actionDescriptor, IDictionary<string, object> parameters)
        {             
            Type type = actionDescriptor.ControllerDescriptor.ControllerType;
            string name = actionDescriptor.ActionName;
            ResultBase responseVo = new ResultBase();
            ActionResult actionResult = base.InvokeActionMethod(controllerContext, actionDescriptor, parameters);
            if (actionResult is ContentResult)
            {
                ContentResult contentResult = (ContentResult)actionResult;
                GetErroResult(type, name, contentResult.Content, ref responseVo);
                contentResult.Content = JsonConvert.SerializeObject(responseVo);
            }
            controllerContext.HttpContext.Response.Clear();
            return actionResult;
        }






        /// <summary>
        /// 获取错误
        /// </summary>
        /// <param name="aConType">控制器类型</param>
        /// <param name="methodName">方法名字</param>
        /// <param name="result">执行结果</param>
        /// <param name="responseVo">响应实体类</param>
        private void GetErroResult(Type aConType, string methodName, string result, ref ResultBase responseVo)
        {
            MethodInfo method = aConType.GetMethod(methodName);
            Attribute attribute = method.GetCustomAttribute(typeof(ErroAttribute));
            if (attribute != null)
            {
                Type type = attribute.GetType();
                object[] objRelus = (object[])type.GetProperty("Rule").GetValue(attribute);
                string strCode = null;
                for (int i = 1; i < objRelus.Length; i += 2)
                {
                    if (objRelus[i - 1].Equals(result))
                    {
                        strCode = objRelus[i].ToString();
                        responseVo.ErrorMsg = ResourceHelp.GetResourceString(strCode);
                        responseVo.ErrorCode = strCode;
                        break;
                    }
                }
                if (strCode == null)
                {
                    responseVo.Result = result;
                }
            }
            else
            {
                responseVo.Result = result;
            }
        }

      
    }
}