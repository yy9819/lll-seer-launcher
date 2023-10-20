using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System.IO;
using lll_seer_launcher.core.Utils;
using lll_seer_launcher.core.Dto;

namespace lll_seer_launcher.core.Servise
{
    static class DBServise
    {
        private static string dbPath = Directory.GetCurrentDirectory() + "\\bin\\DB\\";
        private static Dictionary<string, string> dbMap = new Dictionary<string, string>()
            {
                { "suitDB","suit.db" }
            };
        #region
        public static class SuitAndAchieveTitleDbServise
        {
            private static string suitDBPath = dbPath + dbMap["suitDB"];
            private static SqliteConnection db;
            public static bool CheckAndInitDB()
            {
                try
                {
                    Logger.Log("DBInit", "初始化装备数据库...");
                    if (!Directory.Exists(dbPath)) Directory.CreateDirectory(dbPath);
                    if (!File.Exists(suitDBPath))
                    {
                        Logger.Log("CreateDB", "装备数据库不存在！创建数据库...");
                        File.Create(suitDBPath).Close();
                        Logger.Log("CreateDB", "数据库文件创建完成！");
                        Logger.Log("CreateDBTableStart", "开始创建数据库table");
                        using (db = new SqliteConnection($"Filename={suitDBPath}"))
                        {
                            db.Open();
                            createTableSql[] createTableCmds = new createTableSql[5]
                            {
                                new createTableSql("achieve_title", "称号表", "CREATE TABLE achieve_title (id INTEGER PRIMARY KEY AUTOINCREMENT,title_name CHAR(32) NOT NULL," +
                                "title_abtext_text TEXT NOT NULL,title_id INT UNIQUE NOT NULL);"),
                                new createTableSql("suit", "套装表", "CREATE TABLE suit (id INTEGER PRIMARY KEY AUTOINCREMENT,suit_name CHAR(32) NOT NULL," +
                                "suit_desc_text TEXT NOT NULL,suit_id INT UNIQUE NOT NULL,suit_cloth_id TEXT NOT NULL);"),
                                new createTableSql("glasses", "目镜表", "CREATE TABLE glasses (id INTEGER PRIMARY KEY AUTOINCREMENT,glasses_name CHAR(32) NOT NULL," +
                                "glasses_desc_text TEXT NOT NULL,glasses_id INT UNIQUE NOT NULL);"),
                                new createTableSql("user", "用户装备持有明细表", "CREATE TABLE user (id INTEGER PRIMARY KEY AUTOINCREMENT,user_id INT UNIQUE NOT NULL,suit_list TEXT NOT NULL," +
                                "glasses_list TEXT NOT NULL,achieve_title_list TEXT NOT NULL);"),
                                new createTableSql("plan", "方案表", "CREATE TABLE plan (id INTEGER PRIMARY KEY AUTOINCREMENT,plan_name CHAR(32) NOT NULL," +
                                "user_id INT NOT NULL,suit_id INT NOT NULL,glasses_id INT NOT NULL,achieve_title_id INT NOT NULL);")
                            };
                            foreach (var cmd in createTableCmds)
                            {
                                try
                                {
                                    Logger.Log("CrateTableStart", $"正在创建--{cmd.dbTableName}--");
                                    SqliteCommand createTableCmd = new SqliteCommand(cmd.sqlString, db);
                                    createTableCmd.ExecuteNonQuery();
                                    Logger.Log("CrateTableEnd", $"创建--{cmd.dbTableCheneseName}--成功!");
                                }
                                catch (Exception ex)
                                {
                                    Logger.Error($"创建{cmd.dbTableCheneseName}时出错！errorMessage：{ex.Message}");
                                    return false;
                                }
                            }
                        }
                    }
                    else
                    {
                        db = new SqliteConnection($"Filename={suitDBPath}");
                    }
                    Logger.Log("DBInit", "初始化装备数据库完成!!!");
                }
                catch (Exception ex)
                {
                    Logger.Error($"装备数据库加载出错！ errorMessage{ex.Message}");
                }
                return true;
            }
            #region
            /*==========================================称号数据表操作============================================*/
            public static int AchieveTitleTableInsertData(AchieveTitleInfo insertData)
            {
                try
                {
                    using (db)
                    {
                        if(AchieveTitleTableSelectData(insertData.id) != null) return 0;
                        db.Open();
                        string insertSql = $"INSERT INTO achieve_title (title_name,title_abtext_text,title_id) " +
                            $"VALUES('{insertData.title}','{insertData.abtext}',{insertData.id});";
                        SqliteCommand insertCmd = new SqliteCommand(insertSql, db);
                        int value = insertCmd.ExecuteNonQuery();
                        return value;
                    }
                }catch (Exception ex)
                {
                    //if (!ex.Message.Contains("UNIQUE")) return 0;
                    Logger.Error($"数据库称号数据信息插入失败！ errorMessage：{ex.Message}");
                    return -1;
                }
            }

