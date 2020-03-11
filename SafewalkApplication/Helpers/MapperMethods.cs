using SafewalkApplication.Models;

namespace SafewalkApplication.Helpers
{
    public static class MapperMethods
    {
        public static Walk MapFields(this Walk oldWalk, Walk newWalk)
        {
            oldWalk.Status = newWalk.Status ?? oldWalk.Status;
            
            return oldWalk;
        }

        public static User MapFields(this User oldUser, User newUser)
        {
            oldUser.LastName = newUser.LastName ?? oldUser.LastName;
            oldUser.FirstName = newUser.FirstName ?? oldUser.FirstName;
            oldUser.Password = newUser.Password ?? oldUser.Password;
            oldUser.PhoneNumber = newUser.PhoneNumber ?? oldUser.PhoneNumber;
            oldUser.Photo = newUser.Photo ?? oldUser.Photo;
            oldUser.HomeAddress = newUser.HomeAddress ?? oldUser.HomeAddress;
            oldUser.Interest = newUser.Interest ?? oldUser.Interest;

            return oldUser;
        }

        public static Safewalker MapFields(this Safewalker oldWalker, Safewalker newWalker)
        {
            oldWalker.LastName = newWalker.LastName ?? newWalker.LastName;
            oldWalker.FirstName = newWalker.FirstName ?? newWalker.FirstName;
            oldWalker.Password = newWalker.Password ?? newWalker.Password;
            oldWalker.PhoneNumber = newWalker.PhoneNumber ?? newWalker.PhoneNumber;
            oldWalker.LastName = newWalker.LastName ?? newWalker.LastName;
            oldWalker.PhoneNumber = newWalker.PhoneNumber ?? newWalker.PhoneNumber;
            oldWalker.Photo = newWalker.Photo ?? newWalker.Photo;

            return oldWalker;
        }
    }
}
