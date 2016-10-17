using System.Collections.Generic;
using Weird.Data;
using Xamarin.Forms;

namespace Weird
{
    public class App : Application
    {
        public App(string dbPath)
        {
            //var database = new WeirdDatabase(dbPath);
            var database = new TestWeirdDatabase();
            MainPage = new NavigationPage(new QuestionPage(database));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }


    public class TestWeirdDatabase : IWeirdDatabase
    {
        readonly List<Question> _questions = new List<Question>();


        Question LoadQuestion(string text, string answerCommaList, int rightanswerIndex, string explanation)
        {
            var questionId = _questions.Count + 1;

            Question question = new Question
            {
                Text = text,
                Id = questionId,
                Explanation = explanation
            };

            List<Answer> answers = new List<Answer>();

            int answerIndex = 0;
            string[] answerArray = answerCommaList.Split(',');

            foreach (var answerItem in answerArray)
            {
                answerIndex++;


                Answer answer = new Answer
                {
                    Text = answerItem,
                    Id = answerIndex,
                    IsCorrect = rightanswerIndex == answerIndex
                };
                answers.Add(answer);
            }

            question.Answers = answers;
            return question;
        }


        public TestWeirdDatabase()
        {
            _questions.Add(LoadQuestion("What is 1+1?", "2,It is a question", 1, "One plus one is two"));
            _questions.Add(LoadQuestion("Hera was absorbed __ the movie", "at,with,in", 3,
                "(often be absorbed in) take up the attention of(someone); interest greatly"));
            _questions.Add(LoadQuestion("East accused the North __ stealing his 4.5 million diamond ring", "for,of,with",
                2, "Accuse of"));
            _questions.Add(LoadQuestion("The sofa looks great with it’s new cover", "Correct,Wrong", 2,
                "“It’s” is only ever used when short for “it is”. “Its” indicates something belonging to something that isn’t masculine or feminine (like “his” and “hers”, but used when you’re not talking about a person)."));
        }


        public List<Question> GetRandomQuestions(int number)
        {
            return _questions;
        }
    }

}


