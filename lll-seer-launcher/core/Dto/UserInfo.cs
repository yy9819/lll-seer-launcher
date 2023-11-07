using System;
using System.Text;
using System.Collections.Generic;
using lll_seer_launcher.core.Utils;
using lll_seer_launcher.core.Controller;

namespace lll_seer_launcher.core.Dto
{
    public class UserInfo
    {
        public int userId { get; set; }
        public int regTime { get; set; }
        public string nickName { get; set; }
        public int coins { get; set; }
        public int vipLevel { get; set; }
        public int petBagGreatestAmount { get; set; }
        public int fireBuffType { get; set; }
        public List<int> clothList { get; set; } = new List<int>();

        public int SetUserInfoForFireType(int index,byte[] inputData)
        {
            index += 4;
            //获取当前循环的玩家id
            this.userId = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 161;
            //根据当前循环读取的账号*数量对火焰index进行增加
            index += ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4)) * 4 + 64;
            //根据当前循环读取的账号所装备的部件数量对火焰index进行增加
            index += ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4)) * 8 + 8;
            //获取当前账号的火焰效果
            this.fireBuffType = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 1));
            index += 29;
            return index;
        }
        public void SetUserInfoForOnlogin(byte[] inputData)
        {
            int index = 0;
            this.userId = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            this.regTime = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            this.nickName = Encoding.UTF8.GetString(ByteConverter.TakeBytes(inputData, index, 16)).Replace("?", "");
            index += 16 + 3 * 4;
            this.coins = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += (int)(30.75 * 4);
            this.vipLevel = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            this.petBagGreatestAmount = 6 + (this.vipLevel < 1 ? 3 : (this.vipLevel < 3 ? 4 : (this.vipLevel < 5 ? 5 : 6)));
        }
    }
   
    
}
