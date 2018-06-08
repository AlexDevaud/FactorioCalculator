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
                    OilNeeds oilNeeds = new OilNeeds();
                    ComputeFactoryCosts(product, totalPerSecond, ingredientsList, "Root", oilNeeds);

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
                    // Dispay refinery needs.
                    DisplayRefineryStats(oilNeeds);

                    // Display all the lists.
                    DisplayListOfFactories(ironFurnace);
                    DisplayListOfFactories(steelFurnace);
                    DisplayListOfFactories(copperFurnace);
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
        /// Calculates and displays data related to needs from oil refineries.
        /// </summary>
        /// <param name="oilNeeds"></param>
        private void DisplayRefineryStats(OilNeeds oilNeeds)
        {
            // Deal with refinery needs.
            // Should we use basic refining or advanced refining?
            double advancedNeeds = oilNeeds.SolidFuelIngredientsNeeded + oilNeeds.LightOilNeeded + oilNeeds.PetroleumGasNeeded;
            double basicNeeds = oilNeeds.HeavyOilNeeded;
            double refineryCraftSpeed = 1;
            double chemicalPlantCraftingSpeed = 1.25;
            string processNeeded = "";
            double exactRefineriesNeeded = 0;
            int roundedRefineriesNeeded = 0;

            // We may not need oil.
            if (advancedNeeds + basicNeeds > 0)
            {
                products.Dictionary.TryGetValue("Heavy Oil", out Product heavyOil);
                products.Dictionary.TryGetValue("Light Oil", out Product lightOil);
                products.Dictionary.TryGetValue("Petroleum Gas", out Product petroleumGas);
                products.Dictionary.TryGetValue("Solid Fuel", out Product solidFuel);

                // What type of processing should we use?
                if (advancedNeeds > basicNeeds)
                {
                    // Use advanced oil processing.
                    processNeeded = "Advanced";
                    heavyOil.TotalCreated = 10;
                    lightOil.TotalCreated = 45;
                    petroleumGas.TotalCreated = 55;

                }
                else
                {
                    // Use basic oil processing.
                    processNeeded = "Basic";
                    heavyOil.TotalCreated = 30;
                    lightOil.TotalCreated = 30;
                    petroleumGas.TotalCreated = 40;
                }

                // Get the needed refineries.
                // What if we only need one refinery product.
                if (advancedNeeds == 0)
                {
                    // We only need heavy oil.
                    exactRefineriesNeeded = (1.0 * oilNeeds.HeavyOilNeeded * heavyOil.TimeToProduce) / (heavyOil.TotalCreated * refineryCraftSpeed);
                    DisplayRefineriesNeeded(exactRefineriesNeeded, processNeeded);

                    double refineriesPerLightToSolid = 1.8;
                    double refineriesPerGasToSolid = 1.2;
                    roundedRefineriesNeeded = (int)Math.Ceiling(exactRefineriesNeeded);
                    int lightToSolidPlants = (int)Math.Ceiling(roundedRefineriesNeeded * refineriesPerLightToSolid);
                    int gasToSolidPlants = (int)Math.Ceiling(roundedRefineriesNeeded * refineriesPerGasToSolid);
                    view.TextReport.AppendText("Build " + lightToSolidPlants + " chemical plants to turn excess light oil into solid fuel.");
                    view.TextReport.AppendText("\n");
                    view.TextReport.AppendText("\n");
                    view.TextReport.AppendText("Also build " + gasToSolidPlants + " chemical plants to turn the excess petroleum gas to solid fuel.");
                    view.TextReport.AppendText("\n");
                    view.TextReport.AppendText("\n");
                }
                else
                {
                    // Do we need more than one type of oil?
                    int refineryProductsNeeded = 0;
                    string refineryProductNeeded = "";

                    if (oilNeeds.HeavyOilNeeded > 0)
                    {
                        refineryProductNeeded = "Heavy Oil";
                        refineryProductsNeeded++;
                    }
                    if (oilNeeds.LightOilNeeded > 0)
                    {
                        refineryProductNeeded = "Light Oil";
                        refineryProductsNeeded++;
                    }
                    if (oilNeeds.PetroleumGasNeeded > 0)
                    {
                        refineryProductNeeded = "Petroleum Gas";
                        refineryProductsNeeded++;
                    }
                    if (oilNeeds.SolidFuelIngredientsNeeded > 0)
                    {
                        refineryProductNeeded = "Solid Fuel Ingredient";
                        refineryProductsNeeded++;
                    }

                    if (refineryProductsNeeded == 1)
                    {
                        // We only need one refinery product, and it is not heavy oil.
                        if (refineryProductNeeded == "Light Oil")
                        {
                            // Heavy oil can be cracked. Petroleum gas will have to be discarded as solid fuel.

                        }
                        else if (refineryProductNeeded == "Petroleum Gas")
                        {
                            // We can crack the 45 petroleum gas to 90 total every cycles.
                            // With 5 seconds per cycle we produce 18 gas per second.
                            exactRefineriesNeeded = (1.0 * oilNeeds.PetroleumGasNeeded * petroleumGas.TimeToProduce) / (18 * refineryCraftSpeed);
                            DisplayRefineriesNeeded(exactRefineriesNeeded, processNeeded);

                            double refineriesPerHeavyCracking = 20.0 / 3;
                            double refineriesPerLightCracking = 20.0 / 21;
                            roundedRefineriesNeeded = (int)Math.Ceiling(exactRefineriesNeeded);
                            int heavyCrackingPlants = (int)Math.Ceiling(1.0 * roundedRefineriesNeeded / (refineriesPerHeavyCracking * chemicalPlantCraftingSpeed));
                            int lightCrackingPlants = (int)Math.Ceiling(1.0 * roundedRefineriesNeeded / (refineriesPerLightCracking * chemicalPlantCraftingSpeed));

                            view.TextReport.AppendText("Build " + heavyCrackingPlants + " chemical plants to crack heavy oil to light oil.");
                            view.TextReport.AppendText("\n");
                            view.TextReport.AppendText("\n");
                            view.TextReport.AppendText("Also build " + lightCrackingPlants + " chemical plants to crack light oil to pertroleum gas.");
                            view.TextReport.AppendText("\n");
                            view.TextReport.AppendText("\n");
                        }
                        else if (refineryProductNeeded == "Solid Fuel Ingredient")
                        {
                            // Get refinery needs.


                            // 0.75 is the exhage rate of heavy to light oil.
                            double totalSolidCreatedPerCycle = (lightOil.TotalCreated / 10.0 + petroleumGas.TotalCreated / 20.0 + (heavyOil.TotalCreated * 0.75) / 10);
                            exactRefineriesNeeded = (oilNeeds.SolidFuelIngredientsNeeded * lightOil.TimeToProduce) / (totalSolidCreatedPerCycle * refineryCraftSpeed);
                            DisplayRefineriesNeeded(exactRefineriesNeeded, processNeeded);

                            // 1.6 is the amount of solid fuel that can be craeted at a refinery per second at default craft speeds.
                            //exactRefineriesNeeded = (oilNeeds.SolidFuelIngredientsNeeded * lightOil.TimeToProduce) / (1.6 * refineryCraftSpeed);
                            roundedRefineriesNeeded = (int)Math.Ceiling(exactRefineriesNeeded);

                            // We should crack heavy to light oil then make solid fuel with all the light oil and petroleum gas.
                            // Calculating chem plants to be used to create solid fuel.
                            // 0.75 the exchange rate of heavy to light oil with cracking.
                            double lightOilPerRefineryCycle = heavyOil.TotalCreated * 0.75 + lightOil.TotalCreated;
                            // 10 is the cost in light oil to make a solid fuel.
                            double lightOilToSolidChemPlants = (lightOilPerRefineryCycle * refineryCraftSpeed * roundedRefineriesNeeded * solidFuel.TimeToProduce) / (lightOil.TimeToProduce * 10 * chemicalPlantCraftingSpeed);
                            // 20 is the cost in gas to create a solid fuel
                            double gasToSolidChemPlants = (petroleumGas.TotalCreated * refineryCraftSpeed * roundedRefineriesNeeded * solidFuel.TimeToProduce) / (petroleumGas.TimeToProduce * 20 * chemicalPlantCraftingSpeed);

                            double totalSolidChemPlants = lightOilToSolidChemPlants + gasToSolidChemPlants;

                            double refineriesPerHeavyCracking = 20.0 / 3;
                            int heavyCrackingPlants = (int)Math.Ceiling(1.0 * roundedRefineriesNeeded / (refineriesPerHeavyCracking * chemicalPlantCraftingSpeed));

                            /*
                            // Assume we can created 1 solid fuel with each ingredient.
                            // We can make 1.6 solid fuel ingredients per second with 1 refinery.
                            exactRefineriesNeeded = (1.0 * oilNeeds.SolidFuelIngredientsNeeded * solidFuelIngredient.TimeToProduce) / (1.6 * refineryCraftSpeed);
                            
                            roundedRefineriesNeeded = (int)Math.Ceiling(exactRefineriesNeeded);
                            

                            // Need to display the chemical plants creating from light and plants creating from gas.
                            // Need to not display the chemical plants using this made up ingredient.

                            int lightToSolidFacs = (int)Math.Ceiling(roundedRefineriesNeeded * 3.9375);
                            int gasToSolidFacs = (int)Math.Ceiling(roundedRefineriesNeeded * 2.0625);
                            */
                            view.TextReport.AppendText("Build " + heavyCrackingPlants + " chemical plants to crack heavy oil to light oil.");
                            view.TextReport.AppendText("\n");
                            view.TextReport.AppendText("\n");
                            view.TextReport.AppendText("Build " + (int)Math.Ceiling(lightOilToSolidChemPlants) + " chemical plants to create solid fuel from light oil.");
                            view.TextReport.AppendText("\n");
                            view.TextReport.AppendText("\n");
                            view.TextReport.AppendText("Build " + (int)Math.Ceiling(gasToSolidChemPlants) + " chemical plants to create solid fuel from petroleum oil.");
                            view.TextReport.AppendText("\n");
                        }

                    }

                }
                // What is the most needed ingredient?
                // To calculate mixed refinery needed.


            }
        }

        /// <summary>
        /// Displays the total refineries needed.
        /// This is its own method so it can be show before type specific chemical plant needs.
        /// </summary>
        /// <param name="exactRefineries"></param>
        /// <param name="processNeeded"></param>
        private void DisplayRefineriesNeeded(double exactRefineries, string processNeeded)
        {
            view.TextReport.AppendText("Build " + (int)Math.Ceiling(exactRefineries) + " refineries and set them to " + processNeeded + " Oil Processing.");
            view.TextReport.AppendText("\n");
            view.TextReport.AppendText("\n");
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
                if (stats.Ingredient.Name != "Solid Fuel")
                {
                    // Check if the report table is not empty.
                    if (view.TextReport.Text.Length > 0)
                    {
                        view.TextReport.AppendText("\n");
                    }
                    view.TextReport.AppendText("Build " + stats.RoundedFactories + " " + stats.Ingredient.Producer.Name + "s for " + stats.Ingredient.Name + " that feeds to make " + stats.ParentName);

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
               
                /*
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
                */
               
            }
            

           
        }

        /// <summary>
        /// Traverses the chain of ingredients recursively. Builds a list of stats about how to build the required ingredients. Also builds a data object to track the oil needs separately.
        /// Count is the amount of this item we need to produce at the given rate.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="count"></param>
        private List<IngredientStats> ComputeFactoryCosts(Product product, double count, List<IngredientStats> ingredientsList, string parentName, OilNeeds oilNeeds)
        {
            
            double factoriesNeeded = (1.0 * count * product.TimeToProduce) / (product.TotalCreated * product.Producer.CraftSpeed);
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
            // Calculating oil refinery needs is handled at the end. This stores refinery product needs.
            else if (product.Name == "Light Oil" || product.Name == "Heavy Oil" || product.Name == "Petroleum Gas" || product.Name == "Solid Fuel Ingredient")
            {
                // Ingredients needed per second.
                double ingredientsNeeded = count;
                if (product.Name == "Light Oil")
                {
                    oilNeeds.LightOilNeeded += ingredientsNeeded;
                }
                else if (product.Name == "Heavy Oil")
                {
                    oilNeeds.HeavyOilNeeded += ingredientsNeeded;
                }
                else if (product.Name == "Petroleum Gas")
                {
                    oilNeeds.PetroleumGasNeeded += ingredientsNeeded;
                }
                else if (product.Name == "Solid Fuel Ingredient")
                {
                    oilNeeds.SolidFuelIngredientsNeeded += ingredientsNeeded;
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
                    ComputeFactoryCosts(ingredient.Product, newCost, ingredientsList, product.Name + " that feeds to make " + parentName, oilNeeds);
                }
            }

            return ingredientsList;
            }

        /// <summary>
        /// Adjust the belt load to have a max of 40 load.
        /// </summary>

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
