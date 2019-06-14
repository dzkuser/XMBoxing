using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMBOXING.DAL
{
    public class GridData
    {
        public string SortField { get; set; }
        public string SortDirection { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int RecordCount { get; set; }
    }

    public class WhereBuilder
    {
        private DynamicParameters _parameters = new DynamicParameters();

        public DynamicParameters Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }

        private List<string> _wheres = new List<string>();

        public StringBuilder mobjSql = new StringBuilder();

        public Dictionary<string, object> mobjSqlParam = new Dictionary<string, object>();
       

        public List<string> Wheres
        {
            get { return _wheres; }
            set { _wheres = value; }
        }

        public string GetSql() {
            string strSql = String.Format("select * from {0} where 1=1",FromSql);
            return strSql + mobjSql.ToString();

        }
       

        private string _fromSql = String.Empty;

        public string FromSql
        {
            get { return _fromSql; }
            set { _fromSql = value; }
        }

        public void AddWhere(string item)
        {
            _wheres.Add(item);
        }

        public void AddParameter(string name, object value)
        {
            
            _parameters.Add(name, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paras">当前参数集</param>
        /// <param name="key">paras[key]中的key</param>  
        /// <param name="dbField">DB字段</param>
        /// <param name="operatorTag">运算符(= > < >= like ……)</param>
        /// <param name="obj"></param>
        /// <param name="addParameter"></param>
        public void AddWhereAndParameter(Dictionary<string, object> paras, string key, string dbField = "", string operatorTag = "=", string obj = "", string connect="", bool addParameter = true)
        {
            if (paras.ContainsKey(key) && paras[key] != null)
            {
                if (paras[key] == null) {
                    return;
                }
                if (string.IsNullOrEmpty(dbField))
                {
                    dbField = key;
                }
                if (string.IsNullOrEmpty(obj))
                {
                    obj = key;
                }
                 string strParamName= obj.Equals(key) ? "@" + obj : obj;
                string strWhere= string.Format(" {3} {0} {1} {2}", dbField, operatorTag, strParamName, connect);
                AddWhere(strWhere);
                mobjSql.Append(strWhere);
                if (addParameter)
                {
                 //   mobjSqlParam.Add("@"+dbField,paras[key]);
                    AddParameter(key, paras[key]);
                }
            }
        }
    }
}
