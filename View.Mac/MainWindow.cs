using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace View.Mac
{
    public partial class MainWindow : MonoMac.AppKit.NSWindow
    {
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
            View();
        }

        partial void buttonView_Clicked(NSObject sender)
        {
            View();
        }

        async public void View()
        {
            Clear();
            NetAtmo.Gadget gadget = new NetAtmo.Gadget(Config.Id, Config.Secret, Config.UserName, Config.Password);
            if (await gadget.ClientCredentials.ExecuteAsync())
            {
                if (await gadget.DeviceList.ExecuteAsync())
                {
                    foreach (var device in gadget.DeviceList.Executed.Result.Body.Devices)
                        WriteLine("Station={0}", device.StationName);

                    foreach (var device in gadget.DeviceList.Executed.Result.Body.Devices)
                        WriteLine("Name={0} Temp={1}", device.ModuleName, device.last_data_store.Temperature.ToString());

                    foreach (var module in gadget.DeviceList.Executed.Result.Body.Modules)
                        WriteLine("Name={0} Temp={1}", module.ModuleName, module.last_data_store.Temperature.ToString());
                }
                else if (gadget.DeviceList.Executed.IsException)
                    WriteLine("Exception gadget.DeviceList {0}", gadget.DeviceList.Executed.Exception.ToString());
                else
                    WriteLine("Error gadget.DeviceList");
            }
            else if (gadget.ClientCredentials.Executed.IsException)
                WriteLine("Exception gadget.ClientCredentials {0}", gadget.ClientCredentials.Executed.Exception.ToString());
            else
                WriteLine("Error gadget.ClientCredentials");
        }

        protected void Clear()
        {
            if (textFieldView != null)
                textFieldView.StringValue = "";
        }

        public void WriteLine(string format, params object[] args)
        {
            textFieldView.StringValue += string.Format(format, args) + "\r\n";
        }
    }
}

