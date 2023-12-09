using System;
using System.Collections.Generic;
using lll_seer_launcher.core.Utils;
using lll_seer_launcher.core.Controller;

namespace lll_seer_launcher.core.Dto.PetDto
{
    public class AttackValueInfo
    {
        public int userId;
        public int skillId;
        public string effectName;
        /// <summary>
        /// 技能次数(攻击次数)
        /// </summary>
        public int atkTimes;
        /// <summary>
        /// 对方掉血
        /// </summary>
        public int lostHP;
        /// <summary>
        /// 自己回血
        /// </summary>
        public int gainHP;
        /// <summary>
        /// 剩余
        /// </summary>
        public int remainHP;
        /// <summary>
        /// 最大血量
        /// </summary>
        public int maxHP;
        /// <summary>
        /// 是否暴击
        /// -0 NO
        /// -1 YES
        /// </summary>
        public int isCrit;
        /// <summary>
        /// 技能
        /// </summary>
        public Dictionary<int, SkillInfo> skillArray { get; set; } = new Dictionary<int, SkillInfo>();
        /// <summary>
        /// 异常状态
        /// </summary>
        public List<int> status = new List<int>();
        /// <summary>
        /// 精灵特殊状态
        /// </summary>
        public int state;
        /// <summary>
        /// 某些精灵的特殊状态显示
        /// </summary>
        public int petStatus;
        /// <summary>
        /// 
        /// </summary>
        public List<PetStatusEffectInfo> sideEffects = new List<PetStatusEffectInfo>();
        public int battleLv;
        public int changeBitset;
        /// <summary>
        /// 先手
        /// </summary>
        public int priority;
        /// <summary>
        /// 免疫
        /// </summary>
        public List<bool> immunizationStates = new List<bool>();
        public List<int> specailArr = new List<int>();
        public bool issecondFight;
        public int changeValue;
        public List<Dictionary<string,int>> changehps = new List<Dictionary<string, int>>();
        public int requireSwitchCthTime;
        public int changeValue2;
        public int maxHpSelf;
        public int maxHpOther;
        public int secretLaw;
        /// <summary>
        /// 真实伤害血量
        /// </summary>
        public int realHurtHp;
        public List<int> skillRunawayMarks = new List<int>();
        public SiteBuffInfo siteBuffInfo = new SiteBuffInfo();
        public SiteBuffInfo bothSiteBuffInfo = new SiteBuffInfo();
        public MarkBuffInfo markBuffInfo = new MarkBuffInfo();
        public Dictionary<int,MarkBuffInfo> petBagMarkDic = new Dictionary<int, MarkBuffInfo>();
        public Dictionary<int,FightSignInfo> signInfoHash = new Dictionary<int, FightSignInfo>();
        public List<int> lockedSkillArr = new List<int>();
        public SkillStateInfos skillStateInfo = new SkillStateInfos();
        public List<int> skillResultArr = new List<int>();
        
        private Dictionary<int,string> fightEffectNameDic = new Dictionary<int, string>()
        {
            {1,"无效" },
            {2,"微弱" },
            {3,"克制" },
        };
        public int SetAttackValueInfo(int index, byte[] inputData)
        {
            this.userId = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            this.skillId = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            index += 4 * 2;

            this.fightEffectNameDic.TryGetValue(ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4)), out this.effectName);
            index += 4;

            this.atkTimes = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            this.lostHP = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            this.realHurtHp = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            this.gainHP = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            this.remainHP = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            this.maxHP = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            this.state = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            this.petStatus = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            //设置技能
            int skillNum = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            for (int i = 0; i < skillNum; i++)
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

