﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepScore
{
    internal class Box : TextBox
    {
        Label spareImgLabel = new Label();
        Label strikeImgLabel = new Label();

        Image spareImg = Image.FromFile("C:/Users/Aaron/source/repos/KeepScore/KeepScore/img/Spare.png");
        Image strikeImg = Image.FromFile("C:/Users/Aaron/source/repos/KeepScore/KeepScore/img/Strike.png");

        public int baseScore;
        public int boxTotal;
        public Boolean isStrike;
        public Boolean isSpare;
        public int markLoad;
        public int boxNumber;

        public Box(int baseScore, int markLoad, int boxTotal, bool isStrike, bool isSpare)
        {
            this.baseScore = baseScore;
            this.markLoad = markLoad;
            this.boxTotal = boxTotal;
            this.isStrike = isStrike;
            this.isSpare = isSpare;

            this.Size = new Size(40,35);
        }

        public Box(int in_boxNumber)
        {
            this.isStrike=false;
            this.isSpare=false;
            this.baseScore = 0;
            this.boxTotal = 0;
            this.markLoad = 0;
            this.boxNumber = in_boxNumber;

            this.Size = new Size(40,35);
            this.TextAlign = HorizontalAlignment.Center;
            this.Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold, GraphicsUnit.Point);

            spareImgLabel.Image = spareImg;
            spareImgLabel.AutoSize = false;
            spareImgLabel.Size = new Size(7, 7);
            spareImgLabel.ImageAlign = ContentAlignment.MiddleCenter;
            spareImgLabel.Text = "";
            spareImgLabel.BackColor = Color.Transparent;
            spareImgLabel.Parent = this;
            spareImgLabel.Location = new Point(0,0);

            strikeImgLabel.Image = strikeImg;
            strikeImgLabel.AutoSize = false;
            strikeImgLabel.Size = new Size(8, 8);
            strikeImgLabel.ImageAlign = ContentAlignment.MiddleCenter;
            strikeImgLabel.Text = "";
            strikeImgLabel.BackColor = Color.Transparent;
            strikeImgLabel.Parent = this;
            strikeImgLabel.Location = new Point(28, 11);

            this.spareImgLabel.Hide();
            this.strikeImgLabel.Hide();
        }

        public void setBaseScore(string in_score)
        {
            if (in_score.ToUpper() == "R")
            {
                this.baseScore = 0;
                isStrike = false;
                isSpare = false;
                this.spareImgLabel.Hide();
                this.strikeImgLabel.Hide();
                this.Text = "";
                this.Focus();
            }
            else 
            { 
                if (!isStrike && !isSpare)
                {
                    switch (in_score.ToUpper())
                    {
                        case "X":
                            this.baseScore = 10;
                            isStrike = true;
                            isSpare = false;
                            this.spareImgLabel.Show();
                            this.strikeImgLabel.Show();
                            this.Text = "";
                            this.Focus();
                            break;
                        case "/":
                            this.baseScore = 10;
                            isStrike = false;
                            isSpare = true;
                            this.spareImgLabel.Show();
                            this.Text = "";
                            this.Focus();
                            break;
                        default:
                            try
                            {
                                this.baseScore = int.Parse(in_score);

                                if (this.baseScore > 10)
                                {
                                    this.baseScore = 0;
                                    this.Text = "";
                                    this.Focus();
                                    MessageBox.Show("Please enter a number that is 10 or less.", "Score Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            catch (Exception e)
                            {
                                this.baseScore = 0;
                                this.Text = "";
                                MessageBox.Show("Please enter a number, an X or a /.", "Score Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Focus();
                            }
                            isStrike = false;
                            isSpare = false;
                            break;
                    }
                }
                else
                {
                    try
                    {
                        if (in_score.ToUpper() == "X" || in_score.ToUpper() == "/")
                        {
                            in_score = "10";
                        }

                        if (int.Parse(in_score) < 11 && ((this.isStrike && this.markLoad < 20) || (this.isSpare && this.markLoad < 10)))
                        {
                            this.markLoad += int.Parse(in_score);
                            this.Text = markLoad.ToString();
                        }
                        else
                        {
                            this.markLoad = 0;
                            this.Text = "";
                            this.Focus();
                        }
                    }
                    catch (Exception e)
                    {
                        this.baseScore = 0;
                        MessageBox.Show("Please enter a number, an X or a /.", "Score Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Focus();
                    }
                }
            }

            this.calcBoxTotal();
        }

        public void calcBoxTotal() {

            if (this.isSpare || this.isStrike)
            {
                this.boxTotal = 10 + this.markLoad;
            }
            else { 
                    this.boxTotal = this.baseScore;
            }
        }

        public void Box_TextChanged(object sender, EventArgs e)
        {
            if (this.Text != "")
            {
                this.setBaseScore(this.Text);
            }
        }

    }
}