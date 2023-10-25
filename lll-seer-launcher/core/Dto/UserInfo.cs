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
            //Console.WriteLine($"userId:{this.userId} regTime:{this.regTime} nicName:{this.nickName} coins:{this.coins} vipLevel:{this.vipLevel}");
        }
    }
    public class PetInfo
    {
        #region
        public int petId { get; set; }
        public string petName { get; set; }
        /// <summary>
        /// 是否为一代精灵(繁殖精灵)
        /// </summary>
        public int generation { get; set; }
        /// <summary>
        /// 个体
        /// </summary>
        public int dv { get; set; }
        /// <summary>
        /// 性格
        /// </summary>
        public int nature { get; set; }
        /// <summary>
        /// 属性
        /// </summary>
        public int abilityType { get; set; }
        /// <summary>
        /// 等级
        /// </summary>
        public int level { get; set; }
        /// <summary>
        /// 当前体力
        /// </summary>
        public int hp { get; set; }
        /// <summary>
        /// 最大体力
        /// </summary>
        public int maxHp { get; set; }
        /// <summary>
        /// 攻击力
        /// </summary>
        public int attack { get; set; }
        /// <summary>
        /// 防御力
        /// </summary>
        public int defence { get; set; }
        /// <summary>
        /// 特攻
        /// </summary>
        public int spAttack { get; set; }
        /// <summary>
        /// 特防
        /// </summary>
        public int spDefence { get; set; }
        /// <summary>
        /// 速度
        /// </summary>
        public int speed { get; set; }
        /// <summary>
        /// 体力学习力
        /// </summary>
        public int evHp { get; set; }
        /// <summary>
        /// 攻击学习力
        /// </summary>
        public int evAttack { get; set; }
        /// <summary>
        /// 防御学习力
        /// </summary>
        public int evDefence { get; set; }
        /// <summary>
        /// 特攻学习力
        /// </summary>
        public int evSpAttack { get; set; }
        /// <summary>
        /// 特防学习力
        /// </summary>
        public int evSpDefence { get; set; }
        /// <summary>
        /// 速度学习力
        /// </summary>
        public int evSpeed { get; set; }
        /// <summary>
        /// 技能总数
        /// </summary>
        public int skillLens { get; set; }
        /// <summary>
        /// 技能List
        /// -skillLens不管为多少，skillArray固定占5 * 8个字节(技能Id + pp)
        /// </summary>
        public int[] skillArray { get; set; } = new int[5];
        /// <summary>
        /// 捕捉时间
        /// </summary>
        public int catchTime { get; set; }
        /// <summary>
        /// 捕捉地图
        /// </summary>
        public int catchMap { get; set; }
        public int catchRect { get; set; }
        /// <summary>
        /// 被捕捉时的等级
        /// </summary>
        public int catchLevel { get; set; }
        /// <summary>
        /// 刻印对应的时间码(无论佩戴了几个刻印，固定占3 * 4个字节)
        /// </summary>
        public int[] markList { get; set; } = new int[3];
        /// <summary>
        /// 是否已开启第三孔
        /// </summary>
        public bool commonMarkActived { get; set; }
        /// <summary>
        /// 精灵所持效果总数
        /// 两个字节
        /// </summary>
        public int effectCount { get; set; }
        /// <summary>
        /// 精灵所持效果（每一个效果占6 * 4个字节）
        /// -1 特性
        /// -2 战队加成
        /// -3 魂印
        /// -4~ 能量珠
        /// </summary>
        public List<PetEffectInfo> petEffectInfos { get; set; } = new List<PetEffectInfo> { };
        /// <summary>
        /// 无论是否已开启抗性，抗性所占字节数固定为14*4
        /// </summary>
        public const int resistanceInfoLens = 14 * 4;
        /// <summary>
        /// 其他信息
        /// -皮肤，坐骑，pvp.pve.base的各项能力值等所占字符数
        /// </summary>
        public const int otherInfoLens = 26 * 4;
        #endregion
        public int SetPetInfo(int index,byte[] inputData)
        {
            this.petId = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            this.petName = DBController.PetDBController.SearchPetNameByPetId(petId);
            index += 16;

            this.generation = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            //设置个体
            this.dv = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            //设置性格
            this.nature = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            //设置属性
            this.abilityType = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            //设置等级
            this.level = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            //增加经验index（exp,lvexp,nextexp）
            index += 12;

            //设置当前体力
            this.hp = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            //设置最大体力
            this.maxHp = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            //设置攻击力
            this.attack = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            //设置防御
            this.defence = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            //设置特攻
            this.spAttack = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            //设置特防
            this.spDefence = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            //设置速度
            this.speed = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            //体力学习力
            this.evHp = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            //攻击学习力
            this.evAttack = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            //防御学习力
            this.evDefence = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            //特攻学习力
            this.evSpAttack = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            //特防学习力
            this.evSpDefence = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            //速度学习力
            this.evSpeed = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            //设置技能
            this.skillLens = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            for(int i = 0; i < 5; i++)
            {
                this.skillArray[i] = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
                index += 8;
            }
            //设置捕捉时间戳（每只精灵的唯一key）
            this.catchTime = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            //设置捕捉地图
            this.catchMap = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            //
            this.catchRect = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            //设置捕捉时等级
            this.catchLevel = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            //设置刻印唯一key（获得时间戳）
            for(int i= 0; i < 3; i++)
            {
                this.markList[0] = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
                index += 4;
            }
            this.commonMarkActived = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4)) != 0;
            index += 4;

            //效果总数
            this.effectCount = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 2));
            index += 2;
            for(int i = 0;i < this.effectCount; i++)
            {
                PetEffectInfo tmpPetEffectInfo = new PetEffectInfo();
                index = tmpPetEffectInfo.SetPetEffectInfo(index, inputData);
                this.petEffectInfos.Add(tmpPetEffectInfo);
            }
            index += PetInfo.resistanceInfoLens;
            index += PetInfo.otherInfoLens;
            return index;
        }
    }
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
        public int SetPetEffectInfo(int index,byte[] data)
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
            if(argList[0] == 0 && argList[1] == 0 && argList[2] == 0 &&  argList[3] == 0 &&  argList[4] == 0 &&  argList[5] == 0)
            {
                this.args = $"{arg1}-{0}";
            }
            else
            {
                this.args = arg1.ToString();
                int i = 6;
                while (i>=0)
                {
                    if(argList[i] > 0)
                    {
                        int j = 0;
                        while( j < i)
                        {
                            this.args += $"-{args[j]}";
                            j++;
                        }
                        break;
                    }
                    i--;
                }
            }
            for(int i = 0;i < 5; i++)
            {
                this.args6 += $"{argList[i]}-";
            }
            this.args6 += arg1.ToString();
            if(this.effectId == 171)
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
