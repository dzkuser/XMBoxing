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
    /// 功能：用户信息处理控制类
    /// </summary>
    public class UserController : BaseController
    {
        /// <summary>
        /// 用户逻辑对象
        /// </summary>
        private UserBLL mobjUserBLL = new UserBLL();

        /// <summary>
        /// 用户角色关联逻辑处理对象
        /// </summary>
        private UserRoleBLL mobjUserRole = new UserRoleBLL();

        /// <summary>
        /// 权限逻辑处理对象
        /// </summary>
        private PowerBLL mobjPower = new PowerBLL();

        /// <summary>
        /// 跳转到登录视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="aobjUser">用户信息实体类</param>
        /// <returns></returns>       
        public ActionResult Login(UserEntity aobjUser) {

            UserEntity objUser =mobjUserBLL.Login(aobjUser.AccountName,aobjUser.UserPassWord);
            if (objUser!=null) {
                Session["user"] = aobjUser;
                List<int> objRoleIDs = mobjUserRole.GetRoleIDByAccount(aobjUser.AccountName);
                Session["power"] = mobjPower.GetPowerByIDs(objRoleIDs).ToDictionary(t=>t.ID); 
            }
   
         return Content(JsonConvert.SerializeObject(objUser));
        }
      
    }
}