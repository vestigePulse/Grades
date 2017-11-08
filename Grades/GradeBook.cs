using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Grades
{
    public class GradeBook : GradeTracker
    {

        public GradeBook()
        {
            _name = "Empty";
            grades = new List<float>();
        }

        public override GradeStatistics ComputeStatistics()      //"virtual" to invoke polymorphism (GradeBook/ThrowAwayGradeBook); replace with "override" for abstract method
        {
            Console.WriteLine("GradeBook::ComputeStatistics");     //Simple debug to ensure going through of the GradeBook:ComputeStatistics method

            GradeStatistics stats = new GradeStatistics();

            float sum = 0;
            foreach (float grade in grades)
            {
                stats.HighestGrade = Math.Max(grade, stats.HighestGrade);
                stats.LowestGrade = Math.Min(grade, stats.LowestGrade);
                sum += grade;
            }
            stats.AverageGrade = sum / grades.Count;
            return stats;
        }

        public override void WriteGrades(TextWriter destination)        //Since GradeBook class is inherited from GradeTracker (abstract class, which is a virtual base class), "override" has to be added.
        {
            for (int i = 0; i < grades.Count; i++)
            {
                destination.WriteLine(grades[i]);
            }
        }

        public override void AddGrade(float grade)
        {
            grades.Add(grade);
        }

        public override IEnumerator GetEnumerator()
        {
            return grades.GetEnumerator();
        }

        protected List<float> grades;
    }
}
