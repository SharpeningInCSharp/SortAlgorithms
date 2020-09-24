namespace Algorithms.Help
{
	internal class Node
	{
		internal static int Count { get; private set; } = 0;
		public int Data { get; private set; }
		public Node Left { get; private set; }
		public Node Right { get; private set; }

		public int Index { get; set; }

		public Node(int data)
		{
			Data = data;
		}

		public void Add(int data)
		{
			if (data < Data)
			{
				if (Left == null)
				{
					Left = new Node(data, Count);
					Count++;
				}
				else
				{
					Left.Add(data);
				}
			}
			else
			{
				if (Right == null)
				{
					Right = new Node(data, Count);
					Count++;
				}
				else
				{
					Right.Add(data);
				}
			}
		}

		public Node(int data, int index) : this(data)
		{
			Index = index;
		}
	}
}