using System.Collections.Generic;
using System.Linq;

namespace Years.Utils
{
    public class TreeNode<T>
    {
        public TreeNode(){}
        public TreeNode(T t)
        { 
            Node = t; 
        }


        public T Node;
        public TreeNode<T> Parent;
        public List<TreeNode<T>> Children = new List<TreeNode<T>>();
        public object Object;//Use with hard casts, to store some addition info with each generic node.

        public override string ToString()
        {
            return $"{Node.ToString()} Children: {Children.Count}";
        }

        public bool TryFindNode(T t, out TreeNode<T> node)
        {
            if(Node.Equals(t))
            {
                node = this;
                return true;
            }
            
            Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>(Children);
            while(stack.Any())
            {
                var currentNode = stack.Pop();
                if(currentNode.Node.Equals(t))
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
    }
}
