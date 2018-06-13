using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FactorioSolver
{
    public partial class Ui : Form, IUiInterface
    {
        Graphics g;

        public Ui()
        {
            InitializeComponent();

            // Create a graphics object.
            g = this.CreateGraphics();
        }

        public TextBox TextReport
        {
            get { return boxDebug; }
            set { boxDebug = value; }
        }
        public TextBox TextIngredient
        {
            get { return boxIngredient; }
            set { boxIngredient = value; }
        }

        public TextBox TextTotalPerSecond
        {
            get { return boxTotalPerSecond; }
            set { boxTotalPerSecond = value; }
        }


        public event Action ClickCalculate;
        public event Action ClickOptimizeBeltLoad;

        private void ButtonCalculate_Click(object sender, EventArgs e)
        {
            // Test drawing
            int gridSize = 32;

            // Draw furnace
            Image furnace = Image.FromFile("C:\\Users\\Alex\\source\\repos\\FactorioSolver\\Images\\source\\Electric_furnace.png");
            PointF pointUpperLeft = new PointF(drawingTopLeft.Location.X, drawingTopLeft.Location.Y);
            g.DrawImage(furnace, new PointF(pointUpperLeft.X, pointUpperLeft.Y + gridSize));

            // Draw count.
            Font drawFont = new System.Drawing.Font("Arial", 16);
            SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            g.DrawString("70", drawFont, drawBrush, pointUpperLeft);

            // Draw product
            Image ironPlate = Image.FromFile("C:\\Users\\Alex\\source\\repos\\FactorioSolver\\Images\\source\\Iron_plate.png");
            g.DrawImage(ironPlate, new PointF(pointUpperLeft.X, pointUpperLeft.Y + 2 * gridSize));

            ClickCalculate?.Invoke();
        }

        private void ButtonOptimizeBeltLoad_Click(object sender, EventArgs e)
        {
            ClickOptimizeBeltLoad?.Invoke();
        }

        private void Ui_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\n')
            {
                ClickCalculate?.Invoke();
            }
        }
    }
}
