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
    /// 创建时间:2019-5-29
    /// 修改时间：2019-
    /// 功能：公司信息数据访问实现类
    /// </summary>
    public class CompanyDAL:BaseDAL<CompanyEntity>,ICompanyDAL
    {

        public CompanyDAL() {
            this.ToTable("tbCompany");
            this.ToKey("ID");
        }

      

    }
}
