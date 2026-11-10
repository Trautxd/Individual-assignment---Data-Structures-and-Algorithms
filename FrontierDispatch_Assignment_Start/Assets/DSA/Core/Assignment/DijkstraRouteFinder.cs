using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace FrontierDispatch.Core
{
    // Assignment Phase 4.

    /// <summary>
    /// Finds minimum-total-cost routes through a Frontier Dispatch world
    /// using Dijkstra's shortest-path algorithm.
    ///
    /// This class is intentionally incomplete in the assignment starter.
    /// It should eventually use the student's prepared world representation
    /// and MinPriorityQueue implementation.
    /// </summary>
    public sealed class DijkstraRouteFinder
    {
        private readonly WorldData _world;
        private readonly List<Connection>[] _outgoing;
        private readonly bool[] _exists;
        /// <summary>
        /// Constructor. Creates a route finder operating on the supplied world.
        /// </summary>
        public DijkstraRouteFinder(WorldData world)
        {
            _world = world ?? throw new ArgumentNullException(nameof(world));

            int maxId = -1;
            for (int i = 0; i < _world.Locations.Count; i++)
            {
                if (_world.Locations[i].Id > maxId)
                {
                    maxId = _world.Locations[i].Id;
                }

            }
        }

        /// <summary>
        /// Finds a minimum-total-travel-cost route from the start location
        /// to the goal location.
        ///
        /// A successful RouteResult should contain the route as location IDs,
        /// including both the start and goal locations.
        /// </summary>
        public RouteResult FindRoute(int startLocationId, int goalLocationId)
        {
            // TODO: implement Dijkstra using your prepared world representation
            // and MinPriorityQueue<T>.

            new RouteResult(false, new List<int>(), 0, 0);

            if (!IsKnown(startLocationId) || !IsKnown(goalLocationId))
            {
                return new RouteResult(false, new List<int>(), 0, 0);
            }

            int size = _outgoing.Length;
            int[] dist = new int[size];
            int[] previous = new int[size];
            bool[] done = new bool[size];

            Array.Fill(dist, int.MaxValue);
            Array.Fill(previous, -1);
            dist[startLocationId] = 0;

            var queue = new MinPriorityQueue<int>();
            queue.Enqueue(startLocationId, 0);
            int expanded = 0;


            throw new NotImplementedException();

        }

        
        private bool IsKnown(int id)
        {
            return id >= 0 && id < _exists.Length && _exists[id];

        }


    }
}
