using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wrssolutions.Services.Interfaces
{
    public interface ISvcRabbitMQ
    {
        bool BasicPublish(string eventName, object model);
    }
}
