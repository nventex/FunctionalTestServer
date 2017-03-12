using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using CSharpFunctionalExtensions;
using Dapper;
using FunctionalTestServer.Application.Domain;

namespace FunctionalTestServer.Application.Data
{
    public class TeamsRepository : RepositoryBase, ITeamsRepository
    {
        public Result<Maybe<ITeam>> GetTeamById(string teamId)
        {
            var connection = GetConnection();

            ITeam team;

            try
            {
                team = connection.QueryFirstOrDefault<Team>("SELECT TeamId, TeamName FROM dbo.Teams WHERE TeamId = @teamid", new { teamId });
            }
            catch (NotSupportedException supportedEx)
            {
                return Result.Fail<Maybe<ITeam>>(supportedEx.Message);
            }
            catch (SqlException sqlEx)
            {
                return Result.Fail<Maybe<ITeam>>(sqlEx.Message);
            }

            return Result.Ok(Maybe<ITeam>.From(team));
        }

        public Result<IReadOnlyList<ITeam>> GetAllTeams()
        {
            var connection = GetConnection();

            IReadOnlyList<ITeam> users;

            try
            {
                users = connection.Query<Team>("SELECT TeamId, TeamName FROM dbo.Teams ORDER BY TeamName").ToList();
            }
            catch (NotSupportedException supportedEx)
            {
                return Result.Fail<IReadOnlyList<ITeam>>(supportedEx.Message);
            }
            catch (SqlException sqlEx)
            {
                return Result.Fail<IReadOnlyList<ITeam>>(sqlEx.Message);
            }

            return Result.Ok(users);
        }
    }
}