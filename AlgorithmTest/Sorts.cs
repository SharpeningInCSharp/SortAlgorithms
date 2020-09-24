using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms;
using System.Threading;

namespace AlgorithmTest
{
	[TestClass]
	public class Sorts
	{
		private readonly List<int> Items = new List<int>();
		private readonly List<int> Sorted = new List<int>();
		private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

		[TestInitialize]
		public void Initialize()
		{
			Items.Clear();
			var rnd = new Random();
			for(int i=0;i<2000;i++)
			{
				//Items.Add(rnd.Next(1,21));
				Items.Add(rnd.Next(0,100));
			}

			Sorted.Clear();
			Sorted.AddRange(Items.OrderBy(x => x));
		}

		[TestMethod]
		public void BaseSort()
		{
			Items.Sort();

			for(int i=0;i<Items.Count;i++)
			{
				Assert.AreEqual(Sorted[i], Items[i]);
			}
		}
		
		[TestMethod]
		public void BubbleTest()
		{
			Initialize();
			var bubble = new BubbleAlgorithm(Items.ToArray());

			bubble.Sort(cancellationTokenSource.Token);

			var collection = bubble.GetCollection();
			for (int i = 0; i < bubble.CollectionSize; i++)
			{
				Assert.AreEqual(Sorted[i], collection[i]);
			}
		}

		[TestMethod]
		public void CoctailTest()
		{
			Initialize();
			var coctail = new CoctailAlgorithm(Items.ToArray());

			coctail.Sort(cancellationTokenSource.Token);

			var collection = coctail.GetCollection();
			for (int i = 0; i < coctail.CollectionSize; i++)
			{
				Assert.AreEqual(Sorted[i], collection[i]);
			}
		}

		[TestMethod]
		public void GnomeTest()
		{
			Initialize();
			var gnome = new GnomeAlgorithm(Items.ToArray());

			gnome.Sort(cancellationTokenSource.Token);

			var collection = gnome.GetCollection();
			for (int i = 0; i < gnome.CollectionSize; i++)
			{
				Assert.AreEqual(Sorted[i], collection[i]);
			}
		}

		[TestMethod]
		public void InsertionTest()
		{
			Initialize();
			var insertion = new InsertionAlgorithm(Items.ToArray());

			insertion.Sort(cancellationTokenSource.Token);

			var collection = insertion.GetCollection();
			for (int i = 0; i < insertion.CollectionSize; i++)
			{
				Assert.AreEqual(Sorted[i], collection[i]);
			}
		}

		[TestMethod]
		public void SelectionTest()
		{
			Initialize();
			var selection = new SelectionAlgorithm(Items.ToArray());

			selection.Sort(cancellationTokenSource.Token);

			var collection = selection.GetCollection();
			for (int i = 0; i < selection.CollectionSize; i++)
			{
				Assert.AreEqual(Sorted[i], collection[i]);
			}
		}

		[TestMethod]
		public void ShellTest()
		{
			Initialize();
			var shell = new ShellAlgorithm(Items.ToArray());

			shell.Sort(cancellationTokenSource.Token);

			var collection = shell.GetCollection();
			for (int i = 0; i < shell.CollectionSize; i++)
			{
				Assert.AreEqual(Sorted[i], collection[i]);
			}
		}

		[TestMethod]
		public void LSDRadixTest()
		{
			Initialize();
			var lsdRadix = new LSDRadixAlgorithm(Items.ToArray());

			lsdRadix.Sort(cancellationTokenSource.Token);

			var collection = lsdRadix.GetCollection();
			for (int i = 0; i < lsdRadix.CollectionSize; i++)
			{
				Assert.AreEqual(Sorted[i], collection[i]);
			}
		}

		[TestMethod]
		public void MSDRadixTest()
		{
			Initialize();
			var msdRadix = new MSDRadixAlgorithm(Items.ToArray());

			msdRadix.Sort(cancellationTokenSource.Token);

			var collection = msdRadix.GetCollection();
			for (int i = 0; i < msdRadix.CollectionSize; i++)
			{
				Assert.AreEqual(Sorted[i], collection[i]);
			}
		}
	}
}
