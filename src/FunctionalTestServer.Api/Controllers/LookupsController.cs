using System;
using System.Web.Http;
using CSharpFunctionalExtensions;
using FunctionalTestServer.Application.App;

namespace FunctionalTestServer.Api.Controllers
{
    public class LookupsController : ApiController
    {
        private readonly GetLookupsCommandHandler commandHandler;

        public LookupsController(GetLookupsCommandHandler commandHandler)
        {
            this.commandHandler = commandHandler;
        }

        [Route("api/lookups")]
        public IHttpActionResult Get()
        {
            return commandHandler.GetLookups()
                .OnBoth(y => y.IsSuccess ? this.Ok(y.Value) : (IHttpActionResult)this.InternalServerError(new Exception(y.Error)));
        }
    }
}