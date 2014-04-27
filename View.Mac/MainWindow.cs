using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace View.Mac
{
    public partial class MainWindow : MonoMac.AppKit.NSWindow
    {
        #region Constructors

        // Called when created from unmanaged code
        public MainWindow(IntPtr handle) : base(handle)
        {
            Initialize();
        }
        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public MainWindow(NSCoder coder) : base(coder)
        {
            Initialize();
        }
        // Shared initialization code
        void Initialize()
        {
        }

        #endregion

        async public void View()
        {
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

        partial void buttonView_Clicked(NSObject sender)
        {
            View();
        }

        protected void Clear()
        {
            textFieldView.StringValue= "";
        }

        public void WriteLine(string format, params object[] args)
        {
            textFieldView.StringValue+= string.Format(format, args)+ "\r\n";
        }



    }
}

