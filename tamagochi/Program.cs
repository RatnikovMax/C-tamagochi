using System;
using System.Collections.Generic;
using System.Threading;

class Game
{
    public int Cheerfulness { get; private set; }
    public int Energy { get; private set; }
    public int Hunger { get; private set; }
    public int UniqueNeed { get; private set; }
    public string UniqueNeedName { get; private set; }
    public string Name { get; private set; }

    public Game(string name, string uniqueNeedName)
    {
        Name = name;
        UniqueNeedName = uniqueNeedName;
        Cheerfulness = 100;
        Energy = 100;
        Hunger = 100;
        UniqueNeed = 100;
    }

    public void Feed()
    {
        if (Hunger < 100)
        {
            Hunger += 10;
            if (Hunger > 100) Hunger = 100;
            Console.WriteLine($"{Name} был накормлен. Уровень голода: {Hunger}");
        }
        else
        {
            Console.WriteLine($"{Name} не голоден.");
        }
    }

    public void SatisfyUniqueNeed()
    {
        if (UniqueNeed < 100)
        {
            UniqueNeed += 10;
            if (UniqueNeed > 100) UniqueNeed = 100;
            Console.WriteLine($"Вы удовлетворили уникальную потребность {Name} ({UniqueNeedName}). Уровень: {UniqueNeed}");
        }
        else
        {
            Console.WriteLine($"Уникальная потребность {Name} уже удовлетворена.");
        }
    }

    public void Sleep()
    {
        if (Energy < 100)
        {
            Energy += 20;
            if (Energy > 100) Energy = 100;
            Console.WriteLine($"{Name} спит. Энергия восстановлена: {Energy}");
        }
        else
        {
            Console.WriteLine($"{Name} уже полностью отдохнул.");
        }
    }

    public void DecreaseNeedsOverTime()
    {
        Cheerfulness = Math.Max(Cheerfulness - 2, 0);
        Energy = Math.Max(Energy - 1, 0);
        Hunger = Math.Max(Hunger - 3, 0);
        UniqueNeed = Math.Max(UniqueNeed - 1, 0);
    }

    public void ShowStatus()
    {
        Console.WriteLine($"Имя питомца: {Name}");
        Console.WriteLine($"Радость: {Cheerfulness}");
        Console.WriteLine($"Энергия: {Energy}");
        Console.WriteLine($"Голод: {Hunger}");
        Console.WriteLine($"Уникальная потребность ({UniqueNeedName}): {UniqueNeed}");
    }

    public bool IsGameOver()
    {
        int zeroCount = 0;
        if (Cheerfulness == 0) zeroCount++;
        if (Energy == 0) zeroCount++;
        if (Hunger == 0) zeroCount++;
        if (UniqueNeed == 0) zeroCount++;

        return zeroCount >= 3;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Выберите питомца:");
        Console.WriteLine("1. Дракон");
        Console.WriteLine("2. Единорог");
        string choice = Console.ReadLine();

        Game game;

        if (choice == "1")
        {
            game = new Game("Дракон", "Полет");
        }
        else if (choice == "2")
        {
            game = new Game("Единорог", "Магия");
        }
        else
        {
            Console.WriteLine("Неверный выбор!");
            return;
        }

        if (game == null)
        {
            Console.WriteLine("Неверный выбор!");
            return;
        }

        bool gameRunning = true;
        DateTime lastDecreaseTime = DateTime.Now;

        while (gameRunning)
        {
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("1. Покормить питомца");
            Console.WriteLine("2. Удовлетворить уникальную потребность");
            Console.WriteLine("3. Показать статус питомца");
            Console.WriteLine("4. Уложить питомца спать");
            Console.WriteLine("5. Выйти из игры");

            string action = Console.ReadLine();

            switch (action)
            {
                case "1":
                    game.Feed();
                    break;
                case "2":
                    game.SatisfyUniqueNeed();
                    break;
                case "3":
                    game.ShowStatus();
                    break;
                case "4":
                    Console.WriteLine("Питомец спит...");
                    Thread.Sleep(5000); // Задержка в 5 секунд
                    game.Sleep();
                    break;
                case "5":
                    gameRunning = false;
                    break;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }

            if ((DateTime.Now - lastDecreaseTime).TotalSeconds >= 1)
            {
                game.DecreaseNeedsOverTime();
                game.ShowStatus();
                lastDecreaseTime = DateTime.Now;
            }

            if (game.IsGameOver())
            {
                Console.WriteLine($"Ваш питомец {game.Name} сбежал!");
                gameRunning = false;
            }
        }

        Console.WriteLine("Игра окончена.");
    }
}