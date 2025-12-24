using System;
using System.Collections.Generic;
using Npgsql;
using WebApplication.Config;
using PosModel = WebApplication.Models.Position;

namespace WebApplication.Repositories
{
    public class PositionRepository
    {
        public List<PosModel> GetAll()
        {
            var list = new List<PosModel>();

            using (var conn = new NpgsqlConnection(DbConfig.PgConn))
            {
                conn.Open();
                string sql = "SELECT id, name, level FROM positions ORDER BY id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PosModel
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Level = reader.IsDBNull(2) ? "" : reader.GetString(2)
                        });
                    }
                }
            }

            return list;
        }

        // Get by Id (untuk edit)
        public PosModel GetById(int id)
        {
            using (var conn = new NpgsqlConnection(DbConfig.PgConn))
            {
                conn.Open();

                string sql = "SELECT id, name, level FROM positions WHERE id=@id";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PosModel
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Level = reader.IsDBNull(2) ? "" : reader.GetString(2)
                            };
                        }
                    }
                }
            }

            return null;
        }

        // Insert (Create)
        public void Insert(PosModel pos)
        {
            using (var conn = new NpgsqlConnection(DbConfig.PgConn))
            {
                conn.Open();

                string sql = "INSERT INTO positions(name, level) VALUES (@name, @level)";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@name", pos.Name);
                    cmd.Parameters.AddWithValue("@level", (object)pos.Level ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Update (Edit)
        public void Update(PosModel pos)
        {
            using (var conn = new NpgsqlConnection(DbConfig.PgConn))
            {
                conn.Open();

                string sql = "UPDATE positions SET name=@name, level=@level WHERE id=@id";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", pos.Id);
                    cmd.Parameters.AddWithValue("@name", pos.Name);
                    cmd.Parameters.AddWithValue("@level", (object)pos.Level ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = new NpgsqlConnection(DbConfig.PgConn))
            {
                conn.Open();

                string sql = "DELETE FROM positions WHERE id=@id";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}