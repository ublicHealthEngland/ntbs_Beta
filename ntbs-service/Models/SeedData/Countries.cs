﻿using System.Collections.Generic;
using System.Linq;
using ntbs_service.Models.Entities;

namespace ntbs_service.Models.SeedData
{
    public static class Countries
    {
        public static readonly string[] notHighTbOccurenceCountries = new[]
        {
            "Andorra",
            "Australia",
            "Austria",
            "Belgium",
            "Canada",
            "Cyprus",
            "Denmark",
            "Finland",
            "France",
            "Germany",
            "Greece",
            "Greenland",
            "Guernsey",
            "Holy See (Vatican City State)",
            "Iceland",
            "Ireland",
            "Isle Of Man",
            "Italy",
            "Jersey",
            "Liechtenstein",
            "Luxembourg",
            "Monaco",
            "Netherlands",
            "New Zealand",
            "Norway",
            "Portugal",
            "Spain",
            "Sweden",
            "Switzerland",
            "United Kingdom",
            "United States",
            "United States Minor Outlying Islands"
        };

        public static IEnumerable<Country> GetCountriesArray()
        {
            var allCountries = GetAllCountryData().ToList();

            // Set all countries to high occurence and conditionally flag them as low.
            // The vast majority are high occurence.
            allCountries.ForEach(c => c.HasHighTbOccurence = true);


            foreach (var notHighTbCountry in notHighTbOccurenceCountries)
            {
                // If country not found, or duplicate countries found with the name, this will throw an exception.
                // Couldn't come up with a neat strongly typed way of doing this.
                var lowCountry = allCountries.Single(c => c.Name == notHighTbCountry);
                lowCountry.HasHighTbOccurence = false;
            }

            return allCountries;
        }

