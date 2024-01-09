using Notes.Application.Commons.Mappings;
using Notes.Domain;
using AutoMapper;
using Notes.Application.Notes.Queries.GetNoteDetails;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    public class NoteLookupDto : IMapWith<Note>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Note, NoteDetailsVm>()
                .ForMember(noteVm => noteVm.Title,
                    opt => opt.MapFrom(note => note.Title))
                .ForMember(noteVm => noteVm.Id,
                    opt => opt.MapFrom(note => note.Id));
        }
    }
}
