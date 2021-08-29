using System;
using System.Data;
using System.Text.RegularExpressions;

namespace FIAP.GabrielBatista.Redis
{
    public class Program
    {
        public static void Main()
        {
            var dt = new DataTable();
            var text = "Quantos que é ((1+1)*4)/3";
            var calc = Regex.Replace(text, @"[^\d+\-*\/\(\)*\b]", "").Trim();
            var result = dt.Compute(calc, "");

            Console.WriteLine(result.ToString());
            Console.ReadKey();
        }
    }
}