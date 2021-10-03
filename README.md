# JOS.WeightedResult
Different algorithms implemented in C# to use for getting a weighted result back (think A/B testing).

Currently implemented algorithms

* **Alias Method (Vose)**
Heavily inspired by [this article by Keith Schwarz](https://www.keithschwarz.com/darts-dice-coins/) and [this answer on Stack Overflow](https://stackoverflow.com/a/9958717/1141089). 
As you can see in the [tests](test/JOS.WeightedResult.Tests/AliasMethodVoseTests.cs), the max deviation is roughly 0.65% (depending on the sample size ofc, higher == better).

## Usage

It's **highly recommended** that you only create one ```WeightedResultQuery<T>``` instance per type/data set, preferably you register it as a **singleton** in your favorite DI container or use it as a static field. We want to avoid creating ```AliasMethodVose``` instances frequently since it does some initialization in the constructor.

The below example will print different results based on the following probability:

* 1/10 - "template-1"
* 2/10 - "template-2"
* 7/10 - "template-3"

```csharp
var data = new List<ProbabilityItem<string>>
{
    new ProbabilityItem<string>(1, "template-1", "Template 1"),
    new ProbabilityItem<string>(2, "template-2", "Template 2"),
    new ProbabilityItem<string>(7, "template-3", "Template 3")
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
template-3
template-2
template-3
template-2
template-3
template-1
template-3
template-3
template-3
template-2
template-3
template-3
template-3
template-3
template-3
template-1
template-3
template-3
template-2
template-2
template-3
template-3
template-2
template-3
template-3
template-3
template-3
template-3
template-3
template-3
template-3
template-3
template-1
template-3
template-3
template-2
template-1
template-2
template-3
template-3
template-3
template-2
template-3
template-1
template-3
template-3
template-1
template-3
template-3
template-2
template-2
template-3
template-2
template-3
template-2
template-3
template-2
template-2
template-3
template-3
template-3
template-3
template-3
template-3
template-3
template-2
template-3
template-1
template-3
template-2
template-3
template-3
template-3
template-3
template-2
template-3
template-3
template-3
template-1
template-3
template-3
template-3
template-3
template-3
template-3
template-3
template-2
template-3
template-3
template-3
template-3
template-1
template-3
template-3
template-3
template-1
template-3
template-3
template-3
template-3
</details

**Summary**
```
template-3: 71
template-2: 19
template-1: 10
```

The probability for each item will always be the sum of all items / probability.
Example:
```csharp
var data = new List<ProbabilityItem<string>>
{
    new ProbabilityItem<string>(1, "template-1", "Template 1"),
    new ProbabilityItem<string>(2, "template-2", "Template 2"),
    new ProbabilityItem<string>(7, "template-3", "Template 3")
};

// The sum of all probabilities is 10 (1 + 2 + 7).
// The probability for the first item is 1/10 (10%)
// The probability for the second item is 2/10 (20%)
// The probability for the third item is 7/10 (70%)

```

## Roadmap
* Add support for simple A/B testing in ASP.NET Core.
* Implement more algorithms, for example [Roulette Wheel Selection](https://en.wikipedia.org/wiki/Fitness_proportionate_selection).
