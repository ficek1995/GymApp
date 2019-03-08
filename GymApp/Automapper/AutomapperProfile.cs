using AutoMapper;
using GymApp.Models;
using GymApp.Models.Dto;
using GymApp.Models.ViewsModels;
using GymApp.ViewsModels;

namespace GymApp.Automapper
{
	public class AutomapperProfile : Profile
	{
		public AutomapperProfile()
		{
			CreateMap<ApplicationUser, UserViewModel>();
            CreateMap<LessonDTO, Lesson>();
            CreateMap<Lesson, LessonDTO>();
            CreateMap<Message, MessageViewModel>();
            CreateMap<Message, MessageReadViewModel>();
            CreateMap<MessageViewModel, Message>();

            CreateMap<MessageViewModel, Message>().ForSourceMember(x => x.Id, opt => opt.Ignore());
        }
    }
}