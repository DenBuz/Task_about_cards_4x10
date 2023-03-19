﻿using System;
using System.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        private static readonly string[] masColor = { "R", "G", "B", "W" };
        private static readonly string[] masNumeric = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };

        static void Main(string[] args)
        {
            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            string[] cards = InitialCards(masColor, masNumeric); // массив для колоды
            int[,] gamers = null; // команды игроков с номерами карт
            int c, n;
            Console.WriteLine("Команда 'start N C' для раздачи N случайных карт С игрокам;");
            Console.WriteLine("команда 'get-cards C', где C - номер игрока;");
            Console.WriteLine("команда 'exit' для выхода.");
            while (true)
            {
                Console.Write("\nКоманда: ");
                string[] com = Console.ReadLine().Split(' ');
                switch (com[0])
                {
                    case "exit": return;
                    case "start":
                        Shuffle(ref cards); // перемешиваем колоду
                        n = int.Parse(com[1]);
                        c = int.Parse(com[2]);
                        if (n * c > cards.Length)
                        {
                            Console.WriteLine("Карт не хватает! Повторите ввод.");
                            continue;
                        }
                        gamers = new int[c, n]; // создаём команды игроков
                        for (int i = 0; i < c; i++)
                        {
                            for (int j = 0; j < n; j++)
                            {
                                gamers[i, j] = j + (i * n);
                            }
                        }
                        break;
                    case "get-cards":
                        if (gamers == null)
                        {
                            Console.WriteLine("Игроков ещё нет! Создайте команды игроков.");
                            continue;
                        }
                        c = int.Parse(com[1]);
                        if (c > gamers.GetLength(0) || c < 1)
                        {
                            Console.WriteLine("Игрока с таким номеров не существует!");
                            continue;
                        }
                        Console.Write($"{c}");
                        for (int i = 0; i < gamers.GetLength(1); i++)
                        {
                            Console.Write($" {cards[gamers[c - 1, i]]}");
                        }

                        Console.WriteLine();
                        break;
                    default:
                        Console.WriteLine("Неизвестная команда. Повторите.");
                        break;
                }
            }
        }

        private static string[] InitialCards(string[] mas1, string[] mas2) // заполнение массива
        {
            string[] result = new string[mas1.Length * mas2.Length];
            for (int i = 0; i < mas1.Length; i++)
            {
                for (int j = 0; j < mas2.Length; j++) result[j + i * mas2.Length] = mas1[i] + mas2[j];
            }
            return result;
        }

        private static void Shuffle(ref string[] mas) // перемешивание массива
        {
            Random r = new Random();
            mas = mas.OrderBy(x => r.Next()).ToArray();
        }
    }
}
