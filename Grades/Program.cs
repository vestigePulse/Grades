using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grades
{
    class Program
    {
        static void Main(string[] args)
        {
            GradeTracker book = CreateGradeBook();
            //GetBookName(book);              //Extract Method (part of refactoring)

            // book.NameChanged += new NameChangedDelegate(OnNameChanged);
            //book.NameChanged += OnNameChanged;     //Same as the above, but less verbose

            //book.Name = "Alan's Gradebook";
            //book.Name = "Gradebook";
            AddGrades(book);                //Extract Method (part of refactoring)
            SaveGrades(book);               //Extract Method (part of refactoring)

            //Console.WriteLine(book.Name);
            WriteResults(book);             //Extract Method (part of refactoring)
        }

        private static GradeTracker CreateGradeBook()
        {
            return new ThrowAwayGradeBook();
        }

        private static void WriteResults(GradeTracker book)
        {
            GradeStatistics stats = book.ComputeStatistics();
            WriteResult("Average", stats.AverageGrade);
            WriteResult("Highest", (int)stats.HighestGrade);
            WriteResult("Lowest", stats.LowestGrade);
            WriteResult(stats.Description, stats.LetterGrade);
        }

        private static void SaveGrades(GradeTracker book)
        {
            using (StreamWriter outputFile = File.CreateText("Grades.txt"))     //using (): For closing and disposing properly (cleaning up resources)
            {
                book.WriteGrades(outputFile);
            }
        }

        private static void AddGrades(GradeTracker book)
        {
            book.AddGrade(91);
            book.AddGrade(89.5f);
            book.AddGrade(75);
        }

        private static void GetBookName(GradeTracker book)
        {
            try
            {
                Console.WriteLine("Enter a name:");
                book.Name = Console.ReadLine();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception)       //The less specific the exceptions, the lower level they are within the code, to avoid catching everything yet don't know what's happening
            {
                Console.WriteLine("Something went wrong!");
            }
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
