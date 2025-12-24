using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using WebApplication.Config;
using EmpModel = WebApplication.Models.Employee;

namespace WebApplication.Repositories
{
    public class EmployeeRepository
    {

        public List<EmpModel> GetAll()
        {
            var list = new List<EmpModel>();
            using (var conn = new NpgsqlConnection(DbConfig.PgConn))
            {
                conn.Open();
                string sql = "SELECT id, nama, alamat, email FROM employees ORDER BY id";
                using (var cmd = new NpgsqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new EmpModel
                        {
                            Id = reader.GetInt32(0),
                            Nama = reader.GetString(1),
                            Alamat = reader.GetString(2),
                            Email = reader.GetString(3)
                        });
                    }
                }
            }
            return list;
        }

        public EmpModel GetById(int id)
        {
            using (var conn = new NpgsqlConnection(DbConfig.PgConn))
            {
                conn.Open();
                string sql = "SELECT id, nama, alamat, email FROM employees WHERE id=@id";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new EmpModel
                            {
                                Id = reader.GetInt32(0),
                                Nama = reader.GetString(1),
                                Alamat = reader.GetString(2),
                                Email = reader.GetString(3)
                            };
                        }
                    }
                }
            }
            return null;
        }

        public void Insert(EmpModel emp)
        {
            using (var conn = new NpgsqlConnection(DbConfig.PgConn))
            {
                conn.Open();
                string sql = "INSERT INTO employees (nama, alamat, email) VALUES (@nama, @alamat, @email)";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nama", emp.Nama);
                    cmd.Parameters.AddWithValue("@alamat", emp.Alamat);
                    cmd.Parameters.AddWithValue("@email", emp.Email);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(EmpModel emp)
        {
            using (var conn = new NpgsqlConnection(DbConfig.PgConn))
            {
                conn.Open();
                string sql = "UPDATE employees SET nama=@nama, alamat=@alamat, email=@email WHERE id=@id";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nama", emp.Nama);
                    cmd.Parameters.AddWithValue("@alamat", emp.Alamat);
                    cmd.Parameters.AddWithValue("@email", emp.Email);
                    cmd.Parameters.AddWithValue("@id", emp.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = new NpgsqlConnection(DbConfig.PgConn))
            {
                conn.Open();
                string sql = "DELETE FROM employees WHERE id=@id";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }

        }
    }
}
