using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lll_seer_launcher.core.Dto
{
    public class HeadInfo
    {
        public int packageLen;
        public int version;
        public int cmdId;
        public int userId;
        public int seq;
        public byte[] encryptData;
        public byte[] decryptData;

        public override string ToString()
        {
            return $"packageLen:{packageLen}--version:{version}--cmdId:{cmdId}--userId:{userId}--seq:{seq}";
        }
    }
}
