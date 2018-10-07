using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Utility
{
    /// <summary>
    /// Object where robot can drop off a crate.
    /// </summary>
    public interface DropOffTarget
    {
        /// <summary>
        /// Triggered by robot when droping the crate by DropOffTarget.
        /// </summary>
        /// <param name="crate"></param>
        void HandelDropOff(Crate crate);
    }

    /// <summary>
    /// Object where robot can pick up a crate.
    /// </summary>
    public interface PickUpTarget
    {
        /// <summary>
        /// Triggered by robot when picking up the crate by PickUpTarget.
        /// </summary>
        void HandelPickUp();
    }
}
