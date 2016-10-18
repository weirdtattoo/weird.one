using System.Collections.Generic;
using Weird.Data;
using Weird.Services;


namespace Weird
{
    using System;
    using Xamarin.Forms;


    class QuestionPage : ContentPage
    {
        
        int clickTotal = 0;
        private readonly QuestionSessionManager _testSm;
        private readonly Question _question;
    
        private ContentView _contentView;
        private RelativeLayout _relativeLayout;

        public QuestionPage(IWeirdDatabase database)
        {

           // this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            _testSm = new QuestionSessionManager(database);
            _question = _testSm.GetNewQuestion();


            Label header = new Label
            {
                Text = _question.Text,
                Font = Font.BoldSystemFontOfSize(50),
                HorizontalOptions = LayoutOptions.Center
            };


          

            _relativeLayout = new RelativeLayout();

            _relativeLayout.Children.Add(header, Constraint.RelativeToParent((parent) =>
            {
                return 0;
            }));

           
            _contentView  = new ContentView();


            var stackLayout = new StackLayout();

 
            AddButtons(stackLayout, _question.Answers);

          

            stackLayout.Spacing = 10;




            Button button = new Button
            {
                  
                     Text = "Check",
                     Font = Font.SystemFontOfSize(NamedSize.Large),
                     BorderWidth = 1,
                     HorizontalOptions = LayoutOptions.FillAndExpand,
                     VerticalOptions = LayoutOptions.CenterAndExpand,
                    
                    
                 };
            button.Clicked += OnButtonClicked;

            _relativeLayout.Children.Add(button, Constraint.RelativeToParent((parent) => {
                return (parent.Width / 2) - (button.Width / 2);
            }),
             Constraint.RelativeToParent((parent) => {
                 return parent.Height -50;
             }));

            _relativeLayout.Children.Add(stackLayout, Constraint.RelativeToView(header, (parent, sibling) => {
                return 10;
            }),
             Constraint.RelativeToView(header, (parent, sibling) => {
                 return sibling.Height + 30;
             })


            );

            //https://forums.xamarin.com/discussion/17742/relativelayout-in-xaml





            this.Content = _relativeLayout;


 
        }

        private  void AddButtons(StackLayout stackLayout, List<Answer> questionAnswers)
        {
            foreach (var questionAnswer in questionAnswers)
            {
                var button2 = new Button
                {
                    Text = questionAnswer.Text,
                    AutomationId = questionAnswer.Id.ToString()

                };

                button2.Clicked += OnSelectAnswerButtonClicked;
                stackLayout.Children.Add(button2);
            }
         

        }


        private Button _lastSelectedButton = null;
        private string _selectedId = "";
        void OnSelectAnswerButtonClicked(object sender, EventArgs e)
        {
            Button someButton = sender as Button;
          

            if (someButton != null)
            {
               // someButton.BorderColor= Color.Blue;
                someButton.BackgroundColor = Color.Aqua;
                _selectedId = someButton.AutomationId;
                if (_lastSelectedButton != null)
                {
                   // _lastSelectedButton.BorderColor = default(Color);
                    _lastSelectedButton.BackgroundColor = default(Color);
                }

                _lastSelectedButton = someButton;
            }

        }

        void OnButtonClicked(object sender, EventArgs e)
        {

            int selectItemId = int.Parse(_selectedId);

            var questionAnswer = _testSm.CheckAnswer(_question.Id, selectItemId);
            var contentViewContent = new Label
            {
                Text = questionAnswer.Explanation,
                BackgroundColor = questionAnswer.IsCorrect ? Color.Green : Color.Red
            };




            _contentView.Content = contentViewContent;

            var parentContainer = (RelativeLayout) Content;
          

            parentContainer.Children.Add(_contentView,
              Constraint.RelativeToParent((parent) => {
                  return parent.Width / 3;
              }),
              Constraint.RelativeToParent((parent) => {
                  return parent.Height / 2;
              }));



        }
    }
}


//    public class QuestionPage : ContentPage
//    {
//        private readonly IWeirdDatabase _database;
//        private readonly ListView _thoughtList;

//        public QuestionPage(IWeirdDatabase database)
//        {
//            _database = database;
//            Title = "Questions";
//            var thoughts = _database.GetRandomQuestions(10);


//            var testSm = new QuestionSessionManager(new TestWeirdDatabase());
//            var question = testSm.GetNewQuestion();


//            //    for (int i = 0; i < 10; i++)
//            //    {
//            //        Assert.IsTrue(i < 5);
//            //        var question = testSm.GetNewQuestion();

//            //        if (i == 0)
//            //        {
//            //            var questionAnswer = testSm.CheckAnswer(question.Id, 2);
//            //            Assert.AreEqual(questionAnswer.IsCorrect, false);
//            //            Assert.AreEqual(questionAnswer.Explanation, "One plus one is two");

//            //        }


//            //        if (i == 1)
//            //        {
//            //            var questionAnswer = testSm.CheckAnswer(question.Id, 2);
//            //            Assert.AreEqual(questionAnswer.IsCorrect, false);
//            //            Assert.AreEqual(questionAnswer.Explanation, "(often be absorbed in) take up the attention of(someone); interest greatly");

//            //        }

//            //        if (i == 2)
//            //        {
//            //            var questionAnswer = testSm.CheckAnswer(question.Id, 2);
//            //            Assert.AreEqual(questionAnswer.IsCorrect, true);
//            //            Assert.AreEqual(questionAnswer.Explanation, "Accuse of");

//            //        }


//            //        if (i == 3)
//            //        {
//            //            var questionAnswer = testSm.CheckAnswer(question.Id, 2);
//            //            Assert.AreEqual(questionAnswer.IsCorrect, true);
//            //            Assert.AreEqual(questionAnswer.Explanation, "“It’s” is only ever used when short for “it is”. “Its” indicates something belonging to something that isn’t masculine or feminine (like “his” and “hers”, but used when you’re not talking about a person).");

//            //        }


//            //        if (question.IsLastQuestion)
//            //        {
//            //            var questionSummary = testSm.GetSummary();
//            //            Assert.AreEqual(questionSummary.TotalQuestions, 4);
//            //            Assert.AreEqual(questionSummary.CorrectQuestions, 2);
//            //            break;

//            //        }
//            //    }

//            //}

//            //_thoughtList = new ListView();
//            //_thoughtList.ItemsSource = thoughts;
//            //_thoughtList.ItemTemplate = new DataTemplate(typeof(TextCell));
//            //_thoughtList.ItemTemplate.SetBinding(TextCell.TextProperty, "Id");
//            //_thoughtList.ItemTemplate.SetBinding(TextCell.DetailProperty, "Text");


//            //Content = _thoughtList;
//        }

//        public void Refresh()
//        {
//           // _thoughtList.ItemsSource = _database.GetRandomQuestions(10);
//        }
//    }
//}