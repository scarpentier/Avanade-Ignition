// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VehicleInfo.ascx.cs" company="Avanade">
//   Avanade Ignition Team 4 © 2012
// </copyright>
// <summary>
//   Defines the VehicleInfo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web.UserControls
{
    using System;
    using System.Linq;
    using System.Web.UI.WebControls;
    using System.Collections.Generic;


    /// <summary>
    /// UserControl used when showing the details of a single vehicle
    /// </summary>
    public partial class VehicleInfo : System.Web.UI.UserControl
    {
        /// <summary>
        /// Event fired when a save or update operation is complete
        /// </summary>
        public event EventHandler SaveComplete;

        /// <summary>
        /// Get the vehicle's VIN
        /// </summary>
        public string VehicleVin { get { return txtVin.Text; } set { txtVin.Text = value; } }

        /// <summary>
        /// Get the Vehicle make
        /// </summary>
        public string VehicleMake { get { return txtMake.Text; } set { txtMake.Text = value; } }

        /// <summary>
        /// Get the vehicle model
        /// </summary>
        public string VehicleModel { get { return txtModel.Text; } set { txtModel.Text = value; } }

        /// <summary>
        /// Get the vehicle year
        /// </summary>
        public string VehicleYear { get { return txtYear.Text; } set { txtYear.Text = value; } }

        /// <summary>
        /// Get the Vehicle Value
        /// </summary>
        public string VehicleValue { get { return txtValue.Text; } set { txtValue.Text = value; } }

        /// <summary>
        /// Get the primary driver of the vehicle
        /// </summary>
        public string VehiclePrimaryDriver
        {
            get { return Convert.ToString(ddlPrimaryDriver.SelectedValue); }
            set { ddlPrimaryDriver.SelectedValue = value; }
        }

        /// <summary>
        /// Get the vehicle's annual mileage
        /// </summary>
        public string VehicleAnnualMileage { get { return txtAnnualMileage.Text; } set { txtAnnualMileage.Text = value; } }

        /// <summary>
        /// Get the number of days the vehicle is drivcen
        /// </summary>
        public string VehicleDaysperwk { get { return txtDaysDrivenperwk.Text; } set { txtDaysDrivenperwk.Text = value; } }

        /// <summary>
        /// Get the number of miles driven to work
        /// </summary>
        public string VehicleMilesToWork { get { return txtMilesToWork.Text; } set { txtMilesToWork.Text = value; } }

        /// <summary>
        /// Check to see if day time running lights are installed on the vehicle
        /// </summary>
        public bool daytimerunninglights 
        {
            get { return daytimeRunningLights.Checked; }
            set { daytimeRunningLights.Checked = value; }
        }

        /// <summary>
        /// Check for anti-lock brakes on the vehicle
        /// </summary>
        public bool antilockbrakes
        {
            get { return antiLockBrakes.Checked; }
            set { antiLockBrakes.Checked = value; }
        }

        /// <summary>
        /// Check for anti-theft mechanisms on the vehicle
        /// </summary>
        public bool antitheft
        {
            get { return antiTheft.Checked; }
            set { antiTheft.Checked = value; }
        }

        /// <summary>
        /// Check to see if vehicle qualifies for reduced use discount
        /// </summary>
        public bool reduceuse
        {
            get { return reduceUse.Checked; }
            set { reduceUse.Checked = value; }
        }

        /// <summary>
        /// Check to see if vehicle to stored in a different garage
        /// </summary>
        public bool garageaddress
        {
            get { return garageAddress.Checked; }
            set { garageAddress.Checked = value; }
        }

        /// <summary>
        /// Check to see if vehicle has passive restraints installed
        /// </summary>
        public bool passiverestraint
        {
            get { return passiveRestraint.Checked; }
            set { passiveRestraint.Checked = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Populate the fields with the vehicle to be edited
        /// </summary>
        /// <param name="id"></param>
        public void Read(Vehicle car) {
            
            var db = new QuotesDBEntities();

            CurrentVehicle = car;
            VehicleAnnualMileage = Convert.ToString(CurrentVehicle.AnnualMileage);
            VehicleVin = CurrentVehicle.Vin;
            VehicleMake = CurrentVehicle.Make;
            VehicleDaysperwk = Convert.ToString(CurrentVehicle.DaysDrivenPerWeek);
            VehicleModel = CurrentVehicle.Model;
            VehicleMilesToWork = Convert.ToString(CurrentVehicle.MilesDrivenToWork);
            VehicleValue = Convert.ToString(CurrentVehicle.CurrentValue);
            VehicleYear = Convert.ToString(CurrentVehicle.Year);
            daytimerunninglights = CurrentVehicle.DaytimeRunningLights;
            garageaddress = CurrentVehicle.DifferentGarageAddress;
            reduceuse = CurrentVehicle.ReducedUsedDiscount;
            antitheft = CurrentVehicle.AntiTheft;
            antilockbrakes = CurrentVehicle.AntiLock;
            passiverestraint = CurrentVehicle.PassiveRestraints;

        }

        /// <summary>
        /// Retrieve the selected vehicle from the gridview
        /// </summary>
        public Vehicle CurrentVehicle
        {
            get
            {
                return (Vehicle)ViewState["CurrentVehicle"];
            }
            set
            {
                ViewState["CurrentVehicle"] = value;
            }
        }


        /// <summary>
        /// Creates a new vehicle
        /// </summary>
        /// <param name="quote">Quote associated with that vehicle</param>
        public void Create(Quote quote)
        {


            CurrentVehicle = new Vehicle { QuoteId = quote.ID };
            ClearAll();
        }
            
            // Clear all the fields
        private void ClearAll(){
            
            txtYear.Text = string.Empty;
            txtVin.Text = string.Empty;
            txtMake.Text = string.Empty;
            txtModel.Text = string.Empty;
            txtMilesToWork.Text = string.Empty;
            txtValue.Text = string.Empty;
            txtDaysDrivenperwk.Text = string.Empty;
            txtAnnualMileage.Text = string.Empty;
            daytimeRunningLights.Checked = false;
            antiLockBrakes.Checked = false;
            passiveRestraint.Checked = false;
            reduceUse.Checked = false;
            garageAddress.Checked = false;
            antiTheft.Checked = false;

        }

        /// <summary>
        /// Updates the vehicle information in the database
        /// </summary>
        /// <remarks>Fires the SaveComplete Event</remarks>

        private void Update()
        {

            CurrentVehicle.AnnualMileage = Convert.ToInt32(VehicleAnnualMileage);
            CurrentVehicle.Vin = VehicleVin;
            CurrentVehicle.Make = VehicleMake;
            CurrentVehicle.DaysDrivenPerWeek = Convert.ToByte(VehicleDaysperwk);
            CurrentVehicle.Model = VehicleModel;
            CurrentVehicle.MilesDrivenToWork = Convert.ToInt16(VehicleMilesToWork);
            CurrentVehicle.CurrentValue = Convert.ToDecimal(VehicleValue);
            CurrentVehicle.Year = Convert.ToInt32(VehicleYear);
            CurrentVehicle.DaytimeRunningLights = daytimerunninglights;
            CurrentVehicle.DifferentGarageAddress = garageaddress;
            CurrentVehicle.ReducedUsedDiscount = reduceuse;
            CurrentVehicle.AntiTheft = antitheft;
            CurrentVehicle.AntiLock = antilockbrakes;
            CurrentVehicle.PassiveRestraints = passiverestraint;
            CurrentVehicle.PrimaryDriver = Convert.ToInt32(VehiclePrimaryDriver);
            
            var db = new QuotesDBEntities();

            if (CurrentVehicle.ID == 0)
            {
                db.Vehicles.AddObject(CurrentVehicle);
            }
            else
            {
                var vehicle = db.Vehicles.Single(i => i.ID == CurrentVehicle.ID);

                vehicle.QuoteId = CurrentVehicle.QuoteId;
                vehicle.AnnualMileage = CurrentVehicle.AnnualMileage;
                vehicle.Vin = CurrentVehicle.Vin;
                vehicle.Make = CurrentVehicle.Make;
                vehicle.DaysDrivenPerWeek = CurrentVehicle.DaysDrivenPerWeek;
                vehicle.Model = CurrentVehicle.Model;
                vehicle.MilesDrivenToWork = CurrentVehicle.MilesDrivenToWork;
                vehicle.CurrentValue = CurrentVehicle.CurrentValue;
                vehicle.Year = CurrentVehicle.Year;
                vehicle.DaytimeRunningLights = CurrentVehicle.DaytimeRunningLights;
                vehicle.DifferentGarageAddress = CurrentVehicle.DifferentGarageAddress;
                vehicle.ReducedUsedDiscount= CurrentVehicle.ReducedUsedDiscount;
                vehicle.AntiTheft= CurrentVehicle.AntiTheft;
                vehicle.AntiLock = CurrentVehicle.AntiLock;
                vehicle.PassiveRestraints = CurrentVehicle.PassiveRestraints;
                vehicle.PrimaryDriver = CurrentVehicle.PrimaryDriver;
            }
            
            db.SaveChanges();

            this.SaveComplete(this, null);
        }

        /// <summary>
        /// Update the primary driver list to only show the drivers asscoiated to that quote
        /// </summary>
        public void PrimaryDriver(){

            ddlPrimaryDriver.Items.Clear();
            
            string Fullname;
            var db = new QuotesDBEntities();
            List<Driver> primarydriver = new List<Driver>();

            Quote CurrentQuote = new Quote();

            CurrentQuote = db.Quotes.Single(i=>i.ID==CurrentVehicle.QuoteId);
            if (CurrentVehicle.PrimaryDriver == 0 || CurrentQuote.Incomplete)
               primarydriver = db.Drivers.Where(i => i.QuoteID == CurrentVehicle.QuoteId).ToList();
            else
               primarydriver = db.Drivers.Where(i=>i.ID == CurrentVehicle.PrimaryDriver).ToList();
            
            foreach (Driver driver in primarydriver)
            {   
                Fullname = driver.FirstName + ' ' + driver.LastName;
                ListItem listItem = new ListItem(Fullname,Convert.ToString(driver.ID));
                ddlPrimaryDriver.Items.Add(listItem);
            }
        }

        /// <summary>
        /// Clear any changes done by the user and return to the original screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearAll();
            this.SaveComplete(this, null);
        }

        protected void btnSaveVehicle_Click(object sender, EventArgs e)
        {
            this.Update();
        }
    }
}

