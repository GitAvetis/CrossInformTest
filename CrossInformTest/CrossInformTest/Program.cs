using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
            Stopwatch _stopWatch = new Stopwatch();

            try
            {
                Console.WriteLine("Укажите путь к вашему файлу с его названием в формате: C:\\Users\\1\\Documents\\vlastelin_kolec_bratstvo_kol_ca.txt");
                
                int _maxValue = 0; //наибольшее число повторов среди триплетов
                char[] punctuation_marks = { '.', ',', '!', '?', ';', ':', '-','>','<',')','(',' ', '»', '«',' '};
                string _path = Console.ReadLine();
                _stopWatch.Start();//запуск таймера работы программы
             
                string[] text = File.ReadAllLines($@"{_path}");//формирование массива строк из указанного файла
                
                Dictionary<string, int> DictionaryOfTriplets = new Dictionary<string, int>();//объявление словоря, где триплет-это ключ,а количество его повторов - значение

                foreach (var line in text)
                {
                    string[] words = line.Split(' ');//разбиение строк на слова и занесение их в массив строк
                    
                    foreach (var word in words)
                    {
                        char[] wordAsArray = word.ToLower().Trim(punctuation_marks).ToCharArray();//слово в виде массива букв

                        if (wordAsArray.Length >= 3)
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

                                    DictionaryOfTriplets.Add(triplet, 1);//если он новый, то добавляется в словарь со значением 1

                                }
                                else
                                {
                                    DictionaryOfTriplets[triplet] += 1;//если уже присутствует, его значение в словаре увеличится на единицу
                                    if (DictionaryOfTriplets[triplet] > _maxValue)
                                        _maxValue = DictionaryOfTriplets[triplet];
                                }
                                
                            }
                            

                        }
                        
                    }
                    
                }

        
              //  _maxValue = MaxValue(_maxValue, DictionaryOfTriplets)+1;
                int _counterOfCycles = 0;
                _maxValue++;//увеличиваю максимальное значение на 1, так как в последующем цикле оно будет уменьшено на 1
                while (_counterOfCycles < 10)//вывод 10 триплетов с наибольшим числом повторов в тексте
                {
                    _maxValue -=   1;

                    if (DictionaryOfTriplets.Values.Contains(_maxValue))
                    {
                        foreach(var key in DictionaryOfTriplets.Keys)
                        {
                            if (DictionaryOfTriplets[key] == _maxValue)
                            {
                                if (_counterOfCycles == 9)
                                {
                                   Console.WriteLine($"{key}.");
                                }
                                else
                                {
                                   Console.Write($"{key},");
                                }
                                _counterOfCycles++;
                            }

                        }
                    }
                   
                }   

            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            finally
            {
                _stopWatch.Stop();
                // Get the elapsed time as a TimeSpan value.
                TimeSpan ts = _stopWatch.Elapsed;

                // Format and display the TimeSpan value.
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds );
                Console.WriteLine("RunTime " + elapsedTime);
                GC.Collect();
            }
            
            Console.ReadKey();
        }

    }
}
