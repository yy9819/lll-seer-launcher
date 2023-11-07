using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace lll_seer_launcher.core.Dto.JSON
{
    public class TypeDto
    {
        public TypeRoot root { get; set; }
    }
    public class TypeRoot
    {
        public List<TypeItem> item { get; set; }
    }
    public class TypeItem
    {
        public int id { get; set; }

        [JsonProperty("cn")]
        public string name { get; set; }
    }
}
