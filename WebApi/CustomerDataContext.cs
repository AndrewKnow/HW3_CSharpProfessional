using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data.Common;
using System;
using WebApi.Models;

namespace WebApi
{
    public class CustomerDataContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; } 
        public CustomerDataContext(DbContextOptions<CustomerDataContext> options) : base(options)
        {
            CreateTables();
        }

        void CreateTables()
        {
            var canCreate = CanCreateBase();
            if (canCreate)
            {
                //Если таблица не создана, создаём
                var sql = @"
                        CREATE TABLE customers 
                        (
                        id              BIGINT                      NOT NULL GENERATED ALWAYS AS IDENTITY,
	                    firstname       CHARACTER VARYING(255)      NOT NULL,
	                    lastname        CHARACTER VARYING(255)      NOT NULL
                        );";

                var connectionString = ConnectionString();
                using var connection = new NpgsqlConnection(connectionString);
                connection.Open();

                using var cmd = new NpgsqlCommand(sql, connection);
                cmd.ExecuteNonQuery();
            }
        }

        public static string ConnectionString()
        {
            string connString = string.Format
            (
              "Server={0};Username={1};Database={2};Port={3};Password={4};SSLMode=Prefer",
              "localhost",
              "postgres",
              "HW3_ProCSharp",
              "5432",
              "Dynamo1923"
              );

            return connString;
        }

        static bool CanCreateBase()
        {
            var connectionString = ConnectionString();
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            var sql = "SELECT EXISTS(SELECT * FROM pg_tables WHERE tablename = 'Customers' AND schemaname = 'public');";
            using var cmd = new NpgsqlCommand(sql, connection);

            var result = cmd.ExecuteScalar().ToString();

            if (result.ToLower() == "true")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}


