using System;
using System.Collections.Generic;

namespace FrontierDispatch.Core
{
    // Assignment Phase 5. Adapt shortest-path reasoning to a travel-cost budget.

    /// <summary>
    /// Finds locations that can be reached from a starting location without
    /// exceeding a maximum total travel cost.
    ///
    /// This is an adaptation of shortest-path reasoning: instead of searching
    /// for one goal, the query determines the cheapest known cost to every
    /// location that fits within the supplied budget.
    ///
    /// This class is intentionally incomplete in the assignment starter.
    ///
    /// What do we mean by "query"? In this context, a query is simply a one-time search for information.
    /// </summary>
    public sealed class ReachabilityQuery
    {
        private readonly WorldData _world;
        public ReachabilityQuery(WorldData world)
        {
            _world = world ?? throw new ArgumentNullException(nameof(world));
        }

        public IReadOnlyDictionary<int, int> FindReachable(int startLocationId, int maxTravelCost)
        {
            // 1. Find max ID to size our arrays
            int maxId = -1;
            foreach (var loc in _world.Locations)
            {
                if (loc.Id > maxId) maxId = loc.Id;
            }

            int size = maxId + 1;
            var exists = new bool[size];
            var outgoing = new List<Connection>[size];

            for (int i = 0; i < size; i++) outgoing[i] = new List<Connection>();
            foreach (var loc in _world.Locations) exists[loc.Id] = true;
            foreach (var c in _world.Connections) outgoing[c.FromLocationId].Add(c);

            if (startLocationId < 0 || startLocationId >= size || !exists[startLocationId])
            {
                return new Dictionary<int, int>();
            }

            int[] dist = new int[size];
            Array.Fill(dist, int.MaxValue);
            dist[startLocationId] = 0;

            var queue = new MinPriorityQueue<int>();
            queue.Enqueue(startLocationId, 0);

            while (queue.Count > 0)
            {
                int current = queue.Dequeue();

                foreach (var conn in outgoing[current])
                {
                    int newCost = dist[current] + conn.TravelCost;

                    // Only update if it's within budget and cheaper
                    if (newCost <= maxTravelCost && newCost < dist[conn.ToLocationId])
                    {
                        dist[conn.ToLocationId] = newCost;
                        queue.Enqueue(conn.ToLocationId, newCost);
                    }
                }
            }
            var result = new Dictionary<int, int>();
            for (int i = 0; i < size; i++)
            {
                if (exists[i] && dist[i] != int.MaxValue && dist[i] <= maxTravelCost)
                {
                    result[i] = dist[i];
                }
            }
            {
                return result;
            }
        }
    }
}
