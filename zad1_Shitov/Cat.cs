using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad1_Shitov
{
    internal class Cat
    {
        private string name;
        private double weight;
        public Cat(string CatName, double CatWeight) { Name = CatName; Weight = CatWeight; }
        public void Meow() { Console.WriteLine($"{name}: МЯЯЯЯУ!!!!"); }
        public void CatInfo()
        {
            Console.WriteLine($"{name}, вес: {weight} кг");
        }
        public void SetCatName(string CatName)
        {
            bool OnlyLetters = true;
            foreach (var ch in CatName)
            {
                if (!char.IsLetter(ch)) 
                {
                    OnlyLetters = false; 
                }
            } 
            if (OnlyLetters) name = CatName;
            else
                Console.WriteLine($"{CatName} - неправильное имя!!!"); 
        }
        public string Name
        {
            get { return name; }
            set
            {
                bool onlyLetters = true;
                if (string.IsNullOrWhiteSpace(value))
                {
                    Console.WriteLine("Имя не может быть пустым!!!");
                    name = "ERROR";
                    return;
                }
                foreach (var ch in value)
                {
                    if (!char.IsLetter(ch))
                    {
                        onlyLetters = false;
                        break;
                    }
                }
                if (onlyLetters)
                {
                    name = value;
                }
                else
                {
                    Console.WriteLine($"{value} - неправильное имя!!!");
                    name = "ERROR";
                }
            }
        }
        public double Weight
        {
            get { return weight; }
            set
            {
                if (value > 0.1 && value < 15.0)
                {
                    weight = value;
                }
                else
                {
                    Console.WriteLine($"{value} - Неправильный вес!!!");
                    weight = -1;
                }
            }
        }
    }
}
