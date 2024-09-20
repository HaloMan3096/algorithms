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
