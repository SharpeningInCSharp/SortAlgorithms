using System.Collections.Generic;
using System.Threading;

namespace Algorithms
{
	/// <summary>
	/// Every time find Min and set it in required position
	/// </summary>
	public sealed class SelectionAlgorithm : AlgorithmBase
	{
		public SelectionAlgorithm() : base()
		{ }

		public SelectionAlgorithm(int amount) : base(amount)
		{ }

		public SelectionAlgorithm(IEnumerable<int> input) : base(input)
		{ }

		protected override void MakeSort(CancellationToken INT)
		{
			for (int i = 0; i < CollectionSize - 1; i++)
			{
				int minInd = i;
				for (int j = i + 1; j < CollectionSize && !INT.IsCancellationRequested; j++)
				{
					if (Compare(minInd, j))
					{
						minInd = j;
					}
				}

				Swop(i, minInd);
			}
		}
	}
}
