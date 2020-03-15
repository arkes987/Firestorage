using Firebase.Auth;
using Firebase.Database;
using System.Threading.Tasks;

namespace Firestorage.Database
{
    public class FirebaseConnector
    {
        private const string AppKey = "AIzaSyCebjHqGmKPu-9Agp9bU58lJkHJFavTnf0";
        public FirebaseClient Instance { get; set; }

        public async Task<string> Authenticate(string login, string password)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(AppKey));

            try
            {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(login, password);


                Instance = new FirebaseClient("https://firestorage-bb9fd.firebaseio.com",
                    new FirebaseOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(auth.FirebaseToken),
                    });

                return auth.User.LocalId;
            }
            catch (FirebaseAuthException authexception)
            {
                return null;
            }

        }

        public async Task<string> Register(string login, string password)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(AppKey));

            try
            {
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(login, password);


                Instance = new FirebaseClient("https://firestorage-bb9fd.firebaseio.com",
                    new FirebaseOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(auth.FirebaseToken),
                    });

                return auth.User.LocalId;
            }
            catch (FirebaseAuthException authexception)
            {
                return null;
            }
        }
    }
}
