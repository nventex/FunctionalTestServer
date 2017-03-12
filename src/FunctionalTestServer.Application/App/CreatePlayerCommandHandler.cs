using CSharpFunctionalExtensions;
using FunctionalTestServer.Application.Data;
using FunctionalTestServer.Application.Domain;

namespace FunctionalTestServer.Application.App
{
    public class CreatePlayerCommandHandler
    {
        private readonly IPlayersRepository repository;

        private readonly ITeamsRepository teamsRepository;

        private readonly IPositionsRepository positionsRepository;

        public CreatePlayerCommandHandler(IPlayersRepository repository, ITeamsRepository teamsRepository, IPositionsRepository positionsRepository)
        {
            this.repository = repository;
            this.teamsRepository = teamsRepository;
            this.positionsRepository = positionsRepository;
        }

        public Result<string> CreatePlayer(CreatePlayerCommand command)
        {
	        IPosition position = null;
			ITeam team = null;

			return this.teamsRepository.GetTeamById(command.TeamId)
				.Ensure(y => y.HasValue, "No team found for given teamId")
				.OnSuccess(x =>
				{
					team = x.Value;
					return this.positionsRepository.GetPositionById(command.PositionId);
				})
				.Ensure(y => y.HasValue, "No position found for given statusId")
				.OnSuccess(x =>
				{
					position = x.Value;
				})
				.OnSuccess(b => this.repository.CreatePlayer(Player.CreateInstance(command.Name, team, position)));
        }
    }
}