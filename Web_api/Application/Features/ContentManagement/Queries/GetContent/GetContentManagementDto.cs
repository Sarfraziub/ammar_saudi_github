using Application.Features.Common.Mappings;
using AutoMapper;
using Domain.DbModel;

namespace Application.Features.ContentManagement.Queries.GetContentManagment
{
    public class GetContentManagementDto : IMapFrom<ContentSetting>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ContentSetting, GetContentManagementDto>();
        }
        public string Key { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ArabicContent { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string WhatsApp { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string Snapchat { get; set; }
        
    }
}
