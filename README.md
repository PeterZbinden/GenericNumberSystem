# GenericNumberSystem
A Library that lets you define and use custom Number-Systems.

https://www.nuget.org/packages/GenericNumberSystem
https://www.nuget.org/packages/GenericNumberSystem.Abstractions/

Octal:

``` csharp 

	var oct = new GenericNumberSystem.NumberSystem("01234567");
	for (int i = 0; i <= 20; i++)
	{
		Console.WriteLine(oct.Convert(i));
	}
	
	// Output:
	// 0
    // 1
    // 2
    // 3
    // 4
    // 5
    // 6
    // 7
    // 10
    // 11
    // 12
    // 13
    // 14
    // 15
    // 16
    // 17
    // 20
    // 21
    // 22
    // 23
    // 24
```

Custom:

``` csharp 

	var custom = new GenericNumberSystem.NumberSystem("0abcdefg");
	for (int i = 0; i <= 20; i++)
	{
		Console.WriteLine(custom.Convert(i));
	}
	
	// Output:
	// 0
    // a
    // b
    // c
    // d
    // e
    // f
    // g
    // a0
    // aa
    // ab
    // ac
    // ad
    // ae
    // af
    // ag
    // b0
    // ba
    // bb
    // bc
    // bd

```