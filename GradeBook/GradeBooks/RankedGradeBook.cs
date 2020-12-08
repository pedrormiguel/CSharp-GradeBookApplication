using System;
using System.Linq;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = Enums.GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            var AmountStundent = Students.Count;
            
            if (AmountStundent < 5)
                throw new InvalidOperationException();

            var AmountStudentPerpercentage = int.Parse( (AmountStundent * .20).ToString() );

            var orderStudentGrade = Students.OrderByDescending( x => x.AverageGrade ).ToList();

            int range = 0;

            for (var i = 0; i < AmountStundent; i += AmountStudentPerpercentage)
            {
                var itemsSelected = orderStudentGrade.GetRange(i, AmountStudentPerpercentage);

                if (itemsSelected.Exists(x => x.AverageGrade >= averageGrade))
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

                default:
                    return 'F';
            }

            return 'F';

        }
    }
}
