using GameLogic;
using GameModel.Models;
using StorageRepository;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace FFYLife
{
    internal class Control : FrameworkElement
    {
        private IStorageRepository repo;
        private Renderer renderer;
        private IGameModel gameModel;
        private IGameLogic logic;
        private double mouseX;
        private double mouseY;
        private double Difference = 130;
        private DispatcherTimer OneStepTimer;
        private DispatcherTimer EnemyAttackTimer;

        static public string PlayerName = "teszt";
        static public string SaveFile;
        DispatcherTimer HitCooldown;
        public Control()
        {
            Loaded += Control_Loaded;
        }

        private void Control_Loaded(object sender, RoutedEventArgs e)
        {
            repo = new StorageRepo();
            ;
            if (SaveFile == null)
            {
                gameModel = new GameModel.Models.GameModel(1300, 800, PlayerName);
            }
            else
            {
                gameModel = repo.LoadGame(SaveFile);
            }

            logic = new GameLogicc(gameModel, repo);
            renderer = new Renderer(gameModel);

            Window win = Window.GetWindow(this);

            //MonsterstepTimer.Interval = TimeSpan.FromMilliseconds(30);
            //MonsterstepTimer.Tick += MonsterstepTimer_Tick;
            //MonsterstepTimer.Start();

            if (win != null)
            {
                win.MouseLeftButtonDown += Win_MouseLeftButtonDown;
                win.KeyDown += Win_KeyDown;
            }

            EnemyAttackTimer = new DispatcherTimer();
            EnemyAttackTimer.Tick += delegate
            {
                if (gameModel.GameOver)
                {
                    GameOver();
                    EnemyAttackTimer.Stop();
                }
                else
                {
                    DispatcherTimer HitTime = new DispatcherTimer();
                    gameModel.CanAttack = false;
                    HitTime.Tick += delegate
                    {
                        gameModel.CanAttack = true;
                        HitTime.Stop();
                        InvalidateVisual();
                    };
                    HitTime.Interval = TimeSpan.FromMilliseconds(400);
                    HitTime.Start();
                    InvalidateVisual();
                    logic.MonsterAttack();
                }
                InvalidateVisual();
            };
            EnemyAttackTimer.Interval = TimeSpan.FromMilliseconds(1500);
            EnemyAttackTimer.Start();
            gameModel.Hero.CanAttack = true;

            HitCooldown = new DispatcherTimer();
            InvalidateVisual();
        }

        private void Win_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            
            DispatcherTimer Move = new DispatcherTimer();
            switch (e.Key)
            {
                case Key.Escape:
                    if (MessageBox.Show("Are you sure?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        //Window win = Window.GetWindow(this);
                        //win.Close();
                        ReturnToMenu();
                    }

                    break;

                case Key.D:

                    gameModel.Hero.IsDefending = true;

                    break;

                case Key.S:

                    gameModel.Hero.IsDefending = false;

                    break;

                case Key.Space:

                    var entity = logic.StepCalculator();

                    //logic.StepTick();

                    //logic.FindGameItem(entity);
                    if (gameModel.Monsters[0].CX <= 195)
                    {
                        gameModel.IsInFight = true;
                    }
                    if (entity.CX >= 195 && !gameModel.ChestIsOn)
                    {
                        gameModel.Moving = true;
                        logic.StepTick();
                        Move.Tick += delegate
                        {
                            HitCooldown.Stop();

                            InvalidateVisual();
                        };

                        HitCooldown.Interval = TimeSpan.FromMilliseconds(50);
                        HitCooldown.Start();
                        gameModel.Moving = false;
                        InvalidateVisual();
                    }

                    InvalidateVisual();
                    break;

                case Key.A:
                    if (gameModel.Hero.CanAttack)
                    {
                       
                        logic.HeroAttack();
                        gameModel.Hero.CanAttack = false;

                        HitCooldown.Tick += delegate
                        {
                            gameModel.Hero.CanAttack = true;
                            HitCooldown.Stop();
                            InvalidateVisual();
                        };
                        HitCooldown.Interval = TimeSpan.FromMilliseconds(1000);
                        HitCooldown.Start();
                        InvalidateVisual();
                    }

                    break;
            }
            InvalidateVisual();
        }

        //private void MonsterstepTimer_Tick(object sender, EventArgs e)
        //{
        //    if (gameModel.Monsters[0])
        //    {
        //        MonsterstepTimer.Stop();
        //        return;
        //    }

        //   logic.MonstersTick(gameModel.Monsters)
        //   InvalidateVisual();

        //}

        private void Win_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mouseX = e.GetPosition(this).X;
            mouseY = e.GetPosition(this).Y;

            if (gameModel.ChestIsOn)
            {
                if (mouseX >= 1200 && mouseX <= 1230)
                {
                    if (mouseY >= 615 && mouseY <= 645)
                    {
                        if (logic.AnswerB())
                        {
                            MessageBox.Show("Right Answer!");
                        }
                        else
                        {
                            MessageBox.Show("U are Stupid!");
                        }
                    }
                    else if (mouseY >= 700 && mouseY <= 730)
                    {
                        if (logic.AnswerD())
                        {
                            MessageBox.Show("Right Answer!");
                        }
                        else
                        {
                            MessageBox.Show("U are Stupid!");
                        }
                    }
                }
                if (mouseX >= 890 && mouseX <= 920)
                {
                    if (mouseY >= 615 && mouseY <= 645)
                    {
                        if (logic.AnswerA())
                        {
                            MessageBox.Show("Right Answer!");
                        }
                        else
                        {
                            MessageBox.Show("U are Stupid!");
                        }
                    }
                    else if (mouseY >= 700 && mouseY <= 730)
                    {
                        if (logic.AnswerC())
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
                if (mouseY >= 530 && mouseY <= 580)
                {
                    if (mouseX >= 825 && mouseX <= 925)
                    {
                        if (!logic.BuyDmg())
                        {
                            MessageBox.Show("You don't have enough vbuck , ask mommy to buy some more!");
                        }
                    }
                    else if (mouseX >= 1150 && mouseX <= 1250)
                    {
                        if (logic.BuyHP() == 1)
                        {
                            MessageBox.Show("You don't have enough vbuck , ask mommy to buy some more!");
                        }
                        else if (logic.BuyHP() == 2)
                        {
                            MessageBox.Show("You are already at full HP");
                        }
                    }
                }
                if (mouseY >= 730 && mouseY <= 780 && mouseX >= 825 && mouseX <= 925)
                {
                    if (logic.BuyArmor() == 1)
                    {
                        MessageBox.Show("You don't have enough vbuck , ask mommy to buy some more!");
                    }
                    else if (logic.BuyArmor() == 2)
                    {
                        MessageBox.Show("You are already at full Armor");
                    }
                }
                if (mouseY >= 650 && mouseY <= 750 && mouseX >= 1035 && mouseX <= 1235)
                {
                    if (MessageBox.Show("Are you sure you want to save and quit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        repo.SaveGame(gameModel);
                        ReturnToMenu();
                    }
                }
            }

            InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (renderer != null) drawingContext.DrawDrawing(renderer.BuildDrawing());
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
            StorageRepo.SaveHighScore(gameModel);
            ReturnToMenu();
        }

        //private void GameOver()
        //{
        //    GameLogic.LostEncounter();
        //    MessageBox.Show("GAME OVER!");
        //    this.ReturnToMenu();
        //}
    }
}