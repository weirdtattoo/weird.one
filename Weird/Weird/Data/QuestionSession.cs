namespace Weird.Services
{
    public class QuestionSession
    {
        public int Id { get; set; }
        public int CorrectQuestions { get; set; }
        public int TotalQuestions { get; set; }
        public string SummaryMessage { get; } = "Tattoo received : Pink pyramid - Thanks for doing the quiz, until next time... ¡Adios Amigos!";

    }
}