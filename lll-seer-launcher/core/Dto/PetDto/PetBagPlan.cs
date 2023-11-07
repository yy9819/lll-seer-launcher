using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lll_seer_launcher.core.Dto.PetDto
{
    public class PetBagPlan
    {
        public int planId { get; set; }
        public int userId { get; set; }
        public string planName { get; set; }
        public string fightPetsName { get; set; }
        public string fightPetsCatchTime { get; set; }
        public string awaitPetsName { get; set; }
        public string awaitPetsCatchTime { get; set; }
    }
}
