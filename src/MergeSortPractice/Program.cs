namespace MergeSortPractice;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Merge");
        int n =0;
        
        Console.Write("n = ");
        if(int.TryParse(Console.ReadLine(), out n)){
            int[] array = new int[n];
            Console.WriteLine("Elements: ");
            for(int i = 0; i < n; i++){
                Console.Write($"Element [{i}] = ");
                bool readOk = false;
                do{
                    readOk = int.TryParse(Console.ReadLine(), out var elemValue);
                    array[i] = elemValue;
                }
                while(!readOk);
            }
            Console.Write("Input recorded. Press Enter to continue.");
            Console.ReadLine();
            Console.WriteLine("Array: ");
            DisplayArray(ref array);
            MergeSort.Sort(ref array, 0, n - 1);
            Console.WriteLine("Sorted. Press Enter to display results.");
            Console.ReadLine();
            DisplayArray(ref array);
            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }

    private static void DisplayArray(ref int[] A){
        for(int i = 0; i < A.Length; i++){
                Console.Write($"{A[i]}{(i < A.Length - 1 ? ", " : '.')}");
            }
        Console.WriteLine();
    }
}
