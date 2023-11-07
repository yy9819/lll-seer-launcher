using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lll_seer_launcher.core.Dto.JSON
{
    public class PetEffectDto
    {
        public PetEffectRoot root { get; set; }
    }
    public class PetEffectRoot
    {
        public List<PetEffect> effect { get; set; }
    }
    public class PetEffect
    {
        public string petId { get; set; } = "";
        public int iconId { get; set; } = 0;
        public int effectId { get; set; } = 0;
        public string tips { get; set; } = "";

    }
}
