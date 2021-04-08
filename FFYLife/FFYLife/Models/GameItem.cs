using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;


namespace Models
{
    public abstract class GameItem
    {
        protected Geometry area;

        public double CX { get; set; } 
        public double CY { get; set; }


        public double rotDegree;


        public Geometry RealArea
        {

            get
            {
                TransformGroup tg = new TransformGroup();
                tg.Children.Add(new TranslateTransform(CX, CY));
                tg.Children.Add(new RotateTransform(rotDegree, CX, CY));
                area.Transform = tg;
                return area.GetFlattenedPathGeometry();


            }
        }

        public bool IsCollision(GameItem other)
        {
            return Geometry.Combine(this.RealArea, other.RealArea, GeometryCombineMode.Intersect, null).GetArea() > 0;
        }

    }
}
