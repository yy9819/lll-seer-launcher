using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Dto.JSON;
using lll_seer_launcher.core.Utils;
using lll_seer_launcher.core.Controller;

namespace lll_seer_launcher.core.Service
{
    public static class InitJsonService
    {
        private static string folderPath = Directory.GetCurrentDirectory() + "\\bin\\json\\";
        private static string taomeeJsonLink = "https://seerh5.61.com/resource/config/xml/";
        private static string taomeeJsonVersionLink = "https://seerh5.61.com/version/version.json";

        private static string jsonLink = "http://52.68.134.105/resource/json/";

        public static void LoadConfigJsonFile(VersionConfig serverJsonInfo)
        {
            if(!Directory.Exists(folderPath))Directory.CreateDirectory(folderPath);
            string loaclVersionPath = folderPath + "loaclversion.json";
            try
            {
                if (File.Exists(loaclVersionPath))
                {
                    string localJsonString = File.ReadAllText(loaclVersionPath);
                    VersionConfig localJsonInfo = JsonConvert.DeserializeObject<VersionConfig>(localJsonString);
                    GlobalVariable.shoudUpdateJsonDic.Add("suit", !(serverJsonInfo.suitjson == localJsonInfo.suitjson));
                    GlobalVariable.shoudUpdateJsonDic.Add("glasses", !(serverJsonInfo.glassesjson == localJsonInfo.glassesjson));
                    GlobalVariable.shoudUpdateJsonDic.Add("achieveTitle", !(serverJsonInfo.achievetitlejson == localJsonInfo.achievetitlejson));
                }
                else
                {
                    GlobalVariable.shoudUpdateJsonDic.Add("suit", true);
                    GlobalVariable.shoudUpdateJsonDic.Add("glasses", true);
                    GlobalVariable.shoudUpdateJsonDic.Add("achieveTitle", true);
                }
                var data = new
                {
                    suitjson = serverJsonInfo.suitjson,
                    glassesjson = serverJsonInfo.glassesjson,
                    achievetitlejson = serverJsonInfo.achievetitlejson,
                };
                File.WriteAllText(loaclVersionPath, JsonConvert.SerializeObject(data));
            }
            catch { }
        }

        public static bool LoadTaomeeJson()
        {
            string serverVersionJson = GlobalUtil.GetJsonString(taomeeJsonVersionLink);
            if(serverVersionJson == "")
            {
                return false;
            }
            else
            {
                string localTaomeeVersionJsonPath = folderPath + "localtaomeeversionjson.json";
                try
                {
                    TaomeeVersionDto taomeeServerJsonInfo = JsonConvert.DeserializeObject<TaomeeVersionDto>(serverVersionJson);
                    if (File.Exists(localTaomeeVersionJsonPath))
                    {
                        string localJsonString = File.ReadAllText(localTaomeeVersionJsonPath);
                        TaomeeVersionDto localJsonInfo = JsonConvert.DeserializeObject<TaomeeVersionDto>(localJsonString);

                        GlobalVariable.shoudUpdateJsonDic.Add("pet", !(taomeeServerJsonInfo.files.resource.config.xml.monstersJson == localJsonInfo.files.resource.config.xml.monstersJson));
                        GlobalVariable.shoudUpdateJsonDic.Add("skill", !(taomeeServerJsonInfo.files.resource.config.xml.movesJson == localJsonInfo.files.resource.config.xml.movesJson));
                    }
                    else
                    {
                        GlobalVariable.shoudUpdateJsonDic.Add("pet", true);
                        GlobalVariable.shoudUpdateJsonDic.Add("skill", true);
                    }
                    GlobalVariable.jsonPathDic.Add("pet", taomeeServerJsonInfo.files.resource.config.xml.monstersJson);
                    GlobalVariable.jsonPathDic.Add("skill", taomeeServerJsonInfo.files.resource.config.xml.movesJson);

                    string data = "{\"files\":{\"resource\":{\"config\":{\"xml\":{" + 
                        $"\"monsters.json\":\"{taomeeServerJsonInfo.files.resource.config.xml.monstersJson}\"," +
                        $"\"moves.json\":\"{taomeeServerJsonInfo.files.resource.config.xml.movesJson}\""+
                        "}}}}}";
                    File.WriteAllText(localTaomeeVersionJsonPath, data);
                }
                catch { }
                return true;
            }
            
        }

        public static bool InitAchieveTitleDictionary()
        {
            string link = jsonLink + "achievetitlelist.json";
            string jsonString = GlobalUtil.GetJsonString(link);
            try
            {
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
            string link = jsonLink + "suitlist.json";
            string jsonString = GlobalUtil.GetJsonString(link);
            try
            {
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
            string link = jsonLink + "glasseslist.json";
            string jsonString = GlobalUtil.GetJsonString(link);
            try
            {
                if (jsonString != "")
                {

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
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"目镜json加载失败！！！errorMessage{ex.Message}");
                return false;
            }
        }

        public static bool InitPetDB(string jsonName)
        {
            string link = taomeeJsonLink + jsonName;
            try
            {
                string jsonString = GlobalUtil.GetJsonString(link);
                if (jsonString != "")
                {
                    PetJsonDto info = JsonConvert.DeserializeObject<PetJsonDto>(jsonString);
                    DBController.PetDBController.PetTableTransactionInsertData(info.monsters.monster);
                    Logger.Log("jsonFileInit", "精灵json加载完成。");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"精灵json加载失败！！！errorMessage{ex.Message}");
                return false;
            }
        }

        public static bool InitSkillDB(string jsonName)
        {
            string link = taomeeJsonLink + jsonName;
            try
            {
                string jsonString = GlobalUtil.GetJsonString(link);
                if(jsonString != "")
                {
                    SkillJsonDto info = JsonConvert.DeserializeObject<SkillJsonDto>(jsonString);
                    DBController.SkillDBController.SkillTableTransactionInsertData(info.movesTbl.moves.move);
                    Logger.Log("jsonFileInit", "技能json加载完成。");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"技能json加载失败！！！errorMessage{ex.Message}");
                return false;
            }
        }

        
    }
}
