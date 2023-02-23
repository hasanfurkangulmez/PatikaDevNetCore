using System.Linq;

namespace LinqPractices.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize()
        {
            using (var context = new LinqDbContext())
            {
                if (context.Students.Any()) { return; }

                context.Students.AddRange(
                    new Student()
                    {
                        //StudentId = 1,
                        Name = "Ayşe",
                        Surname = "Yılmaz",
                        ClassId = 1
                    },
                    new Student()
                    {
                        //StudentId = 2,
                        Name = "Deniz",
                        Surname = "Arda",
                        ClassId = 1
                    },
                    new Student()
                    {
                        //StudentId = 3,
                        Name = "Umut",
                        Surname = "Arda",
                        ClassId = 2
                    },
                    new Student()
                    {
                        //StudentId = 4,
                        Name = "Merve",
                        Surname = "Çalışkan",
                        ClassId = 2
                    }
                );
                context.SaveChanges();
            }
        }
    }
}