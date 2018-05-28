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
        //private double craftSpeed;
        private double largestBeltLoad;

        public Controller(IUiInterface uiInterface)
        {
            view = uiInterface;
            products = new AllProducts();
            products.CreateDefaultProducts();
            //craftSpeed = 2;
            largestBeltLoad = 0;

            view.ClickCalculate += HandleCalculate;
            view.ClickOptimizeBeltLoad += HandleOptimizeBeltLoad;
        }


        private void HandleCalculate()
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

        /// <summary>
        /// Count is the amount of this item we need to produce at the given rate.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="count"></param>
        private void PrintFactoryCosts(Product product, double count)
        {
            // Cost of this item.
            double factoriesNeeded = (1.0 * count * product.TimeToProduce) / (1.0 * product.TotalCreated * product.Producer.CraftSpeed);
            string factoriesNeededString = TrimDoubleLength(factoriesNeeded);
            
            if (view.TextReport.Text.Length > 0)
            {
                view.TextReport.AppendText("\n");
            }
            view.TextReport.AppendText(product.Producer.Name + " for " + product.Name + " = " + factoriesNeededString);

            // Report on the load it will place on the belt.
            if (product.Producer.UsesBelt)
            {
                double beltLoad = 1.0 * product.TotalCreated * product.Producer.CraftSpeed * factoriesNeeded / product.TimeToProduce;
                string beltLoadString = TrimDoubleLength(beltLoad);
                view.TextReport.AppendText(" Belt load = " + beltLoadString);

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
                    PrintFactoryCosts(ingredient.Product, (1.0 * ingredient.Amount * count) / product.TotalCreated);
                }
            }
        }

        private void HandleOptimizeBeltLoad()
        {
            view.TextTotalPerSecond.Text = "" + 1;
            HandleCalculate();

            double optimalRate = 40.0 / largestBeltLoad;
            view.TextTotalPerSecond.Text = "" + optimalRate;

            HandleCalculate();
        }

        /// <summary>
        /// Trims the length of a double.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string TrimDoubleLength(double value)
        {
            string valueString = "" + value;
            if (valueString.Length > 5)
            {
                valueString = valueString.Substring(0, 5);
            }
            return valueString;
        }
    }
}
