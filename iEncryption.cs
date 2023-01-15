using System.Security.Cryptography;
using System.Text;

namespace TP_APP_CONSOLE
{
    internal interface iEncryption
    {

        public static void EncryptFile(string filePath, string password)
        {
            byte[] salt = new byte[16];
            new RNGCryptoServiceProvider().GetBytes(salt);

            using (FileStream inputStream = File.OpenRead(filePath))
            using (FileStream outputStream = File.OpenWrite(filePath + ".enc"))
            using (RijndaelManaged aes = new RijndaelManaged())
            using (Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt))
            {
                aes.Key = key.GetBytes(aes.KeySize / 8);
                aes.IV = key.GetBytes(aes.BlockSize / 8);
                outputStream.Write(salt, 0, salt.Length);
                using (CryptoStream cs = new CryptoStream(outputStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                using (StreamReader sr = new StreamReader(inputStream, Encoding.UTF8))
                using (StreamWriter sw = new StreamWriter(cs, Encoding.UTF8))
                {
                    sw.Write(sr.ReadToEnd());
                }
            }
            File.Delete(filePath);
        }

        public static void DecryptFile(string filePath, string password)
        {
            byte[] salt = new byte[16];
            using (FileStream inputStream = File.OpenRead(filePath))
            {
                inputStream.Read(salt, 0, salt.Length);
                using (FileStream outputStream = File.OpenWrite(filePath.Replace(".enc", "")))
                using (RijndaelManaged aes = new RijndaelManaged())
                using (Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt))
                {
                    aes.Key = key.GetBytes(aes.KeySize / 8);
                    aes.IV = key.GetBytes(aes.BlockSize / 8);
                    using (CryptoStream cs = new CryptoStream(inputStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    using (StreamReader sr = new StreamReader(cs, Encoding.UTF8))
                    using (StreamWriter sw = new StreamWriter(outputStream, Encoding.UTF8))
                    {
                        sw.Write(sr.ReadToEnd());
                    }
                }
            }
            File.Delete(filePath);
        }
    }
}
