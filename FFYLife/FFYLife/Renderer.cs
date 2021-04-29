// <copyright file="Renderer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FFYLife
{
    using System.Globalization;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using GameModel.Models;

    /// <summary>
    /// Documentation of the public Renderer class.
    /// </summary>
    public class Renderer
    {
        private IGameModel model;
        private Drawing oldGameplayBackground;
        private Drawing oldUI;
        private Drawing oldShop;
        private Drawing oldBlocks;
        private Drawing oldHero;

        private Pen stroke = new Pen(Brushes.Black, 3);
        private Pen iss = new Pen(Brushes.White, 2);

        /// <summary>
        /// Initializes a new instance of the <see cref="Renderer"/> class.
        /// </summary>
        /// <param name="gmd">asd.</param>
        public Renderer(IGameModel gmd)
        {
            this.model = gmd;
        }

        /// <summary>
        /// builds one frame of the game.
        /// </summary>
        /// <returns> Drawing .</returns>
        public Drawing BuildDrawing()
        {
            DrawingGroup dg = new DrawingGroup();
            dg.Children.Add(this.GetGamePlayBackground());
            dg.Children.Add(this.GetBlocks());
            dg.Children.Add(this.GetUIBackground());

            dg.Children.Add(this.GetHero());
            dg.Children.Add(this.GetMonsters());
            if (!this.model.ChestIsOn)
            {
                dg.Children.Add(this.GetShop());
                dg.Children.Add(this.GetButtons());
            }
            else
            {
                dg.Children.Add(this.GetChest());
                dg.Children.Add(this.GetChestButtons());
            }

            return dg;
        }

        /// <summary>
        /// returns a picture.
        /// </summary>
        /// <param name="fileName">asd.</param>
        /// <returns> BitmapImage .</returns>
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
            foreach (var monster in this.model.Monsters)
            {
                ImageDrawing monsterPic;
                if (this.model.CanAttack && monster.CX <= 195)
                {
                    if (monster.MonsterLVL < 4)
                    {
                        monsterPic = new ImageDrawing(GetImage($"monster{monster.MonsterLVL}attack.png"), new Rect(monster.CX, this.model.Hero.CY, this.model.GameDisplayWidth / 5, this.model.GameDisplayHeight / 4));
                    }
                    else
                    {
                        monsterPic = new ImageDrawing(GetImage($"boss{monster.MonsterLVL / 5}attack.png"), new Rect(monster.CX, this.model.Hero.CY, this.model.GameDisplayWidth / 5, this.model.GameDisplayHeight / 4));
                    }
                }
                else
                {
                    if (monster.MonsterLVL < 4)
                    {
                        monsterPic = new ImageDrawing(GetImage($"monster{monster.MonsterLVL}.png"), new Rect(monster.CX, this.model.Hero.CY, this.model.GameDisplayWidth / 5, this.model.GameDisplayHeight / 4));
                    }
                    else
                    {
                        monsterPic = new ImageDrawing(GetImage($"boss{monster.MonsterLVL / 5}.png"), new Rect(monster.CX, this.model.Hero.CY, this.model.GameDisplayWidth / 5, this.model.GameDisplayHeight / 4));
                    }
                }

                // Geometry b = new RectangleGeometry(new Rect(block.CX, block.CY / 2 *3, model.GameDisplayWidth/5, model.GameDisplayHeight/4));
                dg.Children.Add(monsterPic);
            }

            if (this.model.ChestIsOn)
            {
                ImageDrawing chestpic = new ImageDrawing(GetImage($"chest.png"), new Rect(this.model.Chest.CX, this.model.Chest.CY + 57, 200, 200));
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

#pragma warning disable CS0618 // Type or member is obsolete
            FormattedText question = new FormattedText(this.model.Chest.Question.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 40, Brushes.Black);
#pragma warning restore CS0618 // Type or member is obsolete
            Geometry geo = question.BuildGeometry(new Point(760, 500));
            GeometryDrawing gd = new GeometryDrawing(Brushes.White, null, geo);

#pragma warning disable CS0618 // Type or member is obsolete
            FormattedText answer0 = new FormattedText(this.model.Chest.Answers[0].ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 40, Brushes.Black);
#pragma warning restore CS0618 // Type or member is obsolete
            Geometry geo1 = answer0.BuildGeometry(new Point(730, 600));
            GeometryDrawing gd1 = new GeometryDrawing(Brushes.White, null, geo1);

#pragma warning disable CS0618 // Type or member is obsolete
            FormattedText answer1 = new FormattedText(this.model.Chest.Answers[1].ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 40, Brushes.Black);
#pragma warning restore CS0618 // Type or member is obsolete
            Geometry geo2 = answer1.BuildGeometry(new Point(1000, 600));
            GeometryDrawing gd2 = new GeometryDrawing(Brushes.White, null, geo2);

#pragma warning disable CS0618 // Type or member is obsolete
            FormattedText answer2 = new FormattedText(this.model.Chest.Answers[2].ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 40, Brushes.Black);
#pragma warning restore CS0618 // Type or member is obsolete
            Geometry geo3 = answer2.BuildGeometry(new Point(720, 685));
            GeometryDrawing gd3 = new GeometryDrawing(Brushes.White, null, geo3);

#pragma warning disable CS0618 // Type or member is obsolete
            FormattedText answer3 = new FormattedText(this.model.Chest.Answers[3].ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 40, Brushes.Black);
#pragma warning restore CS0618 // Type or member is obsolete
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

            if (this.model.Hero.Type == "Light" || this.model.Hero.Type == null)
            {
                if (this.model.Hero.IsDefending)
                {
                    hero = new ImageDrawing(GetImage("steveDefend.png"), new Rect(this.model.Hero.CX + 50, this.model.Hero.CY, 200, 200));
                }
                else
                {
                    if (this.model.Moving)
                    {
                        hero = new ImageDrawing(GetImage("steveMove.png"), new Rect(this.model.Hero.CX + 50, this.model.Hero.CY, 200, 200));
                    }
                    else if (this.model.Hero.CanAttack)
                    {
                        hero = new ImageDrawing(GetImage("steve.png"), new Rect(this.model.Hero.CX + 50, this.model.Hero.CY, 100, 200));
                    }
                    else
                    {
                        hero = new ImageDrawing(GetImage("steveattack.png"), new Rect(this.model.Hero.CX + 50, this.model.Hero.CY, 200, 200));
                    }
                }
            }
            else
            {
                if (this.model.Hero.IsDefending)
                {
                    hero = new ImageDrawing(GetImage("steveDefend1.png"), new Rect(this.model.Hero.CX + 50, this.model.Hero.CY, 150, 200));
                }
                else
                {
                    if (this.model.Moving)
                    {
                        hero = new ImageDrawing(GetImage("steveMove1.png"), new Rect(this.model.Hero.CX + 50, this.model.Hero.CY, 200, 200));
                    }
                    else if (this.model.Hero.CanAttack)
                    {
                        hero = new ImageDrawing(GetImage("steve1.png"), new Rect(this.model.Hero.CX + 50, this.model.Hero.CY, 150, 200));
                    }
                    else
                    {
                        hero = new ImageDrawing(GetImage("steveattack1.png"), new Rect(this.model.Hero.CX + 50, this.model.Hero.CY, 160, 210));
                    }
                }
            }

            // Geometry g = new RectangleGeometry(new Rect(model.Hero.CX, model.Hero.CY, 40, 200));
            // oldHero = new GeometryDrawing(Brushes.Yellow, stroke, g);
            this.oldHero = hero;
            return this.oldHero;
        }

        private Drawing GetBlocks()
        {
            if (this.oldBlocks == null)
            {
                DrawingGroup dg = new DrawingGroup();
                foreach (var block in this.model.Blocks)
                {
                    ImageDrawing blockPic = new ImageDrawing(GetImage("block.png"), new Rect(block.CX, block.CY / 2 * 3, this.model.GameDisplayWidth / 5, this.model.GameDisplayHeight / 4));

                    // Geometry b = new RectangleGeometry(new Rect(block.CX, block.CY / 2 *3, model.GameDisplayWidth/5, model.GameDisplayHeight/4));
                    dg.Children.Add(blockPic);
                }

                this.oldBlocks = dg;
            }

            return this.oldBlocks;
        }

        private Drawing GetGamePlayBackground()
        {
            if (this.oldGameplayBackground == null)
            {
                this.oldGameplayBackground = new ImageDrawing(GetImage("ringBackground.gif"), new Rect(0, 0, this.model.GameDisplayWidth, this.model.GameDisplayHeight));

                // Geometry g = new RectangleGeometry(new Rect(0,0,model.GameDisplayWidth,model.GameDisplayHeight));
                // oldGameplayBackground = new GeometryDrawing(Brushes.LightBlue , null , g);
            }

            return this.oldGameplayBackground;
        }

        private Drawing GetButtons()
        {
            DrawingGroup dg = new DrawingGroup();

            GeometryDrawing hPButn = new GeometryDrawing(Brushes.LightGray, this.stroke, new RectangleGeometry(new Rect(1150, 530, 100, 50)));
#pragma warning disable CS0618 // Type or member is obsolete
            FormattedText hPText = new FormattedText(this.model.HPPrice.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 50, Brushes.Black);
#pragma warning restore CS0618 // Type or member is obsolete
            hPText.TextAlignment = TextAlignment.Center;
            Geometry hpGeo = hPText.BuildGeometry(new Point(1200, 525));
            GeometryDrawing hpTextGeo = new GeometryDrawing(Brushes.Black, null, hpGeo);

            GeometryDrawing dMGButn = new GeometryDrawing(Brushes.LightGray, this.stroke, new RectangleGeometry(new Rect(825, 530, 100, 50)));

#pragma warning disable CS0618 // Type or member is obsolete
            FormattedText dmgText = new FormattedText(this.model.DmgPrice.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 50, Brushes.Black);
#pragma warning restore CS0618 // Type or member is obsolete
            dmgText.TextAlignment = TextAlignment.Center;
            Geometry dmgGeo = dmgText.BuildGeometry(new Point(875, 525));
            GeometryDrawing dmgTextGeo = new GeometryDrawing(Brushes.Black, null, dmgGeo);

            GeometryDrawing armorButn = new GeometryDrawing(Brushes.LightGray, this.stroke, new RectangleGeometry(new Rect(825, 730, 100, 50)));

#pragma warning disable CS0618 // Type or member is obsolete
            FormattedText armorText = new FormattedText(this.model.ArmorPrice.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 50, Brushes.Black);
#pragma warning restore CS0618 // Type or member is obsolete
            armorText.TextAlignment = TextAlignment.Center;
            Geometry armorGeo = armorText.BuildGeometry(new Point(875, 725));
            GeometryDrawing armorTextGeo = new GeometryDrawing(Brushes.Black, null, armorGeo);

            GeometryDrawing menubtn = new GeometryDrawing(Brushes.LightGray, this.stroke, new RectangleGeometry(new Rect(1035, 650, 200, 100)));

#pragma warning disable CS0618 // Type or member is obsolete
            FormattedText menuText = new FormattedText("MENU", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 50, Brushes.Black);
#pragma warning restore CS0618 // Type or member is obsolete
            menuText.TextAlignment = TextAlignment.Center;
            Geometry menuGeo = menuText.BuildGeometry(new Point(1135, 670));
            GeometryDrawing menuTextGeo = new GeometryDrawing(Brushes.Black, null, menuGeo);

            dg.Children.Add(menubtn);
            dg.Children.Add(menuTextGeo);
            dg.Children.Add(armorButn);
            dg.Children.Add(armorTextGeo);
            dg.Children.Add(dMGButn);
            dg.Children.Add(dmgTextGeo);
            dg.Children.Add(hPButn);
            dg.Children.Add(hpTextGeo);

            return dg;
        }

        private Drawing GetChestButtons()
        {
            DrawingGroup dg = new DrawingGroup();

            GeometryDrawing aButn = new GeometryDrawing(Brushes.LightGray, this.stroke, new RectangleGeometry(new Rect(1200, 615, 30, 30)));

#pragma warning disable CS0618 // Type or member is obsolete
            FormattedText aText = new FormattedText("B", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 25, Brushes.Black);
#pragma warning restore CS0618 // Type or member is obsolete
            aText.TextAlignment = TextAlignment.Center;
            Geometry aGeo = aText.BuildGeometry(new Point(1215, 615));
            GeometryDrawing aTextGeo = new GeometryDrawing(Brushes.Black, null, aGeo);

            GeometryDrawing bButn = new GeometryDrawing(Brushes.LightGray, this.stroke, new RectangleGeometry(new Rect(890, 615, 30, 30)));

#pragma warning disable CS0618 // Type or member is obsolete
            FormattedText bText = new FormattedText("A", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 25, Brushes.Black);
#pragma warning restore CS0618 // Type or member is obsolete
            bText.TextAlignment = TextAlignment.Center;
            Geometry bGeo = bText.BuildGeometry(new Point(905, 615));
            GeometryDrawing bTextGeo = new GeometryDrawing(Brushes.Black, null, bGeo);

            GeometryDrawing cButn = new GeometryDrawing(Brushes.LightGray, this.stroke, new RectangleGeometry(new Rect(890, 700, 30, 30)));

#pragma warning disable CS0618 // Type or member is obsolete
            FormattedText cText = new FormattedText("C", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 25, Brushes.Black);
#pragma warning restore CS0618 // Type or member is obsolete
            cText.TextAlignment = TextAlignment.Center;
            Geometry cGeo = cText.BuildGeometry(new Point(905, 700));
            GeometryDrawing cTextGeo = new GeometryDrawing(Brushes.Black, null, cGeo);

            GeometryDrawing dButn = new GeometryDrawing(Brushes.LightGray, this.stroke, new RectangleGeometry(new Rect(1200, 700, 30, 30)));
#pragma warning disable CS0618 // Type or member is obsolete
            FormattedText dText = new FormattedText("D", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 25, Brushes.Black);
#pragma warning restore CS0618 // Type or member is obsolete
            dText.TextAlignment = TextAlignment.Center;
            Geometry dGeo = dText.BuildGeometry(new Point(1215, 700));
            GeometryDrawing dTextGeo = new GeometryDrawing(Brushes.Black, null, dGeo);

            dg.Children.Add(aButn);
            dg.Children.Add(aTextGeo);
            dg.Children.Add(bButn);
            dg.Children.Add(bTextGeo);
            dg.Children.Add(cButn);
            dg.Children.Add(cTextGeo);
            dg.Children.Add(dButn);
            dg.Children.Add(dTextGeo);

            return dg;
        }

        private Drawing GetShop()
        {
            // if (oldShop == null)
            // {
            DrawingGroup dg = new DrawingGroup();

            ////GeometryDrawing Background = new GeometryDrawing(Brushes.HotPink, stroke, new RectangleGeometry(new Rect(650, 400, model.GameDisplayWidth, model.GameDisplayHeight / 2)));
            // GeometryDrawing LeftRec = new GeometryDrawing(Brushes.HotPink, stroke, new RectangleGeometry(new Rect(650, 400, model.GameDisplayWidth / 2, model.GameDisplayHeight / 4)));
            // GeometryDrawing RightRec = new GeometryDrawing(Brushes.HotPink, stroke, new RectangleGeometry(new Rect(975, 600, model.GameDisplayWidth / 2, model.GameDisplayHeight / 4)));
            ImageDrawing background = new ImageDrawing(GetImage("shopBackground.jpg"), new Rect(650, 400, 650, 400));

            ImageDrawing anvilPic = new ImageDrawing(GetImage("anvil.png"), new Rect(650, 400, 170, 170));
            ImageDrawing potionPic = new ImageDrawing(GetImage("potion.png"), new Rect(950, 400, 170, 170));
            ImageDrawing armorPotionPic = new ImageDrawing(GetImage("armorPotion.png"), new Rect(650, 600, 170, 170));

            dg.Children.Add(background);
            dg.Children.Add(this.GetButtons());
            dg.Children.Add(anvilPic);
            dg.Children.Add(potionPic);
            dg.Children.Add(armorPotionPic);

            this.oldShop = dg;

            // }
            return this.oldShop;
        }

        private Drawing GetUIBackground()
        {
            DrawingGroup dg = new DrawingGroup();

            // Geometry g = new RectangleGeometry(new Rect(650,0, gm.GameDisplayWidth, gm.GameDisplayHeight / 2));
            GeometryDrawing background = new GeometryDrawing(Brushes.Orange, this.stroke, new RectangleGeometry(new Rect(650, 0, this.model.GameDisplayWidth, this.model.GameDisplayHeight / 2)));
            GeometryDrawing leftRec = new GeometryDrawing(Brushes.Orange, this.stroke, new RectangleGeometry(new Rect(650, 200, this.model.GameDisplayWidth / 2, this.model.GameDisplayHeight / 4)));
            GeometryDrawing rightRec = new GeometryDrawing(Brushes.Orange, this.stroke, new RectangleGeometry(new Rect(975, 0, this.model.GameDisplayWidth / 2, this.model.GameDisplayHeight / 4)));

            // VBuck
            ImageDrawing vBuckPic = new ImageDrawing(GetImage("vbuck.png"), new Rect(1197, 50, 100, 100));

#pragma warning disable CS0618 // Type or member is obsolete
            FormattedText vbuckCount = new FormattedText(this.model.Hero.Cash.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 130, Brushes.Black);
#pragma warning restore CS0618 // Type or member is obsolete
            Geometry geo = vbuckCount.BuildGeometry(new Point(985, 20));
            GeometryDrawing gd = new GeometryDrawing(Brushes.Black, this.stroke, geo);

            // HP
            ImageDrawing hPPic = new ImageDrawing(GetImage("hearth.png"), new Rect(800, 20, 130, 130));

#pragma warning disable CS0618 // Type or member is obsolete
            FormattedText hPCount = new FormattedText(this.model.Hero.Hp.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 130, Brushes.Black);
#pragma warning restore CS0618 // Type or member is obsolete
            Geometry geo2 = hPCount.BuildGeometry(new Point(660, 20));
            GeometryDrawing gd2 = new GeometryDrawing(Brushes.Black, this.stroke, geo2);

            // DMG
            ImageDrawing dMGPic;
            if (this.model.Hero.AttackDMG <= 4)
            {
                dMGPic = new ImageDrawing(GetImage($"sword{this.model.Hero.AttackDMG}.png"), new Rect(800, 220, 130, 130));
            }
            else
            {
                dMGPic = new ImageDrawing(GetImage($"sword5.png"), new Rect(800, 220, 130, 130));
            }

#pragma warning disable CS0618 // Type or member is obsolete
            FormattedText dMGCount = new FormattedText(this.model.Hero.AttackDMG.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 130, Brushes.Black);
#pragma warning restore CS0618 // Type or member is obsolete
            Geometry geo3 = dMGCount.BuildGeometry(new Point(660, 220));
            GeometryDrawing gd3 = new GeometryDrawing(Brushes.Black, this.stroke, geo3);

            // Armor
            ImageDrawing armmorPic = new ImageDrawing(GetImage("armor4.png"), new Rect(1125, 220, 130, 130));

#pragma warning disable CS0618 // Type or member is obsolete
            FormattedText armorCount = new FormattedText(this.model.Hero.Armor.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 130, Brushes.Black);
#pragma warning restore CS0618 // Type or member is obsolete
            Geometry geo4 = armorCount.BuildGeometry(new Point(985, 220));
            GeometryDrawing gd4 = new GeometryDrawing(Brushes.Black, this.stroke, geo4);

#pragma warning disable CS0618 // Type or member is obsolete
            FormattedText blockCount = new FormattedText(this.model.BlockNumber.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 130, Brushes.Wheat);
#pragma warning restore CS0618 // Type or member is obsolete
            Geometry geo5 = blockCount.BuildGeometry(new Point(450, 50));
            GeometryDrawing gd5 = new GeometryDrawing(Brushes.Black, this.iss, geo5);

#pragma warning disable CS0618 // Type or member is obsolete
            FormattedText name = new FormattedText(this.model.Name.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 80, Brushes.Wheat);
#pragma warning restore CS0618 // Type or member is obsolete
            Geometry geo6 = name.BuildGeometry(new Point(50, 50));
            GeometryDrawing gd6 = new GeometryDrawing(Brushes.Black, this.iss, geo6);

            // enemy hp
            if (this.model.IsInFight)
            {
                GeometryDrawing enemyHp = new GeometryDrawing(Brushes.LightGray, this.stroke, new RectangleGeometry(new Rect(220, 650, 70, 60)));

#pragma warning disable CS0618 // Type or member is obsolete
                FormattedText eHP = new FormattedText(this.model.Monsters[0].Hp.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 50, Brushes.Wheat);
#pragma warning restore CS0618 // Type or member is obsolete
                Geometry geo7 = eHP.BuildGeometry(new Point(240, 650));
                GeometryDrawing gd7 = new GeometryDrawing(Brushes.Black, this.stroke, geo7);
                dg.Children.Add(enemyHp);
                dg.Children.Add(gd7);
            }

            dg.Children.Add(background);
            dg.Children.Add(leftRec);
            dg.Children.Add(rightRec);
            dg.Children.Add(gd);
            dg.Children.Add(gd2);
            dg.Children.Add(gd3);
            dg.Children.Add(gd4);
            dg.Children.Add(gd5);
            dg.Children.Add(gd6);

            dg.Children.Add(hPPic);
            dg.Children.Add(dMGPic);
            dg.Children.Add(armmorPic);
            dg.Children.Add(vBuckPic);
            this.oldUI = dg;

            return this.oldUI;
        }
    }
}