// See https://aka.ms/new-console-template for more information
Console.WriteLine("Selection sort");

int[] input = new int[] {5, 3, 7, 1, 8, 2};

SelectionSortSol.Sort(input);

Console.WriteLine("Result:");
for(int i=0; i<input.Length; i++){
    Console.Write($"{input[i]}, ");
}
Console.WriteLine("Ready.");
Console.ReadLine();