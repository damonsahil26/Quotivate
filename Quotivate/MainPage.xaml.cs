namespace Quotivate
{
    public partial class MainPage : ContentPage
    {
        List<string> quotesList = new List<string>();

        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await LoadMauiAsset();
        }

        async Task LoadMauiAsset()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("quotes.txt");
            using var reader = new StreamReader(stream);

            while (reader.Peek() != -1)
            {
                quotesList.Add(reader.ReadLine());
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var random = new Random();
            SetBackgroundGradientColor(random);
            int index = random.Next(quotesList.Count);

            lbl_quotes.Text = quotesList[index];

        }

        private void SetBackgroundGradientColor(Random random)
        {
            var startColor = System.Drawing.Color
                .FromArgb(random.Next(0, 256),
                random.Next(0, 256),
                random.Next(0, 256));

            var endColor = System.Drawing.Color
                .FromArgb(random.Next(0, 256),
                random.Next(0, 256),
                random.Next(0, 256));

            var colors = ColorUtility.ColorControls.GetColorGradient(startColor, endColor, 6);

            float stopOffset = .0f;

            var stops = new GradientStopCollection();

            foreach (var item in colors)
            {
                stops.Add(new GradientStop(Color.FromArgb(item.Name), stopOffset));
                stopOffset += .2f;
            }

            var gradient = new LinearGradientBrush(stops,new Point(0,0), new Point(1,1));

            background.Background = gradient;
        }
    }

}
