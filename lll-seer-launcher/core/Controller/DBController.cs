using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lll_seer_launcher.core.Servise;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Utils;

namespace lll_seer_launcher.core.Controller
{
    public class DBController
    {
        /*==========================================装备相关controller============================================*/
        #region
        public static class SuitAndAchieveTitleDBController
        {
            public static bool CheckAndInitDB()
            {
                return DBServise.SuitAndAchieveTitleDbServise.CheckAndInitDB();
            }
            /*==========================================称号明细表============================================*/
            #region
            public static void InitAchieveTitleTable()
            {
                foreach (AchieveTitleInfo info in GlobalVariable.achieveTitleDictionary.Values)
                {
                    DBServise.SuitAndAchieveTitleDbServise.AchieveTitleTableInsertData(info);
                }
                Logger.Log("InitTableData", "称号表更新完成!");
            }
            public static bool SetAchieveTitleDic()
            {
                return DBServise.SuitAndAchieveTitleDbServise.AchieveTitleTableAllDataAndSetAchieveTitleDic();
            }
            #endregion
            /*==========================================套装明细表============================================*/
            #region
            public static void InitSuitTable()
            {
                foreach (SuitInfo info in GlobalVariable.suitDictionary.Values)
                {
                    DBServise.SuitAndAchieveTitleDbServise.SuitTableInsertData(info);
                }
                Logger.Log("InitTableData", "装备表更新完成!");
            }
            public static bool SetSuitTitleDic()
            {
                return DBServise.SuitAndAchieveTitleDbServise.SuitTableSelectAllDataAndSetSuitDic();
            }
            #endregion
            /*==========================================目镜明细表============================================*/
            #region
            public static void InitGlassesTable()
            {
                foreach (GlassesInfo info in GlobalVariable.glassesDictionary.Values)
                {
                    DBServise.SuitAndAchieveTitleDbServise.GlassesTableInsertData(info);
                }
                Logger.Log("InitTableData", "目镜表更新完成!");
            }
            public static bool SetGlassesTitleDic()
            {
                return DBServise.SuitAndAchieveTitleDbServise.GlassesTableAllDataAndSetGlassesDic();
            }
            #endregion
            /*==========================================用户装备持有明细表============================================*/
            #region
            public static int UserTableInsertData(UserSuitAndAchieveTitleInfo insertData)
            {
                return DBServise.SuitAndAchieveTitleDbServise.UserTableInsertData(insertData);
            }
            public static int UserTableUpadateClothData(UserSuitAndAchieveTitleInfo insertData)
            {
                return DBServise.SuitAndAchieveTitleDbServise.UserTableUpdateClothData(insertData);
            }
            public static int UserTableUpadateAchieveTitleData(UserSuitAndAchieveTitleInfo insertData)
            {
                return DBServise.SuitAndAchieveTitleDbServise.UserTableUpdateAchieveTitleData(insertData);
            }
            public static Dictionary<int, UserSuitAndAchieveTitleInfo> UserTableSelectDataGetUserClothDic()
            {
                return DBServise.SuitAndAchieveTitleDbServise.UserTableSelectDataGetUserList();
            }
            public static int UserTableDeleteUser(int userId)
            {
                return DBServise.SuitAndAchieveTitleDbServise.UserTableDeleteDataByUserId(userId);
            }
            #endregion
            /*==========================================方案明细表============================================*/
            #region
            public static Dictionary<int, SuitAchieveTitlePlan> GetUserPlan(int userId)
            {
                return DBServise.SuitAndAchieveTitleDbServise.PlanTableSelectData(userId);
            }
            public static Dictionary<int, SuitAchieveTitlePlan> SearchUserPlan(int userId, string searchWord)
            {
                return DBServise.SuitAndAchieveTitleDbServise.PlanTableSearch(userId, searchWord);
            }
            public static int InsertPlan(SuitAchieveTitlePlan plan)
            {
                return DBServise.SuitAndAchieveTitleDbServise.PlanTableInsertData(plan);
            }
            public static int DeletePlan(int planId)
            {
                return DBServise.SuitAndAchieveTitleDbServise.PlanTableDeleteData(planId);
            }
            public static int DeletePlanByuserId(int userId)
            {
                return DBServise.SuitAndAchieveTitleDbServise.PlanTableDeleteDataByUserId(userId);
            }
            public static int UpdatePlan(SuitAchieveTitlePlan plan)
            {
                return DBServise.SuitAndAchieveTitleDbServise.PlanTableUpdateData(plan);
            }
            #endregion
        }
        #endregion
        public static class PetDBController
        {
            public static bool CheckAndInitDB()
            {
                return DBServise.PetDBServise.CheckAndInitDB();
            }
            public static int InsertPetData(Pet petInfo)
            {
                return DBServise.PetDBServise.PetTableInsertData(petInfo);
            }

            public static List<Pet> LikeSearchPetByPetId(int petId)
            {
                return DBServise.PetDBServise.PetTableLikeSelectDataByPetId(petId);
            }
            public static List<Pet> LikeSearchPetByPetName(string petName)
            {
                return DBServise.PetDBServise.PetTableSelectDataByPetName(petName);
            }
            public static int InsertPetSkinsData(PetSkins petSkinsInfo)
            {
                return DBServise.PetDBServise.PetSkinsTableInsertData(petSkinsInfo);
            }
            public static List<PetSkins> LikeSearchPetSkinsByPetId(int skinsId)
            {
                return DBServise.PetDBServise.PetSkinsTableLikeSelectDataBySkinsId(skinsId);
            }
            public static List<PetSkins> LikeSearchPetSkinsByPetName(string skinsName)
            {
                return DBServise.PetDBServise.PetSkinsTableSelectDataBySkinsName(skinsName);
            }
            public static int AddPlan(PetSkinsReplacePlan planInfo)
            {
                return DBServise.PetDBServise.PetSkinsPlanTableInsertData(planInfo);
            }
            public static int UpdatePlan(PetSkinsReplacePlan planInfo)
            {
                return DBServise.PetDBServise.PetSkinsPlanTableUpdateData(planInfo);
            }
            public static int DeletePlan(int petId)
            {
                return DBServise.PetDBServise.PetSkinsPlanTableDeleteData(petId);
            }
            public static List<PetSkinsReplacePlan> SearchPlan(string petName)
            {
                return DBServise.PetDBServise.PetSkinsPlanTableSelectData(petName);
            }
        }
    }
}
