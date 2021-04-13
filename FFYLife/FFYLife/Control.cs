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
                win.KeyDown += Win_KeyDown;
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
                case Key.Space :
                    logic.StepTick();
                    gameModel.BlockNumber++;
                    break;
            }
            InvalidateVisual();

        }
    }
}
