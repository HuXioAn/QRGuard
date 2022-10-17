using System;
using static System.Console;
using GuardCrypto;
using QRGen;
using System.Security.Cryptography;



namespace QRGuard{
    class Program{
        static void Main(){
           

            
            string data = "HuXiaoAn;1665991663";

            var hash = new byte[128];
            var sha256 = new shaCrypto();
            if(sha256 != null){
                sha256.shaCompute(
                    System.Text.Encoding.GetEncoding("ASCII").GetBytes(data),ref hash);
            }

            WriteLine(BitConverter.ToString(hash));
            WriteLine(System.Text.Encoding.Default.GetString(hash));

            
            string partSha = BitConverter.ToString(hash);
            partSha = partSha.Replace("-","");
            data = data + ";" + partSha[0..12];
            WriteLine($"AfterSHA Data:{data}");

            
            var cipher = new byte[128];
            var aes = Aes.Create();
            // string key = System.Text.Encoding.ASCII.GetString(aes.Key);
            // WriteLine(key);
            aes.Key = System.Text.Encoding.ASCII.GetBytes("11451411451411451411451411451444");
            aes.IV  = System.Text.Encoding.ASCII.GetBytes("1145141145144444");
            WriteLine($"key:{BitConverter.ToString(aes.Key)},\niv:{BitConverter.ToString(aes.IV)}");

            var aes128 = new aesCrypto();
            aes128.aesInit(aes.Key,aes.IV);
            aes128.aesLoadData(data);

            aes128.aesEncrypt();
            aes128.aesFetchCipher(ref cipher);
            
            WriteLine(BitConverter.ToString(cipher));
            WriteLine(System.Text.Encoding.ASCII.GetString(cipher));
            WriteLine("Base64:{0},Length:{1}",Convert.ToBase64String(cipher),Convert.ToBase64String(cipher).Length);


            QrCodeUtil.Create(Convert.ToBase64String(cipher),"./a.jpg");

        }
    }
}