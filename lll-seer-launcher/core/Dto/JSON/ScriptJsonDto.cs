using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lll_seer_launcher.core.Dto.JSON
{
    public class ScriptJsonDto
    {
        public string title { get; set; }
        public string description { get; set; }
        public List<ScriptDto> scripts { get; set; } = new List<ScriptDto>();
        public string pwd { get; set; }
        public string securityCode { get; set; }
        public int type { get; set; }
    }
    public class ScriptDto
    {
        public int CmdId { get; set; }
        public string Body { get; set; }
        public int times { get; set; }
    }
}
