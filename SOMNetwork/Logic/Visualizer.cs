using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Configuration;
using System.Drawing.Drawing2D;
using SOMNetwork.Model;

namespace SOMNetwork.Logic
{
    public class Visualizer
    {

        public Bitmap Visualize(List<Orderliness> orderlinesses)
        {
            var scale = 10;

            var mapWidth = 100 * scale;
            var mapHeight = 100 * scale;

            var margin = 10;

            var imageWidth = mapWidth + 2*margin;
            var imageHeight = mapHeight  + 2*margin;

            Bitmap bm = new Bitmap(imageWidth,imageHeight);

            var fontSize = 30;
            var stringMargin = 30;



            var ellipserRadius = 5;

            using (Graphics g = Graphics.FromImage(bm))
            {
                // Modify the image using g here... 
                // Create a brush with an alpha value and use the g.FillRectangle function


                Brush b = new SolidBrush(Color.Red);
                Pen p = new Pen(Color.Black);

                var startPoint = new Point(margin, margin);

                var ramka = new Rectangle(startPoint, new Size(mapWidth, mapHeight));
                
                g.DrawRectangle(p, ramka);


                FontFamily fontFamily = new FontFamily("Arial");

                Font font = new Font(
                   fontFamily,
                   fontSize,
                   FontStyle.Regular,
                   GraphicsUnit.Pixel);
                SolidBrush shadowBrush = new SolidBrush(Color.Black);

                foreach (var orderliness in orderlinesses)
                {
                    var cityXScale = (int)orderliness.Coordinate.At(0, 0)*scale;
                    var cityYScale = (int) orderliness.Coordinate.At(0, 1)*scale;

                    var leftUpper = new Point(cityXScale+margin - ellipserRadius, cityYScale+margin - ellipserRadius);
                    var leftUpperForString = new Point(leftUpper.X, leftUpper.Y - stringMargin);

                    g.DrawString(orderliness.Order.ToString(), font, shadowBrush, leftUpperForString);

                    var rec = new Rectangle(leftUpper, new Size(2*ellipserRadius, 2*ellipserRadius));
                    g.FillEllipse(b, rec);
                }
            }

           

            return bm;
        }

    }
}
