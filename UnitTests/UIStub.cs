using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FactorioSolver;
using System.Windows.Forms;

namespace UnitTests
{
    public class UIStub : IUiInterface
    {
        public event Action ClickCalculate;
        public event Action ClickOptimizeBeltLoad;

        private bool checkSplitBelts = true;
        private Graphics g;
        private TextBox textErrors = new TextBox();
        private TextBox textIngredient = new TextBox();
        private TextBox textTotalPerSecond = new TextBox();
        private TextBox textMiningProductivity = new TextBox();
        private TextBox topLeftMain = new TextBox();
        private TextBox topLeftRefinery = new TextBox();
        private TextBox topLeftMining = new TextBox();
        private Label stringSize = new Label();

        public TextBox TextErrors
        {
            get { return textErrors; }
            set { textErrors = value; }
        }

        public TextBox TextIngredient
        {
            get { return textIngredient; }
            set { textIngredient = value; }
        }

        public TextBox TextTotalPerSecond
        {
            get { return textTotalPerSecond; }
            set { textTotalPerSecond = value; }
        }

        public TextBox TextMiningProductivity
        {
            get { return textMiningProductivity; }
            set { textMiningProductivity = value; }
        }

        public bool CheckBoxSplitBelts
        {
            get { return checkSplitBelts; }
            set { checkSplitBelts = value; }
        }

        public Graphics G
        {
            get { return g; }
        }

        public TextBox TopLeftMain
        {
            get { return TopLeftMain; }
            set { TopLeftMain = value; }
        }

        public TextBox TopLeftRefinery
        {
            get { return TopLeftRefinery; }
            set { TopLeftRefinery = value; }
        }

        public TextBox TopLeftMining
        {
            get { return TopLeftMining; }
            set { TopLeftMining = value; }
        }

        public Label StringSize
        {
            get { return stringSize; }
            set { stringSize = value; }
        }

        public bool Testing
        {
            get { return true; }
        }

        // Methods to click buttons.
        public void FireCalculate()
        {
            ClickCalculate?.Invoke();
        }

        public void FireOptimize()
        {
            ClickOptimizeBeltLoad?.Invoke();
        }

     

        // Constructor
        public UIStub()
        {
            g = null;
        }
    }
}
