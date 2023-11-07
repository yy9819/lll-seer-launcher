using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lll_seer_launcher.core.Service.DBService;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Dto.PetDto;
using lll_seer_launcher.core.Dto.JSON;
using lll_seer_launcher.core.Utils;

namespace lll_seer_launcher.core.Controller
{
    public class DBController
    {
        /*==========================================装备相关controller============================================*/
        #region
        public class SuitAndAchieveTitleDBController
        {
            public static bool CheckAndInitDB()
            {
                return SuitAndAchieveTitleDbService.CheckAndInitDB();
            }
            /*==========================================称号明细表============================================*/
            #region
            public static void InitAchieveTitleTable()
            {
                List<AchieveTitleInfo> achieveTitleInfo = new List<AchieveTitleInfo>();
                foreach (AchieveTitleInfo info in GlobalVariable.achieveTitleDictionary.Values)
                {
                    achieveTitleInfo.Add(info);
                }
                SuitAndAchieveTitleDbService.AchieveTitleTableTransactionInsertData(achieveTitleInfo);
                Logger.Log("InitTableData", "称号表更新完成!");
            }
            public static bool SetAchieveTitleDic()
            {
                return SuitAndAchieveTitleDbService.AchieveTitleTableAllDataAndSetAchieveTitleDic();
            }
            #endregion
            /*==========================================套装明细表============================================*/
            #region
            public static void InitSuitTable()
            {
                List<SuitInfo> suitInfo = new List<SuitInfo>();
                foreach (SuitInfo info in GlobalVariable.suitDictionary.Values)
                {
                    suitInfo.Add(info);
                }
                SuitAndAchieveTitleDbService.SuitTableTransactionInsertData(suitInfo);
                Logger.Log("InitTableData", "装备表更新完成!");
            }
            public static bool SetSuitTitleDic()
            {
                return SuitAndAchieveTitleDbService.SuitTableSelectAllDataAndSetSuitDic();
            }
            #endregion
            /*==========================================目镜明细表============================================*/
            #region

            public static void InitGlassesTable()
            {
                List<GlassesInfo> list = new List<GlassesInfo>();
                foreach (GlassesInfo info in GlobalVariable.glassesDictionary.Values)
                {
                    list.Add(info);
                }
                SuitAndAchieveTitleDbService.GlassesTableTransactionInsertData(list);
                Logger.Log("InitTableData", "目镜表更新完成!");
            }
            public static bool SetGlassesTitleDic()
            {
                return SuitAndAchieveTitleDbService.GlassesTableAllDataAndSetGlassesDic();
            }
            #endregion
            /*==========================================用户装备持有明细表============================================*/
            #region
            public static int UserTableInsertData(UserSuitAndAchieveTitleInfo insertData)
            {
                return SuitAndAchieveTitleDbService.UserTableInsertData(insertData);
            }
            public static int UserTableUpadateClothData(UserSuitAndAchieveTitleInfo insertData)
            {
                return SuitAndAchieveTitleDbService.UserTableUpdateClothData(insertData);
            }
            public static int UserTableUpadateAchieveTitleData(UserSuitAndAchieveTitleInfo insertData)
            {
                return SuitAndAchieveTitleDbService.UserTableUpdateAchieveTitleData(insertData);
            }
            public static Dictionary<int, UserSuitAndAchieveTitleInfo> UserTableSelectDataGetUserClothDic()
            {
                return SuitAndAchieveTitleDbService.UserTableSelectDataGetUserList();
            }
            public static int UserTableDeleteUser(int userId)
            {
                return SuitAndAchieveTitleDbService.UserTableDeleteDataByUserId(userId);
            }
            #endregion
            /*==========================================方案明细表============================================*/
            #region
            public static Dictionary<int, SuitAchieveTitlePlan> GetUserPlan(int userId)
            {
                return SuitAndAchieveTitleDbService.PlanTableSelectData(userId);
            }
            public static Dictionary<int, SuitAchieveTitlePlan> SearchUserPlan(int userId, string searchWord)
            {
                return SuitAndAchieveTitleDbService.PlanTableSearch(userId, searchWord);
            }
            public static int InsertPlan(SuitAchieveTitlePlan plan)
            {
                return SuitAndAchieveTitleDbService.PlanTableInsertData(plan);
            }
            public static int DeletePlan(int planId)
            {
                return SuitAndAchieveTitleDbService.PlanTableDeleteData(planId);
            }
            public static int DeletePlanByuserId(int userId)
            {
                return SuitAndAchieveTitleDbService.PlanTableDeleteDataByUserId(userId);
            }
            public static int UpdatePlan(SuitAchieveTitlePlan plan)
            {
                return SuitAndAchieveTitleDbService.PlanTableUpdateData(plan);
            }
            #endregion
        }
        #endregion
        public static class PetDBController
        {
            public static void PetTableTransactionInsertData(List<Pet> pets)
            {
                PetDBService.PetTableTransactionInsertData(pets);
            }
            /*==========================================精灵明细表============================================*/
            public static bool CheckAndInitDB()
            {
                return PetDBService.CheckAndInitDB();
            }
            public static int InsertPetData(Pet petInfo)
            {
                return PetDBService.PetTableInsertData(petInfo);
            }

