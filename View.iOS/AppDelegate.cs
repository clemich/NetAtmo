﻿using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace View.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        public override UIWindow Window
        {
            get;
            set;
        }
        // This method is invoked when the application is about to move from active to inactive state.
        // OpenGL applications should use this method to pause.
        public override void OnResignActivation(UIApplication application)
        {
        }
        // This method should be used to release shared resources and it should store the application state.
        // If your application supports background exection this method is called instead of WillTerminate
        // when the user quits.
        public override void DidEnterBackground(UIApplication application)
        {
        }
        // This method is called as part of the transiton from background to active state.
        public override void WillEnterForeground(UIApplication application)
        {
        }
        // This method is called when the application is about to terminate. Save data, if needed.
        public override void WillTerminate(UIApplication application)
        {
        }

        RootElement rootElement;
        Section textSection;
        Section buttonSection;

        async public void Refresh()
        {
            textSection.Clear();
            NetAtmo.Gadget gadget = new NetAtmo.Gadget(Config.Id, Config.Secret, Config.UserName, Config.Password);
            if (await gadget.ClientCredentials.ExecuteAsync())
            {
                if (await gadget.DeviceList.ExecuteAsync())
                {
                    foreach (var station in gadget.Stations)
                        textSection.Add(new  StringElement("Station", station.Value));

                    foreach (var device in gadget.DeviceList.Executed.Result.Data.Body.Devices)
                        foreach (var datastore in device.last_data_store)
                            textSection.Add(new StringElement(gadget.Modules[datastore.Key], datastore.Value.Temperature.ToString()));
                }
                else if (gadget.DeviceList.Executed.IsException)
                    textSection.Add(new StringElement("Exception gadget.DeviceList {0}", gadget.DeviceList.Executed.Exception.ToString()));
                else
                    textSection.Add(new StringElement("Error gadget.DeviceList {0}", gadget.DeviceList.Executed.Result.StatusDescription));
            }
            else if (gadget.ClientCredentials.Executed.IsException)
                textSection.Add(new StringElement("Exception gadget.ClientCredentials {0}", gadget.ClientCredentials.Executed.Exception.ToString()));
            else
                textSection.Add(new StringElement("Error gadget.ClientCredentials {0}", gadget.ClientCredentials.Executed.Result.StatusDescription));
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            Window = new UIWindow(UIScreen.MainScreen.Bounds);

            rootElement = new RootElement("View.iOS - NetAtmo");

            textSection = new Section();

            buttonSection = new Section()
            {
                new StringElement("Refresh", () =>
                {
                    Refresh();
                })  
            };

            Refresh();

            rootElement.Add(buttonSection);
            rootElement.Add(textSection);


            var rootVC = new DialogViewController(rootElement);
            var nav = new UINavigationController(rootVC);
            Window.RootViewController = nav;
            Window.MakeKeyAndVisible();

            return true;
        }
    }
}

