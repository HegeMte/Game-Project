// <copyright file="Chest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

[assembly: System.CLSCompliant(false)]

namespace GameModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Chest class which contains the properties that are needed to represent the quiz section.
    /// </summary>
    public class Chest : GameItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Chest"/> class.
        /// </summary>
        public Chest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Chest"/> class.
        /// </summary>
        /// <param name="cx">the first parameter.</param>
        /// <param name="cy">the second parameter.</param>
        public Chest(double cx, double cy) // Load
        {
            this.CX = cx;
            this.CY = cy;
        }

        /// <summary>
        /// Gets or sets documentation for the RewardCash.
        /// </summary>
        public int RewardCash { get; set; }

        /// <summary>
        /// Gets or sets documentation for the Question.
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// Gets or sets documentation for the Answers.
        /// </summary>
        public List<string> Answers { get; set; }

        /// <summary>
        /// Gets or sets documentation for the Right.
        /// </summary>
        public int Right { get; set; }
    }
}
