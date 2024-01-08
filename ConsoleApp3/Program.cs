using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
namespace ConsoleApp
{
    public abstract class Shapes : IComparable
    {
        public abstract double Perimeter();
        public abstract double Square();
        public abstract void Info();
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }
            if (obj is Shapes otherShapes)
            {
                return Square().CompareTo(otherShapes.Square());
            }
            else
            {
                throw new ArgumentException("Этот объект не является типом фигура");
            }
        }
    }
    class Triangle : Shapes
    {
        public string Name { get; private set; }
        public int SideA { get; private set; }
        public int SideB { get; private set; }
        public int SideC { get; private set; }
        public Triangle(int sideA, int sideB, int sideC)
        {
            if ((sideA + sideB) > sideC && (sideA + sideC) > sideB && (sideB + sideC) > sideA)
            {
                SideA = sideA;
                SideB = sideB;
                SideC = sideC;
                Name = "Треугольник";
            }
            else
            {
                throw new Exception($"Треугольника со сторонами {sideA}, {sideB}, {sideC} не существует");
            }
        }
        public override double Perimeter()
        {
            return SideA + SideB + SideC;
        }
        public override double Square()
        {
            double p = Perimeter() / 2;
            double h = (2 * Math.Sqrt(p * (p - SideA) * (p - SideB) * (p - SideC))) / SideC;
            return (SideC * h) / 2;
        }
        public override void Info()
        {
            Console.Write($"{Name}  {SideA,10}, {SideB}, {SideC} {Perimeter(),9:0.00} {Square(),12:0.00} ");
            if (SideA == SideB && SideB == SideC)
            {
                Console.Write("\tравностороний");
            }
            else if (SideA == SideB || SideA == SideC || SideC == SideB)
            {
                Console.Write("\tравнобедренный");
            }
            Console.WriteLine();
        }
    }
    class Rectangle : Shapes
    {
        public string Name { get; private set; }
        public int SideA { get; private set; }
        public int SideB { get; private set; }
        public Rectangle(int sideA, int sideB)
        {
            SideA = sideA;
            SideB = sideB;
            if (SideA == SideB)
            {
                Name = "Квадрат      ";
            }
            else
            {
                Name = "Прямоугольник";
            }
        }
        public override double Perimeter()
        {
            return 2 * (SideA + SideB);
        }
        public override double Square()
        {
            return SideA * SideB;
        }
        public override void Info()
        {
            Console.WriteLine($"{Name}{SideA,10}, {SideB} {Perimeter(),12:0.00} {Square(),12:0.00} ");
        }
    }
    class Elipse : Shapes
    {
        public string Name { get; private set; }
        public int Radius1 { get; private set; }
        public int Radius2 { get; private set; }
        public Elipse(int radius1, int radius2)
        {
            Radius1 = radius1;
            Radius2 = radius2;
            if (Radius1 == Radius2)
            {
                Name = "Окружность  ";
            }
            else
            {
                Name = "Эллипс      ";
            }
        }
        public override double Perimeter()
        {
            return 2 * Math.PI * Math.Sqrt((Radius1 * Radius1 + Radius2 * Radius2) / 2);
        }
        public override double Square()
        {
            return Math.PI * Radius1 * Radius2;
        }
        public override void Info()
        {
            Console.WriteLine($"{Name} {Radius1,10}, {Radius2} {Perimeter(),12:0.00} {Square(),13:0.00}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new();
            Console.Write("Введите кол-во эл.: ");
            int n = Convert.ToInt32(Console.ReadLine());
            List<Shapes> shapesList = new();
            try
            {
                for (int i = 0; i < n; i++)
                {
                    switch (rnd.Next(3))
                    {
                        case 0:
                            shapesList.Add(new Triangle(rnd.Next(5, 10), rnd.Next(5, 10), rnd.Next(5, 10)));
                            break;
                        case 1:
                            shapesList.Add(new Rectangle(rnd.Next(5, 10), rnd.Next(5, 10)));
                            break;
                        case 2:
                            shapesList.Add(new Elipse(rnd.Next(5, 10), rnd.Next(5, 10)));
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("Наименование          Стороны     Периметр     Площадь");
            foreach (var i in shapesList)
            {
                i.Info();
            }
            shapesList.Sort();
            Console.WriteLine("\nСортировка по площади \nНаименование          Стороны     Периметр     Площадь");
            foreach (var i in shapesList)
            {
                i.Info();
            }
            Console.Write("\nЗапустить программу еще раз? (0 - да, 1 - нет) ");
            n = Convert.ToInt32(Console.ReadLine());
            if (n == 0)
            {
                Main(args);
            }
        }
    }
}
