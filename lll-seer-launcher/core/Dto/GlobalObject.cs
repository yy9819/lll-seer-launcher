using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lll_seer_launcher.core.Dto
{
    #region
    public class VersionJsonObject
    {
        public string suitVersion { get; set; }
        public string glassesVersion { get; set; }
        public string achieveTitleVersion { get; set; }
        public string petVersion { get; set; }
        public string petSkinsVersion { get; set; }
    }
    #endregion
    #region
    /// <summary>
    /// 称号信息Json解析对象
    /// </summary>
    public class AchieveTitleJsonObject
    {
        public string date { get; set; }
        public List<AchieveTitleInfo> data { get; set; }
    }
    /// <summary>
    /// 称号对象
    /// </summary>
    public class AchieveTitleInfo
    {
        public int id { get; set; } = 0;
        public string title { get; set; } = string.Empty;
        public string desc8 { get; set; } = string.Empty;
        public string abtext { get; set; } = string.Empty;
    }
    #endregion
    #region
    /// <summary>
    /// 套装信息Json解析对象
    /// </summary>
    public class SuitJsonObject
    {
        public string date { get; set; }
        public List<SuitInfo> data { get; set; }
    }
    /// <summary>
    /// 套装信息对象
    /// </summary>
    public class SuitInfo
    {
        public int suitId { get; set; } = 0;
        public string name { get; set; } = string.Empty;
        public string desc { get; set; } = string.Empty;
        public List<int> clothIdList { get; set; }
    }
    #endregion
    #region
    /// <summary>
    /// 目镜信息Json解析对象
    /// </summary>
    public class GlassesJsonObject
    {
        public string date { get; set; }
        public List<GlassesInfo> data { get; set; }
    }
    /// <summary>
    /// 目镜信息对象
    /// </summary>
    public class GlassesInfo
    {
        public int glassesId { get; set; } = 0;
        public string name { get; set; } = string.Empty;
        public string desc { get; set; } = string.Empty;
    }
    #endregion
    #region
    /// <summary>
    /// 用户持有套装、目镜、称号一览对象
    /// </summary>
    public class UserSuitAndAchieveTitleInfo
    {
        public int userId { get; set; } = GlobalVariable.userId;
        public List<int> suitIdList { get; set; }
        public List<int> glassesIdList { get; set; }
        public List<int> achieveTitleIdList { get; set; }
        public UserSuitAndAchieveTitleInfo(int userId, List<int> suitIdList, List<int> glassesIdList, List<int> achieveTitleIdList)
        {
            this.userId = userId;
            this.suitIdList = suitIdList;
            this.glassesIdList = glassesIdList;
            this.achieveTitleIdList = achieveTitleIdList;
        }
        public UserSuitAndAchieveTitleInfo() { }
    }
    #endregion
    #region
    public class SuitAchieveTitlePlan
    {
        public int userId { get; set; }
        public int suitId { get; set; }
        public int glassesId { get; set; }
        public int achieveTitleId { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    }
    #endregion

    #region
    public class PetJsonObject
    {
        public string date { get; set; }
        public List<Pet> data { get; set; }
    }
    /// <summary>
    /// 精灵信息
    /// </summary>
    public class Pet
    {
        public int id { get; set; }
        public string name { get; set; }
        public int hp { get; set; }
        public int atk { get; set; }
        public int def { get; set; }
        public int spatk { get; set; }
        public int spdef { get; set; }
        public int spd { get; set; }
        public int type { get; set; }
    }
    public class PetSkinsJsonObject
    {
        public string date { get; set; }
        public List<PetSkins> data { get; set; }
    }
    /// <summary>
    /// 精灵皮肤
    /// </summary>
    public class PetSkins
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    /// <summary>
    /// 皮肤替换方案
    /// </summary>
    public class PetSkinsReplacePlan
    {
        public PetSkinsReplacePlan() { }
        public PetSkinsReplacePlan(int petId,string petName, int skinsId, string skinsName)
        {
            this.petId = petId;
            this.petName = petName;
            this.skinsId = skinsId;
            this.skinsName = skinsName;
        }
        public int id { get; set; }
        public int petId { get; set; }
        public string petName { get; set; }
        public int skinsId { get; set; }
        public string skinsName { get; set; }
    }
    #endregion
}
