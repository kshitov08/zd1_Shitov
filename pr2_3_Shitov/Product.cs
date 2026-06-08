using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace pr2_3_Shitov
{
    internal class Product
    {
        private decimal price;
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
            }
        }
        public decimal Price
        {
            get { return price; }
            set
            {
                price = value;
            }
        }
        public Product(string name, decimal price)
        {
            Name = name; Price = price;
        }
        public string GetInfo()
        { return $"Наименование: {name}; Цена: {price}"; }
    }
}
