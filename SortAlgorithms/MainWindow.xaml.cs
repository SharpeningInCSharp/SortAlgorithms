using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Algorithms;


namespace SortAlgorithms
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private const int onePbSize = 18;
		private const int algAmount = 6;
		private int pbMaxValue = 100;
		private const string textBoxStartText = "Enter array size";
		//flag only for rgr2
		private readonly bool testRgrFlag = true;
		private const string progressBarStyleName = "CollectionItem";

		private AlgorithmType algorithmType;
		private List<int> myOwncollection = null;
		private AlgorithmBase sortAlgorithm;
		private bool makeVisualization = true;
		private bool isOngoing = false;
		private int threadSleepTime = 200;
		private CancellationTokenSource cts = new CancellationTokenSource();

		public MainWindow()
		{
			//TODO: make icon
			//SOLVE: stop all threads on OnClising()
			InitializeComponent();
			Closing += MainWindow_Closing;
			//WindowState = WindowState.Maximized;
		}

		private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{

		}

		private void FillSP(List<int> output)
		{
			ItemsSP.Children.Clear();

			if (output.Count * onePbSize <= MaxWidth)
			{
				makeVisualization = true;

				if (output.Any(x => x < 4))
					pbMaxValue = output.Max();

				if (output.Count * onePbSize > ActualWidth && WindowState != WindowState.Minimized)
					WindowState = WindowState.Maximized;

				foreach (var item in output)
				{
					var pb = new ProgressBar()
					{
						Style = FindResource(progressBarStyleName) as Style,
						Value = item,
						Maximum = pbMaxValue,
					};

					if (pb.Style == null)
					{
						throw new ArgumentNullException($"Can't apply {progressBarStyleName} style");
					}

					ItemsSP.Children.Add(pb);

					//var pb = new SortingItem(item);
					//ItemsSP.Children.Add(pb);
				}
			}
			else
			{
				if (makeVisualization)
					MessageBox.Show("Collection's too big for visualization\n");

				makeVisualization = false;
			}

		}

		/// <summary>
		/// Prepare for sorting(chose size, alg type and initialize alg(if neccessary)
		/// Out initialized collection
		/// Start sorting
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Start_Click(object sender, RoutedEventArgs e)
		{
			if (isOngoing)
			{
				RenameToStartButton();
				cts.Cancel();
				isOngoing = false;
				StopSort();
				return;
			}

			var rnd = new Random();
			int algNum = rnd.Next(0, algAmount);
			if (int.TryParse(CapacityTB.Text, out int amount) && sortAlgorithm == null)
			{
				InitializeAlgorithm(amount);
				ShowSelectedAlg(algNum);
			}
			else if (sortAlgorithm == null)
			{
				InitializeAlgorithm(rnd.Next(20, 100));
				ShowSelectedAlg(algNum);
			}
			else if (sortAlgorithm.CollectionSize == 0)
			{
				if (int.TryParse(CapacityTB.Text, out int size))
					InitializeAlgorithm(size);
				else
					InitializeAlgorithm();
			}

			//TODO: add flag sortAlgorithm.Sorted, if it's true, generate new one sort

			RenameToINTButton();
			FillSP(sortAlgorithm.GetCollection());
			DisableControls();
			StartSort();
		}

		private void RenameToStartButton()
		{
			EnableConrols();
			Start.ToStartButton();
		}

		private void RenameToINTButton()
		{
			Start.ToINTButton();
		}

		private void StopSort()
		{
			sortAlgorithm.Swoped -= SortAlgorithm_Swoped;
			sortAlgorithm.Compared -= SortAlgorithm_Compared;
		}

		private void DisableControls()
		{
			CapacityTB.IsEnabled = false;
			SortTypesCB.IsEnabled = false;
		}

		private void EnableConrols()
		{
			CapacityTB.IsEnabled = true;
			SortTypesCB.IsEnabled = true;
			Start.IsEnabled = true;
		}

		private void StartSort()
		{
			InitializeINT();

			if (makeVisualization)
			{
				sortAlgorithm.Swoped += SortAlgorithm_Swoped;
				sortAlgorithm.Compared += SortAlgorithm_Compared;
			}

			sortAlgorithm.SortFinished += SortAlgorithm_SortFinished;
			Thread thread = new Thread(sortAlgorithm.Sort);
			thread.Start(cts.Token);
		}

		private void InitializeINT()
		{
			isOngoing = true;
			cts = new CancellationTokenSource();
		}

		private void SortAlgorithm_SortFinished(object sender, EventArgs e)
		{
			isOngoing = false;
			try
			{
				Dispatcher.Invoke(new Action(
				delegate ()
				{
					UpdateInfo();
					RenameToStartButton();
				}
				));
			}
			catch
			{
				return;
			}

		}

		private void UpdateInfo()
		{
			TimeToNormalWay(sortAlgorithm.SpentTime);
			ComparationsTB.Text = sortAlgorithm.ComprasionsCount.ToString();
			SwopsTB.Text = sortAlgorithm.SwopsCount.ToString();
		}

		private void TimeToNormalWay(Stopwatch stopwatch)
		{
			TimeTB.Text = new string(stopwatch.Elapsed.ToString().Skip(3).Take(8).ToArray());
		}

		private void ShowSelectedAlg(int algNum)
		{
			ShowCapacity();
			if (algNum < algAmount)
				SortTypesCB.SelectedIndex = algNum;
		}

		private void ShowCapacity()
		{
			var temp = CapacityTB.ActualWidth;
			CapacityTB.Text = sortAlgorithm.CollectionSize.ToString();
			CapacityTB.Width = temp;
		}

		private void SortAlgorithm_Compared(int left, int right)
		{
			try
			{
				Dispatcher.Invoke(new Action(
					delegate ()
					{
						//((ProgressBar)ItemsSP.Children[eAr.Left]).Background = comparedABrush;
						//((ProgressBar)ItemsSP.Children[eAr.Right]).Background = comparedBBrush;
						sortAlgorithm.SpentTime.Stop();
						//((ProgressBar)ItemsSP.Children[right]).IsEnabled = false;
						//((ProgressBar)ItemsSP.Children[left]).IsHitTestVisible = false;
						((ProgressBar)ItemsSP.Children[left]).ToComparedView();
						((ProgressBar)ItemsSP.Children[right]).ToComparableView();
					}
					));

				Thread.Sleep(threadSleepTime);

				Dispatcher.Invoke(new Action(
					delegate ()
					{
						//((ProgressBar)ItemsSP.Children[left]).IsHitTestVisible = true;
						//((ProgressBar)ItemsSP.Children[right]).IsHitTestVisible = true;
						//((ProgressBar)ItemsSP.Children[right]).IsEnabled = true;
						//((ProgressBar)ItemsSP.Children[left]).IsEnabled = true;
						((ProgressBar)ItemsSP.Children[right]).ToNormalView();
						((ProgressBar)ItemsSP.Children[left]).ToNormalView();
						//((ProgressBar)ItemsSP.Children[eAr.Left]).Background = defaultBrush;
						//((ProgressBar)ItemsSP.Children[eAr.Right]).Background = defaultBrush;
						sortAlgorithm.SpentTime.Start();
					}
					));
			}
			catch
			{ return; }

		}

		private void SortAlgorithm_Swoped(int left, int right)
		{
			try
			{
				Dispatcher.Invoke(new Action(() =>
					{
						((ProgressBar)ItemsSP.Children[right]).ToSelectedView();
						((ProgressBar)ItemsSP.Children[left]).ToSelectedView();
					}));

				Thread.Sleep(threadSleepTime);

				Dispatcher.Invoke(new Action(
					delegate ()
					{
						var temp = ((ProgressBar)ItemsSP.Children[left]).Value;
						((ProgressBar)ItemsSP.Children[left]).Value = ((ProgressBar)ItemsSP.Children[right]).Value;
						((ProgressBar)ItemsSP.Children[right]).Value = temp;
						((ProgressBar)ItemsSP.Children[right]).ToNormalView();
						((ProgressBar)ItemsSP.Children[left]).ToNormalView();
						UpdateInfo();
					}
					));

			}
			catch { return; }
		}

		/// <summary>
		/// Initialize algorithm according to type by index=<paramref name="algNum"/>
		/// </summary>
		/// <param name="algNum">algorithm type index</param>
		/// <param name="amount">collection size</param>
		private void InitializeAlgorithm(int amount = 0)
		{
			switch (algorithmType)
			{
				case AlgorithmType.BubbleSort:
					if (myOwncollection == null)
						if (amount <= 0)
							sortAlgorithm = new BubbleAlgorithm();
						else
							sortAlgorithm = new BubbleAlgorithm(amount);
					else
						sortAlgorithm = new BubbleAlgorithm(myOwncollection);
					break;

				case AlgorithmType.CocktailSort:
					if (myOwncollection == null)
						if (amount <= 0)
							sortAlgorithm = new CoctailAlgorithm();
						else
							sortAlgorithm = new CoctailAlgorithm(amount);
					else
						sortAlgorithm = new CoctailAlgorithm(myOwncollection);
					break;

				case AlgorithmType.InsertionSort:
					if (myOwncollection is null)
						if (amount <= 0)
							sortAlgorithm = new InsertionAlgorithm();
						else
							sortAlgorithm = new InsertionAlgorithm(amount);
					else
						sortAlgorithm = new InsertionAlgorithm(myOwncollection);
					break;

				case AlgorithmType.ShellSort:
					if (myOwncollection is null)
						if (amount <= 0)
							sortAlgorithm = new ShellAlgorithm();
						else
							sortAlgorithm = new ShellAlgorithm(amount);
					else
						sortAlgorithm = new ShellAlgorithm(myOwncollection);
					break;

				case AlgorithmType.SelectionSort:
					if (myOwncollection is null)
						if (amount <= 0)
							sortAlgorithm = new SelectionAlgorithm();
						else
							sortAlgorithm = new SelectionAlgorithm(amount);
					else
						sortAlgorithm = new SelectionAlgorithm(myOwncollection);
					break;

				case AlgorithmType.GnomeSort:
					if (myOwncollection is null)
						if (amount <= 0)
							sortAlgorithm = new GnomeAlgorithm();
						else
							sortAlgorithm = new GnomeAlgorithm(amount);
					else
						sortAlgorithm = new GnomeAlgorithm(myOwncollection);
					break;

				case AlgorithmType.BinaryTreeSort:
					if (myOwncollection is null)
						if (amount <= 0)
							sortAlgorithm = new BinaryTree();
						else
							sortAlgorithm = new BinaryTree(amount);
					else
						sortAlgorithm = new BinaryTree(myOwncollection);
					break;

				case AlgorithmType.RadixSort:
					break;

				case AlgorithmType.MergeSort:
					break;

				case AlgorithmType.QuickSort:
					break;

				default:
					throw new WrongAlgExcetion();
			}
		}

		private void CapacityTB_GotFocus(object sender, RoutedEventArgs e)
		{
			if (CapacityTB.Text == textBoxStartText)
			{
				var width = CapacityTB.ActualWidth;
				CapacityTB.Text = "";
				CapacityTB.Width = width;
			}
		}

		private void CapacityTB_LostFocus(object sender, RoutedEventArgs e)
		{
			if (!int.TryParse(CapacityTB.Text, out int amount))
			{
				CapacityTB.Text = textBoxStartText;
			}
		}

		private void SortTypesCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			algorithmType = FigureOutAlgorithmType();

			if (int.TryParse(CapacityTB.Text, out int amount))
			{
				InitializeAlgorithm(amount);
			}
			else
			{
				InitializeAlgorithm();
				ShowCapacity();
			}

			FillSP(sortAlgorithm.GetCollection());
		}

		private AlgorithmType FigureOutAlgorithmType()
		{
			var comboBoxItem = SortTypesCB.SelectedItem as ComboBoxItem;
			switch (comboBoxItem.Tag)
			{
				case "Bubble":
					return AlgorithmType.BubbleSort;

				case "Cocktail":
					return AlgorithmType.CocktailSort;

				case "Insertion":
					return AlgorithmType.InsertionSort;

				case "Shell":
					return AlgorithmType.ShellSort;

				case "Selection":
					return AlgorithmType.SelectionSort;

				case "Gnome":
					return AlgorithmType.GnomeSort;

				case "Tree":
					return AlgorithmType.BinaryTreeSort;

				case "Radix":
					return AlgorithmType.RadixSort;

				case "Merge":
					return AlgorithmType.MergeSort;

				case "Quick":
					return AlgorithmType.QuickSort;

				default:
					throw new ArgumentException($"Tag was {comboBoxItem.Tag}");
			}
		}

		private void SortSpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			threadSleepTime = (int)e.NewValue;
		}

		private void AddMyOwnSequenceButton_Click(object sender, RoutedEventArgs e)
		{
			var setCollectionWindow = new SetCollectionWindow(testRgrFlag);
			setCollectionWindow.ShowDialog();

			myOwncollection = new List<int>();
			myOwncollection.AddRange(setCollectionWindow.Collection);

			var width = CapacityTB.ActualWidth;
			CapacityTB.Text = myOwncollection.Count.ToString();
			CapacityTB.Width = width;
		}

		private void Interrupt()
		{
			if (isOngoing)
			{
				cts.Cancel();
				isOngoing = false;
			}
		}
	}
}