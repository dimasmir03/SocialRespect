using System.Net;
using System.Text;

public class HttpServer
{
    private readonly HttpListener _listener = new HttpListener();

    public HttpServer(string prefix)
    {
        _listener.Prefixes.Add(prefix);
    }

    public void Start()
    {
        _listener.Start();
        Console.WriteLine("Listening...");
        while (true)
        {
            var context = _listener.GetContext();
            new Thread(() => Process(context)).Start();
        }
    }

    public void Stop()
    {
        _listener.Stop();
        _listener.Close();
    }

    public void Process(HttpListenerContext context)
    {
        var numString = context.Request.QueryString["number"];
        int number;
        if (int.TryParse(numString, out number))
        {
            var result = number * number;
            var responseString = "Result: " + result;
            var buffer = Encoding.UTF8.GetBytes(responseString);
            context.Response.ContentLength64 = buffer.Length;
            var output = context.Response.OutputStream;
            output.WriteAsync(buffer, 0, buffer.Length);
        }
        context.Response.Close();
    }
}

class Program
{
    static void Main(string[] args)
    {
        string host = "http://localhost:8080/";
        if (args.Length != 0) { host = args[0]; }
        var server = new HttpServer("http://socialrespect.rf.gd/");
        server.Start();
    }
}