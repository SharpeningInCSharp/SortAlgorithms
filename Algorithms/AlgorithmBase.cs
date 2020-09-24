using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Algorithms
{
	/// <summary>
	/// Represents the basic functions of SortAlgorithm
	/// </summary>
	public abstract class AlgorithmBase : ISortable
	{
		/// <summary>
		/// Delegate void with two int params
		/// </summary>
		/// <param name="left">left index</param>
		/// <param name="right">right index</param>
		public delegate void TwoItemHandler(int left, int right);

		/// <summary>
		/// Represents Swop(indA, indB) event
		/// </summary>
		public event TwoItemHandler Swoped;
		/// <summary>
		/// Represents Compare(indA, indB) event
		/// </summary>
		public event TwoItemHandler Compared;

		public event EventHandler SortFinished;

		public int CollectionSize => collection.Count;
		public int ComprasionsCount { get; private set; } = 0;
		public int SwopsCount { get; private set; } = 0;

		protected List<int> collection;
		private const int maxSize = 85;
		public Stopwatch SpentTime { get; private set; }

		private readonly Random rnd = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));

		protected AlgorithmBase()
		{
			InitialInitialize();
			int amount = rnd.Next(10, maxSize + 1);
			RandomFill(amount);
		}
		
		protected AlgorithmBase(int amount)
		{
			InitialInitialize();
			RandomFill(amount);
		}

		protected AlgorithmBase(IEnumerable<int> input)
		{
			InitialInitialize();
			collection.AddRange(input);
		}

		private void InitialInitialize()
		{
			SpentTime = new Stopwatch();
			collection = new List<int>();
		}

		/// <summary>
		/// Fill collection with <paramref name="amount"/> random items
		/// </summary>
		private void RandomFill(int amount)
		{
			for (int i = 0; i < amount; i++)
			{
				RndDouble();
			}
		}

		private void RndDouble()
		{
			var current = rnd.Next(11, 101);
			//var current = Math.Round(rnd.NextDouble() * rnd.Next(100), 0);
			collection.Add(current);
		}

		public void Sort(object obj)
		{
			var INT = (CancellationToken)obj;

			SpentTime.Start();
			MakeSort(INT);
			SpentTime.Stop();

			if (!INT.IsCancellationRequested)
				SortFinished?.Invoke(this, null);
		}

		/// <summary>
		/// Compare items[<paramref name="indA"/>] > item[<paramref name="indB"/>]
		/// </summary>
		protected bool Compare(int indA, int indB)
		{
			Compared?.Invoke(indA, indB);
			ComprasionsCount++;
			return collection[indA] > collection[indB];
		}

		/// <summary>
		/// Swop items[<paramref name="indA"/>] to items[<paramref name="intB"/>]
		/// </summary>
		protected void Swop(int indA, int indB)
		{
			SwopsCount++;
			var temp = collection[indA];
			collection[indA] = collection[indB];
			collection[indB] = temp;

			Swoped?.Invoke(indA, indB);
		}

		public List<int> GetCollection()
		{
			return new List<int>(collection);
		}

		protected abstract void MakeSort(CancellationToken INT);
	}
}