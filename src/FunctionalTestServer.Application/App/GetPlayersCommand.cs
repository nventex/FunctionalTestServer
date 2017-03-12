using System;

namespace FunctionalTestServer.Application.App
{
    public class GetPlayersCommand
    {
        public string Name { get; set; }

        public int? PositionId { get; set; }

        public string TeamId { get; set; }

        public DateTime? StartDateStart { get; set; }

        public DateTime? StartDateEnd { get; set; }
    }
}