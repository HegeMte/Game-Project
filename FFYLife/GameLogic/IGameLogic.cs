// <copyright file="IGameLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameLogic
{
    using System.Collections.Generic;
    using GameModel.Models;

    /// <summary>
    /// IGameLogic which requires 'gamelogic' methods.
    /// </summary>
    public interface IGameLogic
    {
        /// <summary>
        /// Requires the MonsterAttack method.
        /// </summary>
        void MonsterAttack();

        /// <summary>
        /// Requires the HeroAttack method.
        /// </summary>
        void HeroAttack();

        /// <summary>
        /// Requires the BlockTick method.
        /// </summary>
        /// <param name="block">the parameter which is a OneBlock entity.</param>
        void BlockTick(OneBlock block);

        /// <summary>
        /// Requires the BlockTick method.
        /// </summary>
        /// <param name="monsters">the parameter which is a ListOneMonster.</param>
        void MonstersTick(List<OneMonster> monsters);

        /// <summary>
        /// Requires the ChestTick method.
        /// </summary>
        void ChestTick();

        /// <summary>
        /// Requires the StepTick method.
        /// </summary>
        void StepTick();

        /// <summary>
        /// Requires the BuyDmg method.
        /// </summary>
        /// <returns>a bool.</returns>
        bool BuyDmg();

        /// <summary>
        /// Requires the BuyHP method.
        /// </summary>
        /// <returns>returns an int.</returns>
        int BuyHP();

        /// <summary>
        /// Requires the BuyArmor method.
        /// </summary>
        /// <returns>returns an int.</returns>
        int BuyArmor();

        /// <summary>
        /// Requires the HeroIsDefending method.
        /// </summary>
        void HeroIsDefending();

        /// <summary>
        /// Requires the StepCalculator method.
        /// </summary>
        /// <returns>a GameItem entity.</returns>
        GameItem StepCalculator();

        /// <summary>
        /// Requires the StepCalculator method.
        /// </summary>
        /// <param name="item">The parameter is a GameItem entity.</param>
        /// <returns>a GameItem entity.</returns>
        GameItem FindGameItem(GameItem item);

        /// <summary>
        /// Requires the ChestAhead method.
        /// </summary>
        /// <returns>returns true or false.</returns>
        bool ChestAhead();

        /// <summary>
        /// Requires the AnswerA method.
        /// </summary>
        /// <returns>returns true or false.</returns>
        bool AnswerA();

        /// <summary>
        /// Requires the AnswerB method.
        /// </summary>
        /// <returns>returns true or false.</returns>
        bool AnswerB();

        /// <summary>
        /// Requires the AnswerC method.
        /// </summary>
        /// <returns>returns true or false.</returns>
        bool AnswerC();

        /// <summary>
        /// Requires the AnswerD method.
        /// </summary>
        /// <returns>returns true or false.</returns>
        bool AnswerD();

        /// <summary>
        /// Requires the MonsterDied method.
        /// </summary>
        /// <param name="monsters">the parameter is a ListOneMonster.</param>
        void MonsterDied(List<OneMonster> monsters);

        /// <summary>
        /// Requires the LoadGame method.
        /// </summary>
        /// <param name="filesave">The parameter is a string.</param>
        /// <returns>A GameModel entity.</returns>
        GameModell LoadGame(string filesave);
    }
}