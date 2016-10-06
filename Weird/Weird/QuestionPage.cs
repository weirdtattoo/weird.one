using Weird.Data;
using Xamarin.Forms;

namespace Weird
{
    public class QuestionPage : ContentPage
    {
        private readonly IWeirdDatabase _database;
        private readonly ListView _thoughtList;

        public QuestionPage(IWeirdDatabase database)
        {
            _database = database;
            Title = "Questions";
            var thoughts = _database.GetRandomQuestions(10);

            _thoughtList = new ListView();
            _thoughtList.ItemsSource = thoughts;
            _thoughtList.ItemTemplate = new DataTemplate(typeof(TextCell));
            _thoughtList.ItemTemplate.SetBinding(TextCell.TextProperty, "Id");
            _thoughtList.ItemTemplate.SetBinding(TextCell.DetailProperty, "Text");


            Content = _thoughtList;
        }

        public void Refresh()
        {
            _thoughtList.ItemsSource = _database.GetRandomQuestions(10);
        }
    }
}