using System;
using System.Collections.Generic;
using System.Text;
using ProfileBook.Models;
using SQLite;

namespace ProfileBook.Data
{
    public class DbConnection
    {
        public readonly SQLiteAsyncConnection database;

        public DbConnection(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<User>().Wait();
        }
    }
}