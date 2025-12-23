using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication.Models;
using DeptModel = WebApplication.Models.Department;

namespace WebApplication.Department
{
    public partial class DepartmentList : Page
    {
        private const string SessionKey = "Departments";

        private List<DeptModel> Departments
        {
            get
            {
                if (Session[SessionKey] == null)
                {
                    Session[SessionKey] = new List<DeptModel>
                    {
                        new DeptModel { Id = 1, Name = "Operasional" },
                        new DeptModel { Id = 2, Name = "Teknik" }
                    };
                }
                return (List<DeptModel>)Session[SessionKey];
            }
            set { Session[SessionKey] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) BindGrid();
        }

        private void BindGrid()
        {
            gvDepartments.DataSource = Departments.OrderBy(x => x.Id).ToList();
            gvDepartments.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("DepartmentForm.aspx");
        }

        protected void gvDepartments_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32(gvDepartments.DataKeys[rowIndex].Value);

            if (e.CommandName == "EditRow")
            {
                Response.Redirect("DepartmentForm.aspx?id=" + id);
                return;
            }

            if (e.CommandName == "DeleteRow")
            {
                var list = Departments;
                var dep = list.FirstOrDefault(x => x.Id == id);
                if (dep != null)
                {
                    list.Remove(dep);
                    Departments = list;
                }
                BindGrid();
            }
        }
    }
}
