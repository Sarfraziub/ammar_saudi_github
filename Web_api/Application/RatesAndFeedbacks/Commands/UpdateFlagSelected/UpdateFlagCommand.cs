using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RatesAndFeedbacks.Commands.UpdateFlagSelected
{
    public class UpdateFlagCommand : IRequest<Unit>
    {
        public int ClientOrderId { get; set; }
        public bool FlgSelected { get; set; }
    }
}
