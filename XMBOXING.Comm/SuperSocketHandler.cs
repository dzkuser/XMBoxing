
using Newtonsoft.Json;
using SuperSocket.SocketBase;
using SuperWebSocket;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web.Script.Serialization;

using XMBOXING.Comm;
using XMBOXING.MODEL;

namespace XMBOXING.Comm
{

    /// <summary>
    /// 作者：邓镇康
    /// 创建时间:2019-5-28
    /// 修改时间：
    /// 功能：socket处理类
    /// </summary>
    public class SuperSocketHandler
    {
        /// <summary>
        /// 用户session集合
        /// </summary>
        private static Dictionary<string, WebSocketSession> gobjUserSession = new Dictionary<string, WebSocketSession>();

        /// <summary>
        /// 赛事房间的集合
        /// </summary>
        private static Dictionary<string, List<string>> gobjGameRoom = new Dictionary<string, List<string>>();

        /// <summary>
        /// 用户令牌集合
        /// </summary>
        private static Dictionary<string, string> gobjUserTkoen = new Dictionary<string, string>();


        /// <summary>
        /// 执行方法的命名空间
        /// </summary>
        private static string gstrClassPath = ResourceHelp.GetResourceString("mbns");

        #region 启动WebSocket
        public void SetUp()
        {
            WebSocketServer webSocket = new WebSocketServer();
            webSocket.NewSessionConnected += HandlerNewSessionConnected;
            webSocket.NewMessageReceived += HandlerNewMessageReceived;
            webSocket.SessionClosed += HanderSessionClosed;
            webSocket.Setup(ConfigurationManager.AppSettings["ip"], Convert.ToInt32(ConfigurationManager.AppSettings["port"]));
       
            webSocket.Start();
        }
        #endregion

        #region socket触发
        /// <summary>
        /// 连接关闭时触发
        /// </summary>
        /// <param name="session">用户连接Session</param>
        /// <param name="value"></param>
        private void HanderSessionClosed(WebSocketSession aobjSocketSession, CloseReason aobjValue)
        {

        }

        /// <summary>
        /// 有新信息接收时触发
        /// </summary>
        /// <param name="session">用户连接Session</param>
        /// <param name="value">接收的值</param>
        private void HandlerNewMessageReceived(WebSocketSession aobjSocketSession, string astrValue)
        {
            handlerMessage(astrValue,aobjSocketSession);
        }

        /// <summary>
        /// 有新连接时触发
        /// </summary>
        /// <param name="aobjSocketSession">用户连接Session</param>
        private void HandlerNewSessionConnected(WebSocketSession aobjSocketSession)
        {
            gobjUserSession.Add(aobjSocketSession.SessionID,aobjSocketSession);
        }

        #endregion

