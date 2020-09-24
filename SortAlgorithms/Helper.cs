using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SortAlgorithms
{
	public static class Helper
	{
		public static void ToStartButton(this Button button)
		{
			var rect = button.Template.FindName("innerRect", button) as Rectangle;
			if(rect is null)
			{
				button.Content = "Start sort";
				button.Background = new SolidColorBrush(Colors.LightBlue);
			}
			else
			{
				button.Content = "Start sort";
				rect.Fill = new SolidColorBrush(Colors.LightBlue);
			}
		}

		public static void ToINTButton(this Button button)
		{
			var rect = button.Template.FindName("innerRect", button) as Rectangle;
			if (rect is null)
			{
				button.Content = "Interrupt";
				button.Background = new SolidColorBrush(Colors.Red);
			}
			else
			{
				button.Content = "Interrupt";
				rect.Fill = new SolidColorBrush(Colors.Red);
			}
		}

		public static void ToNormalView(this ProgressBar progressBar)
		{
			var border = progressBar.Template.FindName("PART_Indicator", progressBar) as Border;
			if (border is null)
			{
				progressBar.Foreground = new SolidColorBrush(Colors.DarkBlue);
			}
			else
			{
				border.Background = new SolidColorBrush(Colors.DarkBlue);
			}
		}

		public static void ToSelectedView(this ProgressBar progressBar)
		{
			var border = progressBar.Template.FindName("PART_Indicator", progressBar) as Border;
			if (border is null)
			{
				progressBar.Foreground = new SolidColorBrush(Colors.DarkCyan);
			}
			else
			{
				border.Background = new SolidColorBrush(Colors.DarkCyan);
			}
		}

		public static void ToComparedView(this ProgressBar progressBar)
		{
			var border = progressBar.Template.FindName("PART_Indicator", progressBar) as Border;
			if (border is null)
			{
				progressBar.Foreground = new SolidColorBrush(Colors.Green);
			}
			else
			{
				border.Background = new SolidColorBrush(Colors.Green);
			}
		}


		public static void ToComparableView(this ProgressBar progressBar)
		{
			var border = progressBar.Template.FindName("PART_Indicator", progressBar) as Border;
			if (border is null)
			{
				progressBar.Foreground = new SolidColorBrush(Colors.Red);
			}
			else
			{
				border.Background = new SolidColorBrush(Colors.Red);
			}
		}
	}
}
