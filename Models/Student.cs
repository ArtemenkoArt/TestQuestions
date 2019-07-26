using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _012_TestQuestions
{
    public class Student : IComparable
    {
        public string Name { get; set; }

        public int CompareTo(object obj)
        {
            Student student = obj as Student;
            if (student != null)
            {
                return this.Name.CompareTo(student.Name);
            }
            else
            {
                throw new Exception("Err in Student.CompareTo()");
            }
        }
    }
}
