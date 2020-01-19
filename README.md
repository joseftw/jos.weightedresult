# JOS.WeightedResult
Different algorithms implemented in C# to use for getting a weighted result back (think A/B testing).

Currently implemented algorithms

* **Alias Method (Vose)**
Heavily inspired by [this article by Keith Schwarz](https://www.keithschwarz.com/darts-dice-coins/) and [this answer on Stack Overflow](https://stackoverflow.com/a/9958717/1141089). 
As you can see in the [tests](test/JOS.WeightedResult.Tests/AliasMethodVoseTests.cs), the max deviation is roughly 0.65% (depending on the sample size ofc, higher == better).

## Usage

It's **highly recommended** that you only create one ```WeightedResultQuery<T>``` instance per type/data set, preferably you register it as a **singleton** in your favorite DI container or use it as a static field. We want to avoid creating ```AliasMethodVose``` instances frequently since it does some initialization in the constructor.

The below example will print different results based on the following probability:

* 7/10 - "Seven"
* 2/10 - "Two"
* 1/10 - "One"

```csharp
var data = new List<(int, string)>
{
    (1, "One"),
    (2, "Two"),
    (7, "Seven"),
};

var query = new WeightedResultQuery<string>(data);
for(var i = 0; i < 100; i++)
{
    Console.WriteLine(query.Execute());
}
```

The following was printed to the console.
<details>
<summary>Output</summary>
Seven
Two
Seven
Two
Seven
One
Seven
Seven
Seven
Two
Seven
Seven
Seven
Seven
Seven
One
Seven
Seven
Two
Two
Seven
Seven
Two
Seven
Seven
Seven
Seven
Seven
Seven
Seven
Seven
Seven
One
Seven
Seven
Two
One
Two
Seven
Seven
Seven
Two
Seven
One
Seven
Seven
One
Seven
Seven
Two
Two
Seven
Two
Seven
Two
Seven
Two
Two
Seven
Seven
Seven
Seven
Seven
Seven
Seven
Two
Seven
One
Seven
Two
Seven
Seven
Seven
Seven
Two
Seven
Seven
Seven
One
Seven
Seven
Seven
Seven
Seven
Seven
Seven
Two
Seven
Seven
Seven
Seven
One
Seven
Seven
Seven
One
Seven
Seven
Seven
Seven
</details

**Summary**
```
seven: 71
two: 19
one: 10
```

## Roadmap
* Add support for simple A/B testing in ASP.NET Core.
* Implement more algorithms, for example [Roulette Wheel Selection](https://en.wikipedia.org/wiki/Fitness_proportionate_selection).
