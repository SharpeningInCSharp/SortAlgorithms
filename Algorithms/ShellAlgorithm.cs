using System.Collections.Generic;
using System.Threading;

namespace Algorithms
{
	/// <summary>
	/// Based on InsertionSort but with bynamic step
	/// </summary>
	public sealed class ShellAlgorithm : AlgorithmBase
	{
		public ShellAlgorithm() : base()
		{ }

		public ShellAlgorithm(int amount) : base(amount)
		{ }

		public ShellAlgorithm(IEnumerable<int> input) : base(input)
		{ }

		protected override void MakeSort(CancellationToken INT)
		{
			var step = CollectionSize / 4 - 1;

			while (step > 0)
			{
				for (int i = step; i < CollectionSize; i++)
				{
					int j = i;
					while (j >= step && Compare(j - step, j) 
									&& !INT.IsCancellationRequested)
					{
						Swop(j - step, j);
						j -= step;
					}
				}

				step /= 2;
			}

		}
	}
}
