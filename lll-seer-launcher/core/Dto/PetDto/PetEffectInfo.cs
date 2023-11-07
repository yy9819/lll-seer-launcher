using System;
using System.Text;
using System.Collections.Generic;
using lll_seer_launcher.core.Utils;
using lll_seer_launcher.core.Controller;

namespace lll_seer_launcher.core.Dto.PetDto
{
    public class PetEffectInfo
    {
        public int itemId { get; set; }
        public int status { get; set; }
        public int leftCount { get; set; }
        public int effectId { get; set; }
        public string args { get; set; }
        public string args6 { get; set; }
        public List<int> addedAblityList { get; set; } = new List<int>();
        public List<int> levelList { get; set; } = new List<int>();
        public int SetPetEffectInfo(int index, byte[] data)
        {
            this.itemId = ByteConverter.BytesTo10(ByteConverter.TakeBytes(data, index, 4));
            index += 4;
            this.status = ByteConverter.BytesTo10(ByteConverter.TakeBytes(data, index++, 1));
            this.leftCount = ByteConverter.BytesTo10(ByteConverter.TakeBytes(data, index++, 1));
            this.effectId = ByteConverter.BytesTo10(ByteConverter.TakeBytes(data, index, 2));
            index += 2;

            int arg1;
            arg1 = ByteConverter.BytesTo10(ByteConverter.TakeBytes(data, index++, 1));
            arg1 += this.CheckAdd(ByteConverter.TakeBytes(data, index++, 1));

            List<int> argList = new List<int>();
            for (int i = 0; i < 7; i++)
            {
                int arg = ByteConverter.BytesTo10(ByteConverter.TakeBytes(data, index++, 1));
                arg += ByteConverter.BytesTo10(ByteConverter.TakeBytes(data, index++, 1));
                argList.Add(arg);
            }
            if (argList[0] == 0 && argList[1] == 0 && argList[2] == 0 &&  argList[3] == 0 &&  argList[4] == 0 &&  argList[5] == 0)
            {
                this.args = $"{arg1} {0}";
            }
            else
            {
                this.args = arg1.ToString();
                int i = 6;
                while (i >= 0)
                {
                    if (argList[i] > 0)
                    {
                        int j = 0;
                        while (j <= i)
                        {
                            this.args += $" {argList[j]}";
                            j++;
                        }
                        break;
                    }
                    i--;
                }
            }
            for (int i = 0; i < 5; i++)
            {
                this.args6 += $"{argList[i]} ";
            }
            this.args6 += arg1.ToString();
            if (this.effectId == 171)
            {
                this.addedAblityList.Add(arg1);
                for (int i = 0; i < 5; i++)
                {
                    this.addedAblityList.Add(argList[i]);
                }
                //战队加成当前强化等级？
                int level = argList[5];
                this.levelList.Add(level % 16);
                level -= level % 16;
                level /= 16;
                this.levelList.Add(level % 16);
                level -= level % 16;
                level /= 16;
                this.levelList.Add(level % 16);

                level = argList[6];
                this.levelList.Add(level % 16);
                level -= level % 16;
                level /= 16;
                this.levelList.Add(level % 16);
                level -= level % 16;
                level /= 16;
                this.levelList.Add(level % 16);
            }
            return index;
        }
        private int CheckAdd(byte[] data)
        {
            int result = Convert.ToInt32(data[0]);
            return result == 0 ? 0 : result * 256;
        }

    }

}
