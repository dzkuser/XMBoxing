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
    /// 功能：赛事玩法关联逻辑处理类
    /// </summary>
    public class GameMethodBLL
    {

        /// <summary>
        /// 赛事玩法关联数据访问对象
        /// </summary>
        private IGameMethodDAL mobjGameMethod = new GameMethodDAL();

        /// <summary>
        /// 投注逻辑处理类
        /// </summary>
        private IBetDAL mobjBetDAL = new BetDAL();

        /// <summary>
        /// 添加一条赛事玩法关联记录
        /// </summary>
        /// <param name="aobjGameMethod">赛事玩法关联实体</param>
        /// <returns></returns>
        public bool InsertGameMethod(GameMethodEntity aobjGameMethod) {

            return mobjGameMethod.Insert(aobjGameMethod);
        }

        /// <summary>
        /// 修改赛事玩法关联记录
        /// </summary>
        /// <param name="aobjEditGameMethod">赛事玩法关联实体</param>
        /// <returns></returns>
        public bool UpdateGameMethod(GameMethodEntity aobjEditGameMethod) {
            return mobjGameMethod.Update(aobjEditGameMethod);
        }

        /// <summary>
        /// 根据编号删除一条记录
        /// </summary>
        /// <param name="aintId">编号</param>
        /// <returns></returns>
        public bool DeleteGameMethod(int aintId) {
            return mobjGameMethod.Delete(aintId);
        }


        /// <summary>
        /// 根据编号查询
        /// </summary>
        /// <param name="aintId">编号</param>
        /// <returns></returns>
        public GameMethodEntity GetExTypeByID(int aintId)
        {
            return mobjGameMethod.GetEntityByID(aintId);
        }

        /// <summary>
        /// 查询所有记录
        /// </summary>
        /// <returns></returns>
        public IQueryable<GameMethodEntity> GetAll()
        {
            return mobjGameMethod.GetAll();
        }

        /// <summary>
        /// 得到赛事玩法设定
        /// </summary>
        /// <returns></returns>
        public IQueryable<GameMethodDTO> GetGameMethodData() {
            return mobjGameMethod.GetGameMethodData();
        }

        /// <summary>
        /// 查询该赛事的玩法
        /// </summary>
        /// <param name="aintComprtitionID">赛事ID</param>
        /// <returns></returns>
        public IQueryable<GameMethodDTO> GetPlayMethodByComprtition(int aintComprtitionID)
        {
            return mobjGameMethod.GetPlayMethodByComprtition(aintComprtitionID);
        }


        /// <summary>
        /// 查询该赛事的玩法详细
        /// </summary>
        /// <returns></returns>
        public object GetCompetitionMethod(int aintComprtitionID,string astrAccountName) {
             IQueryable<GameMethodDTO> objPlayMethods = GetPlayMethodByComprtition(aintComprtitionID);
             IQueryable<BetEntity> objBetGroup= mobjBetDAL.GetBetGroup(aintComprtitionID);
             IQueryable<BetEntity> objUserBet = mobjBetDAL.GetBetByAccountName(astrAccountName);
            return AssembleMethodData(objPlayMethods,objBetGroup,objUserBet);
        }

        /// <summary>
        /// 在结算时或得改赛事的玩法
        /// </summary>
        /// <param name="aintComprtitionID">赛事id</param>
        /// <returns></returns>
        public object GetGameMethodByResult(int aintComprtitionID)
        {
            return AssembleGameMethodResult(mobjGameMethod.GetGameMethodByResult(aintComprtitionID));
        }



        /// <summary>
        /// 修改赢的玩法
        /// </summary>
        /// <param name="aintMethodID">玩法ID集合</param>
        /// <returns></returns>
        public bool UpdateWinMethod(List<int> aintMethodID) {

            return mobjGameMethod.UpdateWinMethod(aintMethodID);

        }


        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="aobjResult">前台传来参数</param>
        /// <returns></returns>
        public bool UpdateWinner(Dictionary<string, List<GameMethodDTO>> aobjResult,int aintBetID) {
            return PickMethodWinIntegral(aobjResult,aintBetID);
        }


        /// <summary>
        /// 删除多条游戏设定记录
        /// </summary>
        /// <param name="aobjGameMethodIDs">游戏设定ID集合</param>
        /// <returns></returns>
        public bool DeleteGameMethodMore(List<int> aobjGameMethodIDs) {
            return mobjGameMethod.DeleteGameMethodMore(aobjGameMethodIDs);
        }


        /// <summary>
        /// 添加多条赛事玩法记录
        /// </summary>
        /// <param name="aintCompetitionID">赛事ID</param>
        /// <param name="aintCompanyID">公司ID</param>
        /// <param name="aobjPlayMethodIDs">玩法集合</param>
        /// <returns></returns>
        public bool InsertGameMethodMore(int aintCompetitionID, int aintCompanyID, List<int> aobjPlayMethodIDs) {
            List<GameMethodEntity> objGameMethods = new List<GameMethodEntity>();
            foreach (var item in aobjPlayMethodIDs)
            {
                GameMethodEntity objGameMethod = new GameMethodEntity();
                objGameMethod.CompetitionID = aintCompetitionID;
                objGameMethod.CompanyID = aintCompanyID;
                objGameMethod.PlayMethodID = item;
                objGameMethods.Add(objGameMethod);
            }
            return mobjGameMethod.InsertMore(objGameMethods);


        }



        /// <summary>
        /// 挑选赢了的玩法和算出赢了多少
        /// </summary>
        /// <param name="aobjResult"></param>
        /// <returns></returns>
        private bool  PickMethodWinIntegral(Dictionary<string, List<GameMethodDTO>> aobjResult,int aintBetID) {
            List<int> objMethodIDs = new List<int>();
            Dictionary<int, double> objWinIntegral = new Dictionary<int, double>();
            foreach (var item in aobjResult)
            {
                List<GameMethodDTO> objLoster= item.Value.Where(t=>t.IsWin==0).ToList();
                int sum = 0;
                foreach (var value in objLoster)
                {
                    sum += value.TotalIntegral;
                
                    objWinIntegral.Add(value.MethodID, 0);
                }
                GameMethodDTO objWin = item.Value.Where(t=>t.IsWin==1).FirstOrDefault();
                  if (objWin!=null) {
                     double dobIntegral = 0;
                     objMethodIDs.Add(objWin.MethodID);
                    if (sum > 0 && objWin.TotalIntegral > 0) {
                        dobIntegral = sum / (objWin.TotalIntegral * 1.0);
                    }                
                    objWinIntegral.Add(objWin.MethodID,dobIntegral);
                }
            }

            return ExecueUpdateMethodWin(objMethodIDs,objWinIntegral,aintBetID);

        }

        /// <summary>
        /// 执行结算修改积分
        /// </summary>
        /// <param name="aobjMethodIDs">玩法ID集合</param>
        /// <param name="aobjWinIntegral">每个用户得到的积分</param>
        /// <returns></returns>
        private bool ExecueUpdateMethodWin(List<int> aobjMethodIDs, Dictionary<int, double> aobjWinIntegral,int aintBetID) {
            bool isSuccess = UpdateWinMethod(aobjMethodIDs);
            if (isSuccess) {
                foreach (var item in aobjWinIntegral)
                {
                    mobjBetDAL.UpdateWinIntegral(item.Key,item.Value,aintBetID);
                }
            }

            return isSuccess;
        }

        /// <summary>
        /// 装配结算时要放回的类型数据
        /// </summary>
        /// <param name="objGameMethodData"></param>
        /// <returns></returns>
        private object AssembleGameMethodResult(IQueryable<GameMethodDTO> objGameMethodData) {
            List<string> objType = new List<string>();
            Dictionary<string,List<GameMethodDTO>> objResult = new Dictionary<string, List<GameMethodDTO>>();
            foreach (var item in objGameMethodData)
            {
                if (!objType.Contains(item.TypeName)) {
                    objType.Add(item.TypeName);
                    objResult.Add(item.TypeName,objGameMethodData.Where(t=>t.TypeName.Equals(item.TypeName)).ToList());
                }
            }

            return objResult;

        }

        /// <summary>
        /// 装配数据返回前台
        /// </summary>
        /// <param name="aobjPlayMethods"></param>
        /// <param name="aobjBetGroup"></param>
        /// <param name="aobjUserBet"></param>
        /// <returns></returns>
        private object AssembleMethodData(IQueryable<GameMethodDTO> aobjPlayMethods, IQueryable<BetEntity> aobjBetGroup, IQueryable<BetEntity> aobjUserBet) {    
            List<string> objType = new List<string>();
            List<object> objResult = new List<object>();       
            foreach (var item in aobjPlayMethods)
            {
                if (!objType.Contains(item.TypeName)) {
                    objType.Add(item.TypeName);
                   IQueryable<GameMethodDTO> objTypeMethod= aobjPlayMethods.Where(t => t.TypeName.Equals(item.TypeName));
                   List<int> objKeys= objTypeMethod.ToDictionary(t=>t.MethodID).Keys.ToList();
                    var obj = new
                    {
                        TypeName = item.TypeName,
                        GameMethod = objTypeMethod,
                        MethodCount = aobjBetGroup.Where(t => objKeys.Contains(t.MethodID)),
                        User = aobjUserBet.Where(t=>objKeys.Contains(t.MethodID))
                    };
                    objResult.Add(obj);
                }
            }

            return objResult;
                
        }

    }
}
