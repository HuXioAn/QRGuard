using System;
using System.IO;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;

namespace QRGen{
    
    public static class QrCodeUtil
    {
        static QRCodeEncoder encoder;

        static QrCodeUtil(){
            encoder = new QRCodeEncoder();
        }

        public static void EncodeConfig(QRCodeEncoder.ENCODE_MODE mode = QRCodeEncoder.ENCODE_MODE.BYTE, 
                                int scale = 4, 
                                QRCodeEncoder.ERROR_CORRECTION correctionRatio 
                                = QRCodeEncoder.ERROR_CORRECTION.M){
            encoder.QRCodeEncodeMode = mode;
            encoder.QRCodeScale = scale;
            encoder.QRCodeErrorCorrect = correctionRatio;
            //encoder.QRCodeVersion = 4;
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
    

    public class QRCodeHelper
    {
        /// <summary>
        ///  生成二维码
        /// </summary>
        /// <param name="url">扫描后跳转url</param>
        /// <param name="insertImgUrl">中间图片url（相对路径），为空则不带图片</param>
        /// <returns></returns>
        public static Bitmap CreateQRCode(string url,string insertImgUrl=null)
        {
            // QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            // qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            // qrCodeEncoder.QRCodeScale = 4;
            // //qrCodeEncoder.QRCodeVersion = 8;
            // qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            Image image = QrCodeUtil.Encode(url);
            if (string.IsNullOrEmpty(insertImgUrl))
            {
                return AddFrame(image,Margin: 15);
            }
            //插入图片路径
            string path = insertImgUrl;//HttpContext.Current.Server.MapPath(insertImgUrl);
            var bitmap = AddFrame(CombinImage(image, path),Margin: 15);
            return bitmap;
        }
 
        /// <summary>
        /// 在图片四周加入白边
        /// </summary>
        /// <param name="Img">图片</param>
        /// <param name="Margin">白边的高度，单位是像素</param>
        /// <returns>Bitmap</returns>
        private static Bitmap AddFrame(Image Img, int Margin=10)
        {
            //位图宽高
            int width = Img.Width + Margin;
            int height = Img.Height + Margin;
            Bitmap BitmapResult = new Bitmap(width, height);
            Graphics Grp = Graphics.FromImage(BitmapResult);
            SolidBrush b = new SolidBrush(Color.White);//这里修改颜色
            Grp.FillRectangle(b, 0, 0, width, height);
            System.Drawing.Rectangle Rec = new System.Drawing.Rectangle(0, 0, Img.Width, Img.Height);
            //向矩形框内填充Img
            Grp.DrawImage(Img, Margin/2, Margin/2 , Rec, GraphicsUnit.Pixel);
            //返回位图文件
            Grp.Dispose();
            GC.Collect();
            return BitmapResult;
        }
 
        /// <summary>   
        /// 调用此函数后使此两种图片合并，类似相册，有个   
        /// 背景图，中间贴自己的目标图片   
        /// </summary>   
        /// <param name="imgBack">粘贴的源图片</param>   
        /// <param name="destImg">粘贴的目标图片</param>   
        private static Image CombinImage(Image imgBack, string destImg)
        {
            Image img = Image.FromFile(destImg);        //照片图片     
            if (img.Height != 65 || img.Width != 65)
            {
                img = KiResizeImage(img, 30, 30, 0);
            }
            Graphics g = Graphics.FromImage(imgBack);
 
            g.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);      //g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);    
 
            //g.FillRectangle(System.Drawing.Brushes.White, imgBack.Width / 2 - img.Width / 2 - 1, imgBack.Width / 2 - img.Width / 2 - 1,1,1);//相片四周刷一层黑色边框   
 
            //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);   
 
            g.DrawImage(img, imgBack.Width / 2 - img.Width / 2, imgBack.Width / 2 - img.Width / 2, img.Width, img.Height);
            GC.Collect();
            return imgBack;
        }
 
 
        /// <summary>   
        /// Resize图片   
        /// </summary>   
        /// <param name="bmp">原始Bitmap</param>   
        /// <param name="newW">新的宽度</param>   
        /// <param name="newH">新的高度</param>   
        /// <param name="Mode">保留着，暂时未用</param>   
        /// <returns>处理以后的图片</returns>   
        private static Image KiResizeImage(Image bmp, int newW, int newH, int Mode)
        {
            try
            {
                Image b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);
                // 插值算法的质量   
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch
            {
                return null;
            }
        }
    }
}