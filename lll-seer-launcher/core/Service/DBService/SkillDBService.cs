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
    public class SkillDBService : DBService
    {
        private static string skillDBPath = dbPath + dbMap["skillDB"];
        private static SqliteConnection db;
        private static Dictionary<string, CreateTableSql> tableDic = new Dictionary<string, CreateTableSql>()
        {
            { "skill" , new CreateTableSql("skill", "技能信息表", "CREATE TABLE skill (id INTEGER PRIMARY KEY AUTOINCREMENT," +
                                "skill_name CHAR(32) NOT NULL," +
                                "skill_id INT UNIQUE NOT NULL," +
                                "skill_type INT NOT NULL," +
                                "skill_power INT NOT NULL," +
                                "skill_maxpp INT NOT NULL," +
                                "skill_accuracy INT NOT NULL);")},

        };
        public static bool CheckAndInitDB()
        {
            try
            {
                Logger.Log("DBInit", "初始化技能数据库...");
                if (!Directory.Exists(dbPath)) Directory.CreateDirectory(dbPath);
                if (!File.Exists(skillDBPath))
                {
                    Logger.Log("CreateDB", "技能数据库不存在！创建数据库...");
                    File.Create(skillDBPath).Close();
                    Logger.Log("CreateDB", "数据库文件创建完成！");
                    Logger.Log("CreateDBTableStart", "开始创建数据库table");
                }
                using (db = new SqliteConnection($"Filename={skillDBPath}"))
                {
                    db.Open();
                    foreach (var key in tableDic.Keys)
                    {
                        if (!TableExists(db, key)) if (!CrateTable(db, tableDic[key])) return false;
                    }
                }
                Logger.Log("DBInit", "初始化技能数据库完成!!!");
            }
            catch (Exception ex)
            {
                Logger.Error($"技能数据库加载出错！ errorMessage{ex.Message}");
            }
            return true;
        }
        public static void SkillTableTransactionInsertData(List<Move> insertDatas)
        {
            using (db)
            {
                db.Open();
                using (SqliteTransaction transaction = db.BeginTransaction())
                {
                    using (SqliteCommand command = db.CreateCommand())
                    {
                        command.Transaction = transaction;

                        command.CommandText = "INSERT OR REPLACE INTO skill (skill_name,skill_id,skill_type,skill_power,skill_maxpp,skill_accuracy) " +
                        "VALUES(@skill_name,@skill_id,@skill_type,@skill_power,@skill_maxpp,@skill_accuracy);";

                        command.Parameters.Add(new SqliteParameter("@skill_name", DBNull.Value));
                        command.Parameters.Add(new SqliteParameter("@skill_id", DBNull.Value));
                        command.Parameters.Add(new SqliteParameter("@skill_type", DBNull.Value));
                        command.Parameters.Add(new SqliteParameter("@skill_power", DBNull.Value));
                        command.Parameters.Add(new SqliteParameter("@skill_maxpp", DBNull.Value));
                        command.Parameters.Add(new SqliteParameter("@skill_accuracy", DBNull.Value));
                        // Generate and insert a large amount of data
                        foreach (var skill in insertDatas)
                        {
                            command.Parameters["@skill_name"].Value = skill.name;
                            command.Parameters["@skill_id"].Value = skill.id;
                            command.Parameters["@skill_type"].Value = skill.type;
                            command.Parameters["@skill_power"].Value = skill.power;
                            command.Parameters["@skill_maxpp"].Value = skill.maxPP;
                            command.Parameters["@skill_accuracy"].Value = skill.accuracy;
                            command.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                }
            }
            
        }

        public static int SkillTableInsertData(Move insertData)
        {
            try
            {
                using (db)
                {
                    db.Open();
                    string insertSql = "INSERT INTO skill (skill_name,skill_id,skill_type,skill_power,skill_maxpp,skill_accuracy) " +
                        "VALUES(@skill_name,@skill_id,@skill_type,@skill_power,@skill_maxpp,@skill_accuracy);";
                    SqliteCommand insertCmd = new SqliteCommand(insertSql, db);
                    insertCmd.Parameters.Add(new SqliteParameter("@skill_name", insertData.name));
                    insertCmd.Parameters.Add(new SqliteParameter("@skill_id", insertData.id));
                    insertCmd.Parameters.Add(new SqliteParameter("@skill_type", insertData.type));
                    insertCmd.Parameters.Add(new SqliteParameter("@skill_power", insertData.power));
                    insertCmd.Parameters.Add(new SqliteParameter("@skill_maxpp", insertData.maxPP));
                    insertCmd.Parameters.Add(new SqliteParameter("@skill_accuracy", insertData.accuracy));
                    int value = insertCmd.ExecuteNonQuery();
                    return value;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UNIQUE"))
                {
                    return SkillTableUpdateData(insertData);
                }
                Logger.Error($"数据库精灵数据信息插入失败！ errorMessage：{ex.Message}");
                return -1;
            }
        }
        public static int SkillTableUpdateData(Move updateData)
        {
            try
            {
                using (db)
                {
                    db.Open();
                    string updateSql = "UPDATE skill SET skill_name = @skill_name,skill_id = @skill_id,skill_type = @skill_type," +
                        "skill_power = @skill_power,skill_maxpp = @skill_maxpp,skill_accuracy = @skill_accuracy " +
                        "WHERE skill_id = @skill_id;";
                    SqliteCommand updateCmd = new SqliteCommand(updateSql, db);
                    updateCmd.Parameters.Add(new SqliteParameter("@skill_name", updateData.name));
                    updateCmd.Parameters.Add(new SqliteParameter("@skill_id", updateData.id));
                    updateCmd.Parameters.Add(new SqliteParameter("@skill_type", updateData.type));
                    updateCmd.Parameters.Add(new SqliteParameter("@skill_power", updateData.power));
                    updateCmd.Parameters.Add(new SqliteParameter("@skill_maxpp", updateData.maxPP));
                    updateCmd.Parameters.Add(new SqliteParameter("@skill_accuracy", updateData.accuracy));
                    int value = updateCmd.ExecuteNonQuery();
                    return value;
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"数据库技能数据信息更新失败！ errorMessage：{ex.Message}");
                return -1;
            }
        }
        public static string SkillTableSearchSkillNameBySkillId(int skillId)
        {
            try
            {
                using (db)
                {
                    db.Open();
                    string selectSql = "SELECT skill_name " +
                        "FROM skill WHERE skill_id = @skill_id;";
                    SqliteCommand selectCmd = new SqliteCommand(selectSql, db);
                    selectCmd.Parameters.Add(new SqliteParameter("@skill_id", $"{skillId}"));
                    SqliteDataReader reader = selectCmd.ExecuteReader();
                    string skillName = $"{skillId}";
                    while (reader.Read())
                    {
                        skillName = reader.GetString(0);
                    }
                    return skillName;

                }
            }
            catch (Exception ex)
            {
                Logger.Error($"数据库技能数据信息查询失败！ errorMessage：{ex.Message}");
                return $"{skillId}";
            }
        }
    }
}
