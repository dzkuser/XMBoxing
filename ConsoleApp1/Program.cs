using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMBOXING.BLL;
using XMBOXING.MODEL;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            CompetitionBLL competitionBLL = new CompetitionBLL();
            CompetitionEntity entity = new CompetitionEntity()
            {
                GameName = "DZK"
            };
            competitionBLL.InsertCompetition(entity);
        }
    }
}
