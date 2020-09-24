using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Controls
{
	public static class Helper
	{
		public static void SetForegroundColor(this ProgressBar progressBar, Color color)
		{
			var textBlock = progressBar.Template.FindName("txtValue", progressBar) as TextBlock;

			if (textBlock != null)
			{
				textBlock.Foreground = new SolidColorBrush(color);
			}
		}
	}
}
