using System;
using System.Collections.Generic;
using System.IO;

namespace lll_seer_launcher.core.Servise.DBServise
{
    public class DBServise
    {
        protected static string dbPath = Directory.GetCurrentDirectory() + "\\bin\\DB\\";
        protected static Dictionary<string, string> dbMap = new Dictionary<string, string>()
            {
                { "suitDB","suit.db" }, 
                { "petDB","pet.db" }
            };
        protected class createTableSql
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
