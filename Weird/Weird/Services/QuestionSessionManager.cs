using System.Collections.Generic;
using System.Linq;
using Weird.Data;

namespace Weird.Services
{
    public class QuestionSessionManager : IQuestionSessionManager
    {
        
        private readonly List<Question> _questions;
        private readonly QuestionSession _questionSession;
        private int _questionIndex;
        private int _correctQuestions;
        const int QuestionCount = 10; //HardCoded....for now...

        public QuestionSessionManager(IWeirdDatabase weirdDatabase)
        {
            
            _questions = weirdDatabase.GetRandomQuestions(QuestionCount);
            _questionSession = new QuestionSession {TotalQuestions = _questions.Count};
        }


        public Question GetNewQuestion()
        {
            var question = _questions[_questionIndex];
            _questionIndex++;

            if (_questionIndex == _questions.Count)
            {
                question.IsLastQuestion = true;
            }
            return question;


        }

        public QuestionSession GetSummary()
        {
            _questionSession.CorrectQuestions = _correctQuestions;
            return _questionSession;
        }

        public QuestionAnswer CheckAnswer(int questionId, int answerId)
        {
            QuestionAnswer questionAnswer = new QuestionAnswer();
            var question = _questions.First(d => d.Id == questionId);
            questionAnswer.Explanation = question.Explanation;
            var questionAnswerIsCorrent = question.Answers.First(d => d.Id == answerId).IsCorrect;
            if (questionAnswerIsCorrent)
            {
                ++_correctQuestions;
              
            }
            questionAnswer.IsCorrect = questionAnswerIsCorrent;
            return questionAnswer;
        }

      
    }
}