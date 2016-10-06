using Weird.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class QuestionSessionManagerTests
    {
        [TestMethod]
        public void TestNewQuestionCheckAnswerAndGetSummary()
        {
            var testSm = new QuestionSessionManager(new TestWeirdDatabase());

            

            for (int i = 0; i < 10; i++)
            {
                Assert.IsTrue(i < 5);
                var question = testSm.GetNewQuestion();

                if (i == 0)
                {
                    var questionAnswer = testSm.CheckAnswer(question.Id, 2);
                    Assert.AreEqual(questionAnswer.IsCorrect, false);
                    Assert.AreEqual(questionAnswer.Explanation , "One plus one is two");

                }


                if (i == 1)
                {
                    var questionAnswer = testSm.CheckAnswer(question.Id, 2);
                    Assert.AreEqual(questionAnswer.IsCorrect, false);
                    Assert.AreEqual(questionAnswer.Explanation, "(often be absorbed in) take up the attention of(someone); interest greatly");

                }

                if (i == 2)
                {
                    var questionAnswer = testSm.CheckAnswer(question.Id, 2);
                    Assert.AreEqual(questionAnswer.IsCorrect, true);
                    Assert.AreEqual(questionAnswer.Explanation, "Accuse of");

                }


                if (i == 3)
                {
                    var questionAnswer = testSm.CheckAnswer(question.Id, 2);
                    Assert.AreEqual(questionAnswer.IsCorrect, true);
                    Assert.AreEqual(questionAnswer.Explanation, "“It’s” is only ever used when short for “it is”. “Its” indicates something belonging to something that isn’t masculine or feminine (like “his” and “hers”, but used when you’re not talking about a person).");

                }

                

                if (question.IsLastQuestion)
                {
                  var questionSummary =  testSm.GetSummary();
                  Assert.AreEqual(questionSummary.TotalQuestions, 4);
                  Assert.AreEqual(questionSummary.CorrectQuestions, 2);
                  break;
                    
                }
            }
         
        }
    }
}
