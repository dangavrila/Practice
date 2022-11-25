namespace ReverseArrayConsole{
    public static class Solution{
        public static void Reverse(ref int[] A){
            for(int i = 0; i < A.Length / 2; i++){
                int k = A.Length - i - 1;
                int temp = A[i];
                A[i] = A[k];
                A[k] = temp;
            }
        }
        public static void DisplayArray(ref int[] array){
            for(int i = 0; i < array.Length; i++){
                Console.Write($"{array[i]}{(i < array.Length - 1 ? ' ' : '.')}");
            }
            Console.WriteLine();
        }
    }
}