using AutoMapper;
using GeekBurger.Order.Contracts;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GeekBurger.Order.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<NewOrder, Model.Order>()
                .AfterMap<MatchOrderFromRepository>();

            CreateMap<Payment, Model.Payment>()
                .AfterMap<MatchPaymentFromRepository>();

            CreateMap<EntityEntry<Product>, Model.Product>()
                .ForMember(
                    dest => dest.ProductId,
                    opt => opt.AllowNull()
                ); ;

            //CreateMap<EntityEntry<Store>, Model.Store>()
            //    .ForMember(
            //        opt => opt.Ignore()
            //    );

            //CreateMap<EntityEntry<OrderChangedMessage>, Model.Order>()
            //    .ForMember(
            //        dest => dest.OrderId,
            //        opt => opt.Ignore()
            //    );

            //CreateMap<EntityEntry<OrderChangedEvent>, Model.Order>()
            //    .ForMember(
            //        dest => dest.Order,
            //        opt => opt.Ignore()
            //    );

        }
    }
}
