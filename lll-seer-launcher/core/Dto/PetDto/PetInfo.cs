using System;
using System.Text;
using System.Collections.Generic;
using lll_seer_launcher.core.Utils;
using lll_seer_launcher.core.Controller;

namespace lll_seer_launcher.core.Dto.PetDto
{
    public class PetInfo
    {
        private Dictionary<int, string> natureDic = new Dictionary<int, string>()
        {
            {0,"孤独(攻击+10% ,防御-10%)"},
            {1,"固执(攻击+10% ,特攻-10%)"},
            {2,"调皮(攻击+10% ,特防-10%)"},
            {3,"勇敢(攻击+10% ,速度-10%)"},
            {4,"大胆(防御+10% ,攻击-10%)"},
            {5,"顽皮(防御+10% ,特攻-10%)"},
            {6,"无虑(防御+10% ,特防-10%)"},
            {7,"悠闲(防御+10% ,速度-10%)"},
            {8,"保守(特攻+10% ,攻击-10%)"},
            {9,"稳重(特攻+10% ,防御-10%)"},
            {10,"马虎(特攻+10% ,特防-10%)"},
            {11,"冷静(特攻+10% ,速度-10%)"},
            {12,"沉着(特防+10% ,攻击-10%)"},
            {13,"温顺(特防+10% ,防御-10%)"},
            {14,"慎重(特防+10% ,特攻-10%)"},
            {15,"狂妄(特防+10% ,速度-10%)"},
            {16,"胆小(速度+10% ,攻击-10%)"},
            {17,"急躁(速度+10% ,防御-10%)"},
            {18,"开朗(速度+10% ,特攻-10%)"},
            {19,"天真(速度+10% ,特防-10%)"},
            {20,"害羞(平衡发展)"},
            {21,"实干(平衡发展)"},
            {22,"坦率(平衡发展)"},
            {23,"浮躁(平衡发展)"},
            {24,"认真(平衡发展)"},
        };
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
        public string nature { get; set; }
        /// <summary>
        /// 属性
        /// </summary>
        public int abilityType { get; set; }
        /// <summary>
        /// 精灵属性
        /// </summary>
        public string type { get; set; }
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
        public Dictionary<int, SkillInfo> skillArray { get; set; } = new Dictionary<int, SkillInfo>();
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
        public PetInfo(PetListInfo petListInfo)
        {
            this.petId = petListInfo.petId;
            this.petName = petListInfo.petName;
            this.catchTime = petListInfo.catchTime;
            this.level = petListInfo.level;
        }
        public PetInfo() { }
        public int GetNextIndex(int index, byte[] inputData)
        {
            index += 180;
            //效果总数
            this.effectCount = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 2));
            index += 2;
            index += this.effectCount * 24;
            index += PetInfo.resistanceInfoLens;
            index += PetInfo.otherInfoLens;
            return index;
        }
        public int SetPetInfo(int index, byte[] inputData)
        {
            this.petId = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            this.petName = PetNameDic.GetPetName(this.petId);

            index += 16;

            this.generation = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            //设置个体
            this.dv = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            //设置性格
            int natureId = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            this.nature = this.natureDic[natureId];
            index += 4;
            //设置属性
            this.abilityType = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            this.type = PetTypeDic.GetPetType(this.petId);
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
            for (int i = 0; i < 5; i++)
            {
                int skillId = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
                if (!this.skillArray.ContainsKey(skillId))
                {
                    SkillInfo skillInfo = new SkillInfo();
                    skillInfo.skillId = skillId;
                    skillInfo.skillName = SkillNameDic.GetSkillName(skillId);
                    skillInfo.skillPP = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index + 4, 4));
                    this.skillArray.Add(skillId, skillInfo);
                }
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
            for (int i = 0; i < 3; i++)
            {
                this.markList[0] = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
                index += 4;
            }
            this.commonMarkActived = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4)) != 0;
            index += 4;

            //效果总数
            this.effectCount = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 2));
            index += 2;
            for (int i = 0; i < this.effectCount; i++)
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
    public class SkillInfo
    {
        public int skillId { get; set; }
        public string skillName { get; set; }
        public int skillPP { get; set; }
    }

    public static class PetNameDic
    {
        private static Dictionary<int,string> petNameDic = new Dictionary<int,string>();
        private static object lockObj = new object();
        public static string GetPetName(int petId)
        {
            lock (lockObj)
            {
                if (PetNameDic.petNameDic.TryGetValue(petId , out string petName ))
                {
                    return petName;
                }
                else
                {
                    petName =  DBController.PetDBController.SearchPetNameByPetId(petId);
                    PetNameDic.petNameDic.Add(petId, petName);
                    return petName;
                }
            }
        }
    }

    public static class PetTypeDic
    {
        private static Dictionary<int, string> petTypeDic = new Dictionary<int, string>();
        private static object lockObj = new object();
        public static string GetPetType(int petId)
        {
            lock (lockObj)
            {
                if (PetTypeDic.petTypeDic.TryGetValue(petId, out string petType))
                {
                    return petType;
                }
                else
                {
                    petType = DBController.SkillDBController.GetTypeName(DBController.PetDBController.GetPetType(petId));
                    PetTypeDic.petTypeDic.Add(petId, petType);
                    return petType;
                }
            }
        }
    }

    public static class SkillNameDic
    {
        private static Dictionary<int,string> skillNameDic = new Dictionary<int, String>();
        private static object lockObj = new object();
        public static string GetSkillName(int skillId)
        {
            lock (lockObj)
            {
                if (SkillNameDic.skillNameDic.TryGetValue(skillId, out string skillName))
                {
                    return skillName;
                }
                else
                {
                    skillName = DBController.SkillDBController.SearchName(skillId);
                    SkillNameDic.skillNameDic.Add(skillId, skillName);
                    return skillName;
                }
            }
        }
    }
}
