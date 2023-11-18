using Application.Features.Common.Mappings;
using Application.Features.ContentSettings.Commands.UpdateContentSettings;
using AutoMapper;
using Domain.DbModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ContentManagement.Command.UpdateContentManagment
{
    public class UpdateContentManagmentCommand : IRequest<Unit>, IMapFrom<ContentSetting>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateContentManagmentCommand, ContentSetting>();
        }
        public long Id { get; set; }
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
        public long ImageId { get; set; }

    }
}

