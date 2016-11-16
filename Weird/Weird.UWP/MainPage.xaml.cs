namespace Weird.UWP
{
    public sealed partial class MainPage
    {
        private string dbPath = "";

        public MainPage()
        {
            this.InitializeComponent();
            LoadApplication(new Weird.App(dbPath));
            
        }
    }
}
