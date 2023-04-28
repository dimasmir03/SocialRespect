namespace SocialCredit;

public class NewPage1 : ContentPage
{
    Label l;
    Button button;
    public NewPage1()
    {
        Grid grid = new Grid();

        button = new Button
        {
            Text = "Õ‡ÊÏË",
            FontSize = 22,
            BorderWidth = 1,
            BackgroundColor = Colors.LightPink,
            TextColor = Colors.DarkRed,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
        l = new Label 
        {
            Text = "123",
            FontSize = 22,
            BackgroundColor = Colors.LightPink,
            TextColor = Colors.DarkRed,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Start
        };
        

        button.Clicked += OnButtonClicked;

        grid.Children.Add(button);
        grid.Children.Add(l);
        Content = grid;
    }

    private async void OnButtonClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        button.Text = "ÀŒ’";

        string url = $"https://socialrespect.000webhostapp.com/?";
        System.Net.WebClient client;
        string result;
        try
        {
        client = new System.Net.WebClient();
        result = client.DownloadString(url);
            l.Text = result;
            button.Text = result;

        }
        catch (Exception)
        {
            throw;
        }

        string fileName = "./temp.txt";

        try
        {
            File.ReadAllText("./temp.txt");
        }
        catch (FileNotFoundException)
        {
            try
            {
                client = new System.Net.WebClient();
                result = client.DownloadString(url+"nuser");
                File.WriteAllText(fileName, result);
                l.Text = result;
            }
            catch (Exception)
            {
                
                throw;
            }
            
            throw;
        }
        
        //fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "temp.txt");
        //File.WriteAllText(fileName, result);
       // await DisplayAlert("Alert", File.ReadAllText(fileName), "OK");
    }
}