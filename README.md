# My class work for 'Algorithms'

### Assignment I: Code Examples and Portfolio Start
- [x] Create methods that show: Constant O(1), Linear O(n), Quadratic O(n2)

<details> 
<summary> Constant </summary>  

  ```
    Console.WriteLine(args[0]);
  ```
  accessing the first member of a list is always in <b> Constant Time </b>
</details>

<details>
<summary> Linear </summary>
  
  ```
  foreach (var s in args)
  {
    Console.WriteLine(s);
  }
  ```
  Going through all of a list is in <b> Linear Time </b>
</details>

<details>
<summary> Quadratic </summary> 
  
  ```
  foreach (var s in args)
  {
    foreach (var v in args)
    {
      Console.WriteLine(s + " " + v);
    }
  }
  ```
  Going through a list for each member in that list <sub> (looping through twice) </sub> is in <b> Quadratic Time </b>
</details>

### Assignment: Exponent Method in C#
- [x] Translate the code shown in the video to an algorithm in pseudocode.
- [x] Using what you learned in the The Idea Behind Big O Notation reading, analyze your algorithm using big O notation. What are the worst and best runtimes?

<details>
<summary> Algorithm </summary>

```
static int pow(int baseNum, int exponent) 
{
  var answer = 1;

  for (int i = 0; i < exponent; i++)
  {
    answer *= baseNum;
  }
  return answer;
}
```
</details>

<details>
<summary> Pseudocode </summary> 
This has a Big O of O(n), because the time scales based on how large the exponent is

1. Identify the base and the power
2. Create a temp var to hold the solution
3. For the power we loop through and multiply temp var by the base
4. Return the temp var
</details>

### Assignment: Algorithm Creation and Analysis II (Matryoshka)
  <i>I might have gone a bit overboard on this one</i>
  
- [X] Part 1: Create an algorithm that will count how many dolls there are in total from all sets.
- [X] Part 2: Create an algorithm that will line the doll sets up from the one with the smallest nested amount to the largest.
- [X] Part 3: One of the dolls is unique and has the artist’s signature on it. Create an algorithm that searches through all dolls in all sets until it finds the one with the signature (and then stops).
- [X] Analyze your algorithm using big O notation.

<details>
<summary>Part One: Count All</summary>  
  For this assignment I created a basic doll class that holds a reference to another doll class <i>(not unlike a linked list)</i>. 
  In the constructor for this class we take in an optional number, this number tells us how many dolls you want to create.
  In the constructor we recursivly call subtracting one from the inputted number.
  
  ```
    internal class NestingDoll
    {
      private readonly NestingDoll _innerDoll;
      private NestingDoll GetInnerDoll() { return _innerDoll; }
    
      public NestingDoll(int howManyInnerDolls = 1) 
      {
        _innerDoll = howManyInnerDolls <= 0 ? null : new NestingDoll(howManyInnerDolls - 1); 
      }

      public int CountInnerDolls()...
    }
  ```

  Then to get the actual count of inner dolls was simple, we know that the last doll will have an inner doll value of null.
  So we can loop through and increment an int and set the current doll to the old doll's inner doll.
  Once the current doll is null we have hit the end of the chain of dolls.

  ```
    public int CountInnerDolls()
    {
        var count = 0; 
        var currentDoll = this._innerDoll; 
        while (currentDoll != null) 
        {
            ++count; // We increment the count
            currentDoll = currentDoll.GetInnerDoll(); 
        }
        return count; 
    }
  ```

This gets us the number of dolls that are inside a doll and to get the total number of dolls inside a list of dolls we just have to loop through and count.
We set up a temp var named count that will hold our total number and loop through the list of dolls. we then add the current dolls count to the var.

```
  var count = 0;
  foreach (var doll in nestingDolls)
  {
    count += doll.CountInnerDolls();
  }
  Console.WriteLine($"Count: {count}");
```

This has a time complexity of O(n) since we only go through the list once here.
</details>
<details>
<summary>Part Two: Line Up Sets</summary>
  Here we want to take the list of dolls and line them up in order from smallest to largest. We continue to use the doll class from above.
  Since we are ordering a list I thought the best use of my time would be to use linq since its built to do that type of thing.

  ```
    IEnumerable<NestingDoll> smallestDolls = nestingDolls.OrderBy(dolls => dolls.CountInnerDolls());
    foreach (var doll in smallestDolls)
    {
      Console.WriteLine($"count: {doll.CountInnerDolls()}");
    }
  ```

  When it comes to sorting linq gives us the best time efficency of O(n<sup>2</sup>) since it uses bubble sort.
</details>
<details>
<summary>Part Three: Find Unique in Set</summary>
  Here we added a bool to the doll class that signifies that the doll is signed or not

  ```
  private bool _isSigned = false;
  ``` 

  ```
  public bool checkIfSigned()
  {
    return _isSigned;
  }
  ```

now that the doll has a signifier it is as simple as looping through the list of dolls and checking if the doll is signed.
if it is we can return that doll and do whatever we want with it.

```
public static NestingDoll findTheSignedOne(List<NestingDoll> nestingDolls)
    {
        foreach (var doll in nestingDolls)
        {
            Console.WriteLine($"count: {doll.CountInnerDolls()}");
            if (doll.checkIfSigned())
            {
                return doll;
            }
        }
    }
```

This only checks the outside doll for a signiture allowing it to be O(n) since at most it only takes however many dolls there are in total. 
if you wanted to be able to hide the signiture further in you would have to loop through each of the dolls inner dolls this would make the time O(n<sup>2</sup>).
</details>
