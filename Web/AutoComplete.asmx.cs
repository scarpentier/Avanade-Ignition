// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutoComplete.asmx.cs" company="Avanade">
//   Avanade Ignition Team 4 © 2012
// </copyright>
// <summary>
//   Autocomplete webservice to populate the fields that have autocomplete
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Services;

    /// <summary>
    /// Autocomplete webservice to populate the fields that have autocomplete
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class AutoComplete : System.Web.Services.WebService
    {

        [WebMethod]
        public string[] GetCarriersList(string prefixText, int count)
        {
            var db = new QuotesDBEntities();
            var carriers =
               (from q in db.Quotes
                where q.PreviousCarrier.StartsWith(prefixText)
                select q.PreviousCarrier).Distinct().Take(count).OrderBy(name => name);

            return carriers.ToArray();
        }

        [WebMethod]
        public string[] GetVehicleMake(string prefixText, int count)
        {
            var db = new QuotesDBEntities();
            var make =
               (from q in db.Vehicles
                where q.Make.StartsWith(prefixText)
                select q.Make).Distinct().Take(count).OrderBy(name => name);

            return make.ToArray();
        }

        [WebMethod]
        public string[] GetVehicleModel(string prefixText, int count)
        {
            var db = new QuotesDBEntities();
            var model =
               (from q in db.Vehicles
                where q.Model.StartsWith(prefixText)
                select q.Model).Distinct().Take(count).OrderBy(name => name);

            return model.ToArray();
        }
    }
}
