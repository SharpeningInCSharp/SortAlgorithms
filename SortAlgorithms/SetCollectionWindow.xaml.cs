using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SortAlgorithms
{
	/// <summary>
	/// Логика взаимодействия для SetCollectionWindow.xaml
	/// </summary>
	public partial class SetCollectionWindow : Window
	{
		public List<int> Collection { get; private set; } = new List<int>();

		public SetCollectionWindow(bool testFlag)
		{
			InitializeComponent();

			if (testFlag)
				SetDefaultCollection();
		}

		private void SetDefaultCollection()
		{
			CreateCollection();

			foreach (var item in Collection)
			{
				if (item.Equals(Collection.Last()))
					sequenceTextBox.Text += item;
				else
					sequenceTextBox.Text += item + " ";
			}
		}

		private void CreateCollection()
		{
			int size = 20;
			for (int i = 0; i < size / 2; i++)
			{
				Collection.Add(size - i);
				Collection.Add(i + 1);
			}
		}

		private void ConfirmButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				Collection.Clear();
				Collection.AddRange(Array.ConvertAll(sequenceTextBox.Text.Split(' '), int.Parse));

				Close();
			}
			catch
			{
				MessageBox.Show("Wrong input!\nPlease enter only integer numbers separated by comma.");
			}
		}
	}
}
