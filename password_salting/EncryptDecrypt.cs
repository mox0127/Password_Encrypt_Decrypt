namespace password_salting
{
    using EncryptPasswordHelper.Crypto;
    using EncryptPasswordHelper.Entities;
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the <see cref="EncryptDecrypt" />
    /// </summary>
    static class EncryptDecrypt
    {
        /// <summary>
        /// Defines the passData
        /// </summary>
        private static DataPassword passData;

        /// <summary>
        /// Defines the cipherKey
        /// </summary>
        private static readonly String cipherKey = "$2a$10$PScoX.osxOUQArH.DYbUIe7vwqPZrtuIqUKvGgVBa4jtXpZUb7RMy";

        /// <summary>
        /// Defines the passFilePath
        /// </summary>
        private static String passFilePath = String.Format(@"{0}\binseq.dat", Application.StartupPath);

        /// <summary>
        /// The Encrypter
        /// </summary>
        /// <param name="passText">The passText<see cref="String"/></param>
        public static void Encrypter(String passText)
        {
            CryptoService cryptoService = new CryptoService(cipherKey);
            passData = cryptoService.Encrypt(passText);
            WriteDatFile(passData);
        }

        /// <summary>
        /// The Decrypter
        /// </summary>
        /// <returns>The <see cref="String"/></returns>
        public static String Decrypter()
        {
            CryptoService cryptoService = new CryptoService(cipherKey);
            DataPasswordSerializeHolder passData = null;
            String clearPassword = "";
            try
            {
                passData = ReadDatFile();
                clearPassword = cryptoService.Descrypt(passData.Password, passData.PublicKey);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return (clearPassword);
        }

        /// <summary>
        /// The WriteDatFile
        /// </summary>
        /// <param name="passObject">The passObject<see cref="DataPassword"/></param>
        public static void WriteDatFile(DataPassword passObject)
        {
            FileStream fs = null;
            try
            {
                if (File.Exists(EncryptDecrypt.passFilePath))
                {
                    File.Delete(EncryptDecrypt.passFilePath);
                }
                fs = File.Create(EncryptDecrypt.passFilePath);
                DataPasswordSerializeHolder holder = new DataPasswordSerializeHolder(passObject);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, holder);                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                fs.Close();                
            }
        }

        /// <summary>
        /// The ReadDatFile
        /// </summary>
        /// <returns>The <see cref="DataPasswordSerializeHolder"/></returns>
        public static DataPasswordSerializeHolder ReadDatFile()
        {
            FileStream fs = new FileStream(EncryptDecrypt.passFilePath, FileMode.Open);
            DataPasswordSerializeHolder dataPassHolder = null;
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                dataPassHolder = (DataPasswordSerializeHolder)formatter.Deserialize(fs);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                fs.Close();
            }

            return dataPassHolder;
        }
    }

    /// <summary>
    /// Defines the <see cref="DataPasswordSerializeHolder" />
    /// </summary>
    [Serializable]
    class DataPasswordSerializeHolder
    {
        /// <summary>
        /// Gets or sets the Password
        /// </summary>
        public byte[] Password { get; set; }

        /// <summary>
        /// Gets or sets the PublicKey
        /// </summary>
        public string PublicKey { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataPasswordSerializeHolder"/> class.
        /// </summary>
        /// <param name="dataObject">The dataObject<see cref="EncryptPasswordHelper.Entities.DataPassword"/></param>
        public DataPasswordSerializeHolder(EncryptPasswordHelper.Entities.DataPassword dataObject)
        {
            Password = dataObject.Password;
            PublicKey = dataObject.PublicKey;
        }
    }
}
