using System;
using System.IO;
using QRCoder;
using System.Drawing.Imaging;
using System.Drawing;

namespace QR{
    class program{
        public static void Main(){
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode("The text which should be encoded.", QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20,Color.Black,Color.White,true);
            qrCodeImage.Save(@"./a.bmp",System.Drawing.Imaging.ImageFormat.Bmp);
        } 
    }
}