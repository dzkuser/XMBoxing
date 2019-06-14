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
    /// 修改时间：
    /// 功能：用户投注信息数据访问实现类
    /// </summary>
    public class BetDAL:BaseDAL<BetEntity>,IBetDAL
    {

        public BetDAL() {
            this.ToTable("tbBet");
            this.ToKey("ID");
        }

        /// <summary>
        /// 调用存储过程
        /// 查询当前赛事的所有玩法
        /// </summary>
        /// <param name="aintComprtitionID">赛事编号ID</param>
        /// <returns></returns>
        public IQueryable<BetEntity> GetBetGroup(int aintComprtitionID) {
            Dictionary<string, object> objParam = new Dictionary<string, object>();
            objParam.Add("@ComprtitionID",aintComprtitionID);
            return QueryList("P_Bet_GetBetGroup",objParam,System.Data.CommandType.StoredProcedure).AsQueryable();
        }

        /// <summary>
        /// 根据用户查询该用户的投注信息
        /// </summary>
        /// <param name="astrAccountName">用户账号</param>
        /// <returns></returns>
        public IQueryable<BetEntity> GetBetByAccountName(string astrAccountName) {
            string strSql = "select * from tbBet  where AccountName=@AccountName";
            Dictionary<string, object> objParam = new Dictionary<string, object>();
            objParam.Add("@AccountName",astrAccountName);
            return QueryList(strSql,objParam).AsQueryable();
        }


        /// <summary>
        /// 修改赢的积分
        /// </summary>
        /// <param name="aintMethodID">玩法编号</param>
        /// <param name="adobIntegral">积分</param>
        /// <returns></returns>
        public bool UpdateWinIntegral(int aintMethodID,double adobIntegral,int aintBetID) {
            Dictionary<string, object> objParam = new Dictionary<string, object>();
            string strSql = "update tbBet Set BetState=1,WinIntegral =WinIntegral+(Integral*@Integral) where MethodID=@Method";
            if (aintBetID != 0) {
                strSql += " and ID=@ID";
                objParam.Add("@ID",aintBetID);
            }
            objParam.Add("@Method",aintMethodID);
            objParam.Add("@Integral",adobIntegral);
            return Execute(strSql,objParam)>0?true:false;
        }
    }
}
