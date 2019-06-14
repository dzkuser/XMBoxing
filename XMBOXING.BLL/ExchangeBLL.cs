using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMBOXING.DAL;
using XMBOXING.IDAL;
using XMBOXING.MODEL;

namespace XMBOXING.BLL
{

    /// <summary>
    /// 作者：邓镇康
    /// 创建时间:2019-5-28
    /// 修改时间：
    /// 功能：积分兑换逻辑处理层
    /// </summary>
    public class ExchangeBLL
    {

        // <summary>
        /// 积分兑换数据访问对象
        /// </summary>
        private IExchangeDAL mobjExchangeDAL = new ExchangeDAL();

        /// <summary>
        /// 添加一条积分兑换记录
        /// </summary>
        /// <param name="aAddExchange">积分兑换实体记录</param>
        /// <returns></returns>
        public bool InsertExchange(ExchangeEntity aAddExchange)
        {

            return mobjExchangeDAL.Insert(aAddExchange);
        }


        /// <summary>
        /// 修改积分兑换
        /// </summary>
        /// <param name="aEditExchange">积分兑换实体记录</param>
        /// <returns></returns>
        public bool UpdateExchange(ExchangeEntity aEditExchange)
        {

            return mobjExchangeDAL.Update(aEditExchange);
        }

        /// <summary>
        /// 删除一条积分兑换
        /// </summary>
        /// <param name="aintId">编号</param>
        /// <returns></returns>
        public bool DeleteExchange(int aintId)
        {
            return mobjExchangeDAL.Delete(aintId);
        }

        /// <summary>
        /// 根据编号查询
        /// </summary>
        /// <param name="aintId">编号</param>
        /// <returns></returns>
        public ExchangeEntity GetExchangeByID(int aintId)
        {
            return mobjExchangeDAL.GetEntityByID(aintId);
        }

        /// <summary>
        /// 查询所有记录
        /// </summary>
        /// <returns></returns>
        public IQueryable<ExchangeEntity> GetAll()
        {
            return mobjExchangeDAL.GetAll();
        }


        /// <summary>
        /// 获得积分兑换记录的分组数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<ExchangeDTO> GetExchangeGroup(int? aintOutType,int? aintInType,DateTime? aobjStartDate,DateTime? aobjEndTime)
        {
            Dictionary<string, object> objParam = new Dictionary<string, object>();
            objParam.Add("OutType",aintOutType);
            objParam.Add("InType",aintInType);
            objParam.Add("StartDate",aobjStartDate);
            objParam.Add("EndDate",aobjEndTime);
            IQueryable<ExchangeDTO> objExchanges=mobjExchangeDAL.GetExchangeGroup(objParam);
            foreach (var item in objExchanges)
            {
                item.StartDate = aobjStartDate.Value;
                item.EndDate = aobjStartDate.Value;
            }
            return objExchanges;
        }

        /// <summary>
        /// 获得所有积分兑换数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<ExchangeDTO> GetExchangesRecord()
        {
            return mobjExchangeDAL.GetExchangesRecord();
        }

        /// <summary>
        /// 根据转出类型查询积分兑换记录
        /// </summary>
        /// <returns></returns>
        public IQueryable<ExchangeDTO> GetExchangesRecordByOuType(int intOutType)
        {
            return mobjExchangeDAL.GetExchangesRecordByOuType(intOutType);
        }

    }
}
