using System;
using System.Linq;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public bool _studentListIsLessMinimum
        {
            get
            {
                return Students.Count < 5 ? true : false;
            }
        }

        public RankedGradeBook(string name) : base(name)
        {
            Type = Enums.GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            var AmountStundent = Students.Count;
            
            if (this._studentListIsLessMinimum)
                throw new InvalidOperationException();

            var AmountStudentPerpercentage = Convert.ToInt32(AmountStundent * .20);

            var orderStudentGrade = Students.OrderByDescending( x => x.AverageGrade ).ToList();

            int range = 0;

            for (var i = 0; i < AmountStundent; i += AmountStudentPerpercentage)
            {
                var itemsSelected = orderStudentGrade.GetRange(i, AmountStudentPerpercentage);

                if (itemsSelected.Exists(x => x.AverageGrade <= averageGrade))
                {
                    range++;
                    break;
                }

                 range++;
            }

            switch (range)
            {
                case 1:
                    return 'A';
                   
                case 2:
                    return 'B';
                  

                case 3:
                    return 'C';

                case 4:
                    return 'D';
            }

            return 'F';

        }

        public override void CalculateStatistics()
        {
            var message = "Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.";

            if (this._studentListIsLessMinimum)
            {
                  Console.WriteLine(message);
                  return;
            }

            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            var message = "Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.";


            if (this._studentListIsLessMinimum)
            {
                Console.WriteLine(message);
                return;
            }

            base.CalculateStudentStatistics(name);
        }
    }
}
