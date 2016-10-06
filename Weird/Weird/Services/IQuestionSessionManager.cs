using Weird.Data;

namespace Weird.Services
{
    public interface IQuestionSessionManager
    {
        Question GetNewQuestion(); //Returns Question with Answers Object

        QuestionAnswer CheckAnswer(int questionId, int answerId); //Returns Session Object Checks session for questions already asked.Returns a random unanswered question.

        QuestionSession GetSummary();
    }
}