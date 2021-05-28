using System;
using System.Collections.Generic;
using System.Text;

namespace RPN_Recreate
{
    public class Postfix
    {
        static public bool IsOperator(string c) // Проверка на "операция-не-операция"
        {
            if (("+-/*^()".IndexOf(c) != -1)) // Выделяем из списка все операции
                return true;
            return false;
        }
        static private byte GetPriority(string s) // Кейзы с приоритетом операций
        {
            return s switch
            {
                "(" => 0,
                ")" => 1,
                "+" => 2,
                "-" => 3,
                "*" => 4,
                "/" => 4,
                "^" => 5,
                _ => 6,
            };
        }
        static public string[] GetExpression(string[] function)
        {
            Queue<string> queue = new Queue<string>();
            Stack<string> operStack = new Stack<string>();
            for (int i = 0; i < function.Length; i++) // Бежим по всему введённому выражению.
            {
                if (function[i] != "") // Избавляемся от "пустых" пробелов.
                {
                    if (function[i] == "x" || !IsOperator(function[i])) // Проверяем наличие икса, либо просто если число - не операция.
                    {
                        while (!IsOperator(function[i])) // Пока число не операция
                        {
                            queue.Enqueue(function[i]); // Добавляем в очередь элемент из выражения
                            i++; // идём дальше по строке
                            if (i == function.Length) break; // Дошли до конца строки - вышли из цикла
                        }
                        i--;
                    }
                    if (IsOperator(function[i])) // Если элемент - оператор
                    {
                        if (function[i] == "(")
                        {
                            operStack.Push(function[i]);  // Добавляем операцию в стэк
                        }
                        else if (function[i] == ")")
                        {
                            string s = operStack.Pop(); // При закрывающейся скобке вытягиваем самую верхнюю в стэке операцию
                            while (s != "(") // Пока операция - не открывающая скобка
                            {
                                queue.Enqueue(s.ToString()); // Добавляем в очередь элементы
                                s = operStack.Pop();
                            }
                        }
                        // Проверяем скобки
                        else
                        {
                            if (operStack.Count > 0)
                                if (GetPriority(function[i]) <= GetPriority(operStack.Peek())) // Проверяем приоритет операции
                                    queue.Enqueue(operStack.Pop()); // Добавляем операцию в очередь (чтобы впоследствие корректно вывести последова-
                            // тельность знаков)
                            operStack.Push(function[i]); // Докидываем с стэк операцию
                        }
                    }
                }
            }
            while (operStack.Count > 0)
                queue.Enqueue(operStack.Pop());
            return queue.ToArray(); // Возвращаем постфиксное выражение
        }
    }
}
