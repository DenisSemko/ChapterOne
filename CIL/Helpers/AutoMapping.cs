using AutoMapper;
using CIL.DTOs;
using CIL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIL.Helpers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<UserDto, User>();
            CreateMap<BookAddDto, Book>();
            CreateMap<UserSubscriptionDto, User>();
            CreateMap<Guid, Genre>();
            CreateMap<Guid, BookImage>();
            CreateMap<Guid, BookFile>();
        }
    }
}
