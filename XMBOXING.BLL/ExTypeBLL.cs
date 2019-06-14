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
    /// 功能：积分兑换类型逻辑处理类
    /// </summary>
    public class ExTypeBLL
    {
        
        /// 积分兑换数据访问对象
        /// </summary>
        private IExTypeDAL mobjExTypeDAL = new ExTypeDAL();

        /// <summary>
        /// 添加一条积分兑换类型记录
        /// </summary>
        /// <param name="aAddExType">积分兑换类型实体记录</param>
        /// <returns></returns>
        public bool InsertExchange(ExTypeEntity aAddExType)
        {
            return mobjExTypeDAL.Insert(aAddExType);
        }


        /// <summary>
        /// 修改积分兑换类型
        /// </summary>
        /// <param name="aEditExType">积分兑换类型实体记录</param>
        /// <returns></returns>
        public bool UpdateExchange(ExTypeEntity aEditExType)
        {

            return mobjExTypeDAL.Update(aEditExType);
        }

        /// <summary>
        /// 删除一条积分兑换类型
        /// </summary>
        /// <param name="aintId">编号</param>
        /// <returns></returns>
        public bool DeleteExType(int aintId)
        {
            return mobjExTypeDAL.Delete(aintId);
        }

        /// <summary>
        /// 根据编号查询
        /// </summary>
        /// <param name="aintId">编号</param>
        /// <returns></returns>
        public ExTypeEntity GetExTypeByID(int aintId)
        {
            return mobjExTypeDAL.GetEntityByID(aintId);
        }

        /// <summary>
        /// 查询所有记录
        /// </summary>
        /// <returns></returns>
        public IQueryable<ExTypeEntity> GetAll()
        {

            return mobjExTypeDAL.GetAll();

        }
    }
}
