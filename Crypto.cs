using System;
using static System.Console;
using System.Security.Cryptography;
using System.IO;


namespace GuardCrypto{

    public class aesCrypto{
        //AES128 CBC
        private Aes? aes = null;
        private byte[]? key = null;
        private byte[]? iv = null;
        private string? data = null;
        private byte[]? cipher = null;
        public int aesInit(byte[] key,byte[] iv){
            try{
                aes = Aes.Create();
            }catch{
                return -1;
            }

            if(key == null || key.Length <= 0 || iv == null || iv.Length <= 0){
                return -1;
            }
            this.key = key;
            this.iv  = iv;
            aes.Key  = key;
            aes.IV   = iv;

            return 0;
        }

        public int aesLoadData(string data){
            if(data == null || data.Length <= 0){
                return -1;
            }
            this.data = data;
            return 0;
        }

        public int aesEncrypt(){
            if(aes == null || data == null)return -1;
            
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(data);
                            data = null;   
                        }
                        cipher = msEncrypt.ToArray();
                    }
                }

            return 0;
        }

        public int aesFetchCipher(ref byte[] cipher){
            if(this.cipher == null || this.cipher.Length <= 0){
                return -1;
            }
            cipher = this.cipher;
            this.cipher = null;
            return 0;
        }
    }

    public class shaCrypto{
        //SHA256
        SHA256? sha;

        public shaCrypto(){
            try{
                sha = SHA256.Create();
            }catch{
                sha = null;
            }
        }

        public int shaCompute(byte[] data, ref byte[] hash){
            if(sha == null || data == null || data.Length<=0)return -1;

            hash = sha.ComputeHash(data);

            return 0;
        }



    }


}


