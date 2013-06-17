// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Create.aspx.cs" company="Avanade">
//   Avanade Ignition Team 4 © 2012
// </copyright>
// <summary>
//   Defines the Create type.
// </summary>
// <comment>
//   For the brave souls who get this far: You are the chosen ones, 
//   the valiant knights of programming who toil away, without rest,
//   fixing our most awful code. To you, true saviors, kings of men,
//   I say this: never gonna give you up, never gonna let you down,
//   never gonna run around and desert you. Never gonna make you cry,
//   never gonna say goodbye. Never gonna tell a lie and hurt you.
// </comment>
// --------------------------------------------------------------------------------------------------------------------

namespace Web
{
    using System;
    using System.Linq;

    using Web.UserControls;

    public partial class Create : System.Web.UI.Page
    {
        /// <summary>
        /// Gets the current quote object
        /// </summary>
        public Quote CurrentQuote
        {
            get
            {
                return (Quote)ViewState["CurrentQuote"];
            }
            set
            {
                ViewState["CurrentQuote"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ucCustomerInfo.SaveComplete += new EventHandler(ucCustomerInfo_SaveComplete);

                if (Request.QueryString["id"] != null)
                {
                    var id = int.Parse(Request.QueryString["id"]);
                    this.LoadExistingQuote(id);

                    //ucCustomerInfo.Read();
                }
                else
                {
                    if (!IsPostBack)
                        ucCustomerInfo.Create();
                }
        }


        /// <summary>
        /// Loads a previously saved, incomplete quote
        /// </summary>
        /// <param name="id">The ID of the quote to load</param>
        private void LoadExistingQuote(int id)
        {
            if (CurrentQuote == null || CurrentQuote.ID != id)
            {
                var db = new QuotesDBEntities();
                this.CurrentQuote = db.Quotes.Single(i => i.ID == id);
                ucCustomerInfo.Read(CurrentQuote);
            } 

            // TODO: Make sure the quote is incomplete, we don't want to load a completed quote
            
            CompleteCustomerInfo();
        }

        void ucCustomerInfo_SaveComplete(object sender, EventArgs e)
        {
            var quoteid = ((CustomerInfo)sender).CurrentQuote.ID;
            Response.Redirect(string.Format("Create.aspx?id={0}", quoteid));
        }

        /// <summary>
        /// Shows the Drivers and Vehicles tabs as well as the Process quote button
        /// </summary>
        private void CompleteCustomerInfo()
        {
            TabContainer1.Visible = true;
            CheckBox1.Visible = false;
            btnFinish.Visible = true;

            // TODO: Since we're using the viewstate to store the Current Quote, is this still necessary?
            this.ucDriversTab.LoadQuote(CurrentQuote);
            this.ucVehiclesTab.LoadQuote(CurrentQuote);
        }

        protected void btnFinish_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("Finish.aspx?id={0}", CurrentQuote.ID));
        }
    }
}
