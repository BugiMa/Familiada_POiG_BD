using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Familiada.Model.DAL
{
    static class DataAccess
    {
        static MySqlConnectionStringBuilder connStrBuilder;
        static MySqlConnection connection;

        private static string ALL_QUESTIONS_QUERY = "SELECT * FROM pytania";
        private static string ALL_ANSWERS_QUERY = "SELECT * FROM odpowiedzi";
        private static string ALL_JOKES_QUERY = "SELECT * FROM suchary";

        static DataAccess()
        {
            connStrBuilder = new MySqlConnectionStringBuilder();
            
            connStrBuilder.UserID = "root";    //przenieść stringi do zasobów aplikacji
            connStrBuilder.Password = "password";
            connStrBuilder.Server = "localhost";
            connStrBuilder.Database = "familiadaprojekt";
            connStrBuilder.Port = 3306;          
           
        }

        public static List<Question> GetAllQuestions()
        {
            List<Question> Questions = new List<Question>();
            List<string[]> answers = new List<string[]>();
            List<int> points = new List<int>();
            List<string> questions = new List<string>();
            int questionID=0;
            int answerID = 0;
            Question newq;
            
            using(connection = new MySqlConnection(connStrBuilder.ToString()))
            {
                
                MySqlCommand command2 = new MySqlCommand(ALL_ANSWERS_QUERY, connection);

                connection.Open();
                var dataReader = command2.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        answerID = (int)dataReader["id_odpowiedzi"]-1;
                        answers.Add(new string[] { dataReader["id_pytania"].ToString(), dataReader["odpowiedz"].ToString(), dataReader["punkty"].ToString() });
                        //points[answerID] = (int)dataReader["punkty"];
                    }
                }
                else
                {
                    Console.WriteLine("Brak wyników zapytania");
                }
                connection.Close();

                MySqlCommand command = new MySqlCommand(ALL_QUESTIONS_QUERY, connection);

                connection.Open();
                dataReader = command.ExecuteReader();

                if (dataReader.HasRows)
                {
                    questionID = 0;
                    while (dataReader.Read())
                    {
                        string[] currentAnswers = new string[6];
                        string[] currentPoints = new string[6];

                        for (int i = 6*questionID; i < (6*questionID)+6; i++)
                        {
                            currentAnswers[i-6*questionID] = answers[i][1];
                            currentPoints[i-6*questionID] = answers[i][2];
                        }
                        
                        questions.Add(dataReader["pytanie"].ToString());
                        Questions.Add(new Question(questions[questionID], currentAnswers, currentPoints));
                        questionID++;
                    }
                }
                else
                {
                    Console.WriteLine("Brak wyników zapytania");
                }
                connection.Close();
                
            }

            return Questions;
        }

        public static List<string> GetAllJokes()
        {
            List<string> jokes = new List<string>();

            using (connection = new MySqlConnection(connStrBuilder.ToString()))
            {
                MySqlCommand command = new MySqlCommand(ALL_JOKES_QUERY, connection);
                connection.Open();
                var dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        jokes.Add(dataReader["suchar"].ToString());
                    }
                }
                else
                {
                    Console.WriteLine("Brak wyników zapytania");
                }
                connection.Close();
            }

            return jokes;
        }

        public static string[] GetYays()
        {
            string[] yayReactions = System.IO.File.ReadAllLines("../../GameResources/StrasburgerYay.txt");


            return yayReactions;
        }
        public static string[] GetBoos()
        {
            string[] booReactions = System.IO.File.ReadAllLines("../../GameResources/StrasburgerBoo.txt");

            return booReactions;
        }
    }
}
