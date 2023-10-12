using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Utils;

namespace lll_seer_launcher.core.Servise
{
    public static class InitJsonServise
    {
        public static bool InitAchieveTitleDictionary()
        {
            string folder = Directory.GetCurrentDirectory();
            string path = folder + "\\resource\\json\\achieveTitleList.json";
            try
            {
                string jsonString = File.ReadAllText(path);
                AchieveTitleJsonObject info = JsonConvert.DeserializeObject<AchieveTitleJsonObject>(jsonString);
                Dictionary<int, AchieveTitleInfo> achieveDic = new Dictionary<int, AchieveTitleInfo>();
                foreach (var item in info.data)
                {
                    achieveDic.Add(item.id, item);
                }
                GlobalVariable.achieveTitleDictionary = achieveDic;
                Logger.Log("jsonFileInit", "称号json加载完成。");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"称号json加载失败！！！errorMessage{ex.Message}");
                return false;
            }
        }
        public static bool InitSuitDictionary()
        {
            string folder = Directory.GetCurrentDirectory();
            string path = folder + "\\resource\\json\\suitList.json";
            try
            {
                string jsonString = File.ReadAllText(path);
                SuitJsonObject info = JsonConvert.DeserializeObject<SuitJsonObject>(jsonString);
                Dictionary<int, SuitInfo> suitDic = new Dictionary<int, SuitInfo>();
                foreach (var item in info.data)
                {
                    suitDic.Add(item.suitId, item);
                }
                GlobalVariable.suitDictionary = suitDic;
                Logger.Log("jsonFileInit", "套装json加载完成。");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"套装json加载失败！！！errorMessage{ex.Message}");
                return false;
            }
        }

        public static bool InitGlassesDictionary()
        {
            string folder = Directory.GetCurrentDirectory();
            string path = folder + "\\resource\\json\\glassesList.json";
            try
            {
                string jsonString = File.ReadAllText(path);
                GlassesJsonObject info = JsonConvert.DeserializeObject<GlassesJsonObject>(jsonString);
                Dictionary<int, GlassesInfo> glassesDic = new Dictionary<int, GlassesInfo>();
                foreach (var item in info.data)
                {
                    glassesDic.Add(item.glassesId, item);
                }
                GlobalVariable.glassesDictionary = glassesDic;
                Logger.Log("jsonFileInit", "目镜json加载完成。");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"目镜json加载失败！！！errorMessage{ex.Message}");
                return false;
            }
        }
    }
}
