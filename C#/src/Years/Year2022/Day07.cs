using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;
using static Years.Year2022.Day07;

namespace Years.Year2022
{
    public class Day07 : BaseDay
    {
        public Day07() : base(2022, 7) 
        {
            _tree = ParseInput(Input);
        }

        private readonly TreeNode<Entry> _tree = new();

        public override void ProblemOne()
        {
            var sum = _tree.Flatten().Where(n => n.Value.EntryType == EntryType.dir && n.Value.Size <= 100000).Sum(i => i.Value.Size);
            Console.WriteLine(sum);
        }

        public override void ProblemTwo()
        {
            const int totalSize = 70000000;
            const int requiredSize = 30000000;

            var usedSpace = _tree.Flatten().Where(n => n.Value.EntryType == EntryType.file).Sum(i => i.Value.Size);
            var freeSpace = totalSize - usedSpace;

            var result = _tree.Flatten().Where(n => n.Value.EntryType == EntryType.dir && (freeSpace + n.Value.Size) > requiredSize).Min(i => i.Value.Size);
            Console.WriteLine(result);
        }

        public enum EntryType
        {
            ls,
            cd,
            dir,
            file,
        }

        public class Entry
        {
            public EntryType EntryType;
            public string Name;
            public int Size;
            public bool HasSize;

            public override string ToString()
            {
                return $"{EntryType} {Name} {Size}";
            }
        }

        private TreeNode<Entry> ParseInput(string input)
        {
            var tree = new TreeNode<Entry>(new Entry()
            {
                EntryType = EntryType.dir,
                Name = "/"
            });
            var currentNode = tree;

            //Hierarchy helper
            TreeNode<Entry> GetOrCreateChild(TreeNode<Entry> node, Entry entry)
            {
                var child = node.Children.FirstOrDefault(i => i.Value.Name == entry.Name);
                if (child == null)
                {
                    child = node.AddChild(entry);
                }
                return child;
            }

            var index = 0;

            var lines = input.SplitNewLine();//.Where(i => i.StartsWith("$ cd")).ToList();
            foreach (var line in lines)
            {

                //Parse ======================================================================
                var split = line.Split(' ');
                var entry = new Entry();

                if(line.StartsWith('$'))
                {

                    //Ls
                    if (split[1] == "ls")
                    {
                        entry.EntryType = EntryType.ls;
                    }
                    //cd
                    else
                    {
                        entry.EntryType = EntryType.cd;
                        entry.Name = split[2];
                    }
                }
                else
                {
                    //dir
                    if(line.StartsWith("dir"))
                    {
                        entry.EntryType = EntryType.dir;
                        entry.Name = split[1];
                    }
                    //File
                    else
                    {
                        entry.EntryType = EntryType.file;
                        entry.Name = split[1];
                        entry.HasSize = true;
                        entry.Size = int.Parse(split[0]);
                    }
                }

                //Hierarchy ======================================================================
                switch (entry.EntryType)
                {
                    case EntryType.cd:
                        if (entry.Name == "/")
                        {
                            currentNode = tree;//back to root
                        }
                        else if (entry.Name == "..")
                        {
                            currentNode = currentNode.Parent;
                        }
                        else
                        {
                            var child = GetOrCreateChild(currentNode, entry);
                            currentNode = child;
                        }
                        break;

                    case EntryType.ls:
                        break;

                    case EntryType.dir:
                        GetOrCreateChild(currentNode, entry);
                        break;

                    case EntryType.file:
                        var file = GetOrCreateChild(currentNode, entry);
                        break;
                }


                
                //if(entry.EntryType is EntryType.cd or EntryType.ls)
                //{
                //    Console.WriteLine(line);
                //}
                //
                //if (entry.EntryType is EntryType.file or EntryType.dir)
                //{
                //    Console.WriteLine("\t" + line);
                //}
                //
                //if (entry.EntryType == EntryType.cd)
                //{
                //    var pathNode = currentNode;
                //    var path = "";
                //    while (pathNode != null)
                //    {
                //        path = pathNode.Value.Name + @"/" + path;
                //        pathNode = pathNode.Parent;
                //    }
                //    Console.WriteLine("\t" + path);
                //}

                index++;
            }

            //Calculate file sizes ======================================================================
            var flat = tree.Flatten().ToList();
            while (flat.Any(i => !i.Value.HasSize))
            {
                foreach (var n in flat)
                {
                    if (!n.Value.HasSize)
                    {
                        if (n.Children.All(i => i.Value.HasSize))
                        {
                            n.Value.Size = n.Children.Sum(i => i.Value.Size);
                            n.Value.HasSize = true;
                        }
                    }
                }
            }

            return tree;
        }

        private const string Example = @"$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k";
    }
}