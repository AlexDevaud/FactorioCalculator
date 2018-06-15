using System;
using System.Collections.Generic;
using System.Drawing;
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
            // Normal code.
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
                    // Text display.
                    // Dispay text refinery needs.
                    DisplayRefineryStats(oilNeeds);

                    // Display all the text lists.
                    DisplayListOfFactories(ironFurnace);
                    DisplayListOfFactories(steelFurnace);
                    DisplayListOfFactories(copperFurnace);
                    DisplayListOfFactories(chemicalPlants);
                    DisplayListOfFactories(assemblingMachines);

                    view.TextReport.AppendText("\n");
                    view.TextReport.AppendText("The largest belt load is " + largestBeltLoad + " for " + largestBeltProduct);


                    // Graphical display.
                    // Calulate needs for graphical display.
                    GraphicalNeed rootNeed = new GraphicalNeed(product);

                    rootNeed = CalculateGraphicalNeeds(rootNeed, totalPerSecond, rootNeed);


                    DisplayGraphicalReport(rootNeed, ingredientsList, oilNeeds);

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
        /// Method to build the data objects that can be used to create a graphical report.
        /// The root need will need to be returned when done to build the graphical report from.
        /// </summary>
        /// <param name="thisNeed"></param>
        /// <param name="rootNeed"></param>
        private GraphicalNeed CalculateGraphicalNeeds(GraphicalNeed thisNeed, double count, GraphicalNeed rootNeed)
        {
            double factoriesNeeded = (1.0 * count * thisNeed.Product.TimeToProduce) / (thisNeed.Product.TotalCreated * thisNeed.Product.Producer.CraftSpeed);
            //IngredientStats ingredientStats = new IngredientStats(thisNeed.Product, factoriesNeeded, parentName);
            double beltLoad = 0;
            if (thisNeed.Product.Producer.UsesBelt)
            {
                beltLoad = 1.0 * thisNeed.Product.TotalCreated * thisNeed.Product.Producer.CraftSpeed * factoriesNeeded / thisNeed.Product.TimeToProduce;

                // Store the largest belt load.
                if (beltLoad > largestBeltLoad)
                {
                    largestBeltLoad = beltLoad;
                    largestBeltProduct = thisNeed.Product.Name;
                }
            }

            // Store stats for this item
            thisNeed.BeltLoad = beltLoad;
            thisNeed.RoundedFacs = (int)Math.Ceiling(factoriesNeeded);

            // Check for children to be reported on.
            if (thisNeed.Product.Ingredients.Count > 0)
            {
                foreach (Ingredient ingredient in thisNeed.Product.Ingredients)
                {
                    double newCost = 1.0 * (1.0 * ingredient.Amount * count) / thisNeed.Product.TotalCreated;

                    // Create a new stats object for each ingredient.
                    GraphicalNeed nextNeed = new GraphicalNeed(ingredient.Product);
                    thisNeed.ChildNeeds.Add(nextNeed);

                    CalculateGraphicalNeeds(nextNeed, newCost, rootNeed);

                    //ComputeFactoryCosts(ingredient.Product, newCost, ingredientsList, Math.Ceiling(factoriesNeeded) + " " + product.Name + " factories that feeds to " + parentName, oilNeeds);
                }
            }

            return rootNeed;
        }
        
        /// <summary>
        /// Returns the maximum depth of the dependency tree.
        /// 1 node = 1 depth.
        /// </summary>
        /// <param name="thisNeed"></param>
        /// <param name="thisDepth"></param>
        /// <param name="maxDepth"></param>
        /// <returns></returns>
        private int GetMaxDepthOfTree(GraphicalNeed thisNeed, int thisDepth, int maxDepth)
        {
            thisDepth++;

            foreach (GraphicalNeed childNeed in thisNeed.ChildNeeds)
            {
                maxDepth = GetMaxDepthOfTree(childNeed, thisDepth, maxDepth);
            }

            if (thisDepth > maxDepth)
            {
                maxDepth = thisDepth;
            }
            return maxDepth;
        }


        /// <summary>
        /// Returns the widths of all levels of tree.
        /// Finding the max of this needs to be done when it is built.
        /// 1 node = 1 width.
        /// </summary>
        /// <param name="thisNeed"></param>
        /// <param name="thisDepth"></param>
        /// <param name="depthWidths"></param>
        /// <returns></returns>
        private int[] GetWidthsOfTree(GraphicalNeed thisNeed, int thisDepth, int[] depthWidths)
        {
            /*
            view.TextReport.AppendText("At depth " + thisDepth + " .We find product " + thisNeed.Product.Name);
            view.TextReport.AppendText("\n");
            */
            depthWidths[thisDepth]++;
            thisDepth++;

            foreach (GraphicalNeed childNeed in thisNeed.ChildNeeds)
            {
                depthWidths = GetWidthsOfTree(childNeed, thisDepth, depthWidths);
            }

            return depthWidths;
        }


        /// <summary>
        /// Display a graphical report about what to build.
        /// </summary>
        /// <param name="ingredientStats"></param>
        /// <param name="oilNeeds"></param>
        private void DisplayGraphicalReport(GraphicalNeed rootNeed, List<IngredientStats> ingredientStats, OilNeeds oilNeeds)
        {
            view.G.Clear(Color.LightGray);

            // Get the max dimensions of the tree.
            int maxDepth = GetMaxDepthOfTree(rootNeed, 0, 0);
            int[] depthWidths = new int[maxDepth];

            depthWidths = GetWidthsOfTree(rootNeed, 0, depthWidths);

            int maxWidth = 0;
            int widestRow = 0;
            int largestColumnUsed = 0;

            for (int i = 0; i < depthWidths.Length; i++)
            {
                if (depthWidths[i] > maxWidth)
                {
                    maxWidth = depthWidths[i];
                    widestRow = i;
                }
            }

            // Draw with the proper data structure.
            DrawThisFacNeed(rootNeed, 0, widestRow, maxWidth, maxDepth, ref largestColumnUsed, new Point(0, 0));
        }

        /// <summary>
        /// Draws each factory needed
        /// Also call itself to draw its children.
        /// </summary>
        /// <param name="thisNeed"></param>
        /// <param name="widestRow"></param>
        /// <param name="largestColumnUsed"></param>
        private void DrawThisFacNeed(GraphicalNeed thisNeed, int thisDepth, int widestRow, int maxWidth, int maxDepth, ref int largestColumnUsed, Point parentPoint)
        {
            int graphicSize = 48;
            int horizontalSpace = 64;
            int verticalSpace = 192;
            int currentRowHeight = (maxDepth - thisDepth - 1) * verticalSpace + view.TopLeft.Location.Y;

            Image imageProduct = Image.FromFile(thisNeed.Product.ImageString);
            Image imageProducer = Image.FromFile(thisNeed.Product.Producer.ImageString);

            Font font = new System.Drawing.Font("Arial", 16);
            SolidBrush brush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

            int currentX = view.TopLeft.Location.X + largestColumnUsed * horizontalSpace;

            // Center amoung our children.
            if (thisNeed.ChildNeeds.Count > 0)
            {
                // We need to be centered horizontally among our children.
                currentX += (thisNeed.ChildNeeds.Count - 1) * horizontalSpace / 2;
            }

            // Check for root centering.
            if (thisDepth == 0)
            {
                currentX = view.TopLeft.Location.X + (maxWidth - 1) * horizontalSpace / 2;
            }

            PointF pointTop = new PointF(currentX, currentRowHeight + 20);
            PointF pointMid = new PointF(currentX, currentRowHeight + graphicSize);
            PointF pointBot = new PointF(currentX, currentRowHeight + 2 * graphicSize);

            view.G.DrawString("" + thisNeed.RoundedFacs, font, brush, pointTop);
            view.G.DrawImage(imageProducer, pointMid);
            view.G.DrawImage(imageProduct, pointBot);

            /*
            // Test line
            int centerX = currentX + (graphicSize / 2);
            Point startPoint = new Point(centerX, currentRowHeight + 20);
            Point endPoint = new Point(centerX, currentRowHeight + 3 * graphicSize);
            Pen pen = new Pen(brush);
            view.G.DrawLine(pen, startPoint, endPoint);
            */

            // Relationship lines.
            int centerX = currentX + (graphicSize / 2);
            Point newInPoint = new Point(centerX, currentRowHeight + 15);
            
            // Draw a relationship line if this is not the root.
            if (thisDepth > 0)
            {
                Pen pen = new Pen(brush);
                Point outPoint = new Point(centerX, currentRowHeight + 3 * graphicSize);
                view.G.DrawLine(pen, outPoint, parentPoint);
            }
            

            // Draw our children.
            foreach (GraphicalNeed childNeed in thisNeed.ChildNeeds)
            {
                DrawThisFacNeed(childNeed, thisDepth + 1, widestRow, maxWidth, maxDepth, ref largestColumnUsed, newInPoint);
            }

            // Update the widest row.
            if (thisDepth == widestRow)
            {
                largestColumnUsed++;
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
                            // 0.75 is the heavy to light exchange rate.
                            double totalLightPerCycle = lightOil.TotalCreated + (heavyOil.TotalCreated * 0.75);

                            // 40 is the heavy oil consumed per cycle. 3 is the time to crack light.
                            double heavyCrackPerRefinery = (heavyOil.TotalCreated * refineryCraftSpeed * 3.0) / (heavyOil.TimeToProduce * chemicalPlantCraftingSpeed * 40);

                            // 30 is light oil consumed per cycle. 3 is the time to crack light.
                            double lightCrackFacPerRefinery = (totalLightPerCycle * refineryCraftSpeed * 3.0) / (lightOil.TimeToProduce * chemicalPlantCraftingSpeed * 30);

                            // We can create 90 gas per cycle with cracking.
                            exactRefineriesNeeded = (oilNeeds.PetroleumGasNeeded * petroleumGas.TimeToProduce) / (90.0 * refineryCraftSpeed);
                            DisplayRefineriesNeeded(exactRefineriesNeeded, processNeeded);


                            roundedRefineriesNeeded = (int)Math.Ceiling(exactRefineriesNeeded);
                            int heavyCrackingPlants = (int)Math.Ceiling(roundedRefineriesNeeded * heavyCrackPerRefinery);
                            int lightCrackingPlants = (int)Math.Ceiling(roundedRefineriesNeeded * lightCrackFacPerRefinery);

                            view.TextReport.AppendText("Build " + heavyCrackingPlants + " chemical plants to crack heavy oil to light oil.");
                            view.TextReport.AppendText("\n");
                            view.TextReport.AppendText("\n");
                            view.TextReport.AppendText("Also build " + lightCrackingPlants + " chemical plants to crack light oil to pertroleum gas.");
                            view.TextReport.AppendText("\n");
                        }
                        else if (refineryProductNeeded == "Solid Fuel Ingredient")
                        {
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
                            double lightOilToSolidChemPlants = (lightOilPerRefineryCycle * refineryCraftSpeed * exactRefineriesNeeded * solidFuel.TimeToProduce) / (lightOil.TimeToProduce * 10 * chemicalPlantCraftingSpeed);
                            // 20 is the cost in gas to create a solid fuel
                            double gasToSolidChemPlants = (petroleumGas.TotalCreated * refineryCraftSpeed * exactRefineriesNeeded * solidFuel.TimeToProduce) / (petroleumGas.TimeToProduce * 20 * chemicalPlantCraftingSpeed);

                            double totalSolidChemPlants = lightOilToSolidChemPlants + gasToSolidChemPlants;

                            double refineriesPerHeavyCracking = 20.0 / 3;
                            int heavyCrackingPlants = (int)Math.Ceiling(1.0 * roundedRefineriesNeeded / (refineriesPerHeavyCracking * chemicalPlantCraftingSpeed));

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
                    view.TextReport.AppendText("Build " + stats.RoundedFactories + " " + stats.Ingredient.Producer.Name + "s for " + stats.Ingredient.Name + " that feeds to " + stats.ParentName);

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
                    ComputeFactoryCosts(ingredient.Product, newCost, ingredientsList, Math.Ceiling(factoriesNeeded) + " " + product.Name + " factories that feeds to " + parentName, oilNeeds);
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
