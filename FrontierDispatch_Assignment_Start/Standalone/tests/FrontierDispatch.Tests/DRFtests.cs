using NUnit.Framework;
using FrontierDispatch.Core;

namespace FrontierDispatch.Tests
{
    public sealed class DRFtests
    {
        [Test]
        public void unrealistic_item_goal_test()
        {
            var finder = new DijkstraRouteFinder(WorldDatasetFactory.CreateSmall());
            Assert.That(finder.FindRoute(0, 999999).Found, Is.False);
        }

        [Test]
        public void cheap_route_test()
        {
            var world = WorldDatasetFactory.CreateSmall();
            var finder = new DijkstraRouteFinder(world);
            foreach (var c in world.Connections)
            {
                RouteResult result = finder.FindRoute(c.FromLocationId, c.ToLocationId);
                Assert.That(result.TotalCost, Is.LessThanOrEqualTo(c.TravelCost));
            }
        }
    }
}