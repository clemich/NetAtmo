using System;

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
                    foreach (var station in gadget.Stations)
                        Console.WriteLine("Station={0}", station.Value);

                    foreach (var device in gadget.DeviceList.Executed.Result.Data.Body.Devices)
                        foreach (var datastore in device.last_data_store)
                            Console.WriteLine("Name={0} Temp={1}", gadget.Modules[datastore.Key], datastore.Value.Temperature);
                }
                else if (gadget.DeviceList.Executed.IsException)
                    Console.WriteLine("Exception gadget.DeviceList {0}", gadget.DeviceList.Executed.Exception.ToString());
                else
                    Console.WriteLine("Error gadget.DeviceList {0}", gadget.DeviceList.Executed.Result.StatusDescription);
            }
            else if (gadget.ClientCredentials.Executed.IsException)
                Console.WriteLine("Exception gadget.ClientCredentials {0}", gadget.ClientCredentials.Executed.Exception.ToString());
            else
                Console.WriteLine("Error gadget.ClientCredentials {0}", gadget.ClientCredentials.Executed.Result.StatusDescription);
        }

        public static void Main(string[] args)
        {
            View();
            Console.ReadLine();
        }
    }
}
