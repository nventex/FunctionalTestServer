using System.Collections.Generic;
using CSharpFunctionalExtensions;
using FunctionalTestServer.Application.Domain;

namespace FunctionalTestServer.Application.Data
{
    public interface ITeamsRepository
    {
        Result<Maybe<ITeam>> GetTeamById(string teamId);

        Result<IReadOnlyList<ITeam>> GetAllTeams();
    }
}