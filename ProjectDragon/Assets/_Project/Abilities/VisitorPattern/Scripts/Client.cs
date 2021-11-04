using System.Collections.Generic;

namespace Abilities.VisitorPattern.Scripts
{
    public class Client
    {
        public List<IVisitor> Visitors { get; }

        protected Client()
        {
            Visitors = new List<IVisitor>();
        }
    }

    public class GlobalClients : Client
    {
        public static GlobalClients Instance => _instance ?? new GlobalClients();
        private static GlobalClients _instance;
        
    }
}