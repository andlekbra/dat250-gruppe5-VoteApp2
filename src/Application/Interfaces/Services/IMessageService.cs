using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoteApp.Application.Interfaces.Services
{
    public interface IMessageService
    {
        public Task Send(object o);
    }
}
