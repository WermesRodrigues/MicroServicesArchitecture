//using System;

//class Program
//{
//    static double CalcularDistanciaEntrePontos(double x1, double y1, double x2, double y2)
//    {
//        return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
//    }

//    static double CalcularDistanciaMediaEntreTresPontos(double x1, double y1, double x2, double y2, double x3, double y3)
//    {
//        double dist1 = CalcularDistanciaEntrePontos(x1, y1, x2, y2);
//        double dist2 = CalcularDistanciaEntrePontos(x2, y2, x3, y3);
//        double dist3 = CalcularDistanciaEntrePontos(x3, y3, x1, y1);

//        double distanciaMedia = (dist1 + dist2 + dist3) / 3;
//        return distanciaMedia;
//    }

//    static void Main()
//    {
//        double x1 = 1, y1 = 2;
//        double x2 = 4, y2 = 6;
//        double x3 = 7, y3 = 1;

//        double resultado = CalcularDistanciaMediaEntreTresPontos(x1, y1, x2, y2, x3, y3);

//        Console.WriteLine($"A distância média entre os três pontos é: {resultado}");
//    }
//}