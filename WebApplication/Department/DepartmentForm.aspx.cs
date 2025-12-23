using System;
using System.Collections.Generic;
using System.Linq;

// Alias: DeptModel dipakai sebagai nama singkat untuk model Department
// Tujuannya menghindari konflik nama dengan namespace WebApplication.Department
using DeptModel = WebApplication.Models.Department;

namespace WebApplication.Department
{
    // Code-behind untuk halaman DepartmentForm.aspx
    public partial class DepartmentForm : System.Web.UI.Page
    {
        // Key Session untuk menyimpan data Department
        // Dipakai bersama oleh List & Form
        private const string SessionKey = "Departments";

        // Property untuk mengambil / menyimpan list Department dari Session
        private List<DeptModel> Departments
        {
            get
            {
                // Jika Session belum ada, buat list kosong
                // Ini mencegah NullReferenceException
                if (Session[SessionKey] == null)
                    Session[SessionKey] = new List<DeptModel>();

                // Ambil data dari Session
                return (List<DeptModel>)Session[SessionKey];
            }

            // Simpan kembali list ke Session
            set { Session[SessionKey] = value; }
        }

        // Property untuk mengambil ID dari query string (mode edit)
        // Contoh URL: DepartmentForm.aspx?id=3
        private int? EditId
        {
            get
            {
                int id;
                // Jika id ada dan valid → return id
                // Jika tidak ada → return null (mode tambah)
                return int.TryParse(Request.QueryString["id"], out id) ? id : (int?)null;
            }
        }

        // Event yang dijalankan saat halaman pertama kali dibuka
        protected void Page_Load(object sender, EventArgs e)
        {
            // Pastikan kode hanya dijalankan sekali (bukan saat postback)
            if (!IsPostBack)
            {
                // Jika ada id → mode Edit
                if (EditId.HasValue)
                {
                    lblTitle.Text = "Edit Department";

                    // Cari data department berdasarkan id
                    var dep = Departments.FirstOrDefault(x => x.Id == EditId.Value);

                    // Jika data tidak ditemukan → kembali ke list
                    if (dep == null)
                    {
                        Response.Redirect("DepartmentList.aspx");
                        return;
                    }

                    // Isi textbox dengan data yang akan diedit
                    txtName.Text = dep.Name;
                }
                else
                {
                    // Jika tidak ada id → mode Tambah
                    lblTitle.Text = "Tambah Department";
                }
            }
        }

        // Event saat tombol Simpan diklik
        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Ambil list department dari Session
            var list = Departments;

            // Jika EditId ada → UPDATE data
            if (EditId.HasValue)
            {
                // Cari department yang akan diupdate
                var dep = list.FirstOrDefault(x => x.Id == EditId.Value);
                // Update nama department
                if (dep != null)
                    dep.Name = txtName.Text.Trim();
            }
            else
            {
                // Jika tidak ada EditId → CREATE data baru

                // Generate Id baru
                int newId = list.Count == 0 ? 1 : list.Max(x => x.Id) + 1;
                // Tambahkan department baru ke list
                list.Add(new DeptModel { Id = newId, Name = txtName.Text.Trim() });
            }

            // Simpan kembali list ke Session
            Departments = list;
            Response.Redirect("DepartmentList.aspx");
        }

        // Event saat tombol Kembali diklik
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("DepartmentList.aspx");
        }

    }
}