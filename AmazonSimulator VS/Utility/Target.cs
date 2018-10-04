using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Utility
{
    public interface DropOffTarget
    {
        void HandelDropOff(Crate crate);
    }

    public interface PickUpTarget
    {
        void HandelPickUp();
    }
}
