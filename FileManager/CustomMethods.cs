using System;
using System.Collections.Generic;

namespace FileManager
{
    /// <summary>
    /// Класс кастомных методов
    /// </summary>
    public static class CustomMethods
    {
        /// <summary>
        /// Вычисление префикс функций
        /// </summary>
        /// <param name="source">Строка для которой вычисляется функция</param>
        /// <returns>Массив префикс функций</returns>
        public static int[] GetPrefixFunc(string source)
        {
            int[] prefixFunc = new int[source.Length];
            
            if(source.Length == 0)
            {
                return prefixFunc;
            }

            prefixFunc[0] = 0;
            int index;

            for (int i = 1; i < source.Length; ++i)
            {
                index = prefixFunc[i - 1];
                while (source[index] != source[i] && index > 0)
                {
                    index = prefixFunc[index - 1];
                }
                if (source[index] == source[i])
                {
                    prefixFunc[i] = index + 1;
                }
                else
                {
                    prefixFunc[i] = 0;
                }
            }

            return prefixFunc;
        }

        /// <summary>
        /// Поиск подстроки в строке
        /// </summary>
        /// <param name="target">Подстрока</param>
        /// <param name="source">Строка</param>
        /// <returns>Список с начальными индексами вхождения</returns>
        public static List<int> FindSubstring(string target, string source)
        {
            List<int> indexes = new List<int>();
            
            if(target.Length > source.Length || target.Length == 0 || source.Length == 0)
            {
                return indexes;
            } 
            else if(target.Length == source.Length)
            {
                for(int i = 0; i < target.Length; i++)
                {
                    if(target[i] != source[i])
                    {
                        return indexes;
                    }
                }
            }
            
            int[] prefixFunc = GetPrefixFunc(target);
            int size = 0;
            int index = 0;
            for (int i = 0; i < source.Length; ++i)
            {
                while (target[index] != source[i] && index > 0)
                {
                    index = prefixFunc[index - 1];
                }
                if (target[index] == source[i])
                {
                    index = index + 1;
                    if (index == target.Length)
                    {
                        indexes.Add(i + 1 - index);
                        size += 1;
                        index = prefixFunc[index - 1];
                    }
                }
                else
                {
                    index = 0;
                }
            }
            return indexes;
        }

        /// <summary>
        /// Разделение строки на массив подстрок
        /// </summary>
        /// <param name="target">Строка разделитель</param>
        /// <param name="source">Исходная строка</param>
        /// <returns>Массив подстрок</returns>
        public static List<string> SplitString(string target, string source)
        {
            List<string> result = new List<string>();
            result.Add("");
            List<int> index = FindSubstring(target, source);
            int oldIndex = 0;
            int indexList = 0;
            int length = 0;
            foreach(int i in index)
            {
                result[indexList] = source.Substring(0, i - oldIndex - length);
                oldIndex = i;
                length = target.Length;
                source = source.Remove(0, (result[indexList].Length + length));
                result.Add(source);
                indexList++;
            }
            return result;
        }
    }
}
