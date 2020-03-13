using SafewalkApplication.Models;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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

        public static Walk WithoutWalkerInfo(this Walk walk)
        {
            walk.WalkerEmail = null;
            walk.WalkerCurrLat = null;
            walk.WalkerCurrLng = null;

            return walk;
        }

        // Deep clone
        public static T DeepClone<T>(this T a)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, a);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
