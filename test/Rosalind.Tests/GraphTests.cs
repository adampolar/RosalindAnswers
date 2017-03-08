using Xunit;
using System.Linq;
using System.Collections.Generic;

namespace Rosalind
{
    public class GraphTests
    {
        Graph<int> graph = new Graph<int>(new Graph<int>.Node(2,
                new Graph<int>.Node(1,
                    new Graph<int>.Node(4,
                        new Graph<int>.Node(3,
                            new Graph<int>.Node(5)),
                        new Graph<int>.Node(6,
                            new Graph<int>.Node(5))))));
        [Fact]
        public void TestPopulatingGraphWorks()
        {
            Assert.Equal(
                new int[] { 1, 2, 3, 4, 5, 6 },
                graph.Nodes.Select(n => n.Value).OrderBy(n => n).ToArray());
        }

        [Fact]
        public void TestDistanceWorks()
        {
            Assert.Equal(1, graph.FindMinimumDistanceBetweenNodes(2, 1));
            Assert.Equal(3, graph.FindMinimumDistanceBetweenNodes(2, 6));
            Assert.Equal(2, graph.FindMinimumDistanceBetweenNodes(4, 5));
        }

        GraphUsingEdges<int> graphUsingEdges = new GraphUsingEdges<int>(
            new List<GraphUsingEdges<int>.Edge>()
        {
            new GraphUsingEdges<int>.Edge(4,6),
            new GraphUsingEdges<int>.Edge(6,5),
            new GraphUsingEdges<int>.Edge(4,3),
            new GraphUsingEdges<int>.Edge(3,5),
            new GraphUsingEdges<int>.Edge(2,1),
            new GraphUsingEdges<int>.Edge(1,4)

        });

        [Fact]
        public void TestDistanceWorks2()
        {
            Assert.Equal(1, graphUsingEdges.FindMinimumDistanceBetweenNodes(2, 1));
            Assert.Equal(3, graphUsingEdges.FindMinimumDistanceBetweenNodes(2, 6));
            Assert.Equal(2, graphUsingEdges.FindMinimumDistanceBetweenNodes(4, 5));

            Assert.Equal(null, graphUsingEdges.FindMinimumDistanceBetweenNodes(1, 1));
            Assert.Equal(null, graphUsingEdges.FindMinimumDistanceBetweenNodes(1, 2));
            Assert.Equal(2, graphUsingEdges.FindMinimumDistanceBetweenNodes(1, 3));
            Assert.Equal(1, graphUsingEdges.FindMinimumDistanceBetweenNodes(1, 4));
            Assert.Equal(3, graphUsingEdges.FindMinimumDistanceBetweenNodes(1, 5));
            Assert.Equal(2, graphUsingEdges.FindMinimumDistanceBetweenNodes(1, 6));
        }



        GraphUsingEdges<int> graphUsingEdgesWithCosts = new GraphUsingEdges<int>(
            new List<GraphUsingEdges<int>.Edge>()
        {
            new GraphUsingEdges<int>.Edge(3,4,4),
            new GraphUsingEdges<int>.Edge(1,2,4),
            new GraphUsingEdges<int>.Edge(1,3,2),
            new GraphUsingEdges<int>.Edge(2,3,3),
            new GraphUsingEdges<int>.Edge(6,3,2),
            new GraphUsingEdges<int>.Edge(3,5,5),
            new GraphUsingEdges<int>.Edge(5,4,1),
            new GraphUsingEdges<int>.Edge(3,2,1),
            new GraphUsingEdges<int>.Edge(2,4,2),
            new GraphUsingEdges<int>.Edge(2,5,3)

        });

        [Fact]
        public void TestCostWiseDistanceWorks()
        {
            int[] ans = new int[6];

            for (int i = 0; i < 6; i++)
            {
                int? a = graphUsingEdgesWithCosts.FindMinimumDistanceBetweenNodes(1, i + 1);
                ans[i] = a.HasValue ? a.Value : -1;

            }
            Assert.Equal(new int[] { -1, 3, 2, 5, 6, -1 }, ans);
        }


        GraphUsingEdges<int> graph2Ways = new GraphUsingEdges<int>(
            new List<GraphUsingEdges<int>.Edge>()
        {
            new GraphUsingEdges<int>.Edge(1,2, bothWays: true),
            new GraphUsingEdges<int>.Edge(1,5, bothWays: true),
            new GraphUsingEdges<int>.Edge(5,9, bothWays: true),
            new GraphUsingEdges<int>.Edge(5,10, bothWays: true),
            new GraphUsingEdges<int>.Edge(9,10, bothWays: true),
            new GraphUsingEdges<int>.Edge(3,4, bothWays: true),
            new GraphUsingEdges<int>.Edge(3,7, bothWays: true),
            new GraphUsingEdges<int>.Edge(3,8, bothWays: true),
            new GraphUsingEdges<int>.Edge(4,8, bothWays: true),
            new GraphUsingEdges<int>.Edge(7,11, bothWays: true),
            new GraphUsingEdges<int>.Edge(8,11, bothWays: true),
            new GraphUsingEdges<int>.Edge(8,12, bothWays: true),
            new GraphUsingEdges<int>.Edge(6,6, bothWays: true)
        });

        [Fact]
        public void GetConnectedComponentsAmount()
        {
            int unedgedComponents = 0;
            for(int i = 1; i < 13; i++)
            {
                if(!graph2Ways.Edges.Keys.Contains(i))
                {
                    unedgedComponents++;
                }
            }
            Assert.Equal(3, graph2Ways.GetConnectedComponentsCount() + unedgedComponents);

        }
    }
}