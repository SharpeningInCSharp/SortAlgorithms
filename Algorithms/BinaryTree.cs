using System;
using System.Collections.Generic;
using System.Threading;
using Algorithms.Help;

namespace Algorithms
{
	public class BinaryTree : AlgorithmBase
	{
		private Node Root { get; set; }

		private Dictionary<int, int> collectionItems;
		private Dictionary<int, int> takenItems;
		private int takenAmount = 0;

		public BinaryTree() : base()
		{
			InitializeTree();
		}

		public BinaryTree(int amount) : base(amount)
		{
			InitializeTree();
		}

		public BinaryTree(IEnumerable<int> input) : base(input)
		{
			InitializeTree();
		}

		private void InitializeTree()
		{
			foreach (var item in collection)
			{
				Add(item);
			}

			AnalyseTree();
		}

		private void AnalyseTree()
		{
			collectionItems = new Dictionary<int, int>();
			takenItems = new Dictionary<int, int>();
			foreach (var item in collection)
			{
				if (collectionItems.ContainsKey(item))
					collectionItems[item]++;
				else
				{
					collectionItems.Add(item, 1);
					takenItems.Add(item, 0);
				}
			}
		}

		public void Add(int data)
		{
			if (Root == null)
			{
				Root = new Node(data);
				return;
			}

			Root.Add(data);
		}

		private void Inorder() => Inorder(Root);

		protected override void MakeSort(CancellationToken INT)
		{
			Inorder();
		}

		private List<int> Inorder(Node node)
		{
			var list = new List<int>();

			if (node != null)
			{
				if (node.Left != null)
				{
					list.AddRange(Inorder(node.Left));
					OrderStep(list);
				}

				list.Add(node.Data);
				OrderStep(list);

				if (node.Right != null)
				{
					list.AddRange(Inorder(node.Right));
					OrderStep(list);
				}
			}

			return list;
		}

		private void OrderStep(List<int> currentStep)
		{
			///В силу того, что мы, по сути, сразу вытасктиваем нужные значения,
			///можно располагать получаемые последовательности по порядку на их нужные значения
			foreach (var item in currentStep)
			{
				if (ItRemained(item, out var was, out var become))
				{
					//проблема: одинаковые элементы могут находиться в разных индексах
					//taken.Add(item);
					takenAmount++;
					Swop(was.Value, become.Value);
				}
			}
		}

		private bool ItRemained(int item, out int? was, out int? become)
		{
			//если такой элемент уникален
			if (collectionItems[item] == 1)
			{
				if (++takenItems[item] <= collectionItems[item])    //если количество взятых меньше их общего количества
				{
					become = takenAmount;
					was = collection.IndexOf(item);
					return true;
				}
				else
				{
					become = null;
					was = null;
					return false;
				}
			}
			else
			{
				if (++takenItems[item] <= collectionItems[item])    //если количество взятых меньше их общего количества
				{
					if (takenItems[item] > 1)       //если один элемент из нескольких уже упорядочен
					{
						become = collection.IndexOf(item) + takenItems[item] - 1;
						was = FindIndOfItem(item, takenItems[item]);
						return true;
					}
					else
					{
						become = takenAmount;
						was = FindIndOfItem(item, takenItems[item]);
						return true;
					}

				}
				else
				{
					was = null;
					become = null;
					return false;
				}
			}
		}

		private int FindIndOfItem(int item, int number)
		{
			for (int i = 0; i < collection.Count; i++)
			{
				if (collection[i] == item)
				{
					number--;

					if (number == 0)
						return i;
				}
			}

			throw new Exception($"Can't find this {item} in collection.");
		}
	}
}
