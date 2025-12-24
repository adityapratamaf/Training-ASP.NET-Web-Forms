using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Department
{
    public partial class DepartmentList : Page
    {
        private readonly DepartmentRepository _repo = new DepartmentRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindGrid();
        }

        protected void BindGrid()
        {             
            var data = _repo.GetAll();
            gvDepartments.DataSource = data;
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
                // delete dari database
                _repo.Delete(id);
                // refresh grid
                BindGrid();
            }
        }

    }
}
