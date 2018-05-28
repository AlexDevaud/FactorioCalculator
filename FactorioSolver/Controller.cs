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
        private double largestBeltLoad;

        public Controller(IUiInterface uiInterface)
        {
            view = uiInterface;
            products = new AllProducts();
            products.CreateDefaultProducts();
            craftSpeed = 2;
            largestBeltLoad = 0;

            view.ClickCalculate += HandleCalculate;
            view.ClickOptimizeBeltLoad += HandleOptimizeBeltLoad;
        }


        public void HandleCalculate()
        {
            view.TextReport.Text = "";
            largestBeltLoad = 0;

            if (products.Dictionary.TryGetValue(view.TextIngredient.Text, out Product product))
            {
                // Get the total needed per second.
                if (Double.TryParse(view.TextTotalPerSecond.Text, out double totalPerSecond))
                {
                    PrintFactoryCosts(product, totalPerSecond);

                    view.TextReport.AppendText("\n");
                    view.TextReport.AppendText("\n");
                    view.TextReport.AppendText("The largest belt load is " + largestBeltLoad);
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

        public void PrintFactoryCosts(Product product, double count)
        {
            // Cost of this item.
            double factoriesNeeded = (1.0 * count * product.TimeToProduce) / (1.0 * product.TotalCreated * craftSpeed);
            
            if (view.TextReport.Text.Length > 0)
            {
                view.TextReport.AppendText("\n");
            }
            view.TextReport.AppendText("Factories for " + product.Name + " = " + factoriesNeeded);

            // Report on the load it will place on the belt.
            if (product.UsesBelt)
            {
                double beltLoad = 1.0 * product.TotalCreated * craftSpeed * factoriesNeeded / product.TimeToProduce;
                view.TextReport.AppendText(" Belt load = " + beltLoad);

                // Store the largest belt load.
                if (beltLoad > largestBeltLoad)
                {
                    largestBeltLoad = beltLoad;
                }

            }

            if (product.Ingredients.Count > 0)
            {
                foreach (Ingredient ingredient in product.Ingredients)
                {
                    PrintFactoryCosts(ingredient.Product, (ingredient.Amount * count));
                }
            }
        }

        public void HandleOptimizeBeltLoad()
        {
            view.TextTotalPerSecond.Text = "" + 1;
            HandleCalculate();

            double optimalRate = 40.0 / largestBeltLoad;
            view.TextTotalPerSecond.Text = "" + optimalRate;

            HandleCalculate();
        }
    }
}
