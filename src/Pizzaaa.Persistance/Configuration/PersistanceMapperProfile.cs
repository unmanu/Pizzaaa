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
			.ForMember(dest => dest.StoreId, opt => opt.Ignore())
			.ForMember(dest => dest.Store, opt => opt.Ignore())
			.ForMember(dest => dest.Ingredients, opt => opt.Ignore());

        CreateMap<Models.User, BLL.Models.User>();
        CreateMap<BLL.Models.User, Models.User>();
    }
}