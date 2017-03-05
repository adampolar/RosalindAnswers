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
        }

    }
}