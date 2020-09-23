using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using UltimateNiconicoCommentViewer.src.common.stringList;

namespace UltimateNiconicoCommentViewer.src.viewModel
{
    public class SqlConnectionCookie
    {   

        /// <summary>
        /// クッキーファイルからニコニコにログインをしているか確認します。
        /// </summary>
        /// <param name="filePath"> クッキーファイル </param>
        /// <returns> True  -> ログイン中
        ///           False -> ログインしていません
        /// </returns>
        public bool ReadCookie(string filePath)
        {
            using (SQLiteConnection conn = new SQLiteConnection($@"Data Source={filePath};pooling=false"))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand cmd = new SQLiteCommand(SQLString.SELECT_USERSESSION_FROM_COOKIES, conn);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        var flag = (reader.HasRows);
                        while (reader.Read())
                        {
                            var result = (Byte[])reader["encrypted_value"];
                            if (result.Length == 0)
                            {
                                continue;
                            }
                            string localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                            string encKey = File.ReadAllText($@"{localPath}\Google\Chrome\User Data\Local State");
                            encKey = JObject.Parse(encKey)["os_crypt"]["encrypted_key"].ToString();
                            var decodedKey = ProtectedData.Unprotect(Convert.FromBase64String(encKey).Skip(5).ToArray(), null, System.Security.Cryptography.DataProtectionScope.LocalMachine);
                            var cookieValue = _decryptWithKey(result, decodedKey, 3);

                            UserSetting.Default.userCookiePath = filePath;
                            UserSetting.Default.cookieValue = cookieValue;
                            UserSetting.Default.Save();
                          
                        }
                        return flag;

                    }

                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
        }

        private string _decryptWithKey(byte[] message, byte[] key, int nonSecretPayloadLength)
        {
            const int KEY_BIT_SIZE = 256;
            const int MAC_BIT_SIZE = 128;
            const int NONCE_BIT_SIZE = 96;

            if (key == null || key.Length != KEY_BIT_SIZE / 8)
                throw new ArgumentException(String.Format("Key needs to be {0} bit!", KEY_BIT_SIZE), "key");
            if (message == null || message.Length == 0)
                throw new ArgumentException("Message required!", "message");

            using (var cipherStream = new MemoryStream(message))
            using (var cipherReader = new BinaryReader(cipherStream))
            {
                var nonSecretPayload = cipherReader.ReadBytes(nonSecretPayloadLength);
                var nonce = cipherReader.ReadBytes(NONCE_BIT_SIZE / 8);
                var cipher = new GcmBlockCipher(new AesEngine());
                var parameters = new AeadParameters(new KeyParameter(key), MAC_BIT_SIZE, nonce);
                cipher.Init(false, parameters);
                var cipherText = cipherReader.ReadBytes(message.Length);
                var plainText = new byte[cipher.GetOutputSize(cipherText.Length)];
                try
                {
                    var len = cipher.ProcessBytes(cipherText, 0, cipherText.Length, plainText, 0);
                    cipher.DoFinal(plainText, len);
                }
                catch (InvalidCipherTextException)
                {
                    return null;
                }
                return Encoding.Default.GetString(plainText);
            }
        }
    }
}
