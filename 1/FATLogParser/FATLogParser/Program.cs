using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FATLogParser
{
    public class Parameter
    {
        public String Name { get; set; }
        public String Value { get; set; }
    }
    public class Difference
    {
        enum State
        {
            Name,
            Value,
            ArrayValue
        }
        public List<Parameter> ClassicValues { get; set; }
        public List<Parameter> PlusValues { get; set; }
        public String PricingID { get; set; }
        public String RequestName { get; set; }

        //private List<Parameter> GetParams(String source)
        //{
        //    try
        //    {
        //        source = TrimParameterString(source);
        //        return GetAllData(source);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Input " + source);
        //        throw ex;
        //    }
        //}

        private List<Parameter> GetParametersForRDS(String source)
        {
            try
            {
                if (!String.IsNullOrEmpty(source))
                    return GetAllData(source.TrimStart('[').TrimEnd(']'));
                else return new List<Parameter>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("RDS " + source);
                throw ex;
            }
        }
        

        private String TrimParameterString(String source)
        {
            var indexOfStartBreak = source.IndexOf('[');
            var indexOfLastBreak = source.LastIndexOf(']');
            return source.Substring(
                indexOfStartBreak + 1, indexOfLastBreak - indexOfStartBreak - 1);
        }

        private String TrimValuedString(String source)
        {
            var indexStart = source.IndexOf("MathTest");
            var indexOfBreak = source.LastIndexOf(']');

            return source.Substring(
                indexStart + 16, indexOfBreak - indexStart - 15);
        }

        private List<Parameter> GetAllData(string source)
        {
            State currentState = State.Name;

            List<Parameter> result = new List<Parameter>();
            String name = "";
            String value = "";
            int arrayGate = 0;
            foreach (var sym in source)
            {
                switch (sym)
                {
                    case ' ': continue;
                    case ',':
                        if (currentState == State.ArrayValue)
                        {
                            value += sym;
                        }
                        else
                        {
                            result.Add(new Parameter() { Name = name, Value = value });
                            name = value = "";
                            currentState = State.Name;
                        }
                        break;
                    case '=':
                        if (arrayGate == 0)
                            currentState = State.Value;

                        break;

                    case ':':
                        if (arrayGate == 0)
                            currentState = State.Value;
                        break;

                    case '[':
                        if (currentState == State.Value)
                        {
                            value += sym;
                            arrayGate++;
                            currentState = State.ArrayValue;
                        }
                        else
                            currentState = State.Name;
                        break;

                    case ']':
                        if (currentState == State.ArrayValue)
                        {
                            value += sym;
                            arrayGate--;
                            if (arrayGate == 0)
                                currentState = State.Value;
                        }
                        break;

                    default:
                        //    if (currentState != State.SkippedParam)
                        {
                            if (currentState == State.Name)
                                name += sym;
                            else
                                value += sym;


                        }
                        break;
                }
            }
            return result;
        }

        public Difference(String source1, String source2, String source3)
        {
            var valued1 = TrimValuedString(source1);
            var valued2 = TrimValuedString(source2);
            var valued3 = TrimValuedString(source3);

            var indexOfSpace = valued1.IndexOf(' ');
            var indexOfBreak = valued1.IndexOf('[');

            this.PricingID = valued1.Substring(0, indexOfSpace);
            this.RequestName = valued1.Substring(indexOfSpace + 3, indexOfBreak - indexOfSpace - 3).Replace("Request", "");

            var trimParams = true;
            while(trimParams)
            {
                var indexOfGate = valued2.IndexOf('[');
                var secIndexOfGate = valued2.IndexOf('[', indexOfGate + 1);
                if (secIndexOfGate - indexOfGate > 0)
                {
                    var value = valued2.Substring(indexOfGate, secIndexOfGate - indexOfGate);
                    if (!value.Contains('=') && !value.Contains(':'))
                    {
                        valued2 = TrimParameterString(valued2);
                        valued3 = TrimParameterString(valued3);
                    }
                    else trimParams = false;
                }
                else trimParams = false;
            }


            this.PlusValues = GetParametersForRDS(valued2);
            this.ClassicValues = GetParametersForRDS(valued3);
          //  if (PlusValues.Count != ClassicValues.Count)
                //     throw new Exception(
                //Console.WriteLine(String.Format("fucking shit!\n{0}", source2));
          //      Console.WriteLine("fucking shit!");
        }
    }


    class Program
    {


        static IEnumerable<String> GetAllLines(String path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("Source file isn't found: {0}", path);
            return File.ReadAllLines(path);
        }


        static bool CustomSpaceSymbolCheck(String value)
        {
            if (String.IsNullOrEmpty(value))
                return true;

            var allSpaceSymbols = new[] { " ", "\t", "\n" };
            return allSpaceSymbols.Any(value.StartsWith);
        }


        static IEnumerable<Difference> GetAllDifferences(String path)
        {
            var allSourceLines = GetAllLines(path);
            //  .Where(line => !CustomSpaceSymbolCheck(line));
            List<Difference> list = new List<Difference>();
            for (int i = 0; i < allSourceLines.Count(); i += 3)
            {
                list.Add(new Difference(allSourceLines.ElementAt(i), allSourceLines.ElementAt(i + 1), allSourceLines.ElementAt(i + 2)));
            }
            return list.OrderBy(entity => entity.RequestName);
            //  }
            //var allScenarios = allSourceLines

            //    .Select(line => new Difference(line));

            //   return allScenarios;
        }

        static DateTime GetDateFromScpecialFormat(string source, string format)
        {
            DateTime result;
            DateTime.TryParseExact(source, format, null, DateTimeStyles.None, out result);
            return result.Date;
        }

        static bool CompareDoubles(String val1, String val2)
        {
            double res1 = 0, res2 = 0;
            Double.TryParse(val1, out res1);
            Double.TryParse(val2, out res2);

            return (Math.Abs(res1 - res2) < 0.00001);
        }


        static void Run()
        {
            var pathInput = @"FATWithComparison.log";
           var sources = GetAllDifferences(pathInput);

            var pathOutput = @"DifferencesRerun.txt";

            using(StreamWriter sw = new StreamWriter(pathOutput, true))
            {
                foreach(var source in sources)
                {
                    bool pricingWritten = false;

                   // var classicProps = source.ClassicValues.Where(cl => cl.Value.Contains('.'));

                   // var plusProps = source.PlusValues.Where(cl => cl.Value.Contains('.'));

                  //  foreach (var classic in source.ClassicValues.Where(cl => cl.Value.Contains('.'))
                      //  .OrderBy(cl => cl.Name)
                  //      )
                  for (int i = 0; i < source.ClassicValues.Count() && i < source.PlusValues.Count(); i++)
                    {
                        var classic = source.ClassicValues.ElementAt(i);
                      
                        var plus = source.PlusValues.ElementAt(i);//.FirstOrDefault(pl => pl.Name == classic.Name);
                        if (classic.Value.Contains('.') || plus.Value.Contains('.'))
                        {
                            if (!CompareDoubles(plus.Value, classic.Value) /*|| plus.Value != classic.Value*/ )
                            {
                                if (!pricingWritten)
                                {
                                    sw.Write("{0}\t{1}\t", source.PricingID, source.RequestName);
                                    pricingWritten = true;
                                }
                                sw.Write("\t{0}\t{1}\t{2}\t", classic.Name, classic.Value, plus == null ? "  " : plus.Value);
                            }
                        }
                    }
                    if (pricingWritten)
                        sw.WriteLine();
                }
            }
           
        }

        static void Main(string[] args)
        {
            try
            {
                Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\nStackTrace: " + ex.StackTrace);
            }
            finally
            {
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
