using System;
using CSharpFunctionalExtensions;
using FunctionalTestServer.Application.Domain;

namespace FunctionalTestServer.Application.Data
{
    public interface IPlayersRepository
    {
        Maybe<Players> GetFilteredPlayers(PlayersFilter filter);

        Result<string> CreatePlayer(Player player);
    }
}