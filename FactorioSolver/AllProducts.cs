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
            Product highTechPack = new Product("High Tech Pack", 14, 2);
            Product battery = new Product("Battery", 5, 1);
            Product sulfuricAcid = new Product("Sulfuric Acid", 1, 50);
            Product sulfur = new Product("Sulfur", 1, 2);
            Product petroleumGas = new Product("Petroleum Gas", 5, 55);
            Product lightOil = new Product("Light Oil", 5, 45);
            Product heavyOil = new Product("Heavy Oil", 5, 10);
            Product copperCable = new Product("CopperCable", 0.5, 2);
            Product processingUnit = new Product("Processing Unit", 10, 1);
            Product electronicCircuit = new Product("Electronic Circuit", 0.5, 1);
            Product advancedCircuit = new Product("Advanced Circuit", 6, 1);
            Product plasticBar = new Product("Plastic Bar", 1, 1);
            Product speedModule = new Product("Speed Module", 15, 1);
            Product ironPlate = new Product("Iron Plate", 3.5, 1);
            Product copperPlate = new Product("Copper Plate", 3.5, 1);
            Product water = new Product("Water", 1, 1200);

            // Resources that don't have real stats yet.
            Product coal = new Product("Coal", 1, 1);
            

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


            // Add all to the dictionary
            foreach (Product product in products)
            {
                Dictionary.Add(product.Name, product);
            }
        }
    }
}
