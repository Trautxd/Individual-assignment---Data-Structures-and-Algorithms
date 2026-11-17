#nullable enable

using System;
using System.Collections.Generic;

namespace FrontierDispatch.Core
{
    /// <summary>
    /// Straightforward queries over the supplied WorldData.
    ///
    /// The starter project intentionally performs these queries directly over the stored
    /// collections. The implementation is correct, but students should analyze
    /// how its work scales as the world becomes larger and queries are repeated.
    ///
    /// Do not assume that every method must be replaced. Identify the operations
    /// that matter first, then justify any representation changes.
    /// </summary>
    public sealed class WorldQueries
    {
        private readonly Dictionary<int, Location> _locations = new Dictionary<int, Location>();
        private readonly Dictionary<int, List<Connection>> _outgoing = new Dictionary<int, List<Connection>>();

        public WorldQueries(WorldData world)
        {
            if (world == null) throw new ArgumentNullException(nameof(world));
            for (int i = 0; i < world.Locations.Count; ++i)
            {
                Location location = world.Locations[i];
                _locations[location.Id] = location; 
            }
            
            for (int i = 0; i < world.Connections.Count; ++i)
            {
                Connection connection = world.Connections[i];
                int fromId = connection.FromLocationId;

                if (!_outgoing.ContainsKey(fromId))
                {
                    _outgoing[fromId] = new List<Connection>();
                }
                _outgoing[fromId].Add(connection);
            }
            
           
        }

        public Location? FindLocationById(int id)
        {
            if (_locations.ContainsKey(id))
            {
                return _locations[id];
            }

            return null;
        }

        public IReadOnlyList<Connection> FindOutgoingConnections(int fromLocationId)
        {
            if (_outgoing.ContainsKey(fromLocationId))
            {
                return _outgoing[fromLocationId];
            }

            return new List<Connection>();
        }
    }
}
