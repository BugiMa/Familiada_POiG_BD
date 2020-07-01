using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Familiada.Model.DAL
{
    class DataAccess
    {
        MySqlConnectionStringBuilder connStrBuilder;
        MySqlConnection connection;

        private static string ALL_QUESTIONS_QUERY = "SELECT * FROM questions";
        private static string ALL_JOKES_QUERY = "SELECT * FROM jokes";

        public DataAccess()
        {
            connStrBuilder = new MySqlConnectionStringBuilder();
            /*
            connStrBuilder.UserID = ;    //przenieść stringi do zasobów aplikacji
            connStrBuilder.Password = ;
            connStrBuilder.Server = ;
            connStrBuilder.Database = ;
            connStrBuilder.Port = ;
            */
        }

        public List<Question> GetAllQuestions()
        {
            List<Question> questions = new List<Question>();
            string[] answers;
            int[] points;
            Question newq;
            
            using(connection = new MySqlConnection(connStrBuilder.ToString()))
            {
                MySqlCommand command = new MySqlCommand(ALL_QUESTIONS_QUERY, connection);
                connection.Open();
                var dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        answers = new string[] { dataReader["answer1"].ToString(), dataReader["answer2"].ToString(), dataReader["answer3"].ToString(), dataReader["answer4"].ToString(), dataReader["answer5"].ToString(), dataReader["answer6"].ToString() };
                        points = new int[] { (int)dataReader["points1"], (int)dataReader["points2"], (int)dataReader["points3"], (int)dataReader["points4"], (int)dataReader["points5"], (int)dataReader["points6"] };
                        newq = new Question(dataReader["question"].ToString(), answers, points);
                        questions.Add(newq);
                    }
                }
                else
                {
                    Console.WriteLine("Brak wyników zapytania");
                }
                connection.Close(); 
            }

            return questions;
        }

        public List<string> GetAllJokes()
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
                        jokes.Add(dataReader["joke"].ToString());
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
    }
}
