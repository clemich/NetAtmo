using System;
using NetAtmo;

namespace View.Command
{
    class MainClass
    {
        async public static void View()
        {
            NetAtmo.Gadget gadget = new NetAtmo.Gadget(Config.Id, Config.Secret, Config.UserName, Config.Password);
            if (await gadget.ClientCredentials.ExecuteAsync())
            {
                if (await gadget.DeviceList.ExecuteAsync())
                {
                    foreach (var device in gadget.DeviceList.Executed.Result.Body.Devices)
                        Console.WriteLine("Station={0}", device.StationName);

                    foreach (var device in gadget.DeviceList.Executed.Result.Body.Devices)
                        Console.WriteLine("Name={0} Temp={1}", device.ModuleName, device.LastDataStore.Temperature.ToString());

                    foreach (var module in gadget.DeviceList.Executed.Result.Body.Modules)
                        Console.WriteLine("Name={0} Temp={1}", module.ModuleName, module.LastDataStore.Temperature.ToString());
                }
                else if (gadget.DeviceList.Executed.IsException)
                    Console.WriteLine("Exception gadget.DeviceList {0}", gadget.DeviceList.Executed.Exception.ToString());
                else
                    Console.WriteLine("Error gadget.DeviceList");
            }
            else if (gadget.ClientCredentials.Executed.IsException)
                Console.WriteLine("Exception gadget.ClientCredentials {0}", gadget.ClientCredentials.Executed.Exception.ToString());
            else
                Console.WriteLine("Error gadget.ClientCredentials");
        }

        public static void Main(string[] args)
        {
            View();
            Console.ReadLine();
        }
    }
}
