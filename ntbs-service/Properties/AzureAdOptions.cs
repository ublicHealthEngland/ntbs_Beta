﻿namespace ntbs_service.Properties
{
    public class AzureAdOptions
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        /** The authentication endpoint for the Azure AD tenant hosting the Azure AD Application. e.g. https://login.microsoftonline.com/mydomain.com */
        public string Authority { get; set; }

        /** The URL to send the id/access token back to. e.g. /Index */
        public string CallbackPath { get; set; }

        /** Group that contains all users of NTBS, used for authorizing access to the app */
        public string BaseUserGroup { get; set; }

        /** This will override the use of Adfs with Azure Active Directory **/
        public string Enabled { get; set; }
    }
}
