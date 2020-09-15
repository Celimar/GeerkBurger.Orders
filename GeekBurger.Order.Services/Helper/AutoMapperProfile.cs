using AutoMapper;
using GeekBurger.Order.Contracts;
using GeekBurger.Order.Services.Model;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GeekBurger.Order.Services.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<NewOrder, Model.Order>().AfterMap<MatchStoreFromRepository>();
            CreateMap<Contracts.Payment, Model.Payment>().AfterMap<MatchOrderFromRepository>();

            CreateMap<EntityEntry<Model.Order>, OrderChangedMessage>()
                .ForMember(
                    dest => dest.OrderId,
                    opt => opt.MapFrom(src => src.Entity)
                );
            CreateMap<EntityEntry<Model.Order>, OrderChangedEvent>()
                .ForMember(
                    dest => dest.Order,
                    opt => opt.MapFrom(src => src.Entity)
                );


        }
    }
}
