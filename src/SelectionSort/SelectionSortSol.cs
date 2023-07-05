public static class SelectionSortSol{
    public static void Sort(int[] A){
        for(int j = 0; j < A.Length - 1; j++){
            int minIdx = GetNextMinIndex(j + 1, ref A);
            if(A[j] > A[minIdx])
            {
                int t = A[j];
                A[j] = A[minIdx];
                A[minIdx] = t;
            }
        }
    }

    private static int GetNextMinIndex(int startIdx, ref int[] array){
        int minIdx = startIdx;
        int min = int.MaxValue;
        for(int i = startIdx; i < array.Length; i++){
            if (array[i] < min)
            {
                minIdx = i;
                min = array[i];
            }
        }
        return minIdx;
    }
}