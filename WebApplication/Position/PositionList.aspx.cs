using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

using PosModel = WebApplication.Models.Position;

namespace WebApplication.Position
{
    public partial class PositionList : Page
    {
        private const string SessionKey = "Positions";

        // Ambil data Position dari Session (sekali simpan, dipakai terus)
        private List<PosModel> Positions
        {
            get
            {
                if (Session[SessionKey] == null)
                {
                    // data awal (dummy)
                    Session[SessionKey] = new List<PosModel>
                    {
                        new PosModel { Id = 1, Name = "Staff", Level = "Junior" },
                        new PosModel { Id = 2, Name = "Supervisor", Level = "Senior" }
                    };
                }
                return (List<PosModel>)Session[SessionKey];
            }
            set { Session[SessionKey] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) BindGrid();
        }

        private void BindGrid()
        {
            gvPositions.DataSource = Positions.OrderBy(x => x.Id).ToList();
            gvPositions.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            // aman pakai path absolut
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
                var list = Positions;
                var pos = list.FirstOrDefault(x => x.Id == id);
                if (pos != null)
                {
                    list.Remove(pos);
                    Positions = list;
                }
                BindGrid();
            }
        }
    }
}
