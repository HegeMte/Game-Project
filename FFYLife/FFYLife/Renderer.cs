using GameModel.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FFYLife
{
    public  class Renderer
    {
        IGameModel model;

        public Renderer(IGameModel _gm)
        {
            this.model = _gm;
        }

        Drawing oldGameplayBackground;
        Drawing oldUI;
        Drawing oldShop;
        Drawing oldBlocks;
        Drawing oldHero;
       
        Pen stroke = new Pen(Brushes.Black , 3);
        Pen Is = new Pen(Brushes.White,2);



        public Drawing BuildDrawing()
        {

            DrawingGroup dg = new DrawingGroup();
            dg.Children.Add(GetGamePlayBackground());
            dg.Children.Add(GetBlocks());
            dg.Children.Add(GetUIBackground());
           
            dg.Children.Add(GetHero());
            dg.Children.Add(GetMonsters());
            if (!model.ChestIsOn)
            {
                dg.Children.Add(GetShop());
                dg.Children.Add(GetButtons());
            }
            else
            {
                dg.Children.Add(GetChest());
                dg.Children.Add(GetChestButtons());
            }
            
            
            return dg;
        }
        
        internal static BitmapImage GetImage(string fileName)
        {  

            
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();

            bmp.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream("FFYLife.Images." + fileName);

            bmp.EndInit();
            return bmp;
        }

        private Drawing GetMonsters()
        {
           

            DrawingGroup dg = new DrawingGroup();
            foreach (var monster in model.Monsters)
            {
                ImageDrawing monsterPic;
                if (model.CanAttack && monster.CX <= 195 )
                {
                    if (monster.MonsterLVL < 4)
                    {
                        monsterPic = new ImageDrawing(GetImage($"monster{monster.MonsterLVL}attack.png"), new Rect(monster.CX, model.Hero.CY, model.GameDisplayWidth / 5, model.GameDisplayHeight / 4));
                    }
                    else
                    {
                        monsterPic = new ImageDrawing(GetImage($"boss{(monster.MonsterLVL)/5}attack.png"), new Rect(monster.CX, model.Hero.CY, model.GameDisplayWidth / 5, model.GameDisplayHeight / 4));
                    }
                  
                }
                else
                {
                    if (monster.MonsterLVL < 4)
                    {
                        monsterPic = new ImageDrawing(GetImage($"monster{monster.MonsterLVL}.png"), new Rect(monster.CX, model.Hero.CY, model.GameDisplayWidth / 5, model.GameDisplayHeight / 4));
                    }
                    else
                    {
                        monsterPic = new ImageDrawing(GetImage($"boss{(monster.MonsterLVL) / 5}.png"), new Rect(monster.CX, model.Hero.CY, model.GameDisplayWidth / 5, model.GameDisplayHeight / 4));
                    }
                   
                }
                


                //  Geometry b = new RectangleGeometry(new Rect(block.CX, block.CY / 2 *3, model.GameDisplayWidth/5, model.GameDisplayHeight/4));

                dg.Children.Add(monsterPic);
            }

            if (model.ChestIsOn)
            {
                ImageDrawing chestpic = new ImageDrawing(GetImage($"chest.png"), new Rect(model.Chest.CX,  model.Chest.CY + 57, 200, 200));
                dg.Children.Add(chestpic);
            }


            /*

            if (oldMonsters == null || oldMonsterPosition.X != model.Monsters[0].CX )
            {
                GeometryGroup g = new GeometryGroup();
                foreach (var monster in model.Monsters)
                {
                    Geometry m = new RectangleGeometry(new Rect(monster.CX, monster.CY, 43, 200));

                    g.Children.Add(m);
                }
                oldMonsterPosition.X = model.Monsters[0].CX;
                oldMonsters = new GeometryDrawing(Brushes.Red,stroke, g);
            }

            return oldMonsters;*/
            return dg;
        }

        private Drawing GetChest()
        {
            DrawingGroup dg = new DrawingGroup();
            ImageDrawing background = new ImageDrawing(GetImage("questions.png"), new Rect(650, 400, 650, 400));

            FormattedText question = new FormattedText(this.model.Chest.Question.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 40, Brushes.Black);
            Geometry geo = question.BuildGeometry(new Point(760,500));
            GeometryDrawing gd = new GeometryDrawing(Brushes.White, null, geo);


            FormattedText answer0 = new FormattedText(this.model.Chest.Answers[0].ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 40, Brushes.Black);
            Geometry geo1 = answer0.BuildGeometry(new Point(730, 600));
            GeometryDrawing gd1 = new GeometryDrawing(Brushes.White, null, geo1);


            FormattedText answer1 = new FormattedText(this.model.Chest.Answers[1].ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 40, Brushes.Black);
            Geometry geo2 = answer1.BuildGeometry(new Point(1000, 600));
            GeometryDrawing gd2 = new GeometryDrawing(Brushes.White, null, geo2);

            FormattedText answer2 = new FormattedText(this.model.Chest.Answers[2].ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 40, Brushes.Black);
            Geometry geo3 = answer2.BuildGeometry(new Point(720, 685));
            GeometryDrawing gd3 = new GeometryDrawing(Brushes.White, null, geo3);

            FormattedText answer3 = new FormattedText(this.model.Chest.Answers[3].ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 40, Brushes.Black);
            Geometry geo4 = answer3.BuildGeometry(new Point(1010, 685));
            GeometryDrawing gd4 = new GeometryDrawing(Brushes.White, null, geo4);



            dg.Children.Add(background);
            dg.Children.Add(gd);
            dg.Children.Add(gd1);
            dg.Children.Add(gd2);
            dg.Children.Add(gd3);
            dg.Children.Add(gd4);
            return dg;
        }



        private Drawing GetHero()
        {

            ImageDrawing hero;
            if (model.Hero.IsDefending)
            {
                hero = new ImageDrawing(GetImage("steveDefend.png"), new Rect(model.Hero.CX + 50, model.Hero.CY, 200, 200));
            }
            else
            {
                if ( model.Moving)
                {
                    hero = new ImageDrawing(GetImage("steveMove.png"), new Rect(model.Hero.CX + 50, model.Hero.CY, 200, 200));
                    ;
                }
                else if (!model.Hero.CanAttack)
                {
                    hero = new ImageDrawing(GetImage("steve.png"), new Rect(model.Hero.CX + 50, model.Hero.CY, 100, 200));
                }
                else
                {
                    hero = new ImageDrawing(GetImage("steveattack.png"), new Rect(model.Hero.CX + 50, model.Hero.CY, 200, 200));
                }
            }
            
            

            //Geometry g = new RectangleGeometry(new Rect(model.Hero.CX, model.Hero.CY, 40, 200));
            //oldHero = new GeometryDrawing(Brushes.Yellow, stroke, g);

            oldHero = hero;
            return oldHero;
        }

        private Drawing GetBlocks()
        {
            if (oldBlocks == null)
            {
                DrawingGroup dg = new DrawingGroup();
                foreach (var block in model.Blocks)
                {

                    ImageDrawing blockPic = new ImageDrawing(GetImage("block.png"), new Rect(block.CX, block.CY / 2 * 3, model.GameDisplayWidth / 5, model.GameDisplayHeight / 4));


                  //  Geometry b = new RectangleGeometry(new Rect(block.CX, block.CY / 2 *3, model.GameDisplayWidth/5, model.GameDisplayHeight/4));

                     dg.Children.Add(blockPic);
                }

                oldBlocks = dg;
            }

            return oldBlocks;
        }

        private Drawing GetGamePlayBackground()
        {

            ;
            if (oldGameplayBackground == null)
            {
                
                oldGameplayBackground = new ImageDrawing(GetImage("ringBackground.gif"), new Rect(  0, 0, model.GameDisplayWidth, model.GameDisplayHeight));
                
                //Geometry g = new RectangleGeometry(new Rect(0,0,model.GameDisplayWidth,model.GameDisplayHeight));
                //oldGameplayBackground = new GeometryDrawing(Brushes.LightBlue , null , g);
            }

            return oldGameplayBackground;
        }





        private Drawing GetButtons()
        {
            DrawingGroup dg = new DrawingGroup();

            GeometryDrawing HPButn = new GeometryDrawing(Brushes.LightGray,stroke, new RectangleGeometry(new Rect(1150,530, 100, 50 )));

            FormattedText HPText = new FormattedText(model.HPPrice.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 50, Brushes.Black);
            HPText.TextAlignment = TextAlignment.Center;
            Geometry hpGeo = HPText.BuildGeometry(new Point(1200, 525));
            GeometryDrawing hpTextGeo = new GeometryDrawing(Brushes.Black, null, hpGeo);




            GeometryDrawing DMGButn = new GeometryDrawing(Brushes.LightGray, stroke, new RectangleGeometry(new Rect(825, 530, 100, 50)));

            FormattedText DmgText = new FormattedText(model.DmgPrice.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 50, Brushes.Black);
            DmgText.TextAlignment = TextAlignment.Center;
            Geometry dmgGeo = DmgText.BuildGeometry(new Point(875, 525));
            GeometryDrawing dmgTextGeo = new GeometryDrawing(Brushes.Black, null, dmgGeo);



            GeometryDrawing armorButn = new GeometryDrawing(Brushes.LightGray, stroke, new RectangleGeometry(new Rect(825, 730, 100, 50)));

            FormattedText ArmorText = new FormattedText(model.ArmorPrice.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 50, Brushes.Black);
            ArmorText.TextAlignment = TextAlignment.Center;
            Geometry armorGeo = ArmorText.BuildGeometry(new Point(875, 725));
            GeometryDrawing armorTextGeo = new GeometryDrawing(Brushes.Black, null, armorGeo);


            GeometryDrawing Menubtn = new GeometryDrawing(Brushes.LightGray, stroke, new RectangleGeometry(new Rect(1035, 650, 200, 100)));

            FormattedText MenuText = new FormattedText("MENU", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 50, Brushes.Black);
            MenuText.TextAlignment = TextAlignment.Center;
            Geometry MenuGeo = MenuText.BuildGeometry(new Point(1135, 670));
            GeometryDrawing menuTextGeo = new GeometryDrawing(Brushes.Black, null, MenuGeo);


            dg.Children.Add(Menubtn);
            dg.Children.Add(menuTextGeo);
            dg.Children.Add(armorButn);
            dg.Children.Add(armorTextGeo);
            dg.Children.Add(DMGButn);
            dg.Children.Add(dmgTextGeo);
            dg.Children.Add(HPButn);
            dg.Children.Add(hpTextGeo);

            return dg;

        }
        private Drawing GetChestButtons()
        {
            DrawingGroup dg = new DrawingGroup();

            GeometryDrawing AButn = new GeometryDrawing(Brushes.LightGray, stroke, new RectangleGeometry(new Rect(1200, 615, 30, 30)));
            
            FormattedText AText = new FormattedText("B", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 25, Brushes.Black);
            AText.TextAlignment = TextAlignment.Center;
            Geometry aGeo = AText.BuildGeometry(new Point(1215, 615));
            GeometryDrawing aTextGeo = new GeometryDrawing(Brushes.Black, null, aGeo);




            GeometryDrawing BButn = new GeometryDrawing(Brushes.LightGray, stroke, new RectangleGeometry(new Rect(890, 615, 30, 30)));

            FormattedText BText = new FormattedText("A", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 25, Brushes.Black);
            BText.TextAlignment = TextAlignment.Center;
            Geometry BGeo = BText.BuildGeometry(new Point(905, 615));
            GeometryDrawing BTextGeo = new GeometryDrawing(Brushes.Black, null, BGeo);



            GeometryDrawing CButn = new GeometryDrawing(Brushes.LightGray, stroke, new RectangleGeometry(new Rect(890, 700, 30, 30)));

            FormattedText CText = new FormattedText("C", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 25, Brushes.Black);
            CText.TextAlignment = TextAlignment.Center;
            Geometry cGeo = CText.BuildGeometry(new Point(905, 700));
            GeometryDrawing cTextGeo = new GeometryDrawing(Brushes.Black, null, cGeo);
            
            GeometryDrawing DButn = new GeometryDrawing(Brushes.LightGray, stroke, new RectangleGeometry(new Rect(1200, 700, 30, 30)));
            FormattedText DText = new FormattedText("D", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 25, Brushes.Black);
            DText.TextAlignment = TextAlignment.Center;
            Geometry DGeo = DText.BuildGeometry(new Point(1215, 700));
            GeometryDrawing DTextGeo = new GeometryDrawing(Brushes.Black, null, DGeo);


            dg.Children.Add(AButn);
            dg.Children.Add(aTextGeo);
            dg.Children.Add(BButn);
            dg.Children.Add(BTextGeo);
            dg.Children.Add(CButn);
            dg.Children.Add(cTextGeo);
            dg.Children.Add(DButn);
            dg.Children.Add(DTextGeo);

            return dg;

        }
        private Drawing GetShop()
        {
            //if (oldShop == null)
            //{
                DrawingGroup dg = new DrawingGroup();

            ////GeometryDrawing Background = new GeometryDrawing(Brushes.HotPink, stroke, new RectangleGeometry(new Rect(650, 400, model.GameDisplayWidth, model.GameDisplayHeight / 2)));
            //GeometryDrawing LeftRec = new GeometryDrawing(Brushes.HotPink, stroke, new RectangleGeometry(new Rect(650, 400, model.GameDisplayWidth / 2, model.GameDisplayHeight / 4)));
            //GeometryDrawing RightRec = new GeometryDrawing(Brushes.HotPink, stroke, new RectangleGeometry(new Rect(975, 600, model.GameDisplayWidth / 2, model.GameDisplayHeight / 4)));

            ImageDrawing background = new ImageDrawing(GetImage("shopBackground.jpg"), new Rect(650, 400, 650, 400));

            ImageDrawing anvilPic = new ImageDrawing(GetImage("anvil.png"), new Rect(650, 400, 170, 170));
                ImageDrawing potionPic = new ImageDrawing(GetImage("potion.png"), new Rect(950, 400, 170, 170));
                ImageDrawing armorPotionPic = new ImageDrawing(GetImage("armorPotion.png"), new Rect(650, 600, 170, 170));

                dg.Children.Add(background);
                //dg.Children.Add(LeftRec);
                //dg.Children.Add(RightRec);

                dg.Children.Add(GetButtons());
                dg.Children.Add(anvilPic);
                dg.Children.Add(potionPic);
                dg.Children.Add(armorPotionPic);


                oldShop = dg;
            //}

            return oldShop;
        }

        


        private Drawing GetUIBackground()
        {
           
           
                DrawingGroup dg = new DrawingGroup();

                //Geometry g = new RectangleGeometry(new Rect(650,0, gm.GameDisplayWidth, gm.GameDisplayHeight / 2));

                GeometryDrawing  Background = new GeometryDrawing(Brushes.Orange, stroke, new RectangleGeometry(new Rect(650, 0, model.GameDisplayWidth, model.GameDisplayHeight / 2)));
                GeometryDrawing LeftRec = new GeometryDrawing(Brushes.Orange, stroke, new RectangleGeometry(new Rect(650, 200, model.GameDisplayWidth / 2, model.GameDisplayHeight / 4)));
                GeometryDrawing RightRec = new GeometryDrawing(Brushes.Orange, stroke, new RectangleGeometry(new Rect(975, 0, model.GameDisplayWidth / 2, model.GameDisplayHeight / 4)));


                 //VBuck
                ImageDrawing VBuckPic = new ImageDrawing(GetImage("vbuck.png"), new Rect(1197, 50, 100, 100));

                FormattedText vbuckCount = new FormattedText(this.model.Hero.Cash.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 130 , Brushes.Black );
                Geometry geo = vbuckCount.BuildGeometry(new Point(985, 20));
                GeometryDrawing gd = new GeometryDrawing(Brushes.Black, stroke, geo);

                //HP
                ImageDrawing HPPic = new ImageDrawing(GetImage("hearth.png"), new Rect(800, 20, 130, 130)) ;

                FormattedText HPCount = new FormattedText(this.model.Hero.Hp.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 130, Brushes.Black);
                Geometry geo2 = HPCount.BuildGeometry(new Point(660, 20));
                GeometryDrawing gd2 = new GeometryDrawing(Brushes.Black, stroke, geo2);

            //DMG
            ImageDrawing DMGPic;
            if (this.model.Hero.AttackDMG <= 4 )
            {
                 DMGPic = new ImageDrawing(GetImage($"sword{this.model.Hero.AttackDMG}.png"), new Rect(800, 220, 130, 130));
            }
            else
            {
                 DMGPic = new ImageDrawing(GetImage($"sword5.png"), new Rect(800, 220, 130, 130));
            }
               

                FormattedText DMGCount = new FormattedText(this.model.Hero.AttackDMG.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 130, Brushes.Black);
                Geometry geo3 = DMGCount.BuildGeometry(new Point(660, 220));
                GeometryDrawing gd3 = new GeometryDrawing(Brushes.Black, stroke, geo3);


                //Armor
                ImageDrawing ArmmorPic = new ImageDrawing(GetImage("armor4.png"), new Rect(1125, 220, 130, 130));

                FormattedText ArmorCount = new FormattedText(this.model.Hero.Armor.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 130, Brushes.Black);
                Geometry geo4 = ArmorCount.BuildGeometry(new Point(985, 220));
                GeometryDrawing gd4 = new GeometryDrawing(Brushes.Black, stroke, geo4);
                
                FormattedText BlockCount = new FormattedText(this.model.BlockNumber.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 130, Brushes.Wheat);
                Geometry geo5 = BlockCount.BuildGeometry(new Point(450, 50));
                GeometryDrawing gd5 = new GeometryDrawing(Brushes.Black, Is, geo5);

                 FormattedText Name = new FormattedText(this.model.Name.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 80, Brushes.Wheat);
                 Geometry geo6 = Name.BuildGeometry(new Point(50, 50));
                 GeometryDrawing gd6 = new GeometryDrawing(Brushes.Black, Is, geo6);



            //enemy hp


            if (model.IsInFight)
            {
                GeometryDrawing EnemyHp = new GeometryDrawing(Brushes.LightGray, stroke, new RectangleGeometry(new Rect(220, 650, 70, 60)));



                FormattedText EHP = new FormattedText(this.model.Monsters[0].Hp.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 50, Brushes.Wheat);
                Geometry geo7 = EHP.BuildGeometry(new Point(240, 650));
                GeometryDrawing gd7 = new GeometryDrawing(Brushes.Black, stroke, geo7);
                dg.Children.Add(EnemyHp);
                dg.Children.Add(gd7);       
            }
           




                dg.Children.Add(Background);
                dg.Children.Add(LeftRec);
                dg.Children.Add(RightRec);
                dg.Children.Add(gd);
                dg.Children.Add(gd2);
                dg.Children.Add(gd3);
                dg.Children.Add(gd4);
                dg.Children.Add(gd5);
                dg.Children.Add(gd6);
          
           

            dg.Children.Add(HPPic);
                dg.Children.Add(DMGPic);
                dg.Children.Add(ArmmorPic);
                dg.Children.Add(VBuckPic);
                oldUI = dg;
            

           


             return oldUI;
        }




    }
}
