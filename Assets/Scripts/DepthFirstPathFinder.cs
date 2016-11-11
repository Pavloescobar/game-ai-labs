using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class DepthFirstPathFinder :Pathfinder
    {
        public override List<int> findPath(int a, int b)
        {
            /*references:
             *  1- https://www.kirupa.com/developer/actionscript/depth_breadth_search2.htm-
             *  2- http://stackoverflow.com/questions/21508765/how-to-implement-depth-first-search-for-graph-with-non-recursive-aprroach
             */ 
            Stack<int> open = new Stack<int>();
            List<int> closed = new List<int>();
            open.Push(a);
            List<int> Path = new List<int>();
            while (open.Count > 0)
            {
                int current = open.Pop();
                if (!closed.Contains(current))//not in the closed list
                {
                    closed.Add(current);
                    Path.Add(current);
                    if (current != b)
                    {
                        List<int> children = navGraph.neighbours(current);
                        foreach (int ch in children)
                            open.Push(ch);
                    }
                }
            }
            return Path;
        }
    }
}
