using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CrossInformTest
//    Тестовое задание
//    Необходимо написать консольное приложение на С#, выполняющее частотный анализ текста.
//    Входные параметры:путь к текстовому файлу.
//    Выходные резлуьтаты: вывести на экран черезе запятую 10 самых часто встречающихся в
// тексте триплетов(3 идущих подряд буквы слова), и на следующей строке время работы прог-
// раммы в миллисекундах.
//    Требования: программа должна обрабатывать текст в многопоточном режиме, и поддерживать
// отмену обработки на нажатие любой клавиши, после чего на экран должны выводиться текущие
// реузльтаты.
//    Оцениваться будет правильность решения, качество кода и быстродействие программы.
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Введите путь к файлу");
            Stopwatch stopWatch = new Stopwatch();
            try
            {
                Console.WriteLine("Укажите путь к вашему файлу с его названием в формате: C:\\Users\\1\\Documents\\vlastelin_kolec_bratstvo_kol_ca.txt");
                string path = Console.ReadLine();
                stopWatch.Start();//запуск таймера работы программы
             
                string[] text = File.ReadAllLines($@"{path}");//формирование массива строк из указанного файла
                
                Dictionary<string, int> DictionaryOfTriplets = new Dictionary<string, int>();//объявление словоря, где триплет-это ключ,а количество его повторов - значение
                
                int _counterOfTriplets = 1;//счётчик для подсчёта количества конкретных триплетов в общем списке

                foreach (var line in text)
                {
                    string[] words = line.Split(' ');//разбиение строк на слова и занесение их в массив строк
                    foreach (var word in words)
                    {   
                        char[] wordAsArray = word.ToLower().ToCharArray();//разбиение слова на буквы-символы и перевод их в нижний регистр с последующим занесением в массив
                        
                        if (wordAsArray.Length >= 3 )
                        {
                            
                            for (int j = 0; j <= wordAsArray.Length-3; j++)//формирование триплетов из символов
                            {
                                string triplet = null;
                                
                                for (int i = j; i <j+ 3; i++)
                                {
                                        triplet += wordAsArray[i].ToString();
                                }

                                if (DictionaryOfTriplets.ContainsKey(triplet) != true)//проверка наличия полученного триплета в словаре триплетов
                                {
                                    
                                    DictionaryOfTriplets.Add(triplet, _counterOfTriplets);//если он новый, то добавляется в словарь со значением 1
  
                                }
                                else
                                {
                                    DictionaryOfTriplets[triplet] +=1;//если уже присутствует, его значение в словаре увеличится на единицу
                                }

                              _counterOfTriplets = 0; 
                            }
                            

                        }
                        
                    }
                    
                }
                
                int _maxValue = 0; //наибольшее число повторов среди триплетов
                 _maxValue = MaxValue(_maxValue, DictionaryOfTriplets)+1;
                int _counterOfCycles = 0;

                while (_counterOfCycles < 10)//вывод 10 триплетов с наибольшим числом повторов в тексте
                {
                    _maxValue -=   1;

                    if (DictionaryOfTriplets.Values.Contains(_maxValue))
                    {
                        foreach (string key in DictionaryOfTriplets.Keys)
                        {
                            if (DictionaryOfTriplets[key] == _maxValue)
                            {
                                if (_counterOfCycles == 9)
                                {
                                    Console.WriteLine($"{key}.");
                                    _counterOfCycles++;
                                }
                                else
                                {
                                    Console.Write($"{key},");
                                    _counterOfCycles++;
                                }
                                
                            }
                            
                        }
                    }
                   
                }   

            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            finally{
                stopWatch.Stop();
                // Get the elapsed time as a TimeSpan value.
                TimeSpan ts = stopWatch.Elapsed;

                // Format and display the TimeSpan value.
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds );
                Console.WriteLine("RunTime " + elapsedTime);
            }
            
            Console.ReadKey();
        }
        public static int  MaxValue(int _maxValue, Dictionary<string,int> keyValuePairs)//функция для поиска наибольшего числа повторов среди триплетов
        {
            foreach (string key in keyValuePairs.Keys)
            {
                if (keyValuePairs[key] > _maxValue)
                    _maxValue = keyValuePairs[key];
            }
            return _maxValue;
        }
        
    }
}
