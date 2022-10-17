using System;
using System.IO;
using System.Drawing;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;

namespace QRGen{
    
    public static class QrCodeUtil
    {
        static QRCodeEncoder encoder;

        static QrCodeUtil(){
            encoder = new QRCodeEncoder();
        }

        static void EncodeConfig(QRCodeEncoder.ENCODE_MODE mode = QRCodeEncoder.ENCODE_MODE.BYTE, 
                                int scale = 4, 
                                QRCodeEncoder.ERROR_CORRECTION correctionRatio 
                                = QRCodeEncoder.ERROR_CORRECTION.M){
            encoder.QRCodeEncodeMode = mode;
            encoder.QRCodeScale = scale;
            encoder.QRCodeErrorCorrect = correctionRatio;
        }


        /// <summary>
        /// 返回二维码图片
        /// </summary>
        public static Bitmap Encode(string text)
        {
            return encoder.Encode(text);
        }

        /// <summary>
        /// 定义参数,生成二维码
        /// </summary>
        public static void Create(string text, string path)
        => Encode(text).Save(path);

    }
    
}