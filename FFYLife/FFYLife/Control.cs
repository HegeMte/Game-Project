// <copyright file="Control.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FFYLife
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;
    using GameLogic;
    using GameModel.Models;
    using Logic;
    using StorageRepository;

    internal class Control : FrameworkElement
    {

        public static string PlayerName = "teszt";
        public static string PlayerType = "light";
        public static string SaveFile;
        private IStorageRepository repo;
        private Renderer renderer;
        private IGameModel gameModel;
        private IGameLogic logic;
        private ILogic resourcelogic;
        private double mouseX;
        private double mouseY;
        private double Difference = 130;
        private DispatcherTimer OneStepTimer;
        private DispatcherTimer EnemyAttackTimer;

        private DispatcherTimer HitCooldown;

        public Control()
        {
            this.Loaded += Control_Loaded;
        }

        private void Control_Loaded(object sender, RoutedEventArgs e)
        {
            this.repo = new StorageRepo();
            this.resourcelogic = new ResourceLogic(repo as StorageRepo);
            if (SaveFile == null)
            {
                this.gameModel = new GameModel(1300, 800, PlayerName, PlayerType);
            }
            else
            {
                this.gameModel = this.resourcelogic.LoadGame(SaveFile);
            }
            SaveFile = null;

            this.logic = new GameLogicc(this.gameModel, this.repo);
            this.renderer = new Renderer(this.gameModel);

            Window win = Window.GetWindow(this);

            //MonsterstepTimer.Interval = TimeSpan.FromMilliseconds(30);
            //MonsterstepTimer.Tick += MonsterstepTimer_Tick;
            //MonsterstepTimer.Start();

            if (win != null)
            {
                win.MouseLeftButtonDown += this.Win_MouseLeftButtonDown;
                win.KeyDown += this.Win_KeyDown;
            }

            this.EnemyAttackTimer = new DispatcherTimer();
            this.EnemyAttackTimer.Tick += delegate
            {
                if (this.gameModel.GameOver)
                {
                    this.GameOver();
                    this.EnemyAttackTimer.Stop();
                }
                else
                {
                    DispatcherTimer HitTime = new DispatcherTimer();
                    this.gameModel.CanAttack = false;
                    HitTime.Tick += delegate
                    {
                        this.gameModel.CanAttack = true;
                        HitTime.Stop();
                        this.InvalidateVisual();
                    };
                    HitTime.Interval = TimeSpan.FromMilliseconds(400);
                    HitTime.Start();
                    this.InvalidateVisual();
                    this.logic.MonsterAttack();
                }
                this.InvalidateVisual();
            };
            this.EnemyAttackTimer.Interval = TimeSpan.FromMilliseconds(1500);
            this.EnemyAttackTimer.Start();
            this.gameModel.Hero.CanAttack = true;

            this.HitCooldown = new DispatcherTimer();
            this.InvalidateVisual();
        }

        private void Win_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            DispatcherTimer Move = new DispatcherTimer();
            switch (e.Key)
            {
                case Key.Escape:
                    if (MessageBox.Show("Are you sure?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        // Window win = Window.GetWindow(this);
                        // win.Close();
                        this.ReturnToMenu();
                    }

                    break;

                case Key.D:

                    this.gameModel.Hero.IsDefending = true;

                    break;

                case Key.S:

                    this.gameModel.Hero.IsDefending = false;

                    break;

                case Key.Space:

                    var entity = this.logic.StepCalculator();

                    // logic.StepTick();

                    // logic.FindGameItem(entity);
                    if (this.gameModel.Monsters[0].CX <= 195)
                    {
                        this.gameModel.IsInFight = true;
                    }

                    if (entity.CX >= 195 && !this.gameModel.ChestIsOn)
                    {
                        this.gameModel.Moving = true;
                        this.logic.StepTick();
                        Move.Tick += delegate
                        {
                            this.HitCooldown.Stop();

                            this.InvalidateVisual();
                        };

                        this.HitCooldown.Interval = TimeSpan.FromMilliseconds(50);
                        this.HitCooldown.Start();
                        this.gameModel.Moving = false;
                        this.InvalidateVisual();
                    }

                    this.InvalidateVisual();
                    break;

                case Key.A:
                    if (this.gameModel.Hero.CanAttack)
                    {
                        this.logic.HeroAttack();
                        this.gameModel.Hero.CanAttack = false;

                        this.HitCooldown.Tick += delegate
                        {
                            this.gameModel.Hero.CanAttack = true;
                            this.HitCooldown.Stop();
                            this.InvalidateVisual();
                        };
                        this.HitCooldown.Interval = TimeSpan.FromMilliseconds(this.gameModel.Hero.AttackSpeed);
                        this.HitCooldown.Start();
                        this.InvalidateVisual();
                    }

                    break;
            }

            this.InvalidateVisual();
        }

        // private void MonsterstepTimer_Tick(object sender, EventArgs e)
        // {
        //    if (gameModel.Monsters[0])
        //    {
        //        MonsterstepTimer.Stop();
        //        return;
        //    }

        // logic.MonstersTick(gameModel.Monsters)
        //   InvalidateVisual();

        // }

        private void Win_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.mouseX = e.GetPosition(this).X;
            this.mouseY = e.GetPosition(this).Y;

            if (this.gameModel.ChestIsOn)
            {
                if (this.mouseX >= 1200 && this.mouseX <= 1230)
                {
                    if (this.mouseY >= 615 && this.mouseY <= 645)
                    {
                        if (this.logic.AnswerB())
                        {
                            MessageBox.Show("Right Answer!");
                        }
                        else
                        {
                            MessageBox.Show("U are Stupid!");
                        }
                    }
                    else if (this.mouseY >= 700 && this.mouseY <= 730)
                    {
                        if (this.logic.AnswerD())
                        {
                            MessageBox.Show("Right Answer!");
                        }
                        else
                        {
                            MessageBox.Show("U are Stupid!");
                        }
                    }
                }

                if (this.mouseX >= 890 && this.mouseX <= 920)
                {
                    if (this.mouseY >= 615 && this.mouseY <= 645)
                    {
                        if (this.logic.AnswerA())
                        {
                            MessageBox.Show("Right Answer!");
                        }
                        else
                        {
                            MessageBox.Show("U are Stupid!");
                        }
                    }
                    else if (this.mouseY >= 700 && this.mouseY <= 730)
                    {
                        if (this.logic.AnswerC())
                        {
                            MessageBox.Show("Right Answer!");
                        }
                        else
                        {
                            MessageBox.Show("U are Stupid!");
                        }
                    }
                }
            }
            else
            {
                if (this.mouseY >= 530 && this.mouseY <= 580)
                {
                    if (this.mouseX >= 825 && this.mouseX <= 925)
                    {
                        if (!this.logic.BuyDmg())
                        {
                            MessageBox.Show("You don't have enough vbuck , ask mommy to buy some more!");
                        }
                    }
                    else if (this.mouseX >= 1150 && this.mouseX <= 1250)
                    {
                        if (this.logic.BuyHP() == 1)
                        {
                            MessageBox.Show("You don't have enough vbuck , ask mommy to buy some more!");
                        }
                        else if (this.logic.BuyHP() == 2)
                        {
                            MessageBox.Show("You are already at full HP");
                        }
                    }
                }

                if (this.mouseY >= 730 && this.mouseY <= 780 && this.mouseX >= 825 && this.mouseX <= 925)
                {
                    int number = this.logic.BuyArmor();

                    if (number == 1)
                    {
                        MessageBox.Show("You don't have enough vbuck , ask mommy to buy some more!");
                    }
                    else if (number == 2)
                    {
                        MessageBox.Show("You are already at full Armor");
                    }
                }

                if (this.mouseY >= 650 && this.mouseY <= 750 && this.mouseX >= 1035 && this.mouseX <= 1235)
                {
                    if (MessageBox.Show("Are you sure you want to save and quit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        this.resourcelogic.SaveGame(this.gameModel);
                        this.ReturnToMenu();
                    }
                }
            }

            this.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (this.renderer != null) drawingContext.DrawDrawing(this.renderer.BuildDrawing());
        }

        private void ReturnToMenu()
        {
            MainMenu menu = new MainMenu();
            Window win = Window.GetWindow(this);
            menu.Show();
            win.Close();
        }

        private void GameOver()
        {
            MessageBox.Show("Game over dickhole!");
            this.resourcelogic.SaveHighScore(this.gameModel);
            this.ReturnToMenu();
        }

        // private void GameOver()
        // {
        //    GameLogic.LostEncounter();
        //    MessageBox.Show("GAME OVER!");
        //    this.ReturnToMenu();
        // }
    }
}