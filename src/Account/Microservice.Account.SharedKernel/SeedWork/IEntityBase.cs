﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Account.SharedKernel.SeedWork
{
    public interface IEntityBase<TId>
    {
        TId Id { get; }
    }
    public interface IEntityBase
    {

    }
}
