﻿using System;
using System.Collections.Generic;
using lll_seer_launcher.core.Utils;

namespace lll_seer_launcher.core.Dto
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
        public List<List<int>> skillList = new List<List<int>>();
        public List<int> status = new List<int>();
        /// <summary>
        /// 精灵特殊状态
        /// </summary>
        public int state;
        /// <summary>
        /// 某些精灵的特殊状态显示
        /// </summary>
        public int petStatus;
        public List<PetStatusEffectInfo> sideEffects = new List<PetStatusEffectInfo>();
        public int battleLv;
        public int changeBitset;
        public int priority;
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
                index += 4;
                int skillPP = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
                index += 4;
                this.skillList.Add(new List<int>(2) { skillId, skillPP });
            }

            //是否暴击
            this.isCrit = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            //特效状态
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
            for (int i = 0; i<petStatusEffectLen; i++)
            {
                PetStatusEffectInfo petStatusEffectInfo = new PetStatusEffectInfo();
                index = petStatusEffectInfo.SetPetStatusEffectInfo(index, inputData);
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
}
