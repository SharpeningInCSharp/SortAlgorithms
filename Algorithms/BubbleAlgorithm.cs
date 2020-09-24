using System.Collections.Generic;
using System.Threading;

namespace Algorithms
{
	/// <summary>
	/// It assumes pairwise comparing and swoping of items
	/// </summary>
	public sealed class BubbleAlgorithm : AlgorithmBase
	{
		public BubbleAlgorithm() : base()
		{ }

		public BubbleAlgorithm(int amount) : base(amount)
		{ }

		public BubbleAlgorithm(IEnumerable<int> input) : base(input)
		{ }

		protected override void MakeSort(CancellationToken INT)
		{
			for (int i = 0; i < CollectionSize; i++)
			{
				for (int j = 0; j < CollectionSize - i - 1 && !INT.IsCancellationRequested; j++)
				{
					
					if (Compare(j, j + 1))
					{
						
						Swop(j, j + 1);
					}
				}
			}
		}
	}
}
