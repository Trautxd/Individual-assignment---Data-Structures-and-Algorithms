using NUnit.Framework;
using FrontierDispatch.Core;

namespace FrontierDispatch.Tests
{
    public sealed class RQtests
    {
        [Test]
        public void unrealistic_goal_test2()
        {
            var query = new ReachabilityQuery(WorldDatasetFactory.CreateSmall());
            Assert.That(query.FindReachable(999999, 100), Is.Empty);
        }

        [Test]
        public void costs_dijkstra_test2()
        {
            var world = WorldDatasetFactory.CreateSmall();
            var finder = new DijkstraRouteFinder(world);
            foreach (var pair in new ReachabilityQuery(world).FindReachable(0, int.MaxValue))
            {
                Assert.That(pair.Value, Is.EqualTo(finder.FindRoute(0, pair.Key).TotalCost));
            }
        }
    }
}