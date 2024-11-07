using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Reflection;

namespace dotNetLab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string message = "Hello world! Hello world! Hello world!";
            string elem = "o";

            string invertMessage = message.InvertLine();
            Console.WriteLine("Розширенні методи для String");
            Console.WriteLine($"Початковий рядок: {message}");
            Console.WriteLine($"Перевернутий рядок: {invertMessage}");

            int count = message.ContainLine(elem);
            Console.WriteLine($"Кількість елементу '{elem}' в рядку: {count}");

            Console.WriteLine("\n\n");
            Console.WriteLine("Розширенні методи для одновимірних масивів");
            int[] num = { 2, 2, 3, 1, 5, 6, 2, 2, 9, 10 };

            Console.WriteLine($"Масив чисел: {string.Join(", ", num)}");
            count = num.CountElemtsInArr(2);

            Console.WriteLine($"Кількість елементу '2' в масиві: {count}");
            string[] words = { "football", "voleyball", "voleyball", "tennis" };
            Console.WriteLine($"Масив: {string.Join(", ", words)}");

            count = words.CountElemtsInArr("voleyball");
            Console.WriteLine($"Кількість елементу 'voleyball' в масиві: {count}");

            string[] uniqueWords = words.GetUniqueElements();
            Console.WriteLine($"Унікальні елементи в масиві: {string.Join(", ", uniqueWords)}");

            Console.WriteLine("\n\n");
            var accountDictionary = new ExtendedDictionary<string, string, string>();
            accountDictionary.Add("tinko", "Віталій", "vitalik@gmail.com");
            accountDictionary.Add("marimo", "Василь", "vasya@gmail.com");
            accountDictionary.Add("Scorp", "Богдан", "bogdan@gmail.com");

            Console.WriteLine("Виведення словника:");
            foreach (var user in accountDictionary)
            {
                Console.WriteLine($"Логін: {user.Key}, Ім'я: {user.Value1}, Email: {user.Value2}");
            }
            Console.WriteLine("Кiлькiсть елементiв у словнику: " + accountDictionary.Count());
            Console.WriteLine("Чи мiстить логін 'marimo': " + accountDictionary.ContainsKey("marimo"));

            Console.WriteLine("Чи мiстить значення 'Василь' та 'vasya@gmail.com': " +
                              accountDictionary.ContainsValue("Василь", "vasya@gmail.com"));
            Console.WriteLine("Чи мiстить значення 'Дмитро' та 'dima@gmail.com': " +
                             accountDictionary.ContainsValue("Дмитро", "vasya@gmail.com"));

            var userElement = accountDictionary["marimo"];
            Console.WriteLine($"Виведення даних за ключем 'marimo': Логін: {userElement.Key}, Ім'я: {userElement.Value1}, Email: {userElement.Value2}");

            Console.WriteLine($"Видалення аккаунта за логіном 'marimo': {accountDictionary.Remove("marimo")}");

            Console.WriteLine("Кiлькiсть елементiв пiсля видалення: " + accountDictionary.Count());

        }
    }

    public static class StringExtensions
    {
        public static string InvertLine(this string line)
        {
            string newLine = "";
            for (int i = line.Length - 1; i >= 0; i--)
            {
                newLine += line[i];
            }
            return newLine;
        }

        public static int ContainLine(this string line, string elem)
        {
            int count = 0;
            int index = 0;
            line = line.ToLower();
            elem = elem.ToLower();

            while ((index = line.IndexOf(elem, index)) != -1)
            {
                count++;
                index += elem.Length;
            }

            return count;
        }
    }
    public static class ArrayExtensions
    {
        public static int CountElemtsInArr<Type>(this Type[] array, Type value) where Type : IEquatable<Type>
        {
            int count = 0;
            foreach (Type element in array)
            {
                if (element.Equals(value))
                    count++;
            }
            return count;
        }
        public static Type[] GetUniqueElements<Type>(this Type[] array)
        {
            HashSet<Type> uniqueEl = new HashSet<Type>(array);
            return uniqueEl.ToArray();
        }
    }
    public class ExtendedDictionaryElement<T, U, V>
    {
        public T Key { get; }
        public U Value1 { get; }
        public V Value2 { get; }

        public ExtendedDictionaryElement(T key, U value1, V value2)
        {
            Key = key;
            Value1 = value1;
            Value2 = value2;
        }
    }
    public class ExtendedDictionary<T, U, V> : IEnumerable<ExtendedDictionaryElement<T, U, V>>
    {
        private Dictionary<T, ExtendedDictionaryElement<T, U, V>> elements = new Dictionary<T, ExtendedDictionaryElement<T, U, V>>();

        public bool Add(T key, U value1, V value2)
        {
            if (!elements.ContainsKey(key))
            {
                elements[key] = new ExtendedDictionaryElement<T, U, V>(key, value1, value2);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Remove(T key)
        {
            return elements.Remove(key);
        }
        public bool ContainsKey(T key)
        {
            return elements.ContainsKey(key);
        }
        public bool ContainsValue(U value1, V value2)
        {
            foreach (var element in elements.Values)
            {
                if (EqualityComparer<U>.Default.Equals(element.Value1, value1) && EqualityComparer<V>.Default.Equals(element.Value2, value2))
                {
                    return true;
                }
            }
            return false;
        }
        public ExtendedDictionaryElement<T, U, V> this[T key]
        {
            get
            {
                if (elements.ContainsKey(key))
                {
                    return elements[key];
                }
                return null;
            }
        }
        public int Count()
        {
            return elements.Count;
        }

        public IEnumerator<ExtendedDictionaryElement<T, U, V>> GetEnumerator()
        {
            return elements.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}