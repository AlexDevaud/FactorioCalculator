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
        public Ui()
        {
            InitializeComponent();
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

        private void ButtonCalculate_Click(object sender, EventArgs e)
        {
            ClickCalculate?.Invoke();
        }
    }
}
