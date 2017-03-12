using System;

namespace FunctionalTestServer.Application.Data
{
    public class PlayersFilter
    {
        public PlayersFilter()
        {
        }

        public int? PositionId { get; set; }

        public string TeamId { get; set; }

        public string Name { get; set; }

        public DateTime? StartDateStart { get; set; }

        public DateTime? StartDateEnd { get; set; }
    }
}