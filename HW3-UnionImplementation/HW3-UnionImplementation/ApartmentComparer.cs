using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3_UnionImplementation
{
    public class ApartmentComparer : IEqualityComparer<Apartment>
    {
        public bool Equals(Apartment x, Apartment y)
        {
            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the apartments' properties are equal.
            return x.Id == y.Id && x.Name == y.Name && x.Country == y.Country && x.Latitude == y.Latitude &&
                x.Location == y.Location && x.Longitude == y.Longitude && x.ZipCode == y.ZipCode;
        }

        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.
        public int GetHashCode(Apartment apartment)
        {
            if (Object.ReferenceEquals(apartment, null)) return 0;

            int hash = 17;
            hash = hash * 23 + apartment.Id.GetHashCode();
            hash = hash * 23 + (apartment.Name ?? "").GetHashCode();
            hash = hash * 23 + (apartment.Country ?? "").GetHashCode();
            hash = hash * 23 + apartment.Latitude.GetHashCode();
            hash = hash * 23 + (apartment.Location ?? "").GetHashCode();
            hash = hash * 23 + apartment.Longitude.GetHashCode();
            hash = hash * 23 + (apartment.ZipCode ?? "").GetHashCode();
            return hash;

        }
    }
}
