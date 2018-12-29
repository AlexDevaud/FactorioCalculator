using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioSolver
{
    public class Controller
    {
        private AllProducts products;
        private IUiInterface view;
        //private double craftSpeed;
        private double largestBeltLoad;
        private string largestBeltProduct;


        // Contants
        private int horizontalSpace = 80;
        private int verticalSpace = 300;

        public Controller(IUiInterface uiInterface)
        {
            view = uiInterface;
            products = new AllProducts();
            products.CreateDefaultProducts();
            largestBeltLoad = 0;
            largestBeltProduct = "";

            view.ClickCalculate += HandleCalculate;
            view.ClickOptimizeBeltLoad += HandleOptimizeBeltLoad;

            // Display items in the box.
            foreach (Product product in products.Products)
            {
                if (product.Ingredients.Count > 0)
                {
                    view.ItemsBox.Items.Add(product.Name);
                }
            }
        }

        /// <summary>
        /// Calculated the factories needed to produce the given item at the given rate.
        /// </summary>
        private void HandleCalculate()
        {
            view.TextErrors.Text = "";
            largestBeltLoad = 0;

            //if (products.Dictionary.TryGetValue(view.TextIngredient.Text, out Product product))
            if (products.Dictionary.TryGetValue(view.ItemsBox.Text, out Product product))
            {
                // Get the total needed per second.
                if (Double.TryParse(view.TextTotalPerSecond.Text, out double totalPerSecond))
                {
                    // Graphical display.
                    GraphicalNeed rootNeed = new GraphicalNeed(product);
                    MiningNeeds miningNeeds = new MiningNeeds();
                    OilNeeds oilNeeds = new OilNeeds();
                    rootNeed = CalculateGraphicalNeeds(rootNeed, totalPerSecond, rootNeed, miningNeeds, oilNeeds);

                    // Redraw the tree.
                    if (view.Testing == false)
                    {
                        view.G.Clear(Color.White);
                        DisplayGraphicalReport(rootNeed, oilNeeds, miningNeeds);
                    }
                }
                else
                {
                    view.TextErrors.Text = "Invalid count";
                }
            }
            else
            {
                view.TextErrors.Text = "Item not found";
            }
        }

        /// <summary>
        /// Method to build the data objects that can be used to create a graphical report.
        /// The root need will need to be returned when done to build the graphical report from.
        /// </summary>
        /// <param name="thisNeed"></param>
        /// <param name="rootNeed"></param>
        private GraphicalNeed CalculateGraphicalNeeds(GraphicalNeed thisNeed, double count, GraphicalNeed rootNeed, MiningNeeds miningNeeds, OilNeeds oilNeeds)
        {
            double factoriesNeeded = (1.0 * count * thisNeed.Product.TimeToProduce) / (thisNeed.Product.TotalCreated * thisNeed.Product.Producer.CraftSpeed);
            double beltLoad = 0;
            const double maxBeltLoad = 40;
            const double roundingError = 0.0000000000001;
            if (thisNeed.Product.Producer.UsesBelt)
            {
                beltLoad = 1.0 * thisNeed.Product.TotalCreated * thisNeed.Product.Producer.CraftSpeed * factoriesNeeded / thisNeed.Product.TimeToProduce;

                // Check for weird rounding when optimizing for 40.
                if (beltLoad > maxBeltLoad && beltLoad < maxBeltLoad + roundingError)
                {
                    beltLoad = maxBeltLoad;
                    factoriesNeeded -= roundingError;
                    count -= roundingError;
                }

                // Split belts if selected.
                if (view.CheckBoxSplitBelts && beltLoad > maxBeltLoad) 
                {
                    // Re calulate the needs of each set.
                    int setsOfBuildings = (int)Math.Ceiling(beltLoad / maxBeltLoad);
                    factoriesNeeded /= setsOfBuildings;
                    beltLoad /= setsOfBuildings;
                    count /= setsOfBuildings;
                    thisNeed.Copies *= setsOfBuildings;

                    // Make our parent reference this building multiple times. Then it will be drawn multiple times.
                    for (int i = 1; i < setsOfBuildings; i++)
                    {
                        thisNeed.Parent.ChildNeeds.Add(thisNeed);
                    }
                }

                // Round up belt load slightly.
                if (Math.Abs(beltLoad - Math.Ceiling(beltLoad)) <= roundingError)
                {
                    beltLoad = Math.Ceiling(beltLoad);
                }

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

                    // Don't add mining or refinery needs to the main list.
                    // This child is also assumed to be the last item in the chain.
                    if (ingredient.Product.Producer.MainList)
                    {
                        // Create a new stats object for each ingredient.
                        GraphicalNeed nextNeed = new GraphicalNeed(ingredient.Product);
                        nextNeed.Parent = thisNeed;
                        nextNeed.Copies = thisNeed.Copies;
                        thisNeed.ChildNeeds.Add(nextNeed);

                        // Create stats for this child need.
                        CalculateGraphicalNeeds(nextNeed, newCost, rootNeed, miningNeeds, oilNeeds);
                    }
                    else if (ingredient.Product.Producer.Name == "Electric Mining Drill")
                    {
                        // Total mining costs need to know if this was split for belts.
                        newCost *= thisNeed.Copies;

                        if (ingredient.Product.Name == "Iron Ore")
                        {
                            miningNeeds.IronOre += newCost;
                        }
                        else if (ingredient.Product.Name == "Copper Ore")
                        {
                            miningNeeds.CopperOre += newCost;
                        }
                        else if (ingredient.Product.Name == "Coal")
                        {
                            miningNeeds.Coal += newCost;
                        }
                        else if (ingredient.Product.Name == "Stone")
                        {
                            miningNeeds.Stone += newCost;
                        }
                    }
                    else if (ingredient.Product.Producer.Name == "Oil Refinery")
                    {
                        // Total mining costs need to know if this was split for belts.
                        newCost *= thisNeed.Copies;

                        if (ingredient.Product.Name == "Light Oil")
                        {
                            oilNeeds.LightOilNeeded += newCost;
                        }
                        else if (ingredient.Product.Name == "Heavy Oil")
                        {
                            oilNeeds.HeavyOilNeeded += newCost;
                        }
                        else if (ingredient.Product.Name == "Petroleum Gas")
                        {
                            oilNeeds.PetroleumGasNeeded += newCost;
                        }
                        else if (ingredient.Product.Name == "Solid Fuel Ingredient")
                        {
                            oilNeeds.SolidFuelIngredientsNeeded += newCost;
                        }
                    }

                }
            }
            return rootNeed;
        }
        
        


        /// <summary>
        /// Display a graphical report about what to build.
        /// </summary>
        /// <param name="ingredientStats"></param>
        /// <param name="oilNeeds"></param>
        private void DisplayGraphicalReport(GraphicalNeed rootNeed, OilNeeds oilNeeds, MiningNeeds miningNeeds)
        {
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

            // Draw with the main tree
            DrawThisFacNeed(rootNeed, 0, maxDepth, ref largestColumnUsed, new Point(0, 0), view.TopLeftMain.Location);

            // Draw oil needs
            DisplayRefineryStats(oilNeeds, ref largestColumnUsed, maxDepth);

            // Calculate needs for mining Drills.
            // Also drawing them
            // Get the mining productivity
            Int32.TryParse(view.TextMiningProductivity.Text, out int miningLevel);

            double miningBoost = 1 + 0.02 * miningLevel;

            if (miningNeeds.IronOre > 0)
            {
                products.Dictionary.TryGetValue("Iron Ore", out Product thisOre);
                double orePerSecond = miningNeeds.IronOre;
                int neededDrills = (int)Math.Ceiling((orePerSecond / miningBoost) * thisOre.TimeToProduce);

                GraphicalNeed thisNeed = new GraphicalNeed(thisOre);
                thisNeed.BeltLoad = 0;
                thisNeed.RoundedFacs = neededDrills;
                DrawThisFacNeed(thisNeed, 0, maxDepth, ref largestColumnUsed, new Point(0, 0), view.TopLeftMain.Location);
            }
            if (miningNeeds.CopperOre > 0)
            {
                products.Dictionary.TryGetValue("Copper Ore", out Product thisOre);
                double orePerSecond = miningNeeds.CopperOre;
                int neededDrills = (int)Math.Ceiling((orePerSecond / miningBoost) * thisOre.TimeToProduce);

                GraphicalNeed thisNeed = new GraphicalNeed(thisOre);
                thisNeed.BeltLoad = 0;
                thisNeed.RoundedFacs = neededDrills;
                DrawThisFacNeed(thisNeed, 0, maxDepth, ref largestColumnUsed, new Point(0, 0), view.TopLeftMain.Location);
            }
            if (miningNeeds.Coal > 0)
            {
                products.Dictionary.TryGetValue("Coal", out Product thisOre);
                double orePerSecond = miningNeeds.Coal;
                int neededDrills = (int)Math.Ceiling((orePerSecond / miningBoost) * thisOre.TimeToProduce);

                GraphicalNeed thisNeed = new GraphicalNeed(thisOre);
                thisNeed.BeltLoad = 0;
                thisNeed.RoundedFacs = neededDrills;
                DrawThisFacNeed(thisNeed, 0, maxDepth, ref largestColumnUsed, new Point(0, 0), view.TopLeftMain.Location);
            }
            if (miningNeeds.Stone > 0)
            {
                products.Dictionary.TryGetValue("Stone", out Product thisOre);
                double orePerSecond = miningNeeds.Coal;
                int neededDrills = (int)Math.Ceiling((orePerSecond / miningBoost) * thisOre.TimeToProduce);

                GraphicalNeed thisNeed = new GraphicalNeed(thisOre);
                thisNeed.BeltLoad = 0;
                thisNeed.RoundedFacs = neededDrills;
                DrawThisFacNeed(thisNeed, 0, maxDepth, ref largestColumnUsed, new Point(0, 0), view.TopLeftMain.Location);
            }

            // Resize the window.
            int windowWidth = (largestColumnUsed + 1) * horizontalSpace + view.TopLeftMain.Location.X;
            Ui.ActiveForm.Width = windowWidth;

            int windowHeight = maxDepth * verticalSpace;
            Ui.ActiveForm.Height = windowHeight;
        }



        /// <summary>
        /// Draws the given factory. Also call itself to draw its children.
        /// </summary>
        /// <param name="thisNeed"></param>
        /// <param name="thisDepth"></param>
        /// <param name="widestRow"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxDepth"></param>
        /// <param name="largestColumnUsed"></param>
        /// <param name="parentPoint"></param> If 0, 0 assumes this is the root.
        private void DrawThisFacNeed(GraphicalNeed thisNeed, int thisDepth, int maxDepth, ref int largestColumnUsed, Point parentPoint, Point topLeftPoint)
        {
            // Solid fuel is a special weird case that is drawn by refinery needs.
            if (thisNeed.Product.Name != "Solid Fuel")
            {
                // Tracking where to draw in the scence.
                int graphicSpacing = 44;
                int imageDimension = 32;
                
                
                int nextY = (maxDepth - thisDepth - 1) * verticalSpace + topLeftPoint.Y;
                int topY = nextY;
                const int spacingLabel = 18;
                const int spacingRoundedFacs = 38;
                const int spacingBeltLoad = 26;

                // Needed images.
                Image imageProduct = Image.FromFile(thisNeed.Product.ImageString);
                Image imageProducer = Image.FromFile(thisNeed.Product.Producer.ImageString);

                Font fontFacs = new Font("Arial", 30);
                Font fontLabel = new Font("Arial", 12);
                Font fontBeltLoad = new Font("Arial", 18);
                SolidBrush brush = new SolidBrush(Color.Black);

                int currentLeftX = topLeftPoint.X + largestColumnUsed * horizontalSpace;

                // Center among all our children.
                if (thisNeed.ChildNeeds.Count > 0)
                {
                    int maxRowWidth = GetMaxWidthOfTree(thisNeed);
                    currentLeftX += (maxRowWidth - 1) * horizontalSpace / 2;
                }

                // Draw all the data for this set of buildings.
                string nextString = "Build";
                PointF nextPoint = new PointF(currentLeftX + CenterXStringOffset(nextString, fontLabel, horizontalSpace), nextY);
                view.G.DrawString(nextString, fontLabel, brush, nextPoint);
                nextY += spacingLabel;

                nextString = "" + thisNeed.RoundedFacs;
                nextPoint = new PointF(currentLeftX + CenterXStringOffset(nextString, fontFacs, horizontalSpace), nextY);
                view.G.DrawString(nextString, fontFacs, brush, nextPoint);
                nextY += spacingRoundedFacs;

                nextPoint = new PointF(currentLeftX + CenterXImageOffset(imageDimension, horizontalSpace), nextY);
                view.G.DrawImage(imageProducer, nextPoint);
                nextY += graphicSpacing;

                nextString = "for";
                nextPoint = new PointF(currentLeftX + CenterXStringOffset(nextString, fontLabel, horizontalSpace), nextY);
                view.G.DrawString(nextString, fontLabel, brush, nextPoint);
                nextY += spacingLabel;

                nextPoint = new PointF(currentLeftX + CenterXImageOffset(imageDimension, horizontalSpace), nextY);
                view.G.DrawImage(imageProduct, nextPoint);
                nextY += graphicSpacing;

                // Drawing belt load info.
                if (thisNeed.BeltLoad > 0)
                {
                    nextString = "Belt load:";
                    nextPoint = new PointF(currentLeftX + CenterXStringOffset(nextString, fontLabel, horizontalSpace), nextY);
                    view.G.DrawString(nextString, fontLabel, brush, nextPoint);
                    nextY += spacingLabel;

                    nextString = "" + TrimDoubleLength(thisNeed.BeltLoad);
                    nextPoint = new PointF(currentLeftX + CenterXStringOffset(nextString, fontBeltLoad, horizontalSpace), nextY);
                    view.G.DrawString(nextString, fontBeltLoad, brush, nextPoint);
                    nextY += spacingBeltLoad;
                }


                // Relationship lines.
                int centerX = currentLeftX + (imageDimension / 2);

                // Draw a relationship line if this is not the root.
                // This is needed to pass even if this is the root.
                nextPoint = new PointF(currentLeftX + horizontalSpace / 2, nextY);
                if (thisDepth > 0)
                {
                    Pen pen = new Pen(brush, 2);
                    //view.G.DrawLine(pen, nextPoint, parentPoint);

                    // Draw an extra line if this doesn't have a belt load.
                    if (thisNeed.BeltLoad == 0)
                    {
                        //This extra point extends the line to where the top normally is.
                        Point extraPoint = new Point((int)nextPoint.X, (int)(nextPoint.Y + spacingLabel + spacingBeltLoad + 1));

                        // Giving a slight overlap
                        extraPoint = new Point((int)extraPoint.X, (int)(extraPoint.Y - 1));
                        view.G.DrawLine(pen, nextPoint, extraPoint);
                        view.G.DrawLine(pen, extraPoint, parentPoint);
                    }
                    else
                    {
                        view.G.DrawLine(pen, nextPoint, parentPoint);
                    }
                }


                // Draw our children.
                Point newInPoint = new Point((int)nextPoint.X, topY - 2);
                foreach (GraphicalNeed childNeed in thisNeed.ChildNeeds)
                {
                    DrawThisFacNeed(childNeed, thisDepth + 1, maxDepth, ref largestColumnUsed, newInPoint, topLeftPoint);

                }
            }
            // Move to the next column.
            if (thisNeed.ChildNeeds.Count == 0)
            {
                largestColumnUsed++;
            }
        }

        /// <summary>
        /// Calculates and displays data related to needs from oil refineries.
        /// </summary>
        /// <param name="oilNeeds"></param>
        private void DisplayRefineryStats(OilNeeds oilNeeds, ref int largestColumnUsed, int maxDepth)
        {
            // Deal with refinery needs.
            // Should we use basic refining or advanced refining?
            const double refineryCraftSpeed = 1;
            const double chemicalPlantCraftingSpeed = 1.25;
            double exactRefineriesNeeded = 0;
            int roundedRefineriesNeeded = 0;

            // Get references to oil processing data.
            products.Dictionary.TryGetValue("Heavy Oil", out Product heavyOil);
            products.Dictionary.TryGetValue("Light Oil", out Product lightOil);
            products.Dictionary.TryGetValue("Petroleum Gas", out Product petroleumGas);
            products.Dictionary.TryGetValue("Solid Fuel", out Product solidFuel);

            products.Dictionary.TryGetValue("Advanced Oil Processing", out Product advacedOilProcessing);
            products.Dictionary.TryGetValue("Basic Oil Processing", out Product basicOilProcessing);

            products.Dictionary.TryGetValue("Heavy Oil Cracking", out Product heavyCracking);
            products.Dictionary.TryGetValue("Light Oil Cracking", out Product lightCracking);

            products.Dictionary.TryGetValue("Solid Fuel From Light Oil", out Product solidFuelFromLight);
            products.Dictionary.TryGetValue("Solid Fuel From Petroleum Gas", out Product solidFuelFromGas);

            // Max depth may be too low to not cut off the top.
            if (maxDepth < 3)
            {
                maxDepth = 3;
            }

            // Just make a tree for each oil type needed.
            // Light oil is never directly needed.
            if (oilNeeds.HeavyOilNeeded > 0)
            {
                // Heavy oil is  needed
                exactRefineriesNeeded = (1.0 * oilNeeds.HeavyOilNeeded * heavyOil.TimeToProduce) / (heavyOil.TotalCreated * refineryCraftSpeed);

                const double refineriesPerLightToSolid = 1.8;
                const double refineriesPerGasToSolid = 1.2;
                roundedRefineriesNeeded = (int)Math.Ceiling(exactRefineriesNeeded);
                int lightToSolidPlants = (int)Math.Ceiling(roundedRefineriesNeeded * refineriesPerLightToSolid);
                int gasToSolidPlants = (int)Math.Ceiling(roundedRefineriesNeeded * refineriesPerGasToSolid);


                // Graphical report.
                GraphicalNeed needSolidPlantsFromLight = new GraphicalNeed(solidFuelFromLight);
                needSolidPlantsFromLight.RoundedFacs = lightToSolidPlants;

                GraphicalNeed needSolidPlantsFromGas = new GraphicalNeed(solidFuelFromGas);
                needSolidPlantsFromGas.RoundedFacs = gasToSolidPlants;

                GraphicalNeed needBasicProcessing = new GraphicalNeed(basicOilProcessing);
                needBasicProcessing.RoundedFacs = roundedRefineriesNeeded;

                // Establish relationship between oil needs.
                needBasicProcessing.ChildNeeds.Add(needSolidPlantsFromLight);
                needBasicProcessing.ChildNeeds.Add(needSolidPlantsFromGas);

                DrawThisFacNeed(needBasicProcessing, 0, maxDepth, ref largestColumnUsed, new Point(0, 0), view.TopLeftMain.Location);
            }

            if (oilNeeds.PetroleumGasNeeded > 0)
            {
                // Petroleum gas is needed.
                // 0.75 is the heavy to light exchange rate.
                double totalLightPerCycle = lightOil.TotalCreated + (heavyOil.TotalCreated * 0.75);

                // 40 is the heavy oil consumed per cycle. 3 is the time to crack light.
                double heavyCrackPerRefinery = (heavyOil.TotalCreated * refineryCraftSpeed * 3.0) / (heavyOil.TimeToProduce * chemicalPlantCraftingSpeed * 40);

                // 30 is light oil consumed per cycle. 3 is the time to crack light.
                double lightCrackFacPerRefinery = (totalLightPerCycle * refineryCraftSpeed * 3.0) / (lightOil.TimeToProduce * chemicalPlantCraftingSpeed * 30);

                // We can create 90 gas per cycle with cracking.
                exactRefineriesNeeded = (oilNeeds.PetroleumGasNeeded * petroleumGas.TimeToProduce) / (90.0 * refineryCraftSpeed);

                //DisplayRefineriesNeededText(exactRefineriesNeeded, processNeeded);

                roundedRefineriesNeeded = (int)Math.Ceiling(exactRefineriesNeeded);
                int heavyCrackingPlants = (int)Math.Ceiling(roundedRefineriesNeeded * heavyCrackPerRefinery);
                int lightCrackingPlants = (int)Math.Ceiling(roundedRefineriesNeeded * lightCrackFacPerRefinery);


                // Grapical report.
                GraphicalNeed needLightCracking = new GraphicalNeed(lightCracking);
                needLightCracking.RoundedFacs = lightCrackingPlants;

                GraphicalNeed needHeavyCracking = new GraphicalNeed(heavyCracking);
                needHeavyCracking.RoundedFacs = heavyCrackingPlants;

                GraphicalNeed needAdvancedProcessing = new GraphicalNeed(advacedOilProcessing);
                needAdvancedProcessing.RoundedFacs = roundedRefineriesNeeded;

                // Establish relationship between oil needs.
                needAdvancedProcessing.ChildNeeds.Add(needHeavyCracking);
                needHeavyCracking.ChildNeeds.Add(needLightCracking);

                DrawThisFacNeed(needAdvancedProcessing, 0, maxDepth, ref largestColumnUsed, new Point(0, 0), view.TopLeftMain.Location);
            }

            if (oilNeeds.SolidFuelIngredientsNeeded > 0)
            {
                // Solid fuel is needed.
                // 0.75 is the exhage rate of heavy to light oil.
                double totalSolidCreatedPerCycle = (lightOil.TotalCreated / 10.0 + petroleumGas.TotalCreated / 20.0 + (heavyOil.TotalCreated * 0.75) / 10);
                exactRefineriesNeeded = (oilNeeds.SolidFuelIngredientsNeeded * lightOil.TimeToProduce) / (totalSolidCreatedPerCycle * refineryCraftSpeed);

                //DisplayRefineriesNeededText(exactRefineriesNeeded, processNeeded);

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

                const double refineriesPerHeavyCracking = 20.0 / 3;
                int heavyCrackingPlants = (int)Math.Ceiling(1.0 * roundedRefineriesNeeded / (refineriesPerHeavyCracking * chemicalPlantCraftingSpeed));

                // Grapical report.
                GraphicalNeed needSolidPlantsFromLight = new GraphicalNeed(solidFuelFromLight);
                needSolidPlantsFromLight.RoundedFacs = (int)Math.Ceiling(lightOilToSolidChemPlants);

                GraphicalNeed needSolidPlantsFromGas = new GraphicalNeed(solidFuelFromGas);
                needSolidPlantsFromGas.RoundedFacs = (int)Math.Ceiling(gasToSolidChemPlants);

                GraphicalNeed needHeavyCracking = new GraphicalNeed(heavyCracking);
                needHeavyCracking.RoundedFacs = heavyCrackingPlants;

                GraphicalNeed needAdvancedProcessing = new GraphicalNeed(advacedOilProcessing);
                needAdvancedProcessing.RoundedFacs = roundedRefineriesNeeded;

                // Establish relationship between oil needs.
                needHeavyCracking.ChildNeeds.Add(needSolidPlantsFromLight);
                needAdvancedProcessing.ChildNeeds.Add(needHeavyCracking);
                needAdvancedProcessing.ChildNeeds.Add(needSolidPlantsFromGas);

                DrawThisFacNeed(needAdvancedProcessing, 0, maxDepth, ref largestColumnUsed, new Point(0, 0), view.TopLeftMain.Location);
            }
        }

        // Smaller utility methods.

        /// <summary>
        /// Adjust the belt load to have a max of 40 load.
        /// </summary>

        private void HandleOptimizeBeltLoad()
        {
            const int maxBeltLoad = 40;

            // Get a base rate.
            view.TextTotalPerSecond.Text = "" + 1;
            HandleCalculate();

            double optimalRate = maxBeltLoad / largestBeltLoad;
            /*
            // Check if we will have an issue with double imprecision.
            double actualBeltLoad = optimalRate * largestBeltLoad;
            if (actualBeltLoad > maxBeltLoad)
            {
                optimalRate -= 0.000000000000001;
            }
            */

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

        // Finds the greatest width in this nodes children.
        private int GetMaxWidthOfTree(GraphicalNeed thisNeed)
        {
            int maxDepth = GetMaxDepthOfTree(thisNeed, 0, 0);
            int[] depthWidths = new int[maxDepth];
            depthWidths = GetWidthsOfTree(thisNeed, 0, depthWidths);
            int maxWidth = 0;

            for (int i = 0; i < maxDepth; i++)
            {
                if (depthWidths[i] > maxWidth)
                {
                    maxWidth = depthWidths[i];
                }
            }
            return maxWidth;
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
            depthWidths[thisDepth]++;
            thisDepth++;

            foreach (GraphicalNeed childNeed in thisNeed.ChildNeeds)
            {
                depthWidths = GetWidthsOfTree(childNeed, thisDepth, depthWidths);
            }

            return depthWidths;
        }

        /// <summary>
        /// Centers images in the graphical display of things to build.
        /// </summary>
        /// <param name="itemWidth"></param>
        /// <param name="totalWidth"></param>
        /// <returns></returns>
        private int CenterXImageOffset(int itemWidth, int totalWidth)
        {
            return (totalWidth - itemWidth - 16) / 2;
        }

        /// <summary>
        /// Centers text in the graphic display of things to build.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="totalWidth"></param>
        /// <returns></returns>
        private int CenterXStringOffset(string text, Font font, int totalWidth)
        {
            view.StringSize.Text = text;
            view.StringSize.Font = font;
            int textWidth = view.StringSize.Size.Width;

            return (totalWidth - textWidth) / 2;

        }
    }
}
