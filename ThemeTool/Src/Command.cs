/*
 * User: AJ01
 * Date: 24.08.2011
 * Time: 0:39
 */
using System;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Threading;

using AvalonDock;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop;
using ICSharpCode.SharpDevelop.Gui;

namespace ThemeTool
{
	public class ToolCommandDefault : AbstractMenuCommand
	{
		public override void Run()
		{
			var settings = new ToolSettings();
			settings.SetTheme("default");
			settings.SaveSettings("default");
		}
	}
	public class ToolCommandGeneric : AbstractMenuCommand
	{
		public override void Run()
		{
			var settings = new ToolSettings();
			settings.SetTheme("generic");
			settings.SaveSettings("generic");
		}
	}
	
	public class ToolCommandAutumn : AbstractMenuCommand
	{
		public override void Run()
		{
			var settings = new ToolSettings();
			settings.SetTheme("#FF800000");
			settings.SaveSettings("#FF800000");
		}
	}
	public class ToolCommandGrass : AbstractMenuCommand
	{
		public override void Run()
		{
			var settings = new ToolSettings();
			settings.SetTheme("#FF008000");
			settings.SaveSettings("#FF008000");
		}
	}
	public class ToolCommandClassic : AbstractMenuCommand
	{
		public override void Run()
		{
			var settings = new ToolSettings();
			settings.SetTheme("classic");
			settings.SaveSettings("classic");
		}
	}
	public class ToolCommandAero : AbstractMenuCommand
	{
		public override void Run()
		{
			var settings = new ToolSettings();
			settings.SetTheme("aero.normalcolor");
			settings.SaveSettings("aero.normalcolor");
		}
	}
	public class ToolCommandLuna : AbstractMenuCommand
	{
		public override void Run()
		{
			var settings = new ToolSettings();
			settings.SetTheme("luna.normalcolor");
			settings.SaveSettings("luna.normalcolor");
		}
	}
	public class ToolCommandDev10 : AbstractMenuCommand
	{
		public override void Run()
		{
			var settings = new ToolSettings();
			settings.SetTheme("dev2010");
			settings.SaveSettings("dev2010");
		}
	}
	public class ToolCommandDev10Green : AbstractMenuCommand
	{
		public override void Run()
		{
			var settings = new ToolSettings();
			settings.SetTheme("dev2010green");
			settings.SaveSettings("dev2010green");
		}
	}
	public class ToolCommandExpressDark : AbstractMenuCommand
	{
		public override void Run()
		{
			var settings = new ToolSettings();
			settings.SetTheme("ExpressionDark");
			settings.SaveSettings("ExpressionDark");
		}
	}
	public class ToolCommandCustom : AbstractMenuCommand
	{
		public override void Run()
		{
			var colorDialog = new System.Windows.Forms.ColorDialog();
			var result = colorDialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				var color = colorDialog.Color;
				string clrStr = "#" + color.ToArgb().ToString("X8");
				var settings = new ToolSettings();
				settings.SetTheme(clrStr);
				settings.SaveSettings(clrStr);
			}
		}
	}
	public class ToolCommandStartup : AbstractMenuCommand
	{
		public BackgroundWorker bw;
		public override void Run()
		{
			try
			{
				string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(this.GetType()).Location);
				System.Reflection.Assembly.LoadFrom(System.IO.Path.Combine(path, "AvalonDock.Themes.dll"));
				
				bw = new BackgroundWorker();
				bw.DoWork += delegate
				{
					try
					{
						while (ICSharpCode.SharpDevelop.Gui.WorkbenchSingleton.MainWindow == null)
						{
							Thread.Sleep(100);
						}
						ICSharpCode.SharpDevelop.Gui.WorkbenchSingleton.MainWindow.Dispatcher.Invoke
							(DispatcherPriority.Normal, new System.Threading.ThreadStart
							 (
							 	delegate
							 	{
							 		//string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(typeof(ToolSettings)).Location);
							 		//string uri = "/AvalonDock.Themes;component/themes/dev2010.xaml";
							 		//ThemeFactory.ChangeTheme(new Uri(uri, UriKind.RelativeOrAbsolute));
							 		var settings = new ToolSettings();
							 		var theme = settings.LoadSettings();
							 		settings.SetTheme(theme);
							 	}
							 )
							);
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.ToString(), "ToolCommandStartup.Run()");
					}
				};
				bw.RunWorkerAsync();
			}
			catch
			{
			}
		}
	}
}
