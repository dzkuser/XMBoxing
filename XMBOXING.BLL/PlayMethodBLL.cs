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
    /// 功能：玩法信息逻辑处理层
    /// </summary>
    public class PlayMethodBLL
    {

        /// 玩法数据访问对象
        /// </summary>
        private IPlayMethodDAL mobjPlayMethodDAL = new PlayMethodDAL();



        /// <summary>
        /// 添加一条玩法记录
        /// </summary>
        /// <param name="aAddPlayMethod">玩法实体信息</param>
        /// <returns></returns>
        public bool InsertPlayMethod(PlayMethodEntity aAddPlayMethod)
        {
            return mobjPlayMethodDAL.Insert(aAddPlayMethod);
        }


        /// <summary>
        /// 修改玩法
        /// </summary>
        /// <param name="aEditPlayMethod">玩法实体信息</param>
        /// <returns></returns>
        public bool UpdatePlayMethod(PlayMethodEntity aEditPlayMethod)
        {
            return mobjPlayMethodDAL.Update(aEditPlayMethod);
        }

        /// <summary>
        /// 删除玩法
        /// </summary>
        /// <param name="aintId">编号</param>
        /// <returns></returns>
        public bool DeleteExType(int aintId)
        {
            return mobjPlayMethodDAL.Delete(aintId);
        }

        /// <summary>
        /// 根据编号查询
        /// </summary>
        /// <param name="aintId">编号</param>
        /// <returns></returns>
        public PlayMethodEntity GetExTypeByID(int aintId)
        {
            return mobjPlayMethodDAL.GetEntityByID(aintId);
        }

        /// <summary>
        /// 查询所有记录
        /// </summary>
        /// <returns></returns>
        public IQueryable<PlayMethodEntity> GetAll()
        {
            return mobjPlayMethodDAL.GetAll();
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="aobjPlayMethiod">玩法类型集合</param>
        /// <returns></returns>
        public bool InserMore(List<PlayMethodEntity> aobjPlayMethiod) {

            return mobjPlayMethodDAL.InsertMore(aobjPlayMethiod);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="aobjIDs">编号集合</param>
        /// <returns></returns>
        public bool DeleteMore(List<int> aobjIDs) {
            return mobjPlayMethodDAL.DeleteMore(aobjIDs);
        }
    }
}
