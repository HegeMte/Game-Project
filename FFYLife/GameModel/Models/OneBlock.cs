// <copyright file="OneBlock.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameModel.Models
{
    /// <summary>
    /// OneBlock class.
    /// </summary>
    public class OneBlock : GameItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneBlock"/> class.
        /// </summary>
        /// <param name="cx">cx.</param>
        /// <param name="cy">cy.</param>
        public OneBlock(double cx, double cy)
        {
            this.CX = cx;
            this.CY = cy;
        }
    }
}