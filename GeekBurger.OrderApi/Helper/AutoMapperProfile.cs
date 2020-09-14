using AutoMapper;
using GeekBurger.Order.Contracts;


namespace GeekBurger.OrderApi.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<NewOrder, Model.Order>().AfterMap<MatchStoreFromRepository>();
            CreateMap<Payment, Model.Payment>().AfterMap<MatchOrderFromRepository>();

        }
    }
}
