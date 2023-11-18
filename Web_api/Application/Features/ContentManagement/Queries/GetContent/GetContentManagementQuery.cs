using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ContentManagement.Queries.GetContentManagment
{
    public class GetContentManagementQuery : IRequest<List<GetContentManagementDto>>
    {
    }
}
