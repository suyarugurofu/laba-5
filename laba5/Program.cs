using System.Text.RegularExpressions;

namespace lab5;

class Program
{
    static int InputInt(string requestInput, string errorMessage, int minValue = int.MinValue, int maxValue = int.MaxValue)
    {
        bool isCorrect;
        int answer;
        string buffer;
        do
        {
            Console.Write(requestInput);
            buffer = Console.ReadLine();
            Console.WriteLine();
            isCorrect = int.TryParse(buffer, out answer);
            if (!isCorrect || answer < minValue || answer > maxValue)
                Console.WriteLine($"{errorMessage}\n");
        } while (!isCorrect || answer < minValue || answer > maxValue);
        return answer;
    }
    
    static int[,] CreateRandomTwoDimensionalArray(int numberLines, int numberColumns)
    {
        int[,] randomTwoDimensionalArray = new int[numberLines, numberColumns];
        Random random = new Random();
        for (int i = 0; i < numberLines; i++)
        {
            for (int j = 0; j < numberColumns; j++)
            {
                randomTwoDimensionalArray[i, j] = random.Next(-100, 100);
            }
        }
        return randomTwoDimensionalArray;
    }

    static int[,] CreateTwoDimensionalArray(int numberLines, int numberColumns)
    {
        int[,] twoDimensionalArray = new int[numberLines, numberColumns];
        for (int i = 0; i < numberLines; i++)
        {
            for (int j = 0; j < numberColumns; j++)
            {
                twoDimensionalArray[i, j] = InputInt($"Введите элемент {i + 1} строки {j + 1} столбца (целое число от -100 до 100): ", "Ошибка при вводе элемента, введите целое число от -100 до 100", -100, 100);
            }
        }
        return twoDimensionalArray;
    }

    static int[][] CreateRandomJaggedArray(int numberLines)
    {
        int[][] randomJaggedArray = new int[numberLines][];
        Random random = new Random();
        for (int i = 0; i < numberLines; i++)
        {
            int stringLength = random.Next(1, 10);
            randomJaggedArray[i] = new int[stringLength];
            for (int j = 0; j < stringLength; j++)
            {
                randomJaggedArray[i][j] = random.Next(-100, 100);
            }
        }
        return randomJaggedArray;
    }

    static int[][] CreateJaggedArray(int numberLines)
    {
        int[][] jaggedArray = new int[numberLines][];
        Random random = new Random();
        for (int i = 0; i < numberLines; i++)
        {
            int stringLength = InputInt($"Введите длину строки {i + 1} (от 1 до 10): ", "Ошибка при вводе длины строки, введите целое неотрицательное число от 0 до 10", 1, 10);
            jaggedArray[i] = new int[stringLength];
            for (int j = 0; j < stringLength; j++)
            {
                jaggedArray[i][j] = InputInt($"Введите элемент {i + 1} строки {j + 1} столбца (целое число от -100 до 100): ", "Ошибка при вводе элемента, введите целое число от -100 до 100", -100, 100);
            }
        }
        return jaggedArray;
    }

    static bool IsNullOrEmptyArray(Array array)
    {
        return (array == null || array.Length == 0);
    }

