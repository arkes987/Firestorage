using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.jivesoftware.util;

namespace Firestorage.TEST
{
    [TestClass]
    public class ProtectedDataTest
    {
        [TestMethod]
        public void TestEncryptAndDecrypt()
        {
            string pass = "06Ark9812!";
            string email = "arkes987@gmail.com";
            Blowfish algo = new Blowfish(email);
            string encryptedTxt = algo.encryptString(pass);
            string decryptedTxt = algo.decryptString(encryptedTxt);
        }
    }
}
