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

            int size = maxId + 1;
            _exists = new bool[size];
            _outgoing = new List<Connection>[size];

            for (int i = 0; i < size; i++)
            {
                _outgoing[i] = new List<Connection>();
            }

            for (int i = 0; i < _world.Locations.Count; i++)
            {
                int locId = _world.Locations[i].Id;
                _exists[locId] = true;
            }

            for (int i = 0; i < _world.Connections.Count; i++)
            {
                var c = _world.Connections[i];
                _outgoing[c.FromLocationId].Add(c);
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

            while (queue.Count > 0)
            {
                int current = queue.Dequeue();
                if (done[current])
                {
                    continue;
                }

                done[current] = true;
                expanded++;
                if (current == goalLocationId)
                {
                    break;
                }

                foreach (var conn in _outgoing[current])
                {
                    int newCost = dist[current] + conn.TravelCost;

                    if (newCost < dist[conn.ToLocationId])
                    {
                        dist[conn.ToLocationId] = newCost;
                        previous[conn.ToLocationId] = current;
                        queue.Enqueue(conn.ToLocationId, newCost);
                    }
                }
            }

            if (dist[goalLocationId] == int.MaxValue)
            {
                return new RouteResult(false, new List<int>(), 0, expanded);
            }

            var path = new List<int>();
            int curr = goalLocationId;

            while (curr != -1)
            {
                path.Add(curr);
                curr = previous[curr];
            }

            path.Reverse();

            return new RouteResult(true, path, dist[goalLocationId], expanded);
        }




            private bool IsKnown(int id)
            {
                return id >= 0 && id < _exists.Length && _exists[id];

            }

        }
    }