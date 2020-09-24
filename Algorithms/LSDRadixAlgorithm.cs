using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Algorithms
{
	public sealed class LSDRadixAlgorithm : AlgorithmBase
	{
		public LSDRadixAlgorithm() : base()
		{ }

		public LSDRadixAlgorithm(int amount) : base(amount)
		{ }

		public LSDRadixAlgorithm(params int[] input) : base(input)
		{ }

		protected override void MakeSort(CancellationToken INT)
		{
			var groups = new List<List<int>>();
			InitializeGroups(groups);

			var maxLength = (int)Math.Log10(collection.Max()) + 1;

			for (int step = 0; step < maxLength && !INT.IsCancellationRequested; step++)
			{
				foreach (var item in collection)
				{
					var value = item % (int)Math.Pow(10, step + 1) / (int)Math.Pow(10, step);
					groups[value].Add(item);
				}

				collection.Clear();

				//Take items from groups in intermediate order
				foreach (var group in groups)
				{
					collection.AddRange(group);
				}

				ClearGroups(groups);
			}
		}

		private void ClearGroups(List<List<int>> groups)
		{
			foreach (var group in groups)
			{
				group.Clear();
			}
		}

		private void InitializeGroups(List<List<int>> groups)
		{
			for (int i = 0; i < 10; i++)
			{
				groups.Add(new List<int>());
			}
		}
	}
}
