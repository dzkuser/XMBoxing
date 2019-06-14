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
    /// 创建时间:2019-5-29
    /// 修改时间：
    /// 功能：用户投注信息
    /// </summary>
    public interface IBetDAL:IBaseDAL<BetEntity>
    {

        /// <summary>
        /// 查询当前赛事的所有玩法
        /// </summary>
        /// <param name="aintComprtitionID">赛事编号ID</param>
        /// <returns></returns>
        IQueryable<BetEntity> GetBetGroup(int aintComprtitionID);

        /// <summary>
        /// 根据用户查询该用户的投注信息
        /// </summary>
        /// <param name="astrAccountName">用户账号</param>
        /// <returns></returns>
         IQueryable<BetEntity> GetBetByAccountName(string astrAccountName);

        /// <summary>
        /// 修改赢的积分
        /// </summary>
        /// <param name="aintMethodID">玩法编号</param>
        /// <param name="adobIntegral">积分</param>
        /// <returns></returns>
        bool UpdateWinIntegral(int aintMethodID, double adobIntegral,int aintBetID);

    }
}
