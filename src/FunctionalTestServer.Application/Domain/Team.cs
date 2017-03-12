using CSharpFunctionalExtensions;

namespace FunctionalTestServer.Application.Domain
{
    public class Team : ValueObject<Team>, ITeam
    {
        internal Team()
        {
            TeamId = string.Empty;
            TeamName = string.Empty;
        }

        public string TeamId { get; }
        public string TeamName { get; }
        protected override bool EqualsCore(Team other) => other?.TeamId == TeamId;
        protected override int GetHashCodeCore() => TeamId.GetHashCode();
    }
}