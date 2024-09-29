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
- [X] Part 1: Create an algorithm that will count how many dolls there are in total from all sets.
- [ ] Part 2: Create an algorithm that will line the doll sets up from the one with the smallest nested amount to the largest.
- [ ] Part 3: One of the dolls is unique and has the artist’s signature on it. Create an algorithm that searches through all dolls in all sets until it finds the one with the signature (and then stops).
- [X] Analyze your algorithm using big O notation.

<details>
<summary>PART 1</summary>
<details>
<summary>Algorithm</summary>
  <i>I might have gone a bit overboard on this one</i>
  
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
</details>
<details>
<summary>Break Down</summary>
  The code above in the algorithm drop down, getting the cound of the total dolls is actually linear time complexity <i>O(x)</i>.
  Since we are using a while loop the code takes as long to run as there are number of dolls to execute.
</details>
</details>

<details>
<summary>PART 2</summary>
<details>
<summary>Algorithm<summary>
Continuing with the same doll class from above
</details>
</details>
