// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomerInfo.ascx.cs" company="Avanade">
//   Avanade Ignition Team 4 © 2012
// </copyright>
// <summary>
//   Defines the CustomerInfo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web.UserControls
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Security;

    public partial class CustomerInfo : System.Web.UI.UserControl
    {
        
        /// <summary>
        /// Event fired when a save or update operation is complete
        /// </summary>
        public event EventHandler SaveComplete;

        /// <summary>
        /// Person's First Name
        /// </summary>
        public string FirstName
        {
            get
            {
                return this.txtFirstName.Text;
            }
            set
            {
                this.txtFirstName.Text = value;
            }
        }

        /// <summary>
        /// Person's Last Name
        /// </summary>
        public string LastName
        {
            get
            {
                return this.txtLastName.Text;
            }
            set
            {
                this.txtLastName.Text = value;
            }
        }

        /// <summary>
        /// Person's Social Security Number
        /// </summary>
        public string Ssn
        {
            get
            {
                return this.txtSocialSecurityNumber.Text;
            }
            set
            {
                this.txtSocialSecurityNumber.Text = value;
            }
        }

        /// <summary>
        /// Person's Address
        /// </summary>
        public string Address
        {
            get
            {
                return this.txtAddressLine1.Text;
            }
            set
            {
                this.txtAddressLine1.Text = value;
            }
        }

        /// <summary>
        /// Person's City
        /// </summary>
        public string City
        {
            get
            {
                return this.txtCity.Text;
            }
            set
            {
                this.txtCity.Text = value;
            }
        }

        /// <summary>
        /// Person's ZipCode
        /// </summary>
        public string ZipCode
        {
            get
            {
                return this.txtAddressZip.Text;
            }
            set
            {
                this.txtAddressZip.Text = value;
            }
        }
        /// <summary>
        /// Customer's State
        /// </summary>
        public int StateId
        {
            get
            {
                return int.Parse(ddlState.SelectedValue);
            }
            set
            {
                ddlState.SelectedValue = value.ToString();
            }
        }

        
        /// <summary>
        /// Customer's Date of Birth
        /// </summary>
        public DateTime DateOfBirth
        {
            get
            {
                return Convert.ToDateTime(calendarPersonDOB.Text);
            }
            set
            {
                calendarPersonDOB.Text = value.ToShortDateString();
            }
        }
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
        
        public void Read(Quote quote)
        {
            CurrentQuote = quote;

            this.FirstName = CurrentQuote.CustomerFirstName;
            this.LastName = CurrentQuote.CustomerLastName;
            this.DateOfBirth = CurrentQuote.DateOfBirth;
            this.Ssn = CurrentQuote.Ssn;
            this.Address = CurrentQuote.Address;
            this.City = CurrentQuote.City;
            this.ZipCode = CurrentQuote.Zip;
            this.StateId = CurrentQuote.StateId;
            this.ForceMultiCarDiscount = CurrentQuote.ForceMultiCarDiscount;
            this.LessThan3Yrs = CurrentQuote.HasLessThan3YrsDriving;
            this.ClaimsInPast5Yrs = CurrentQuote.ClaimsInPast5Yrs;
            this.MovingViolations = CurrentQuote.HasMovingViolations;
            this.PreviousCarrier = CurrentQuote.PreviousCarrier;
        }

        protected string PreviousCarrier
        {
            get
            {
                return this.txtPreviousCarrier.Text;
            }
            set
            {
                this.txtPreviousCarrier.Text = value;
            }
        }

        protected bool MovingViolations
        {
            get
            {
                return this.cbMovingViolation.Checked;
            }
            set
            {
                this.cbMovingViolation.Checked = value;
            }
        }

        protected bool ClaimsInPast5Yrs
        {
            get
            {
                return this.cbClaimPenalty.Checked;
            }
            set
            {
                this.cbClaimPenalty.Checked= value;
            }
        }

        protected bool LessThan3Yrs
        {
            get
            {
                return this.cbLessThan3years.Checked;
            }
            set
            {
                this.cbLessThan3years.Checked = value;
            }
        }

        protected bool ForceMultiCarDiscount
        {
            get
            {
                return this.cbMultiCar.Checked;
            }
            set
            {
                this.cbMultiCar.Checked = value;
            }
        }

        private void Update()
        {
            CurrentQuote.CustomerFirstName = this.FirstName;
            CurrentQuote.CustomerLastName = this.LastName;
            CurrentQuote.DateOfBirth = this.DateOfBirth;
            CurrentQuote.Ssn = this.Ssn;
            CurrentQuote.Address = this.Address;
            CurrentQuote.City = this.City;
            CurrentQuote.Zip = this.ZipCode;
            CurrentQuote.StateId = this.StateId;
            CurrentQuote.ForceMultiCarDiscount = this.ForceMultiCarDiscount;
            CurrentQuote.HasLessThan3YrsDriving = this.LessThan3Yrs;
            CurrentQuote.ClaimsInPast5Yrs = this.ClaimsInPast5Yrs;
            CurrentQuote.HasMovingViolations = this.MovingViolations;
            CurrentQuote.PreviousCarrier = this.PreviousCarrier;

            var db = new QuotesDBEntities();

            if (CurrentQuote.ID == 0)
            {
                db.Quotes.AddObject(CurrentQuote);

                if (cbAddAsDriver.Checked)
                {
                    var driver = new Driver
                        {
                            Quote = CurrentQuote,
                            FirstName = CurrentQuote.CustomerFirstName,
                            LastName = CurrentQuote.CustomerLastName,
                            DLState = CurrentQuote.StateId,
                            DateOfBirth = CurrentQuote.DateOfBirth,
                            Ssn = CurrentQuote.Ssn,
                            DriverLicenseNumber = string.Empty
                        };
                    db.Drivers.AddObject(driver);
                }
            }
            else
            {
                var quote = db.Quotes.Single(q => q.ID == CurrentQuote.ID);

                quote.CustomerFirstName = this.FirstName;
                quote.CustomerLastName = this.LastName;
                quote.DateOfBirth = this.DateOfBirth;
                quote.Ssn = this.Ssn;
                quote.Address = this.Address;
                quote.City = this.City;
                quote.Zip = this.ZipCode;
                quote.StateId = this.StateId;
                quote.ForceMultiCarDiscount = this.ForceMultiCarDiscount;
                quote.HasLessThan3YrsDriving = this.LessThan3Yrs;
                quote.ClaimsInPast5Yrs = this.ClaimsInPast5Yrs;
                quote.HasMovingViolations = this.MovingViolations;
                quote.PreviousCarrier = this.PreviousCarrier;
            }

            db.SaveChanges();

            this.SaveComplete(this, null);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            extender.EndDate = DateTime.Now.AddYears(-16);           
        }

        public void Create()
        {
            CurrentQuote = new Quote();
            CurrentQuote.DateCreated = DateTime.Now;
            CurrentQuote.UserId = (Guid)Membership.GetUser(HttpContext.Current.User.Identity.Name).ProviderUserKey;
            CurrentQuote.Incomplete = true;

            CurrentQuote.DateOfBirth = DateTime.Now.AddYears(-16);
            extender.SelectedDate = CurrentQuote.DateOfBirth;

            cbAddAsDriver.Visible = true;
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            this.Update();
        }
    }
}
