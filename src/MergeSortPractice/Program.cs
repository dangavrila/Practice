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

            ReshufleArray(ref array);
            Console.WriteLine("Reshufled array.");
            DisplayArray(ref array);

            Console.WriteLine("Find maximum subarray.");
            var maxSubarrayTuple = MaximumSubarray.FindMaxSubarray(ref array, 0, array.Length - 1);
            Console.WriteLine($"low = {maxSubarrayTuple.Item1}, high = {maxSubarrayTuple.Item2}, max = {maxSubarrayTuple.Item3}");

            Console.WriteLine("Done.");
        }

        static void ReshufleArray(ref int[]A){
            var random = new Random();
            for(int i = 0; i < A.Length - 1; i++){
                var ri = random.Next(i + 1, A.Length);
                if(ri != i){
                    var t = A[i];
                    A[i] = A[ri];
                    A[ri] = t;
                }
            }
        }
    }

    private static void DisplayArray(ref int[] A){
        for(int i = 0; i < A.Length; i++){
                Console.Write($"{A[i]}{(i < A.Length - 1 ? ", " : '.')}");
            }
        Console.WriteLine();
    }
}
