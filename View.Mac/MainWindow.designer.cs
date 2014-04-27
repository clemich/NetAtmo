// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoMac.Foundation;
using System.CodeDom.Compiler;

namespace View.Mac
{
	[Register ("MainWindow")]
	partial class MainWindow
	{
		[Outlet]
		MonoMac.AppKit.NSButton buttoView { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField textFieldView { get; set; }

		[Action ("buttonView_Clicked:")]
		partial void buttonView_Clicked (MonoMac.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (buttoView != null) {
				buttoView.Dispose ();
				buttoView = null;
			}

			if (textFieldView != null) {
				textFieldView.Dispose ();
				textFieldView = null;
			}
		}
	}

	[Register ("MainWindowController")]
	partial class MainWindowController
	{
		[Outlet]
		MonoMac.AppKit.NSButton buttonView { get; set; }

		[Outlet]
		MonoMac.AppKit.NSScrollView ScrollView { get; set; }

		[Action ("buttonView_Clicked:")]
		partial void buttonView_Clicked (MonoMac.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (buttonView != null) {
				buttonView.Dispose ();
				buttonView = null;
			}

			if (ScrollView != null) {
				ScrollView.Dispose ();
				ScrollView = null;
			}
		}
	}
}
