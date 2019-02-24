using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_11._1._1
{
    class Program
    {
        static void Main(string[] args)
        {
            IPrintable printer = PrinterCreator.create(PrinterType.CONSOLE);

            Student student1 = new Student("Roma", 7.5);
            Student student2 = new Student("Leha", 8);
            Student student3 = new Student("Ilya", 6.9);

            Student[] students = new Student[] { student1, student2 };

            Brigade brigade = new Brigade("no name", students);

            printer.print(brigade);

            brigade.add(student3);
            printer.print(brigade);

            brigade.remove(student2);
            printer.print(brigade);

            double averageMark = Calculator.calcAverageMark(brigade);
            printer.print(averageMark);

            Student studentWithMaxMark = Searcher.findStudentWithMaxMark(brigade);
            printer.print(studentWithMaxMark);

            Student studentWithMinMark = Searcher.findStudentWithMinMark(brigade);
            printer.print(studentWithMinMark);

            brigade.replaceByIndex(student2, 0);
            printer.print(brigade);

            bool atBrigade = brigade.isAtBrigade(student1);
            printer.print(atBrigade);

            Console.ReadKey();
        }
    }

    public class Brigade
    {

        private string name;
        private Student[] students;

        public Brigade() { }

        public Brigade(string name, Student[] students)
        {
            this.name = name;
            this.students = students;
        }

        public string getName()
        {
            return name;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public Student[] getStudents()
        {
            return students;
        }

        public void setStudents(Student[] students)
        {
            this.students = students;
        }

        public void add(Student student)
        {
            Student[] newEquipment = new Student[students.Length + 1];

            Array.Copy(students, 0, newEquipment, 0, students.Length);
            newEquipment[students.Length] = student;

            students = newEquipment;
        }

        public void remove(Student student)
        {
            if (isAtBrigade(student))
            {
                Student[] newEquipment = new Student[students.Length - 1];

                for (int i = 0, j = 0; i < students.Length; i++, j++)
                {
                    if (!(student.Equals(students[i])))
                    {
                        newEquipment[j] = students[i];
                    }
                    else
                    {
                        j--;
                    }
                }
                students = newEquipment;
            }
        }

        public void replaceByIndex(Student student, int index)
        {
            if (student == null || (index > students.Length - 1 || index < 0))
            {
                return;
            }

            students[index] = student;
        }

        public bool isAtBrigade(Student student)
        {
            if (student == null)
            {
                return false;
            }

            foreach (Student student1 in students)
            {
                if (student.Equals(student1))
                {
                    return true;
                }
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            var brigade = obj as Brigade;
            return brigade != null &&
                   name == brigade.name &&
                   EqualityComparer<Student[]>.Default.Equals(students, brigade.students);
        }

        public override int GetHashCode()
        {
            var hashCode = -1239716481;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
            hashCode = hashCode * -1521134295 + EqualityComparer<Student[]>.Default.GetHashCode(students);
            return hashCode;
        }

        public override string ToString()
        {   string concat = string.Join<Student>(",", students);
            return concat;
        }
    }
    public class Student
    {

        private string name;
        private double mark;

        public Student() { }

        public Student(string name, double mark)
        {
            this.name = name;
            this.mark = mark;
        }

        public string getName()
        {
            return name;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public double getMark()
        {
            return mark;
        }

        public void setMark(double mark)
        {
            this.mark = mark;
        }

        public override bool Equals(object obj)
        {
            var student = obj as Student;
            return student != null &&
                   name == student.name &&
                   mark == student.mark;
        }

        public override string ToString()
        {
            return "Student{name=" + name + ",mark=" + mark + "}";
        }

        public override int GetHashCode()
        {
            var hashCode = -584733578;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
            hashCode = hashCode * -1521134295 + mark.GetHashCode();
            return hashCode;
        }
    }
    public class Calculator
    {
        public static double calcAverageMark(Brigade brigade)
        {
            if (brigade == null)
            {
                return 0.0;
            }

            Student[] students = brigade.getStudents();
            double sumMark = 0.0;

            if (students == null || students.Length == 0)
            {
                return sumMark;
            }


            foreach (Student student in students)
            {
                sumMark += student.getMark();
            }

            return sumMark / students.Length;
        }
    }
    public class Searcher
    {
        public static Student findStudentWithMaxMark(Brigade brigade)
        {
            if (brigade == null)
            {
                return null;
            }

            Student[] equipment = brigade.getStudents();

            if (equipment == null || equipment.Length == 0)
            {
                return null;
            }

            Student maxPowerEquipment = equipment[0];

            foreach (Student anEquipment in equipment)
            {
                if (maxPowerEquipment.getMark() < anEquipment.getMark())
                {
                    maxPowerEquipment = anEquipment;
                }
            }
            return maxPowerEquipment;
        }

        public static Student findStudentWithMinMark(Brigade brigade)
        {
            if (brigade == null)
            {
                return null;
            }

            Student[] equipment = brigade.getStudents();

            if (equipment == null || equipment.Length == 0)
            {
                return null;
            }

            Student minPowerEquipment = equipment[0];

            foreach (Student anEquipment in equipment)
            {
                if (minPowerEquipment.getMark() > anEquipment.getMark())
                {
                    minPowerEquipment = anEquipment;
                }
            }
            return minPowerEquipment;
        }
    }
    public class PrinterCreator
    {
        public static IPrintable create(PrinterType printerType)
        {
            IPrintable printer = null;

            switch (printerType)
            {
                case PrinterType.CONSOLE:
                    {
                        printer = new ConsolePrint();
                        break;
                    }
            }

            return printer;
        }
    }
    public interface IPrintable
    {
        void print(Object o);
    }
    public enum PrinterType
    {
        CONSOLE,
        FILE
    }
    public class ConsolePrint : IPrintable
    {
    
    public void print(Object o)
    {
        if (o == null)
        {
            return;
        }
        Console.WriteLine(o);
    }
}
}
