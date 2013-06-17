// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DriverInfo.ascx.cs" company="Avanade">
//   Avanade Ignition Team 4 © 2012
// </copyright>
// <summary>
//   UserControl used when showing the details of a single driver
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web.UserControls
{
    using System;
    using System.Linq;

    /// <summary>
    /// UserControl used when showing the details of a single driver
    /// </summary>
    public partial class DriverInfo : System.Web.UI.UserControl
    {
        /// <summary>
        /// Event fired when a save or update operation is complete
        /// </summary>
        public event EventHandler SaveComplete;

        /// <summary>
        /// Gets the driver's First Name
        /// </summary>
        public string FirstName
        {
            get { return txtFirstName.Text; }
            private set { txtFirstName.Text = value; }
        }

        /// <summary>
        /// Gets the driver's Last Name
        /// </summary>
        public string LastName
        {
            get { return txtLastName.Text; }
            private set { txtLastName.Text = value; }
        }

        /// <summary>
        /// Gets the driver's Social Security Number
        /// </summary>
        public string SocialSecurityNumber
        {
            get { return txtSocialSecurityNumber.Text; }
            private set { txtSocialSecurityNumber.Text = value; }
        }

        /// <summary>
        /// Gets the driver's License Number
        /// </summary>
        public string LicenseNumber
        {
            get { return txtLicenseNumber.Text; }
            private set { txtLicenseNumber.Text = value; }
        }

        /// <summary>
        /// Gets the driver's License State
        /// </summary>
        public int LicenseStateId
        {
            get { return int.Parse(ddlLicenceState.SelectedValue); }
            private set { ddlLicenceState.SelectedValue = value.ToString(); }
        }

        /// <summary>
        /// Gets the driver's Date of Birth
        /// </summary>
        public DateTime DateOfBirth
        {
            get
            {
                return Convert.ToDateTime(calendarDriverDOB.Text);
            }
            private set
            {
                calendarDriverDOB.Text = value.ToShortDateString();
            }
        }

        /// <summary>
        /// Gets or sets if the driver attended save driving school
        /// </summary>
        public bool AttendedSafeDrivingSchool
        {
            get { return this.cbAttendedSafeSchool.Checked; }
            private set { this.cbAttendedSafeSchool.Checked = value; }
        }

        /// <summary>
        /// Gets the current driver object
        /// </summary>
        public Driver CurrentDriver
        {
            get
            {
                // HACK: THIS IS A HACK; WE SHOULD NOT STORE THIS IN THE VIEWSTATE
                return (Driver)ViewState["CurrentDriver"];
            }

            private set
            {
                ViewState["CurrentDriver"] = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            extender.EndDate = DateTime.Now.AddYears(-16);
        }

        /// <summary>
        /// Creates a new driver
        /// </summary>
        /// <param name="quote">Quote associated with that driver</param>
        public void Create(Quote quote)
        {
            CurrentDriver = new Driver { QuoteID = quote.ID };
            CurrentDriver.DateOfBirth = DateTime.Now.AddYears(-16);
            extender.SelectedDate = CurrentDriver.DateOfBirth;

            // Clear all the fields
            this.Clear();
        }

        /// <summary>
        /// Clear data in the controls
        /// </summary>
        private void Clear()
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtSocialSecurityNumber.Text = string.Empty;
            txtLicenseNumber.Text = string.Empty;
            cbAttendedSafeSchool.Checked = false;
        }

        /// <summary>
        /// Loads an existing driver
        /// </summary>
        /// <param name="driver">Driver object to load</param>
        public void Read(Driver driver)
        {
            CurrentDriver = driver;

            this.FirstName = CurrentDriver.FirstName;
            this.LastName = CurrentDriver.LastName;
            this.DateOfBirth = CurrentDriver.DateOfBirth;
            this.SocialSecurityNumber = CurrentDriver.Ssn;
            this.LicenseNumber = CurrentDriver.DriverLicenseNumber;
            this.LicenseStateId = CurrentDriver.DLState;
            this.AttendedSafeDrivingSchool = CurrentDriver.SafeDrivingSchool;
        }

        /// <summary>
        /// Updates the driver information in the database
        /// </summary>
        /// <remarks>Fires the SaveComplete Event</remarks>
        private void Update()
        {
            CurrentDriver.FirstName = this.FirstName;
            CurrentDriver.LastName = this.LastName;
            CurrentDriver.DateOfBirth = this.DateOfBirth;
            CurrentDriver.Ssn = this.SocialSecurityNumber;
            CurrentDriver.DriverLicenseNumber = this.LicenseNumber;
            CurrentDriver.DLState = this.LicenseStateId;
            CurrentDriver.SafeDrivingSchool = this.AttendedSafeDrivingSchool;

            var db = new QuotesDBEntities();

            if (CurrentDriver.ID == 0)
            {
                db.Drivers.AddObject(CurrentDriver);
            }
            else
            {
                var driver = db.Drivers.Single(i => i.ID == CurrentDriver.ID);

                driver.FirstName = this.FirstName;
                driver.LastName = this.LastName;
                driver.DateOfBirth = this.DateOfBirth;
                driver.Ssn = this.SocialSecurityNumber;
                driver.DriverLicenseNumber = this.LicenseNumber;
                driver.DLState = this.LicenseStateId;
                driver.SafeDrivingSchool = this.AttendedSafeDrivingSchool;
            }
            
            db.SaveChanges();

            this.SaveComplete(this, null);
        }

        protected void saveDriver_Click(object sender, EventArgs e)
        {
            this.Update();
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            this.SaveComplete(this,null);


        }

        protected void cancel_Click1(object sender, EventArgs e)
        {
            this.Clear();
            this.SaveComplete(this, null);
        }

    }
}
