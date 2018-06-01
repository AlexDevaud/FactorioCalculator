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
        private string largestBeltProduct;

        public Controller(IUiInterface uiInterface)
        {
            view = uiInterface;
            products = new AllProducts();
            products.CreateDefaultProducts();
            //craftSpeed = 2;
            largestBeltLoad = 0;
            largestBeltProduct = "";

            view.ClickCalculate += HandleCalculate;
            view.ClickOptimizeBeltLoad += HandleOptimizeBeltLoad;
        }

        /// <summary>
        /// Calculated the factories needed to produce the given item at the given rate.
        /// </summary>
        private void HandleCalculate()
        {
            view.TextReport.Text = "";
            largestBeltLoad = 0;

            if (products.Dictionary.TryGetValue(view.TextIngredient.Text, out Product product))
            {
                // Get the total needed per second.
                if (Double.TryParse(view.TextTotalPerSecond.Text, out double totalPerSecond))
                {
                    // Built a list of sets of factories we will need.
                    List<IngredientStats> ingredientsList = new List<IngredientStats>();
                    ComputeFactoryCosts(product, totalPerSecond, ingredientsList, "Root");

                    // Sort the factories list into sub lists.
                    List<IngredientStats> ironFurnace = new List<IngredientStats>();
                    List<IngredientStats> copperFurnace = new List<IngredientStats>();
                    List<IngredientStats> steelFurnace = new List<IngredientStats>();
                    List<IngredientStats> assemblingMachines = new List<IngredientStats>();
                    List<IngredientStats> chemicalPlants = new List<IngredientStats>();
                    List<IngredientStats> refineries = new List<IngredientStats>();

                    foreach (IngredientStats ingredientStats in ingredientsList)
                    {
                        switch (ingredientStats.Ingredient.Producer.Name)
                        {
                            case "Electric Furnace":
                                if (ingredientStats.Ingredient.Name == "Iron Plate")
                                {
                                    ironFurnace.Add(ingredientStats);
                                }
                                else if (ingredientStats.Ingredient.Name == "Copper Plate")
                                {
                                    copperFurnace.Add(ingredientStats);
                                }
                                else if (ingredientStats.Ingredient.Name == "Steel Plate")
                                {
                                    steelFurnace.Add(ingredientStats);
                                }
                                    break;
                            case "Assembling Machine":
                                assemblingMachines.Add(ingredientStats);
                                break;
                            case "Chemical Plant":
                                chemicalPlants.Add(ingredientStats);
                                break;
                            case "Basic Processing Oil Refinerie":
                                refineries.Add(ingredientStats);
                                break;
                            case "Advanced Processing Oil Refinerie":
                                refineries.Add(ingredientStats);
                                break;
                        }
                    }

                    // Display all the lists.
                    DisplayListOfFactories(ironFurnace);
                    DisplayListOfFactories(steelFurnace);
                    DisplayListOfFactories(copperFurnace);
                    DisplayListOfFactories(refineries);
                    DisplayListOfFactories(chemicalPlants);
                    DisplayListOfFactories(assemblingMachines);

                    view.TextReport.AppendText("\n");
                    view.TextReport.AppendText("The largest belt load is " + largestBeltLoad + " for " + largestBeltProduct);
                }
                else
                {
                    view.TextReport.Text = "Invalid count";
                }
            }
            else
            {
                view.TextReport.Text = "Item not found";
            }
            
        }

        /// <summary>
        /// Displays all the sets of factoires in a list. This will be all factories of a certain type.
        /// </summary>
        /// <param name="list"></param>
        private void DisplayListOfFactories(List<IngredientStats> list)
        {
            list.Reverse();

            foreach (IngredientStats stats in list)
            {
                if (view.TextReport.Text.Length > 0)
                {
                    view.TextReport.AppendText("\n");
                }
                view.TextReport.AppendText("Build " + stats.RoundedFactories + " " + stats.Ingredient.Producer.Name + "s for " + stats.Ingredient.Name + " that feeds to make " + stats.ParentName);
                if (stats.Ingredient.Name == "Petroleum Gas")
                {
                    double refineriesPerHeavyCracking = 25.0 / 3;
                    double refineriesPerLightCracking = 25.0 / 21;
                    int heavyCrackingPlants = (int)Math.Ceiling(stats.RoundedFactories / refineriesPerHeavyCracking);
                    int lightCrackingPlants = (int)Math.Ceiling(stats.RoundedFactories / refineriesPerLightCracking);
                    view.TextReport.AppendText("\n");
                    view.TextReport.AppendText("\n");
                    view.TextReport.AppendText("For the last set of refineries also build " + heavyCrackingPlants + " chemical plants to crack heavy oil to light oil.");
                    view.TextReport.AppendText("\n");
                    view.TextReport.AppendText("Also build " + lightCrackingPlants + " chemical plants to crack light oil to pertroleum gas.");
                }
                else if (stats.Ingredient.Name == "Heavy Oil")
                {
                    double refineriesPerLightToSolid = 2.25;
                    double refineriesPerGasToSolid = 1.5;
                    int lightToSolidPlants = (int)Math.Ceiling(stats.RoundedFactories * refineriesPerLightToSolid);
                    int gasToSolidPlants = (int)Math.Ceiling(stats.RoundedFactories * refineriesPerGasToSolid);
                    view.TextReport.AppendText("\n");
                    view.TextReport.AppendText("\n");
                    view.TextReport.AppendText("For the last set of refineries also build " + lightToSolidPlants + " chemical plants to turn excess light oil into solid fuel.");
                    view.TextReport.AppendText("\n");
                    view.TextReport.AppendText("Also build " + gasToSolidPlants + " chemical plants to turn the excess petroleum gas to solid fuel.");
                }
                if (stats.BeltLoad > 0)
                {
                    string beltLoadString = "" + stats.BeltLoad;
                    if (beltLoadString.Length > 6)
                    {
                        beltLoadString = beltLoadString.Substring(0, 6);
                    }

                    view.TextReport.AppendText(". This will create a belt load of " + beltLoadString);
                }
                view.TextReport.AppendText("\n");
            }
            

           
        }

        /// <summary>
        /// Count is the amount of this item we need to produce at the given rate.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="count"></param>
        private List<IngredientStats> ComputeFactoryCosts(Product product, double count, List<IngredientStats> ingredientsList, string parentName)
        {
            
            double factoriesNeeded = (1.0 * count * product.TimeToProduce) / (1.0 * product.TotalCreated * product.Producer.CraftSpeed);
            IngredientStats ingredientStats = new IngredientStats(product, factoriesNeeded, parentName);
            double beltLoad = 0;
            if (product.Producer.UsesBelt)
            {
                beltLoad = 1.0 * product.TotalCreated * product.Producer.CraftSpeed * ingredientStats.ExactFactories / product.TimeToProduce;

                // Store the largest belt load.
                if (beltLoad > largestBeltLoad)
                {
                    largestBeltLoad = beltLoad;
                    largestBeltProduct = product.Name;
                }
            }
            ingredientStats.BeltLoad = beltLoad;
            ingredientsList.Add(ingredientStats);

            // Check for children to be reported on.
            if (product.Ingredients.Count > 0)
            {
                foreach (Ingredient ingredient in product.Ingredients)
                {
                    double newCost = 1.0 * (1.0 * ingredient.Amount * count) / product.TotalCreated;
                    ComputeFactoryCosts(ingredient.Product, newCost, ingredientsList, product.Name + " that feeds to make " + parentName);
                }
            }

            return ingredientsList;
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
