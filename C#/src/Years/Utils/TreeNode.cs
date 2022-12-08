using System.Collections.Generic;
using System.Linq;

namespace Years.Utils
{
    public class TreeNode<T>
    {
        public TreeNode(){}
        public TreeNode(T t)
        { 
            Value = t; 
        }


        public T Value;
        public TreeNode<T> Parent;
        public List<TreeNode<T>> Children = new List<TreeNode<T>>();
        public object Object;//Use with hard casts, to store some addition info with each generic node.

        public TreeNode<T> AddChild(T t)
        {
            var childNode = new TreeNode<T>(t);
            childNode.Parent = this;
            Children.Add(childNode);
            return childNode;
        }


        public override string ToString()
        {
            return $"{Value.ToString()} Children: {Children.Count}";
        }

        public bool TryFindNode(T t, out TreeNode<T> node)
        {
            if(Value.Equals(t))
            {
                node = this;
                return true;
            }
            
            Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>(Children);
            while(stack.Any())
            {
                var currentNode = stack.Pop();
                if(currentNode.Value.Equals(t))
                {
                    node = currentNode;
                    return true;
                }

                foreach(var child in currentNode.Children)
                {
                    stack.Push(child);
                }
            }

            node = null;
            return false;
        }


        public IEnumerable<TreeNode<T>> Flatten()
        {
            Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();
            stack.Push(this);

            while (stack.Any())
            {
                var node = stack.Pop();
                yield return node;

                foreach (var child in node.Children)
                {
                    stack.Push(child);
                }
            }
        }
    }
}
