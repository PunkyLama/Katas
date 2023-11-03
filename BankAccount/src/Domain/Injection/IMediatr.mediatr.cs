﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Injection
{
    public interface IMediatr
    {
        Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request);
    }
}