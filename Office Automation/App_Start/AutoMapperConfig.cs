using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Office_Automation.ModelLayer;
using Office_Automation.Views.ViewModels;

namespace WebApplication1.App_Start
{
    public class AutoMapperConfig
    {
        public static IMapper mapper;
        public static void Configuration()
        {
            MapperConfiguration configuration = new MapperConfiguration(t =>
            {
                t.CreateMap<Role, RoleViewModel>();
                t.CreateMap<RoleViewModel, Role>();

                t.CreateMap<User, UserViewModel>();
                t.CreateMap<UserViewModel, User>();

                t.CreateMap<Department, DepartmentViewModel>();
                t.CreateMap<DepartmentViewModel, Department>();

                t.CreateMap<Letter, LetterViewModel>();
                t.CreateMap<LetterViewModel, Letter>();

                t.CreateMap<Message, MessageViewModel>();
                t.CreateMap<MessageViewModel, Message>();
            });

            mapper = configuration.CreateMapper();
        }

    }
}