using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lll_seer_launcher.core.Dto.JSON
{
    public class PetJsonDto
    {
        public TaomeeMonseters monsters { get; set; }
    }
    public class TaomeeMonseters
    {
        public List<Pet> monster { get; set; }
    }
    public class Pet
    {
        public int id { get; set; }
        public string defName { get; set; }
        public int type { get; set; }
        public int hp { get; set; }
        public int atk { get; set; }
        public int def { get; set; }
        public int spatk { get; set; }
        public int spdef { get; set; }
        public int spd { get; set; }
        public int realId { get; set; }
        public LearnableMoves learnableMoves { get; set; }
    }
    public class LearnableMoves
    {
        public List<TaomeeMove> move { get; set; }
        public string GetMoves()
        {
            string moves = "";
            if(move != null)
            {
                foreach (var item in this.move)
                {
                    moves += $"{item.id}-{item.learningLv}|";
                }
                return moves.Substring(0, moves.Length - 1);
            }
            else
            {
                return "";
            }
        }
}
    public class TaomeeMove
    {
        public int id { get; set; }
        public int learningLv { get; set; }
    }
}
