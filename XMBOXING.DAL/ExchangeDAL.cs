using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMBOXING.IDAL;
using XMBOXING.MODEL;

namespace XMBOXING.DAL
{

    /// <summary>
    /// 作者：邓镇康
    /// 创建时间:2019-5-28
    /// 修改时间：2019-
    /// 功能：积分兑换数据访问实现类
    /// </summary>
    public class ExchangeDAL:BaseDAL<ExchangeEntity>,IExchangeDAL
    {

        public ExchangeDAL() {
            this.ToTable("tbExchange");
            this.ToKey("ID");
        }

        /// <summary>
        /// 获得积分兑换记录的分组数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<ExchangeDTO> GetExchangeGroup(Dictionary<string,object> aobjParam) {

            WhereBuilder objWhere = new WhereBuilder();
            objWhere.AddWhereAndParameter(aobjParam,"OutType","OutType","==","@OutType","and");
            objWhere.AddWhereAndParameter(aobjParam, "InType", "InType", "==", "@InType", "and");
            objWhere.AddWhereAndParameter(aobjParam,"StartDate","CreateDate",">=","@StartDate","and");
            objWhere.AddWhereAndParameter(aobjParam, "EndDate", "CreateDate", "<=", "@EndDate", "and");
            string sql =String.Format("select a.InType,a.OutType,b.Name as OutName ,c.Name as InName,SUM(a.Integral) from tbExchange a join tbExType b on a.OutType=b.ID join tbExType c on a.InType=c.ID where 1=1 {0}  group by b.Name,c.Name,a.Intype,a.OutType",objWhere.mobjSql);
            return QueryList<ExchangeDTO>(sql,objWhere.Parameters).AsQueryable();
        }

        /// <summary>
        /// 获得所有积分兑换数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<ExchangeDTO> GetExchangesRecord() {
            string sql = "select a.CreateDate,a.AccountName,a.InType,a.OutType,b.Name as OutName ,c.Name as InName,a.Integral from tbExchange a join tbExType b on a.OutType=b.ID join tbExType c on a.InType=c.ID";
            return QueryList<ExchangeDTO>(sql).AsQueryable();
        }

        /// <summary>
        /// 根据转出类型查询积分兑换记录
        /// </summary>
        /// <returns></returns>
        public IQueryable<ExchangeDTO> GetExchangesRecordByOuType(int intOutType) {

            string strSql = "select a.InType,a.OutType,b.Name as OutName ,c.Name as InName,a.Integral from tbExchange a join tbExType b on a.OutType=b.ID join tbExType c on a.InType=c.ID where a.OutType=@OutType";
            Dictionary<string, object> objParam = new Dictionary<string, object>();
            objParam.Add("@OutType",intOutType);
            return QueryList<ExchangeDTO>(strSql,objParam).AsQueryable();
        }

    }
}
