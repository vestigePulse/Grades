using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grades
{
    class Program
    {
        static void Main(string[] args)
        {
            GradeBook book = new GradeBook();

            // book.NameChanged += new NameChangedDelegate(OnNameChanged);
            book.NameChanged += OnNameChanged;     //Same as the above, but less verbose

            book.Name = "Alan's Gradebook";
            book.Name = "Gradebook";
            
            book.AddGrade(91);
            book.AddGrade(89.5f);
            book.AddGrade(75);

            GradeStatistics stats = book.ComputeStatistics();
            Console.WriteLine(book.Name);
            WriteResult("Average", stats.AverageGrade);
            WriteResult("Highest", (int)stats.HighestGrade);
            WriteResult("Lowest", stats.LowestGrade);
            WriteResult(stats.Description, stats.LetterGrade);
        }

        static void OnNameChanged(object sender, NameChangedEventArgs args)
        {
            Console.WriteLine($"Grade book changing name from {args.ExistingName} to {args.NewName}");
        }

        static void WriteResult(string description, int result)     //String interpolation (After C# 6)
        {
            Console.WriteLine($"{description}: {result:C}");
        }

        static void WriteResult(string description, float result)   //Formatting string
        {
            Console.WriteLine("{0}: {1:F2}", description, result);
        }

        static void WriteResult(string description, string result)
        {
            Console.WriteLine($"{description}: {result}", description, result);
        }
    }
}
