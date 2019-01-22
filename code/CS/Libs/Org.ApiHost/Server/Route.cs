using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.ApiHost
{
    public class Route
    {
        public string Name { get; private set; }
        public Type Controller { get; private set; }

        public Route(string name, Type controller)
        {
            this.Name = name;
            this.Controller = controller;
        }
    }
}
