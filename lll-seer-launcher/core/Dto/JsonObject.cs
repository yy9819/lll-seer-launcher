using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lll_seer_launcher.core.Dto
{
    public class AchieveTitleJsonObject
    {
        public string date { get; set; }
        public List<AchieveTitleInfo> data { get; set; }
    }
    public class AchieveTitleInfo
    {
        public int id { get; set; } = 0;
        public string title { get; set; } = string.Empty;
        public string desc8 { get; set; } = string.Empty;
        public string abtext { get; set; } = string.Empty;
    }

    public class SuitJsonObject
    {
        public string date { get; set; }
        public List<SuitInfo> data { get; set; }
    }

    public class SuitInfo
    {
        public int suitId { get; set; } = 0;
        public string name { get; set; } = string.Empty;
        public string desc { get; set; } = string.Empty;
        public int[] clothIdList { get; set; } = new int[5];
    }

    public class GlassesJsonObject
    {
        public string date { get; set; }
        public List<GlassesInfo> data { get; set; }
    }

    public class GlassesInfo
    {
        public int glassesId { get; set; } = 0;
        public string name { get; set; } = string.Empty;
        public string desc { get; set; } = string.Empty;
    }
}
