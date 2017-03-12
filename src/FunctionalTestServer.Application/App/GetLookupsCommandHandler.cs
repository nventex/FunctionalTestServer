using System.Linq;
using CSharpFunctionalExtensions;
using FunctionalTestServer.Application.Data;
using FunctionalTestServer.Application.DataTransferObjects;

namespace FunctionalTestServer.Application.App
{
    public class GetLookupsCommandHandler
    {
        private readonly IPositionsRepository positionsRepository;
        private readonly ITeamsRepository teamsRepository;

        public GetLookupsCommandHandler(ITeamsRepository teamsRepository, IPositionsRepository positionsRepository)
        {
            this.teamsRepository = teamsRepository;
            this.positionsRepository = positionsRepository;
        }

        public Result<LookupsDto> GetLookups()
        {
            var dto = new LookupsDto();

            var users = teamsRepository.GetAllTeams();

            var statuses = positionsRepository.GetAllStatuses();

            var teamsDto = users.Map(u => dto.Teams = u.OrderBy(y => y.TeamName).ToDictionary(y => y.TeamId, y => y.TeamName));

            var positionsDto = statuses.Map(u => dto.Positions = u.OrderBy(y => y.PositionName).ToDictionary(y => y.PositionId, y => y.PositionName));

            return Result.Combine(teamsDto, positionsDto).Map(() => dto);
        }
    }
}