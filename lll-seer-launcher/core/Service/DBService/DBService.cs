using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.Sqlite;
using lll_seer_launcher.core.Utils;

namespace lll_seer_launcher.core.Service.DBService
{
    public class DBService
    {
        protected static string dbPath = Directory.GetCurrentDirectory() + "\\bin\\DB\\";
        protected static Dictionary<string, string> dbMap = new Dictionary<string, string>()
            {
                { "suitDB","suit.db" }, 
                { "petDB","pet.db" },
                { "skillDB","skill.db" },
            };
        protected class CreateTableSql
        {
            public string dbTableName { get; set; }
            public string dbTableCheneseName { get; set; }
            public string sqlString { get; set; }

            public CreateTableSql(string tableName,string tableCheneseName, string sqlString)
            {
                this.dbTableName = tableName;
                this.dbTableCheneseName = tableCheneseName;
                this.sqlString = sqlString;
            }
        }
        protected static bool TableExists(SqliteConnection connection, string tableName)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT name FROM sqlite_master WHERE type='table' AND name='{tableName}'";
                var result = command.ExecuteScalar();
                return (result != null && result.ToString() == tableName);
            }
        }

        protected static bool CrateTable(SqliteConnection db, CreateTableSql cmd)
        {
            try
            {
                Logger.Log("CrateTableStart", $"正在创建--{cmd.dbTableName}--");
                SqliteCommand createTableCmd = new SqliteCommand(cmd.sqlString, db);
                createTableCmd.ExecuteNonQuery();
                Logger.Log("CrateTableEnd", $"创建--{cmd.dbTableCheneseName}--成功!");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"创建{cmd.dbTableCheneseName}时出错！errorMessage：{ex.Message}");
                return false;
            }
        }
    }
}
