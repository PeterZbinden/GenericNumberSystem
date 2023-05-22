# GenericNumberSystem
A Library that lets you define and use custom Number-Systems.

https://www.nuget.org/packages/GenericNumberSystem
https://www.nuget.org/packages/GenericNumberSystem.Abstractions/

Octal:

'''
	var oct = new GenericNumberSystem.NumberSystem("01234567");
	for (int i = 0; i <= 20; i++)
	{
		Console.WriteLine(oct.Convert(i));
	}
'''

Custom:

'''

	var custom = new GenericNumberSystem.NumberSystem("abcdefg");
	for (int i = 0; i <= 20; i++)
	{
		Console.WriteLine(custom.Convert(i));
	}

'''