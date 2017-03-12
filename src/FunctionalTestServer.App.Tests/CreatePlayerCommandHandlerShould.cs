using CSharpFunctionalExtensions;
using FunctionalTestServer.Application.App;
using FunctionalTestServer.Application.Data;
using FunctionalTestServer.Application.Domain;
using Moq;
using NUnit.Framework;

namespace FunctionalTestServer.App.Tests
{
    [TestFixture]
    public class CreatePlayerCommandHandlerShould
    {
        private readonly Mock<IPlayersRepository> mockPlayerRepository = new Mock<IPlayersRepository>();

        private readonly Mock<IPositionsRepository> mockPositionsRepository = new Mock<IPositionsRepository>();

        private readonly Mock<ITeamsRepository> mockTeamsRepository = new Mock<ITeamsRepository>();

        private readonly CreatePlayerCommand command = new CreatePlayerCommand { PositionId = 1, TeamId = "HOU" };

    [SetUp]
        public void Setup()
        {
            var mockUser = new Mock<ITeam>();
            mockUser.Setup(x => x.TeamId).Returns(command.TeamId);

            var mockStatus = new Mock<IPosition>();
            mockStatus.Setup(x => x.PositionId).Returns(command.PositionId);

            var user = Result.Ok(Maybe<ITeam>.From(mockUser.Object));
            var status = Result.Ok(Maybe<IPosition>.From(mockStatus.Object));

            this.mockTeamsRepository.Setup(x => x.GetTeamById(command.TeamId)).Returns(user);
            this.mockPositionsRepository.Setup(x => x.GetPositionById(command.PositionId)).Returns(status);
        }

        [Test]
        public void CreateAPlayer()
        {
            this.mockPlayerRepository.Setup(x => x.CreatePlayer(It.IsAny<Player>())).Returns(Result.Ok("Record saved."));

            var handler = new CreatePlayerCommandHandler(this.mockPlayerRepository.Object, this.mockTeamsRepository.Object, this.mockPositionsRepository.Object);

            var barcode = handler.CreatePlayer(command);

            Assert.IsTrue(barcode.IsSuccess);
        }

        [Test]
        public void ReturnAnErrorResultWhenCallingCreatePlayer()
        {
            this.mockPlayerRepository.Setup(x => x.CreatePlayer(It.IsAny<Player>())).Returns(Result.Fail<string>("Error"));

            var handler = new CreatePlayerCommandHandler(this.mockPlayerRepository.Object, this.mockTeamsRepository.Object, this.mockPositionsRepository.Object);

            var barcode = handler.CreatePlayer(command);

            Assert.IsTrue(barcode.IsFailure);
        }
    }
}