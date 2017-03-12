using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FunctionalTestServer.Application.Domain
{
    public class Players : IEnumerable
    {
        private readonly IList<Player> players;

        public Players(IList<Player> players)
        {
            this.players = players;
        }

        public IEnumerator GetEnumerator() => this.players.GetEnumerator();

        public bool HasPlayers => this.players.Any();
    }
}