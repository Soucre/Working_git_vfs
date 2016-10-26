using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SwipeJob.Utility
{
    /// <summary>
    ///   This class is responsible for providing cryptography functions on the client/server side.
    /// </summary>
    public static class UtilsCryptography
    {
        // ------------------------------------------------------------------------------------------------------------------------
        // Static constructor, methods & properties go here. All static interface of this class.

        public static string Decrypt(string encryptedText)
        {
            return Decrypt(encryptedText, "EDF846F2-BDD5-4239-9689-21DCABC7BFF0");
        }

        public static string Encrypt(string plainText)
        {
            return Encrypt(plainText, "EDF846F2-BDD5-4239-9689-21DCABC7BFF0");
        }

        public static string Decrypt(string encryptedText, string password)
        {
            if (encryptedText == null)
            {
                throw new ArgumentNullException();
            }

            // Hard-code the security key to make sure that user could not catch it by using reflection tool
            // This security key is used for decrypting the data that got encrypted with the same security key
            TripleDES des = CreateTripleDes(password);
            byte[] bytes = Convert.FromBase64String(encryptedText);
            ICryptoTransform ct = des.CreateDecryptor();
            byte[] output = ct.TransformFinalBlock(bytes, 0, bytes.Length);

            return Encoding.UTF8.GetString(output);
        }

        public static string Encrypt(string plainText, string password)
        {
            if (plainText == null)
            {
                throw new ArgumentNullException();
            }

            // Hard-code the security key to make sure that user could not catch it by using reflection tool
            // This security key is used for encrypting the data that will be sent through network
            TripleDES des = CreateTripleDes(password);
            ICryptoTransform ct = des.CreateEncryptor();
            byte[] input = Encoding.UTF8.GetBytes(plainText);

            return Convert.ToBase64String(ct.TransformFinalBlock(input, 0, input.Length));
        }

        //public static string GenerateHash(string userName, string password)
        //{
        //    var passwordByte = Encoding.UTF8.GetBytes(userName.ToLower() + password);
        //    var sha1 = new SHA1CryptoServiceProvider();
        //    var passwordHash = sha1.ComputeHash(passwordByte);

        //    //Convert byte[] to hexa string format
        //    var hex = BitConverter.ToString(passwordHash);
        //    hex = hex.Replace("-", "");

        //    return hex;
        //}

        public static string GenerateFileHash(byte[] fileBinaries)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] fileHash = sha1.ComputeHash(fileBinaries);

            //Convert byte[] to hexa string format
            string hex = BitConverter.ToString(fileHash);
            hex = hex.Replace("-", "");

            return hex;
        }

        public static bool IsStrongPassword(string password)
        {
            const string pattern = @"(?=^.{6,}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s)[0-9a-zA-Z!@#$%^&*()]*$";
            return Regex.Match(password, pattern).Success;
        }

        public static string GenerateUniqueKey(int length)
        {
            const string a = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            char[] chars = a.ToCharArray();
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            byte[] data = new byte[length];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(length);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }

            return result.ToString();
        }

        public static string GenerateBCryptHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, 12);
        }

        public static bool VerifyBCryptPassword(string password, string hashedPassword)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            }
            catch
            {
                return false;
            }
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        // ------------------------------------------------------------------------------------------------------------------------
        // Public constructor, methods and properties go here. All public interfaces of this class.

        // ------------------------------------------------------------------------------------------------------------------------
        // Helper methods, event handlers and private constructor go here. All non-public interfaces of this class.

        private static TripleDES CreateTripleDes(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException();
            }

            PasswordDeriveBytes pwdGenerator = new PasswordDeriveBytes(key, null);
            byte[] keyForAlgorithm = pwdGenerator.GetBytes(16);
            byte[] initVec = pwdGenerator.GetBytes(8);
            TripleDES des = new TripleDESCryptoServiceProvider();
            des.Key = keyForAlgorithm;
            des.IV = initVec;

            return des;
        }

        // ------------------------------------------------------------------------------------------------------------------------
        // Member variables, including instance and static members. All member vars must have an underscore prefix in front of them.
    }
}