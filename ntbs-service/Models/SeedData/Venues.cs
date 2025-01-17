using System.Collections.Generic;
using ntbs_service.Models.Entities;

namespace ntbs_service.Models.SeedData
{
    public static class Venues
    {
        public static IEnumerable<VenueType> GetTypes()
        {
            return new[]
            {
                new VenueType { VenueTypeId = 1, Name = "Armed forces", Category = "Workplace" },
                new VenueType { VenueTypeId = 2, Name = "Catering", Category = "Workplace" },
                new VenueType { VenueTypeId = 3, Name = "Construction", Category = "Workplace" },
                new VenueType { VenueTypeId = 4, Name = "Driving", Category = "Workplace" },
                new VenueType { VenueTypeId = 5, Name = "Education", Category = "Workplace" },
                new VenueType { VenueTypeId = 6, Name = "Emergency services", Category = "Workplace" },
                new VenueType { VenueTypeId = 7, Name = "Factory", Category = "Workplace" },
                new VenueType { VenueTypeId = 8, Name = "Farming", Category = "Workplace" },
                new VenueType { VenueTypeId = 9, Name = "Hospital or medical centre", Category = "Workplace" },
                new VenueType { VenueTypeId = 10, Name = "Office", Category = "Workplace" },
                new VenueType { VenueTypeId = 11, Name = "Pub, bar or club", Category = "Workplace" },
                new VenueType { VenueTypeId = 12, Name = "Restaurant or cafe", Category = "Workplace" },
                new VenueType { VenueTypeId = 13, Name = "Hospitality", Category = "Workplace" },
                new VenueType { VenueTypeId = 14, Name = "Retail", Category = "Workplace" },
                new VenueType { VenueTypeId = 15, Name = "Warehouse", Category = "Workplace" },
                new VenueType { VenueTypeId = 16, Name = "Hair/beauty salon", Category = "Workplace" },
                new VenueType { VenueTypeId = 17, Name = "Health club or spa", Category = "Workplace" },
                new VenueType { VenueTypeId = 18, Name = "Recreational centre", Category = "Workplace" },
                new VenueType { VenueTypeId = 19, Name = "Other workplace", Category = "Workplace" },
                new VenueType { VenueTypeId = 20, Name = "Church", Category = "Place of worship" },
                new VenueType { VenueTypeId = 21, Name = "Temple", Category = "Place of worship" },
                new VenueType { VenueTypeId = 22, Name = "Mosque", Category = "Place of worship" },
                new VenueType { VenueTypeId = 23, Name = "Community centre", Category = "Place of worship" },
                new VenueType { VenueTypeId = 24, Name = "Multi-faith centre", Category = "Place of worship" },
                new VenueType { VenueTypeId = 25, Name = "Synagogue", Category = "Place of worship" },
                new VenueType { VenueTypeId = 26, Name = "Other place of worship", Category = "Place of worship" },
                new VenueType { VenueTypeId = 27, Name = "Arcade/gambling venue", Category = "Social" },
                new VenueType { VenueTypeId = 28, Name = "Pub, bar or club", Category = "Social" },
                new VenueType { VenueTypeId = 29, Name = "Restaurant or cafe", Category = "Social" },
                new VenueType { VenueTypeId = 30, Name = "Library", Category = "Social" },
                new VenueType { VenueTypeId = 31, Name = "Cinema", Category = "Social" },
                new VenueType { VenueTypeId = 32, Name = "Shopping centre", Category = "Social" },
                new VenueType { VenueTypeId = 33, Name = "Hair/beauty salon", Category = "Social" },
                new VenueType { VenueTypeId = 34, Name = "Health club or spa", Category = "Social" },
                new VenueType { VenueTypeId = 35, Name = "Exercise class", Category = "Social" },
                new VenueType { VenueTypeId = 36, Name = "Recreational centre", Category = "Social" },
                new VenueType { VenueTypeId = 37, Name = "Music classes", Category = "Social" },
                new VenueType { VenueTypeId = 38, Name = "Community centre", Category = "Social" },
                new VenueType { VenueTypeId = 39, Name = "Job/unemployment centre", Category = "Social" },
                new VenueType { VenueTypeId = 40, Name = "Crack house/smoking den", Category = "Social" },
                new VenueType { VenueTypeId = 41, Name = "Friends house", Category = "Social" },
                new VenueType { VenueTypeId = 42, Name = "Other social venue", Category = "Social" },
                new VenueType { VenueTypeId = 43, Name = "Pre-school or play group", Category = "Childcare & education" },
                new VenueType { VenueTypeId = 44, Name = "Nursery", Category = "Childcare & education" },
                new VenueType { VenueTypeId = 45, Name = "Primary school", Category = "Childcare & education" },
                new VenueType { VenueTypeId = 46, Name = "Secondary school", Category = "Childcare & education" },
                new VenueType { VenueTypeId = 47, Name = "College or sixth form", Category = "Childcare & education" },
                new VenueType { VenueTypeId = 48, Name = "After school clubs", Category = "Childcare & education" },
                new VenueType { VenueTypeId = 49, Name = "University", Category = "Childcare & education" },
                new VenueType { VenueTypeId = 50, Name = "Adult education", Category = "Childcare & education" },
                new VenueType { VenueTypeId = 51, Name = "Private tutoring", Category = "Childcare & education" },
                new VenueType { VenueTypeId = 52, Name = "Religious learning centre", Category = "Childcare & education" },
                new VenueType { VenueTypeId = 53, Name = "Other childcare & education", Category = "Childcare & education" },
                new VenueType { VenueTypeId = 54, Name = "Immigration detention centre", Category = "Place of detention" },
                new VenueType { VenueTypeId = 55, Name = "Prison", Category = "Place of detention" },
                new VenueType { VenueTypeId = 56, Name = "Youth detention centre", Category = "Place of detention" },
                new VenueType { VenueTypeId = 57, Name = "Other place of detention", Category = "Place of detention" },
                new VenueType { VenueTypeId = 58, Name = "Alcohol rehabilitation centre", Category = "Treatment and rehab" },
                new VenueType { VenueTypeId = 59, Name = "Drug rehabilitation centre", Category = "Treatment and rehab" },
                new VenueType { VenueTypeId = 60, Name = "Medical or physical rehabilitation centre", Category = "Treatment and rehab" },
                new VenueType { VenueTypeId = 61, Name = "Mental health rehabilitation centre", Category = "Treatment and rehab" },
                new VenueType { VenueTypeId = 62, Name = "Other treatment or rehab centre", Category = "Treatment and rehab" },
                new VenueType { VenueTypeId = 63, Name = "Crisis centre or refuge", Category = "Health and care" },
                new VenueType { VenueTypeId = 64, Name = "Initial accommodation centre ", Category = "Residential" },
                new VenueType { VenueTypeId = 65, Name = "Dispersal accommodation ", Category = "Residential" },
                new VenueType { VenueTypeId = 66, Name = "Homeless shelter", Category = "Residential" },
                new VenueType { VenueTypeId = 67, Name = "Squat", Category = "Residential" },
                new VenueType { VenueTypeId = 68, Name = "Care home", Category = "Residential" },
                new VenueType { VenueTypeId = 69, Name = "Halfway house", Category = "Residential" },
                new VenueType { VenueTypeId = 70, Name = "Hostel", Category = "Residential" },
                new VenueType { VenueTypeId = 71, Name = "Hall of residence", Category = "Residential" },
                new VenueType { VenueTypeId = 72, Name = "Sofa surfing", Category = "Residential" },
                new VenueType { VenueTypeId = 73, Name = "Other residential", Category = "Residential" },
                new VenueType { VenueTypeId = 74, Name = "Hospital", Category = "Health and care" },
                new VenueType { VenueTypeId = 75, Name = "Walk-in Centre/Minor Injuries", Category = "Health and care" },
                new VenueType { VenueTypeId = 76, Name = "Pharmacy", Category = "Health and care" },
                new VenueType { VenueTypeId = 77, Name = "GP Practice", Category = "Health and care" },
                new VenueType { VenueTypeId = 78, Name = "Nursing Home", Category = "Health and care" },
                new VenueType { VenueTypeId = 79, Name = "Hospice", Category = "Health and care" },
                new VenueType { VenueTypeId = 80, Name = "Health Centre/Clinic", Category = "Health and care" },
                new VenueType { VenueTypeId = 81, Name = "Other heathcare", Category = "Health and care" },
                new VenueType { VenueTypeId = 82, Name = "Car", Category = "Transport" },
                new VenueType { VenueTypeId = 83, Name = "Bus", Category = "Transport" },
                new VenueType { VenueTypeId = 84, Name = "Train", Category = "Transport" },
                new VenueType { VenueTypeId = 85, Name = "Tram", Category = "Transport" },
                new VenueType { VenueTypeId = 86, Name = "Metro", Category = "Transport" },
                new VenueType { VenueTypeId = 87, Name = "Plane", Category = "Transport" },
                new VenueType { VenueTypeId = 88, Name = "Taxi", Category = "Transport" },
                new VenueType { VenueTypeId = 89, Name = "Boat", Category = "Transport" },
                new VenueType { VenueTypeId = 90, Name = "Cruise ship", Category = "Transport" },
                new VenueType { VenueTypeId = 91, Name = "Other transport", Category = "Transport" },
            };
        }
    }
}
