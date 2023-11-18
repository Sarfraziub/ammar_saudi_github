using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Message
{
    public class DeliveryDoneModel
    {
        public string Id { get; set; }
        public string OrderNumber { get; set; }
        public string PaymentDate { get; set; }
    }
}
