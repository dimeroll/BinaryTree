using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTrees
{
    public class BinaryTree<T> : IEnumerable<T>
        where T : IComparable
    {
        public T Value { get; private set; }
        public bool isEmpty { get; private set; } = true;

        public BinaryTree<T> Left { get; private set; }
        private int leftSize;
        public BinaryTree<T> Right { get; private set; }
        private int rightSize;

        public T this[int i]
        {
            get
            {
                if (i < leftSize)
                    return Left[i];
                if(i > leftSize)
                    return Right[i - leftSize - 1];

                return Value;
            }
        }

        public void Add(T key)
        {
            if (isEmpty)
            {
                Value = key;
                isEmpty = false;
            }

            else AddCycle(this, key);
        }

        private void AddCycle(BinaryTree<T> currTree, T key)
        {
            bool movedToSubtree = false;
            while (true)
            {
                if (key.CompareTo(currTree.Value) < 0)
                {
                    if (movedToSubtree) currTree.leftSize++;
                    else leftSize++;

                    if (currTree.Left == null)
                    {
                        currTree.Left = new BinaryTree<T> { Value = key, isEmpty = false };
                        return;
                    }
                    currTree = currTree.Left;
                    movedToSubtree = true;
                }
                else if (key.CompareTo(currTree.Value) >= 0)
                {
                    if (movedToSubtree) currTree.rightSize++;
                    else rightSize++;

                    if (currTree.Right == null)
                    {
                        currTree.Right = new BinaryTree<T> { Value = key, isEmpty = false };
                        return;
                    }
                    currTree = currTree.Right;
                    movedToSubtree = true;
                }
            }
        }

        public bool Contains(T key)
        {
            if (isEmpty) return false;

            var tree = this;
            while (tree != null)
            {
                if (tree.Value.Equals(key)) return true;
                if (key.CompareTo(tree.Value) < 0)
                    tree = tree.Left;
                else if (key.CompareTo(tree.Value) > 0)
                    tree = tree.Right;
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (isEmpty)
                yield break;

            if (Left != null)
                foreach (var item in Left)
                    yield return item;

            yield return Value;

            if (Right != null)
                foreach (var item in Right)
                    yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
