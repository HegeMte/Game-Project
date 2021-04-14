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

        public Control()
        {
            Loaded += Control_Loaded;
        }

        private void Control_Loaded(object sender, RoutedEventArgs e)
        {
            repo = new StorageRepo();
            gameModel = new GameModel(1300,800,"Fasz");
           
            logic = new GameLogicc(gameModel, repo);
            renderer = new Renderer(gameModel);


            Window win = Window.GetWindow(this);

             

            if (win != null)
            {
                win.MouseLeftButtonDown += Win_MouseLeftButtonDown;
                win.KeyDown += Win_KeyDown;
            }


            InvalidateVisual();


        }

        private void Win_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mouseX = e.GetPosition(this).X;
            mouseY = e.GetPosition(this).Y;

            if (gameModel.ChestIsOn)
            {

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
                if (mouseY >= 650 && mouseY <= 750 && mouseX >= 1135 && mouseX <= 1335)
                {
                    logic.ReturnToMenu();

                }

            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (renderer != null) drawingContext.DrawDrawing(renderer.BuildDrawing());
           
        }

        private void Win_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        { 
            switch (e.Key)
            {
                case Key.Space :
                    logic.StepTick();
                    gameModel.BlockNumber++;
                    break;
            }
            InvalidateVisual();

        }
    }
}
