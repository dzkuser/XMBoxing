using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMBOXING.MODEL
{

    /// <summary>
    /// 作者：邓镇康
    /// 创建时间:2019-5-30
    /// 修改时间：
    /// 功能：Socket标识码的枚举
    /// </summary>
    public enum SocketEnum:int
    {
        /// <summary>
        /// 改变Socket链接Session
        /// </summary>
        c,
        /// <summary>
        /// 表示要执行方法
        /// </summary>
        ac,
        /// <summary>
        /// 表示要进入房间
        /// </summary>
        i,
        /// <summary>
        /// 退出房间
        /// </summary>
        q,
        /// <summary>
        /// 新信息
        /// </summary>
        n
        
    }
}
