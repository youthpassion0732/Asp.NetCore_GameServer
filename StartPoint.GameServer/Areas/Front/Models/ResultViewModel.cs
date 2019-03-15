using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartPoint.GameServer.Areas.Front.Models
{
    public class ResultViewModel
    {
        public int TotalScore { get; set; }

        public int ObtainedScore { get; set; }

        public bool IsPass { get; set; }
    }
}
