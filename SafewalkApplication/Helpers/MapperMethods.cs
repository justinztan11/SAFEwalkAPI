using SafewalkApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafewalkApplication.Helpers
{
    public static class MapperMethods
    {
        public static Walk MapFieldsWalker(this Walk oldWalk, Walk newWalk)
        {
            oldWalk.WalkerEmail = newWalk.WalkerEmail ?? oldWalk.WalkerEmail;
            oldWalk.Status = newWalk.Status ?? oldWalk.Status;
            return oldWalk;
        }

        public static Walk MapFieldsUser(this Walk oldWalk, Walk newWalk)
        {
            oldWalk.Status = newWalk.Status ?? oldWalk.Status;
            return oldWalk;
        }
    }
}
