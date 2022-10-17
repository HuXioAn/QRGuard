using System;
using static System.Console;
using GuardCrypto;
using QRGen;


namespace QRGuard{
    class Program{
        static void Main(){
            // var qr = QrCodeUtil.Encode("院士挠头");
            // qr.Save("")
            //QrCodeUtil.Create("院士挠头","./a.jpg");
            string data = "1145141145;某某某;1919810114514;25632563";
            var hash = new byte[128];
            var sha256 = new shaCrypto();
            if(sha256 != null){
                sha256.shaCompute(
                    System.Text.Encoding.GetEncoding("UTF-8").GetBytes(data),ref hash);
            }

            WriteLine(BitConverter.ToString(hash));
            WriteLine(System.Text.Encoding.Default.GetString(hash));


        }
    }
}