using System;
using System.Security.Cryptography;
using System.Text;
using VendorMachine.Core.ViewModels;

namespace VendorMachine.Core.Helpers
{
    public static class GlobalHelpers
    {
        public static class Constants
        {
            public static readonly string c_userId = "UserId";
            public static readonly int[] amounts = { 5, 10, 20, 50, 100 };
        }

        public static class ResponseHelper
        {
            public static GenericResponse SuccessResponse(string message, object data)
            {
                var res = new GenericResponse()
                {
                    Message = message,
                    Success = true,
                    Reponse = data
                };
                return res;
            }
            public static GenericResponse FailResponse(string message, object data)
            {
                var res = new GenericResponse()
                {
                    Message = message,
                    Success = false,
                    Reponse = data
                };
                return res;
            }
        }

        public static class EncryptionHelpers
        {

            private const string cryptoKey = "cryptoKey";
            private static readonly byte[] IV = new byte[8] { 240, 3, 45, 29, 0, 76, 173, 59 };

            public static string Encrypt(string s)
            {
                if (s == null || s.Length == 0) return string.Empty;

                string result = string.Empty;

                try
                {
                    byte[] buffer = Encoding.ASCII.GetBytes(s);

                    using (TripleDES alg = TripleDES.Create())
                    {
                        alg.Key = CreateMD5(cryptoKey);
                        alg.IV = IV;
                        result = Convert.ToBase64String(alg.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length));
                    }
                }
                catch
                {
                    result = "0";
                }
                if (result.Contains("+"))
                {
                    result = result.Replace("+", "~");
                }
                return result;
            }

            public static string Decrypt(string s)
            {
                if (s.Contains("~"))
                {
                    s = s.Replace("~", "+");
                }

                if (s == null || s.Length == 0) return string.Empty;

                string result = string.Empty;

                try
                {
                    byte[] buffer = Convert.FromBase64String(s);

                    using (TripleDES alg = TripleDES.Create())
                    {
                        alg.Key = CreateMD5(cryptoKey);
                        alg.IV = IV;
                        result = Encoding.ASCII.GetString(alg.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length));
                    }
                }
                catch
                {
                    result = "0";
                }
                return result;
            }

            public static byte[] CreateMD5(string input)
            {
                // Use input string to calculate MD5 hash
                using (MD5 md5 = MD5.Create())
                {
                    byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                    byte[] hashBytes = md5.ComputeHash(inputBytes);
                    return hashBytes;
                }
            }
        }
    }
}
