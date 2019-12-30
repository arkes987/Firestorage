using org.jivesoftware.util;

namespace Firestorage.Crypto
{
    public class ProtectDataEngine
    {
        private readonly Blowfish blowfish;
        public ProtectDataEngine(string key)
        {
            blowfish = new Blowfish(key);
        }
        public string Encrypt(string planText)
        {
            return blowfish.encryptString(planText); 
        }

        public string Decrypt(string encryptedText)
        {
            return blowfish.decryptString(encryptedText);
        }
    }
}
