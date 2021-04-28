// <copyright file="GameItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameModel.Models
{
    using System.Windows.Media;

    /// <summary>
    /// GameItem abstract class.
    /// </summary>
    public abstract class GameItem
    {
        /// <summary>
        /// Gets or sets  the Rotdegree.
        /// </summary>
        public double Rotdegree;

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        protected Geometry area;

        /// <summary>
        /// Gets or sets  the CX.
        /// </summary>
        public double CX { get; set; }

        /// <summary>
        /// Gets or sets  the CY.
        /// </summary>
        public double CY { get; set; }

        /// <summary>
        /// Gets or sets  the RealArea.
        /// </summary>
        public Geometry RealArea
        {
            get
            {
                TransformGroup tg = new TransformGroup();
                tg.Children.Add(new TranslateTransform(this.CX, this.CY));
                tg.Children.Add(new RotateTransform(this.Rotdegree, this.CX, this.CY));
                this.area.Transform = tg;
                return this.area.GetFlattenedPathGeometry();
            }
        }

        /// <summary>
        /// IsCollision method.
        /// </summary>
        /// <param name="other">the parameter is a GameItem entity.</param>
        /// <returns>a bool.</returns>
        public bool IsCollision(GameItem other)
        {
            return Geometry.Combine(this.RealArea, other.RealArea, GeometryCombineMode.Intersect, null).GetArea() > 0;
        }
    }
}