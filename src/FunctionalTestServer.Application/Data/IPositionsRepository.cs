using System.Collections.Generic;
using CSharpFunctionalExtensions;
using FunctionalTestServer.Application.Domain;

namespace FunctionalTestServer.Application.Data
{
    public interface IPositionsRepository
    {
        Result<Maybe<IPosition>> GetPositionById(int statusId);

        Result<IReadOnlyList<IPosition>> GetAllStatuses();
    }
}