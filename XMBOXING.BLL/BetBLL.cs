
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMBOXING.Comm;
using XMBOXING.DAL;
using XMBOXING.IDAL;
using XMBOXING.MODEL;

namespace XMBOXING.BLL
{

    /// <summary>
    /// 作者：邓镇康
    /// 创建时间:2019-5-29
    /// 修改时间：
    /// 功能：用户投注信息逻辑处理类
    /// </summary>
    public class BetBLL
    {
        /// <summary>
        /// 投注数据访问对象
        /// </summary>
        private IBetDAL mobjBetDAL = new BetDAL();

        /// <summary>
        /// 用户数据访问对象
        /// </summary>
        private IUserDAL mobjUserDAL = new UserDAL();

        /// <summary>
        /// 新增一条用户投注记录
        /// </summary>
        /// <param name="aAddBet"></param>
        /// <returns></returns>
        public bool InsertBet(BetEntity aAddBet)
        {
             bool isSuccess=mobjBetDAL.Insert(aAddBet);
            if (isSuccess) {
                mobjUserDAL.UpdateIntegral(aAddBet.AccountName,aAddBet.Integral);
                SuperSocketHandler objSocket = new SuperSocketHandler();
                objSocket.SendBetData(aAddBet.CompetitionID.ToString(),aAddBet);
            }
            return isSuccess;
        }

        /// <summary>
        /// 查询当前赛事的所有玩法
        /// </summary>
        /// <param name="aintComprtitionID">赛事编号ID</param>
        /// <returns></returns>
        public IQueryable<BetEntity> GetBetGroup(int aintComprtitionID)
        {
            return mobjBetDAL.GetBetGroup(aintComprtitionID);
        }

        /// <summary>
        /// 删除投注订单
        /// </summary>
        /// <param name="aintBetID">投注编号</param>
        /// <param name="astrAccountName">用户账号</param>
        /// <returns></returns>
        public bool DeleteBet(int aintBetID,string astrAccountName) {

            BetEntity objBetDelete=mobjBetDAL.GetEntityByID(aintBetID);
            decimal decIntegral=objBetDelete.WinIntegral;
            if (decIntegral > 0) {
                mobjUserDAL.UpdateIntegral(astrAccountName,decIntegral);
            }

            return mobjBetDAL.Delete(aintBetID);

        }


        
    }
}
