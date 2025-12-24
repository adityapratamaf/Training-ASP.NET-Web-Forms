using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Repositories;


namespace WebApplication.Employee
{
    // Code-behind untuk halaman EmployeeForm.aspx
    public partial class EmployeeForm : System.Web.UI.Page
    {

        private readonly EmployeeRepository _repo = new EmployeeRepository();

        private int? EditId
        {
            get
            {
                if (int.TryParse(Request.QueryString["id"], out int id))
                    return id;
                return null;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (EditId.HasValue)
                {
                    lblTitle.Text = "Edit Employee";
                    var emp = _repo.GetById(EditId.Value);
                    if (emp == null)
                    {
                        Response.Redirect("~/Employee/EmployeeList.aspx");
                        return;
                    }
                    txtNama.Text = emp.Nama;
                    txtAlamat.Text = emp.Alamat;
                    txtEmail.Text = emp.Email;
                }
                else
                {
                    lblTitle.Text = "Add Employee";
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var nama = txtNama.Text.Trim();
            var alamat = txtAlamat.Text.Trim();
            var email = txtEmail.Text.Trim();
            if (EditId.HasValue)
            {
                var emp = _repo.GetById(EditId.Value);
                if (emp == null)
                {
                    Response.Redirect("~/Employee/EmployeeList.aspx");
                    return;
                }
                emp.Nama = nama;
                emp.Alamat = alamat;
                emp.Email = email;
                _repo.Update(emp);
            }
            else
            {
                var newEmp = new Models.Employee
                {
                    Nama = nama,
                    Alamat = alamat,
                    Email = email
                };
                _repo.Insert(newEmp);
            }
            Response.Redirect("~/Employee/EmployeeList.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Employee/EmployeeList.aspx");
        }

    }
}
