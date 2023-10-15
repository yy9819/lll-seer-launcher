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
        public static class SuitAndAchieveTitleDbController
        {
            public static bool CheckAndInitDB()
            {
                return DBServise.SuitAndAchieveTitleDbServise.CheckAndInitDB();
            }

            #region
            public static void InitAchieveTitleTable()
            {
                foreach(AchieveTitleInfo info in GlobalVariable.achieveTitleDictionary.Values)
                {
                    DBServise.SuitAndAchieveTitleDbServise.AchieveTitleTableInsertData(info);
                }
                Logger.Log("InitTableData","称号表更新完成!");
            }
            public static bool SetAchieveTitleDic()
            {
                return DBServise.SuitAndAchieveTitleDbServise.AchieveTitleTableAllDataAndSetAchieveTitleDic();
            }
            #endregion
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
            public static int UserTableUpadateData(UserSuitAndAchieveTitleInfo insertData)
            {
                return DBServise.SuitAndAchieveTitleDbServise.UserTableUpdateData(insertData);
            }
            public static Dictionary<int, UserSuitAndAchieveTitleInfo> UserTableSelectDataGetUserClothDic()
            {
                return DBServise.SuitAndAchieveTitleDbServise.UserTableSelectDataGetUserList();
            }
            #endregion
            #region
            public static Dictionary<int,SuitAchieveTitlePlan> GetUserPlan(int userId)
            {
                return DBServise.SuitAndAchieveTitleDbServise.PlanTableSelectData(userId);
            }
            public static Dictionary<int, SuitAchieveTitlePlan> SearchUserPlan(int userId,string searchWord)
            {
                return DBServise.SuitAndAchieveTitleDbServise.PlanTableSearch(userId,searchWord);
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
    }
}
