//using System;

//class ProgramQuasePolidromo
//{
//    static bool QuasePolidromo(string str)
//    {
//        // Converte a string para minúsculas para ignorar maiúsculas/minúsculas
//        str = str.ToLower();

//        int i = 0;
//        int j = str.Length - 1;

//        while (i < j)
//        {
//            if (str[i] != str[j])
//            {
//                // Tenta remover o caractere na posição i ou j e verifica se o restante é um palíndromo
//                return EhPolidromo(str, i + 1, j) || EhPolidromo(str, i, j - 1);
//            }

//            i++;
//            j--;
//        }

//        return true;
//    }

//    static bool EhPolidromo(string str, int start, int end)
//    {
//        while (start < end)
//        {
//            if (str[start] != str[end])
//            {
//                return false;
//            }
//            start++;
//            end--;
//        }
//        return true;
//    }

//    static void Main()
//    {
//        string exemplo1 = "abccba";
//        string exemplo2 = "abccbx";

//        Console.WriteLine($"'{exemplo1}' é Quase Palíndromo: {QuasePolidromo(exemplo1)}");
//        Console.WriteLine($"'{exemplo2}' é Quase Palíndromo: {QuasePolidromo(exemplo2)}");
//    }
//}