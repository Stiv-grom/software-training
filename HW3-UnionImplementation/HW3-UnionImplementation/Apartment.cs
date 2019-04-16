using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3_UnionImplementation
{
    public class Apartment
    {
        public int Id;

        public string Name;

        public string ZipCode;

        public string Location;

        public string Country;

        public float Latitude;

        public float Longitude;

        public Apartment(string[] tableRow, bool idNeedsParsing = false)
        {
            if (tableRow.Count() == 7)
            {
                this.Id = idNeedsParsing ? ParseIdFromUrl(tableRow[0]) : int.Parse(tableRow[0], CultureInfo.InvariantCulture);
                this.Name = tableRow[1];
                this.ZipCode = tableRow[2];
                this.Location = tableRow[3];
                this.Country = tableRow[4];
                this.Latitude = float.Parse(tableRow[5], CultureInfo.InvariantCulture);
                this.Longitude = float.Parse(tableRow[6], CultureInfo.InvariantCulture);
            }

        }

        private int ParseIdFromUrl(string url)
        {
            return int.Parse(url.Substring(url.LastIndexOf('/') + 1), CultureInfo.InvariantCulture);
        }
    }
}
