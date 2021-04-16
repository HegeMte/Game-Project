using GameLogic;
using StorageRepository;
using StorageRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace FFYLife
{
    class Control : FrameworkElement
    {
        IStorageRepository repo;
        Renderer renderer;
        IGameModel gameModel;
        IGameLogic logic;
        private double mouseX;
        private double mouseY;
        private double Difference = 130;
        DispatcherTimer OneStepTimer;
        static public string PlayerName = "teszt";
        static public string SaveFile;

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
                gameModel = new GameModel(1300, 800, PlayerName);
            }
            else
            {
               gameModel =   repo.LoadGame(SaveFile);
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


            InvalidateVisual();


        }

        //private void MonsterstepTimer_Tick(object sender, EventArgs e)
        //{
        //    if (gameModel.Monsters[0])
        //    {
        //        MonsterstepTimer.Stop();
        //        return;
        //    }
               
        //   logic.MonstersTick(gameModel.Monsters);
        //   InvalidateVisual();

           
            
        //}

        private void Win_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mouseX = e.GetPosition(this).X;
            mouseY = e.GetPosition(this).Y;

            if (gameModel.ChestIsOn)
            {
                //if (mouseX >= 1200 && mouseX <= 1230)
                //{

                //    if (mouseY >= 615 && mouseY <= 645)
                //    {
                //        logic.AnswerA();
                //    }
                //    else if(mouseY >= 700 && mouseY <= 730)
                //    {
                //        logic.AnswerD();
                //    }


                //}
                //if (mouseX >= 890 && mouseX <= 920)
                //{
                //    if (mouseY >= 615 && mouseY <= 645)
                //    {
                //        logic.AnswerB();
                //    }
                //    else if(mouseY >= 700 && mouseY <= 730)
                //    {
                //        logic.AnswerC();
                //    }

                //}

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
                        if (!logic.BuyHP())
                        {
                            MessageBox.Show("You don't have enough vbuck , ask mommy to buy some more!");
                        }

                    }
                }
                if (mouseY >= 730 && mouseY <= 780 && mouseX >= 825 && mouseX <= 925)
                {
                    if (!logic.BuyArmor())
                    {
                        MessageBox.Show("You don't have enough vbuck , ask mommy to buy some more!");
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

        private void Win_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        { 
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
                
                
                
                case Key.Space :

                    var entity = logic.StepCalculator();

                    
                    //logic.StepTick();
                    OneStepTimer = new DispatcherTimer();
                    double destination =   this.gameModel.Monsters[0].CX - Difference;
                    OneStepTimer.Tick += delegate
                    {
                        if (logic.FindGameItem(entity).CX == 195)
                        {
                            
                            OneStepTimer.Stop();
                            return;
                        }

                        logic.StepTick();
                        InvalidateVisual();


                    };

                    OneStepTimer.Interval = TimeSpan.FromMilliseconds(20);
                    OneStepTimer.Start();
                    gameModel.BlockNumber++;
                    break;
            }
            InvalidateVisual();

        }


        private void ReturnToMenu()
        {
            MainMenu menu = new MainMenu();
            Window win = Window.GetWindow(this);
            menu.Show();
            win.Close();
        }


        //private void GameOver()
        //{
        //    GameLogic.LostEncounter();
        //    MessageBox.Show("GAME OVER!");
        //    this.ReturnToMenu();
        //}




    }
}
