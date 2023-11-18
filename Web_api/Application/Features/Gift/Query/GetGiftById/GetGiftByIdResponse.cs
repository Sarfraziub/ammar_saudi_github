using Application.Features.Common.Mappings;
using AutoMapper;

namespace Application.Features.Gift.Query.GetGiftByClientOrderId
{
    public class GetGiftByIdResponse : IMapFrom<Domain.DbModel.Gift>
    {
        public int Id { get; set; }
        public string ReceiverName { get; set; }
        public string SenderName { get; set; }
        public string PhoneNumber { get; set; }
        public int ClientOrderId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.DbModel.Gift, GetGiftByIdResponse>();
        }
    }
}
