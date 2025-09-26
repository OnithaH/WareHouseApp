using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseApp.Material
{
    public class Material
    {
        public int materialID;
        public string materialName;
        private int materialCount;
        public decimal Price { get; set; }

        public int MaterialCount
        {
            get { return materialCount; }
            set { materialCount = value; }
        }

        // Constructor
        public Material() { }

        public Material(string name, int count, decimal price)
        {
            this.materialName = name;
            this.materialCount = count;
            this.Price = price;
        }
    }
}