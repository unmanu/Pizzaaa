using AutoMapper;
using Pizzaaa.BLL.Models;
using Pizzaaa.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.Persistance.Configuration;

public class PersistanceMapperProfile : Profile
{
	public PersistanceMapperProfile()
	{
		CreateMap<Models.Pizza, BLL.Models.Pizza>();
		CreateMap<BLL.Models.Pizza, Models.Pizza>()
			.ForMember(dest => dest.InsertUser, opt => opt.Ignore())
			.ForMember(dest => dest.InsertDate, opt => opt.Ignore())
			.ForMember(dest => dest.UpdateUser, opt => opt.Ignore())
			.ForMember(dest => dest.UpdateDate, opt => opt.Ignore())
			.ForMember(dest => dest.UserPizzaPreference, opt => opt.Ignore())
			.ForMember(dest => dest.StoreId, opt => opt.Ignore())
			.ForMember(dest => dest.Store, opt => opt.Ignore());

        CreateMap<Models.User, BLL.Models.User>();
        CreateMap<BLL.Models.User, Models.User>()
            .ForMember(dest => dest.InsertUser, opt => opt.Ignore())
            .ForMember(dest => dest.InsertDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdateUser, opt => opt.Ignore())
            .ForMember(dest => dest.UpdateDate, opt => opt.Ignore())
            .ForMember(dest => dest.LastAccess, opt => opt.Ignore())
            .ForMember(dest => dest.UserPizzaPreference, opt => opt.Ignore());

        CreateMap<Models.Store, BLL.Models.Store>();
        CreateMap<BLL.Models.Store, Models.Store>()
            .ForMember(dest => dest.InsertUser, opt => opt.Ignore())
            .ForMember(dest => dest.InsertDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdateUser, opt => opt.Ignore())
            .ForMember(dest => dest.UpdateDate, opt => opt.Ignore());

        CreateMap<Models.Ingredient, BLL.Models.Ingredient>();
        CreateMap<BLL.Models.Ingredient, Models.Ingredient>()
            .ForMember(dest => dest.InsertUser, opt => opt.Ignore())
            .ForMember(dest => dest.InsertDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdateUser, opt => opt.Ignore())
            .ForMember(dest => dest.UpdateDate, opt => opt.Ignore())
            .ForMember(dest => dest.Pizzas, opt => opt.Ignore());

        CreateMap<Models.UserPizzaPreference, BLL.Models.UserPizzaPreference>();
        CreateMap<BLL.Models.UserPizzaPreference, Models.UserPizzaPreference>()
            .ForMember(dest => dest.InsertUser, opt => opt.Ignore())
            .ForMember(dest => dest.InsertDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdateUser, opt => opt.Ignore())
            .ForMember(dest => dest.UpdateDate, opt => opt.Ignore());

        CreateMap<Models.Order, BLL.Models.Order>();
        CreateMap<BLL.Models.Order, Models.Order>()
            .ForMember(dest => dest.InsertUser, opt => opt.Ignore())
            .ForMember(dest => dest.InsertDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdateUser, opt => opt.Ignore())
            .ForMember(dest => dest.UpdateDate, opt => opt.Ignore());
    }
}