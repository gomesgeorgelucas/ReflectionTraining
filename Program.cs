using System.Reflection;

void ListMethodsAndProperties<T>(T obj)
{
    Console.WriteLine("Object properties:");
    foreach (var prop in obj!.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance))
    {
        Console.WriteLine($"Name: {prop.Name} - Type: {prop.PropertyType}");
    }
    Console.WriteLine("\nObject methods:");
    foreach (var mtd in obj!.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance))
    {
        MethodInfo info = (MethodInfo)mtd;

        Console.Write($"Name: {info.Name}");
        if (info.GetParameters().Length <= 0)
        {
            Console.WriteLine();
            continue;
        }

        Console.Write(" - Params: ");

        foreach (var param in info.GetParameters())
        {
            Console.Write($"{param.Name} {param.ParameterType}, ");
        }

        Console.Write($" - Returns: {info.ReturnType}");
        Console.WriteLine();
    }
}

T InstantiateGenericType<T>(T obj) where T : class, new()
{
    if (obj == null)
    {
        return new();
    }

    return (T)Activator.CreateInstance(typeof(T))!;
}

ListMethodsAndProperties(InstantiateGenericType(new MyClass()));

class MyClass
{
    private int PrivateProperty { get; set; }
    protected string? ProtectedProperty { get; set; }
    public string? PublicProperty { get; set; }
    public static string? StaticProperty { get; set; }

    private decimal PrivateMethod(int param)
    {
        return 0.0M;
    }

    protected void ProtectedMethod(int param, decimal param2)
    {

    }

    public void PublicMethod(int param)
    {

    }

    public static void StaticMethod(int param)
    {

    }
}