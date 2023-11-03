﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tests
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class OrderAttribute : Attribute
    {
        public int Priority { get; private set; }

        public OrderAttribute(int priority) => Priority = priority;
    }
}
