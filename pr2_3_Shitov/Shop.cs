using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace pr2_3_Shitov
{
    internal class Shop
    {
        private Dictionary<Product, int> products;
        public decimal Profit { get; set; }

        public Shop()
        {
            products = new Dictionary<Product, int>();
            Profit = 0;
        }

        // Получить все товары (для отображения в DataGrid)
        public List<KeyValuePair<Product, int>> GetAllProducts()
        {
            return products.ToList();
        }

        // Создать и добавить новый товар
        public void CreateProduct(string name, decimal price, int count)
        {
            var product = new Product(name, price);
            products.Add(product, count);
        }
        // Продать товар
        public void Sell(Product product)
        {
            if (products.ContainsKey(product))
            {
                if (products[product] == 0)
                {
                    MessageBox.Show("Нет в наличии!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    products[product]--;
                    Profit += product.Price;
                }
            }
            else
            {
                MessageBox.Show("Товар не найден!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // ПЕРЕГРУЗКА по названию
        public void Sell(string productName)
        {
            Product product = FindByName(productName);

            if (product != null)
            {
                Sell(product);
            }
            else
            {
                MessageBox.Show($"Товар '{productName}' не найден!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Найти товар по названию
        public Product FindByName(string name)
        {
            return products.Keys.FirstOrDefault(p => p.Name == name);
        }
    }
}