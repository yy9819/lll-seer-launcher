using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lll_seer_launcher.core.Dto.JSON
{
    public class SkillJsonDto
    {
        public MovesTbl movesTbl { get; set; }
    }
    public class MovesTbl
    {
        public Moves moves { get; set; }
    }   
    public class Moves
    {
        public List<Move> move { get; set; }
    }
    public class Move
    {
        public int id { get; set; }
        public string name { get; set; }
        public int type { get; set; }
        public int power { get; set; }
        public int maxPP { get; set; }
        public int accuracy { get; set; }
    }
}
