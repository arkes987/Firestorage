using Firebase.Database;
using System.Threading.Tasks;

namespace Firestorage.Database
{
    public class FirebaseConnector
    {

        public FirebaseClient Instance { get; set; }

        public FirebaseConnector()
        {
            string authKey = "NkoVSV3H7nqHGX0p6l0j73UnlG0SPlXdTgXyknEu";
            Instance = new FirebaseClient("https://firestorage-bb9fd.firebaseio.com",
              new FirebaseOptions
              {
                  AuthTokenAsyncFactory = () => Task.FromResult(authKey)
              });
        }
    }
}
