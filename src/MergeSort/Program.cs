// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

int n = 0;
Console.Write("n = ");
var ns = Console.ReadLine();
int.TryParse(ns, out n);
int[] arr = new int[n];
for(int i=0;i<n;i++){
    Console.Write($"A[{i}]=");
    var value = Console.ReadLine();
    if(value == null)
        throw new InvalidOperationException();
    arr[i] = int.Parse(value);
}
Console.Write("A = ");
for(int i = 0; i < n; i++){
    Console.Write($"{arr[i]}, ");
}

MergeSortProcedure mergeSortProcedure = new MergeSortProcedure();
mergeSortProcedure.MergeSort(ref arr, 0, n - 1);

Console.WriteLine();
Console.WriteLine("Merge sorted array:");
Console.Write("A = ");
for(int i = 0; i < n; i++){
    Console.Write($"{arr[i]}, ");
}

/*int[] arrayToSortByHeapsort = new int[] {5, 13, 2, 25, 7, 17, 20, 8, 4};
var heapsort = new HeapSort();
var arr = heapsort.Heapsort(arrayToSortByHeapsort);

Console.WriteLine();
Console.WriteLine("Heapsorted array:");
Console.Write("A = ");
for(int i = 0; i < arr.Length; i++){
    Console.Write($"{arr[i]}, ");
}
*/
Console.WriteLine("End.");
Console.ReadLine();