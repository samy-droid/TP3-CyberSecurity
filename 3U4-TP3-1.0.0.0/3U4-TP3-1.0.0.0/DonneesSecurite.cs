// https://crackstation.net/
// https://www.mscs.dal.ca/~selinger/md5collision/


using BCrypt.Net;
using System;
using System.Text;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;


namespace consoleApp
{
    class DonneesSecurite
    {
        //cle BlowFish (entre 4 et 56 bytes)
        static byte[] KEY = Encoding.UTF8.GetBytes("cle12345"); // 4–56 bytes

        //Chiffrement Texte en Base64
        public static string Encrypter(string input)
        {
            var cipher = new PaddedBufferedBlockCipher(new BlowfishEngine());
            cipher.Init(true, new KeyParameter(KEY));
            byte[] data = Encoding.UTF8.GetBytes(input);
            byte[] output = new byte[cipher.GetOutputSize(data.Length)];
            int len = cipher.ProcessBytes(data, 0, data.Length, output, 0);
            len += cipher.DoFinal(output, len);
            return Convert.ToBase64String(output, 0, len);
        }

        //Chiffrement Base64 en Texte 
        public static string Decrypter(string input)
        {
            var cipher = new PaddedBufferedBlockCipher(new BlowfishEngine());
            cipher.Init(false, new KeyParameter(KEY));
            byte[] data = Convert.FromBase64String(input);
            byte[] output = new byte[cipher.GetOutputSize(data.Length)];
            int len = cipher.ProcessBytes(data, 0, data.Length, output, 0);
            len += cipher.DoFinal(output, len);
            return Encoding.UTF8.GetString(output, 0, len);
        }
    

        // fonctions permettant le hachage des mots de passe
        public static string HacherLeMotDePasse(string motDePasse)
        {
            // Utilise la chaîne d'entrée pour générer un sel unique et un hachage sécurisé
            return BCrypt.Net.BCrypt.HashPassword(motDePasse);
        }

        public static bool VerifierLeMotDePasse(string motDePasse, string hache)

        {

            // Vérifie le mot de passe en utilisant le sel stocké dans le hash

            return BCrypt.Net.BCrypt.Verify(motDePasse, hache);

        }


    }
}


