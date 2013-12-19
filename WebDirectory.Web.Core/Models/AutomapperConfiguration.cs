using AutoMapper;

namespace WebDirectory.Web.Core.Models
{
    public class AutomapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<WebDirectory.Core.Person, ViewModels.Person>();
            Mapper.CreateMap<ViewModels.Person, WebDirectory.Core.Person>();
        }
    }
}