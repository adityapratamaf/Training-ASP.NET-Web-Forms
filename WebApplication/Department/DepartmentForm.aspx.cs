using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using WebApplication.Repositories;


namespace WebApplication.Department 
{
    // Code-behind untuk halaman DepartmentForm.aspx
    public partial class DepartmentForm : System.Web.UI.Page
    {
        private readonly DepartmentRepository _repo = new DepartmentRepository();

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
                    lblTitle.Text = "Edit Department";
                    var dep = _repo.GetById(EditId.Value);
                    if (dep == null)
                    {
                        Response.Redirect("~/Department/DepartmentList.aspx");
                        return;
                    }
                    txtName.Text = dep.Name;
                }
                else
                {
                    lblTitle.Text = "Add Department";
                }
            }
        }
        
        protected void btnSave_Click(object sender, EventArgs e)
            {
                var name = txtName.Text.Trim();
                if (EditId.HasValue)
                {
                    var dep = _repo.GetById(EditId.Value);
                    if (dep == null)
                    {
                        Response.Redirect("~/Department/DepartmentList.aspx");
                        return;
                    }
                    dep.Name = name;
                    _repo.Update(dep);
                }
                else
                {
                    var newDep = new Models.Department
                    {
                        Name = name
                    };
                    _repo.Insert(newDep);
                }
                Response.Redirect("~/Department/DepartmentList.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Department/DepartmentList.aspx");
        }

    }
}