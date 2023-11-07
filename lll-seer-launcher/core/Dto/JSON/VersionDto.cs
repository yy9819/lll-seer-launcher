using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace lll_seer_launcher.core.Dto.JSON
{
    public class TaomeeVersionDto
    {
        public TaomeeVersionFiles files { get; set; }
    }
    public class TaomeeVersionFiles
    {
        public TaomeeVersionResource resource { get; set; }
    }
    public class TaomeeVersionResource
    {
        public TaomeeVersionConfig config { get; set; }
    }
    public class TaomeeVersionConfig
    {
        public TaomeeVersionXml xml { get; set; }
    }
    public class TaomeeVersionXml
    {
        [JsonProperty("moves.json")]
        public string movesJson { get; set; }

        [JsonProperty("monsters.json")]
        public string monstersJson { get; set; }

        [JsonProperty("mintmark.json")]
        public string mintmarkJson { get; set; }

        [JsonProperty("items.json")]
        public string itemsJson { get; set; }

        [JsonProperty("effectIcon.json")]
        public string effectIconJson { get; set; }

        [JsonProperty("new_se.json")]
        public string newSeJson { get; set; }
        [JsonProperty("skillTypes.json")]
        public string skillType { get; set; }
    }
}
