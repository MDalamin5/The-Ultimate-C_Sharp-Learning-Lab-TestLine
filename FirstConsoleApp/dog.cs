public class Dog
{
    public string Name { get; set; }

    public Dog(string name)
    {
        Name = name;
    }

    public void Bark()
    {
        Console.WriteLine($"{Name} is barking!");
    }
}