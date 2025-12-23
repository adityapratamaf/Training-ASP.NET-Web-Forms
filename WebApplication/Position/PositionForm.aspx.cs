using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

using PosModel = WebApplication.Models.Position;

namespace WebApplication.Position
{
    public partial class PositionForm : Page
    {
        private const string SessionKey = "Positions";

        private List<PosModel> Positions
        {
            get
            {
                if (Session[SessionKey] == null)
                    Session[SessionKey] = new List<PosModel>();
                return (List<PosModel>)Session[SessionKey];
            }
            set { Session[SessionKey] = value; }
        }

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
                    lblTitle.Text = "Edit Position";

                    var pos = Positions.FirstOrDefault(x => x.Id == EditId.Value);
                    if (pos == null)
                    {
                        Response.Redirect("PositionList.aspx");
                        return;
                    }

                    txtName.Text = pos.Name;
                    txtLevel.Text = pos.Level;
                }
                else
                {
                    lblTitle.Text = "Tambah Position";
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var list = Positions;

            if (EditId.HasValue)
            {
                var pos = list.FirstOrDefault(x => x.Id == EditId.Value);
                if (pos != null)
                {
                    pos.Name = txtName.Text.Trim();
                    pos.Level = txtLevel.Text.Trim();
                }
            }
            else
            {
                int newId = list.Count == 0 ? 1 : list.Max(x => x.Id) + 1;

                list.Add(new PosModel
                {
                    Id = newId,
                    Name = txtName.Text.Trim(),
                    Level = txtLevel.Text.Trim()
                });
            }

            Positions = list;
            Response.Redirect("PositionList.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("PositionList.aspx");
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtName.Text = string.Empty;
            txtLevel.Text = string.Empty;
        }

        protected void btnMove_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Employee/EmployeeList.aspx");
        }



    }
}