public class HeapSort{
    public void MaxHeapify(Heap heap, int i){
        int l = Left(i);
        int r = Right(i);
        int largest = int.MinValue;
        if(l < heap.HeapSize && heap.Array[l] > heap.Array[i])
            largest = l;
        else largest = i;
        if(r < heap.HeapSize && heap.Array[r] > heap.Array[i])
            largest = r;
        if(largest != i){
            int leftOperand = heap.Array[i];
            heap.Array[i] = heap.Array[largest];
            heap.Array[largest] = leftOperand;
            MaxHeapify(heap, largest);
        }
    }

    public Heap BuildMaxHeap(int[] arr){
        var h = new Heap(arr.Length);
        for(int i = 0; i < h.Size; i++){
            h.Array[i] = arr[i];
        }
        h.HeapSize = arr.Length;
        for(int i = arr.Length / 2 - 1; i >= 0; i--){
            MaxHeapify(h, i);
        }
        return h;
    }

    public int[] Heapsort(int[] arr){
        var heap = BuildMaxHeap(arr);
        for(int i = arr.Length - 1; i > 0; i--){
            int leftOperand = heap.Array[i];
            heap.Array[0] = heap.Array[heap.Size - 1];
            heap.Array[heap.Size - 1] = leftOperand;
            heap.HeapSize -= 1;
            MaxHeapify(heap, i);
        }
        return heap.Array;
    }

    public int Parent(int i){
        return i>>1;
    }

    public int Left(int i){
        return i<<1;
    }

    public int Right(int i){
        return (i<<1) + 1;
    }

    public class Heap{
        public Heap(int n){
            Size = n;
            Array = new int[n];
        }
        public int Size {get; private set;}
        public int HeapSize {get; set;}
        public int[] Array {get; private set;}
    }
}