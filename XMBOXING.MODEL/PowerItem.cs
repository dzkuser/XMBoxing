using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMBOXING.MODEL
{

    /// <summary>
    /// 作者：邓镇康
    /// 创建时间:2019-6-5
    /// 修改时间：
    /// 功能：菜单选项
    /// </summary>
    public class PowerItem
    {

        public int id { get; set; }

        public string text { get; set; }

        public string level { get; set; }

        public List<string> role { get; set; }

        public List<string> gametype { get; set; }

        public string sort { get; set; }

        public string path { get; set; }

        public List<PowerItem> action { get; set; }

        public int parentID { get; set; }

    }
}
