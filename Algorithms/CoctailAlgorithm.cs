using System.Collections.Generic;
using System.Threading;

namespace Algorithms
{
	/// <summary>
	/// While left less than right
	/// Sort from left to right, then from right to left
	/// Decrease right, increase left
	/// </summary>
	public sealed class CoctailAlgorithm : AlgorithmBase
	{
		public CoctailAlgorithm() : base()
		{ }

		public CoctailAlgorithm(int amount) : base(amount)
		{ }

		public CoctailAlgorithm(IEnumerable<int> input) : base(input)
		{ }

		protected override void MakeSort(CancellationToken INT)
		{
			int left = 0;
			int right = collection.Count - 1;

			while (left < right)
			{
				for (int i = left; i < right && !INT.IsCancellationRequested; i++)
				{
					if (Compare(i, i + 1))
					{
						Swop(i, i + 1);
					}
				}

				right--;

				for (int i = right; i > left && !INT.IsCancellationRequested; i--)
				{
					if (Compare(i - 1, i))
					{
						Swop(i, i - 1);
					}
				}

				left++;
			}
		}
	}
}