using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using CSharpFunctionalExtensions;
using Dapper;
using FunctionalTestServer.Application.Domain;

namespace FunctionalTestServer.Application.Data
{
    public class PositionsRepository : RepositoryBase, IPositionsRepository
    {
        public Result<Maybe<IPosition>> GetPositionById(int positionId)
        {
            var connection = GetConnection();

            IPosition position;

            try
            {
                position = connection.QueryFirstOrDefault<Position>("SELECT PositionId, PositionName FROM dbo.Positions WHERE PositionId = @positionid", new { positionId });
            }
            catch (NotSupportedException supportedEx)
            {
                return Result.Fail<Maybe<IPosition>>(supportedEx.Message);
            }
            catch (SqlException sqlEx)
            {
                return Result.Fail<Maybe<IPosition>>(sqlEx.Message);
            }

            return Result.Ok(Maybe<IPosition>.From(position));
        }

        public Result<IReadOnlyList<IPosition>> GetAllStatuses()
        {
            var connection = GetConnection();

            IReadOnlyList<IPosition> statuses;

            try
            {
                statuses = connection.Query<Position>("SELECT PositionId, PositionName FROM dbo.Positions ORDER BY [PositionName]").ToList();
            }
            catch (NotSupportedException supportedEx)
            {
                return Result.Fail<IReadOnlyList<IPosition>>(supportedEx.Message);
            }
            catch (SqlException sqlEx)
            {
                return Result.Fail<IReadOnlyList<IPosition>>(sqlEx.Message);
            }

            return Result.Ok(statuses);
        }
    }
}