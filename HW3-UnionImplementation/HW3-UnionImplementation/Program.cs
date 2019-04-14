using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3_UnionImplementation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("HW3: UnionAll and Union for 2 entities with apartment");

            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string barcelonaFirstFilePath = Path.Combine(projectDirectory, "Barcelona1.csv");
            string barcelonaSecondFilePath = Path.Combine(projectDirectory, "Barcelona2.csv");

            List<Apartment> apartments = ParseCsv(barcelonaFirstFilePath, false);
            List<Apartment> apartments2 = ParseCsv(barcelonaSecondFilePath, true);

            //union all
            var watch = Stopwatch.StartNew();
            List<ApartmentResult> unionAllResult = apartments.Union(apartments2).Select(x => new ApartmentResult()
            {
                Name = x.Name,
                Latitude = x.Latitude,
                Longitude = x.Longitude
            }).ToList();
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(@"UnionAll was done in {0}, results count: {1}", elapsedMs, unionAllResult.Count);

            //use custom comparer for union
            watch = Stopwatch.StartNew();
            var customApartmentComparer = new ApartmentComparer();
            List<ApartmentResult> unionResult = apartments.Union(apartments2, customApartmentComparer).Select(x => new ApartmentResult()
            {
                Name = x.Name,
                Latitude = x.Latitude,
                Longitude = x.Longitude
            }).ToList();
            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(@"Union was done in {0}, results count: {1}", elapsedMs, unionResult.Count);

            Console.WriteLine("Completed");
        }

        public static List<Apartment> ParseCsv(string csvFilePath, bool parseId)
        {
            List<Apartment> apartments = new List<Apartment>();
            using (TextFieldParser parser = new TextFieldParser(csvFilePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                bool firstLine = true;
                while (!parser.EndOfData)
                {
                    //Processing row
                    string[] fields = parser.ReadFields();

                    if (firstLine)
                    {
                        firstLine = false;
                        continue;
                    }
                    apartments.Add(new Apartment(fields, parseId));
                }
            }
            return apartments;
        }
    }
}
