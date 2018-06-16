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

        // Drawing interface
        public Graphics G
        {
            get { return g; }
        }

        public TextBox TopLeftMain
        {
            get { return drawingMainTopLeft; }
        }

        public TextBox TopLeftRefinery
        {
            get { return drawingRefineryTopLeft; }
        }

        public Label StringSize
        {
            get { return textSizeCheck; }
        }

        public event Action ClickCalculate;
        public event Action ClickOptimizeBeltLoad;

        private void ButtonCalculate_Click(object sender, EventArgs e)
        {
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