            //是否暴击
            this.isCrit = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            //异常状态
            int statusLen = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index++, 1));
            for (int i = 0; i < statusLen; i++)
            {
                this.status.Add(ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index++, 1)));
            }

            //
            int specailLen = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            for (int i = 0;i < specailLen; i++)
            {
                this.specailArr.Add(ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4)));
                index += 4;
            }
            if (specailLen > 13) this.changeValue = this.specailArr[13];
            if (specailLen > 25) this.changeValue2 = this.specailArr[25];

            //对战状态处理--属性的变化
            int petStatusEffectLen = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            for (int i = 0; i < petStatusEffectLen; i++)
            {
                PetStatusEffectInfo petStatusEffectInfo = new PetStatusEffectInfo();
                index = petStatusEffectInfo.SetPetStatusEffectInfo(index, inputData);
                this.sideEffects.Add(petStatusEffectInfo);
            }

            //能力变化-->大于0能力提升小于0能力下降
            this.battleLv = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            //异常变化-每位-0-不变-1-改变
            this.changeBitset = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            //先手变化-大于0-先手展示
            this.priority = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            //免疫状态
            int immunizationStateLen = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            for (int i = 0; i<immunizationStateLen; i++)
            {
                int tmpInt = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
                index += 4;
                bool status = (tmpInt & 15) == 9 || (tmpInt & 15) == 1;
                this.immunizationStates.Add(status);
            }

            int changHpLen = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            for (int i=0; i<changHpLen; i++)
            {
                int id = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
                index += 4;
                this.changehps.Add(new Dictionary<string, int>() 
                {
                    {"id" , id},
                    {"hp" , ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4))},
                    {"maxhp" , ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index + 4, 4))},
                    {"lock" , ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index + 8, 4))},
                    {"chujueNumber" , ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index + 12, 4))},
                    {"chujueRound" , ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index + 16, 4))},
                });
                index += 5 * 4;
                MarkBuffInfo markBuffInfo = new MarkBuffInfo();
                index = markBuffInfo.SetMarkBuffInfo(index, inputData);
                this.petBagMarkDic.Add(id, markBuffInfo);
            }
            this.requireSwitchCthTime = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            this.maxHpSelf = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            if (this.maxHpSelf == 0) this.maxHpSelf = this.maxHP;
            index += 4;
            this.maxHpOther = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            this.secretLaw = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            int skillRunawayMarkLen = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            for (int i = 0; i < skillRunawayMarkLen; i++)
            {
                this.skillRunawayMarks.Add(ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4)));
                index += 4;
            }
            SiteBuffInfo buffInfo = new SiteBuffInfo();
            index = buffInfo.SetSiteBuffInfo(index, inputData);
            this.siteBuffInfo = buffInfo;

            SiteBuffInfo bothBuffInfo = new SiteBuffInfo();
            index = bothBuffInfo.SetSiteBuffInfo(index, inputData);
            this.bothSiteBuffInfo = bothBuffInfo;

            MarkBuffInfo markBuff = new MarkBuffInfo();
            index = markBuff.SetMarkBuffInfo(index, inputData);
            this.markBuffInfo =  markBuff;

            int signInfoLen = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            for (int i = 0;i < signInfoLen; i++)
            {
                FightSignInfo fightSignInfo = new FightSignInfo();
                index = fightSignInfo.SetFightSignInfo(index, inputData);
                this.signInfoHash.Add(fightSignInfo.id, fightSignInfo);
            }

            for(int i = 0; i < 5; i++)
            {
                this.lockedSkillArr.Add(ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4)));
                index += 4;
            }

            /*
             * TODO
                 this._skillStateInfo = new SkillStateInfos();
                 this._skillStateInfo.runawayMoveNum = _loc10_;
                 this._skillStateInfo.skillRunawayMarks = this._skillRunawayMarks;
                 this._skillStateInfo.lockedSkillArr = this._lockedSkillArr;
                 this._skillResultArr = [];
             */

            int skillResultLen = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            for ( int i = 0; i<skillResultLen; i++)
            {
                this.skillResultArr.Add(ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4)));
                index += 4;
            }
            return index;
        }
    }
    public class FightSignInfo
    {
        public int id;
        public int lvNum;
        public int roundNum;
        public int spValue;
        public int SetFightSignInfo(int index, byte[] inputData)
        {
            int basicValue = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            this.id = this.subByte(basicValue, 0, 16);
            this.lvNum = this.subByte(basicValue, 16, 8);
            this.roundNum = this.subByte(basicValue, 24, 8);
            this.spValue = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            return index;
        }
        private int subByte(int basicValue , int index , int len)
        {
            int result = 0;
            if(index + len > 32)
            {
                throw new Exception("超出uint上限！");
            }
            if(len > 16)
            {
                result = this.subByte(basicValue, index, 16) + this.subByte((int)(basicValue - result), index + 16, len - 16);
                return result;
            }
            return 65535 >> 16 - len & basicValue >> index;
        }
    }
    public class SkillStateInfos
    {
        public int runawayMoveNum;
        public List<int> skillRunawayMarks = new List<int>();

        public int lockedSkillId;
        public List<int> lockedSkillArr = new List<int>();

    }
    public class PetStatusEffectInfo
    {
        public int type;
        public int id;
        public int parm;
        public string name;
        public string description;
        public bool isShow;
        public int SetPetStatusEffectInfo(int index ,byte[] inputData)
        {
            this.type = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            this.id = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            this.parm = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            /* TODO
                this.name = PetStatusEffectConfig.getName(this.type,this.id);
                this.des = PetStatusEffectConfig.getDescription(this.type,this.id);
                this.isShow = PetStatusEffectConfig.isShow(this.type,this.id); */
            return index;
        }
    }
    public class StatusInfo
    {
        public readonly static List<Dictionary<int,string>> Status = new List<Dictionary<int, string>>()
        {
            new Dictionary<int, string>()
            {
                {0,"麻痹" },
                {1,"中毒" },
                {2,"烧伤" },
                {3,"寄生对手" },
                {4,"寄生" },
                {5,"冻伤" },
                {6,"害怕" },
                {7,"疲惫" },
                {8,"睡眠" },
                {9,"石化" },
                {10,"混乱" },
                {11,"衰弱" },
                {12,"山神守护" },
                {13,"易燃" },
                {14,"狂暴" },
                {15,"冰封" },
                {16,"流血" },
                {17,"免疫" },
                {18,"免疫" },
                {19,"瘫痪" },
                {20,"失明" },
                {21,"异常免疫" },
                {22,"焚烬" },
                {23,"诅咒" },
                {24,"烈焰诅咒" },
                {25,"致命诅咒" },
                {26,"虚弱诅咒" },
                {27,"感染" },
                {28,"束缚" },
                {29,"失神" },
                {30,"沉默" },
                {31,"臣服" },
                {32,"凝滞" },
            },
            new Dictionary<int, string>()
            {
                //参数n 以6为基准  n-6为负数表示能力减弱了多少，为正数表示提升了多少
                {1,"攻击" },
                {2,"防御" },
                {3,"特攻" },
                {4,"特防" },
                {5,"速度" },
                {6,"命中" },
            },
            new Dictionary<int, string>()
            {
                {1,"印记" }
            },
        };
    }
}
