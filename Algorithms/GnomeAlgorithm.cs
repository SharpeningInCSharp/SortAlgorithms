using System.Collections.Generic;
using System.Threading;

namespace Algorithms
{
	public sealed class GnomeAlgorithm : AlgorithmBase
	{
		public GnomeAlgorithm() : base()
		{ }

		public GnomeAlgorithm(int amount) : base(amount)
		{ }

		public GnomeAlgorithm(IEnumerable<int> input) : base(input)
		{ }

		protected override void MakeSort(CancellationToken INT)
		{
			for (int i = 0; i < CollectionSize - 1 && !INT.IsCancellationRequested; i++)
			{
				if (Compare(i, i + 1))
				{
					Swop(i, i + 1);

					if (i > 0)
						i -= 2;
				}
			}
		}
	}
}
