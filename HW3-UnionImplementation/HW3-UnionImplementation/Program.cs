﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
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

            List<Apartment> apartments = UnionImplementation.ParseCsv(barcelonaFirstFilePath, false);
            List<Apartment> apartments2 = UnionImplementation.ParseCsv(barcelonaSecondFilePath, true);

            UnionImplementation.ListsUnionAll(apartments, apartments2);
            UnionImplementation.ListsUnion(apartments, apartments2);
            UnionImplementation.CustomUnionAll(apartments, apartments2);

            // benchmark goes out of memory during file read
            // perhaps a root of this issue: https://github.com/dotnet/BenchmarkDotNet/issues/828
            //var summary = BenchmarkRunner.Run<UnionImplementation>();

            Console.WriteLine("Completed");
            Console.ReadLine();
        }
    }

    public class UnionImplementation
    {
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

        public static void ListsUnion(List<Apartment> apartments, List<Apartment> apartments2)
        {
            var watch = Stopwatch.StartNew();
            var customApartmentComparer = new ApartmentComparer();
            List<ApartmentResult> unionResult = apartments.Union(apartments2, customApartmentComparer).Select(x => new ApartmentResult()
            {
                Name = x.Name,
                Latitude = x.Latitude,
                Longitude = x.Longitude
            }).ToList();
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(@"Union was done in {0} ms, results count: {1}", elapsedMs, unionResult.Count);
        }

        public static void ListsUnionAll(List<Apartment> apartments, List<Apartment> apartments2)
        {
            var watch = Stopwatch.StartNew();
            List<ApartmentResult> unionAllResult = apartments.Union(apartments2).Select(x => new ApartmentResult()
            {
                Name = x.Name,
                Latitude = x.Latitude,
                Longitude = x.Longitude
            }).ToList();
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(@"UnionAll was done in {0} ms, results count: {1}", elapsedMs, unionAllResult.Count);
        }

        public static void CustomUnionAll(IEnumerable<Apartment> apartments, IEnumerable<Apartment> apartments2)
        {
            var watch = Stopwatch.StartNew();
            var customApartmentComparer = new ApartmentComparer();
            var customUnionAllResult = new HashSet<Apartment>(apartments, customApartmentComparer);
            foreach (Apartment ap in apartments2)
            {
                customUnionAllResult.Add(ap);
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            var result = customUnionAllResult.Select(x => new ApartmentResult()
            {
                Name = x.Name,
                Latitude = x.Latitude,
                Longitude = x.Longitude
            }).ToList();

            Console.WriteLine(@"Custom UnionAll was done in {0} ms, results count: {1}", elapsedMs, customUnionAllResult.Count);
        }

        #region Benchmarks

        public List<Apartment> ApartmentsForBenchmark => ParseCsv(@"D:\projects\software-training\HW3-UnionImplementation\HW3-UnionImplementation\Barcelona1.csv", false);
        public List<Apartment> Apartments2ForBenchmark => ParseCsv(@"D:\projects\software-training\HW3-UnionImplementation\HW3-UnionImplementation\Barcelona2.csv", true);

        [ParamsSource(nameof(ApartmentsForBenchmark))]
        public static List<Apartment> Apart { get; set; }

        [ParamsSource(nameof(Apartments2ForBenchmark))]
        public static List<Apartment> Apart2 { get; set; }

        [Benchmark]

        public static void BenchmarkUnion()
        {

            ListsUnion(Apart, Apart2);
        }

        [Benchmark]
        public static void BenchmarkUnionAll()
        {
            ListsUnionAll(Apart, Apart2);
        }
        #endregion
    }
}
