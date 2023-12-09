using System;
using System.Collections.Generic;
using lll_seer_launcher.core.Utils;
using lll_seer_launcher.core.Controller;

namespace lll_seer_launcher.core.Dto.PetDto
{
    public class ChangePetInfo
    {
        public int userId { get; set; }
        public int petId { get; set; }
        public int catchTime { get; set; }
        public string petName { get; set; }
        public int level { get; set; }

        public int hp { get; set; }
        public int maxHp { get; set; }
        public PetResistanceInfo petResistanceInfo = new PetResistanceInfo();
        public int skinID { get; set; }
        public int xinHp { get; set; }
        public int xinMaxHp { get; set; }
        public int isChangeFace { get; set; }
        public List<Dictionary<string, int>> changehps = new List<Dictionary<string, int>>();
        public List<List<int>> skillList = new List<List<int>>();
        public List<int> skillRunawayMarks = new List<int>();
        public int holyAndEvilThoughts { get; set; }
        public int yearVip2022Shengjian { get; set; }
        public int yearVip2022chujue { get; set; }
        public int laborDay2022Yinji { get; set; }
        public int suli2022 { get; set; }
        public SiteBuffInfo siteBuffInfo = new SiteBuffInfo();
        public SiteBuffInfo bothSiteBuffInfo = new SiteBuffInfo();
        public MarkBuffInfo markBuffInfo = new MarkBuffInfo();
        public int mulian2022 { get; set; }
        public Dictionary<int, MarkBuffInfo> petBagMarkArr = new Dictionary<int, MarkBuffInfo>();
        public Dictionary<int, FightSignInfo> signInfoHash = new Dictionary<int, FightSignInfo>();
        public SkillStateInfos skillStateInfo = new SkillStateInfos();
        public List<int> lockedSkillArr = new List<int>();
        public int SetChangPetInfo(int index, byte[] inputData)
        {
            this.userId = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            this.petId = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            this.catchTime = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            this.petName =  PetNameDic.GetPetName(this.petId);
            index += 16;

            this.level = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            this.hp = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            this.maxHp = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            int skillLen = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            for(int i = 0; i < skillLen; i++)
            {
                List<int> skill = new List<int>();
                skill.Add(ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4)));
                index += 4;
                skill.Add(ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4)));
                index += 4;
                this.skillList.Add(skill);
            }
            //PetResistanceInfo
            //TODO
            index += 14*4;

            this.skinID = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            int petBagMarkLen = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            for( int i = 0;i < petBagMarkLen; i++)
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
            this.xinHp = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            this.xinMaxHp = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            this.isChangeFace = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            
            int skillRunawayMarkLen = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            for(int i = 0; i < skillRunawayMarkLen; i++)
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

            this.laborDay2022Yinji = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            this.suli2022 = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            this.mulian2022 = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            index = this.siteBuffInfo.SetSiteBuffInfo(index, inputData);
            index = this.bothSiteBuffInfo.SetSiteBuffInfo(index, inputData);
            index = this.markBuffInfo.SetMarkBuffInfo(index, inputData);

            int signLen = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            for (int i = 0; i < signLen; i++)
            {
                FightSignInfo fightSignInfo = new FightSignInfo();
                index = fightSignInfo.SetFightSignInfo(index, inputData);
                if (!this.signInfoHash.ContainsKey(fightSignInfo.id)) this.signInfoHash.Add(fightSignInfo.id, fightSignInfo);
            }

            for (int i = 0; i < 5; i++)
            {
                this.lockedSkillArr.Add(ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4)));
                index += 4;
            }
            return index;
        }
    }
}
