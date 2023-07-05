public class MergeSortProcedure{
    public void MergeSort(ref int[] A, int p, int r){
        if( p >= r)
            return;
        if(p < r){
            int q = (int)Math.Floor((p+r)/2m);
            MergeSort(ref A, p, q);
            MergeSort(ref A, q + 1, r);
            Merge(ref A, p, q, r);
        }
    }

    private void Merge(ref int[] a, int p, int q, int r)
    {
        int n1 = q - p + 1;
        int n2 = r - q;
        int[] L = new int[n1 + 1];
        int[] R = new int[n2 + 1];
        for(int i = 0; i < n1; i++){
            L[i] = a[p + i];
        }
        for(int j = 0; j < n2; j++){
            R[j] = a[q + j + 1];
        }
        L[n1 - 1] = int.MaxValue;
        R[n2 - 1] = int.MaxValue;
        int ii = 0;
        int jj = 0;
        for(int k = p; k < r; k++){
            if(L[ii] <= R[jj]){
                a[k] = L[ii++];
            }
            else{
                a[k] = R[jj++];
            }
        }
    }
}
