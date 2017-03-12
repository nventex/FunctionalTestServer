using CSharpFunctionalExtensions;

namespace FunctionalTestServer.Application.Domain
{
    public class Position : ValueObject<Position>, IPosition
    {
        public string PositionName { get; }

        public int PositionId { get; }

        internal Position()
        {
            this.PositionName = string.Empty;
            this.PositionId = 0;
        }

        protected override bool EqualsCore(Position other) => other?.PositionId == PositionId;

        protected override int GetHashCodeCore() => PositionId.GetHashCode();
    }
}