            public static List<Pet> LikeSearchPetByPetId(int petId)
            {
                return PetDBService.PetTableLikeSelectDataByPetId(petId);
            }
            public static List<Pet> LikeSearchPetByPetName(string petName)
            {
                return PetDBService.PetTableSelectDataByPetName(petName);
            }
            public static string SearchPetNameByPetId(int petId)
            {
                return PetDBService.PetTableSearchPetNameByPetId(petId);
            }

            /*==========================================皮肤明细表============================================*/
            public static int InsertPetSkinsData(PetSkins petSkinsInfo)
            {
                return PetDBService.PetSkinsTableInsertData(petSkinsInfo);
            }
            public static List<PetSkins> LikeSearchPetSkinsByPetId(int skinsId)
            {
                return PetDBService.PetSkinsTableLikeSelectDataBySkinsId(skinsId);
            }
            public static List<PetSkins> LikeSearchPetSkinsByPetName(string skinsName)
            {
                return PetDBService.PetSkinsTableSelectDataBySkinsName(skinsName);
            }
            public static int GetPetRealId(int petId)
            {
                return PetDBService.PetTableGetRealId(petId);
            }
            public static int GetPetType(int petId)
            {
                return PetDBService.GetPetType(petId);
            }
            /*==========================================方案明细表============================================*/
            public static int AddPlan(PetSkinsReplacePlan planInfo)
            {
                return PetDBService.PetSkinsPlanTableInsertData(planInfo);
            }
            public static int UpdatePlan(PetSkinsReplacePlan planInfo)
            {
                return PetDBService.PetSkinsPlanTableUpdateData(planInfo);
            }
            public static int DeletePlan(int petId)
            {
                return PetDBService.PetSkinsPlanTableDeleteData(petId);
            }
            public static List<PetSkinsReplacePlan> SearchPlan(string petName)
            {
                return PetDBService.PetSkinsPlanTableSelectData(petName);
            }
            public static int GetPetSkinsRealId(int petId)
            {
                return PetDBService.PetSkinsTableGetRealId(petId);
            }
            /*==========================================背包方案明细表============================================*/
            public static int AddPetBagPlan(PetBagPlan petBagPlan)
            {
                return PetDBService.InsertPetBagPlan(petBagPlan);
            }
            public static int DeletePetBagPlan(int planId)
            {
                return PetDBService.DeletePetBagPlan(planId);
            }
            public static int UpdatePetBagPlan(PetBagPlan petBagPlan)
            {
                return PetDBService.UpdatePetBagPlan(petBagPlan);
            }
            public static List<PetBagPlan> SearchPetPlan(string planName)
            {
                return PetDBService.SearchPetBagPlanByPlanName(planName);
            }
        }

        public static class SkillDBController
        {
            public static bool CheckAndInitDB()
            {
                return SkillDBService.CheckAndInitDB();
            }
            public static void SkillTableTransactionInsertData(List<Move> skillInfo)
            {
                SkillDBService.SkillTableTransactionInsertData(skillInfo);
            }
            public static int InsertData(Move skillInfo)
            {
                return SkillDBService.SkillTableInsertData(skillInfo);
            }
            public static string SearchName(int skillId)
            {
                return SkillDBService.SkillTableSearchSkillNameBySkillId(skillId);
            }

            public static void TypeTableTransactionInsertData(List<TypeItem> types)
            {
                SkillDBService.TypeTableTransactionInsertData(types);
            }
            public static string GetTypeName(int typeId)
            {
                return SkillDBService.GetTypeName(typeId);
            }
        }

        public static class EffectDBController
        {
            public static bool CheckAndInitDB()
            {
                return EffectDBService.CheckAndInitDB();
            }
            public static void PetEffectTableTransactionInsertData(List<PetEffect> insertData)
            {
                EffectDBService.PetEffectTableTransactionInsertData(insertData);
            }
            public static string GetPetEffect(int effectId)
            {
                return EffectDBService.PetEffectTableGetTips(effectId);
            }

            public static void PetNewSeTableTransactionInsertData(List<NewSeIdx> insertData)
            {
                EffectDBService.PetNewSeTableTransactionInsertData(insertData);
            }
            public static NewSeIdx GetNewSeInfo(int eid,string args)
            {
                return EffectDBService.PetNewSeTableGetInfo(eid,args);
            }
        }
    }
}
