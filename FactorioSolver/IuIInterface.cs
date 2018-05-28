using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FactorioSolver
{
    interface IUiInterface
    {
        TextBox TextReport { get; set; }
        TextBox TextIngredient { get; set; }
        TextBox TextTotalPerSecond { get; set; }

        event Action ClickCalculate;
    }
}
