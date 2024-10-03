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

        //private static string jsonLink = "http://52.68.134.105/resource/json/";

        public static bool LoadTaomeeJson()
        {
            string serverVersionJson = GlobalUtil.GetJsonString(taomeeJsonVersionLink);
            if(serverVersionJson == "") return false;
            if(!Directory.Exists(folderPath))Directory.CreateDirectory(folderPath);
            string localTaomeeVersionJsonPath = folderPath + "localtaomeeversionjson.json";
            try
            {
                TaomeeVersionDto taomeeServerJsonInfo = JsonConvert.DeserializeObject<TaomeeVersionDto>(serverVersionJson);
            Defalut:
                if (File.Exists(localTaomeeVersionJsonPath))
                {
                    string localJsonString = File.ReadAllText(localTaomeeVersionJsonPath);
                    TaomeeVersionDto localJsonInfo = JsonConvert.DeserializeObject<TaomeeVersionDto>(localJsonString);
                    if(localJsonInfo == null)
                    {
                        File.Delete(localTaomeeVersionJsonPath);
                        goto Defalut;
                    }
                    GlobalVariable.shoudUpdateJsonDic.Add("pet", !(taomeeServerJsonInfo.files.resource.config.xml.monstersJson == localJsonInfo.files.resource.config.xml.monstersJson));
                    GlobalVariable.shoudUpdateJsonDic.Add("skill", !(taomeeServerJsonInfo.files.resource.config.xml.movesJson == localJsonInfo.files.resource.config.xml.movesJson));
                    GlobalVariable.shoudUpdateJsonDic.Add("effectIcon", !(taomeeServerJsonInfo.files.resource.config.xml.effectIconJson == localJsonInfo.files.resource.config.xml.effectIconJson));
                    GlobalVariable.shoudUpdateJsonDic.Add("newSe", !(taomeeServerJsonInfo.files.resource.config.xml.newSeJson == localJsonInfo.files.resource.config.xml.newSeJson));
                    GlobalVariable.shoudUpdateJsonDic.Add("skillType", !(taomeeServerJsonInfo.files.resource.config.xml.skillType == localJsonInfo.files.resource.config.xml.skillType));
                    GlobalVariable.shoudUpdateJsonDic.Add("achievements", !(taomeeServerJsonInfo.files.resource.config.xml.achievements == localJsonInfo.files.resource.config.xml.achievements));
                    GlobalVariable.shoudUpdateJsonDic.Add("equip", !(taomeeServerJsonInfo.files.resource.config.xml.equip == localJsonInfo.files.resource.config.xml.equip));
                    GlobalVariable.shoudUpdateJsonDic.Add("suit", !(taomeeServerJsonInfo.files.resource.config.xml.suit == localJsonInfo.files.resource.config.xml.suit));
                    if (taomeeServerJsonInfo.files.resource.config.xml.moveStones == "" || taomeeServerJsonInfo.files.resource.config.xml.moveStones == null) taomeeServerJsonInfo.files.resource.config.xml.moveStones = "move_stones.json";
                    GlobalVariable.shoudUpdateJsonDic.Add("move_stones", !(taomeeServerJsonInfo.files.resource.config.xml.moveStones == localJsonInfo.files.resource.config.xml.moveStones));
                }
                else
                {
                    GlobalVariable.shoudUpdateJsonDic.Add("pet", true);
                    GlobalVariable.shoudUpdateJsonDic.Add("skill", true);
                    GlobalVariable.shoudUpdateJsonDic.Add("effectIcon", true);
                    GlobalVariable.shoudUpdateJsonDic.Add("newSe", true);
                    GlobalVariable.shoudUpdateJsonDic.Add("skillType", true);
                    GlobalVariable.shoudUpdateJsonDic.Add("achievements", true);
                    GlobalVariable.shoudUpdateJsonDic.Add("equip", true);
                    GlobalVariable.shoudUpdateJsonDic.Add("suit", true);
                    GlobalVariable.shoudUpdateJsonDic.Add("move_stones", true);
                }
                GlobalVariable.jsonPathDic.Add("pet", taomeeServerJsonInfo.files.resource.config.xml.monstersJson);
                GlobalVariable.jsonPathDic.Add("skill", taomeeServerJsonInfo.files.resource.config.xml.movesJson);
                GlobalVariable.jsonPathDic.Add("effectIcon", taomeeServerJsonInfo.files.resource.config.xml.effectIconJson);
                GlobalVariable.jsonPathDic.Add("newSe", taomeeServerJsonInfo.files.resource.config.xml.newSeJson);
                GlobalVariable.jsonPathDic.Add("skillType", taomeeServerJsonInfo.files.resource.config.xml.skillType);
                GlobalVariable.jsonPathDic.Add("achievements", taomeeServerJsonInfo.files.resource.config.xml.achievements);
                GlobalVariable.jsonPathDic.Add("equip", taomeeServerJsonInfo.files.resource.config.xml.equip);
                GlobalVariable.jsonPathDic.Add("suit", taomeeServerJsonInfo.files.resource.config.xml.suit);
                GlobalVariable.jsonPathDic.Add("move_stones", taomeeServerJsonInfo.files.resource.config.xml.moveStones);
                
                string data = "{\"files\":{\"resource\":{\"config\":{\"xml\":{" +
                    $"\"monsters.json\":\"{taomeeServerJsonInfo.files.resource.config.xml.monstersJson}\"," +
                    $"\"moves.json\":\"{taomeeServerJsonInfo.files.resource.config.xml.movesJson}\","+
                    $"\"effectIcon.json\":\"{taomeeServerJsonInfo.files.resource.config.xml.effectIconJson}\","+
                    $"\"new_se.json\":\"{taomeeServerJsonInfo.files.resource.config.xml.newSeJson}\","+
                    $"\"skillTypes.json\":\"{taomeeServerJsonInfo.files.resource.config.xml.skillType}\","+
                    $"\"achievements.json\":\"{taomeeServerJsonInfo.files.resource.config.xml.achievements}\","+
                    $"\"equip.json\":\"{taomeeServerJsonInfo.files.resource.config.xml.equip}\","+
                    $"\"suit.json\":\"{taomeeServerJsonInfo.files.resource.config.xml.suit}\","+
                    $"\"move_stones.json\":\"{taomeeServerJsonInfo.files.resource.config.xml.moveStones}\""+
                    "}}}}}";
                File.WriteAllText(localTaomeeVersionJsonPath, data);
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.ToString());
            }
            return true;
        }

        public static bool InitAchieveTitleDictionary(string jsonName)
        {
            string link = taomeeJsonLink + jsonName;
            string jsonString = GlobalUtil.GetJsonString(link);
            try
            {
                AchieveTitleJsonObject info = JsonConvert.DeserializeObject<AchieveTitleJsonObject>(jsonString);
                Dictionary<int, AchieveTitleInfo> achieveDic = new Dictionary<int, AchieveTitleInfo>();
                foreach (var type in info.achievementRules.type)
                {
                    foreach(var branches in type.branches)
                    {
                        foreach(var branch in branches.branch)
                        {
                            foreach (var achieve in branch.rule)
                            {
                                if (achieve.abtext != "") achieveDic.Add(achieve.id, achieve);
                            }
                        }
                    }
                }
                GlobalVariable.achieveTitleDictionary = achieveDic;
                Logger.Log("jsonFileInit", "称号json加载完成。");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"称号json加载失败！！！errorMessage:{ex.Message}");
                return false;
            }
        }

        public static bool InitPetDB(string jsonName)
        {
            string link = taomeeJsonLink + jsonName;
            try
            {
                string jsonString = GlobalUtil.GetJsonString(link);
                if (jsonString == "") return false;
                PetJsonDto info = JsonConvert.DeserializeObject<PetJsonDto>(jsonString);
                DBController.PetDBController.PetTableTransactionInsertData(info.monsters.monster);
                Logger.Log("jsonFileInit", "精灵json加载完成。");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"精灵json加载失败！！！errorMessage{ex.Message}");
                return false;
            }
        }

        public static bool InitPetEffectDB(string jsonName)
        {
            string link = taomeeJsonLink + jsonName;
            try
            {
                string jsonString = GlobalUtil.GetJsonString(link);
                if (jsonString == "") return false;
                PetEffectDto info = JsonConvert.DeserializeObject<PetEffectDto>(jsonString);
                DBController.EffectDBController.PetEffectTableTransactionInsertData(info.root.effect);
                Logger.Log("jsonFileInit", "魂印json加载完成。");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"魂印json加载失败！！！errorMessage:{ex.Message}");
                return false;
            }
        }

        public static bool InitNewSeDB(string jsonName)
        {
            string link = taomeeJsonLink + jsonName;
            try
            {
                string jsonString = GlobalUtil.GetJsonString(link);
                if (jsonString == "") return false;
                PetNewSeDto info = JsonConvert.DeserializeObject<PetNewSeDto>(jsonString);
                DBController.EffectDBController.PetNewSeTableTransactionInsertData(info.newSe.newSeIdx);
                Logger.Log("jsonFileInit", "特性json加载完成。");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"特性json加载失败！！！errorMessage:{ex.Message}");
                return false;
            }
        }

        public static bool InitSkillDB(string jsonName)
        {
            string link = taomeeJsonLink + jsonName;
            try
            {
                string jsonString = GlobalUtil.GetJsonString(link);
                if(jsonString == "") return false;
                SkillJsonDto info = JsonConvert.DeserializeObject<SkillJsonDto>(jsonString);
                DBController.SkillDBController.SkillTableTransactionInsertData(info.movesTbl.moves.move);
                Logger.Log("jsonFileInit", "技能json加载完成。");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"技能json加载失败！！！errorMessage:{ex.Message}");
                return false;
            }
        }

        public static bool InitTypeDB(string jsonName)
        {
            string link = taomeeJsonLink + jsonName;
            try
            {
                string jsonString = GlobalUtil.GetJsonString(link);
                if (jsonString == "") return false;
                TypeDto info = JsonConvert.DeserializeObject<TypeDto>(jsonString);
                DBController.SkillDBController.TypeTableTransactionInsertData(info.root.item);
                Logger.Log("jsonFileInit", "技能属性json加载完成。");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"技能属性json加载失败！！！errorMessage:{ex.Message}");
                return false;
            }
        }

        public static bool InitMoveStones(string jsonName)
        {
            string link = taomeeJsonLink + jsonName;
            try
            {
                string jsonString = GlobalUtil.GetJsonString(link);
                if (jsonString == "") return false;
                MoveStonesJsonDto info = JsonConvert.DeserializeObject<MoveStonesJsonDto>(jsonString);
                for(int i = 0; i < info.moveStones.moveStone.Count; i++)
                {
                    info.moveStones.moveStone[i].id += 200000;
                }
                DBController.SkillDBController.SkillTableTransactionInsertData(info.moveStones.moveStone);
                Logger.Log("jsonFileInit", "技能json加载完成。");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"技能属性json加载失败！！！errorMessage:{ex.Message}");
                return false;
            }
        }

        public static bool InitEquipAndSuit(string equipJsonPath,string suitJsonPath)
        {
            string equiplink = taomeeJsonLink + equipJsonPath;
            string suitlink = taomeeJsonLink + suitJsonPath;
            try
            {
                string euqipJsonString = GlobalUtil.GetJsonString(equiplink);
                string suitJsonString = GlobalUtil.GetJsonString(suitlink);
                if (euqipJsonString == "" || suitJsonString == "") return false;
                ClothDto equipJSON = JsonConvert.DeserializeObject<ClothDto>(euqipJsonString);
                SuitJSON suitJSON = JsonConvert.DeserializeObject<SuitJSON>(suitJsonString);
                Dictionary<int,SuitItem> suitItemDic = new Dictionary<int, SuitItem>();

                Dictionary<int,GlassesInfo> glasses = new Dictionary<int,GlassesInfo>();
                Dictionary<int,SuitInfo> suitDic = new Dictionary<int,SuitInfo>();
                foreach (var suit in suitJSON.root.item)
                {
                    suitItemDic.Add(suit.id, suit);
                }
                foreach(var euip in equipJSON.equips.equip)
                {
                    if(euip.part == 1 && euip.suitId == 0) 
                    {
                        GlassesInfo glassesInfo = new GlassesInfo();
                        glassesInfo.name = euip.name;
                        glassesInfo.glassesId = euip.itemId;
                        glassesInfo.desc = euip.desc;
                        glasses.Add(glassesInfo.glassesId,glassesInfo);
                    }else if(euip.suitId != 0)
                    {
                        if (!suitDic.ContainsKey(euip.suitId))
                        {
                            SuitInfo suitInfo = new SuitInfo();
                            suitInfo.desc = euip.desc;
                            suitInfo.suitId = euip.suitId;
                            suitInfo.name = suitItemDic[euip.suitId].name;
                            string[] clothsString = suitItemDic[euip.suitId].cloths.Split(' ');
                            List<int> clothsInt = new List<int>();
                            foreach(string cloth in clothsString) clothsInt.Add(int.Parse(cloth));
                            suitInfo.clothIdList= clothsInt;
                            suitDic.Add(suitInfo.suitId, suitInfo);
                        }
                    }
                }
                GlobalVariable.glassesDictionary = glasses;
                GlobalVariable.suitDictionary = suitDic;
                DBController.SuitAndAchieveTitleDBController.InitSuitTable();
                DBController.SuitAndAchieveTitleDBController.InitGlassesTable();
                Logger.Log("jsonFileInit", "套装json加载完成。");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"套装json加载失败！！！errorMessage:{ex.Message}");
                return false;
            }
        }
    }
}
