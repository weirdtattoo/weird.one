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
        private bool _isLastQuestion = false;
        internal static string FontFamily = Device.OnPlatform("MarkerFelt-Thin", "Droid Sans Mono", "Segoe UI");

        public QuestionPage(IWeirdDatabase database)
        {
             
            _testSm = new QuestionSessionManager(database);
 
            _header = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                FontAttributes = FontAttributes.None,
                HorizontalOptions = LayoutOptions.Center,
                FontFamily = FontFamily,
                Margin = 5,
                TextColor = Color.FromHex("#373a3c")
            };

           

            var relativeLayout = new RelativeLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.FromHex("#f9f9f9"),
                //TODO does not work ... Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5)
            };

            relativeLayout.Children.Add(_header, Constraint.RelativeToParent((parent) => { return 0; }));


            _contentView = new ContentView();


            _stackLayout = new StackLayout
            {
                Spacing = 10,
                HorizontalOptions =
                    LayoutOptions.FillAndExpand,
                Padding = new Thickness(5, 5, 5, 5)
            };


            _button = new Button
            {
                Text = "Check",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                IsEnabled = false,
            };

            _button.Clicked += OnButtonClicked;

            relativeLayout.Children.Add(_button,
                Constraint.RelativeToParent((parent) => { return 5; }),
                Constraint.RelativeToParent((parent) => { return parent.Height - 50; }),
                Constraint.RelativeToParent((parent) => { return parent.Width - 10; })
            );

            relativeLayout.Children.Add(
                _stackLayout,
                Constraint.RelativeToView(_header, (parent, sibling) => { return 10; }),
                Constraint.RelativeToView(_header, (parent, sibling) => { return sibling.Height + 30; }),
                Constraint.RelativeToParent((parent) => { return parent.Width - 20; })
            );


            _contentViewContent = new Label {FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))};
            //Label... no padding!!!

            _contentView.Content = _contentViewContent;
            _contentViewContent.IsVisible = false;

            //var parentContainer = (RelativeLayout)Content;


            relativeLayout.Children.Add(_contentView,
                Constraint.RelativeToParent((parent) => { return 0; }),
                Constraint.RelativeToParent((parent) => { return parent.Height/3; }),
                Constraint.RelativeToParent((parent) => { return parent.Width - 20; }),
                Constraint.RelativeToParent((parent) => { return parent.Height/3; }));


            this.Content = relativeLayout;
            LoadQuestion();
        }

        private void LoadQuestion()
        {
            DisableCheck();

            if (_isLastQuestion)
            {
                var summary = _testSm.GetSummary();
                _button.IsVisible = false;
                _stackLayout.IsVisible = false;
                _header.Text = "Score " + summary.CorrectQuestions + "/" +
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

        private void AddButtons(StackLayout stackLayout, List<Answer> questionAnswers)
        {
            foreach (var questionAnswer in questionAnswers)
            {
                var button2 = new Button
                {
                    Text = questionAnswer.Text,
                    AutomationId = questionAnswer.Id.ToString(),
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Font = Font.SystemFontOfSize(NamedSize.Large),
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
                EnableCheck();


                someButton.BackgroundColor = Color.FromHex("#1B82DB");
                someButton.BorderColor = Color.FromHex("#0275d8");

                _selectedId = someButton.AutomationId;
                if (_lastSelectedButton != null)
                {
                    // _lastSelectedButton.BorderColor = default(Color);
                    _lastSelectedButton.BackgroundColor = default(Color);
                }

                _lastSelectedButton = someButton;
            }
        }

        private void EnableCheck()
        {
            if (!_button.IsEnabled)
            {
                _button.BackgroundColor = Color.FromHex("#449d44");
                _button.BorderColor = Color.FromHex("#419641");
                _button.IsEnabled = true;
            }
        }


        private void DisableCheck()
        {
            _button.BackgroundColor = default(Color);
            _button.BorderColor = default(Color);
            _button.IsEnabled = false;
        }


        void OnButtonClicked(object sender, EventArgs e)
        {
            if (_contentViewContent.IsVisible)
            {
                _contentViewContent.IsVisible = false;


                LoadQuestion();

                _contentViewContent.Text = "";
                _contentViewContent.BackgroundColor = default(Color);
                _button.Text = "Check";
            }

            else
            {
                _contentViewContent.IsVisible = true;

                foreach (var child in _stackLayout.Children)
                {
                    Button someButton = child as Button;
                    if (someButton != null)
                    {
                        someButton.IsEnabled = false;
                    }
                }


                int selectItemId = int.Parse(_selectedId);
                var questionAnswer = _testSm.CheckAnswer(_question.Id, selectItemId);

                _contentViewContent.Text = questionAnswer.Explanation;

                if (questionAnswer.IsCorrect)
                {
                    _contentViewContent.BackgroundColor = Color.FromHex("#dff0d8");
                    //Border color???  _contentViewContent. = Color.FromHex("#d0e9c6");
                    _contentViewContent.TextColor = Color.FromHex("#3c763d");
                }
                else
                {
                    //border ??#ebcccc
                    _contentViewContent.BackgroundColor = Color.FromHex("#f2dede");
                    _contentViewContent.TextColor = Color.FromHex("#a94442");
                }


                _button.Text = "Continue";
            }
        }
    }
}