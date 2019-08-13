using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _012_TestQuestions
{
    class Program
    {
        //
        //public static List<Question> GetQuestion()
        public static Dictionary<string, List<Question>> GetQuestion()
        {
            Dictionary<string, List<Question>> dictQuestions = new Dictionary<string, List<Question>>();
            //List<Question> questions = new List<Question>();

            string[] lines = File.ReadAllLines("Questions2.txt");
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
                    dictQuestions.Add(category, new List<Question>());
                    continue;
                }
                else
                {
                    dictQuestions[category].Add(new Question { Category = category, QuestionText = line });
                    //questions.Add(new Question { Category = category, QuestionText = line });
                }
                //Console.WriteLine(line);
            }
            //return questions;
            return dictQuestions;
        }

        //
        public static void WriteToFile(IEnumerable<Question> questions)
        {
            using (StreamWriter outputFile = new StreamWriter(Path.Combine("SortedQuestions2.txt")))
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

        ////
        //public static List<Question> SetQuestionsToStudent(List<Question> questions, List<Student> students)
        //{
        //    List<Question> questionsWithStudents = new List<Question>();
        //    while (questions.Count != 0)
        //    {
        //        foreach (var student in students)
        //        {
        //            var currentQuestion = questions[rnd.GetRandom(0, questions.Count)];
        //            currentQuestion.Student = student;
        //            questionsWithStudents.Add(currentQuestion);
        //            questions.Remove(currentQuestion);
        //            if (questions.Count == 0)
        //            {
        //                break;
        //            }
        //        }
        //    }
        //    return questionsWithStudents;
        //}

        // random choise in each category
        public static List<Question> SetDictQuestionsToStudent(Dictionary<string, List<Question>> dictQuestions, List<Student> students)
        {
            List<Question> questionsWithStudents = new List<Question>();

            while (dictQuestions.Count != 0)
            {
                foreach (var student in students)
                {
                    if (dictQuestions.Keys.Count != 0)
                    {
                        var nameKey = dictQuestions.Keys.First();
                        var currentQuestion = dictQuestions[nameKey][rnd.GetRandom(0, dictQuestions[nameKey].Count)];
                        currentQuestion.Student = student;
                        var count = questionsWithStudents.Where(s => s.Student == student).Count()+1;
                        currentQuestion.QuestionText = count+" "+currentQuestion.QuestionText;
                        questionsWithStudents.Add(currentQuestion);
                        dictQuestions[nameKey].Remove(currentQuestion);
                        if (dictQuestions[nameKey].Count == 0)
                        {
                            dictQuestions.Remove(nameKey);
                        }
                    }  
                }
            }
            return questionsWithStudents;
        }

        //
        static void Main(string[] args)
        {
            List<Question> questionsWithStudents = SetDictQuestionsToStudent(GetQuestion(), GetStudents());
            IEnumerable<Question> sortedQuestions = questionsWithStudents.OrderBy(i => i.Student);

            WriteToFile(sortedQuestions);
            Console.WriteLine("Ok!");
            Console.ReadKey();
        }
    }
}
