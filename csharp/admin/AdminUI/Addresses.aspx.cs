﻿using System;

namespace Health.Direct.AdminUI
{
    public partial class Addresses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AddressListControl1.Command += AddressListControl1_Command;
            AddressDetailsControl1.AddressSaved += AddressDetailsControl1_AddressSaved;
            AddressDetailsControl1.AddressCancelled += AddressDetailsControl1_AddressCancelled;

            // Provide context for user control
            NewAddressControl1.Owner = Request.QueryString["Owner"];
            long domainId = -1;
            long.TryParse(Request.QueryString["DomainId"], out domainId);
            NewAddressControl1.DomainId = domainId;

        }

        void AddressDetailsControl1_AddressSaved(object sender, EventArgs e)
        {
            AddressesMultiView.SetActiveView(MasterView);
        }
        
        void AddressDetailsControl1_AddressCancelled(object sender, EventArgs e)
        {
            AddressesMultiView.SetActiveView(MasterView);
        }

        void AddressListControl1_Command(object sender, global::Health.Direct.AdminUI.Logic.Views.AddressListControl.AddressListControlEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Details":
                    AddressDetailsControl1.EmailAddress = e.EmailAddress;
                    // AddressDetailsControl1.AddressId = e.AddressId;
                    AddressDetailsControl1.Owner = AddressListControl1.Owner;
                    AddressesMultiView.SetActiveView(DetailsView);
                    break;
            }
        }
    }
}