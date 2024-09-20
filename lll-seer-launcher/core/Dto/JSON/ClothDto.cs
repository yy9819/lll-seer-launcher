using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lll_seer_launcher.core.Dto.JSON
{
    public class ClothDto
    {
        public Equips equips { get; set; }
    }
    public class Equips
    {
        public List<EquipDto> equip { get; set; }
    }
    public class EquipDto
    {
        public int id { get; set; }
        public int itemId { get; set; }
        public int qulity { get; set; }
        public int occasion { get; set; }
        public int part { get; set; }
        public int target { get; set; }
        public string monId { get; set; }
        public int suitId { get; set; }
        public int suitNewseId { get; set; }
        public string AddArgs { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
    }

    public class SuitJSON
    {
        public SuitRoot root { get; set; }
    }
    public class SuitRoot
    {
        public List<SuitItem> item { get; set; }
    }
    public class SuitItem
    {
        public int id { get; set; }
        public string cloths { get; set; }
        public string name { get; set; }
        public string suitdes { get; set; }
    }
}
