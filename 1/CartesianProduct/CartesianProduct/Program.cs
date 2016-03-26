using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartesianProduct
{
    public static class Program
    {

        static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<IEnumerable<T>> sequences) 
        { 
            // основа индукции: 
            IEnumerable<IEnumerable<T>> result = new[] { Enumerable.Empty<T>() }; 
            foreach(var sequence in sequences) 
            { 
                var s = sequence; // нельзя замыкаться на переменную цикла 
                // индукционный переход: используем SelectMany, чтобы построить новое произведение из старого 
                result =  
                    from seq in result
                    from item in s
                    select seq.Concat(new[] {item}); 
            } 
            return result; 
        } 


        static void Main(string[] args)
        {
            //var S1 = new[] { 'a', 'b' };
            //var S2 = new[] { 'x', 'y', 'z' };
            //var S3 = new[] { 'p', 'q', 'r', 's', 't' };
            var Products = File.ReadLines("../../Product.txt");
            var Lobs = File.ReadLines("../../Lob.txt");
            var Carriers = File.ReadLines("../../Carrier.txt");

            //var sequences = new[] { S1, S2, S3 };
            //var products = sequences.CartesianProduct(); // реализован как метод расширения


            var sequences = new[] { Products, Lobs, Carriers };
            var products = sequences.CartesianProduct(); // реализован как метод расширения
            

            var allCombinations = sequences.CartesianProduct(); // реализован как метод расширения
            //    products.ToArray()

            foreach (var product in products)
            {
                Console.WriteLine(String.Join(", ", product.ToArray()));
                

            }
            Console.ReadKey();
        }
    }
}
