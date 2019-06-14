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
    /// 功能：赛事设定数据访问接口
    /// </summary>
    public interface ICompetitionDAL:IBaseDAL<CompetitionEntity>
    {
        /// <summary>
        /// 根据条件查询赛事信息
        /// </summary>
        /// <param name="adicParam">条件</param>
        /// <returns></returns>
        IQueryable<CompetitionEntity> GetCompetitionByWhere(Dictionary<string, object> adicParam);

        /// <summary>
        /// 查询要进行图片轮播的赛事
        /// </summary>
        /// <returns></returns>
        IQueryable<CompetitionEntity> GetCompetitionByCarousel();

        /// <summary>
        /// 查询要在首页展示的赛事
        /// </summary>
        /// <returns></returns>
        IQueryable<CompetitionEntity> GetCompetitionByIndex();

        /// <summary>
        /// 查询在日期内的赛事
        /// </summary>
        /// <param name="aDate">时间</param>
        /// <returns></returns>
        IQueryable<CompetitionEntity> GetComprtitionByDate(DateTime aDate);

        /// <summary>
        /// 得到赛事结果
        /// </summary>
        /// <param name="adicParam">条件参数</param>
        /// <returns></returns>
        IQueryable<CompetitionDTO> GetComprtitionResult(Dictionary<string, object> adicParam);


        /// <summary>
        /// 得到赛事结果详细
        /// </summary>
        /// <param name="adicParam">条件参数</param>
        /// <returns></returns>
        IQueryable<CompetitionDTO> GetComportitionResultInfo(Dictionary<string, object> adicParam);


        /// <summary>
        /// 查询未开始
        /// </summary>
        /// <returns></returns>
        IQueryable<CompetitionEntity> GetComprtitionByNotStart();
    }
}
