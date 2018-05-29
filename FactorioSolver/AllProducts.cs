﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioSolver
{
    /// <summary>
    /// For creating a data structure that has information about products that can be crafted.
    /// </summary>
    class AllProducts
    {
        public Dictionary<string, Product> Dictionary { get; }
        private List<Product> products;

        public AllProducts()
        {
            Dictionary = new Dictionary<string, Product>();
            products = new List<Product>();
        }

        public void CreateDefaultProducts()
        {
            // Create factory types.
            Factory assemblingMachine = new Factory("Assembling Machine", true, 1.25);
            Factory chemicalPlantBelt = new Factory("Chemical Plant", true, 1.25);
            Factory chemicalPlantPipe = new Factory("Chemical Plant", false, 1.25);
            Factory oilRefinery = new Factory("Oil Refinery", false, 1);
            Factory electricFurnace = new Factory("Electric Furnace", true, 2);
            Factory offshorePump = new Factory("Offshore Pump", false, 1);
            Factory electricMiningDrill = new Factory("Electric Mining Drill", true, 0.5);

            // High tech pack
            Product highTechPack = new Product("High Tech Pack", 14, 2, assemblingMachine);
            Product battery = new Product("Battery", 5, 1, chemicalPlantBelt);
            Product sulfuricAcid = new Product("Sulfuric Acid", 1, 50, chemicalPlantPipe);
            Product sulfur = new Product("Sulfur", 1, 2, chemicalPlantBelt);
            Product petroleumGas = new Product("Petroleum Gas", 5, 55, oilRefinery);
            Product lightOil = new Product("Light Oil", 5, 45, oilRefinery);
            Product heavyOil = new Product("Heavy Oil", 5, 10, oilRefinery);
            Product copperCable = new Product("CopperCable", 0.5, 2, assemblingMachine);
            Product processingUnit = new Product("Processing Unit", 10, 1, assemblingMachine);
            Product electronicCircuit = new Product("Electronic Circuit", 0.5, 1, assemblingMachine);
            Product advancedCircuit = new Product("Advanced Circuit", 6, 1, assemblingMachine);
            Product plasticBar = new Product("Plastic Bar", 1, 1, chemicalPlantBelt);
            Product speedModule = new Product("Speed Module", 15, 1, assemblingMachine);
            Product ironPlate = new Product("Iron Plate", 3.5, 1, electricFurnace);
            Product copperPlate = new Product("Copper Plate", 3.5, 1, electricFurnace);
            Product water = new Product("Water", 1, 1200, offshorePump);

            // Science pack 1
            Product sciencePack1 = new Product("Science Pack 1", 5, 1, assemblingMachine);
            Product ironGearWheel = new Product("Iron Gear Wheel", 0.5, 1, assemblingMachine);

            // Science pack 2
            Product sciencePack2 = new Product("Science Pack 2", 6, 1, assemblingMachine);
            Product inserter = new Product("Inserter", 0.5, 1, assemblingMachine);
            Product transportBelt = new Product("Transport Belt", 0.5, 2, assemblingMachine);

            // Science pack 3

            // Miltitary science pack

            // Production science pack


            // Resources that don't have real stats yet.
            Product coal = new Product("Coal", 1, 1, electricMiningDrill);
            Product ironOre = new Product("Iron Ore", 1, 1, electricMiningDrill);
            Product copperOre = new Product("Copper Ore", 1, 1, electricMiningDrill);


            // Add ingredients
            highTechPack.Ingredients.Add(new Ingredient(battery, 1));
            highTechPack.Ingredients.Add(new Ingredient(copperCable, 30));
            highTechPack.Ingredients.Add(new Ingredient(processingUnit, 3));
            highTechPack.Ingredients.Add(new Ingredient(speedModule, 1));

            battery.Ingredients.Add(new Ingredient(ironPlate, 1));
            battery.Ingredients.Add(new Ingredient(copperPlate, 1));
            battery.Ingredients.Add(new Ingredient(sulfuricAcid, 20));

            sulfuricAcid.Ingredients.Add(new Ingredient(ironPlate, 1));
            sulfuricAcid.Ingredients.Add(new Ingredient(sulfur, 5));
            sulfuricAcid.Ingredients.Add(new Ingredient(water, 100));

            sulfur.Ingredients.Add(new Ingredient(water, 30));
            sulfur.Ingredients.Add(new Ingredient(petroleumGas, 30));

            copperCable.Ingredients.Add(new Ingredient(copperPlate, 1));

            processingUnit.Ingredients.Add(new Ingredient(electronicCircuit, 20));
            processingUnit.Ingredients.Add(new Ingredient(advancedCircuit, 2));
            processingUnit.Ingredients.Add(new Ingredient(sulfuricAcid, 5));

            electronicCircuit.Ingredients.Add(new Ingredient(ironPlate, 1));
            electronicCircuit.Ingredients.Add(new Ingredient(copperCable, 3));

            advancedCircuit.Ingredients.Add(new Ingredient(plasticBar, 2));
            advancedCircuit.Ingredients.Add(new Ingredient(copperCable, 4));
            advancedCircuit.Ingredients.Add(new Ingredient(electronicCircuit, 2));

            plasticBar.Ingredients.Add(new Ingredient(coal, 1));
            plasticBar.Ingredients.Add(new Ingredient(petroleumGas, 20));

            speedModule.Ingredients.Add(new Ingredient(electronicCircuit, 5));
            speedModule.Ingredients.Add(new Ingredient(advancedCircuit, 5));

            sciencePack1.Ingredients.Add(new Ingredient(copperPlate, 1));
            sciencePack1.Ingredients.Add(new Ingredient(ironGearWheel, 1));

            ironGearWheel.Ingredients.Add(new Ingredient(ironPlate, 2));
            ironPlate.Ingredients.Add(new Ingredient(ironOre, 1));
            copperPlate.Ingredients.Add(new Ingredient(copperOre, 1));

            sciencePack2.Ingredients.Add(new Ingredient(inserter, 1));
            sciencePack2.Ingredients.Add(new Ingredient(transportBelt, 1));

            inserter.Ingredients.Add(new Ingredient(electronicCircuit, 1));
            inserter.Ingredients.Add(new Ingredient(ironGearWheel, 1));
            inserter.Ingredients.Add(new Ingredient(ironPlate, 1));

            transportBelt.Ingredients.Add(new Ingredient(ironGearWheel, 1));
            transportBelt.Ingredients.Add(new Ingredient(ironPlate, 1));

            // Add all to the list.
            products.Add(highTechPack);
            products.Add(battery);
            products.Add(sulfuricAcid);
            products.Add(sulfur);
            products.Add(petroleumGas);
            products.Add(lightOil);
            products.Add(heavyOil);
            products.Add(copperCable);
            products.Add(processingUnit);
            products.Add(electronicCircuit);
            products.Add(advancedCircuit);
            products.Add(plasticBar);
            products.Add(speedModule);
            products.Add(ironPlate);
            products.Add(copperPlate);
            products.Add(water);

            products.Add(coal);
            products.Add(ironOre);
            products.Add(copperOre);

            products.Add(sciencePack1);
            products.Add(ironGearWheel);

            products.Add(sciencePack2);
            products.Add(inserter);
            products.Add(transportBelt);

            // Add all to the dictionary
            foreach (Product product in products)
            {
                Dictionary.Add(product.Name, product);
            }
        }
    }
}
