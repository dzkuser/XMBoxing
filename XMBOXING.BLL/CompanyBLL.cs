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
    /// 创建时间:2019-5-29
    /// 修改时间：2019-
    /// 功能：公司信息逻辑处理类
    /// </summary>
    public class CompanyBLL
    {

        /// <summary>
        /// 公司信息数据访问对象
        /// </summary>
        private ICompanyDAL mobjCompanyDAL = new CompanyDAL();

        /// <summary>
        /// 得到所有公司信息
        /// </summary>
        /// <returns></returns>
        public IQueryable<CompanyEntity> GetAll() {
            return mobjCompanyDAL.GetAll();
        }

        /// <summary>
        /// 根据编号查询
        /// </summary>
        /// <param name="aintId">编号</param>
        /// <returns></returns>
        public CompanyEntity GetCompanyByID(int aintId) {
            return mobjCompanyDAL.GetEntityByID(aintId);
        }

        /// <summary>
        /// 增加一条公司信息记录
        /// </summary>
        /// <param name="aobjCompany">公司信息实体</param>
        /// <returns></returns>
        public bool InsertCompany(CompanyEntity aobjCompany) {

            return mobjCompanyDAL.Insert(aobjCompany);
        }

    }
}
