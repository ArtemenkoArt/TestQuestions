using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _012_TestQuestions
{
    class Program
    {
        //
        public static List<Question> GetQuestion()
        {
            List<Question> questions = new List<Question>();

            string[] lines = File.ReadAllLines("Questions.txt");
            string category = "";
            foreach (string line in lines)
            {
                if (line.Trim() == "")
                {
                    continue;
                }
                if (line.IndexOf("<") >-1 && line.IndexOf(">") >-1)
                {
                    category = line.Replace("<", "").Replace(">", "");
                    continue;
                }
                else
                {
                    questions.Add(new Question { Category = category, QuestionText = line });
                }
                //Console.WriteLine(line);
            }
            return questions;
        }

        //
        public static void WriteToFile(IEnumerable<Question> questions)
        {
            using (StreamWriter outputFile = new StreamWriter(Path.Combine("SortedQuestions.txt")))
            {
                foreach (Question item in questions)
                {
                    outputFile.WriteLine($"{item.Student.Name} \t {item.Category} \t {item.QuestionText}");
                }
            }
        }

        //
        public static List<Student> GetStudents()
        {
            List<Student> students = new List<Student>
            {
                new Student { Name = "Artem" },
                new Student { Name = "Yaroslav" },
                new Student { Name = "Ivan" },
                new Student { Name = "Vitaliy" },
                new Student { Name = "Vlad" }
            };

            return students;
        }

        //
        public static List<Question> SetQuestionsToStudent(List<Question> questions, List<Student> students)
        {
            List<Question> questionsWithStudents = new List<Question>();
            while (questions.Count != 0)
            {
                foreach (var student in students)
                {
                    var currentQuestion = questions[rnd.GetRandom(0, questions.Count)];
                    currentQuestion.Student = student;
                    questionsWithStudents.Add(currentQuestion);
                    questions.Remove(currentQuestion);
                    if (questions.Count == 0)
                    {
                        break;
                    }
                }
            }
            return questionsWithStudents;
        }

        //
        static void Main(string[] args)
        {
            List<Question> questionsWithStudents = SetQuestionsToStudent(GetQuestion(), GetStudents());
            IEnumerable<Question> sortedQuestions = questionsWithStudents.OrderBy(i => i.Student);

            WriteToFile(sortedQuestions);
            Console.ReadKey();
        }
    }
}
