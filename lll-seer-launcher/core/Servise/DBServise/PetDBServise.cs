using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System.IO;
using lll_seer_launcher.core.Utils;
using lll_seer_launcher.core.Dto;

namespace lll_seer_launcher.core.Servise.DBServise
{
    public class PetDBServise:DBServise
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

}
