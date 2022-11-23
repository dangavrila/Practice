namespace MergeSortPractice;

public static class MergeSort{
    private static void Merge(ref int[] A, int p, int q, int r)
    {
        int n1 = q - p + 1;
        int n2 = r - q;
        int[] L = new int[n1 + 1];
        int[] R = new int[n2 + 1];
        int i=0, j=0;
        for(; i < n1; i++){
            L[i] = A[p + i];
        }
        for(; j < n2; j++){
            R[j] = A[q + j + 1];
        }
        L[^1] = int.MaxValue;
        R[^1] = int.MaxValue;
        i = j = 0;
        for(int k = p; k <= r; k++){
            if(L[i] <= R[j]){
                A[k] = L[i++];
            }
            else{
                A[k] = R[j++];
            }
        }
    }

    public static void Sort(ref int[] A, int p, int r){
        if( p >= r)
            return;
        if(p < r){
            int q = (int)Math.Floor((p+r) / 2m);
            Sort(ref A, p, q);
            Sort(ref A, q + 1, r);
            Merge(ref A, p, q, r);
        }
    }
}