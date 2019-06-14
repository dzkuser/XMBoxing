using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XMBOXING.IDAL;
using XMBOXING.MODEL;

namespace XMBOXING.DAL
{
    public class BaseDAL<T> : IBaseDAL<T> where T : class, new()
    {
        private string mstrTableName;

        private string mstrTableKey;

        public void ToKey(string astrTableKey) {
            mstrTableKey = astrTableKey;
        }
        public void ToTable(string astrTableName) {
            mstrTableName = astrTableName;
        }

        private static int CommandTimeout
        {
            get
            {
                int timeoutSecond = 0;

                if (int.TryParse(ConfigurationManager.AppSettings["CommandTimeout"], out timeoutSecond))
                {
                    return timeoutSecond;
                }

                return 30;
            }
        }

        protected string ConnString { get; set; } = ConfigurationManager.ConnectionStrings["MySqlStr"].ConnectionString;

       



        protected IEnumerable<dynamic> Query(string sql, object param = null)
        {
            using (IDbConnection conn = GetConnection())
            {
                return conn.Query(sql, param, null);
            }
        }

   
        /// <summary>
        /// 得到新增的sql语句和参数
        /// </summary>
        /// <param name="aobjEntity">新增的实体信息</param>
        /// <param name="sql">sql 语句 由该方法返回</param>
        /// <returns></returns>
        private Dictionary<string, object> GetInsertSqlAndParam(T aobjEntity,ref string sql) {
            Dictionary<string, object> aobjParam = new Dictionary<string, object>();
            StringBuilder objParam = new StringBuilder();
            StringBuilder objSql = new StringBuilder();
          //  objSql.AppendFormat("insert into {0} values ",mstrTableName);          
            objSql.Append("(");
            PropertyInfo[] objEntityPro=GetPropertyInfos();
            foreach (var item in objEntityPro)
            {
                object objValue=item.GetValue(aobjEntity);        
                if (!item.Name.Equals(mstrTableKey)) {                
                    objSql.AppendFormat("@{0} ", item.Name).Append(",");
                    aobjParam.Add("@" + item.Name, objValue);
                }

                   
            }
            objSql.Remove(objSql.Length-1,1);
            objSql.Append(")");
            sql = objSql.ToString();
            string strParam = sql.Replace("@","");
             sql=objParam.AppendFormat("INSERT INTO {0} {1} VALUES {2}",mstrTableName,strParam,sql).ToString();
            return  aobjParam;
        }

        /// <summary>
        /// 得到修改的Sql语句
        /// </summary>
        /// <param name="aobjEntity"></param>
        /// <param name="astrSql"></param>
        /// <returns></returns>
        private Dictionary<string,object> GetUpdateSqlAndParam(T aobjEntity,ref string astrSql) {
            Dictionary<string, object> aobjParam = new Dictionary<string, object>();
            StringBuilder objSql = new StringBuilder();
            objSql.AppendFormat("update {0} Set ",mstrTableName);
            PropertyInfo [] arrPropertys=GetPropertyInfos();
            foreach (var item in arrPropertys)
            {
                if (item.Name.Equals(mstrTableKey)) {
                    continue;
                }
                object objValue = item.GetValue(aobjEntity);
                if (objValue != null) {
                    objSql.AppendFormat(" {0}= @{0}",item.Name).Append(",");
                    aobjParam.Add("@"+item.Name,objValue);
                }
            }
            objSql.Remove(objSql.Length - 1, 1);
            int intTableKeyValue = (int)arrPropertys.Where(t=>t.Name.Equals(mstrTableKey)).FirstOrDefault().GetValue(aobjEntity);
            objSql.AppendFormat(" WHERE {0}= {1}",mstrTableKey,intTableKeyValue);
            astrSql = objSql.ToString();
            return aobjParam;
        }

        /// <summary>
        /// 获得实体类属性
        /// </summary>
        /// <returns></returns>
        private PropertyInfo[] GetPropertyInfos() {
            Type objEntityType = typeof(T);
          return objEntityType.GetProperties().Where(t=>t.GetCustomAttribute(typeof(NotEntityFiled))==null).ToArray();         
        }

        #region 公用方法
        protected IDbConnection GetConnection()
        {
            return new SqlConnection(ConnString);
        }

        protected T QuerySingle(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            using (IDbConnection conn = GetConnection())
            {
                return conn.QuerySingleOrDefault<T>(sql, param, null, CommandTimeout, commandType);
            }
        }

        protected T QuerySingle<T>(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            using (IDbConnection conn = GetConnection())
            {
                return conn.QuerySingleOrDefault<T>(sql, param, null, CommandTimeout, commandType);
            }
        }

        protected IEnumerable<T> QueryList(string sql, object param = null, CommandType commandType = CommandType.Text, bool buff = true)
        {
            using (IDbConnection conn = GetConnection())
            {
                return conn.Query<T>(sql, param, null, buff, CommandTimeout, commandType);
            }
        }

        protected IEnumerable<T> QueryList<T>(string sql, object param = null, CommandType commandType = CommandType.Text, bool buff = true)
        {
            using (IDbConnection conn = GetConnection())
            {
                return conn.Query<T>(sql, param, null, buff, CommandTimeout, commandType);
            }
        }



        protected int Execute(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            using (IDbConnection conn = GetConnection())
            {
                return conn.Execute(sql, param, null, CommandTimeout, commandType);
            }
        }

        #endregion
 

        public bool Insert(T entity)
        {
            string strSql = "";
           Dictionary<string,object> objSqlParam= GetInsertSqlAndParam(entity, ref strSql);
            return Execute(strSql,objSqlParam)>0?true:false;
        }

        public bool Update(T entity)
        {
            string strSql = "";
            Dictionary<string, object> objSqlParam = GetUpdateSqlAndParam(entity, ref strSql);
            return Execute(strSql,objSqlParam)>0?true:false;
        }

        public bool Delete(int id)
        {
            Dictionary<string, object> objParam = new Dictionary<string, object>();
            objParam.Add("@ID",id);
            string strSql = String.Format("delete {0} where ID=@ID",mstrTableName);
            return Execute(strSql,objParam)>0?true:false;
        }

        public IQueryable<T> GetAll()
        {
            string strSql =String.Format("select * from {0} ",mstrTableName);
            return QueryList(strSql).AsQueryable();
        }

        public T GetEntityByID(int id)
        {
            Dictionary<string, object> objParam = new Dictionary<string, object>();
            objParam.Add("@ID", id);
            string strSql = String.Format("select * from {0} where ID=@ID", mstrTableName);
            return QuerySingle(strSql, objParam);
        }

        public bool InsertMore(List<T> aEntitys)
        {
            string strSql = "";
            GetInsertSqlAndParam(new T(),ref strSql);
            return Execute(strSql,aEntitys)>0?true:false;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="aobjIDs">编号集合</param>
        /// <returns></returns>
        public bool DeleteMore(List<int> aobjIDs) {
            string strSql =String.Format("delete {0} where {1} in @IDs",mstrTableName,mstrTableKey);
            return Execute(strSql,new { IDs=aobjIDs})>0?true:false;
        }
    }
}
