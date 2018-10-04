using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Utility
{
    public class Refinery : IUpdatable
    {
        private List<Crate> notRefined = new List<Crate>();
        private List<Crate> Refined = new List<Crate>();

        public Refinery()
        {
        }

        public void AddCrate(Crate crate)
        {
            notRefined.Add(crate);
        }

        public virtual bool Update(int tick)
        {
            return true;
        }
    }
}
