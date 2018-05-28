using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioSolver
{
    /// <summary>
    /// Data about an item that can be crafted.
    /// </summary>
    class Product
    {
        public string Name { get; }
        public double TimeToProduce { get; }
        public int TotalCreated { get; } // The number of this type of item that are created when it is crafted.
        public List<Ingredient> Ingredients { get; set; }
        public bool UsesBelt { get; }

        public Product (string name, double timeToProduce, int totalCreated, bool usesBelt)
        {
            Name = name;
            TimeToProduce = timeToProduce;
            TotalCreated = totalCreated;
            UsesBelt = usesBelt;
            Ingredients = new List<Ingredient>();
        }

    }

    /// <summary>
    /// Item to track an ingredient of crafting.
    /// </summary>
    class Ingredient
    {
        public Product Product { get; }
        public int Amount { get; }

        public Ingredient(Product product, int amount)
        {
            Product = product;
            Amount = amount;
        }
    }
}
