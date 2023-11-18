
using Application.Interface.Context;

namespace Application.Services.Context
{
    public class RequestContext : IRequestContext
    {
        public string Currency { get; set; }
    }
}
