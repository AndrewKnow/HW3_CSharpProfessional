using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data.Common;
using WebApi.Models;

namespace WebApi
{
    public class CustomerDataContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public CustomerDataContext(DbContextOptions options) : base(options)
        {
            CreateTables();
        }

        void CreateTables()
        {
            var sql = @"CREATE TABLE IF NOT EXISTS public.customers 
                                    (id BIGINT NOT NULL,
	                                firstname CHARACTER VARYING(255) NOT NULL,
	                                lastname CHARACTER VARYING(255) NOT NULL);";

            // Создание базы "авито" (как я себе представил)
            var connectionString = ConnectionString();
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            using var cmd = new NpgsqlCommand(sql, connection);
            cmd.ExecuteNonQuery();
        }

        public static string ConnectionString()
        {

            string connString = string.Format
            (
              "Server={0};Username={1};Database={2};Port={3};Password={4};SSLMode=Prefer",
              "dumbo.db.elephantsql.com",
              "lsakasyr",
              "lsakasyr",
              "5432",
              "CCU2zFRu7rWDHiGXPeul8i6SxHt1DQ2G");

            return connString;
        }
    }
}
