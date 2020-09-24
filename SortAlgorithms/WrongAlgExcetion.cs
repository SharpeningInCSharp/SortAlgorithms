using System;

namespace SortAlgorithms
{
	public class WrongAlgExcetion : Exception
	{
		public WrongAlgExcetion() : base("Index was out of range of algorithm number")
		{ }

		public WrongAlgExcetion(string message) : base(message)
		{ }
	}
}
