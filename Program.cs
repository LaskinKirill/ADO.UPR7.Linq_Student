using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq_Student
{
    class Program
    {
        static List<Student> students = new List<Student>
                                                        {
 new Student {First="Svetlana", Last="Omelchenko", ID=111, Scores= new List<int> {97, 92, 81, 60}},
 new Student {First="Claire", Last="O’Donnell", ID=112, Scores= new List<int> {75, 84, 91, 39}},
 new Student {First="Sven", Last="Mortensen", ID=113, Scores= new List<int> {88, 94, 65, 91}},
 new Student {First="Cesar", Last="Garcia", ID=114, Scores= new List<int> {97, 89, 85, 82}},
 new Student {First="Debra", Last="Garcia", ID=115, Scores= new List<int> {35, 72, 91, 70}},
 new Student {First="Alex", Last="Bolotov", ID=116, Scores = new List<int>{99, 93, 98, 97}},
 new Student {First="Vlad", Last="Smirnov", ID=117, Scores = new List<int>{98, 99, 96, 95 }}
                                                        };

        static void Main(string[] args)
        {
            IEnumerable<Student> studentQuery = 
                from Student in students
                where Student.Scores[0] > 90 && Student.Scores[3] < 80
                //var studentQueryW = students.Where(st => st.Scores[0] > 90 && st.Scores[3] < 80);
                select Student;
            int studentCount = ( from Student in students
                where Student.Scores[0] > 90 && Student.Scores[3] > 80
                select Student).Count();
            int studentCountW = students.Where(st => st.Scores[0] > 90 && st.Scores[3] > 80).Count();

            Console.WriteLine("StudentQuery1:");

            Console.WriteLine("Количество студентов: {0}, {1}", studentCount, studentCountW);
            var studentList = (
                from Student in students
                where Student.Scores[0] > 90 && Student.Scores[3] > 80
                orderby Student.Last ascending
                orderby Student.Scores[0] descending

                select Student).ToList();

            foreach (Student Student in studentQuery)
            {
                Console.WriteLine("{0}, {1}", Student.Last, Student.First);

            }
            foreach (Student Student in studentList)
            {
                Console.WriteLine("{0}, {1}, {2}", Student.Last, Student.First, Student.Scores[0]);

            }

            Console.WriteLine("StudentQuery2:");

            var studentQuery2 =
            from student in students
            group student by student.Last[0];
            foreach (var studentGroup in studentQuery2)
            {
                Console.WriteLine(studentGroup.Key);
                foreach (Student student in studentGroup)
                {
                    Console.WriteLine(" {0}, {1}", student.Last, student.First);
                }
            }

            Console.WriteLine("StudentQuery3:");

            var studentQuery3 =
            from student in students
            group student by student.Last[0];
            foreach (var groupOfStudents in studentQuery3)
            {
                Console.WriteLine(groupOfStudents.Key);
                foreach (var student in groupOfStudents)
                {
                    Console.WriteLine(" {0}, {1}", student.Last, student.First);
                }
            }

            Console.WriteLine("StudentQuery4:");

            var studentQuery4 =
            from student in students
            group student by student.Last[0] into studentGroup
            orderby studentGroup.Key
            select studentGroup;
            foreach (var groupOfStudents in studentQuery4)
            {
                Console.WriteLine(groupOfStudents.Key);
                foreach (var student in groupOfStudents)
                {
                    Console.WriteLine(" {0}, {1}",
                    student.Last, student.First);
                }
            }

            Console.WriteLine("StudentQuery5:");

            var studentQuery5 =
            from student in students
            let totalScore = student.Scores[0] + student.Scores[1] +
            student.Scores[2] + student.Scores[3]
            where totalScore / 4 < student.Scores[0]
            select student.Last + " " + student.First;
            foreach (string s in studentQuery5)
            {
                Console.WriteLine(s);
            }

            Console.WriteLine("StudentQuery6:");

            var studentQuery6 =
            from student in students
            let totalScore = student.Scores[0] + student.Scores[1] +
            student.Scores[2] + student.Scores[3]
            select totalScore;
            double averageScore = studentQuery6.Average();
            Console.WriteLine("Class average score = {0}", averageScore);
            Console.WriteLine("StudentQuery7:");

            IEnumerable<string> studentQuery7 =
            from student in students
            where student.Last == "Garcia"
            select student.First;
            Console.WriteLine("The Garcias in the class are:");
            foreach (string s in studentQuery7)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("StudentQuery8:");

            var studentQuery8 =
            from student in students
            let x = student.Scores[0] + student.Scores[1] +
            student.Scores[2] + student.Scores[3]
            where x > averageScore
            select new { id = student.ID, score = x };
            foreach (var item in studentQuery8)
            {
                Console.WriteLine("Student ID: {0}, Score: {1}", item.id,
               item.score);
            }


            Console.ReadKey(true);
        }
    }
}
