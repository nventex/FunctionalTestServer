using AutoMapper;
using FunctionalTestServer.Application.App;
using FunctionalTestServer.Application.Data;
using FunctionalTestServer.Application.DataTransferObjects;
using FunctionalTestServer.Application.Domain;
using Microsoft.Practices.Unity;

namespace FunctionalTestServer.Api
{
    public class Bootstrapper
    {
        public static void Start()
        {
            var container = new UnityContainer();

            UnityConfig.RegisterComponents(container);

            container.RegisterTypes(AllClasses.FromLoadedAssemblies(), WithMappings.FromMatchingInterface);

            ConfigureMaps(container);
        }

        private static void ConfigureMaps(IUnityContainer container)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Player, PlayerDto>()
                    .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src => src.Position.PositionName))
                    .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team.TeamName));

                cfg.CreateMap<Players, PlayersDto>()
                    .ForMember(dest => dest.Players, opt => opt.MapFrom(src => src));

                cfg.CreateMap<GetPlayersCommand, PlayersFilter>();
            });

            var mapper = configuration.CreateMapper();

            container.RegisterInstance(mapper, new ContainerControlledLifetimeManager());
        }
    }
}