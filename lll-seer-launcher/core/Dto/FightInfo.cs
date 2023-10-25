using System;
using System.Collections.Generic;
using lll_seer_launcher.core.Utils;

namespace lll_seer_launcher.core.Dto
{
    public class FightInfo
    {
        public int userId { get; set; }
        public int petId { get; set; }
        public int petName { get; set; }
        public int catchTime { get; set; }
        public int hp { get; set; }
        public int maxHp { get; set; }
        public int level { get; set; }
        public PetResistanceInfo petResistanceInfo = new PetResistanceInfo();
        public int skinID { get; set; }
        public int catchType { get; set; }
        public int requireSwitchCthTime { get; set; }
        public int xinHp { get; set; }
        public int xinMaxHp { get; set; }
        public int isChangeFace { get; set; }
        public int secretLaw { get; set; }
        public int skillRunawayMarks { get; set; }
        public int holyAndEvilThoughts { get; set; }
        public int yearVip2022Shengjian { get; set; }
        public int yearVip2022chujue { get; set; }

        public SiteBuffInfo siteBuffInfo = new SiteBuffInfo();
        public SiteBuffInfo bothSiteBuffInfo = new SiteBuffInfo();
        public MarkBuffInfo markBuffInfo = new MarkBuffInfo();
    }
    public class PetResistanceInfo
    {

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
