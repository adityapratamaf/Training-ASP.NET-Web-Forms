using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using WebApplication.Models;

// Alias untuk model Employee
// Dipakai agar tidak bentrok dengan namespace WebApplication.Employee
using EmplModel = WebApplication.Models.Employee;

namespace WebApplication.Employee
{
    // Code-behind untuk halaman EmployeeList.aspx
    public partial class EmployeeList : System.Web.UI.Page
    {
        // Key Session untuk menyimpan data employee
        // Digunakan agar konsisten di seluruh halaman Employee
        private const string SessionKey = "Employees";

        // Property untuk mengambil dan menyimpan list Employee dari Session
        private List<EmplModel> Employees
        {
            get
            {
                // Jika Session belum ada (pertama kali buka halaman)
                // maka inisialisasi data dummy
                if (Session[SessionKey] == null)
                {
                    Session[SessionKey] = new List<EmplModel>
                    {
                        new EmplModel
                        {
                            Id = 1,
                            Nama = "A",
                            Alamat = "Jakarta",
                            Email = "a@test.com"
                        },
                        new EmplModel
                        {
                            Id = 2,
                            Nama = "B",
                            Alamat = "Jakarta",
                            Email = "b@test.com"
                        }
                    };
                }

                // Ambil data employee dari Session
                return (List<EmplModel>)Session[SessionKey];
            }
            set
            {
                // Simpan kembali data employee ke Session
                Session[SessionKey] = value;
            }
        }

        // Event yang dijalankan setiap halaman EmployeeList dibuka
        protected void Page_Load(object sender, EventArgs e)
        {
            // !IsPostBack memastikan data hanya di-bind
            // saat halaman pertama kali dibuka
            if (!IsPostBack)
                BindGrid();
        }

        // Method untuk mengisi GridView dengan data Employee
        private void BindGrid()
        {
            // Ambil data dari Session, urutkan berdasarkan Id
            gvEmployees.DataSource = Employees
                .OrderBy(x => x.Id)
                .ToList();

            // Tampilkan data ke GridView
            gvEmployees.DataBind();
        }

        // Event saat tombol Add diklik
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            // Redirect ke halaman form untuk menambah employee baru
            Response.Redirect("EmployeeForm.aspx");
        }

        // Event saat tombol Edit / Delete di GridView diklik
        protected void gvEmployees_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Ambil index baris yang diklik
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            // Ambil Id employee dari DataKeys GridView
            int id = Convert.ToInt32(gvEmployees.DataKeys[rowIndex].Value);

            // Jika command Edit
            if (e.CommandName == "EditRow")
            {
                // Redirect ke form dengan membawa id employee
                Response.Redirect("EmployeeForm.aspx?id=" + id);
                return;
            }

            // Jika command Delete
            if (e.CommandName == "DeleteRow")
            {
                // Ambil list employee dari Session
                var list = Employees;

                // Cari employee berdasarkan id
                var emp = list.FirstOrDefault(x => x.Id == id);

                // Jika employee ditemukan, hapus dari list
                if (emp != null)
                {
                    list.Remove(emp);

                    // Simpan kembali list ke Session
                    Employees = list;
                }

                // Refresh GridView setelah delete
                BindGrid();
            }
        }
    }
}
