using System;
using System.Web.UI;
using WebApplication.Repositories;

using PosModel = WebApplication.Models.Position;

namespace WebApplication.Position
{
    public partial class PositionForm : Page
    {
        // ✅ pakai repository untuk akses database
        private readonly PositionRepository _repo = new PositionRepository();

        // ambil id dari querystring untuk mode edit
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
                // mode edit kalau ada id
                if (EditId.HasValue)
                {
                    lblTitle.Text = "Edit Position";

                    // ✅ ambil data dari DB
                    var pos = _repo.GetById(EditId.Value);

                    // kalau tidak ditemukan balik ke list
                    if (pos == null)
                    {
                        Response.Redirect("~/Position/PositionList.aspx");
                        return;
                    }

                    // isi form
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
            // ambil input
            var name = txtName.Text.Trim();
            var level = txtLevel.Text.Trim();

            // ✅ jika edit → update DB
            if (EditId.HasValue)
            {
                _repo.Update(new PosModel
                {
                    Id = EditId.Value,
                    Name = name,
                    Level = level
                });
            }
            else
            {
                // ✅ jika tambah → insert DB
                _repo.Insert(new PosModel
                {
                    Name = name,
                    Level = level
                });
            }

            // kembali ke list
            Response.Redirect("~/Position/PositionList.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Position/PositionList.aspx");
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
