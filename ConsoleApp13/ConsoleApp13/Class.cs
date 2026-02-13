using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace OOP_lab13
{
    [Serializable]
    [XmlInclude(typeof(Circle))]
    [XmlInclude(typeof(Rectangle))]
    public abstract class Figure
    {
        [XmlAttribute]
        public string Type { get; set; }

        protected Figure(string type) => Type = type;

        protected Figure() { }
    }

    [Serializable]
    public class Circle : Figure
    {
        [XmlElement]
        public double Radius { get; set; }

        [XmlIgnore]
        public double NonSerializedValue = 42;

        [XmlElement("Area")]
        public double Area => GetArea();

        public Circle(double radius) : base("Circle") => Radius = radius;

        public Circle() : base("Circle") { } 

        public double GetArea() => Math.PI * Radius * Radius;

        public override string ToString() =>
            $"Тип: {Type}, Радиус: {Radius}, Площадь: {GetArea()}";
    }

    [Serializable]
    public class Rectangle : Figure
    {
        [XmlElement]
        public double Width { get; set; }

        [XmlElement]
        public double Height { get; set; }

        [XmlElement("Area")]
        public double Area => GetArea();

        public Rectangle(double width, double height) : base("Rectangle")
        {
            Width = width;
            Height = height;
        }

        public Rectangle() : base("Rectangle") { }

        public double GetArea() => Width * Height;

        public override string ToString() =>
            $"Тип: {Type}, Ширина: {Width}, Высота: {Height}, Площадь: {GetArea()}";
    }
}
