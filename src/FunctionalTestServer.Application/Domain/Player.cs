using System;

namespace FunctionalTestServer.Application.Domain
{
    public class Player
    {
        private Player()
        {
        }

        internal Player(string name, ITeam team, IPosition position, DateTime startDate)
        {
            if (position == null)
            {
                throw new NullReferenceException("position cannot be null.");
            }

            if (team == null)
            {
                throw new NullReferenceException("team cannot be null.");
            }

            this.StartDate = startDate;
            this.Name = name;
            this.Team = team;
            this.Position = position;
        }

        public static Player CreateInstance(string name, ITeam team, IPosition position) => new Player(name, team, position, DateTime.Now);

        public string Name { get; }

        public DateTime StartDate { get; internal set;  }

        public ITeam Team { get; }

        public IPosition Position { get; }
    }
}