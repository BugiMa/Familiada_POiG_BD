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
            List<string> answers = new List<string>();
            List<int> points = new List<int>();
            List<string> questions = new List<string>();
            int questionID=0;
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
                        questions[questionID] = dataReader["pytanie"].ToString();
                        questionID = (int)dataReader["id_pytania"];
                        questionID++;
                    }
                }
                else
                {
                    Console.WriteLine("Brak wyników zapytania");
                }
                connection.Close();
                MySqlCommand command2 = new MySqlCommand(ALL_ANSWERS_QUERY, connection);
                connection.Open();
                dataReader = command2.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        answers[questionID] = dataReader["odpowiedz"].ToString();
                    }
                }
                else
                {
                    Console.WriteLine("Brak wyników zapytania");
                }
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
    }
}
