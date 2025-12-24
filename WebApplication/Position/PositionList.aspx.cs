using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication.Repositories;

namespace WebApplication.Position
{
    public partial class PositionList : Page
    {
        // buat repository untuk akses database
        private readonly PositionRepository _repo = new PositionRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindGrid();
        }

        private void BindGrid()
        {
            // ambil data dari database
            var data = _repo.GetAll();

            gvPositions.DataSource = data;
            gvPositions.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("PositionForm.aspx");
        }

        protected void gvPositions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32(gvPositions.DataKeys[rowIndex].Value);

            if (e.CommandName == "EditRow")
            {
                Response.Redirect("PositionForm.aspx?id=" + id);
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
