using System;
using System.Collections.Generic;
using System.Text;

namespace Years.Utils
{
    public class Graph<T>
    {
        public Graph() { }
        public Graph(T t)
        {
            Object = t;
        }

        public T Object;
        public readonly List<(int Distance, Graph<T> Node)> Nodes = new List<(int Distance, Graph<T> Node)>();

        public override string ToString()
        {
            var sb = new StringBuilder($"self: {Object}:\n");
            foreach (var node in Nodes)
            {
                sb.Append($"node: {node.Node.Object} - distance: {node.Distance}\n");
            }
            return sb.ToString();
        }
    }
}
