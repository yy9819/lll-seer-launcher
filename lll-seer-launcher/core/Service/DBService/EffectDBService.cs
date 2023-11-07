using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System.IO;
using lll_seer_launcher.core.Utils;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Dto.JSON;
namespace lll_seer_launcher.core.Service.DBService
{
    public class EffectDBService : DBService
    {
        private static string petDBPath = dbPath + dbMap["effectDB"];
        private static SqliteConnection db;
        private static Dictionary<string, CreateTableSql> tableDic = new Dictionary<string, CreateTableSql>()
        {
            { "peteffect" , new CreateTableSql("peteffect", "精灵魂印", "CREATE TABLE peteffect (id INTEGER PRIMARY KEY AUTOINCREMENT," +
                                "pet_id TEXT ,icon_id INT,effect_id INT ,tips TEXT);")},
            { "newse" , new CreateTableSql("newse", "精灵特性", "CREATE TABLE newse (id INTEGER PRIMARY KEY AUTOINCREMENT," +
                                "idx INT ,eid INT,intro TEXT ,desc TEXT,args TEXT,star_level INT);")},

        };
        public static bool CheckAndInitDB()
        {
            try
            {
                Logger.Log("DBInit", "初始化效果数据库...");
                if (!Directory.Exists(dbPath)) Directory.CreateDirectory(dbPath);
                if (!File.Exists(petDBPath))
                {
                    Logger.Log("CreateDB", "效果数据库不存在！创建数据库...");
                    File.Create(petDBPath).Close();
                    Logger.Log("CreateDB", "数据库文件创建完成！");
                    Logger.Log("CreateDBTableStart", "开始创建数据库table");
                }
                using (db = new SqliteConnection($"Filename={petDBPath}"))
                {
                    db.Open();
                    foreach (var key in tableDic.Keys)
                    {
                        if (!TableExists(db, key)) if (!CrateTable(db, tableDic[key])) return false;
                    }
                }
                Logger.Log("DBInit", "初始化效果数据库完成!!!");
            }
            catch (Exception ex)
            {
                Logger.Error($"效果数据库加载出错！ errorMessage{ex.Message}");
            }
            return true;
        }
        #region
        public static void PetEffectTableTransactionInsertData(List<PetEffect> insertData)
        {
            using (db)
            {
                db.Open();
                int index = 0;
                using (SqliteTransaction transaction = db.BeginTransaction())
                {
                    using (SqliteCommand command = db.CreateCommand())
                    {
                        command.Transaction = transaction;

                        command.CommandText = "INSERT OR REPLACE INTO peteffect (pet_id,icon_id,effect_id,tips) " +
                            "VALUES(@pet_id,@icon_id,@effect_id,@tips);";

                        command.Parameters.Add(new SqliteParameter("@pet_id", DBNull.Value));
                        command.Parameters.Add(new SqliteParameter("@icon_id", DBNull.Value));
                        command.Parameters.Add(new SqliteParameter("@effect_id", DBNull.Value));
                        command.Parameters.Add(new SqliteParameter("@tips", DBNull.Value));
                        // Generate and insert a large amount of data
                        while (index < insertData.Count)
                        {
                            command.Parameters["@pet_id"].Value = insertData[index].petId;
                            command.Parameters["@icon_id"].Value = insertData[index].iconId;
                            command.Parameters["@effect_id"].Value = insertData[index].effectId;
                            command.Parameters["@tips"].Value = insertData[index].tips;
                            command.ExecuteNonQuery();
                            index++;
                        }

                    }
                    transaction.Commit();
                }
            }
        }
        public static string PetEffectTableGetTips(int effectId)
        {
            try
            {
                using (db)
                {
                    db.Open();
                    string selectSql = "SELECT tips " +
                        "FROM peteffect WHERE effect_id = @effect_id;";
                    SqliteCommand selectCmd = new SqliteCommand(selectSql, db);
                    selectCmd.Parameters.Add(new SqliteParameter("@effect_id", $"{effectId}"));
                    SqliteDataReader reader = selectCmd.ExecuteReader();
                    string tips = "";
                    while (reader.Read())
                    {
                        tips = reader.GetString(0);
                    }
                    return tips;

                }
            }
            catch (Exception ex)
            {
                Logger.Error($"数据库精灵魂印数据信息查询失败！ errorMessage：{ex.Message}");
                return "";
            }
        }
        #endregion
        #region
        public static void PetNewSeTableTransactionInsertData(List<NewSeIdx> insertData)
        {
            using (db)
            {
                db.Open();
                int index = 0;
                using (SqliteTransaction transaction = db.BeginTransaction())
                {
                    using (SqliteCommand command = db.CreateCommand())
                    {
                        command.Transaction = transaction;

                        command.CommandText = "INSERT OR REPLACE INTO newse (idx,eid,intro,desc,args,star_level) " +
                            "VALUES(@idx,@eid,@intro,@desc,@args,@star_level);";

                        command.Parameters.Add(new SqliteParameter("@idx", DBNull.Value));
                        command.Parameters.Add(new SqliteParameter("@eid", DBNull.Value));
                        command.Parameters.Add(new SqliteParameter("@intro", DBNull.Value));
                        command.Parameters.Add(new SqliteParameter("@desc", DBNull.Value));
                        command.Parameters.Add(new SqliteParameter("@args", DBNull.Value));
                        command.Parameters.Add(new SqliteParameter("@star_level", DBNull.Value));
                        // Generate and insert a large amount of data
                        while (index < insertData.Count)
                        {
                            if (insertData[index].starLevel >= 0)
                            {
                                command.Parameters["@idx"].Value = insertData[index].idx;
                                command.Parameters["@eid"].Value = insertData[index].eid;
                                command.Parameters["@intro"].Value = insertData[index].intro;
                                command.Parameters["@desc"].Value = insertData[index].desc;
                                command.Parameters["@args"].Value = insertData[index].args;
                                command.Parameters["@star_level"].Value = insertData[index].starLevel;
                                command.ExecuteNonQuery();
                            }
                            index++;
                        }

                    }
                    transaction.Commit();
                }
            }
        }
        public static NewSeIdx PetNewSeTableGetInfo(int eid,string args)
        {
            try
            {
                using (db)
                {
                    db.Open();
                    string selectSql = "SELECT idx,eid,intro,desc,args,star_level " +
                        "FROM newse WHERE eid = @eid AND args=@args;";
                    SqliteCommand selectCmd = new SqliteCommand(selectSql, db);
                    selectCmd.Parameters.Add(new SqliteParameter("@eid", $"{eid}"));
                    selectCmd.Parameters.Add(new SqliteParameter("@args", $"{args}"));
                    SqliteDataReader reader = selectCmd.ExecuteReader();
                    NewSeIdx info = new NewSeIdx();
                    while (reader.Read())
                    {
                        info.idx = reader.GetInt32(0);
                        info.eid = reader.GetInt32(1);
                        info.intro = reader.GetString(2);
                        info.desc = reader.GetString(3);
                        info.args = reader.GetString(4);
                        info.starLevel = reader.GetInt32(5);
                    }
                    return info;

                }
            }
            catch (Exception ex)
            {
                Logger.Error($"数据库精灵魂印数据信息查询失败！ errorMessage：{ex.Message}");
                return new NewSeIdx();
            }
        }
        #endregion
    }
}
