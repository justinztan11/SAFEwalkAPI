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
            oldUser.SocketId = newUser.SocketId ?? oldUser.SocketId;

            return oldUser;
        }

        public static Safewalker MapFields(this Safewalker oldWalker, Safewalker newWalker)
        {
            oldWalker.LastName = newWalker.LastName ?? oldWalker.LastName;
            oldWalker.FirstName = newWalker.FirstName ?? oldWalker.FirstName;
            oldWalker.Password = newWalker.Password ?? oldWalker.Password;
            oldWalker.PhoneNumber = newWalker.PhoneNumber ?? oldWalker.PhoneNumber;
            oldWalker.Photo = newWalker.Photo ?? oldWalker.Photo;
            oldWalker.SocketId = newWalker.SocketId ?? oldWalker.SocketId;

            return oldWalker;
        }
    }
}
