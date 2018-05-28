using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioSolver
{
    class Controller
    {
        private AllProducts products;
        private IUiInterface view;
        private double craftSpeed;

        public Controller(IUiInterface uiInterface)
        {
            view = uiInterface;
            products = new AllProducts();
            products.CreateDefaultProducts();
            craftSpeed = 2;

            view.ClickCalculate += HandleCalculate;

        }


        public void HandleCalculate()
        {
            view.TextReport.Text = "";

            if (products.Dictionary.TryGetValue(view.TextIngredient.Text, out Product product))
            {
                /*
                int itemsPerSecond = 2;
                double factoriesNeeded = (itemsPerSecond * product.TimeToProduce) / (product.TotalCreated * craftSpeed);
                view.Debug = "Factories for " + product.Name + " = " + factoriesNeeded;
                */

                // Get the total needed per second.
                if (Int32.TryParse(view.TextTotalPerSecond.Text, out int totalPerSecond))
                {
                    PrintFactoryCosts(product, totalPerSecond);
                }
                else
                {
                    view.TextReport.Text = "Invalid count";
                }
            }
            else
            {
                view.TextReport.Text = "Not found";
            }
            
        }

        public void PrintFactoryCosts(Product product, int count)
        {
            // Cost of this item.
            double factoriesNeeded = (count * product.TimeToProduce) / (product.TotalCreated * craftSpeed);
            if (view.TextReport.Text.Length > 0)
            {
                view.TextReport.AppendText("\n");
            }
            view.TextReport.AppendText("Factories for " + product.Name + " = " + factoriesNeeded);

            if (product.Ingredients.Count > 0)
            {
                foreach (Ingredient ingredient in product.Ingredients)
                {
                    PrintFactoryCosts(ingredient.Product, ingredient.Amount);
                }
            }
        }
    }
}
