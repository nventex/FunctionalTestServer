using AutoMapper;
using CSharpFunctionalExtensions;
using FunctionalTestServer.Application.Data;
using FunctionalTestServer.Application.DataTransferObjects;

namespace FunctionalTestServer.Application.App
{
    public class GetPlayersCommandHandler
    {
        private readonly IPlayersRepository repository;

        private readonly IMapper mapper;

        public GetPlayersCommandHandler(IPlayersRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public Result<PlayersDto> GetPlayers(Maybe<GetPlayersCommand> command)
        {
            var filters = new PlayersFilter();

            var dto = command.ToResult("Command is optional")
                .OnSuccess(x => filters = this.mapper.Map<PlayersFilter>(x))
                .OnBoth(x => repository.GetFilteredPlayers(filters))
                .ToResult("Players cannot be null.")
                .OnSuccess(s => mapper.Map(s, new PlayersDto()))
                .Ensure(s => s.HasPlayers, "No players found.");

            return dto;
        }
    }
}