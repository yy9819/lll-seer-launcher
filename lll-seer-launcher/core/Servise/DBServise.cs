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
                { "suitDB","suit.db" }, 
                { "petDB","pet.db" }
            };
        #region
        /*==========================================装备相关Servise============================================*/
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
                        if (AchieveTitleTableSelectData(insertData.id) != null) return 0;
                        db.Open();
                        string insertSql = "INSERT INTO achieve_title (title_name,title_abtext_text,title_id) " +
                            "VALUES(@title,@abtext,@id);";
                        SqliteCommand insertCmd = new SqliteCommand(insertSql, db);
                        insertCmd.Parameters.Add(new SqliteParameter("@title", insertData.title));
                        insertCmd.Parameters.Add(new SqliteParameter("@abtext", insertData.abtext));
                        insertCmd.Parameters.Add(new SqliteParameter("@id", insertData.id));
                        int value = insertCmd.ExecuteNonQuery();
                        return value;
                    }
                }
                catch (Exception ex)
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
                        string updateSql = "UPDATE achieve_title SET title_name = @title,title_abtext_text = @abtext,title_id = @id " +
                            "WHERE title_id = @id;";
                        SqliteCommand updateCmd = new SqliteCommand(updateSql, db);
                        updateCmd.Parameters.Add(new SqliteParameter("@title", updateData.title));
                        updateCmd.Parameters.Add(new SqliteParameter("@abtext", updateData.abtext));
                        updateCmd.Parameters.Add(new SqliteParameter("@id", updateData.id));
                        int value = updateCmd.ExecuteNonQuery();
                        return value;
                    }
                }
                catch (Exception ex)
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
                        string selectSql = "SELECT title_name,title_abtext_text,title_id " +
                            "FROM achieve_title WHERE title_id = @id;";
                        SqliteCommand selectCmd = new SqliteCommand(selectSql, db);
                        selectCmd.Parameters.Add(new SqliteParameter("@id", titleId));
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
                }
                catch (Exception ex)
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
                        string selectSql = "SELECT title_name,title_abtext_text,title_id " +
                            "FROM achieve_title;";
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
                        string insertSql = "INSERT INTO suit (suit_name,suit_desc_text,suit_id,suit_cloth_id) " +
                            "VALUES(@name,@desc,@suitId,@clothIdList);";
                        SqliteCommand insertCmd = new SqliteCommand(insertSql, db);
                        insertCmd.Parameters.Add(new SqliteParameter("@suitId", insertData.suitId));
                        insertCmd.Parameters.Add(new SqliteParameter("@name", insertData.name));
                        insertCmd.Parameters.Add(new SqliteParameter("@desc", insertData.desc));
                        insertCmd.Parameters.Add(new SqliteParameter("@clothIdList", GlobalUtil.IntListToString(insertData.clothIdList)));
                        int value = insertCmd.ExecuteNonQuery();
                        return value;
                    }
                }
                catch (Exception ex)
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
                        string updateSql = "UPDATE suit SET suit_name = @name,suit_desc_text = @desc ,suit_id = @suitId," +
                            "suit_cloth_id = @clothIdList " +
                            "WHERE suit_cloth_id = @suitId;";
                        SqliteCommand updateCmd = new SqliteCommand(updateSql, db);
                        updateCmd.Parameters.Add(new SqliteParameter("@suitId", updateData.suitId));
                        updateCmd.Parameters.Add(new SqliteParameter("@name", updateData.name));
                        updateCmd.Parameters.Add(new SqliteParameter("@desc", updateData.desc));
                        updateCmd.Parameters.Add(new SqliteParameter("@clothIdList", GlobalUtil.IntListToString(updateData.clothIdList)));
                        int value = updateCmd.ExecuteNonQuery();
                        return value;
                    }
                }
                catch (Exception ex)
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
                        string selectSql = "SELECT suit_name,suit_desc_text,suit_id,suit_cloth_id " +
                            "FROM suit WHERE suit_id = @suitId;";
                        SqliteCommand selectCmd = new SqliteCommand(selectSql, db);
                        selectCmd.Parameters.Add(new SqliteParameter("@suitId", suitId));
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
                }
                catch (Exception ex)
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
                        string selectSql = "SELECT suit_name,suit_desc_text,suit_id,suit_cloth_id " +
                            "FROM suit;";
                        SqliteCommand selectCmd = new SqliteCommand(selectSql, db);
                        SqliteDataReader reader = selectCmd.ExecuteReader();
                        while (reader.Read())
                        {
                            SuitInfo result = new SuitInfo();
                            result.name = reader.GetString(0);
                            result.desc = reader.GetString(1);
                            result.suitId = reader.GetInt32(2);
                            result.clothIdList = GlobalUtil.StringToIntList(reader.GetString(3));
                            GlobalVariable.suitDictionary.Add(result.suitId, result);
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
                        string insertSql = "INSERT INTO glasses (glasses_name,glasses_desc_text,glasses_id) " +
                            "VALUES(@name,@desc,@glassesId);";
                        SqliteCommand insertCmd = new SqliteCommand(insertSql, db);
                        insertCmd.Parameters.Add(new SqliteParameter("@name", insertData.name));
                        insertCmd.Parameters.Add(new SqliteParameter("@desc", insertData.desc));
                        insertCmd.Parameters.Add(new SqliteParameter("@glassesId", insertData.glassesId));
                        int value = insertCmd.ExecuteNonQuery();
                        return value;
                    }
                }
                catch (Exception ex)
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
                        string updateSql = "UPDATE glasses SET glasses_name = @name,glasses_desc_text = @desc,glasses_id = @glassesId " +
                            "WHERE glasses_id = @glassesId;";
                        SqliteCommand updateCmd = new SqliteCommand(updateSql, db);
                        updateCmd.Parameters.Add(new SqliteParameter("@name", updateData.name));
                        updateCmd.Parameters.Add(new SqliteParameter("@desc", updateData.desc));
                        updateCmd.Parameters.Add(new SqliteParameter("@glassesId", updateData.glassesId));
                        int value = updateCmd.ExecuteNonQuery();
                        return value;
                    }
                }
                catch (Exception ex)
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
                        string selectSql = "SELECT glasses_name,glasses_desc_text,glasses_id " +
                            "FROM glasses WHERE glasses_id = @glassesId;";
                        SqliteCommand selectCmd = new SqliteCommand(selectSql, db);
                        selectCmd.Parameters.Add(new SqliteParameter("@glassesId", glassesId));
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
                }
                catch (Exception ex)
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
                        string selectSql = "SELECT glasses_name,glasses_desc_text,glasses_id " +
                            "FROM glasses;";
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
                        if (UserTableSelectData(insertData.userId) != null) return 0;
                        db.Open();
                        string achieveTitleListString = GlobalUtil.IntListToString(insertData.achieveTitleIdList);
                        string suitListString = GlobalUtil.IntListToString(insertData.suitIdList);
                        string glassesListString = GlobalUtil.IntListToString(insertData.glassesIdList);

                        string insertSql = "INSERT INTO user (suit_list,glasses_list,achieve_title_list,user_id) " +
                            "VALUES(@suitListString,@glassesListString,@achieveTitleListString,@userId);";
                        SqliteCommand insertCmd = new SqliteCommand(insertSql, db);
                        insertCmd.Parameters.Add(new SqliteParameter("@suitListString", suitListString));
                        insertCmd.Parameters.Add(new SqliteParameter("@glassesListString", glassesListString));
                        insertCmd.Parameters.Add(new SqliteParameter("@achieveTitleListString", achieveTitleListString));
                        insertCmd.Parameters.Add(new SqliteParameter("@userId", insertData.userId));
                        int value = insertCmd.ExecuteNonQuery();
                        return value;
                    }
                }
                catch (Exception ex)
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

                        string updateSql = "UPDATE user " +
                            "SET suit_list = @suitListString,glasses_list = @glassesListString " +
                            "WHERE user_id = @userId;";
                        SqliteCommand updateCmd = new SqliteCommand(updateSql, db);
                        updateCmd.Parameters.Add(new SqliteParameter("@suitListString", suitListString));
                        updateCmd.Parameters.Add(new SqliteParameter("@glassesListString", glassesListString));
                        updateCmd.Parameters.Add(new SqliteParameter("@userId", updateData.userId));
                        int value = updateCmd.ExecuteNonQuery();
                        return value;
                    }
                }
                catch (Exception ex)
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

                        string updateSql = "UPDATE user " +
                            "SET achieve_title_list = @achieveTitleListString " +
                            "WHERE user_id = @userId;";
                        SqliteCommand updateCmd = new SqliteCommand(updateSql, db);
                        updateCmd.Parameters.Add(new SqliteParameter("@achieveTitleListString", achieveTitleListString));
                        updateCmd.Parameters.Add(new SqliteParameter("@userId", updateData.userId));
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
                        string selectSql = "SELECT suit_list,glasses_list,achieve_title_list FROM user WHERE user_id = @userId;";
                        SqliteCommand selectCmd = new SqliteCommand(selectSql, db);
                        selectCmd.Parameters.Add(new SqliteParameter("@userId", userId));
                        SqliteDataReader reader = selectCmd.ExecuteReader();
                        UserSuitAndAchieveTitleInfo result = new UserSuitAndAchieveTitleInfo();
                        result.userId = 0;
                        while (reader.Read())
                        {
                            result.userId = userId;
                            result.suitIdList = reader.GetString(0) == "" ? new List<int>() : GlobalUtil.StringToIntList(reader.GetString(0));
                            result.glassesIdList =  reader.GetString(1) == "" ? new List<int>() : GlobalUtil.StringToIntList(reader.GetString(1));
                            result.achieveTitleIdList = reader.GetString(2) == "" ? new List<int>() : GlobalUtil.StringToIntList(reader.GetString(2));
                        }
                        return result.userId > 0 ? result : null;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error($"数据库用户[{userId}]装备持有明细表查询失败！ errorMessage：{ex.Message}");
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
                        string countSql = "SELECT COUNT(user_id) FROM user;";
                        long userLen = (long)new SqliteCommand(countSql, db).ExecuteScalar();
                        if (userLen == 0) return result;
                        string selectSql = "SELECT suit_list,glasses_list,achieve_title_list,user_id FROM user;";
                        SqliteCommand selectCmd = new SqliteCommand(selectSql, db);
                        SqliteDataReader reader = selectCmd.ExecuteReader();

                        while (reader.Read())
                        {
                            UserSuitAndAchieveTitleInfo info = new UserSuitAndAchieveTitleInfo();
                            info.suitIdList = reader.GetString(0) == "" ? new List<int>() : GlobalUtil.StringToIntList(reader.GetString(0));
                            info.glassesIdList = reader.GetString(1) == "" ? new List<int>() : GlobalUtil.StringToIntList(reader.GetString(1));
                            info.achieveTitleIdList = reader.GetString(2) == "" ? new List<int>() : GlobalUtil.StringToIntList(reader.GetString(2));
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
            public static int UserTableDeleteDataByUserId(int userId)
            {
                try
                {
                    using (db)
                    {
                        db.Open();
                        string deleteSql = "DELETE FROM user WHERE user_id = @userId;";
                        SqliteCommand deleteCmd = new SqliteCommand(deleteSql, db);
                        deleteCmd.Parameters.Add(new SqliteParameter("@userId", userId));
                        int result = deleteCmd.ExecuteNonQuery();

                        return result;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error($"数据库账号持有信息删除失败！ errorMessage：{ex.Message}");
                    return -1;
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
                        string insertSql = "INSERT INTO plan (plan_name,user_id,suit_id,glasses_id,achieve_title_id) " +
                            "VALUES(@name,@userId,@suitId,@glassesId,@achieveTitleId);";
                        SqliteCommand insertCmd = new SqliteCommand(insertSql, db);
                        insertCmd.Parameters.Add(new SqliteParameter("@name", insertData.name));
                        insertCmd.Parameters.Add(new SqliteParameter("@userId", insertData.userId));
                        insertCmd.Parameters.Add(new SqliteParameter("@suitId", insertData.suitId));
                        insertCmd.Parameters.Add(new SqliteParameter("@glassesId", insertData.glassesId));
                        insertCmd.Parameters.Add(new SqliteParameter("@achieveTitleId", insertData.achieveTitleId));
                        int value = insertCmd.ExecuteNonQuery();
                        return value;
                    }
                }
                catch (Exception ex)
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
                        string updateSql = "UPDATE plan " +
                            "SET plan_name = @name,suit_id = @suitId,glasses_id = @glassesId,achieve_title_id = @achieveTitleId " +
                            "WHERE id = @id;";
                        SqliteCommand updateCmd = new SqliteCommand(updateSql, db);
                        updateCmd.Parameters.Add(new SqliteParameter("@name", updateData.name));
                        updateCmd.Parameters.Add(new SqliteParameter("@userId", updateData.userId));
                        updateCmd.Parameters.Add(new SqliteParameter("@suitId", updateData.suitId));
                        updateCmd.Parameters.Add(new SqliteParameter("@glassesId", updateData.glassesId));
                        updateCmd.Parameters.Add(new SqliteParameter("@achieveTitleId", updateData.achieveTitleId));
                        int value = updateCmd.ExecuteNonQuery();
                        return value;
                    }
                }
                catch (Exception ex)
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
                        string deleteSql = "DELETE FROM plan WHERE id = @id;";
                        SqliteCommand deleteCmd = new SqliteCommand(deleteSql, db);
                        deleteCmd.Parameters.Add(new SqliteParameter("@id", id));
                        int result = deleteCmd.ExecuteNonQuery();

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
                        string deleteSql = "DELETE FROM plan WHERE user_id = @userId;";
                        SqliteCommand deleteCmd = new SqliteCommand(deleteSql, db);
                        deleteCmd.Parameters.Add(new SqliteParameter("@userId", userId));
                        int result = deleteCmd.ExecuteNonQuery();

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
                        string countSql = "SELECT COUNT(plan_name) FROM plan WHERE user_id = @userId;";
                        SqliteCommand countCmd = new SqliteCommand(countSql, db);
                        countCmd.Parameters.Add(new SqliteParameter("@userId", userId));
                        long planLen = (long)countCmd.ExecuteScalar();
                        if (planLen == 0) return null;
                        string selectSql = "SELECT plan_name,suit_id,glasses_id,achieve_title_id,id " +
                            "FROM plan WHERE user_id = @userId;";
                        SqliteCommand selectCmd = new SqliteCommand(selectSql, db);
                        selectCmd.Parameters.Add(new SqliteParameter("@userId", userId));
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
                            suitAchieveTitlePlans.Add(result.id, result);
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

            public static Dictionary<int, SuitAchieveTitlePlan> PlanTableSearch(int userId, string searchWord)
            {
                try
                {
                    using (db)
                    {
                        db.Open();
                        string countSql = "SELECT COUNT(plan_name) FROM plan WHERE user_id = @userId;";
                        SqliteCommand countCmd = new SqliteCommand(countSql, db);
                        countCmd.Parameters.Add(new SqliteParameter("@userId", userId));
                        long planLen = (long)countCmd.ExecuteScalar();
                        if (planLen == 0) return null;
                        string selectSql = "SELECT plan_name,suit_id,glasses_id,achieve_title_id,id " +
                            "FROM plan WHERE plan_name LIKE @searchWord AND user_id = @userId;";
                        SqliteCommand selectCmd = new SqliteCommand(selectSql, db);
                        selectCmd.Parameters.Add(new SqliteParameter("@userId", userId));
                        selectCmd.Parameters.Add(new SqliteParameter("@searchWord", $"%{ searchWord }%"));

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
        #endregion
        public static class PetDBServise
        {
            private static string petDBPath = dbPath + dbMap["petDB"];
            private static SqliteConnection db;
            public static bool CheckAndInitDB()
            {
                try
                {
                    Logger.Log("DBInit", "初始化精灵数据库...");
                    if (!Directory.Exists(dbPath)) Directory.CreateDirectory(dbPath);
                    if (!File.Exists(petDBPath))
                    {
                        Logger.Log("CreateDB", "精灵数据库不存在！创建数据库...");
                        File.Create(petDBPath).Close();
                        Logger.Log("CreateDB", "数据库文件创建完成！");
                        Logger.Log("CreateDBTableStart", "开始创建数据库table");
                        using (db = new SqliteConnection($"Filename={petDBPath}"))
                        {
                            db.Open();
                            createTableSql[] createTableCmds = new createTableSql[3]
                            {
                                new createTableSql("pet", "精灵信息表", "CREATE TABLE pet (id INTEGER PRIMARY KEY AUTOINCREMENT,pet_name CHAR(32) NOT NULL," +
                                "pet_id INT UNIQUE NOT NULL,pet_hp INT NOT NULL,pet_atk INT NOT NULL,pet_def INT NOT NULL," +
                                "pet_spatk INT NOT NULL,pet_spdef INT NOT NULL,pet_spd INT NOT NULL,pet_type INT NOT NULL);"),
                                new createTableSql("petskins", "精灵皮肤表", "CREATE TABLE petskins (id INTEGER PRIMARY KEY AUTOINCREMENT,pet_skins_name CHAR(32) NOT NULL," +
                                "pet_skins_id INT UNIQUE NOT NULL);"),
                                new createTableSql("petskinsreplaceplan", "精灵皮肤替换方案表", "CREATE TABLE petskinsreplaceplan (id INTEGER PRIMARY KEY AUTOINCREMENT," +
                                "pet_name CHAR(32) NOT NULL,pet_id INT UNIQUE NOT NULL,pet_skins_name CHAR(32) NOT NULL,pet_skins_id INT NOT NULL);")
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
                        db = new SqliteConnection($"Filename={petDBPath}");
                    }
                    Logger.Log("DBInit", "初始化精灵数据库完成!!!");
                }
                catch (Exception ex)
                {
                    Logger.Error($"精灵数据库加载出错！ errorMessage{ex.Message}");
                }
                return true;
            }

            #region
            public static int PetTableInsertData(Pet insertData)
            {
                try
                {
                    using (db)
                    {
                        db.Open();
                        string insertSql = "INSERT INTO pet (pet_name,pet_id,pet_hp,pet_atk,pet_def,pet_spatk,pet_spdef,pet_spd,pet_type) " +
                            "VALUES(@pet_name,@pet_id,@pet_hp,@pet_atk,@pet_def,@pet_spatk,@pet_spdef,@pet_spd,@pet_type);";
                        SqliteCommand insertCmd = new SqliteCommand(insertSql, db);
                        insertCmd.Parameters.Add(new SqliteParameter("@pet_name", insertData.name));
                        insertCmd.Parameters.Add(new SqliteParameter("@pet_id", insertData.id));
                        insertCmd.Parameters.Add(new SqliteParameter("@pet_hp", insertData.hp));
                        insertCmd.Parameters.Add(new SqliteParameter("@pet_atk", insertData.atk));
                        insertCmd.Parameters.Add(new SqliteParameter("@pet_def", insertData.def));
                        insertCmd.Parameters.Add(new SqliteParameter("@pet_spatk", insertData.spatk));
                        insertCmd.Parameters.Add(new SqliteParameter("@pet_spdef", insertData.spdef));
                        insertCmd.Parameters.Add(new SqliteParameter("@pet_spd", insertData.spd));
                        insertCmd.Parameters.Add(new SqliteParameter("@pet_type", insertData.type));
                        int value = insertCmd.ExecuteNonQuery();
                        return value;
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("UNIQUE")) 
                    {
                        return PetTableUpdateData(insertData);
                    }
                    Logger.Error($"数据库精灵数据信息插入失败！ errorMessage：{ex.Message}");
                    return -1;
                }
            }

            public static int PetTableUpdateData(Pet updateData)
            {
                try
                {
                    using (db)
                    {
                        db.Open();
                        string updateSql = "UPDATE pet SET pet_name = @pet_name,pet_hp = @pet_hp,pet_atk = @pet_atk," +
                            "pet_def = @pet_def,pet_spatk = @pet_spatk,pet_spdef = @pet_spdef,pet_spd = @pet_spd,pet_type=@pet_type " +
                            "WHERE pet_id = @pet_id;";
                        SqliteCommand updateCmd = new SqliteCommand(updateSql, db);
                        updateCmd.Parameters.Add(new SqliteParameter("@pet_name", updateData.name));
                        updateCmd.Parameters.Add(new SqliteParameter("@pet_id", updateData.id));
                        updateCmd.Parameters.Add(new SqliteParameter("@pet_hp", updateData.hp));
                        updateCmd.Parameters.Add(new SqliteParameter("@pet_atk", updateData.atk));
                        updateCmd.Parameters.Add(new SqliteParameter("@pet_def", updateData.def));
                        updateCmd.Parameters.Add(new SqliteParameter("@pet_spatk", updateData.spatk));
                        updateCmd.Parameters.Add(new SqliteParameter("@pet_spdef", updateData.spdef));
                        updateCmd.Parameters.Add(new SqliteParameter("@pet_spd", updateData.spd));
                        updateCmd.Parameters.Add(new SqliteParameter("@pet_type", updateData.type));
                        int value = updateCmd.ExecuteNonQuery();
                        return value;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error($"数据库精灵数据信息更新失败！ errorMessage：{ex.Message}");
                    return -1;
                }
            }

            public static List<Pet> PetTableLikeSelectDataByPetId(int petId)
            {
                try
                {
                    using (db)
                    {
                        db.Open();
                        string selectSql = "SELECT pet_name,pet_id,pet_hp,pet_atk,pet_def,pet_spatk,pet_spdef,pet_spd,pet_type " +
                            "FROM pet WHERE pet_id like @petId;";
                        SqliteCommand selectCmd = new SqliteCommand(selectSql, db);
                        selectCmd.Parameters.Add(new SqliteParameter("@petId", $"%{petId}%"));
                        SqliteDataReader reader = selectCmd.ExecuteReader();
                        List<Pet> result = new List<Pet>();
                        while (reader.Read())
                        {
                            Pet pet = new Pet();
                            pet.name = reader.GetString(0);
                            pet.id = reader.GetInt32(1);
                            pet.hp = reader.GetInt32(2);
                            pet.atk = reader.GetInt32(3);
                            pet.def = reader.GetInt32(4);
                            pet.spdef = reader.GetInt32(5);
                            pet.spdef = reader.GetInt32(6);
                            pet.spd = reader.GetInt32(7);
                            pet.type = reader.GetInt32(8);
                            result.Add(pet);
                        }
                        return result.Count > 0 ? result : null;

                    }
                }
                catch (Exception ex)
                {
                    Logger.Error($"数据库精灵数据信息查询失败！ errorMessage：{ex.Message}");
                    return null;
                }
            }

            public static List<Pet> PetTableSelectDataByPetName(string petName)
            {
                try
                {
                    using (db)
                    {
                        db.Open();
                        string selectSql = "SELECT pet_name,pet_id,pet_hp,pet_atk,pet_def,pet_spatk,pet_spdef,pet_spd,pet_type " +
                            "FROM pet WHERE pet_name like @petName;";
                        SqliteCommand selectCmd = new SqliteCommand(selectSql, db);
                        selectCmd.Parameters.Add(new SqliteParameter("@petName", $"%{petName}%"));
                        SqliteDataReader reader = selectCmd.ExecuteReader();
                        List<Pet> result = new List<Pet>();
                        while (reader.Read())
                        {
                            Pet pet = new Pet();
                            pet.name = reader.GetString(0);
                            pet.id = reader.GetInt32(1);
                            pet.hp = reader.GetInt32(2);
                            pet.atk = reader.GetInt32(3);
                            pet.def = reader.GetInt32(4);
                            pet.spdef = reader.GetInt32(5);
                            pet.spdef = reader.GetInt32(6);
                            pet.spd = reader.GetInt32(7);
                            pet.type = reader.GetInt32(8);
                            result.Add(pet);
                        }
                        return result.Count > 0 ? result : null;

                    }
                }
                catch (Exception ex)
                {
                    Logger.Error($"数据库精灵数据信息查询失败！ errorMessage：{ex.Message}");
                    return null;
                }
            }

            public static string PetTableSearchPetNameByPetId(int petId)
            {
                try
                {
                    using (db)
                    {
                        db.Open();
                        string selectSql = "SELECT pet_name " +
                            "FROM pet WHERE pet_id = @petId;";
                        SqliteCommand selectCmd = new SqliteCommand(selectSql, db);
                        selectCmd.Parameters.Add(new SqliteParameter("@petId", $"{petId}"));
                        SqliteDataReader reader = selectCmd.ExecuteReader();
                        string petName = "";
                        while (reader.Read())
                        {
                            petName = reader.GetString(0);
                        }
                        return petName;

                    }
                }
                catch (Exception ex)
                {
                    Logger.Error($"数据库精灵数据信息查询失败！ errorMessage：{ex.Message}");
                    return "";
                }
            }

            #endregion
            #region
            public static int PetSkinsTableInsertData(PetSkins insertData)
            {
                try
                {
                    using (db)
                    {
                        db.Open();
                        string insertSql = "INSERT INTO petskins (pet_skins_name,pet_skins_id) " +
                            "VALUES(@pet_skins_name,@pet_skins_id);";
                        SqliteCommand insertCmd = new SqliteCommand(insertSql, db);
                        insertCmd.Parameters.Add(new SqliteParameter("@pet_skins_name", insertData.name));
                        insertCmd.Parameters.Add(new SqliteParameter("@pet_skins_id", insertData.id));
                        int value = insertCmd.ExecuteNonQuery();
                        return value;
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("UNIQUE"))
                    {
                        return PetSkinsTableUpdateData(insertData);
                    }
                    Logger.Error($"数据库精灵皮肤数据信息插入失败！ errorMessage：{ex.Message}");
                    return -1;
                }
            }

            public static int PetSkinsTableUpdateData(PetSkins updateData)
            {
                try
                {
                    using (db)
                    {
                        db.Open();
                        string updateSql = "UPDATE petskins SET pet_skins_name = @pet_skins_name,pet_skins_id = @pet_skins_id " +
                            "WHERE pet_skins_id = @pet_skins_id;";
                        SqliteCommand updateCmd = new SqliteCommand(updateSql, db);
                        updateCmd.Parameters.Add(new SqliteParameter("@pet_skins_name", updateData.name));
                        updateCmd.Parameters.Add(new SqliteParameter("@pet_skins_id", updateData.id));
                        int value = updateCmd.ExecuteNonQuery();
                        return value;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error($"数据库精灵皮肤数据信息更新失败！ errorMessage：{ex.Message}");
                    return -1;
                }
            }

            public static List<PetSkins> PetSkinsTableLikeSelectDataBySkinsId(int petSkinsId)
            {
                try
                {
                    using (db)
                    {
                        db.Open();
                        string selectSql = "SELECT pet_skins_name,pet_skins_id " +
                            "FROM petskins WHERE pet_skins_id like @pet_skins_id;";
                        SqliteCommand selectCmd = new SqliteCommand(selectSql, db);
                        selectCmd.Parameters.Add(new SqliteParameter("@pet_skins_id", $"%{petSkinsId}%"));
                        SqliteDataReader reader = selectCmd.ExecuteReader();
                        List<PetSkins> result = new List<PetSkins>();
                        while (reader.Read())
                        {
                            PetSkins petSkins = new PetSkins();
                            petSkins.name = reader.GetString(0);
                            petSkins.id = reader.GetInt32(1);
                            result.Add(petSkins);
                        }
                        return result.Count > 0 ? result : null;

                    }
                }
                catch (Exception ex)
                {
                    Logger.Error($"数据库精灵皮肤数据信息查询失败！ errorMessage：{ex.Message}");
                    return null;
                }
            }

            public static List<PetSkins> PetSkinsTableSelectDataBySkinsName(string petSkinsName)
            {
                try
                {
                    using (db)
                    {
                        db.Open();
                        string selectSql = "SELECT pet_skins_name,pet_skins_id " +
                            "FROM petskins WHERE pet_skins_name like @petName;";
                        SqliteCommand selectCmd = new SqliteCommand(selectSql, db);
                        selectCmd.Parameters.Add(new SqliteParameter("@petName", $"%{petSkinsName}%"));
                        SqliteDataReader reader = selectCmd.ExecuteReader();
                        List<PetSkins> result = new List<PetSkins>();
                        while (reader.Read())
                        {
                            PetSkins petSkins = new PetSkins();
                            petSkins.name = reader.GetString(0);
                            petSkins.id = reader.GetInt32(1);
                            result.Add(petSkins);
                        }
                        return result.Count > 0 ? result : null;

                    }
                }
                catch (Exception ex)
                {
                    Logger.Error($"数据库精灵数据信息查询失败！ errorMessage：{ex.Message}");
                    return null;
                }
            }

            #endregion
            #region
            public static int PetSkinsPlanTableInsertData(PetSkinsReplacePlan insertData)
            {
                try
                {
                    using (db)
                    {
                        db.Open();
                        string insertSql = "INSERT INTO petskinsreplaceplan (pet_name,pet_id,pet_skins_name,pet_skins_id) " +
                            "VALUES(@pet_name,@pet_id,@pet_skins_name,@pet_skins_id)";
                        SqliteCommand insertCmd = new SqliteCommand(insertSql, db);
                        insertCmd.Parameters.Add(new SqliteParameter("@pet_name", insertData.petName));
                        insertCmd.Parameters.Add(new SqliteParameter("@pet_id", insertData.petId));
                        insertCmd.Parameters.Add(new SqliteParameter("@pet_skins_name", insertData.skinsName));
                        insertCmd.Parameters.Add(new SqliteParameter("@pet_skins_id", insertData.skinsId));
                        int value = insertCmd.ExecuteNonQuery();
                        return value;
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("UNIQUE"))
                    {
                        return PetSkinsPlanTableUpdateData(insertData);
                    }
                    Logger.Error($"数据库精灵皮肤替换方案数据信息插入失败！ errorMessage：{ex.Message}");
                    return -1;
                }
            }
            public static int PetSkinsPlanTableUpdateData(PetSkinsReplacePlan updateData)
            {
                try
                {
                    using (db)
                    {
                        db.Open();
                        string updateSql = "UPDATE petskinsreplaceplan SET pet_name = @pet_name ,pet_id = @pet_id ," +
                            "pet_skins_name = @pet_skins_name ,pet_skins_id = @pet_skins_id " +
                            "WHERE pet_id = @pet_id;";
                        SqliteCommand updateCmd = new SqliteCommand(updateSql, db);
                        updateCmd.Parameters.Add(new SqliteParameter("@pet_id", updateData.petId));
                        updateCmd.Parameters.Add(new SqliteParameter("@pet_name", updateData.petName));
                        updateCmd.Parameters.Add(new SqliteParameter("@pet_skins_id", updateData.skinsId));
                        updateCmd.Parameters.Add(new SqliteParameter("@pet_skins_name", updateData.skinsName));
                        int value = updateCmd.ExecuteNonQuery();
                        return value;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error($"数据库精灵皮肤替换方案数据信息更新失败！ errorMessage：{ex.Message}");
                    return -1;
                }
            }
            public static int PetSkinsPlanTableDeleteData(int petId)
            {
                try
                {
                    using (db)
                    {
                        db.Open();
                        string deleteSql = "DELETE FROM petskinsreplaceplan WHERE pet_id = @pet_id;";
                        SqliteCommand deleteCmd = new SqliteCommand(deleteSql, db);
                        deleteCmd.Parameters.Add(new SqliteParameter("@pet_id", petId));
                        int value = deleteCmd.ExecuteNonQuery();
                        return value;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error($"数据库精灵皮肤替换方案数据信息删除失败！ errorMessage：{ex.Message}");
                    return -1;
                }
            }
            public static List<PetSkinsReplacePlan> PetSkinsPlanTableSelectData(string petName)
            {
                try
                {
                    using (db)
                    {
                        db.Open();
                        string selectSql = "SELECT pet_name,pet_id,pet_skins_name,pet_skins_id FROM petskinsreplaceplan WHERE pet_name LIKE @pet_name;";
                        SqliteCommand selectCmd = new SqliteCommand(selectSql, db);
                        selectCmd.Parameters.Add(new SqliteParameter("@pet_name", $"%{petName}%"));
                        SqliteDataReader reader = selectCmd.ExecuteReader();
                        List<PetSkinsReplacePlan> result = new List<PetSkinsReplacePlan>();
                        while (reader.Read())
                        {
                            PetSkinsReplacePlan plan = new PetSkinsReplacePlan();
                            plan.petName = reader.GetString(0);
                            plan.petId = reader.GetInt32(1);
                            plan.skinsName = reader.GetString(2);
                            plan.skinsId = reader.GetInt32(3);
                            result.Add(plan);
                        }
                        return result.Count > 0 ? result : null;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error($"数据库精灵皮肤替换方案数据信息查询失败！ errorMessage：{ex.Message}");
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
    }
}
