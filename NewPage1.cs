namespace SocialCredit;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Net.Http.Json;

public class NewPage1 : ContentPage
{
    Label l;
    Button button;
    string url = $"http://185.154.192.96:8989";
    string filename = "/storage/emulated/0/Documents/id";
    HttpClient client = new HttpClient();
    StackLayout grid = new StackLayout() ;
    StackLayout gridnewuser = new StackLayout();
    Entry iname;
    string uid = "";


    public NewPage1()
    {


        button = new Button
        {
            Text = "Нажми",
            FontSize = 22,
            BorderWidth = 1,
            BackgroundColor = Colors.LightPink,
            TextColor = Colors.DarkRed,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
        l = new Label
        {
            Text = "Кредиты пользователей",
            FontSize = 22,
            BackgroundColor = Colors.LightPink,
            TextColor = Colors.DarkRed,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Start
        };

        button.Clicked += OnButtonClickedCreate;

        grid.Children.Add(l);
        //grid.Children.Add(button);
        

        iname = new Entry
        {
            Placeholder = "Имя Фамилия",
            HorizontalOptions = LayoutOptions.Center, 
            VerticalOptions = LayoutOptions.Center,
        };
        gridnewuser.Children.Add(l);
        gridnewuser.Children.Add(iname);
        gridnewuser.Children.Add(button);
        
        grid.Loaded += OnLoadMain;
        // Content = gridnewuser;


        OnLoad();
    }

    private void OnLoadMain(object sender, EventArgs e)
    {
        /*try
        {
            var result = client.GetStringAsync(url + "/auth");
            //File.WriteAllText(filename, result.Result.ToString());
            l.Text = "main";
            //l.Text = "Id: " + result.Result.ToString();
            //Content = grid;
        }
        catch (Exception err)
        {
            l.Text = "Ошибка: " + err.Message;
        }*/
    }

    private void OnLoad()
    {
        //l.Text = File.ReadAllText(filename) ;
        if (!File.Exists(filename))
        {
            l.Text = "Новый пользователь";
            Content = gridnewuser;
            button.Text = "Создать";

            return;
        }
        uid =  File.ReadAllText(filename);
        Content = grid;
    }

    private async void OnButtonClickedCreate(object sender, EventArgs e)
    {
        if (button.Text != "Создать") return;
        try
        {
            var request = new
            {
                name = iname.Text,
            };
            var result = await client.PostAsJsonAsync(url + "/newuser", request);

            uid = await result.Content.ReadAsStringAsync();
            
            File.WriteAllText(filename, uid);
            Content = grid;
            l.Text = "Id: " + uid;
            //l.Text = "Создано";
        }
        catch (Exception err)
        {
            l.Text = $"Ошибка: {err.Message}";
        }
    }
}