using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// 功能：权限控制控制器
    /// </summary>
    public class PowerController:BaseController
    {


        /// <summary>
        /// 权限逻辑处理对象
        /// </summary>
        private PowerBLL mobjPowerBLL = new PowerBLL();

        /// <summary>
        /// 角色逻辑处理对象
        /// </summary>
        private RoleBLL mobjRoleBLL = new RoleBLL();

        /// <summary>
        /// 用户角色逻辑处理对象
        /// </summary>
        private UserRoleBLL mobjUserRoleBLL = new UserRoleBLL();


        /// <summary>
        /// 得到菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMenu() {
            string josnString = System.IO.File.ReadAllText(Server.MapPath("/")+ "\\Content\\authority.json", Encoding.Default);
            JObject jObject = JObject.Parse(josnString);
            JToken zh = jObject["zh_cn"];
            List<PowerItem> pairs = JsonConvert.DeserializeObject<List<PowerItem>>(zh.ToString());
            return Content(JsonConvert.SerializeObject(pairs));
        }

        /// <summary>
        /// 得到菜单权限
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMenuPower() {
            string josnString = System.IO.File.ReadAllText(Server.MapPath("/") + "\\Content\\authority.json", Encoding.Default);
            JObject jObject = JObject.Parse(josnString);
            JToken zh = jObject["zh_cn"];
            List<PowerItem> pairs = JsonConvert.DeserializeObject<List<PowerItem>>(zh.ToString());
            List<PowerItem> objChilden = new List<PowerItem>();
            foreach (var item in pairs)
            {               
                SetMenuPower(ref objChilden, item);
            }
            pairs.AddRange(objChilden);
            return Content(JsonConvert.SerializeObject(pairs));
        }



        /// <summary>
        /// 得到所有的角色
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRoles() {
            return Content(JsonConvert.SerializeObject(mobjRoleBLL.GetRoles()));
        }

        /// <summary>
        /// 根据用户得到角色
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRoleByUser(string astrAccountName) {
            if (astrAccountName == null) {
                astrAccountName = ((UserEntity)Session["user"]).AccountName;
            }
            IQueryable<RoleEntity> objRoles=mobjRoleBLL.GetRoleByAccountName(astrAccountName);
            return Content(JsonConvert.SerializeObject(objRoles));
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="aintRoleID">角色ID</param>
        /// <returns></returns>
        public ActionResult DeleteRole(int aintRoleID) {
            mobjRoleBLL.DeleteRole(aintRoleID);
            mobjUserRoleBLL.DeleteUserRoleByRoleID(aintRoleID);
            bool isSuccess=mobjPowerBLL.DeletePowerByRoleID(aintRoleID);
            return Content(isSuccess.ToString());
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="aobjAddRole">角色实体类</param>
        /// <returns></returns>
        public ActionResult InsertRole(RoleEntity aobjAddRole) {
             bool isSuccess= mobjRoleBLL.InsertRole(aobjAddRole);
            return Content(isSuccess.ToString());
        }

        /// <summary>
        /// 批量删除角色
        /// </summary>
        /// <param name="astrRoleIDs">角色ID集合json字符串</param>
        /// <returns></returns>
        public ActionResult DeleteRoleMore(string astrRoleIDs) {
            List<int> objRoleIDs = JsonConvert.DeserializeObject<List<int>>(astrRoleIDs);
            mobjRoleBLL.DeleteRoleMore(objRoleIDs);
            mobjUserRoleBLL.DeleteUserRoleByRoleIDs(objRoleIDs);
            bool isSuccess=mobjPowerBLL.DeletePowerByRoleIDs(objRoleIDs);
            return Content(isSuccess.ToString());
        }


        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="aobjEditRole">前台传来的修改信息的角色实体类</param>
        /// <returns></returns>
        public ActionResult UpdateRole(RoleEntity aobjEditRole) {

            bool isSuccess=mobjRoleBLL.UpdateRole(aobjEditRole);
            return Content(isSuccess.ToString());

        }


        /// <summary>
        /// 角色授权
        /// </summary>
        /// <param name="aobjPower">授权信息</param>
        /// <returns></returns>
        public ActionResult AuthorizationRole(int aintRoleID,string astrPowerName) {
            List<PowerEntity> aobjPowerName = JsonConvert.DeserializeObject<List<PowerEntity>>(astrPowerName);
               mobjPowerBLL.DeletePowerByRoleID(aintRoleID);
            bool isSuccess = mobjPowerBLL.InsertPowerMore(aintRoleID,aobjPowerName);
            return Content(isSuccess.ToString());
        }

        /// <summary>
        /// 根据角色ID查询拥有的权限
        /// </summary>
        /// <param name="aintRoleID"></param>
        /// <returns></returns>
        public ActionResult GetPowerByRoleID(int aintRoleID) {

            List<int> objRoleIDs = new List<int>();
            objRoleIDs.Add(aintRoleID);
            IQueryable<PowerEntity> objPowers = mobjPowerBLL.GetPowerByIDs(objRoleIDs);
            return Content(JsonConvert.SerializeObject(objPowers));
        }

        /// <summary>
        /// 分配用户
        /// </summary>
        /// <param name="aintRoleID">角色ID</param>
        /// <param name="aobjAccountNames">用户账号集合</param>
        /// <returns></returns>
        public ActionResult AllocationRole(int aintRoleID,string astrAccountNames) {
            List<string> aobjAccountNames = JsonConvert.DeserializeObject<List<string>>(astrAccountNames);
            List<UserRoleEntity> objUserRoles = new List<UserRoleEntity>();
            foreach (var item in aobjAccountNames)
            {
                UserRoleEntity objUserRole = new UserRoleEntity();
                objUserRole.AccountName = item;
                objUserRole.RoleID = aintRoleID;
                objUserRoles.Add(objUserRole);
            }
            bool isSuccess=mobjUserRoleBLL.InsertUserRoleMore(objUserRoles);
            return Content(isSuccess.ToString());
        }

        /// <summary>
        /// 修改菜单权限中的值（私有）
        /// </summary>
        /// <param name="aobjMenuPower">菜单权限集合</param>
        /// <param name="aobjPower">一条记录</param>
        private void SetMenuPower(ref List<PowerItem> aobjMenuPower, PowerItem aobjPower) {

            if (aobjPower.action != null) {

                foreach (var item in aobjPower.action)
                {
                    item.id = -1;
                    item.parentID = aobjPower.id;
                    aobjMenuPower.Add(item);
                }

            }


        }
    }
}