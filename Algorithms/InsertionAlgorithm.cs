using System.Collections.Generic;
using System.Threading;

namespace Algorithms
{
	/// <summary>
	/// Every time search place for a[i] in ordered part of array
	/// </summary>
	public sealed class InsertionAlgorithm : AlgorithmBase
	{
		public InsertionAlgorithm() : base()
		{ }

		public InsertionAlgorithm(int amount) : base(amount)
		{ }

		public InsertionAlgorithm(IEnumerable<int> input) : base(input)
		{ }

		protected override void MakeSort(CancellationToken INT)
		{
			for (int i = 1; i < collection.Count; i++)
			{
				int j = i;

				while (j > 0 && Compare(j - 1, j)
							 && !INT.IsCancellationRequested)
				{
					Swop(j, j - 1);
					j--;
				}

			}
		}
	}
}