            public static int AchieveTitleTableUpdateData(AchieveTitleInfo updateData)
            {
                try
                {
                    using (db)
                    {
                        db.Open();
                        string updateSql = $"UPDATE achieve_title SET title_name = '{updateData.title}',title_abtext_text = '{updateData.abtext}',title_id = {updateData.id} " +
                            $"WHERE title_id = {updateData.id};";
                        SqliteCommand updateCmd = new SqliteCommand(updateSql, db);
                        int value = updateCmd.ExecuteNonQuery();
                        return value;
                    }
                }catch(Exception ex)
                {
                    Logger.Error($"数据库称号数据信息更新失败！ errorMessage：{ex.Message}");
                    return -1;
                }
            }

            public static AchieveTitleInfo AchieveTitleTableSelectData(int titleId)
            {
                try
                {
                    using (db)
                    {
                        db.Open();
                        string selectSql = $"SELECT title_name,title_abtext_text,title_id " +
                            $"FROM achieve_title WHERE title_id = {titleId};";
                        SqliteCommand selectCmd = new SqliteCommand(selectSql, db);
                        SqliteDataReader reader = selectCmd.ExecuteReader();
                        AchieveTitleInfo result = new AchieveTitleInfo();
                        while (reader.Read())
                        {
                            result.title = reader.GetString(0);
                            result.abtext = reader.GetString(1);
                            result.id = reader.GetInt32(2);
                        }
                        return result.id > 0 ? result : null;
                        
                    }
                }catch (Exception ex)
                {
                    Logger.Error($"数据库称号数据信息查询失败！ errorMessage：{ex.Message}");
                    return null;
                }
            }
            public static bool AchieveTitleTableAllDataAndSetAchieveTitleDic()
            {
                try
                {
                    using (db)
                    {
                        db.Open();
                        string selectSql = $"SELECT title_name,title_abtext_text,title_id " +
                            $"FROM achieve_title;";
                        SqliteCommand selectCmd = new SqliteCommand(selectSql, db);
                        SqliteDataReader reader = selectCmd.ExecuteReader();
                        while (reader.Read())
                        {
                            AchieveTitleInfo result = new AchieveTitleInfo();
                            result.title = reader.GetString(0);
                            result.abtext = reader.GetString(1);
                            result.id = reader.GetInt32(2);
                            GlobalVariable.achieveTitleDictionary.Add(result.id, result);
                        }
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error($"从数据库获取称号数据信息失败！ errorMessage：{ex.Message}");
                    return false;
                }
            }
            #endregion

            #region
            /*==========================================套装数据表操作============================================*/
            public static int SuitTableInsertData(SuitInfo insertData)
            {
                try
                {
                    using (db)
                    {
                        if (SuitTableSelectData(insertData.suitId) != null) return 0;
                        db.Open();
                        string insertSql = $"INSERT INTO suit (suit_name,suit_desc_text,suit_id,suit_cloth_id) " +
                            $"VALUES('{insertData.name}','{insertData.desc}',{insertData.suitId},'{GlobalUtil.IntListToString(insertData.clothIdList)}');";
                        SqliteCommand insertCmd = new SqliteCommand(insertSql, db);
                        int value = insertCmd.ExecuteNonQuery();
                        return value;
                    }
                }catch (Exception ex)
                {
                    Logger.Error($"数据库套装数据信息插入失败！ errorMessage：{ex.Message}");
                    return -1;
                }
            }

            public static int SuitTableUpdateData(SuitInfo updateData)
            {
                try
                {
                    using (db)
                    {
                        db.Open();
                        string updateSql = $"UPDATE suit SET suit_name = '{updateData.name}',suit_desc_text = '{updateData.desc}' ,suit_id = {updateData.suitId}," +
                            $"suit_cloth_id = '{GlobalUtil.IntListToString(updateData.clothIdList)}' " +
                            $"WHERE suit_cloth_id = {updateData.suitId};";
                        SqliteCommand updateCmd = new SqliteCommand(updateSql, db);
                        int value = updateCmd.ExecuteNonQuery();
                        return value;
                    }
                }catch(Exception ex)
                {
                    Logger.Error($"数据库套装数据信息更新失败！ errorMessage：{ex.Message}");
                    return -1;
                }
            }
            public static SuitInfo SuitTableSelectData(int suitId)
            {
                try
                {
                    using (db)
                    {
                        db.Open();
                        string selectSql = $"SELECT suit_name,suit_desc_text,suit_id,suit_cloth_id " +
                            $"FROM suit WHERE suit_id = {suitId};";
                        SqliteCommand selectCmd = new SqliteCommand(selectSql, db);
                        SqliteDataReader reader = selectCmd.ExecuteReader();
                        SuitInfo result = new SuitInfo();
                        while (reader.Read())
                        {
                            result.name = reader.GetString(0);
                            result.desc = reader.GetString(1);
                            result.suitId = reader.GetInt32(2);
                            result.clothIdList = GlobalUtil.StringToIntList(reader.GetString(3));
                        }
                        return result.suitId > 0 ? result : null;
                    }
                }catch (Exception ex)
                {
                    Logger.Error($"数据库套装数据信息查询失败！ errorMessage：{ex.Message}");
                    return null;
                }
            }
            public static bool SuitTableSelectAllDataAndSetSuitDic()
            {
                try
                {
                    using (db)
                    {
                        db.Open();
                        string selectSql = $"SELECT suit_name,suit_desc_text,suit_id,suit_cloth_id " +
                            $"FROM suit;";
                        SqliteCommand selectCmd = new SqliteCommand(selectSql, db);
                        SqliteDataReader reader = selectCmd.ExecuteReader();
                        while (reader.Read())
                        {
                            SuitInfo result = new SuitInfo();
                            result.name = reader.GetString(0);
                            result.desc = reader.GetString(1);
                            result.suitId = reader.GetInt32(2);
                            result.clothIdList = GlobalUtil.StringToIntList(reader.GetString(3));
                            GlobalVariable.suitDictionary.Add(result.suitId,result);
                        }
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error($"从数据库获取套装数据信息失败！ errorMessage：{ex.Message}");
                    return false;
                }
            }
            #endregion

            #region
            /*==========================================目镜数据表操作============================================*/
            public static int GlassesTableInsertData(GlassesInfo insertData)
            {
                try
                {
                    using (db)
                    {
                        if (GlassesTableSelectData(insertData.glassesId) != null) return 0;
                        db.Open();
                        string insertSql = $"INSERT INTO glasses (glasses_name,glasses_desc_text,glasses_id) " +
                            $"VALUES('{insertData.name}','{insertData.desc}',{insertData.glassesId});";
                        SqliteCommand insertCmd = new SqliteCommand(insertSql, db);
                        int value = insertCmd.ExecuteNonQuery();
                        return value;
                    }
                }catch (Exception ex)
                {
                    Logger.Error($"数据库目镜信息插入失败！ errorMessage：{ex.Message}");
                    return -1;
                }
            }

            public static int GlassesTableUpdateData(GlassesInfo updateData)
            {
                try
                {
                    using (db)
                    {
                        db.Open();
                        string updateSql = $"UPDATE glasses SET glasses_name = '{updateData.name}',glasses_desc_text = '{updateData.desc}',glasses_id = {updateData.glassesId} " +
                            $"WHERE glasses_id = {updateData.glassesId};";
                        SqliteCommand updateCmd = new SqliteCommand(updateSql, db);
                        int value = updateCmd.ExecuteNonQuery();
                        return value;
                    }
                }catch(Exception ex)
                {
                    Logger.Error($"数据库目镜信息更新失败！ errorMessage：{ex.Message}");
                    return -1;
                }
            }

            public static GlassesInfo GlassesTableSelectData(int glassesId)
            {
                try
                {
                    using (db)
                    {
                        db.Open();
                        string selectSql = $"SELECT glasses_name,glasses_desc_text,glasses_id " +
                            $"FROM glasses WHERE glasses_id = {glassesId};";
                        SqliteCommand selectCmd = new SqliteCommand(selectSql, db);
                        SqliteDataReader reader = selectCmd.ExecuteReader();
                        GlassesInfo result = new GlassesInfo();
                        while (reader.Read())
                        {
                            result.name = reader.GetString(0);
                            result.desc = reader.GetString(1);
                            result.glassesId = reader.GetInt32(2);
                        }
                        return result.glassesId > 0 ? result : null;
                    }
                }catch (Exception ex)
                {
                    Logger.Error($"数据库目镜信息查询失败！ errorMessage：{ex.Message}");
                    return null;
                }
            }
            public static bool GlassesTableAllDataAndSetGlassesDic()
            {
                try
                {
                    using (db)
                    {
                        db.Open();
                        string selectSql = $"SELECT glasses_name,glasses_desc_text,glasses_id " +
                            $"FROM glasses;";
                        SqliteCommand selectCmd = new SqliteCommand(selectSql, db);
                        SqliteDataReader reader = selectCmd.ExecuteReader();
                        while (reader.Read())
                        {
                            GlassesInfo result = new GlassesInfo();
                            result.name = reader.GetString(0);
                            result.desc = reader.GetString(1);
                            result.glassesId = reader.GetInt32(2);
                            GlobalVariable.glassesDictionary.Add(result.glassesId, result);
                        }
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error($"从数据库获取目镜数据信息失败！ errorMessage：{ex.Message}");
                    return false;
                }
            }
            #endregion
            #region
            /*==========================================用户装备持有明细表操作============================================*/
            public static int UserTableInsertData(UserSuitAndAchieveTitleInfo insertData)
            {
                try
                {
                    using (db)
                    {
                        if(UserTableSelectData(insertData.userId) != null) return 0;
                        db.Open();
                        string achieveTitleListString = GlobalUtil.IntListToString(insertData.achieveTitleIdList);
                        string suitListString = GlobalUtil.IntListToString(insertData.suitIdList);
                        string glassesListString = GlobalUtil.IntListToString(insertData.glassesIdList);

                        string insertSql = $"INSERT INTO user (suit_list,glasses_list,achieve_title_list,user_id) " +
                            $"VALUES('{suitListString}','{glassesListString}','{achieveTitleListString}',{insertData.userId});";
                        SqliteCommand insertCmd = new SqliteCommand(insertSql, db);
                        int value = insertCmd.ExecuteNonQuery();
                        return value;
                    }
                }catch (Exception ex)
                {
                    Logger.Error($"数据库新用户持有[装备・称号]明细表插入失败！ errorMessage：{ex.Message}");
                    return -1;
                }
            }

            public static int UserTableUpdateClothData(UserSuitAndAchieveTitleInfo updateData)
            {
                try
                {
                    using (db)
                    {
                        db.Open();
                        string suitListString = GlobalUtil.IntListToString(updateData.suitIdList);
                        string glassesListString = GlobalUtil.IntListToString(updateData.glassesIdList);

                        string updateSql = $"UPDATE user " +
                            $"SET suit_list = '{suitListString}',glasses_list = '{glassesListString}' " +
                            $"WHERE user_id = {updateData.userId};";
                        SqliteCommand updateCmd = new SqliteCommand(updateSql, db);
                        int value = updateCmd.ExecuteNonQuery();
                        return value;
                    }
                } catch (Exception ex)
                {
                    Logger.Error($"数据库用户持有装备明细表更新失败！ errorMessage：{ex.Message}");
                    return -1;
                }
            }
            public static int UserTableUpdateAchieveTitleData(UserSuitAndAchieveTitleInfo updateData)
            {
                try
                {
                    using (db)
                    {
                        db.Open();
                        string achieveTitleListString = GlobalUtil.IntListToString(updateData.achieveTitleIdList);

                        string updateSql = $"UPDATE user " +
                            $"SET achieve_title_list = '{achieveTitleListString}' " +
                            $"WHERE user_id = {updateData.userId};";
                        SqliteCommand updateCmd = new SqliteCommand(updateSql, db);
                        int value = updateCmd.ExecuteNonQuery();
                        return value;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error($"数据库用户持有称号明细表更新失败！ errorMessage：{ex.Message}");
                    return -1;
                }
            }

            public static UserSuitAndAchieveTitleInfo UserTableSelectData(int userId)
            {
                try
                {
                    using (db)
                    {
                        db.Open();
                        string selectSql = $"SELECT suit_list,glasses_list,achieve_title_list " +
                            $"FROM user WHERE user_id = {userId};";
                        SqliteCommand selectCmd = new SqliteCommand(selectSql, db);
                        SqliteDataReader reader = selectCmd.ExecuteReader();
                        UserSuitAndAchieveTitleInfo result = new UserSuitAndAchieveTitleInfo();
                        result.userId = 0;
                        while (reader.Read())
                        {
                            result.userId = userId;
                            result.suitIdList = GlobalUtil.StringToIntList(reader.GetString(0));
                            result.glassesIdList =  GlobalUtil.StringToIntList(reader.GetString(1));
                            result.achieveTitleIdList = GlobalUtil.StringToIntList(reader.GetString(2));
                        }
                        return result.userId > 0 ? result : null;
                    }
                }catch (Exception ex)
                {
                    Logger.Error($"数据库用户装备持有明细表查询失败！ errorMessage：{ex.Message}");
                    return null;
                }
            }

            public static Dictionary<int, UserSuitAndAchieveTitleInfo> UserTableSelectDataGetUserList()
            {
                Dictionary<int, UserSuitAndAchieveTitleInfo> result = new Dictionary<int, UserSuitAndAchieveTitleInfo>();
                try
                {
                    using (db)
                    {
                        db.Open();
                        string countSql = $"SELECT COUNT(user_id) FROM user;";
                        long userLen = (long)new SqliteCommand(countSql, db).ExecuteScalar();
                        if (userLen == 0) return result;
                        string selectSql = $"SELECT suit_list,glasses_list,achieve_title_list,user_id FROM user;";
                        SqliteCommand selectCmd = new SqliteCommand(selectSql, db);
                        SqliteDataReader reader = selectCmd.ExecuteReader();
                        
                        while (reader.Read())
                        {
                            UserSuitAndAchieveTitleInfo info = new UserSuitAndAchieveTitleInfo();
                            info.suitIdList = GlobalUtil.StringToIntList(reader.GetString(0));
                            info.glassesIdList =  GlobalUtil.StringToIntList(reader.GetString(1));
                            info.achieveTitleIdList = GlobalUtil.StringToIntList(reader.GetString(2));
                            info.userId = reader.GetInt32(3);
                            result.Add(info.userId, info);
                        }
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error($"数据库用户装备持有明细表查询失败！ errorMessage：{ex.Message}");
                    return result;
                }
            }
            #endregion
            #region
            /*==========================================方案表操作============================================*/
            public static int PlanTableInsertData(SuitAchieveTitlePlan insertData)
            {
                try
                {
                    using (db)
                    {
                        db.Open();
                        string insertSql = $"INSERT INTO plan (plan_name,user_id,suit_id,glasses_id,achieve_title_id) " +
                            $"VALUES('{insertData.name}',{insertData.userId},{insertData.suitId},{insertData.glassesId},{insertData.achieveTitleId});";
                        SqliteCommand insertCmd = new SqliteCommand(insertSql, db);
                        int value = insertCmd.ExecuteNonQuery();
                        return value;
                    }
                }catch (Exception ex)
                {
                    Logger.Error($"数据库新建方案插入失败！ errorMessage：{ex.Message}");
                    return -1;
                }
            }

            public static int PlanTableUpdateData(SuitAchieveTitlePlan updateData)
            {
                try
                {
                    using (db)
                    {
                        db.Open();
                        string updateSql = $"UPDATE plan " +
                            $"SET plan_name = '{updateData.name}',suit_id = {updateData.suitId},glasses_id = {updateData.glassesId},achieve_title_id = {updateData.achieveTitleId} " +
                            $"WHERE id = {updateData.id};";
                        SqliteCommand updateCmd = new SqliteCommand(updateSql, db);
                        int value = updateCmd.ExecuteNonQuery();
                        return value;
                    }
                } catch (Exception ex)
                {
                    Logger.Error($"数据库方案更新失败！ errorMessage：{ex.Message}");
                    return -1;
                }
            }
            public static int PlanTableDeleteData(int id)
            {
                try
                {
                    using (db)
                    {
                        db.Open();
                        string deleteSql = $"DELETE FROM plan WHERE id = {id};";
                        int result = new SqliteCommand(deleteSql, db).ExecuteNonQuery();
                        
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error($"数据库方案删除失败！ errorMessage：{ex.Message}");
                    return -1;
                }
            }

            public static int PlanTableDeleteDataByUserId(int userId)
            {
                try
                {
                    using (db)
                    {
                        db.Open();
                        string deleteSql = $"DELETE FROM plan WHERE user_id = {userId};";
                        int result = new SqliteCommand(deleteSql, db).ExecuteNonQuery();

                        return result;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error($"数据库方案删除失败！ errorMessage：{ex.Message}");
                    return -1;
                }
            }

            public static Dictionary<int, SuitAchieveTitlePlan> PlanTableSelectData(int userId)
            {
                try
                {
                    using (db)
                    {
                        db.Open();
                        string countSql = $"SELECT COUNT(plan_name) FROM plan WHERE user_id = {userId};";
                        long planLen = (long)new SqliteCommand(countSql, db).ExecuteScalar();
                        if (planLen == 0) return null;
                        string selectSql = $"SELECT plan_name,suit_id,glasses_id,achieve_title_id,id " +
                            $"FROM plan WHERE user_id = {userId};";
                        SqliteCommand selectCmd = new SqliteCommand(selectSql, db);
                        SqliteDataReader reader = selectCmd.ExecuteReader();
                        Dictionary<int, SuitAchieveTitlePlan> suitAchieveTitlePlans = new Dictionary<int, SuitAchieveTitlePlan>();
                        while (reader.Read())
                        {
                            SuitAchieveTitlePlan result = new SuitAchieveTitlePlan();
                            result.name = reader.GetString(0);
                            result.suitId = reader.GetInt32(1);
                            result.glassesId = reader.GetInt32(2);
                            result.achieveTitleId = reader.GetInt32(3);
                            result.id = reader.GetInt32(4);
                            suitAchieveTitlePlans.Add(result.id,result);
                        }
                        return suitAchieveTitlePlans;
                    }
                } catch (Exception ex)
                {
                    Logger.Error($"数据库方案查询失败！ errorMessage：{ex.Message}");
                    return null;
                }
            }

            public static Dictionary<int, SuitAchieveTitlePlan> PlanTableSearch(int userId,string searchWord)
            {
                try
                {
                    using (db)
                    {
                        db.Open();
                        string countSql = $"SELECT COUNT(plan_name) FROM plan WHERE user_id = {userId};";
                        long planLen = (long)new SqliteCommand(countSql, db).ExecuteScalar();
                        if (planLen == 0) return null;
                        string selectSql = $"SELECT plan_name,suit_id,glasses_id,achieve_title_id,id " +
                            $"FROM plan WHERE plan_name LIKE '%{searchWord}%' AND user_id = {userId};";
                        SqliteCommand selectCmd = new SqliteCommand(selectSql, db);
                        SqliteDataReader reader = selectCmd.ExecuteReader();
                        Dictionary<int, SuitAchieveTitlePlan> suitAchieveTitlePlans = new Dictionary<int, SuitAchieveTitlePlan>();
                        int index = 0;
                        while (reader.Read())
                        {
                            SuitAchieveTitlePlan result = new SuitAchieveTitlePlan();
                            result.name = reader.GetString(0);
                            result.suitId = reader.GetInt32(1);
                            result.glassesId = reader.GetInt32(2);
                            result.achieveTitleId = reader.GetInt32(3);
                            result.id = reader.GetInt32(4);
                            suitAchieveTitlePlans.Add(result.id, result);
                            index++;
                        }
                        return suitAchieveTitlePlans;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error($"数据库方案查询失败！ errorMessage：{ex.Message}");
                    return null;
                }
            }
            #endregion
        }

        private class createTableSql
        {
            public string dbTableName { get; set; }
            public string dbTableCheneseName { get; set; }
            public string sqlString { get; set; }

            public createTableSql(string tableName,string tableCheneseName, string sqlString)
            {
                this.dbTableName = tableName;
                this.dbTableCheneseName = tableCheneseName;
                this.sqlString = sqlString;
            }
        }
        #endregion
    }
}
