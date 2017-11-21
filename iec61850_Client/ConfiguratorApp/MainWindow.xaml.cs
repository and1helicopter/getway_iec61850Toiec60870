using ConfiguratorApp.ViewModel;
using System.Windows;
using System;

namespace ConfiguratorApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			DataContext = new HelloViewModel();
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			this.HtmlView.Dispose();
		}
	}
}
