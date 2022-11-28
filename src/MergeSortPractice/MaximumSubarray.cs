namespace MergeSortPractice{
    public static class MaximumSubarray{
        public static Tuple<int, int, int> FindMaxSubarray(ref int[] A, int low, int high){
            int mid = 0;
            if(low == high)
                return new Tuple<int, int, int>(low, high, A[low]);
            else mid = (int)Math.Floor((low + high) / 2m);
            int leftLow = 0;
            int leftHigh = 0;
            int leftSum = 0;
            var leftTuple = FindMaxSubarray(ref A, low, mid);
            leftLow = leftTuple.Item1;
            leftHigh = leftTuple.Item2;
            leftSum = leftTuple.Item3;
            int rightLow, rightHigh, rightSum;
            var rightTupple = FindMaxSubarray(ref A, mid + 1, high);
            rightLow = rightTupple.Item1;
            rightHigh = rightTupple.Item2;
            rightSum = rightTupple.Item3;
            int crossLow, crossHigh, crossSum;
            var crossTupple = FindMaxCrossingSubarray(ref A, low, mid, high);
            crossLow = crossTupple.Item1;
            crossHigh = crossTupple.Item2;
            crossSum = crossTupple.Item3;
            if(leftSum >= rightSum && leftSum >= crossSum)
                return new (leftLow, leftHigh, leftSum);
            if(rightSum >= leftSum && rightSum >= crossSum)
                return new (rightLow, rightHigh, rightSum);
            else return new (crossLow, crossHigh, crossSum);
        }

        private static Tuple<int, int, int> FindMaxCrossingSubarray(ref int[] A, int low, int mid, int high){
            int leftSum = int.MinValue;
            int maxLeftIdx = 0;
            int sum = 0;
            for(int i = mid; i >= low; i--){
                sum += A[i];
                if(sum > leftSum){
                    leftSum = sum;
                    maxLeftIdx = i;
                }
            }
            int rightSum = int.MinValue;
            int maxRightIdx = 0;
            sum = 0;
            for(int j = mid + 1; j <= high; j++){
                sum += A[j];
                if(sum > rightSum){
                    rightSum = sum;
                    maxRightIdx = j;
                }
            }
            return new (maxLeftIdx, maxRightIdx, leftSum + rightSum);
        }
    }
}