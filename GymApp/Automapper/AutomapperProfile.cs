using AutoMapper;
using GymApp.Models;
using GymApp.Models.Dto;
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
        }
    }
}