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
        private Question _question;
    
        private readonly ContentView _contentView;

        private Button _lastSelectedButton = null;
        private string _selectedId = "";
        private readonly Label _contentViewContent;
        private readonly Button _button;
        private readonly Label _header;
        private StackLayout _stackLayout;
        private bool _isLastQuestion =  false;
        internal static string FontFamily = Device.OnPlatform("MarkerFelt-Thin", "Droid Sans Mono", "Segoe UI");
        public QuestionPage(IWeirdDatabase database)
        {

           // this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            _testSm = new QuestionSessionManager(database);
       


          _header = new Label
            {

              FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
              FontAttributes = FontAttributes.Bold,
              HorizontalOptions = LayoutOptions.Center,
              FontFamily = FontFamily,
              Margin  = 5
          };


           

          

            var relativeLayout = new RelativeLayout();
            relativeLayout.HorizontalOptions = LayoutOptions.FillAndExpand;

            relativeLayout.Children.Add(_header, Constraint.RelativeToParent((parent) =>
            {
                return 0;
            }));

           
            _contentView  = new ContentView();


            _stackLayout = new StackLayout {Spacing = 10, HorizontalOptions = 
                LayoutOptions.FillAndExpand };

             
            _button = new Button
            {
                  
                     Text = "Check",
                     Font = Font.SystemFontOfSize(NamedSize.Large),
                     BorderWidth = 1,
               

                //WidthRequest = 500


            };
           
            _button.Clicked += OnButtonClicked;

            relativeLayout.Children.Add(_button, Constraint.RelativeToParent((parent) =>
                {
                    return 0;// (parent.Width / 2) - (_button.Width / 2);
            }),
             Constraint.RelativeToParent((parent) => {
                 return parent.Height -50;
             }),
             Constraint.RelativeToParent((parent) => {
                 return parent.Width ;
             })


             );

            relativeLayout.Children.Add(
                _stackLayout,
                Constraint.RelativeToView(_header, (parent, sibling) => {
                return 10;
            }),
             Constraint.RelativeToView(_header, (parent, sibling) => {
                 return sibling.Height + 30;
             }),

            Constraint.RelativeToParent((parent) => {
                return parent.Width - 20;
            })


            );




            _contentViewContent = new Label();
          

             
            _contentView.Content = _contentViewContent;
            _contentViewContent.IsVisible = false;

            //var parentContainer = (RelativeLayout)Content;


            relativeLayout.Children.Add(_contentView,
              Constraint.RelativeToParent((parent) => {
                  return parent.Width / 3;
              }),
              Constraint.RelativeToParent((parent) => {
                  return parent.Height / 2;
              }));



            //https://forums.xamarin.com/discussion/17742/relativelayout-in-xaml





            this.Content = relativeLayout;
            LoadQuestion();


        }

        private void LoadQuestion()
        {
            if (_isLastQuestion)
            {
                var summary = _testSm.GetSummary();
                _button.IsVisible = false;
                _stackLayout.IsVisible = false;
                _header.Text = "Score " +summary.CorrectQuestions + "/" +
                summary.TotalQuestions + " - " + summary.SummaryMessage;


            }
            else
            {
                _question = _testSm.GetNewQuestion();
                _isLastQuestion = _question.IsLastQuestion;
                _header.Text = _question.Text;

                _stackLayout.Children?.Clear();


                AddButtons(_stackLayout, _question.Answers);
            }
            



        }

        private  void AddButtons(StackLayout stackLayout, List<Answer> questionAnswers)
        {
            foreach (var questionAnswer in questionAnswers)
            {
                var button2 = new Button
                {
                    Text = questionAnswer.Text,
                    AutomationId = questionAnswer.Id.ToString(),
                    HorizontalOptions = LayoutOptions.FillAndExpand

                };

                button2.Clicked += OnSelectAnswerButtonClicked;
                stackLayout.Children.Add(button2);
            }
         

        }


   

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
            if (_contentViewContent.IsVisible)
            {
                _contentViewContent.IsVisible = false;
                LoadQuestion();

                _contentViewContent.Text = "";
                _contentViewContent.BackgroundColor =  default(Color);
                _button.Text = "Check";
            }

            else
            {
                _contentViewContent.IsVisible = true;

                int selectItemId = int.Parse(_selectedId);
                var questionAnswer = _testSm.CheckAnswer(_question.Id, selectItemId);

                _contentViewContent.Text = questionAnswer.Explanation;
                _contentViewContent.BackgroundColor = questionAnswer.IsCorrect ? Color.Green : Color.Red;
               
                _button.Text = "Continue";
            }



        }
    }
}

