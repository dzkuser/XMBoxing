using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMBOXING.MODEL;

namespace XMBOXING.IDAL
{

    /// <summary>
    /// 作者：邓镇康
    /// 创建时间:2019-5-28
    /// 修改时间：
    /// 功能：积分兑换数据访问接口
    /// </summary>
    public interface IExchangeDAL:IBaseDAL<ExchangeEntity>
    {

        /// <summary>
        /// 获得积分兑换记录的分组数据
        /// </summary>
        /// <returns></returns>
        IQueryable<ExchangeDTO> GetExchangeGroup(Dictionary<string, object> aobjParam);


        /// <summary>
        /// 获得所有积分兑换数据
        /// </summary>
        /// <returns></returns>
        IQueryable<ExchangeDTO> GetExchangesRecord();


        /// <summary>
        /// 根据转出类型查询积分兑换记录
        /// </summary>
        /// <returns></returns>
        IQueryable<ExchangeDTO> GetExchangesRecordByOuType(int intOutType);

       
    }
}
