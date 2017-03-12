using System.Collections.Generic;

namespace FunctionalTestServer.Application.DataTransferObjects
{
    public class PlayersDto
    {
        public IList<PlayerDto> Players { get; set; }

        public bool HasPlayers { get; set; }
    }
}