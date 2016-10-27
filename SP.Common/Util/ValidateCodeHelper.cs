﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace SP.Common.Util
{
    public class ValidateCodeHelper
    {
        /// <summary>
        /// 产生图形验证码
        /// </summary>
        /// <param name="Code">传出验证码</param>
        /// <param name="CodeLength">验证码长度</param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="FontSize"></param>
        /// <returns></returns>
        public static byte[] CreateValidateGraphic(out String Code, int CodeLength, int Width, int Height, int FontSize)
        {
            String sCode = String.Empty;
            //颜色列表，用于验证码、噪线、噪点
            Color[] oColors ={
                Color.Black,
                Color.Red,
                Color.Blue,
                Color.Green,
                Color.Orange,
                Color.Brown,
                Color.Brown,
                Color.DarkBlue
            };
            //字体列表，用于验证码
            string[] oFontNames = { "sans-serif", "Microsoft YaHei", "Times New Roman", "MS Mincho", "Book Antiqua", "Gungsuh", "PMingLiU", "Impact" };
            //验证码字集，去掉了一些容易混淆的字
            char[] oCharacter = {
                '2','3','4','5','6','8','9',
                'A','B','C','D','E','F','G','H','J','K', 'L','M','N','P','R','S','T','W','X','Y'
            };
            Random oRnd = new Random();
            Bitmap oBmp = null;
            Graphics oGraphics = null;
            int N1 = 0;
            Point oPoint1 = default(Point);
            Point oPoint2 = default(Point);
            string sFontName = null;
            Font oFont = null;
            Color oColor = default(Color);

            //生成验证码字串
            for (N1 = 0; N1 <= CodeLength - 1; N1++)
            {
                sCode += oCharacter[oRnd.Next(oCharacter.Length)];
            }

            oBmp = new Bitmap(Width, Height);
            oGraphics = Graphics.FromImage(oBmp);
            oGraphics.Clear(System.Drawing.Color.White);
            try
            {
                for (N1 = 0; N1 <= 4; N1++)
                {
                    //画噪线
                    oPoint1.X = oRnd.Next(Width);
                    oPoint1.Y = oRnd.Next(Height);
                    oPoint2.X = oRnd.Next(Width);
                    oPoint2.Y = oRnd.Next(Height);
                    oColor = oColors[oRnd.Next(oColors.Length)];
                    oGraphics.DrawLine(new Pen(oColor), oPoint1, oPoint2);
                }

                float spaceWith = 0, dotX = 0, dotY = 0;
                if (CodeLength != 0)
                {
                    spaceWith = (Width - FontSize * CodeLength - 10) / CodeLength;
                }

                for (N1 = 0; N1 <= sCode.Length - 1; N1++)
                {
                    //画验证码字串
                    sFontName = oFontNames[oRnd.Next(oFontNames.Length)];
                    oFont = new Font(sFontName, FontSize, FontStyle.Italic);
                    oColor = oColors[oRnd.Next(oColors.Length)];

                    dotY = (Height - oFont.Height) / 2 + 2;//中心下移2像素
                    dotX = Convert.ToSingle(N1) * FontSize + (N1 + 1) * spaceWith;

                    oGraphics.DrawString(sCode[N1].ToString(), oFont, new SolidBrush(oColor), dotX, dotY);
                }

                for (int i = 0; i <= 30; i++)
                {
                    //画噪点
                    int x = oRnd.Next(oBmp.Width);
                    int y = oRnd.Next(oBmp.Height);
                    Color clr = oColors[oRnd.Next(oColors.Length)];
                    oBmp.SetPixel(x, y, clr);
                }

                //画图片的边框线
                oGraphics.DrawRectangle(new Pen(Color.White), 0, 0, oBmp.Width - 1, oBmp.Height - 1);

                Code = sCode;
                //保存图片数据
                MemoryStream stream = new MemoryStream();
                oBmp.Save(stream, ImageFormat.Jpeg);
                //输出图片流
                return stream.ToArray();
            }
            finally
            {
                oGraphics.Dispose();
            }
        }
    }
}