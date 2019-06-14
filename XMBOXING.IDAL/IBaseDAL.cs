using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMBOXING.IDAL
{

    /// <summary>
    /// 作者：邓镇康
    /// 创建时间:2019-5-30
    /// 修改时间：
    /// 功能：CRUD 接口
    /// </summary>
    public interface IBaseDAL<T>
    {
        /// <summary>
        /// 增加一条记录
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        bool Insert(T entity);

        /// <summary>
        /// 修改一条记录
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        bool Update(T entity);

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        bool Delete(int id);

        /// <summary>
        /// 得到所有记录
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// 根据ID 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetEntityByID(int id);

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="aEntitys">实体类集合</param>
        /// <returns></returns>
        bool InsertMore(List<T> aEntitys);



        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="aobjIDs">编号集合</param>
        /// <returns></returns>
        bool DeleteMore(List<int> aobjIDs);
    }
}
