using zad1_Shitov;

class Program 
{   
    static void Main(string[] args)
    {
        /*Cat murzik = new Cat("Мурзик", 6.01);
        Cat barsik = new Cat("Барсег", 4.21);
        murzik.Meow();
        barsik.Meow(); 
        barsik.Name = "Барсик";
        barsik.Meow();
        barsik.Name = "1234";
        barsik.Meow();*/
        int count = 0;
        string name;
        double weight;
        do
        {
            Console.WriteLine("Сколько котов хотите ввети?");
            count = Convert.ToInt32(Console.ReadLine());
        } while (count < 1);
        Cat[] cats = new Cat[count];
        Console.WriteLine($"===Заполнение {count} котов===");
        for (int i = 0; i < count; i++)
        {
            Cat tempCat = new Cat("temp", 1.0);
            do
            {
                Console.Write($"Введите имя {i + 1} кота: ");
                string inputName = Console.ReadLine();
                tempCat.Name = inputName;

            } while (tempCat.Name == "ERROR");
            do
            {
                Console.Write($"Введите вес {i + 1} кота (кг): ");
                string input = Console.ReadLine();

                double inputWeight;
                if (!double.TryParse(input, out inputWeight))
                {
                    inputWeight = -1;
                }

                tempCat.Weight = inputWeight;

            } while (tempCat.Weight == -1);

            cats[i] = new Cat(tempCat.Name, tempCat.Weight);
        }

        Console.WriteLine("\n=== Информация о котах ===");
        for (int i = 0; i < count; i++)
        {
            Console.Write($"Кот {i + 1}: ");
            cats[i].CatInfo(); 
        }

        Console.WriteLine("\n=== Все коты мяукают ===");
        for (int i = 0; i < count; i++)
        {
            cats[i].Meow();
        }
    }   
}