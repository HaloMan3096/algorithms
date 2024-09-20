# My class work for 'Algorithms'

## Assignment I: Code Examples and Portfolio Start
>Create methods that show:
>Constant O(1)
>Linear O(n)
>Quadratic O(n2)

### Constant: 
```
Console.WriteLine(args[0]);
```
accessing the first member of a list is always in <b> Constant Time </b>

### Linear:
```
foreach (var s in args)
{
  Console.WriteLine(s);
}
```
Going through all of a list is in <b> Linear Time </b>

### Quadratic: 
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
