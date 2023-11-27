//using System;
//using System.Collections.Generic;
//using System.Linq;

//public class EncontrarNumeroMaisFrequenteClass
//{
//    static int EncontrarNumeroMaisFrequente(int[] array, int tamanho)
//    {
//        Dictionary<int, int> contagem = new Dictionary<int, int>();

//        // Conta a ocorrência de cada número no array
//        for (int i = 0; i < tamanho; i++)
//        {
//            int numero = array[i];
//            if (contagem.ContainsKey(numero))
//            {
//                contagem[numero]++;
//            }
//            else
//            {
//                contagem[numero] = 1;
//            }
//        }

//        // Encontra o número mais frequente
//        int numeroMaisFrequente = contagem.Keys.First();
//        int frequenciaMaxima = contagem[numeroMaisFrequente];

//        foreach (var kvp in contagem)
//        {
//            if (kvp.Value > frequenciaMaxima || (kvp.Value == frequenciaMaxima && kvp.Key < numeroMaisFrequente))
//            {
//                numeroMaisFrequente = kvp.Key;
//                frequenciaMaxima = kvp.Value;
//            }
//        }

//        return numeroMaisFrequente;
//    }

//    static void Main()
//    {
//        int[] array = { 1, 2, 3, 2, 4, 1, 5, 2, 1, 4, 3, 3, 4, 2, 5, 1 };
//        int tamanho = array.Length;

//        int resultado = EncontrarNumeroMaisFrequente(array, tamanho);

//        Console.WriteLine($"O número mais frequente no array é: {resultado}");
//    }
//}