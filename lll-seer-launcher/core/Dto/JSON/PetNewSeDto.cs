using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lll_seer_launcher.core.Dto.JSON
{
    public class PetNewSeDto
    {
        public NewSe newSe { get; set; }
    }
    public class NewSe
    {
        public List<NewSeIdx> newSeIdx { get; set; }
    }
    public class NewSeIdx
    {
        public int idx { get; set; }
        public int eid { get; set; }
        public string intro { get; set; } = "";
        public string desc { get; set; }
        public string args { get; set; }
        public int starLevel { get; set; } = -1;
    }
}
