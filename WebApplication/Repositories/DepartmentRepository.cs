using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using WebApplication.Config;
using DepModel = WebApplication.Models.Department;

namespace WebApplication.Repositories
{
    public class DepartmentRepository
    {
        public List<DepModel> GetAll()
        {
            var list = new List<DepModel>();
            using (var conn = new NpgsqlConnection(DbConfig.PgConn))
            {
                conn.Open();
                string sql = "SELECT id, name FROM departments ORDER BY id";
                using (var cmd = new NpgsqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new DepModel
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                        });
                    }
                }
            }
            return list;
        }

        public DepModel GetById(int id)
        {
            using (var conn = new NpgsqlConnection(DbConfig.PgConn))
            {
                conn.Open();
                string sql = "SELECT id, name FROM departments WHERE id=@id";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new DepModel
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                            };
                        }
                    }
                }
            }
            return null;
        }

        public void Insert(DepModel department)
        {
            using (var conn = new NpgsqlConnection(DbConfig.PgConn))
            {
                conn.Open();
                string sql = "INSERT INTO departments (name) VALUES (@name)";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@name", department.Name);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(DepModel department)
        {
            using (var conn = new NpgsqlConnection(DbConfig.PgConn))
            {
                conn.Open();
                string sql = "UPDATE departments SET name=@name WHERE id=@id";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@name", department.Name);
                    cmd.Parameters.AddWithValue("@id", department.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = new NpgsqlConnection(DbConfig.PgConn))
            {
                conn.Open();
                string sql = "DELETE FROM departments WHERE id=@id";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}