using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

public static class Reflector
{

    public static string GetAssemblyName(string className)
    {
        Type type = Type.GetType(className);
        return type?.Assembly.FullName ?? "Класс не найден.";
    }

    public static bool HasPublicConstructors(string className)
    {
        Type type = Type.GetType(className);
        if (type == null) return false;
        return type.GetConstructors(BindingFlags.Public | BindingFlags.Instance).Length > 0;
    }

    public static IEnumerable<string> GetPublicMethods(string className)
    {
        Type type = Type.GetType(className);
        if (type == null) return new List<string> { "Класс не найден." };
        return type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                   .Select(method => method.Name);
    }

    public static IEnumerable<string> GetFieldsAndProperties(string className)
    {
        Type type = Type.GetType(className);
        if (type == null) return new List<string> { "Класс не найден." };

        var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                         .Select(field => "Field: " + field.Name);
        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                             .Select(property => "Property: " + property.Name);

        return fields.Concat(properties);
    }

    public static IEnumerable<string> GetImplementedInterfaces(string className)
    {
        Type type = Type.GetType(className);
        if (type == null) return new List<string> { "Класс не найден." };
        return type.GetInterfaces().Select(i => i.Name);
    }

    public static IEnumerable<string> GetMethodsByParameterType(string className, Type parameterType)
    {
        Type type = Type.GetType(className);
        if (type == null) return new List<string> { "Класс не найден." };

        return type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                   .Where(method => method.GetParameters().Any(param => param.ParameterType == parameterType))
                   .Select(method => method.Name);
    }

    public static object InvokeMethod(object obj, string methodName, object[] parameters)
    {
        Type type = obj.GetType();
        MethodInfo method = type.GetMethod(methodName);
        if (method == null) throw new Exception("Метод не найден.");
        return method.Invoke(obj, parameters);
    }

    public static object InvokeMethodFromFile(string className, string methodName, string filePath)
    {
        Type type = Type.GetType(className);
        if (type == null) throw new Exception("Класс не найден.");
        MethodInfo method = type.GetMethod(methodName);
        if (method == null) throw new Exception("Метод не найден.");

        string[] fileLines = File.ReadAllLines(filePath);
        ParameterInfo[] parameters = method.GetParameters();
        object[] parameterValues = new object[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            string line = fileLines[i];
            Type parameterType = parameters[i].ParameterType;
            parameterValues[i] = Convert.ChangeType(line, parameterType);
        }

        object obj = Activator.CreateInstance(type);
        return method.Invoke(obj, parameterValues);
    }

    public static T Create<T>() where T : class
    {
        Type type = typeof(T);
        ConstructorInfo constructor = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                                          .FirstOrDefault();
        if (constructor == null) throw new Exception("Публичный конструктор не найден.");
        return (T)constructor.Invoke(null);
    }
}
