#pragma warning disable SYSLIB0011
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
namespace FinalTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student[] students = GetFromBinaryFile();
            string pathToStudentsDir = DirectoryCreation();
            List<string> groups = new List<string>();
            foreach (var student in students)
            {
                if (!groups.Contains(student.Group))
                    groups.Add(student.Group);
            }
            string pathToFile;
            foreach (var group in groups)
            {
                pathToFile = pathToStudentsDir + '\\' + group + ".txt";
                using (File.Create(pathToFile))
                {

                }
                foreach (var student in students)
                {
                    if (student.Group == group)
                    {
                        using (StreamWriter sw = File.AppendText(pathToFile))
                        {
                            sw.WriteLine($"{student.Name}, {student.DateOfBirth:dd.MM.yyyy}");
                        }
                    }
                }
            }
        }
        public static string DirectoryCreation()
        {
            string path = Directory.GetCurrentDirectory() + "\\Students";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }
        public static Student[] GetFromBinaryFile()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Student[] students;
            using (var fs = new FileStream(Directory.GetCurrentDirectory() + "\\Students.dat", FileMode.Open))
            {
                students = (Student[])formatter.Deserialize(fs);
            }
            return students;
        }
    }
    [Serializable]
    public class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}