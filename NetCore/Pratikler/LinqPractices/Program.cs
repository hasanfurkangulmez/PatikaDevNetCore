using System;
using System.Linq;
using LinqPractices.DbOperations;

namespace LinqPractices
{
    class Program
    {
        static void Main(string[] args)
        {
            DataGenerator.Initialize();
            LinqDbContext _context = new LinqDbContext();
            var students = _context.Students.ToList<Student>();
            Console.WriteLine("*** Find ***");
            var student = _context.Students.Where(student => student.StudentId == 1).FirstOrDefault();
            student = _context.Students.Find(1);
            System.Console.WriteLine(student.Name);

            //FirstOrDefault();
            System.Console.WriteLine();
            System.Console.WriteLine("**** FirstOrDefault ****");
            student = _context.Students.Where(student => student.Surname == "Arda").FirstOrDefault();
            System.Console.WriteLine(student.Name);

            student = _context.Students.FirstOrDefault(x => x.Surname == "Arda");
            System.Console.WriteLine(student.Name);

            //SingleIrDefault();
            System.Console.WriteLine();
            System.Console.WriteLine("*** SingleOrDefault ***");
            student = _context.Students.SingleOrDefault(student => student.Name == "Deniz");
            //student=_context.Students.SingleOrDefault(student=>student.Name=="Arda"); Birden fazla aynı veri olduğu için hata verir.
            System.Console.WriteLine(student.Surname);

            //ToList()
            System.Console.WriteLine();
            System.Console.WriteLine("*** ToList ***");
            var studentList = _context.Students.Where(student => student.ClassId == 2).ToList();
            System.Console.WriteLine(studentList.Count);

            //OrderBy
            System.Console.WriteLine();
            System.Console.WriteLine("**** OrderBy ****");
            students = _context.Students.OrderBy(x => x.StudentId).ToList();

            foreach (var st in students)
            {
                System.Console.WriteLine(st.StudentId + " - " + st.Name + " - " + st.Surname);
            }

            //OrderByDescending
            System.Console.WriteLine();
            System.Console.WriteLine("**** OrderByDescending ****");
            students = _context.Students.OrderByDescending(x => x.StudentId).ToList();

            foreach (var st in students)
            {
                System.Console.WriteLine(st.StudentId + " - " + st.Name + " - " + st.Surname);
            }

            //Anonymus Object Result
            System.Console.WriteLine();
            System.Console.WriteLine("*** Anonymous Object Result ****");
            var anonymousObject = _context.Students.Where(x => x.ClassId == 2)
            .Select(x => new
            {
                Id = x.StudentId,
                FullName = x.Name + " " + x.Surname
            });
            foreach (var obj in anonymousObject)
            {
                System.Console.WriteLine(obj.Id + " - " + obj.FullName);
            }
        }
    }
}