    static void PrintTwoDimensionalArray(int[,] array)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            if (i == 0)
                Console.Write("Массив:");
            else
                Console.Write("       ");
            for (int j = 0; j < array.GetLength(1); j++)
            {
                Console.Write($"{array[i, j],4}");
            }
            Console.WriteLine("\n");
        }
    }

    static void PrintJaggedArray(int[][] array)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            if (i == 0)
                Console.Write("Массив:");
            else
                Console.Write("       ");
            for (int j = 0; j < array[i].GetLength(0); j++)
            {
                Console.Write($"{array[i][j],4}");
            }
            Console.WriteLine("\n");
        }
    }

    static int[] CreateRandomOneDimensionalArray(int length)
    {
        int[] array = new int[length];
        Random rnd = new Random();
        for (int i = 0; i < length; i++)
        {
            array[i] = rnd.Next(-10, 10);
        }
        return array;
    }

    static int[] CreateOneDimensionalArray(int length)
    {
        int[] array = new int[length];
        for (int i = 0; i < length; i++)
        {
            array[i] = InputInt($"Введите элемент {i + 1} (целое число от -100 до 100): ", "Ошибка при вводе элемента, введите целое число от -100 до 100", -100, 100);
        }
        return array;
    }

    static int[,] AddColumnAfterMax(int[,] twoDimensionalArray, int[] columnToAdd)
    {
        int numberLines = twoDimensionalArray.GetLength(0);
        int numberColumns = twoDimensionalArray.GetLength(1);
        int maxElement = int.MinValue;
        int maxColumnIndex = 0;
        for (int i = 0; i < numberLines; i++)
        {
            for (int j = 0; j < numberColumns; j++)
            {
                if (twoDimensionalArray[i, j] > maxElement)
                {
                    maxElement = twoDimensionalArray[i, j];
                    maxColumnIndex = j;
                }
            }
        }
        int[,] newTwoDimensionalArray = new int[numberLines, numberColumns + 1];
        for (int i = 0; i < numberLines; i++)
        {
            for (int j = 0, k = 0; j < numberColumns + 1; j++)
            {
                if (j == maxColumnIndex + 1)
                    newTwoDimensionalArray[i, j] = columnToAdd[i];
                else
                    newTwoDimensionalArray[i, j] = twoDimensionalArray[i, k++];
            }
        }
        return newTwoDimensionalArray;
    }

    static int[][] AddLineToEnd(int[][] jaggedArray, int[] lineToAdd)
    {
        int[][] newJaggedArray = new int[jaggedArray.Length + 1][];
        for (int i = 0; i < jaggedArray.Length; i++)
        {
            newJaggedArray[i] = jaggedArray[i];
        }
        newJaggedArray[jaggedArray.Length] = lineToAdd;
        return newJaggedArray;
    }

    static string RemoveWordsWithSameFirstAndLastLetter(string input)
    {
        string[] sentences = Regex.Split(input, @"(?<=[.!?])\s+"); // Текст разбивается на предложения
        for (int i = 0; i < sentences.Length; i++)
        {
            sentences[i] = Regex.Replace(sentences[i], @"\b(\p{L})\p{L}*\1\b", "", RegexOptions.IgnoreCase).Trim(); // Удаление слов
            sentences[i] = Regex.Replace(sentences[i], @"\s([.,!?;:])", "$1"); // Удаление пробелов перед знаками препинания
            while (Regex.IsMatch(sentences[i], "^[;:,]|[;:,]+(?=[;:,]|[.!?])")) // Пока есть лишние знаки препинания
            {
                sentences[i] = Regex.Replace(sentences[i], "^[;:,]|[;:,]+(?=[;:,]|[.!?])", "").Trim(); // Удалять лишние знаки препинания
            }
            sentences[i] = Regex.Replace(sentences[i], @"\s+(?=[.!?])", "").Trim(); // Удаление пробелов перед финальными знаками препинания
            sentences[i] = char.ToUpper(sentences[i][0]) + sentences[i].Substring(1); // Первая буква предложения делается заглавной
            if (Regex.IsMatch(sentences[i], @"^[.!?]+$")) // Удаление финального знака препинания, если в предложении остался только он
                sentences[i] = "";
        }
        string result = string.Join(" ", sentences); // Соединение предложений обратно в текст
        return Regex.Replace(result, @"\s{2,}", " ").Trim(); // Возврат обработанного текста без лишних пробелов
    }

    static string InputCorrectText(string requestInput, string errorMessage)
    {
        string input;
        do
        {
            Console.Write(requestInput);
            input = Console.ReadLine();
            Console.WriteLine();
            if (IsCorrectText(input))
                return input;
            else
            {
                Console.WriteLine(errorMessage);
                Console.WriteLine();
            }
        } while (true);
    }

    static bool IsCorrectText(string input)
    {
        if (!Regex.IsMatch(input, @"^[а-яА-ЯёЁa-zA-Z0-9\s.,;:!?]+$")) // Проверяется, что текст состоит из допустимых символов
            return false;
        if (!Regex.IsMatch(input, @"^[^,;:!?.\s].*")) // Проверяется, что текст не начинается со знака препинания
            return false;
        if (!Regex.IsMatch(input, @"^[^a-zа-я].*")) // Проверяется, что текст не начинается с маленькой буквы
            return false;
        if (!Regex.IsMatch(input, @"[.!?]$")) // Проверяется, что текст заканчивается знаком препинания
            return false;
        if (Regex.IsMatch(input, @"[!?.:;,]{2,}")) // Проверяется, что никакие знаки препинанания не стоят рядом друг с другом
            return false;
        if (Regex.IsMatch(input, @"(?<=[,.!?:;])(?=\S)(?!$)")) // Проверяется, что после всех знаков препинания есть пробелы (не считая конца текста)
            return false;
        if (Regex.IsMatch(input, @"(?<=\s)[!?.:;,]")) // Проверяется, что перед знаками препинаний нет пробелов
            return false;
        if (Regex.IsMatch(input, @"(^|(?<=[.!?]\s))\p{Ll}")) // Проверяется, что каждое предложение начинается с заглавной буквы
            return false;
        if (Regex.IsMatch(input, @"[\s]{2,}")) // Проверяется, что нет двойных пробелов
            return false;
        return true;
    }

    // Основная программа
    static void Main(string[] args)
    {
        int[,] twoDimensionalArray = null;
        int[][] jaggedArray = null;
        int firstAnswer;
        int secondAnswer;
        int thirdAnswer;
        do
        {
            firstAnswer = InputInt("ГЛАВНОЕ МЕНЮ\n" +
                                   "1.Работа с двумерными массивами\n" +
                                   "2.Работа со рваными массивами\n" +
                                   "3.Работа со строками\n" +
                                   "4.Выход\n" +
                                   "Выберите нужную функцию и введите её номер: ",
                                   "Ошибка при выборе функции, попробуйте снова", 1, 4);
            switch (firstAnswer)
            {
                case 1: // Работа с двумерными массивами
                    do
                    {
                        secondAnswer = InputInt("РАБОТА С ДВУМЕРНЫМИ МАССИВАМИ\n" +
                                                "1.Создать двумерный массив из целых чисел\n" +
                                                "2.Напечатать двумерный массив\n" +
                                                "3.Добавить столбец после столбца, содержащего наибольший элемент\n" +
                                                "4.Вернуться в главное меню\nВыберите нужную функцию и введите её номер: ",
                                                "Ошибка при выборе функции, попробуйте снова", 1, 4);
                        switch (secondAnswer)
                        {
                            case 1: // Создать двумерный массив
                                thirdAnswer = InputInt("Каким способом создать двумерный массив?\n" +
                                                       "1.Датчиком случайных чисел\n" +
                                                       "2.Ручным вводом\n" +
                                                       "Выберите нужную функцию и введите её номер: ",
                                                       "Ошибка при выборе функции, попробуйте снова", 1, 2);
                                int numberLines = InputInt("Введите количество строк (от 1 до 10): ", "Ошибка при вводе количества строк, введите натуральное число от 1 до 10", 1, 10);
                                int numberColumns = InputInt("Введите количество столбцов (от 1 до 10): ", "Ошибка при вводе количества столбцов, введите натуральное число от 1 до 10", 1, 10);
                                switch (thirdAnswer)
                                {
                                    case 1: // ДСЧ
                                        twoDimensionalArray = CreateRandomTwoDimensionalArray(numberLines, numberColumns);
                                        Console.WriteLine("Двумерный массив создан\n");
                                        break;
                                    case 2: // Вручную
                                        twoDimensionalArray = CreateTwoDimensionalArray(numberLines, numberColumns);
                                        Console.WriteLine("Двумерный массив создан\n");
                                        break;
                                }
                                break;
                            case 2: // Напечатать двумерный массив
                                if (IsNullOrEmptyArray(twoDimensionalArray))
                                    Console.WriteLine("Массив пустой\n");
                                else
                                    PrintTwoDimensionalArray(twoDimensionalArray);
                                break;
                            case 3: // Добавить столбец после столбца, содержащего наибольший элемент
                                if (IsNullOrEmptyArray(twoDimensionalArray))
                                    Console.WriteLine("Массив пустой\n");
                                else
                                {
                                    thirdAnswer = InputInt("Каким способом добавить столбец?\n" +
                                                           "1.Датчиком случайных чисел\n" +
                                                           "2.Ручным вводом\n" +
                                                           "Выберите нужную функцию и введите её номер: ",
                                                           "Ошибка при выборе функции, попробуйте снова", 1, 2);
                                    int[] columnToAdd = null;
                                    switch (thirdAnswer)
                                    {
                                        case 1: // ДСЧ
                                            columnToAdd = CreateRandomOneDimensionalArray(twoDimensionalArray.GetLength(0));
                                            twoDimensionalArray = AddColumnAfterMax(twoDimensionalArray, columnToAdd);
                                            Console.WriteLine("Столбец добавлен\n");
                                            break;
                                        case 2: // Вручную
                                            columnToAdd = CreateOneDimensionalArray(twoDimensionalArray.GetLength(0));
                                            twoDimensionalArray = AddColumnAfterMax(twoDimensionalArray, columnToAdd);
                                            Console.WriteLine("Столбец добавлен\n");
                                            break;
                                    }
                                }
                                break;
                        }
                    } while (secondAnswer != 4);
                    break;
                case 2: // Работа с рваными массивами
                    do
                    {
                        secondAnswer = InputInt("РАБОТА СО РВАНЫМИ МАССИВАМИ\n" +
                                                "1.Создать рваный массив из целых чисел\n" +
                                                "2.Напечатать рваный массив\n" +
                                                "3.Добавить строку в конец массива\n" +
                                                "4.Вернуться в главное меню\n" +
                                                "Выберите нужную функцию и введите её номер: ",
                                                "Ошибка при выборе функции, попробуйте снова", 1, 4);
                        switch (secondAnswer)
                        {
                            case 1: // Создать рваный массив
                                thirdAnswer = InputInt("Каким способом создать рваный массив?\n" +
                                                       "1.Датчиком случайных чисел\n" +
                                                       "2.Ручным вводом\n" +
                                                       "Выберите нужную функцию и введите её номер: ",
                                                       "Ошибка при выборе функции, попробуйте снова", 1, 2);
                                int numberLines = InputInt("Введите количество строк (от 1 до 10): ", "Ошибка при вводе количества строк, введите целое неотрицательное число от 1 до 10", 1, 10);
                                switch (thirdAnswer)
                                {
                                    case 1: // ДСЧ
                                        jaggedArray = CreateRandomJaggedArray(numberLines);
                                        Console.WriteLine("Рваный массив создан\n");
                                        break;
                                    case 2: // Вручную
                                        jaggedArray = CreateJaggedArray(numberLines);
                                        Console.WriteLine("Рваный массив создан\n");
                                        break;
                                }
                                break;
                            case 2: // Напечатать рваный массив
                                if (IsNullOrEmptyArray(jaggedArray))
                                    Console.WriteLine("Массив пустой\n");
                                else
                                    PrintJaggedArray(jaggedArray);
                                break;
                            case 3: // Добавить строку в конец массива
                                if (IsNullOrEmptyArray(jaggedArray))
                                    Console.WriteLine("Массив пустой\n");
                                else
                                {
                                    thirdAnswer = InputInt("Каким способом добавить строку?\n" +
                                                           "1.Датчиком случайных чисел\n" +
                                                           "2.Ручным вводом\n" +
                                                           "Выберите нужную функцию и введите её номер: ",
                                                           "Ошибка при выборе функции, попробуйте снова", 1, 2);
                                    int lineToAddLength = 0;
                                    int[] lineToAdd = null;
                                    switch (thirdAnswer)
                                    {
                                        case 1: // ДСЧ
                                            Random random = new Random();
                                            lineToAddLength = random.Next(1, 10);
                                            lineToAdd = CreateRandomOneDimensionalArray(lineToAddLength);
                                            jaggedArray = AddLineToEnd(jaggedArray, lineToAdd);
                                            Console.WriteLine("Cтрока добавлена\n");
                                            break;
                                        case 2: // Вручную
                                            lineToAddLength = InputInt("Введите длину добавляемой строки (от 1 до 10): ", "\nОшибка при вводе длины строки, введите натуральное число от 1 до 10\n", 1, 10);
                                            lineToAdd = CreateOneDimensionalArray(lineToAddLength);
                                            jaggedArray = AddLineToEnd(jaggedArray, lineToAdd);
                                            Console.WriteLine("Cтрока добавлена\n");
                                            break;
                                    }
                                }
                                break;
                        }
                    } while (secondAnswer != 4);
                    break;
                case 3: // Работа со строками
                    do
                    {
                        secondAnswer = InputInt("РАБОТА СО СТРОКАМИ\n" +
                                                "1.Ввести строку и обработать её\n" +
                                                "2.Выбрать готовую строку и обработать её\n" +
                                                "3.Вернуться в главное меню\n" +
                                                "Выберите нужную функцию и введите её номер: ",
                                                "Ошибка при выборе функции, попробуйте снова", 1, 3);
                        switch (secondAnswer)
                        {
                            case 1:
                                string input = InputCorrectText("Введите текст, соответствующий следующим правилам:\n" +
                                                                "- текст состоит только из букв кириллического и/или латинского алфавита, цифр и разрешенных знаков препинания (:;,.?!)\n" +
                                                                "- текст начинается с заглавной буквы кириллического/латинского алфавита или цифры, а заканчивается знаком препинания (.?!)\n" +
                                                                "- в конце каждого предложения стоит знак препинания (.?!)\n- каждое предложение начинается с заглавной буквы или цифры\n" +
                                                                "- перед знаками препинания не должно быть пробелов, а после них - должны быть\n" +
                                                                "- никакие два и более знака препинания не должны быть написаны подряд\n" +
                                                                "- нет двух или более пробелов подряд\n" + 
                                                                "После ввода текст будет обработан и из него будут удалены слова, начинающиеся и оканчивающиеся на одну и ту же букву\n\n" +
                                                                "Введите текст: ", "Неверный формат текста, внимательно прочитайте правила ввода текста и попробуйте ввести заново");
                                if (string.IsNullOrEmpty(RemoveWordsWithSameFirstAndLastLetter(input)))
                                    Console.WriteLine($"Результат: <пустая строка>\n");
                                else
                                    Console.WriteLine($"Результат: {RemoveWordsWithSameFirstAndLastLetter(input)}\n");
                                break;
                            case 2:
                                thirdAnswer = InputInt("1.В траве сидел кузнечик! Кузнечик не трогал козявок и дружил с мухом.\n" +
                                                       "2.Самолёт прилетел вовремя, поэтому я успел вечером встретиться с друзьями.\n" +
                                                       "3.Котик занял мою кровать! Мне пришлось спать в другой комнате.\n" +
                                                       "4.Анна, иди ко мне. Я хочу тебе кое-что сказать.\n" +
                                                       "Выберите строку и введите её номер: ", "Ошибка при выборе предложения, попробуйте снова", 1, 5);
                                switch (thirdAnswer)
                                {
                                    case 1:
                                        Console.WriteLine($"Результат: {RemoveWordsWithSameFirstAndLastLetter("В траве сидел кузнечик! Кузнечик не трогал козявок и дружил с мухом.")}\n");
                                        break;
                                    case 2:
                                        Console.WriteLine($"Результат: {RemoveWordsWithSameFirstAndLastLetter("Самолёт прилетел вовремя, поэтому я успел вечером встретиться с друзьями.")}\n");
                                        break;
                                    case 3:
                                        Console.WriteLine($"Результат: {RemoveWordsWithSameFirstAndLastLetter("Котик занял мою кровать! Мне пришлось спать в другой комнате.")}\n");
                                        break;
                                    case 4:
                                        Console.WriteLine($"Результат: {RemoveWordsWithSameFirstAndLastLetter("Анна, иди ко мне. Я хочу тебе кое-что сказать.")}\n");
                                        break;

                                }
                                break;
                        }
                    } while (secondAnswer != 3);
                    break;
            }
        } while (firstAnswer != 4);
        Console.WriteLine("Работа программы завершена");
    }
}

