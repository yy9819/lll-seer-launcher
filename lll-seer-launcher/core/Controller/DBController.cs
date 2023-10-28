using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lll_seer_launcher.core.Servise.DBServise;
using lll_seer_launcher.core.Dto;
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
                foreach (AchieveTitleInfo info in GlobalVariable.achieveTitleDictionary.Values)
                {
                    SuitAndAchieveTitleDbServise.AchieveTitleTableInsertData(info);
                }
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
                foreach (SuitInfo info in GlobalVariable.suitDictionary.Values)
                {
                    SuitAndAchieveTitleDbServise.SuitTableInsertData(info);
                }
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
                foreach (GlassesInfo info in GlobalVariable.glassesDictionary.Values)
                {
                    SuitAndAchieveTitleDbServise.GlassesTableInsertData(info);
                }
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
        }
    }
}
