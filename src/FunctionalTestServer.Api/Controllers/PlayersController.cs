using System;
using System.Web.Http;
using CSharpFunctionalExtensions;
using FunctionalTestServer.Application.App;

namespace FunctionalTestServer.Api.Controllers
{
    public class PlayersController : ApiController
    {
        private readonly GetPlayersCommandHandler getCommandHanlder;

        private readonly CreatePlayerCommandHandler createCommandHandler;

        public PlayersController(GetPlayersCommandHandler getCommandHanlder, CreatePlayerCommandHandler createCommandHandler)
        {
            this.getCommandHanlder = getCommandHanlder;
            this.createCommandHandler = createCommandHandler;
        }

        [Route("api/players/{Name?}/{PositionId?}/{TeamId?}/{StartDateStart?}/{StartDateEnd?}")]
        public IHttpActionResult Get([FromUri] GetPlayersCommand command)
        {
            return this.getCommandHanlder.GetPlayers(command)
                .OnBoth(result => result.IsSuccess ? this.Ok(result.Value) : (IHttpActionResult)this.NotFound());
        }

        [Route("api/player")]
        public IHttpActionResult Post([FromBody] CreatePlayerCommand command)
        {
            return this.createCommandHandler.CreatePlayer(command)
                .OnBoth(result => result.IsSuccess ? this.Ok(result.Value) : (IHttpActionResult)this.InternalServerError(new Exception(result.Error)));
        }
    }
}