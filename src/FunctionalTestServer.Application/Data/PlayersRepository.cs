using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using CSharpFunctionalExtensions;
using Dapper;
using FunctionalTestServer.Application.Common;
using FunctionalTestServer.Application.Domain;

namespace FunctionalTestServer.Application.Data
{
    public class PlayersRepository : RepositoryBase, IPlayersRepository
    {
        private const string GetAllPlayersQuery =
            @"SELECT p.Name, p.StartDate, t.TeamName, po.PositionName
                                        FROM dbo.Players p
                                        JOIN dbo.Teams t
                                        ON p.TeamId = t.TeamId
                                        JOIN dbo.Positions po
                                        ON p.PositionId = po.PositionId";

        public Maybe<Players> GetFilteredPlayers(PlayersFilter filter)
        {
            var connection = GetConnection();

            var builder = new SqlBuilder();

            var template = builder.AddTemplate(GetAllPlayersQuery + " /**where**/");

            builder.AppendWhereWhen("t.TeamId = @teamId", filter.TeamId != null)
                .AppendWhereWhen("po.PositionId = @positionId", filter.PositionId != null)
                .AppendWhereWhen("(p.Name LIKE '%' + @name + '%')", !string.IsNullOrEmpty(filter.Name?.Trim()))
                .AppendWhereWhen("(StartDate BETWEEN @startDateStart AND @startDateEnd)", filter.StartDateStart.HasValue && filter.StartDateEnd.HasValue)
                .AppendWhereWhen("(StartDate >= @startDateStart)", filter.StartDateStart.HasValue && !filter.StartDateEnd.HasValue)
                .AppendWhereWhen("(StartDate >= @startDateEnd)", !filter.StartDateStart.HasValue && filter.StartDateEnd.HasValue);

            var players = RunQuery(connection, template.RawSql, new
            {
                filter.PositionId,
                filter.Name,
                filter.TeamId,
                filter.StartDateStart,
                filter.StartDateEnd
            });

            return new Players(players.ToList());
        }

        public Result<string> CreatePlayer(Player player)
        {
            try
            {
                var connection = GetConnection();

                connection.Execute(
                    "INSERT INTO dbo.Players (Name, PositionId, TeamId, StartDate) VALUES(@Name, @PositionId, @TeamId, @StartDate)",
                    new
                    {
                        player.Name,
                        player.StartDate,
                        player.Team.TeamId,
                        player.Position.PositionId
                    });
            }
            catch (NotSupportedException supportEx)
            {
                return Result.Fail<string>(supportEx.Message);
            }
            catch (SqlException sqlEx)
            {
                return Result.Fail<string>(sqlEx.Message);
            }

            return Result.Ok("Record saved.");
        }

        private static IEnumerable<Player> RunQuery(SqlConnection connection, string query, object parameters)
        {
            var players = connection.Query<Player, Team, Position, Player>(query, 
                (player, team, position) => new Player(player.Name, team, position, player.StartDate),
                parameters,
                splitOn: "TeamName, PositionName");
            return players;
        }
    }
}