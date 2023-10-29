using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lll_seer_launcher.core.Servise.DBServise;
using lll_seer_launcher.core.Dto;
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
                return SuitAndAchieveTitleDbServise.CheckAndInitDB();
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
                SuitAndAchieveTitleDbServise.AchieveTitleTableTransactionInsertData(achieveTitleInfo);
                Logger.Log("InitTableData", "称号表更新完成!");
            }
            public static bool SetAchieveTitleDic()
            {
                return SuitAndAchieveTitleDbServise.AchieveTitleTableAllDataAndSetAchieveTitleDic();
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
                SuitAndAchieveTitleDbServise.SuitTableTransactionInsertData(suitInfo);
                Logger.Log("InitTableData", "装备表更新完成!");
            }
            public static bool SetSuitTitleDic()
            {
                return SuitAndAchieveTitleDbServise.SuitTableSelectAllDataAndSetSuitDic();
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
                SuitAndAchieveTitleDbServise.GlassesTableTransactionInsertData(list);
                Logger.Log("InitTableData", "目镜表更新完成!");
            }
            public static bool SetGlassesTitleDic()
            {
                return SuitAndAchieveTitleDbServise.GlassesTableAllDataAndSetGlassesDic();
            }
            #endregion
            /*==========================================用户装备持有明细表============================================*/
            #region
            public static int UserTableInsertData(UserSuitAndAchieveTitleInfo insertData)
            {
                return SuitAndAchieveTitleDbServise.UserTableInsertData(insertData);
            }
            public static int UserTableUpadateClothData(UserSuitAndAchieveTitleInfo insertData)
            {
                return SuitAndAchieveTitleDbServise.UserTableUpdateClothData(insertData);
            }
            public static int UserTableUpadateAchieveTitleData(UserSuitAndAchieveTitleInfo insertData)
            {
                return SuitAndAchieveTitleDbServise.UserTableUpdateAchieveTitleData(insertData);
            }
            public static Dictionary<int, UserSuitAndAchieveTitleInfo> UserTableSelectDataGetUserClothDic()
            {
                return SuitAndAchieveTitleDbServise.UserTableSelectDataGetUserList();
            }
            public static int UserTableDeleteUser(int userId)
            {
                return SuitAndAchieveTitleDbServise.UserTableDeleteDataByUserId(userId);
            }
            #endregion
            /*==========================================方案明细表============================================*/
            #region
            public static Dictionary<int, SuitAchieveTitlePlan> GetUserPlan(int userId)
            {
                return SuitAndAchieveTitleDbServise.PlanTableSelectData(userId);
            }
            public static Dictionary<int, SuitAchieveTitlePlan> SearchUserPlan(int userId, string searchWord)
            {
                return SuitAndAchieveTitleDbServise.PlanTableSearch(userId, searchWord);
            }
            public static int InsertPlan(SuitAchieveTitlePlan plan)
            {
                return SuitAndAchieveTitleDbServise.PlanTableInsertData(plan);
            }
            public static int DeletePlan(int planId)
            {
                return SuitAndAchieveTitleDbServise.PlanTableDeleteData(planId);
            }
            public static int DeletePlanByuserId(int userId)
            {
                return SuitAndAchieveTitleDbServise.PlanTableDeleteDataByUserId(userId);
            }
            public static int UpdatePlan(SuitAchieveTitlePlan plan)
            {
                return SuitAndAchieveTitleDbServise.PlanTableUpdateData(plan);
            }
            #endregion
        }
        #endregion
        public static class PetDBController
        {
            public static void PetTableTransactionInsertData(List<Pet> pets)
            {
                PetDBServise.PetTableTransactionInsertData(pets);
            }
            /*==========================================精灵明细表============================================*/
            public static bool CheckAndInitDB()
            {
                return PetDBServise.CheckAndInitDB();
            }
            public static int InsertPetData(Pet petInfo)
            {
                return PetDBServise.PetTableInsertData(petInfo);
            }

            public static List<Pet> LikeSearchPetByPetId(int petId)
            {
                return PetDBServise.PetTableLikeSelectDataByPetId(petId);
            }
            public static List<Pet> LikeSearchPetByPetName(string petName)
            {
                return PetDBServise.PetTableSelectDataByPetName(petName);
            }
            public static string SearchPetNameByPetId(int petId)
            {
                return PetDBServise.PetTableSearchPetNameByPetId(petId);
            }

            /*==========================================皮肤明细表============================================*/
            public static int InsertPetSkinsData(PetSkins petSkinsInfo)
            {
                return PetDBServise.PetSkinsTableInsertData(petSkinsInfo);
            }
            public static List<PetSkins> LikeSearchPetSkinsByPetId(int skinsId)
            {
                return PetDBServise.PetSkinsTableLikeSelectDataBySkinsId(skinsId);
            }
            public static List<PetSkins> LikeSearchPetSkinsByPetName(string skinsName)
            {
                return PetDBServise.PetSkinsTableSelectDataBySkinsName(skinsName);
            }
            public static int GetPetRealId(int petId)
            {
                return PetDBServise.PetTableGetRealId(petId);
            }
            /*==========================================方案明细表============================================*/
            public static int AddPlan(PetSkinsReplacePlan planInfo)
            {
                return PetDBServise.PetSkinsPlanTableInsertData(planInfo);
            }
            public static int UpdatePlan(PetSkinsReplacePlan planInfo)
            {
                return PetDBServise.PetSkinsPlanTableUpdateData(planInfo);
            }
            public static int DeletePlan(int petId)
            {
                return PetDBServise.PetSkinsPlanTableDeleteData(petId);
            }
            public static List<PetSkinsReplacePlan> SearchPlan(string petName)
            {
                return PetDBServise.PetSkinsPlanTableSelectData(petName);
            }
            public static int GetPetSkinsRealId(int petId)
            {
                return PetDBServise.PetSkinsTableGetRealId(petId);
            }
        }

        public static class SkillDBController
        {
            public static bool CheckAndInitDB()
            {
                return SkillDBServise.CheckAndInitDB();
            }
            public static void SkillTableTransactionInsertData(List<Move> skillInfo)
            {
                SkillDBServise.SkillTableTransactionInsertData(skillInfo);
            }
            public static int InsertData(Move skillInfo)
            {
                return SkillDBServise.SkillTableInsertData(skillInfo);
            }
            public static string SearchName(int skillId)
            {
                return SkillDBServise.SkillTableSearchSkillNameBySkillId(skillId);
            }
        }
    }
}