        private static IEnumerable<Country> GetAllCountryData()
        {
            return new[] {
                new Country { CountryId = 1, Name = "Afghanistan", IsoCode = "AF" },
                new Country { CountryId = 2, Name = "Åland Islands", IsoCode = "AX" },
                new Country { CountryId = 3, Name = "Albania", IsoCode = "AL" },
                new Country { CountryId = 4, Name = "Algeria", IsoCode = "DZ" },
                new Country { CountryId = 5, Name = "American Samoa", IsoCode = "AS" },
                new Country { CountryId = 6, Name = "Andorra", IsoCode = "AD" },
                new Country { CountryId = 7, Name = "Angola", IsoCode = "AO" },
                new Country { CountryId = 8, Name = "Anguilla", IsoCode = "AI" },
                new Country { CountryId = 9, Name = "Antarctica", IsoCode = "AQ" },
                new Country { CountryId = 10, Name = "Antigua and Barbuda", IsoCode = "AG" },
                new Country { CountryId = 11, Name = "Argentina", IsoCode = "AR" },
                new Country { CountryId = 12, Name = "Armenia", IsoCode = "AM" },
                new Country { CountryId = 13, Name = "Aruba", IsoCode = "AW" },
                new Country { CountryId = 14, Name = "Australia", IsoCode = "AU" },
                new Country { CountryId = 15, Name = "Austria", IsoCode = "AT" },
                new Country { CountryId = 16, Name = "Azerbaijan", IsoCode = "AZ" },
                new Country { CountryId = 17, Name = "Bahamas", IsoCode = "BS" },
                new Country { CountryId = 18, Name = "Bahrain", IsoCode = "BH" },
                new Country { CountryId = 19, Name = "Bangladesh", IsoCode = "BD" },
                new Country { CountryId = 20, Name = "Barbados", IsoCode = "BB" },
                new Country { CountryId = 21, Name = "Belarus", IsoCode = "BY" },
                new Country { CountryId = 22, Name = "Belgium", IsoCode = "BE" },
                new Country { CountryId = 23, Name = "Belize", IsoCode = "BZ" },
                new Country { CountryId = 24, Name = "Benin", IsoCode = "BJ" },
                new Country { CountryId = 25, Name = "Bermuda", IsoCode = "BM" },
                new Country { CountryId = 26, Name = "Bhutan", IsoCode = "BT" },
                new Country { CountryId = 27, Name = "Bolivia", IsoCode = "BO" },
                new Country { CountryId = 28, Name = "Bosnia and Herzegovina", IsoCode = "BA" },
                new Country { CountryId = 29, Name = "Botswana", IsoCode = "BW" },
                new Country { CountryId = 30, Name = "Bouvet Island", IsoCode = "BV" },
                new Country { CountryId = 31, Name = "Brazil", IsoCode = "BR" },
                new Country { CountryId = 32, Name = "British Indian Ocean Territory", IsoCode = "IO" },
                new Country { CountryId = 33, Name = "Brunei Darussalam", IsoCode = "BN" },
                new Country { CountryId = 34, Name = "Bulgaria", IsoCode = "BG" },
                new Country { CountryId = 35, Name = "Burkina Faso", IsoCode = "BF" },
                new Country { CountryId = 36, Name = "Burundi", IsoCode = "BI" },
                new Country { CountryId = 37, Name = "Cambodia", IsoCode = "KH" },
                new Country { CountryId = 38, Name = "Cameroon", IsoCode = "CM" },
                new Country { CountryId = 39, Name = "Canada", IsoCode = "CA" },
                new Country { CountryId = 40, Name = "Cape Verde", IsoCode = "CV" },
                new Country { CountryId = 41, Name = "Cayman Islands", IsoCode = "KY" },
                new Country { CountryId = 42, Name = "Central African Republic", IsoCode = "CF" },
                new Country { CountryId = 43, Name = "Chad", IsoCode = "TD" },
                new Country { CountryId = 44, Name = "Chile", IsoCode = "CL" },
                new Country { CountryId = 45, Name = "China", IsoCode = "CN" },
                new Country { CountryId = 46, Name = "Christmas Island", IsoCode = "CX" },
                new Country { CountryId = 47, Name = "Cocos (Keeling) Islands", IsoCode = "CC" },
                new Country { CountryId = 48, Name = "Colombia", IsoCode = "CO" },
                new Country { CountryId = 49, Name = "Comoros", IsoCode = "KM" },
                new Country { CountryId = 50, Name = "Congo", IsoCode = "CG" },
                new Country { CountryId = 51, Name = "Congo, The Democratic Republic of the", IsoCode = "CD" },
                new Country { CountryId = 52, Name = "Cook Islands", IsoCode = "CK" },
                new Country { CountryId = 53, Name = "Costa Rica", IsoCode = "CR" },
                new Country { CountryId = 54, Name = "Côte D'ivoire", IsoCode = "CI" },
                new Country { CountryId = 55, Name = "Croatia", IsoCode = "HR" },
                new Country { CountryId = 56, Name = "Cuba", IsoCode = "CU" },
                new Country { CountryId = 57, Name = "Cyprus", IsoCode = "CY" },
                new Country { CountryId = 58, Name = "Czech Republic", IsoCode = "CZ" },
                new Country { CountryId = 59, Name = "Denmark", IsoCode = "DK" },
                new Country { CountryId = 60, Name = "Djibouti", IsoCode = "DJ" },
                new Country { CountryId = 61, Name = "Dominica", IsoCode = "DM" },
                new Country { CountryId = 62, Name = "Dominican Republic", IsoCode = "DO" },
                new Country { CountryId = 63, Name = "Ecuador", IsoCode = "EC" },
                new Country { CountryId = 64, Name = "Egypt", IsoCode = "EG" },
                new Country { CountryId = 65, Name = "El Salvador", IsoCode = "SV" },
                new Country { CountryId = 66, Name = "Equatorial Guinea", IsoCode = "GQ" },
                new Country { CountryId = 67, Name = "Eritrea", IsoCode = "ER" },
                new Country { CountryId = 68, Name = "Estonia", IsoCode = "EE" },
                new Country { CountryId = 69, Name = "Ethiopia", IsoCode = "ET" },
                new Country { CountryId = 70, Name = "Falkland Islands (Malvinas)", IsoCode = "FK" },
                new Country { CountryId = 71, Name = "Faroe Islands", IsoCode = "FO" },
                new Country { CountryId = 72, Name = "Fiji", IsoCode = "FJ" },
                new Country { CountryId = 73, Name = "Finland", IsoCode = "FI" },
                new Country { CountryId = 74, Name = "France", IsoCode = "FR" },
                new Country { CountryId = 75, Name = "French Guiana", IsoCode = "GF" },
                new Country { CountryId = 76, Name = "French Polynesia", IsoCode = "PF" },
                new Country { CountryId = 77, Name = "French Southern Territories", IsoCode = "TF" },
                new Country { CountryId = 78, Name = "Gabon", IsoCode = "GA" },
                new Country { CountryId = 79, Name = "Gambia", IsoCode = "GM" },
                new Country { CountryId = 80, Name = "Georgia", IsoCode = "GE" },
                new Country { CountryId = 81, Name = "Germany", IsoCode = "DE" },
                new Country { CountryId = 82, Name = "Ghana", IsoCode = "GH" },
                new Country { CountryId = 83, Name = "Gibraltar", IsoCode = "GI" },
                new Country { CountryId = 84, Name = "Greece", IsoCode = "GR" },
                new Country { CountryId = 85, Name = "Greenland", IsoCode = "GL" },
                new Country { CountryId = 86, Name = "Grenada", IsoCode = "GD" },
                new Country { CountryId = 87, Name = "Guadeloupe", IsoCode = "GP" },
                new Country { CountryId = 88, Name = "Guam", IsoCode = "GU" },
                new Country { CountryId = 89, Name = "Guatemala", IsoCode = "GT" },
                new Country { CountryId = 90, Name = "Guernsey", IsoCode = "GG" },
                new Country { CountryId = 91, Name = "Guinea", IsoCode = "GN" },
                new Country { CountryId = 92, Name = "Guinea-Bissau", IsoCode = "GW" },
                new Country { CountryId = 93, Name = "Guyana", IsoCode = "GY" },
                new Country { CountryId = 94, Name = "Haiti", IsoCode = "HT" },
                new Country { CountryId = 95, Name = "Heard Island and Mcdonald Islands", IsoCode = "HM" },
                new Country { CountryId = 96, Name = "Holy See (Vatican City State)", IsoCode = "VA" },
                new Country { CountryId = 97, Name = "Honduras", IsoCode = "HN" },
                new Country { CountryId = 98, Name = "Hong Kong", IsoCode = "HK" },
                new Country { CountryId = 99, Name = "Hungary", IsoCode = "HU" },
                new Country { CountryId = 100, Name = "Iceland", IsoCode = "IS" },
                new Country { CountryId = 101, Name = "India", IsoCode = "IN" },
                new Country { CountryId = 102, Name = "Indonesia", IsoCode = "ID" },
                new Country { CountryId = 103, Name = "Iran, Islamic Republic of", IsoCode = "IR" },
                new Country { CountryId = 104, Name = "Iraq", IsoCode = "IQ" },
                new Country { CountryId = 105, Name = "Ireland", IsoCode = "IE" },
                new Country { CountryId = 106, Name = "Isle Of Man", IsoCode = "IM" },
                new Country { CountryId = 107, Name = "Israel", IsoCode = "IL" },
                new Country { CountryId = 108, Name = "Italy", IsoCode = "IT" },
                new Country { CountryId = 109, Name = "Jamaica", IsoCode = "JM" },
                new Country { CountryId = 110, Name = "Japan", IsoCode = "JP" },
                new Country { CountryId = 111, Name = "Jersey", IsoCode = "JE" },
                new Country { CountryId = 112, Name = "Jordan", IsoCode = "JO" },
                new Country { CountryId = 113, Name = "Kazakhstan", IsoCode = "KZ" },
                new Country { CountryId = 114, Name = "Kenya", IsoCode = "KE" },
                new Country { CountryId = 115, Name = "Kiribati", IsoCode = "KI" },
                new Country { CountryId = 116, Name = "Korea, Democratic People's Republic of", IsoCode = "KP" },
                new Country { CountryId = 117, Name = "Korea, Republic of", IsoCode = "KR" },
                new Country { CountryId = 118, Name = "Kosovo", IsoCode = "XK" },
                new Country { CountryId = 119, Name = "Kuwait", IsoCode = "KW" },
                new Country { CountryId = 120, Name = "Kyrgyzstan", IsoCode = "KG" },
                new Country { CountryId = 121, Name = "Lao People's Democratic Republic", IsoCode = "LA" },
                new Country { CountryId = 122, Name = "Latvia", IsoCode = "LV" },
                new Country { CountryId = 123, Name = "Lebanon", IsoCode = "LB" },
                new Country { CountryId = 124, Name = "Lesotho", IsoCode = "LS" },
                new Country { CountryId = 125, Name = "Liberia", IsoCode = "LR" },
                new Country { CountryId = 126, Name = "Libyan Arab Jamahiriya", IsoCode = "LY" },
                new Country { CountryId = 127, Name = "Liechtenstein", IsoCode = "LI" },
                new Country { CountryId = 128, Name = "Lithuania", IsoCode = "LT" },
                new Country { CountryId = 129, Name = "Luxembourg", IsoCode = "LU" },
                new Country { CountryId = 130, Name = "Macao", IsoCode = "MO" },
                new Country { CountryId = 131, Name = "Macedonia, The Former Yugoslav Republic of", IsoCode = "MK" },
                new Country { CountryId = 132, Name = "Madagascar", IsoCode = "MG" },
                new Country { CountryId = 133, Name = "Malawi", IsoCode = "MW" },
                new Country { CountryId = 134, Name = "Malaysia", IsoCode = "MY" },
                new Country { CountryId = 135, Name = "Maldives", IsoCode = "MV" },
                new Country { CountryId = 136, Name = "Mali", IsoCode = "ML" },
                new Country { CountryId = 137, Name = "Malta", IsoCode = "MT" },
                new Country { CountryId = 138, Name = "Marshall Islands", IsoCode = "MH" },
                new Country { CountryId = 139, Name = "Martinique", IsoCode = "MQ" },
                new Country { CountryId = 140, Name = "Mauritania", IsoCode = "MR" },
                new Country { CountryId = 141, Name = "Mauritius", IsoCode = "MU" },
                new Country { CountryId = 142, Name = "Mayotte", IsoCode = "YT" },
                new Country { CountryId = 143, Name = "Mexico", IsoCode = "MX" },
                new Country { CountryId = 144, Name = "Micronesia, Federated States of", IsoCode = "FM" },
                new Country { CountryId = 145, Name = "Moldova", IsoCode = "MD" },
                new Country { CountryId = 146, Name = "Monaco", IsoCode = "MC" },
                new Country { CountryId = 147, Name = "Mongolia", IsoCode = "MN" },
                new Country { CountryId = 148, Name = "Montenegro", IsoCode = "ME" },
                new Country { CountryId = 149, Name = "Montserrat", IsoCode = "MS" },
                new Country { CountryId = 150, Name = "Morocco", IsoCode = "MA" },
                new Country { CountryId = 151, Name = "Mozambique", IsoCode = "MZ" },
                new Country { CountryId = 152, Name = "Myanmar", IsoCode = "MM" },
                new Country { CountryId = 153, Name = "Namibia", IsoCode = "NA" },
                new Country { CountryId = 154, Name = "Nauru", IsoCode = "NR" },
                new Country { CountryId = 155, Name = "Nepal", IsoCode = "NP" },
                new Country { CountryId = 156, Name = "Netherlands", IsoCode = "NL" },
                new Country { CountryId = 157, Name = "Netherlands Antilles", IsoCode = "AN" },
                new Country { CountryId = 158, Name = "New Caledonia", IsoCode = "NC" },
                new Country { CountryId = 159, Name = "New Zealand", IsoCode = "NZ" },
                new Country { CountryId = 160, Name = "Nicaragua", IsoCode = "NI" },
                new Country { CountryId = 161, Name = "Niger", IsoCode = "NE" },
                new Country { CountryId = 162, Name = "Nigeria", IsoCode = "NG" },
                new Country { CountryId = 163, Name = "Niue", IsoCode = "NU" },
                new Country { CountryId = 164, Name = "Norfolk Island", IsoCode = "NF" },
                new Country { CountryId = 165, Name = "Northern Mariana Islands", IsoCode = "MP" },
                new Country { CountryId = 166, Name = "Norway", IsoCode = "NO" },
                new Country { CountryId = 167, Name = "Oman", IsoCode = "OM" },
                new Country { CountryId = 168, Name = "Other", IsoCode = "  " },
                new Country { CountryId = 169, Name = "Pakistan", IsoCode = "PK" },
                new Country { CountryId = 170, Name = "Palau", IsoCode = "PW" },
                new Country { CountryId = 171, Name = "Palestinian Territory, Occupied", IsoCode = "PS" },
                new Country { CountryId = 172, Name = "Panama", IsoCode = "PA" },
                new Country { CountryId = 173, Name = "Papua New Guinea", IsoCode = "PG" },
                new Country { CountryId = 174, Name = "Paraguay", IsoCode = "PY" },
                new Country { CountryId = 175, Name = "Peru", IsoCode = "PE" },
                new Country { CountryId = 176, Name = "Philippines", IsoCode = "PH" },
                new Country { CountryId = 177, Name = "Pitcairn", IsoCode = "PN" },
                new Country { CountryId = 178, Name = "Poland", IsoCode = "PL" },
                new Country { CountryId = 179, Name = "Portugal", IsoCode = "PT" },
                new Country { CountryId = 180, Name = "Puerto Rico", IsoCode = "PR" },
                new Country { CountryId = 181, Name = "Qatar", IsoCode = "QA" },
                new Country { CountryId = 182, Name = "Reunion", IsoCode = "RE" },
                new Country { CountryId = 183, Name = "Romania", IsoCode = "RO" },
                new Country { CountryId = 184, Name = "Russian Federation", IsoCode = "RU" },
                new Country { CountryId = 185, Name = "Rwanda", IsoCode = "RW" },
                new Country { CountryId = 186, Name = "Saint Barthélemy", IsoCode = "BL" },
                new Country { CountryId = 187, Name = "Saint Helena", IsoCode = "SH" },
                new Country { CountryId = 188, Name = "Saint Kitts and Nevis", IsoCode = "KN" },
                new Country { CountryId = 189, Name = "Saint Lucia", IsoCode = "LC" },
                new Country { CountryId = 190, Name = "Saint Martin", IsoCode = "MF" },
                new Country { CountryId = 191, Name = "Saint Pierre and Miquelon", IsoCode = "PM" },
                new Country { CountryId = 192, Name = "Saint Vincent and The Grenadines", IsoCode = "VC" },
                new Country { CountryId = 193, Name = "Samoa", IsoCode = "WS" },
                new Country { CountryId = 194, Name = "San Marino", IsoCode = "SM" },
                new Country { CountryId = 195, Name = "Sao Tome and Principe", IsoCode = "ST" },
                new Country { CountryId = 196, Name = "Saudi Arabia", IsoCode = "SA" },
                new Country { CountryId = 197, Name = "Senegal", IsoCode = "SN" },
                new Country { CountryId = 198, Name = "Serbia", IsoCode = "RS" },
                new Country { CountryId = 199, Name = "Seychelles", IsoCode = "SC" },
                new Country { CountryId = 200, Name = "Sierra Leone", IsoCode = "SL" },
                new Country { CountryId = 201, Name = "Singapore", IsoCode = "SG" },
                new Country { CountryId = 202, Name = "Slovakia", IsoCode = "SK" },
                new Country { CountryId = 203, Name = "Slovenia", IsoCode = "SI" },
                new Country { CountryId = 204, Name = "Solomon Islands", IsoCode = "SB" },
                new Country { CountryId = 205, Name = "Somalia", IsoCode = "SO" },
                new Country { CountryId = 206, Name = "South Africa", IsoCode = "ZA" },
                new Country { CountryId = 207, Name = "South Georgia and the South Sandwich Islands", IsoCode = "GS" },
                new Country { CountryId = 208, Name = "South Sudan", IsoCode = "SSD" },
                new Country { CountryId = 209, Name = "Spain", IsoCode = "ES" },
                new Country { CountryId = 210, Name = "Sri Lanka", IsoCode = "LK" },
                new Country { CountryId = 211, Name = "Sudan", IsoCode = "SD" },
                new Country { CountryId = 212, Name = "Suriname", IsoCode = "SR" },
                new Country { CountryId = 213, Name = "Svalbard and Jan Mayen", IsoCode = "SJ" },
                new Country { CountryId = 214, Name = "Swaziland", IsoCode = "SZ" },
                new Country { CountryId = 215, Name = "Sweden", IsoCode = "SE" },
                new Country { CountryId = 216, Name = "Switzerland", IsoCode = "CH" },
                new Country { CountryId = 217, Name = "Syrian Arab Republic", IsoCode = "SY" },
                new Country { CountryId = 218, Name = "Taiwan, Province of China", IsoCode = "TW" },
                new Country { CountryId = 219, Name = "Tajikistan", IsoCode = "TJ" },
                new Country { CountryId = 220, Name = "Tanzania, United Republic of", IsoCode = "TZ" },
                new Country { CountryId = 221, Name = "Thailand", IsoCode = "TH" },
                new Country { CountryId = 222, Name = "Timor-Leste", IsoCode = "TL" },
                new Country { CountryId = 223, Name = "Togo", IsoCode = "TG" },
                new Country { CountryId = 224, Name = "Tokelau", IsoCode = "TK" },
                new Country { CountryId = 225, Name = "Tonga", IsoCode = "TO" },
                new Country { CountryId = 226, Name = "Trinidad and Tobago", IsoCode = "TT" },
                new Country { CountryId = 227, Name = "Tunisia", IsoCode = "TN" },
                new Country { CountryId = 228, Name = "Turkey", IsoCode = "TR" },
                new Country { CountryId = 229, Name = "Turkmenistan", IsoCode = "TM" },
                new Country { CountryId = 230, Name = "Turks and Caicos Islands", IsoCode = "TC" },
                new Country { CountryId = 231, Name = "Tuvalu", IsoCode = "TV" },
                new Country { CountryId = 232, Name = "Uganda", IsoCode = "UG" },
                new Country { CountryId = 233, Name = "Ukraine", IsoCode = "UA" },
                new Country { CountryId = 234, Name = "United Arab Emirates", IsoCode = "AE" },
                new Country { CountryId = 235, Name = "United Kingdom", IsoCode = Models.Countries.UkCode },
                new Country { CountryId = 236, Name = "United States", IsoCode = "US" },
                new Country { CountryId = 237, Name = "United States Minor Outlying Islands", IsoCode = "UM" },
                new Country { CountryId = 238, Name = "Unknown", IsoCode = Models.Countries.UnknownCode },
                new Country { CountryId = 239, Name = "Uruguay", IsoCode = "UY" },
                new Country { CountryId = 240, Name = "Uzbekistan", IsoCode = "UZ" },
                new Country { CountryId = 241, Name = "Vanuatu", IsoCode = "VU" },
                new Country { CountryId = 242, Name = "Venezuela", IsoCode = "VE" },
                new Country { CountryId = 243, Name = "Viet Nam", IsoCode = "VN" },
                new Country { CountryId = 244, Name = "Virgin Islands, British", IsoCode = "VG" },
                new Country { CountryId = 245, Name = "Virgin Islands, U.S.", IsoCode = "VI" },
                new Country { CountryId = 246, Name = "Wallis and Futuna", IsoCode = "WF" },
                new Country { CountryId = 247, Name = "Western Sahara", IsoCode = "EH" },
                new Country { CountryId = 248, Name = "Yemen", IsoCode = "YE" },
                new Country { CountryId = 249, Name = "Zambia", IsoCode = "ZM" },
                new Country { CountryId = 250, Name = "Zimbabwe", IsoCode = "ZW" }
            };
        }

    }
}
