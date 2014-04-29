using System;
using Gtk;
using View;

public partial class MainWindow: Gtk.Window
{
    Button buttonView; 
    TextView textviewView;

    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();

        Table table = new Table (2, 2, true);

        Add(table); 
        ShowAll ();

        buttonView = new Button ("View");
        // when this button is clicked, it'll run hello()
        buttonView.Clicked += (s, a) =>
        {
            View();
        };


        textviewView = new TextView();

        table.Attach (buttonView, 0, 2, 0, 1);
        table.Attach (textviewView, 0, 2, 1, 2);
        table.ShowAll();

        View();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    async public void View()
    {
        Clear();
        NetAtmo.Gadget gadget = new NetAtmo.Gadget(Config.Id, Config.Secret, Config.UserName, Config.Password);
        if (await gadget.ClientCredentials.ExecuteAsync())
        {
            if (await gadget.DeviceList.ExecuteAsync())
            {
                foreach (var station in gadget.Stations)
                    WriteLine("Station={0}", station.Value);

                foreach (var device in gadget.DeviceList.Executed.Result.Data.Body.Devices)
                    foreach (var datastore in device.last_data_store)
                        WriteLine("Name={0} Temp={1}", gadget.Modules[datastore.Key], datastore.Value.Temperature);
            }
            else if (gadget.DeviceList.Executed.IsException)
                WriteLine("Exception gadget.DeviceList {0}", gadget.DeviceList.Executed.Exception.ToString());
            else
                WriteLine("Error gadget.DeviceList {0}", gadget.DeviceList.Executed.Result.StatusDescription);
        }
        else if (gadget.ClientCredentials.Executed.IsException)
            WriteLine("Exception gadget.ClientCredentials {0}", gadget.ClientCredentials.Executed.Exception.ToString());
        else
            WriteLine("Error gadget.ClientCredentials {0}", gadget.ClientCredentials.Executed.Result.StatusDescription);
    }

    protected void Clear()
    {
        if (textviewView != null)
            textviewView.Buffer.Text = "";
    }

    public void WriteLine(string format, params object[] args)
    {
        textviewView.Buffer.Text += string.Format(format, args) + "\r\n";
    }
}
