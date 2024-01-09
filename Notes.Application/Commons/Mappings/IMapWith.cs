using AutoMapper;

namespace Notes.Application.Commons.Mappings
{
    public interface IMapWith <T>
    {
        void Mapping(Profile profile) =>
            profile.CreateMap(typeof(T), GetType());
    }
}
