using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Algorithms
{
	public sealed class MSDRadixAlgorithm : AlgorithmBase
	{
		public MSDRadixAlgorithm() : base()
		{ }

		public MSDRadixAlgorithm(int amount) : base(amount)
		{ }

		public MSDRadixAlgorithm(params int[] input) : base(input)
		{ }

		//SOLVE: interruption in all algorithms!
		protected override void MakeSort(CancellationToken INT)
		{
			var maxLength = (int)Math.Log10(collection.Max());
			collection = SortCollection(collection, maxLength);
		}

		private List<int> SortCollection(List<int> collection, int step)
		{
			var result = new List<int>();
			var groups = new List<List<int>>();
			InitializeGroups(groups);

			foreach (var item in collection)
			{
				var value = item % (int)Math.Pow(10, step + 1) / (int)Math.Pow(10, step);
				groups[value].Add(item);
			}

			//Take items from groups in intermediate order
			foreach (var group in groups)
			{
				if (group.Count > 1 && step > 0)
				{
					result.AddRange(SortCollection(group, step - 1));
					continue;
				}

				result.AddRange(group);
			}

			return result;
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
