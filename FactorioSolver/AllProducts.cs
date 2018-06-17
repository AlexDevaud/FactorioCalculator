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
            Factory facElectricMiningDrill = new Factory("Electric Mining Drill", true, 1);

            // Images of factories.
            facAssemblingMachine.ImageString = "Images\\Assembling_machine_3.png";
            facChemicalPlantBelt.ImageString = "Images\\Chemical_plant.png";
            facChemicalPlantPipe.ImageString = "Images\\Chemical_plant.png";
            facOilRefinery.ImageString = "Images\\Oil_refinery.png";
            facElectricFurnace.ImageString = "Images\\Electric_furnace.png";
            facOffshorePump.ImageString = "Images\\Offshore_pump.png";
            facElectricMiningDrill.ImageString = "Images\\Electric_mining_drill.png";
            

            // Create all product objects.
            // From left to right, top to bottom.
            // Logistics
            Product woodenChest = new Product("Wooden Chest");
            Product ironChest = new Product("Iron Chest");
            Product steelChest = new Product("Steel Chest");
            Product storageTank = new Product("Storage Tank");

            Product transportBelt = new Product("Transport Belt");
            Product fastTransportBelt = new Product("Fast Transport Belt");
            Product expressTransportBelt = new Product("Express Transport Belt");
            Product undergroundBelt = new Product("Underground Belt");
            Product fastUndergroundBelt = new Product("Fast Underground Belt");
            Product expressUndergroundBelt = new Product("Express Underground Belt");
            Product splitter = new Product("Splitter");
            Product fastSplitter = new Product("Fast Splitter");
            Product expressSplitter = new Product("Express Splitter");

            Product burnerInserter = new Product("Burner Inserter");
            Product inserter = new Product("Inserter");
            Product longHandedInserter = new Product("Long Handed Inserter");
            Product fastInserter = new Product("Fast Inserter");
            Product filterInserter = new Product("Filter Inserter");
            Product stackInserter = new Product("Stack Inserter");
            Product stackFilterInserter = new Product("Stack Filter Inserter");

            Product smallElectricPole = new Product("Small Electric Pole");
            Product mediumElectricPole = new Product("Medium Electric Pole");
            Product bigElectricPole = new Product("Big Electric Pole");
            Product substation = new Product("Substation");
            Product pipe = new Product("Pipe");
            Product pipeToGround = new Product("Pipe To Ground");
            Product pump = new Product("Pump");

            Product rail = new Product("Rail");
            Product trainStop = new Product("Train Stop");
            Product railSignal = new Product("Rail Signal");
            Product railChainSignal = new Product("Rail Chain Signal");
            Product locomotive = new Product("Locomotive");
            Product cargoWagon = new Product("Cargo Wagon");
            Product fluidWagon = new Product("Fluid Wagon");
            Product artilleryWagon = new Product("Artillery Wagon");
            Product car = new Product("Car");
            Product tank = new Product("Tank");

            Product logisticRobot = new Product("Logistic Robot");
            Product constructionRobot = new Product("Construction Robot");
            Product activeProviderChest = new Product("Active Provider Chest");
            Product passiveProviderChest = new Product("Passive Provider Chest");
            Product storageChest = new Product("Storage Chest");
            Product bufferChest = new Product("Buffer Chest");
            Product requesterChest = new Product("Requester Chest");
            Product roboport = new Product("Roboport");

            Product lamp = new Product("Lamp");
            Product redWire = new Product("Red Wire");
            Product greenWire = new Product("Green Wire");
            Product arithmeticCombinator = new Product("Arithmetic Combinator");
            Product deciderCombinator = new Product("Decider Combinator");
            Product constantCombinator = new Product("Constant Combinator");
            Product powerSwitch = new Product("Power Switch");
            Product programmableSpeaker = new Product("Programmable Speaker");

            Product stoneBrick = new Product("Stone Brick");
            Product concrete = new Product("Concrete");
            Product hazardConcrete = new Product("Hazard Concrete");
            Product refinedConcrete = new Product("Refined Concrete");
            Product refinedHazardConcrete = new Product("Refined Hazard Concrete");
            Product landfill = new Product("Landfill");
            Product cliffExplosives = new Product("Cliff Explosives");

            // Production
            Product ironAxe = new Product("Iron Axe");
            Product steelAxe = new Product("Steel Axe");
            Product repairPack = new Product("Repair Pack");
            Product blueprint = new Product("Blueprint");
            Product deconstructionPlanner = new Product("Deconstruction Planner");
            Product blueprintBook = new Product("Blueprint Book");

            Product boiler = new Product("Boiler");
            Product steamEngine = new Product("Steam Engine");
            Product steamTurbine = new Product("Steam Turbine");
            Product solarPanel = new Product("Solar Panel");
            Product accumulator = new Product("Accumulator");
            Product nuclearReactor = new Product("NuclearReactor");
            Product heatExchanger = new Product("Heat Exchanger");
            Product heatPipe = new Product("Heat Pipe");

            Product burnerMiningDrill = new Product("Burner Mining Drill");
            Product electricMiningDrill = new Product("Electric Mining Drill");
            Product offshorePump = new Product("Offshore Pump");
            Product pumpjack = new Product("Pumpjack");

            Product stoneFurnace = new Product("Stone Furnace");
            Product steelFurnace = new Product("Steel Furnace");
            Product electricFurnace = new Product("Electric Furnace");

            Product assembligMachine1 = new Product("Assembling Machine 1");
            Product assemblingMachine2 = new Product("Assembling Machine 2");
            Product assemblingMachine3 = new Product("Assembling Machine 3");
            Product oilRefinery = new Product("Oil Refinery");
            Product chemicalPlant = new Product("Chemical Plant");
            Product centrifuge = new Product("Centrifuge");
            Product lab = new Product("Lab");

            Product beacon = new Product("Beacon");
            Product speedModule = new Product("Speed Module");
            Product speedModule2 = new Product("Speed Module 2");
            Product speedModule3 = new Product("Speed Module 3");
            Product efficiencyModule = new Product("Efficiency Module");
            Product efficiencyModule2 = new Product("Efficiency Module 2");
            Product efficiencyModule3 = new Product("Efficiency Module 3");
            Product productivityModule = new Product("Productivity Module");
            Product productivityModule2 = new Product("Productivity Module 2");
            Product productivityModule3 = new Product("Productivity Module 3");

            // Intermediate Product
            Product rawWood = new Product("Raw Wood");
            Product coal = new Product("Coal");
            Product stone = new Product("Stone");
            Product ironOre = new Product("Iron Ore");
            Product copperOre = new Product("Copper Ore");
            Product uraniumOre = new Product("Uranium Ore");
            Product rawFish = new Product("Raw Fish");
            Product crudeOil = new Product("Crude Oil");
            Product heavyOil = new Product("Heavy Oil");
            Product lightOil = new Product("Light Oil");
            Product lubricant = new Product("Lubricant");
            Product petroleumGas = new Product("Petroleum Gas");
            Product sulfuricAcid = new Product("Sulfuric Acid");
            Product water = new Product("Water");
            Product steam = new Product("Steam");

            Product wood = new Product("Wood");
            Product ironPlate = new Product("Iron Plate");
            Product copperPlate = new Product("Copper Plate");
            Product solidFuel = new Product("Solid Fuel");
            Product solidFuelIngredients = new Product("Solid Fuel Ingredient"); // This is a special case made up ingredient to deal with solid fuel having multiple recipes.
            Product steelPlate = new Product("Steel Plate");
            Product plasticBar = new Product("Plastic Bar");
            Product sulfur = new Product("Sulfur");
            Product battery = new Product("Battery");
            Product explosives = new Product("Explosives");
            Product uraniumProcessing = new Product("Uranium Processing");

            Product crudeOilBarrel = new Product("Curde Oil Barrel");
            Product heavyOilBarrel = new Product("Heavy Oil Barrel");
            Product lightOilBarrel = new Product("Light Oil Barrel");
            Product lubricantBarrel = new Product("Lubricant Barrel");
            Product petroleumGasBarrel = new Product("Petroleum Gas Barrel");
            Product sulfuricAcidBarrel = new Product("SulfuricAcidBarrel");
            Product waterBarrel = new Product("Water Barrel");

            Product copperCable = new Product("CopperCable");
            Product ironStick = new Product("Iron Stick");
            Product ironGearWheel = new Product("Iron Gear Wheel");
            Product emptyBarell = new Product("Empty Barrel");
            Product electronicCircuit = new Product("Electronic Circuit");
            Product advancedCircuit = new Product("Advanced Circuit");
            Product processingUnit = new Product("Processing Unit");
            Product engineUnit = new Product("Engine Unit");
            Product electricEngineUnit = new Product("Electric Engine Unit");
            Product flyingRobotFrame = new Product("Flying Robot Frame");
            Product satellite = new Product("Satellite");
            Product rocketPart = new Product("Rocket Part");
            Product rocketControlUnit = new Product("Rocket Control Unit");
            Product lowDensityStructure = new Product("Low Density Structure");
            Product rocketFuel = new Product("Rocket Fuel");
            Product nuclearFuel = new Product("Nuclear Fuel");
            Product uranium235 = new Product("Uranium-235");
            Product uranium238 = new Product("Uranium-238");

            Product uraniumFuelCell = new Product("Uranium Fuel Cell");
            Product usedUpUraniumFuelCell = new Product("Used Up Uranium Fuel Cell");
            Product nuclearFuelReprocessing = new Product("Nuclear Fuel Reprocessing");
            Product kovarexEnrichmentProcess = new Product("Kovarex Enrichment Process");

            Product sciencePack1 = new Product("Science Pack 1");
            Product sciencePack2 = new Product("Science Pack 2");
            Product sciencePack3 = new Product("Science Pack 3");
            Product militarySciencePack = new Product("Military Science Pack");
            Product productionSciencePack = new Product("Production Science Pack");
            Product highTechSciencePack = new Product("High Tech Science Pack");
            Product spaceSciencePack = new Product("Space Science Pack");

            // Combat
            Product pistol = new Product("Pistol");
            Product submachineGun = new Product("Submachine Gun");
            Product shotgun = new Product("Shotgun");
            Product combatShotgun = new Product("Combat Shotgun");
            Product rocketLauncher = new Product("Rocket Launcher");
            Product flamethrower = new Product("Flamethrower");
            Product landMine = new Product("Land Mine");

            Product firearmMagazine = new Product("Firearm Magazine");
            Product piercingRoundsMagazine = new Product("Piercing Rounds Magazine");
            Product uraniumRoundsMagazine = new Product("Uranium Rounds Magazine");
            Product shotgunShells = new Product("Shotgun Shells");
            Product piercingShotgunShells = new Product("Piercing Shotgun Shells");
            Product cannonShell = new Product("Cannon Shell");
            Product explosiveCannonShell = new Product("Explosive Cannon Shell");
            Product uraniumCannonShell = new Product("Uranium Cannon Shell");
            Product explosiveUraniumCannonShell = new Product("Explosive Uranium Cannon Shell");
            Product artilleyShell = new Product("Artillery Shell");
            Product rocket = new Product("Rocket");
            Product explosiveRocket = new Product("Explosive Rocket");
            Product atomicBomb = new Product("Atomic Bomb");
            Product flamethrowerAmmo = new Product("Flamethrower Ammo");

            Product grenade = new Product("Grenade");
            Product clusterGrenade = new Product("Cluster Grenade");
            Product poisonCapsule = new Product("Poison Capsule");
            Product slowdownCapsule = new Product("Slowdown Capsule");
            Product defenderCapsule = new Product("Defender Capsule");
            Product distractorCapsule = new Product("Distractor Capsule");
            Product destroyerCapsule = new Product("Destroyer Capsule");
            Product dischargeDefenseRemote = new Product("Dischange Defense Remote");
            Product artilleryTargetingRemote = new Product("Artillery Targeting Remote");

            Product lightArmor = new Product("Light Armor");
            Product heavyArmor = new Product("Heavy Armor");
            Product modularArmor = new Product("Modular Armor");
            Product powerArmor = new Product("Power Armor");
            Product powerArmorMK2 = new Product("Power Armor MK2");

            Product portableSolarPanel = new Product("Portable Solar Panel");
            Product portableFusionReactor = new Product("Portable Fusion Reactor");
            Product energyShield = new Product("Engergy Shield");
            Product energyShieldMK2 = new Product("Engergy Shield MK2");
            Product batteryMK1 = new Product("Battery MK1");
            Product batteryMK2 = new Product("Battery MK2");
            Product personalLaserDefense = new Product("Personal Laser Defense");
            Product dischargeDefense = new Product("Discharge Defense");
            Product exoskeleton = new Product("Exoskeleton");
            Product personalRoboport = new Product("Personal Roboport");
            Product personalRoboportMK2 = new Product("Personal Roboport MK2");
            Product nightvision = new Product("Nightvision");

            Product stoneWall = new Product("Stone Wall");
            Product gate = new Product("Gate");
            Product gunTurret = new Product("Gun Turret");
            Product laserTurret = new Product("Laser Turret");
            Product flamethrowerTurret = new Product("flamethrowerTurret");
            Product artilleryTurret = new Product("Artillery Turret");
            Product radar = new Product("Radar");
            Product rocketSilo = new Product("Rocket Silo");

            // Machine processes.
            Product advancedOilProcessing = new Product("Advanced Oil Processing");
            Product basicOilProcessing = new Product("Basic Oil Processing");
            Product heavyCracking = new Product("Heavy Oil Cracking");
            Product lightCracking = new Product("Light Oil Cracking");
            Product solidFuelFromHeavyOil = new Product("Solid Fuel From Heavy Oil");
            Product solidFuelFromLightOil = new Product("Solid Fuel From Light Oil");
            Product solidFuelFromPetroleumGas = new Product("Solid Fuel From Petroleum Gas");

            // Add all ingredients and stats.
            // From left to right, top to bottom.

            // Logistics 
            woodenChest.Producer = facAssemblingMachine;
            woodenChest.TimeToProduce = 0.5;
            woodenChest.TotalCreated = 1;
            woodenChest.Ingredients.Add(new Ingredient(wood, 4));
            woodenChest.ImageString = "Images\\Wooden_chest.png";

            ironChest.Producer = facAssemblingMachine;
            ironChest.TimeToProduce = 0.5;
            ironChest.TotalCreated = 1;
            ironChest.Ingredients.Add(new Ingredient(ironPlate, 8));
            ironChest.ImageString = "Images\\Iron_chest.png";

            steelChest.Producer = facAssemblingMachine;
            steelChest.TimeToProduce = 0.5;
            steelChest.TotalCreated = 1;
            steelChest.Ingredients.Add(new Ingredient(steelPlate, 8));
            steelChest.ImageString = "Images\\Steel_chest.png";

            storageTank.Producer = facAssemblingMachine;
            storageTank.TimeToProduce = 3;
            storageTank.TotalCreated = 1;
            storageTank.Ingredients.Add(new Ingredient(ironPlate, 20));
            storageTank.Ingredients.Add(new Ingredient(steelPlate, 5));
            storageTank.ImageString = "Images\\Storage_tank.png";

            transportBelt.Producer = facAssemblingMachine;
            transportBelt.TimeToProduce = 0.5;
            transportBelt.TotalCreated = 2;
            transportBelt.Ingredients.Add(new Ingredient(ironGearWheel, 1));
            transportBelt.Ingredients.Add(new Ingredient(ironPlate, 1));
            transportBelt.ImageString = "Images\\Transport_belt.png";

            fastTransportBelt.Producer = facAssemblingMachine;
            fastTransportBelt.TimeToProduce = 0.5;
            fastTransportBelt.TotalCreated = 1;
            fastTransportBelt.Ingredients.Add(new Ingredient(ironGearWheel, 5));
            fastTransportBelt.Ingredients.Add(new Ingredient(transportBelt, 1));
            fastTransportBelt.ImageString = "Images\\Fast_transport_belt.png";

            expressTransportBelt.Producer = facAssemblingMachine;
            expressTransportBelt.TimeToProduce = 0.5;
            expressTransportBelt.TotalCreated = 1;
            expressTransportBelt.Ingredients.Add(new Ingredient(fastTransportBelt, 1));
            expressTransportBelt.Ingredients.Add(new Ingredient(ironGearWheel, 10));
            expressTransportBelt.Ingredients.Add(new Ingredient(lubricant, 20));
            expressTransportBelt.ImageString = "Images\\Express_transport_belt.png";


            // Intermediate Products
            rocketFuel.Producer = facAssemblingMachine;
            rocketFuel.TimeToProduce = 30;
            rocketFuel.TotalCreated = 1;
            rocketFuel.Ingredients.Add(new Ingredient(solidFuel, 10));
            rocketFuel.ImageString = "Images\\Rocket_fuel.png";

            solidFuel.Producer = facChemicalPlantBelt;
            solidFuel.TimeToProduce = 3;
            solidFuel.TotalCreated = 1;
            solidFuel.Ingredients.Add(new Ingredient(solidFuelIngredients, 1));
            solidFuel.ImageString = "Images\\Solid_fuel.png";

            solidFuelIngredients.Producer = facOilRefinery;
            solidFuelIngredients.TimeToProduce = 5;
            solidFuelIngredients.TotalCreated = 1;
            solidFuelIngredients.ImageString = "Images\\Wood.png";


            // Machine processes
            advancedOilProcessing.Producer = facOilRefinery;
            advancedOilProcessing.TimeToProduce = 5;
            advancedOilProcessing.ImageString = "Images\\Advanced_oil_processing.png";

            basicOilProcessing.Producer = facOilRefinery;
            basicOilProcessing.TimeToProduce = 5;
            basicOilProcessing.ImageString = "Images\\Basic_oil_processing.png";

            heavyCracking.Producer = facChemicalPlantPipe;
            heavyCracking.TimeToProduce = 3;
            heavyCracking.ImageString = "Images\\Heavy_oil_cracking.png";

            lightCracking.Producer = facChemicalPlantPipe;
            lightCracking.TimeToProduce = 3;
            lightCracking.ImageString = "Images\\Light_oil_cracking.png";

            solidFuelFromHeavyOil.Producer = facChemicalPlantBelt;
            solidFuelFromHeavyOil.TimeToProduce = 3;
            solidFuelFromHeavyOil.ImageString = "Images\\Solid_fuel_from_heavy_oil.png";

            solidFuelFromLightOil.Producer = facChemicalPlantBelt;
            solidFuelFromLightOil.TimeToProduce = 3;
            solidFuelFromLightOil.ImageString = "Images\\Solid_fuel_from_light_oil.png";

            solidFuelFromPetroleumGas.Producer = facChemicalPlantBelt;
            solidFuelFromPetroleumGas.TimeToProduce = 3;
            solidFuelFromPetroleumGas.ImageString = "Images\\Solid_fuel_from_petroleum_gas.png";

            // Old sorting
            //assemblingMachine3.ImageString = "Images\\Assembling_machine_3.png";

            lowDensityStructure.Producer = facAssemblingMachine;
            lowDensityStructure.TimeToProduce = 30;
            lowDensityStructure.TotalCreated = 1;
            lowDensityStructure.Ingredients.Add(new Ingredient(copperPlate, 5));
            lowDensityStructure.Ingredients.Add(new Ingredient(plasticBar, 5));
            lowDensityStructure.Ingredients.Add(new Ingredient(steelPlate, 10));
            lowDensityStructure.ImageString = "Images\\Low_density_structure.png";

            rocketControlUnit.Producer = facAssemblingMachine;
            rocketControlUnit.TimeToProduce = 30;
            rocketControlUnit.TotalCreated = 1;
            rocketControlUnit.Ingredients.Add(new Ingredient(processingUnit, 1));
            rocketControlUnit.Ingredients.Add(new Ingredient(speedModule, 1));
            rocketControlUnit.ImageString = "Images\\Rocket_control_unit.png";

            highTechSciencePack.Producer = facAssemblingMachine;
            highTechSciencePack.TimeToProduce = 14;
            highTechSciencePack.TotalCreated = 2;
            highTechSciencePack.Ingredients.Add(new Ingredient(battery, 1));
            highTechSciencePack.Ingredients.Add(new Ingredient(copperCable, 30));
            highTechSciencePack.Ingredients.Add(new Ingredient(processingUnit, 3));
            highTechSciencePack.Ingredients.Add(new Ingredient(speedModule, 1));
            highTechSciencePack.ImageString = "Images\\High_tech_science_pack.png";

            battery.Producer = facChemicalPlantBelt;
            battery.TimeToProduce = 5;
            battery.TotalCreated = 1;
            battery.Ingredients.Add(new Ingredient(ironPlate, 1));
            battery.Ingredients.Add(new Ingredient(copperPlate, 1));
            battery.Ingredients.Add(new Ingredient(sulfuricAcid, 20));
            battery.ImageString = "Images\\Battery.png";

            sulfuricAcid.Producer = facChemicalPlantPipe;
            sulfuricAcid.TimeToProduce = 1;
            sulfuricAcid.TotalCreated = 50;
            sulfuricAcid.Ingredients.Add(new Ingredient(ironPlate, 1));
            sulfuricAcid.Ingredients.Add(new Ingredient(sulfur, 5));
            sulfuricAcid.Ingredients.Add(new Ingredient(water, 100));
            sulfuricAcid.ImageString = "Images\\Sulfuric_acid.png";

            sulfur.Producer = facChemicalPlantBelt;
            sulfur.TimeToProduce = 1;
            sulfur.TotalCreated = 2;
            sulfur.Ingredients.Add(new Ingredient(water, 30));
            sulfur.Ingredients.Add(new Ingredient(petroleumGas, 30));
            sulfur.ImageString = "Images\\Sulfur.png";

            copperCable.Producer = facAssemblingMachine;
            copperCable.TimeToProduce = 0.5;
            copperCable.TotalCreated = 2;
            copperCable.Ingredients.Add(new Ingredient(copperPlate, 1));
            copperCable.ImageString = "Images\\Copper_cable.png";

            processingUnit.Producer = facAssemblingMachine;
            processingUnit.TimeToProduce = 10;
            processingUnit.TotalCreated = 1;
            processingUnit.Ingredients.Add(new Ingredient(advancedCircuit, 2));
            processingUnit.Ingredients.Add(new Ingredient(electronicCircuit, 20));
            processingUnit.Ingredients.Add(new Ingredient(sulfuricAcid, 5));
            processingUnit.ImageString = "Images\\Processing_unit.png";

            electronicCircuit.Producer = facAssemblingMachine;
            electronicCircuit.TimeToProduce = 0.5;
            electronicCircuit.TotalCreated = 1;
            electronicCircuit.Ingredients.Add(new Ingredient(copperCable, 3));
            electronicCircuit.Ingredients.Add(new Ingredient(ironPlate, 1));
            electronicCircuit.ImageString = "Images\\Electronic_circuit.png";

            advancedCircuit.Producer = facAssemblingMachine;
            advancedCircuit.TimeToProduce = 6;
            advancedCircuit.TotalCreated = 1;
            advancedCircuit.Ingredients.Add(new Ingredient(plasticBar, 2));
            advancedCircuit.Ingredients.Add(new Ingredient(copperCable, 4));
            advancedCircuit.Ingredients.Add(new Ingredient(electronicCircuit, 2));
            advancedCircuit.ImageString = "Images\\Advanced_circuit.png";

            plasticBar.Producer = facChemicalPlantBelt;
            plasticBar.TimeToProduce = 1;
            plasticBar.TotalCreated = 2;
            plasticBar.Ingredients.Add(new Ingredient(coal, 1));
            plasticBar.Ingredients.Add(new Ingredient(petroleumGas, 20));
            plasticBar.ImageString = "Images\\Plastic_bar.png";

            speedModule.Producer = facAssemblingMachine;
            speedModule.TimeToProduce = 15;
            speedModule.TotalCreated = 1;
            speedModule.Ingredients.Add(new Ingredient(electronicCircuit, 5));
            speedModule.Ingredients.Add(new Ingredient(advancedCircuit, 5));
            speedModule.ImageString = "Images\\Speed_module.png";

            sciencePack1.Producer = facAssemblingMachine;
            sciencePack1.TimeToProduce = 5;
            sciencePack1.TotalCreated = 1;
            sciencePack1.Ingredients.Add(new Ingredient(copperPlate, 1));
            sciencePack1.Ingredients.Add(new Ingredient(ironGearWheel, 1));
            sciencePack1.ImageString = "Images\\Science_pack_1.png";

            ironGearWheel.Producer = facAssemblingMachine;
            ironGearWheel.TimeToProduce = 0.5;
            ironGearWheel.TotalCreated = 1;
            ironGearWheel.Ingredients.Add(new Ingredient(ironPlate, 2));
            ironGearWheel.ImageString = "Images\\Iron_gear_wheel.png";

            ironPlate.Producer = facElectricFurnace;
            ironPlate.TimeToProduce = 3.5;
            ironPlate.TotalCreated = 1;
            ironPlate.Ingredients.Add(new Ingredient(ironOre, 1));
            ironPlate.ImageString = "Images\\Iron_plate.png";

            copperPlate.Producer = facElectricFurnace;
            copperPlate.TimeToProduce = 3.5;
            copperPlate.TotalCreated = 1;
            copperPlate.Ingredients.Add(new Ingredient(copperOre, 1));
            copperPlate.ImageString = "Images\\Copper_plate.png";

            sciencePack2.Producer = facAssemblingMachine;
            sciencePack2.TimeToProduce = 6;
            sciencePack2.TotalCreated = 1;
            sciencePack2.Ingredients.Add(new Ingredient(inserter, 1));
            sciencePack2.Ingredients.Add(new Ingredient(transportBelt, 1));
            sciencePack2.ImageString = "Images\\Science_pack_2.png";

            inserter.Producer = facAssemblingMachine;
            inserter.TimeToProduce = 0.5;
            inserter.TotalCreated = 1;
            inserter.Ingredients.Add(new Ingredient(electronicCircuit, 1));
            inserter.Ingredients.Add(new Ingredient(ironGearWheel, 1));
            inserter.Ingredients.Add(new Ingredient(ironPlate, 1));
            inserter.ImageString = "Images\\Inserter.png";


            sciencePack3.Producer = facAssemblingMachine;
            sciencePack3.TimeToProduce = 12;
            sciencePack3.TotalCreated = 1;
            sciencePack3.Ingredients.Add(new Ingredient(electricMiningDrill, 1));
            sciencePack3.Ingredients.Add(new Ingredient(advancedCircuit, 1));
            sciencePack3.Ingredients.Add(new Ingredient(engineUnit, 1));
            sciencePack3.ImageString = "Images\\Science_pack_3.png";

            electricMiningDrill.Producer = facAssemblingMachine;
            electricMiningDrill.TimeToProduce = 2;
            electricMiningDrill.TotalCreated = 1;
            electricMiningDrill.Ingredients.Add(new Ingredient(electronicCircuit, 3));
            electricMiningDrill.Ingredients.Add(new Ingredient(ironGearWheel, 5));
            electricMiningDrill.Ingredients.Add(new Ingredient(ironPlate, 10));
            electricMiningDrill.ImageString = "Images\\Electric_mining_drill.png";

            engineUnit.Producer = facAssemblingMachine;
            engineUnit.TimeToProduce = 10;
            engineUnit.TotalCreated = 1;
            engineUnit.Ingredients.Add(new Ingredient(ironGearWheel, 1));
            engineUnit.Ingredients.Add(new Ingredient(pipe, 2));
            engineUnit.Ingredients.Add(new Ingredient(steelPlate, 1));
            engineUnit.ImageString = "Images\\Engine_unit.png";

            pipe.Producer = facAssemblingMachine;
            pipe.TimeToProduce = 0.5;
            pipe.TotalCreated = 1;
            pipe.Ingredients.Add(new Ingredient(ironPlate, 1));
            pipe.ImageString = "Images\\Pipe.png";

            steelPlate.Producer = facElectricFurnace;
            steelPlate.TimeToProduce = 17.5;
            steelPlate.TotalCreated = 1;
            steelPlate.Ingredients.Add(new Ingredient(ironPlate, 5));
            steelPlate.ImageString = "Images\\Steel_plate.png";

            militarySciencePack.Producer = facAssemblingMachine;
            militarySciencePack.TimeToProduce = 10;
            militarySciencePack.TotalCreated = 2;
            militarySciencePack.Ingredients.Add(new Ingredient(piercingRoundsMagazine, 1));
            militarySciencePack.Ingredients.Add(new Ingredient(grenade, 1));
            militarySciencePack.Ingredients.Add(new Ingredient(gunTurret, 1));
            militarySciencePack.ImageString = "Images\\Military_science_pack.png";

            piercingRoundsMagazine.Producer = facAssemblingMachine;
            piercingRoundsMagazine.TimeToProduce = 3;
            piercingRoundsMagazine.TotalCreated = 1;
            piercingRoundsMagazine.Ingredients.Add(new Ingredient(copperPlate, 5));
            piercingRoundsMagazine.Ingredients.Add(new Ingredient(firearmMagazine, 1));
            piercingRoundsMagazine.Ingredients.Add(new Ingredient(steelPlate, 1));
            piercingRoundsMagazine.ImageString = "Images\\Piercing_rounds_magazine.png";

            firearmMagazine.Producer = facAssemblingMachine;
            firearmMagazine.TimeToProduce = 1;
            firearmMagazine.TotalCreated = 1;
            firearmMagazine.Ingredients.Add(new Ingredient(ironPlate, 4));
            firearmMagazine.ImageString = "Images\\Firearm_magazine.png";

            grenade.Producer = facAssemblingMachine;
            grenade.TimeToProduce = 8;
            grenade.TotalCreated = 1;
            grenade.Ingredients.Add(new Ingredient(coal, 10));
            grenade.Ingredients.Add(new Ingredient(ironPlate, 5));
            grenade.ImageString = "Images\\Grenade.png";

            gunTurret.Producer = facAssemblingMachine;
            gunTurret.TimeToProduce = 8;
            gunTurret.TotalCreated = 1;
            gunTurret.Ingredients.Add(new Ingredient(copperPlate, 10));
            gunTurret.Ingredients.Add(new Ingredient(ironGearWheel, 10));
            gunTurret.Ingredients.Add(new Ingredient(ironPlate, 20));
            gunTurret.ImageString = "Images\\Gun_turret.png";

            productionSciencePack.Producer = facAssemblingMachine;
            productionSciencePack.TimeToProduce = 14;
            productionSciencePack.TotalCreated = 2;
            productionSciencePack.Ingredients.Add(new Ingredient(electricEngineUnit, 1));
            productionSciencePack.Ingredients.Add(new Ingredient(electricFurnace, 1));
            productionSciencePack.ImageString = "Images\\Production_science_pack.png";

            electricEngineUnit.Producer = facAssemblingMachine;
            electricEngineUnit.TimeToProduce = 10;
            electricEngineUnit.TotalCreated = 1;
            electricEngineUnit.Ingredients.Add(new Ingredient(electronicCircuit, 2));
            electricEngineUnit.Ingredients.Add(new Ingredient(engineUnit, 1));
            electricEngineUnit.Ingredients.Add(new Ingredient(lubricant, 15));
            electricEngineUnit.ImageString = "Images\\Electric_engine_unit.png";

            lubricant.Producer = facChemicalPlantPipe;
            lubricant.TimeToProduce = 1;
            lubricant.TotalCreated = 10;
            lubricant.Ingredients.Add(new Ingredient(heavyOil, 10));
            lubricant.ImageString = "Images\\Lubricant.png";

            electricFurnace.Producer = facAssemblingMachine;
            electricFurnace.TimeToProduce = 5;
            electricFurnace.TotalCreated = 1;
            electricFurnace.Ingredients.Add(new Ingredient(advancedCircuit, 5));
            electricFurnace.Ingredients.Add(new Ingredient(steelPlate, 10));
            electricFurnace.Ingredients.Add(new Ingredient(stoneBrick, 10));
            electricFurnace.ImageString = "Images\\Electric_furnace.png";

            stoneBrick.Producer = facElectricFurnace;
            stoneBrick.TimeToProduce = 3.5;
            stoneBrick.TotalCreated = 1;
            stoneBrick.Ingredients.Add(new Ingredient(stone, 2));
            stoneBrick.ImageString = "Images\\Stone_brick.png";

            cliffExplosives.Producer = facAssemblingMachine;
            cliffExplosives.TimeToProduce = 8;
            cliffExplosives.TotalCreated = 1;
            cliffExplosives.Ingredients.Add(new Ingredient(emptyBarell, 1));
            cliffExplosives.Ingredients.Add(new Ingredient(explosives, 10));
            cliffExplosives.Ingredients.Add(new Ingredient(grenade, 1));
            cliffExplosives.ImageString = "Images\\Cliff_explosives.png";

            emptyBarell.Producer = facAssemblingMachine;
            emptyBarell.TimeToProduce = 1;
            emptyBarell.TotalCreated = 1;
            emptyBarell.Ingredients.Add(new Ingredient(steelPlate, 1));
            emptyBarell.ImageString = "Images\\Empty_barrel.png";

            explosives.Producer = facChemicalPlantBelt;
            explosives.TimeToProduce = 5;
            explosives.TotalCreated = 2;
            explosives.Ingredients.Add(new Ingredient(coal, 1));
            explosives.Ingredients.Add(new Ingredient(sulfur, 1));
            explosives.Ingredients.Add(new Ingredient(water, 10));
            explosives.ImageString = "Images\\Explosives.png";

            ironOre.Producer = facElectricMiningDrill;
            ironOre.TimeToProduce = 40.0 / 21;
            ironOre.TotalCreated = 1;
            ironOre.ImageString = "Images\\Iron_ore.png";

            copperOre.Producer = facElectricMiningDrill;
            copperOre.TimeToProduce = 40.0 / 21;
            copperOre.TotalCreated = 1;
            copperOre.ImageString = "Images\\Copper_ore.png";

            coal.Producer = facElectricMiningDrill;
            coal.TimeToProduce = 40.0 / 21;
            coal.TotalCreated = 1;
            coal.ImageString = "Images\\Coal.png";

            stone.Producer = facElectricMiningDrill;
            stone.TimeToProduce = 40.0 / 26;
            stone.TotalCreated = 1;
            stone.ImageString = "Images\\Stone.png";

            water.Producer = facOffshorePump;
            water.TimeToProduce = 1;
            water.TotalCreated = 1200;
            water.ImageString = "Images\\Water.png";

            petroleumGas.Producer = facOilRefinery;
            petroleumGas.TimeToProduce = 5;
            petroleumGas.TotalCreated = 90;
            petroleumGas.ImageString = "Images\\Petroleum_gas.png";

            lightOil.Producer = facOilRefinery;
            lightOil.TimeToProduce = 5;
            lightOil.TotalCreated = 45;
            lightOil.ImageString = "Images\\Light_oil.png";

            heavyOil.Producer = facOilRefinery;
            heavyOil.TimeToProduce = 5;
            heavyOil.TotalCreated = 30;
            heavyOil.ImageString = "Images\\Heavy_oil.png";

            // Add all to the list.
            // Logistics
            products.Add(woodenChest);
            products.Add(ironChest);
            products.Add(steelChest);
            products.Add(storageTank);

            products.Add(transportBelt);
            products.Add(fastTransportBelt);
            products.Add(expressTransportBelt);
            products.Add(undergroundBelt);
            products.Add(fastUndergroundBelt);
            products.Add(expressUndergroundBelt);
            products.Add(splitter);
            products.Add(fastSplitter);
            products.Add(expressSplitter);

            products.Add(burnerInserter);
            products.Add(inserter);
            products.Add(longHandedInserter);
            products.Add(fastInserter);
            products.Add(filterInserter);
            products.Add(stackInserter);
            products.Add(stackFilterInserter);

            products.Add(smallElectricPole);
            products.Add(mediumElectricPole);
            products.Add(bigElectricPole);
            products.Add(substation);
            products.Add(pipe);
            products.Add(pipeToGround);
            products.Add(pump);

            products.Add(rail);
            products.Add(trainStop);
            products.Add(railSignal);
            products.Add(railChainSignal);
            products.Add(locomotive);
            products.Add(cargoWagon);
            products.Add(fluidWagon);
            products.Add(artilleryWagon);
            products.Add(car);
            products.Add(tank);

            products.Add(logisticRobot);
            products.Add(constructionRobot);
            products.Add(activeProviderChest);
            products.Add(passiveProviderChest);
            products.Add(storageChest);
            products.Add(bufferChest);
            products.Add(requesterChest);
            products.Add(roboport);

            products.Add(lamp);
            products.Add(redWire);
            products.Add(greenWire);
            products.Add(arithmeticCombinator);
            products.Add(deciderCombinator);
            products.Add(constantCombinator);
            products.Add(powerSwitch);
            products.Add(programmableSpeaker);

            products.Add(stoneBrick);
            products.Add(concrete);
            products.Add(hazardConcrete);
            products.Add(refinedConcrete);
            products.Add(refinedHazardConcrete);
            products.Add(landfill);
            products.Add(cliffExplosives);

            // Production
            products.Add(ironAxe);
            products.Add(steelAxe);
            products.Add(repairPack);
            products.Add(blueprint);
            products.Add(deconstructionPlanner);
            products.Add(blueprintBook);

            products.Add(boiler);
            products.Add(steamEngine);
            products.Add(steamTurbine);
            products.Add(solarPanel);
            products.Add(accumulator);
            products.Add(nuclearReactor);
            products.Add(heatExchanger);
            products.Add(heatPipe);

            products.Add(burnerMiningDrill);
            products.Add(electricMiningDrill);
            products.Add(offshorePump);
            products.Add(pumpjack);

            products.Add(stoneFurnace);
            products.Add(steelFurnace);
            products.Add(electricFurnace);

            products.Add(assembligMachine1);
            products.Add(assemblingMachine2);
            products.Add(assemblingMachine3);
            products.Add(oilRefinery);
            products.Add(chemicalPlant);
            products.Add(centrifuge);
            products.Add(lab);

            products.Add(beacon);
            products.Add(speedModule);
            products.Add(speedModule2);
            products.Add(speedModule3);
            products.Add(efficiencyModule);
            products.Add(efficiencyModule2);
            products.Add(efficiencyModule3);
            products.Add(productivityModule);
            products.Add(productivityModule2);
            products.Add(productivityModule3);

            // Intermediate Products
            products.Add(rawWood);
            products.Add(coal);
            products.Add(stone);
            products.Add(ironOre);
            products.Add(copperOre);
            products.Add(uraniumOre);
            products.Add(rawFish);
            products.Add(crudeOil);
            products.Add(heavyOil);
            products.Add(lubricant);
            products.Add(lightOil);
            products.Add(petroleumGas);
            products.Add(sulfuricAcid);
            products.Add(water);
            products.Add(steam);

            products.Add(wood);
            products.Add(ironPlate);
            products.Add(copperPlate);
            products.Add(solidFuel);
            products.Add(solidFuelIngredients);
            products.Add(steelPlate);
            products.Add(plasticBar);
            products.Add(sulfur);
            products.Add(battery);
            products.Add(explosives);
            products.Add(uraniumProcessing);

            products.Add(crudeOilBarrel);
            products.Add(heavyOilBarrel);
            products.Add(lightOilBarrel);
            products.Add(lubricantBarrel);
            products.Add(petroleumGasBarrel);
            products.Add(sulfuricAcidBarrel);
            products.Add(waterBarrel);

            products.Add(copperCable);
            products.Add(ironStick);
            products.Add(ironGearWheel);
            products.Add(emptyBarell);
            products.Add(electronicCircuit);
            products.Add(advancedCircuit);
            products.Add(processingUnit);
            products.Add(engineUnit);
            products.Add(electricEngineUnit);
            products.Add(flyingRobotFrame);
            products.Add(satellite);
            products.Add(rocketPart);
            products.Add(rocketControlUnit);
            products.Add(lowDensityStructure);
            products.Add(rocketFuel);
            products.Add(nuclearFuel);
            products.Add(uranium235);
            products.Add(uranium238);

            products.Add(uraniumFuelCell);
            products.Add(usedUpUraniumFuelCell);
            products.Add(nuclearFuelReprocessing);
            products.Add(kovarexEnrichmentProcess);

            products.Add(sciencePack1);
            products.Add(sciencePack2);
            products.Add(sciencePack3);
            products.Add(militarySciencePack);
            products.Add(productionSciencePack);
            products.Add(highTechSciencePack);
            products.Add(spaceSciencePack);

            // Combat


            // Machine processes
            products.Add(advancedOilProcessing);
            products.Add(basicOilProcessing);
            products.Add(heavyCracking);
            products.Add(lightCracking);
            products.Add(solidFuelFromHeavyOil);
            products.Add(solidFuelFromLightOil);
            products.Add(solidFuelFromPetroleumGas);

            // Old sorting
            products.Add(piercingRoundsMagazine);
            products.Add(firearmMagazine);
            products.Add(grenade);
            products.Add(gunTurret);


            // Add all to the dictionary
            foreach (Product product in products)
            {
                Dictionary.Add(product.Name, product);
            }
        }
    }
}
