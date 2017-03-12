using System;

namespace FunctionalTestServer.Application.DataTransferObjects
{
    public class PlayerDto
    {
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public string TeamName { get; set; }

        public string PositionName { get; set; }
    }
}