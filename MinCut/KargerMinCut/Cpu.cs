using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KargerMinCut
{
    class Cpu
    {
        Random _rnd = new Random();

        public void GetMinCut (IList<IList<int>> graph, IList<int> results, int iterationCount)
        {
            Parallel.For(0, iterationCount, i =>
            {
                results.Add(Contract(graph));
            });
;        }

        private int Contract(IList<IList<int>> graph)
        {
            var height = graph.Count;
            var redirections = new List<int>(height);
            var validDestinations = new List<bool>(height);

            for (int i = 0; i < height; i++)
            {
                redirections.Add(i);
                validDestinations.Add(false);
            }

            for (int loop = 0; loop < height - 2; loop++)
            {
                int source = 0, destination = 0;

                var sourceIndex = this._rnd.Next(height - loop);
                for (int i = 0; i < height; i++)
                {
                    if (sourceIndex == 0 && redirections[i] == i)
                    {
                        source = i;
                        break;
                    }
                    if(redirections[i] == i) { --sourceIndex; }
                }

                int rank = 0;
                for (int i = 0; i < height; i++)
                {
                    if(redirections[i] == source)
                    {
                        for (int j = 0; j < graph[i].Count; j++)
                        {
                            var local = graph[i][j];
                            var mappedLocal = redirections[local];
                            if (mappedLocal != source
                                && !validDestinations[mappedLocal])
                            {
                                validDestinations[mappedLocal] = true;
                                ++rank;
                            }
                        }
                    }
                }

                var destIndex = this._rnd.Next(rank);
                for (int i = 0; i < height; i++)
                {
                    if (validDestinations[i])
                    {
                        if (destIndex == 0) { destination = i; }
                        --destIndex;
                        validDestinations[i] = false;
                    }
                }

                for (int i = 0; i < height; i++)
                {
                    if(redirections[i] == source) { redirections[i] = destination; }
                }
            }


            int result = 0;
            var nodeValue = redirections[0];
            for (int i = 0; i < height; i++)
            {
                if (redirections[i] == nodeValue)
                {
                    for (int j = 0; j < graph[i].Count; j++)
                    {
                        var local = graph[i][j];
                        if (redirections[local] != nodeValue) { ++result; }
                    }
                }
            }

            return result;
        }
    }
}
