using SafewalkApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafewalkApplication.Helpers
{
    public static class ExtensionMethods
    {
        public static User WithoutPrivateInfo(this User user)
        {
            user.Id = null;
            user.Password = null;
            user.Token = null;
            return user;
        }

        public static Safewalker WithoutPrivateInfo(this Safewalker walker)
        {
            walker.Id = null;
            walker.Password = null;
            walker.Token = null;
            return walker;
        }
    }
}
