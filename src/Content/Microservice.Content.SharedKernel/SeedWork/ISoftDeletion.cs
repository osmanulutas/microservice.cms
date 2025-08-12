using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Content.SharedKernel.SeedWork
{
    public interface ISoftDeletion
    {
        bool IsDeleted();
    }
}
