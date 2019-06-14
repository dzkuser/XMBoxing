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
    /// 功能：参赛人员信息逻辑处理类
    /// </summary>
    public class ParticipantBLL
    {


        /// 参赛人员数据访问对象
        /// </summary>
        private IParticipantDAL mobjParticipantDAL = new ParticipantDAL();

        /// <summary>
        /// 添加一条参赛人员信息记录
        /// </summary>
        /// <param name="aAddParticipant">参赛人员实体</param>
        /// <returns></returns>
        public bool InsertParticipant(ParticipantEntity aAddParticipant)
        {
            return mobjParticipantDAL.Insert(aAddParticipant);
        }


        /// <summary>
        /// 修改参赛人员信息
        /// </summary>
        /// <param name="aEditParticipant">参赛人员实体</param>
        /// <returns></returns>
        public bool UpdateUser(ParticipantEntity aEditParticipant)
        {

            return mobjParticipantDAL.Update(aEditParticipant);
        }

        /// <summary>
        /// 删除参赛人员信息
        /// </summary>
        /// <param name="aintId">编号</param>
        /// <returns></returns>
        public bool DeleteUser(int aintId)
        {
            return mobjParticipantDAL.Delete(aintId);
        }

        /// <summary>
        /// 根据编号查询
        /// </summary>
        /// <param name="aintId">编号</param>
        /// <returns></returns>
        public ParticipantEntity GetExTypeByID(int aintId)
        {
            return mobjParticipantDAL.GetEntityByID(aintId);
        }

        /// <summary>
        /// 查询所有记录
        /// </summary>
        /// <returns></returns>
        public IQueryable<ParticipantEntity> GetAll()
        {
            return mobjParticipantDAL.GetAll();
        }

    }
}
