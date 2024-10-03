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
        /**
         * 技能信息JSON
         * **/
        [JsonProperty("moves.json")]
        public string movesJson { get; set; }
        /**
         * 精灵信息JSON
         * **/
        [JsonProperty("monsters.json")]
        public string monstersJson { get; set; }
        /**
         * 刻印信息JSON
         * **/
        [JsonProperty("mintmark.json")]
        public string mintmarkJson { get; set; }
        /**
         * 道具信息JSON
         * **/
        [JsonProperty("items.json")]
        public string itemsJson { get; set; }
        /**
         * 特效JSON（魂印特效等）
         * **/
        [JsonProperty("effectIcon.json")]
        public string effectIconJson { get; set; }
        /**
         * 特性等JSON
         * **/
        [JsonProperty("new_se.json")]
        public string newSeJson { get; set; }
        /**
         * 属性JSON
         * **/
        [JsonProperty("skillTypes.json")]
        public string skillType { get; set; }
        /**
         * 称号信息JSON
         * **/
        [JsonProperty("achievements.json")]
        public string achievements { get; set; }
        /**
        * 装备信息JSON
        * **/
        [JsonProperty("equip.json")]
        public string equip { get; set; }
        ///
        /// 套装信息JSON///
        [JsonProperty("suit.json")]
        public string suit { get; set; }
        ///
        /// 技能石JSON///
        [JsonProperty("move_stones.json")]
        public string moveStones { get; set; }
    }
}
