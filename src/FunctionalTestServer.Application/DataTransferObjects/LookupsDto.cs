using System.Collections.Generic;

namespace FunctionalTestServer.Application.DataTransferObjects
{
    public class LookupsDto
    {
        public Dictionary<string, string> Teams { get; set; } 

        public Dictionary<int, string> Positions { get; set; }
    }
}