using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Utils;
using lll_seer_launcher.core.Controller;

namespace lll_seer_launcher.core.Servise
{
    public static class InitJsonServise
    {
        private static string folderPath = Directory.GetCurrentDirectory() + "\\resource\\json\\";

        public static void LoadConfigJsonFile()
        {
            string serverVersionPath = folderPath + "serverversion.json";
            string loaclVersionPath = folderPath + "loaclversion.json";
            try
            {
                string serverJsonString = File.ReadAllText(serverVersionPath);
                VersionJsonObject serverJsonInfo = JsonConvert.DeserializeObject<VersionJsonObject>(serverJsonString);
                Dictionary<string, bool> shoudUpdataJsonDic = new Dictionary<string, bool>();
                Dictionary<string, string> jsonPathDic = new Dictionary<string, string>();
                if (File.Exists(loaclVersionPath))
                {
                    string localJsonString = File.ReadAllText(loaclVersionPath);
                    VersionJsonObject localJsonInfo = JsonConvert.DeserializeObject<VersionJsonObject>(localJsonString);
                    shoudUpdataJsonDic.Add("suit", !(serverJsonInfo.suitVersion == localJsonInfo.suitVersion));
                    shoudUpdataJsonDic.Add("glasses", !(serverJsonInfo.glassesVersion == localJsonInfo.glassesVersion));
                    shoudUpdataJsonDic.Add("achieveTitle", !(serverJsonInfo.achieveTitleVersion == localJsonInfo.achieveTitleVersion));
                    shoudUpdataJsonDic.Add("pet", !(serverJsonInfo.petVersion == localJsonInfo.petVersion));
                    shoudUpdataJsonDic.Add("petSkins", !(serverJsonInfo.petSkinsVersion == localJsonInfo.petSkinsVersion));
                }
                else
                {
                    shoudUpdataJsonDic.Add("suit", true);
                    shoudUpdataJsonDic.Add("glasses", true);
                    shoudUpdataJsonDic.Add("achieveTitle", true);
                    shoudUpdataJsonDic.Add("pet", true);
                    shoudUpdataJsonDic.Add("petSkins", true);
                }
                jsonPathDic.Add("suit", serverJsonInfo.suitVersion);
                jsonPathDic.Add("glasses", serverJsonInfo.glassesVersion);
                jsonPathDic.Add("achieveTitle", serverJsonInfo.achieveTitleVersion);
                jsonPathDic.Add("pet", serverJsonInfo.petVersion);
                jsonPathDic.Add("petSkins", serverJsonInfo.petSkinsVersion);
                File.WriteAllText(loaclVersionPath, serverJsonString);
                GlobalVariable.shoudUpdateJsonDic = shoudUpdataJsonDic;
                GlobalVariable.jsonPathDic = jsonPathDic;
            }
            catch (Exception)
            {

            }
        }

        public static bool InitAchieveTitleDictionary(string jsonName)
        {
            string path = folderPath + jsonName;
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
        public static bool InitSuitDictionary(string jsonName)
        {
            string path = folderPath + jsonName;
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

        public static bool InitGlassesDictionary(string jsonName)
        {
            //string path = folderPath + "glassesList.json";
            string path = folderPath + jsonName;
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

        public static bool InitPetDB(string jsonName)
        {
            //string path = folderPath + "glassesList.json";
            string path = folderPath + jsonName;
            try
            {
                string jsonString = File.ReadAllText(path);
                PetJsonObject info = JsonConvert.DeserializeObject<PetJsonObject>(jsonString);
                foreach (var item in info.data)
                {
                    DBController.PetDBController.InsertPetData(item);
                }
                Logger.Log("jsonFileInit", "精灵json加载完成。");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"精灵json加载失败！！！errorMessage{ex.Message}");
                return false;
            }
        }
        public static bool InitPetSkinsDB(string jsonName)
        {
            //string path = folderPath + "glassesList.json";
            string path = folderPath + jsonName;
            try
            {
                string jsonString = File.ReadAllText(path);
                PetSkinsJsonObject info = JsonConvert.DeserializeObject<PetSkinsJsonObject>(jsonString);
                foreach (var item in info.data)
                {
                    DBController.PetDBController.InsertPetSkinsData(item);
                }
                Logger.Log("jsonFileInit", "精灵皮肤json加载完成。");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"精灵json加载失败！！！errorMessage{ex.Message}");
                return false;
            }
        }
    }
}
