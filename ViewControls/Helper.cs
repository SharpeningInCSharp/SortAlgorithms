using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace ViewControls
{
	public static class Helper
	{
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
			if(border is null)
			{
				progressBar.Foreground = new SolidColorBrush(Colors.Cornsilk);
			}
			else
			{
				border.Background = new SolidColorBrush(Colors.Cornsilk);
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
