using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data.Common;
using System;
using WebApi.Models;

namespace WebApi
{
    public class CustomerDataContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; } //должен соответствовть БД сustomers
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

                //var sql = @"
                //        CREATE SEQUENCE customers_id_seq;

                //        CREATE TABLE customers 
                //        (
                //        id              BIGINT                      NOT NULL   DEFAULT NEXTVAL('customers_id_seq'),
                //     firstname       CHARACTER VARYING(255)      NOT NULL,
                //     lastname        CHARACTER VARYING(255)      NOT NULL,
                //        CONSTRAINT customers_id_seq PRIMARY KEY (id)
                //        );";

                var connectionString = ConnectionString();
                using var connection = new NpgsqlConnection(connectionString);
                connection.Open();

                using var cmd = new NpgsqlCommand(sql, connection);
                cmd.ExecuteNonQuery();

                //CREATE TABLE IF NOT EXISTS public.Customers 
                //var sql = @"
                //        CREATE SEQUENCE customers_id2_seq;
                //        CREATE TABLE public.customers 
                //        (
                //        id BIGINT NOT NULL DEFAULT NEXTVAL('customers_id2_seq'),
	               //     firstname CHARACTER VARYING(255) NOT NULL,
	               //     lastname CHARACTER VARYING(255) NOT NULL,
                //        CONSTRAINT customers_id2_seq PRIMARY KEY (id));";
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


