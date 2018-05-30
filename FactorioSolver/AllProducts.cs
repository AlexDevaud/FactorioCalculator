using System;
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
            Factory facAssemblingMachine = new Factory("Assembling Machine", true, 1.25);
            Factory facChemicalPlantBelt = new Factory("Chemical Plant", true, 1.25);
            Factory facChemicalPlantPipe = new Factory("Chemical Plant", false, 1.25);
            Factory facOilRefinery = new Factory("Oil Refinery", false, 1);
            Factory facElectricFurnace = new Factory("Electric Furnace", true, 2);
            Factory facOffshorePump = new Factory("Offshore Pump", false, 1);
            Factory facElectricMiningDrill = new Factory("Electric Mining Drill", true, 0.5);

            // High tech science pack
            Product highTechSciencePack = new Product("High Tech Science Pack", 14, 2, facAssemblingMachine);
            Product battery = new Product("Battery", 5, 1, facChemicalPlantBelt);
            Product sulfuricAcid = new Product("Sulfuric Acid", 1, 50, facChemicalPlantPipe);
            Product sulfur = new Product("Sulfur", 1, 2, facChemicalPlantBelt);
            Product petroleumGas = new Product("Petroleum Gas", 5, 55, facOilRefinery);
            Product lightOil = new Product("Light Oil", 5, 45, facOilRefinery);
            Product heavyOil = new Product("Heavy Oil", 5, 10, facOilRefinery);
            Product copperCable = new Product("CopperCable", 0.5, 2, facAssemblingMachine);
            Product processingUnit = new Product("Processing Unit", 10, 1, facAssemblingMachine);
            Product electronicCircuit = new Product("Electronic Circuit", 0.5, 1, facAssemblingMachine);
            Product advancedCircuit = new Product("Advanced Circuit", 6, 1, facAssemblingMachine);
            Product plasticBar = new Product("Plastic Bar", 1, 1, facChemicalPlantBelt);
            Product speedModule = new Product("Speed Module", 15, 1, facAssemblingMachine);
            Product ironPlate = new Product("Iron Plate", 3.5, 1, facElectricFurnace);
            Product copperPlate = new Product("Copper Plate", 3.5, 1, facElectricFurnace);
            Product water = new Product("Water", 1, 1200, facOffshorePump);

            // Science pack 1
            Product sciencePack1 = new Product("Science Pack 1", 5, 1, facAssemblingMachine);
            Product ironGearWheel = new Product("Iron Gear Wheel", 0.5, 1, facAssemblingMachine);

            // Science pack 2
            Product sciencePack2 = new Product("Science Pack 2", 6, 1, facAssemblingMachine);
            Product inserter = new Product("Inserter", 0.5, 1, facAssemblingMachine);
            Product transportBelt = new Product("Transport Belt", 0.5, 2, facAssemblingMachine);

            // Science pack 3
            Product sciencePack3 = new Product("Science Pack 3", 12, 1, facAssemblingMachine);
            Product electricMiningDrill = new Product("Electric Mining Drill", 2, 1, facAssemblingMachine);
            Product engineUnit = new Product("Engine Unit", 10, 1, facAssemblingMachine);
            Product pipe = new Product("Pipe", 0.5, 1, facAssemblingMachine);
            Product steelPlate = new Product("Steel Plate", 17.5, 1, facElectricFurnace);

            // Miltitary science pack
            Product militarySciencePack = new Product("Military Science Pack", 10, 2, facAssemblingMachine);
            Product piercingRoundsMagazine = new Product("Piercing Rounds Magazine", 3, 1, facAssemblingMachine);
            Product firearmMagazine = new Product("Firearm Magazine", 1, 1, facAssemblingMachine);
            Product grenade = new Product("Grenade", 8, 1, facAssemblingMachine);
            Product gunTurret = new Product("Gun Turret", 8, 1, facAssemblingMachine);

            // Production science pack
            Product productionSciencePack = new Product("Production Science Pack", 14, 2, facAssemblingMachine);
            Product electricEngineUnit = new Product("Electric Engine Unit", 10, 1, facAssemblingMachine);
            Product lubricant = new Product("Lubricant", 1, 10, facChemicalPlantPipe);
            Product electricFurnace = new Product("Electric Furnace", 5, 1, facAssemblingMachine);
            Product stoneBrick = new Product("Stone Brick", 3.5, 1, facElectricFurnace);

            // Mined resources that don't have real stats yet.
            Product coal = new Product("Coal", 1, 1, facElectricMiningDrill);
            Product ironOre = new Product("Iron Ore", 1, 1, facElectricMiningDrill);
            Product copperOre = new Product("Copper Ore", 1, 1, facElectricMiningDrill);
            Product stone = new Product("Stone", 1, 1, facElectricMiningDrill);


            // Add ingredients
            highTechSciencePack.Ingredients.Add(new Ingredient(battery, 1));
            highTechSciencePack.Ingredients.Add(new Ingredient(copperCable, 30));
            highTechSciencePack.Ingredients.Add(new Ingredient(processingUnit, 3));
            highTechSciencePack.Ingredients.Add(new Ingredient(speedModule, 1));

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

            sciencePack3.Ingredients.Add(new Ingredient(electricMiningDrill, 1));
            sciencePack3.Ingredients.Add(new Ingredient(advancedCircuit, 1));
            sciencePack3.Ingredients.Add(new Ingredient(engineUnit, 1));

            electricMiningDrill.Ingredients.Add(new Ingredient(electronicCircuit, 3));
            electricMiningDrill.Ingredients.Add(new Ingredient(ironGearWheel, 5));
            electricMiningDrill.Ingredients.Add(new Ingredient(ironPlate, 10));

            engineUnit.Ingredients.Add(new Ingredient(ironGearWheel, 1));
            engineUnit.Ingredients.Add(new Ingredient(pipe, 2));
            engineUnit.Ingredients.Add(new Ingredient(steelPlate, 1));

            militarySciencePack.Ingredients.Add(new Ingredient(piercingRoundsMagazine, 1));
            militarySciencePack.Ingredients.Add(new Ingredient(grenade, 1));
            militarySciencePack.Ingredients.Add(new Ingredient(gunTurret, 1));

            piercingRoundsMagazine.Ingredients.Add(new Ingredient(copperPlate, 5));
            piercingRoundsMagazine.Ingredients.Add(new Ingredient(firearmMagazine, 1));
            piercingRoundsMagazine.Ingredients.Add(new Ingredient(steelPlate, 1));

            firearmMagazine.Ingredients.Add(new Ingredient(ironPlate, 4));

            grenade.Ingredients.Add(new Ingredient(coal, 10));
            grenade.Ingredients.Add(new Ingredient(ironPlate, 5));

            gunTurret.Ingredients.Add(new Ingredient(copperPlate, 10));
            gunTurret.Ingredients.Add(new Ingredient(ironGearWheel, 10));
            gunTurret.Ingredients.Add(new Ingredient(ironPlate, 20));

            productionSciencePack.Ingredients.Add(new Ingredient(electricEngineUnit, 1));
            productionSciencePack.Ingredients.Add(new Ingredient(electricFurnace, 1));

            electricEngineUnit.Ingredients.Add(new Ingredient(electronicCircuit, 2));
            electricEngineUnit.Ingredients.Add(new Ingredient(engineUnit, 1));
            electricEngineUnit.Ingredients.Add(new Ingredient(lubricant, 15));

            lubricant.Ingredients.Add(new Ingredient(heavyOil, 10));

            electricFurnace.Ingredients.Add(new Ingredient(advancedCircuit, 5));
            electricFurnace.Ingredients.Add(new Ingredient(steelPlate, 10));
            electricFurnace.Ingredients.Add(new Ingredient(stoneBrick, 10));

            stoneBrick.Ingredients.Add(new Ingredient(stone, 2));

            // Add all to the list.
            products.Add(highTechSciencePack);
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
            products.Add(stone);

            products.Add(sciencePack1);
            products.Add(ironGearWheel);

            products.Add(sciencePack2);
            products.Add(inserter);
            products.Add(transportBelt);

            products.Add(sciencePack3);
            products.Add(electricMiningDrill);
            products.Add(engineUnit);
            products.Add(pipe);
            products.Add(steelPlate);

            products.Add(militarySciencePack);
            products.Add(piercingRoundsMagazine);
            products.Add(firearmMagazine);
            products.Add(grenade);
            products.Add(gunTurret);

            products.Add(productionSciencePack);
            products.Add(electricEngineUnit);
            products.Add(lubricant);
            products.Add(electricFurnace);
            products.Add(stoneBrick);

            // Add all to the dictionary
            foreach (Product product in products)
            {
                Dictionary.Add(product.Name, product);
            }
        }
    }
}