        /// <summary>
        /// 处理前端传来的信息判断要执行什么操作
        /// </summary>
        /// <param name="astrMessage">信息</param>
        /// <param name="aobjSocketSession">websocket连接Session</param>
        public void handlerMessage(string astrMessage, WebSocketSession aobjSocketSession)
        {
            SocketEntity socketEntity = JsonConvert.DeserializeObject<SocketEntity>(astrMessage);
            SocketEnum socketEnum = (SocketEnum)Enum.Parse(typeof(SocketEnum), socketEntity.Tag);
            switch (socketEnum)
            {            
                case SocketEnum.ac:
                    handlerControllerAction(socketEntity, aobjSocketSession);
                    break;
                case SocketEnum.c:
                    updateSessionKey(socketEntity, aobjSocketSession);
                    break;
                case SocketEnum.i:
                    InRoom(socketEntity);
                    break;
                case SocketEnum.q:
                    break;            
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="astrRoomID"></param>
        /// <param name="objBet"></param>
        public  void  SendBetData(string astrRoomID,BetEntity objBet) {

            if (!gobjGameRoom.ContainsKey(astrRoomID)) {
                return;
            }
            SocketEntity objSocketMessage = new SocketEntity() {
                Tag = "n",
                ToUser = gobjGameRoom[astrRoomID],
                Message = JsonConvert.SerializeObject(objBet)
            };
            handlerSendMessage(objSocketMessage);
        }

        /// <summary>
        /// 处理用户退出房间
        /// </summary>
        /// <param name="aobjSocketEntity">socket实体类</param>
        private void QuitRoom(SocketEntity aobjSocketEntity) {
           string strKey=gobjGameRoom.Where(t=>t.Value.Contains(aobjSocketEntity.FromUser)).FirstOrDefault().Key;
            if (strKey==null) {
                return;
            }
            List<string> objUserList= gobjGameRoom[strKey];
            objUserList.Remove(aobjSocketEntity.FromUser);
            gobjGameRoom[strKey] = objUserList;
        }

        /// <summary>
        /// 处理进入房间
        /// </summary>
        /// <param name="aobjSocketEntity">socket实体类</param>
        private void InRoom(SocketEntity aobjSocketEntity) {

            if (gobjGameRoom.ContainsKey(aobjSocketEntity.RoomID))
            {
                gobjGameRoom[aobjSocketEntity.RoomID].Add(aobjSocketEntity.FromUser);
            }
            else {
                List<string> objRoom = new List<string>();
                objRoom.Add(aobjSocketEntity.FromUser);
                gobjGameRoom.Add(aobjSocketEntity.RoomID,objRoom);
            }

        }

        /// <summary>
        /// 替换session
        /// </summary>
        /// <param name="aobjSocketEntity">socket实体类</param>
        /// <param name="aobjSession"></param>
        private void updateSessionKey(SocketEntity aobjSocketEntity, WebSocketSession aobjSession)
        {
            gobjUserSession.Remove(aobjSession.SessionID);
            gobjUserSession.Remove(aobjSocketEntity.FromUser);
            gobjUserSession.Add(aobjSocketEntity.FromUser, aobjSession);

            //  gobjUserTkoen.Add(aobjSession.SessionID,aobjSocketEntity.FromUser);
        }


        /// <summary>
        /// 处理发送信息
        /// </summary>
        /// <param name="aSocketMessage">信息实体对象</param>
        /// <param name="aUserSession">用户 websocket session 可以为空</param>
        public void handlerSendMessage(SocketEntity aSocketMessage, WebSocketSession aUserSession = null)
        {

            if (aUserSession != null)
            {

                if (!aUserSession.InClosing)
                    aUserSession.Send(JsonConvert.SerializeObject(aSocketMessage));
                return;
            }
            foreach (var item in aSocketMessage.ToUser)
            {
                if (gobjUserSession.ContainsKey(item))
                {
                    WebSocketSession toUser = gobjUserSession[item];
                    if (!toUser.InClosing)
                        toUser.Send(JsonConvert.SerializeObject(aSocketMessage));
                }

            }

        }

        /// <summary>
        /// 处理要访问方法
        /// </summary>
        /// <param name="aSocketMessage">信息实体对象</param>
        /// <param name="aUserSession">用户 websocket session 可以为空</param>
        private void handlerControllerAction(SocketEntity aobjSocketEnriry, WebSocketSession aobjSocketSession)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            //得到要执行的方法名称和类名
            string message = aobjSocketEnriry.ActionMethod;
            string[] am = message.Split('.');
            //得到需要传入的参数
            Dictionary<string, object> param = js.Deserialize<Dictionary<string, object>>(aobjSocketEnriry.Message);

            object backObj = null;
            //得到要执行的方法对象和类实例对象

            ResultBase responseVo = new ResultBase();
        //    try
          //  {
                MethodInfo method = GetActionMethod(out backObj, className: am[0], method: am[1]);
                //得到方法执行数据
                object result = takeData(method, param, backObj);
                responseVo.Result = result;
              
           // }
           //catch (Exception ex)
           // {

           //     responseVo.ErrorCode = "500";
           //     responseVo.ErrorMsg = ex.Message;
           //}


            //处理ResponseVo对象并发送数据
            aobjSocketEnriry.Message = JsonHelper.ReplaceDateTime(js.Serialize(responseVo));
            if (aobjSocketEnriry.FromUser != "")
            {
                List<string> vs = new List<string>();
                vs.Add(aobjSocketEnriry.FromUser);
                aobjSocketEnriry.ToUser = vs;
                aobjSocketSession = null;
            }

            handlerSendMessage(aobjSocketEnriry, aobjSocketSession);
        }

        /// <summary>
        /// 实体映射 把传来的参数
        /// </summary>
        /// <param name="paramMethod"></param>
        /// <param name="method"></param>
        /// <param name="param"></param>
        private void entityMapping(ref Dictionary<string, object> paramMethod, Dictionary<string, Type> parameter, ref Dictionary<string, object> param, bool takeKey = false)
        {


            foreach (var item in parameter)
            {
                Type type = item.Value;

                if (type.IsClass && type != typeof(string))
                {
                    Dictionary<string, PropertyInfo> pairs = type.GetProperties().ToDictionary(t => t.Name);
                    object newObj = type.Assembly.CreateInstance(type.FullName);
                    if (parameter.Count > 1)
                    {
                        takeKey = true;
                    }
                    if (typeof(IList).IsAssignableFrom(type))
                    {
                        Type[] types = type.GetGenericArguments();
                        if (!param.ContainsKey(item.Key.ToUpper()))
                        {
                            continue;
                        }
                        AssembleList(ref newObj, param[item.Key.ToUpper()], types[0]);
                    }
                    else
                    {
                        if (takeKey)
                        {
                            Dictionary<string, object> objValue = (Dictionary<string, object>)param[item.Key.ToUpper()];
                            keyToUpper(ref objValue);
                            MapperEntity(pairs, objValue, ref newObj);
                        }
                        else
                        {
                            MapperEntity(pairs, param, ref newObj);
                        }

                    }
                    paramMethod.Add(item.Key, newObj);
                }
                else
                {
                    if (param.ContainsKey(item.Key.ToUpper())) {
                        DateTime objTime;
                        object objValue = param[item.Key.ToUpper()];
                        if (DateTime.TryParse(param[item.Key.ToUpper()].ToString(), out objTime)) {
                            objValue = objTime;
                        }
                        paramMethod.Add(item.Key,objValue);
                    }
                        
                     
                }

            }
        }

        /// <summary>
        /// 映射集合类型数据
        /// </summary>
        /// <param name="obj">方法参数对象</param>
        /// <param name="param">参数</param>
        /// <param name="valueType">值类型</param>
        private void AssembleList(ref object obj, object param, Type valueType)
        {
            Type type = param.GetType();

            foreach (var value in (ArrayList)param)
            {
                Dictionary<string, object> valueV = (Dictionary<string, object>)value;
                object objValue = valueType.Assembly.CreateInstance(valueType.FullName);
                keyToUpper(ref valueV);
                MapperEntity(valueType.GetProperties().ToDictionary(t => t.Name), valueV, ref objValue);
                Type objType = obj.GetType();

                objType.GetMethod("Add").Invoke(obj, new object[] { objValue });
            }
        }

        /// <summary>
        /// 功能：把传来的参数映射到调用方法需要的实体类
        /// </summary>
        /// <param name="fields">实体类属性</param>
        /// <param name="param">前端的参数</param>
        /// <param name="obj">实体类对象</param>
        private void MapperEntity(Dictionary<string, PropertyInfo> fields, Dictionary<string, object> param, ref object obj)
        {

            foreach (var item in fields)
            {
                if (item.Value.PropertyType.IsClass && item.Value.PropertyType != typeof(string))
                {
                    object newObj = null;
                    Type objValueType = item.Value.PropertyType;
                    if (typeof(IList).IsAssignableFrom(objValueType))
                    {
                        Type[] types = objValueType.GetGenericArguments();
                        if (!param.ContainsKey(item.Key.ToUpper()))
                        {
                            continue;
                        }
                        newObj = objValueType.Assembly.CreateInstance(objValueType.FullName);

                        AssembleList(ref newObj, param[item.Key.ToUpper()], types[0]);
                    }
                    else
                    {
                        Dictionary<string, object> pairs = new Dictionary<string, object>();
                        PropertyInfo[] propertyInfos = objValueType.GetProperties();
                        Dictionary<string, Type> keys = new Dictionary<string, Type>();
                        keys.Add(objValueType.Name, objValueType);
                        entityMapping(ref pairs, keys, ref param, true);
                        newObj = pairs.Values.ToArray()[0];
                    }

                    item.Value.SetValue(obj, newObj);
                    param.Remove(item.Key);
                }
                else
                {

                    if (param.ContainsKey(item.Key.ToUpper()))
                    {

                        object value = null;
                        if (item.Value.GetCustomAttribute(typeof(DateTimeAttribute)) != null)
                        {
                            value = Convert.ToDateTime(param[item.Key.ToUpper()]);
                        }
                        else
                        {
                            value = param[item.Key.ToUpper()];
                        }
                        item.Value.SetValue(obj, value);
                        param.Remove(item.Key);
                    }

                }

            }
        }

        //private void ChangeAccountValue(PropertyInfo aobjProperty,Dictionary<string,object> aobjParam) {

        //    if (aobjProperty.Name.Equals("AccountName")) {
        //        if (gobjUserTkoen.ContainsKey(aobjParam["AccountName"])) {

        //        }
        //    }

        //}

        /// <summary>
        /// 把字典的键变成大写
        /// </summary>
        /// <param name="param">字典对象</param>
        private void keyToUpper(ref Dictionary<string, object> param)
        {
            Dictionary<string, object> paramUpper = new Dictionary<string, object>();
            foreach (var item in param)
            {
                paramUpper.Add(item.Key.ToUpper(), item.Value);
            }
            param = paramUpper;
        }

        /// <summary>
        /// 根据类名和方法名得到方法对象
        /// </summary>
        /// <param name="backObj">返回出去的类实例</param>
        /// <param name="className">类名</param>
        /// <param name="method">方法名</param>
        /// <returns></returns>
        private MethodInfo GetActionMethod(out object backObj, string className, string method)
        {
            Assembly assembly = Assembly.Load("XMBOXING.BLL");
            Type type = assembly.GetType(gstrClassPath + "." + className);
            backObj = assembly.CreateInstance(gstrClassPath + "." + className, false);
            MethodInfo methodEx = type.GetMethod(method);
            return methodEx;
        }

        /// <summary>
        /// 从数据库中拿数据或从Redis拿数据
        /// </summary>
        /// <param name="method">方法对象</param>
        /// <param name="param">方法需要的参数</param>
        /// <param name="obj">类实例对象</param>
        /// <returns></returns>
        private object takeData(MethodInfo method, Dictionary<string, object> param, object obj)
        {

            if (method == null)
            {
                return null;
            }

            Dictionary<string, object> paramMethod = new Dictionary<string, object>();
            if (param != null)
            {
                keyToUpper(ref param);
                //如果参数为实体的话就把参数封装为实体类
                ParameterInfo[] parameterInfo = method.GetParameters();
                entityMapping(ref paramMethod, ParameterInfoArrayToDic(parameterInfo), ref param);

                if (param == null)
                {
                    param = new Dictionary<string, object>();
                }
            }
        
            return method.Invoke(obj, paramMethod.Values.ToArray());

        }

        /// <summary>
        /// 把要调用的方法的参数数组的变成字典
        /// </summary>
        /// <param name="parameterInfo">参数数组</param>
        /// <returns></returns>
        private Dictionary<string, Type> ParameterInfoArrayToDic(ParameterInfo[] parameterInfo)
        {
            Dictionary<string, Type> pairs = new Dictionary<string, Type>();
            foreach (var item in parameterInfo)
            {
                pairs.Add(item.Name, item.ParameterType);
            }
            return pairs;
        }

        /// <summary>
        /// 把实体类的属性数组变成字典
        /// </summary>
        /// <param name="parameterInfo">属性数组</param>
        /// <returns></returns>
        private Dictionary<string, Type> ParameterInfoArrayToDic(PropertyInfo[] parameterInfo)
        {
            Dictionary<string, Type> pairs = new Dictionary<string, Type>();
            foreach (var item in parameterInfo)
            {
                pairs.Add(item.Name, item.PropertyType);
            }
            return pairs;
        }
    }
}
