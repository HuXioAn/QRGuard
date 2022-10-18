using System;
using static System.Console;
using System.Security.Cryptography;
using GuardCrypto;
using QRGen;




namespace QRGuard{
    class Guard{

        shaCrypto sha256;
        aesCrypto aes128;
        DateTime epoch;

        public Guard(){
            sha256 = new shaCrypto();
            aes128 = new aesCrypto();
            epoch = new DateTime(2000,1,1,0,0,0);
        }

        public int GuardGen(string name){
            Int64 dt = (Int64)DateTime.Now.Subtract(epoch).TotalSeconds;
            var delta_time = dt.ToString();
            //Check name 
            if(string.IsNullOrWhiteSpace(name)){
                return -1;
            }else if(name.Length > 20){
                name = name[0..20];
            }
            string data = name + ";" + delta_time;

            WriteLine($"Raw data:{data},Length:{data.Length}");
            
            var hash = new byte[64];
            if(sha256 != null){
                sha256.shaCompute(
                    System.Text.Encoding.GetEncoding("ASCII").GetBytes(data),ref hash);
            }
            string partSha = BitConverter.ToString(hash);
            partSha = partSha.Replace("-","");
            data = data + ";" + partSha[0..12];

            var cipher = new byte[128];

            var aesKey = System.Text.Encoding.ASCII.GetBytes("11451411451411451411451411451444");
            var aesIV  = System.Text.Encoding.ASCII.GetBytes("1145141145144444");
            aes128.aesInit(aesKey,aesIV);
            aes128.aesLoadData(data);
            aes128.aesEncrypt();
            aes128.aesFetchCipher(ref cipher);

            string cipherBase64 = Convert.ToBase64String(cipher);

            WriteLine($"Base64 cipher:{cipherBase64},Length:{cipherBase64.Length}");

            QRCodeHelper.CreateQRCode(cipherBase64,"./pic/culture.jpg").Save("./pic/code.jpg");


            return 0;
        }
        static void Main(){
           
            var guard = new Guard();
            guard.GuardGen("HuXiaoan");

        }
    }
}