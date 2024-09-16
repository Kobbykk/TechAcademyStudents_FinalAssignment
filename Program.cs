using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Cryptography.X509Certificates;

namespace TechAcademyStudents_Final
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new StudentContext())
            {
                //Create and save a new Student
                Console.WriteLine("Adding new Students");

                var student = new Student
                {
                    FirstName = "Jesse",
                    LastName = "Adam",
                    EnrollmentDate = DateTime.Today
                };

                // Add Student
                context.Students.Add(student);

                var student1 = new Student
                {
                    FirstName = "Edwin",
                    LastName = "Addai",
                    EnrollmentDate = DateTime.Today
                };

                //Add Student1
                context.Students.Add(student1);

                //Save created Student
                context.SaveChanges();

                //Display all Students from the Database
                var students = (from s in context.Students
                                orderby s.FirstName select s).ToList<Student>();

                Console.WriteLine("Retrieve all Students from the database:");

                foreach (var myStudent in students)
                {
                    string fullName = myStudent.FirstName + " " + myStudent.LastName;
                    Console.WriteLine($"ID: {myStudent.Id}  Name: {fullName}");
                }

                Console.ReadLine();
            }
        }


        //Creating Student class
        public class Student
        {
            [Key]
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime EnrollmentDate { get; set; }

            public virtual ICollection<Enrollment> Enrollments { get; set; }
        }

        // Creating Course Class
        public class Course
        {
            [Key]
            public int CourseId { get; set; }
            public string Title { get; set; }
            public int Credits { get; set; }

            public virtual ICollection<Enrollment> Enrollments { get; set; }

        }

        // Creating Enum for Grade 
        public enum Grade
        {
            A, B, C, D, E, F
        }

        // Creating Enrollment Class
        public class Enrollment
        {
            [Key]
            public int EnrollmentId { get; set; }
            public int CourseId { get; set; }
            public int StudentId { get; set; }
            public Grade? Grade { get; set; }

            [ForeignKey("CourseId")]
            public virtual Course Course { get; set; }
            [ForeignKey("StudentId")]
            public virtual Student Student { get; set; }
        }

        //Creating StudentContext to seed the data to a default database (LocalDb or SQLEXPRESS)
        public class StudentContext : DbContext {

            public virtual DbSet<Student> Students { get; set; }
            public virtual DbSet<Course> Courses { get; set; }
            public virtual DbSet<Enrollment> Enrollments { get; set; }

        }

    }
}
