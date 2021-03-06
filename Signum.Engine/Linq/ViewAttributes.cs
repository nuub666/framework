﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Signum.Entities;

namespace Signum.Engine
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class SqlViewNameAttribute : Attribute
    {
        public string Schema { get; private set; }
        public string Name { get; private set; }

        public SqlViewNameAttribute(string schema, string name)
        {
            this.Schema = schema;
            this.Name = name;
        }
    }

    [AttributeUsage(AttributeTargets.Field, Inherited = false)]
    public sealed class SqlViewColumnAttribute : Attribute
    {
        public bool PrimaryKey { get; set; }

        public string Name { get; private set; }

    }

    public interface IView : IRootEntity { }
}
