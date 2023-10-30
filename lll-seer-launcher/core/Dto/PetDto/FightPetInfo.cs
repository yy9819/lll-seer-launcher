using System;
using System.Collections.Generic;
using lll_seer_launcher.core.Utils;
using lll_seer_launcher.core.Controller;

namespace lll_seer_launcher.core.Dto.PetDto
{
    public class FightPetInfo
    {
        public int userId { get; set; }
        public int petId { get; set; }
        public string petName { get; set; }
        public int catchTime { get; set; }
        public int hp { get; set; }
        public int maxHp { get; set; }
        public int level { get; set; }
        public PetResistanceInfo petResistanceInfo = new PetResistanceInfo();
        public int skinID { get; set; }
        public int catchType { get; set; }
        public List<Dictionary<string, int>> changehps = new List<Dictionary<string, int>>();
        public int requireSwitchCthTime { get; set; }
        public int xinHp { get; set; }
        public int xinMaxHp { get; set; }
        public int isChangeFace { get; set; }
        public int secretLaw { get; set; }
        public List<int> skillRunawayMarks = new List<int>();
        public int holyAndEvilThoughts { get; set; }
        public int yearVip2022Shengjian { get; set; }
        public int yearVip2022chujue { get; set; }

        public SiteBuffInfo siteBuffInfo = new SiteBuffInfo();
        public SiteBuffInfo bothSiteBuffInfo = new SiteBuffInfo();
        public MarkBuffInfo markBuffInfo = new MarkBuffInfo();
        public Dictionary<int, MarkBuffInfo> petBagMarkArr = new Dictionary<int, MarkBuffInfo>();
        public Dictionary<int, FightSignInfo> signInfoHash = new Dictionary<int, FightSignInfo>();

        public int curlockedSkillid { get; set; }

        public List<int> lockedSkillArr = new List<int>();

        public SkillStateInfos skillStateInfo = new SkillStateInfos();
        public int SetFightInfo(int index, byte[] inputData)
        {
            this.userId =  ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            this.petId = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            this.petName =  DBController.PetDBController.SearchPetNameByPetId(this.petId);
            index += 16;

            this.catchTime = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            this.hp = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            this.maxHp = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            if(this.hp > this.maxHp) this.maxHp = this.hp;
            index += 4;
            this.level = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            this.catchType = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            //this.petResistanceInfo
            index += 14 * 4;

            this.skinID = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            int petCount = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            for(int i = 0; i < petCount; i++)
            {
                int tmpPetId = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
                index += 4;

                this.changehps.Add(new Dictionary<string, int>()
                {
                    {"id",tmpPetId},
                    {"hp",ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4)) },
                    {"maxhp",ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4)) },
                    {"lock",ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4)) },
                    {"chujueNumber",ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4)) },
                    {"chujueRound",ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4)) }
                });
                index += 5 * 4;
                MarkBuffInfo markBuffInfo = new MarkBuffInfo();
                index = markBuffInfo.SetMarkBuffInfo(index, inputData);
                this.petBagMarkArr.Add(tmpPetId, markBuffInfo);
            }

            this.requireSwitchCthTime = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            this.xinHp = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            this.xinMaxHp = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            if (this.xinHp > this.xinMaxHp) this.xinMaxHp = this.xinHp;
            index += 4;
            this.isChangeFace = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            this.secretLaw = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            int skillRunawayLen = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            for(int i = 0; i < skillRunawayLen; i++)
            {
                this.skillRunawayMarks.Add(ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4)));
                index += 4;
            }

            this.holyAndEvilThoughts = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            this.yearVip2022Shengjian = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            this.yearVip2022chujue = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            index = this.siteBuffInfo.SetSiteBuffInfo(index,inputData);
            index = this.bothSiteBuffInfo.SetSiteBuffInfo(index, inputData);
            index = this.markBuffInfo.SetMarkBuffInfo(index, inputData);

            int signLen = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            for(int i = 0; i < signLen; i++)
            {
                FightSignInfo fightSignInfo = new FightSignInfo();
                index = fightSignInfo.SetFightSignInfo(index, inputData);
                if(!this.signInfoHash.ContainsKey(fightSignInfo.id))this.signInfoHash.Add(fightSignInfo.id, fightSignInfo);
            }

            for (int i = 0; i < 5; i++)
            {
                this.lockedSkillArr.Add(ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4)));
                index += 4;
            }
            //skillStateInfo
            //TODO
            return index;
        }
    }
    /// <summary>
    /// 抗性(固定占14*4)
    /// </summary>
    public class PetResistanceInfo
    {
        //TODO
    }
    public class SiteBuffInfo
    {
        int siteBuffId { get; set; }
        int siteBuffTurn { get; set; }
        public int SetSiteBuffInfo(int index , byte[] inputData)
        {
            this.siteBuffId = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 2));
            index += 2;
            this.siteBuffTurn = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index++, 1));
            return index;
        }
    }
    public class MarkBuffInfo
    {
        public int markBuffCnt;
        public List<Dictionary<string, int>> markBuffArr = new List<Dictionary<string, int>>();
        public int SetMarkBuffInfo(int index, byte[] inputData)
        {
            this.markBuffCnt = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index++, 1));
            for (int i = 0; i < markBuffCnt; i++)
            {
                int id = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 2));
                index += 2;
                int markNum = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index++, 1));
                this.markBuffArr.Add(new Dictionary<string, int>()
                {
                    {"id", id},
                    {"markNum" ,markNum}
                });
            }
            return index;
        }
    }
}
