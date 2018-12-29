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

        public TextBox TextErrors
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

        public ComboBox ItemsBox
        {
            get { return itemsBox; }
            set { itemsBox = value; } 
        }

        // Not being used.
        public TextBox TextMiningProductivity
        {
            get { return boxMiningProductivity; }
            set { boxMiningProductivity = value; }
        }

        public bool CheckBoxSplitBelts
        {
            get { return checkBoxBeltSplit.Checked; }
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

        public TextBox TopLeftMining
        {
            get { return drawingMiningNeeds; }
        }

        public Label StringSize
        {
            get { return textSizeCheck; }
        }

        public bool Testing
        {
            get { return false; }
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

        private void Ui_SizeChanged(object sender, EventArgs e)
        {
            ClickCalculate?.Invoke();
        }
    }
}
