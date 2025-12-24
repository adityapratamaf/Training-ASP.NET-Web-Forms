using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Employee
{
    // Code-behind untuk halaman EmployeeList.aspx
    public partial class EmployeeList : System.Web.UI.Page
    {
        private readonly EmployeeRepository _repo = new EmployeeRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindGrid();
        }

        private void BindGrid()
        {
            // ambil data dari database
            var data = _repo.GetAll();

            gvEmployees.DataSource = data;
            gvEmployees.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeeForm.aspx");
        }

        protected void gvEmployees_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32(gvEmployees.DataKeys[rowIndex].Value);
            if (e.CommandName == "EditRow")
            {
                Response.Redirect("EmployeeForm.aspx?id=" + id);
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
