using System;
using System.Collections.Generic;
using System.Linq;

// Alias untuk model Employee
// Dipakai supaya tidak bentrok dengan namespace WebApplication.Employee
using EmplModel = WebApplication.Models.Employee;

namespace WebApplication.Employee
{
    // Code-behind untuk halaman EmployeeForm.aspx
    public partial class EmployeeForm : System.Web.UI.Page
    {
        // Key Session untuk menyimpan list Employee
        private const string SessionKey = "Employees";

        // Property untuk mengambil dan menyimpan data Employee di Session
        private List<EmplModel> Employees
        {
            get
            {
                // Jika Session belum ada, inisialisasi list kosong
                if (Session[SessionKey] == null)
                    Session[SessionKey] = new List<EmplModel>();

                // Ambil list employee dari Session
                return (List<EmplModel>)Session[SessionKey];
            }
            set
            {
                // Simpan kembali list employee ke Session
                Session[SessionKey] = value;
            }
        }

        // Property untuk mengambil id dari query string
        // Contoh URL edit: EmployeeForm.aspx?id=3
        private int? EditId
        {
            get
            {
                int id;
                // Jika id valid → return id
                // Jika tidak ada / tidak valid → return null
                return int.TryParse(Request.QueryString["id"], out id)
                    ? id
                    : (int?)null;
            }
        }

        // Event yang dijalankan saat halaman pertama kali dibuka
        protected void Page_Load(object sender, EventArgs e)
        {
            // Pastikan hanya dijalankan saat load pertama (bukan postback)
            if (!IsPostBack)
            {
                // Jika ada id → mode Edit
                if (EditId.HasValue)
                {
                    lblTitle.Text = "Edit Employee";

                    // Cari employee berdasarkan id
                    var emp = Employees.FirstOrDefault(x => x.Id == EditId.Value);

                    // Jika data tidak ditemukan → kembali ke list
                    if (emp == null)
                    {
                        Response.Redirect("EmployeeList.aspx");
                        return;
                    }

                    // Isi form dengan data employee
                    txtNama.Text = emp.Nama;
                    txtAlamat.Text = emp.Alamat;
                    txtEmail.Text = emp.Email;
                }
                else
                {
                    // Jika tidak ada id → mode Tambah
                    lblTitle.Text = "Tambah Employee";
                }
            }
        }

        // Event saat tombol Simpan diklik
        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Ambil list employee dari Session
            var list = Employees;

            // Jika EditId ada → UPDATE data
            if (EditId.HasValue)
            {
                var emp = list.FirstOrDefault(x => x.Id == EditId.Value);

                if (emp != null)
                {
                    emp.Nama = txtNama.Text.Trim();
                    emp.Email = txtEmail.Text.Trim();
                    emp.Alamat = txtAlamat.Text.Trim();
                }
            }
            else
            {
                // Jika tidak ada EditId → CREATE data baru

                // Generate Id baru
                int newId = list.Count == 0 ? 1 : list.Max(x => x.Id) + 1;

                // Tambahkan employee baru ke list
                list.Add(new EmplModel
                {
                    Id = newId,
                    Nama = txtNama.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Alamat = txtAlamat.Text.Trim(),
                });
            }

            // Simpan kembali list ke Session
            Employees = list;

            // Kembali ke halaman list setelah simpan
            Response.Redirect("EmployeeList.aspx");
        }

        // Event saat tombol Kembali diklik
        protected void btnBack_Click(object sender, EventArgs e)
        {
            // Kembali ke halaman list tanpa menyimpan
            Response.Redirect("EmployeeList.aspx");
        }
    }
}
