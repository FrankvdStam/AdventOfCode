using System;
using System.Collections.Generic;
using System.Text;

namespace Years.Utils
{
    public class NodeGraph
    {
        public string Name;
        public List<(int distance, NodeGraph Node)> Nodes = new List<(int distance, NodeGraph Node)>();
    }
}
