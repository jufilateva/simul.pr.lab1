using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VegetFarm
{
    public partial class Form1 : Form
    {
        private int day = 0;
        private int coins = 50;
        private int divide = 1;
        private int multiply = 1;

        Dictionary<CheckBox, Cell> field = new Dictionary<CheckBox, Cell>();
        public Form1()
        {
            InitializeComponent();
            foreach (CheckBox cb in tableLayoutPanel1.Controls)
                field[cb] = new Cell();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb.Checked)
            {
                Plant(cb);
                coins-=2;
                money.Text = "Coins: " + coins;
            }

            else
            {
                if (field[cb].state == CellState.Mature)
                {
                    Harvest(cb);
                    coins += 5;
                }
                if (field[cb].state == CellState.Immature)
                {
                    Harvest(cb);
                    coins += 3;
                }
                if (field[cb].state == CellState.Overgrew)
                {
                    Harvest(cb);
                    coins--; ;
                }
                money.Text = "Coins: " + coins;
            }
            

        }

        private void Plant(CheckBox cb)
        {
            field[cb].Plant();
            UpdateBox(cb);
        }

        private void Harvest (CheckBox cb)
        {
            field[cb].Harvest();
            UpdateBox(cb);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (CheckBox cb in tableLayoutPanel1.Controls)
                NextState(cb);
            day++;
            labDay.Text = "Day: "+day;
        }

        private void NextState(CheckBox cb)
        {
            field[cb].NextStep();
            UpdateBox(cb);
        }
        private void UpdateBox(CheckBox cb)
        {
            Color c = Color.White;
            switch (field[cb].state)
            {
                case CellState.Planted: c = Color.Black;
                        break;
                case CellState.Green: c = Color.Green;
                    break;
                case CellState.Immature: c = Color.Yellow;
                    break;
                case CellState.Mature: c = Color.Red;
                    break;
                case CellState.Overgrew: c = Color.Brown;
                    break;
                case CellState.Empty: c = Color.White;
                    break;

            }
            cb.BackColor = c;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void money_Click(object sender, EventArgs e)
        {

        }

        private void plus_Click(object sender, EventArgs e)
        {
            timer1.Interval -= 20;
        }

        private void minus_Click(object sender, EventArgs e)
        {
            timer1.Interval += 20;
        }
    }

    enum CellState
    {
        Empty,
        Planted,
        Green,
        Immature,
        Mature,
        Overgrew

    }

    class Cell
    {
        public CellState state = CellState.Empty;
        private int progress = 0;

        const int prPlanted = 20;
        const int prGreen = 100;
        const int prImmature = 120;
        const int prMature = 160;

        public void Plant()
        {
            state = CellState.Planted;
        }
        public void Harvest()
        {
            state = CellState.Empty;
        }
        public void NextStep()
        {
            if (state != CellState.Empty && state != CellState.Overgrew)
            {
                progress++;
                if ((progress == prPlanted) || (progress == prGreen) || (progress == prImmature) || (progress == prMature)) state++;
            }
        }

    }
}
