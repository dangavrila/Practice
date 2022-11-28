using ReverseArrayConsole;

// See https://aka.ms/new-console-template for more information

int n = 0;
int[] A = null!;
Console.Write("n=");
bool readOk = false;
while(!readOk){
    readOk = int.TryParse(Console.ReadLine(), out n);
    if(readOk){
        A = new int[n];
    }
}
for(int i = 0; i < n; i++){
    Console.Write($"A[{i}]=");
    int value = 0;
    readOk = false;
    while(!readOk){
        readOk = int.TryParse(Console.ReadLine(), out value);
        if(readOk)
            A[i] = value!;
    }
    Console.WriteLine($"Recorded A[{i}]={A[i]!}");
}
Console.WriteLine("Original array:");
Solution.DisplayArray(ref A);
Console.WriteLine("Reversed array:");
Solution.Reverse(ref A);
Solution.DisplayArray(ref A);
