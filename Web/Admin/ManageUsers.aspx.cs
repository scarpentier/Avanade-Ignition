using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Admin
{
    public partial class ApproveUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            RefreshGrid();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index;
            if(e.CommandName=="Remove")
            {
                index = Convert.ToInt32(e.CommandArgument);
                var userId = (Guid)GridView1.DataKeys[index].Value;

                var db = new QuotesDBEntities();
                var selectedUser = db.aspnet_Membership.Single(m => m.UserId == userId);
                db.DeleteObject(selectedUser);
                db.SaveChanges();

                RefreshGrid();
            }

        }

        private void RefreshGrid()
        {
            GridView1.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var db = new QuotesDBEntities();

            foreach (GridViewRow row in GridView1.Rows)
            {
                var userId = (Guid)GridView1.DataKeys[row.DataItemIndex].Value;
                var user = db.aspnet_Membership.Single(m => m.UserId == userId);

                var isApproved = ((CheckBox)row.Cells[3].Controls[1]).Checked;
                
                if (user.IsApproved != isApproved)
                {
                    user.IsApproved = isApproved;
                    db.SaveChanges();
                }
            }

            RefreshGrid();
        }

    }
}