namespace FunctionalTestServer.Application.App
{
    public class CreatePlayerCommand
    {
        public string TeamId{ get; set; }

        public int PositionId { get; set; }

        public string Name { get; set; }
    }
}