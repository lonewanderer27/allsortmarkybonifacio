using System;

class Program
{
    static int lastInd;

    // MAIN
    static void Main(string[] args)
    {
        // User input up to 5 digits (dunno)
        Console.Write("ENTER HOW MANY ITEMS: ");
        int totalNum = Convert.ToInt32(Console.ReadLine());

        int[] arr = new int[totalNum];
        lastInd = arr.Length;

        for (int i = 0; i < arr.Length; i++){
            Console.Write("[" + i + "]: ");

            if (int.TryParse(Console.ReadLine(), out arr[i]))
                Console.WriteLine("--- ADDED ---");
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                i--;
            }
        }

        dispArr("ORIGINAL ARRAY", arr);
        
        int choiceNum;

        while (true){
            // Lets user choose if [0] MERGE SORT or [1] HEAP SORT
            Console.WriteLine("[0] MERGE SORT | [1] HEAP SORT");
            Console.Write("Choice: ");

            int choiceNum1;

            if (int.TryParse(Console.ReadLine(), out choiceNum)){
                switch(choiceNum){
                    case 0:
                        Console.WriteLine("You chose MERGE SORT");
                        Console.WriteLine("---");
                        
                        while (true){
                            Console.WriteLine("[0] MERGE ASCENDING | [1] MERGE DESCENDING");
                            Console.Write("Choice: ");

                            if (int.TryParse(Console.ReadLine(), out choiceNum1)){
                                switch(choiceNum1){
                                    case 0:
                                        Console.WriteLine("You chose M-ASCENDING");
                                        Console.WriteLine("---");
                                        mergeSort(arr, 0);
                                        break;
                                    case 1:
                                        Console.WriteLine("You chose M-DESCENDING");
                                        Console.WriteLine("---");
                                        mergeSort(arr, 1);
                                        break;
                                    default:
                                        Console.WriteLine("Invalid choice. 0 or 1 only.");
                                        break;
                                }
                            }

                            if (choiceNum1 == 0 || choiceNum1 == 1){
                                break;
                            }
                        }

                        break;

                    case 1:
                        Console.WriteLine("You chose HEAP SORT");
                        Console.WriteLine("---");
                        
                        while (true){
                            Console.WriteLine("[0] HEAP ASCENDING | [1] HEAP DESCENDING");
                            Console.Write("Choice: ");

                            if (int.TryParse(Console.ReadLine(), out choiceNum1)){
                                switch(choiceNum1){
                                    case 0:
                                        Console.WriteLine("You chose H-ASCENDING");
                                        Console.WriteLine("---");
                                        
                                        for (int i = 0; i < arr.Length; i++){
                                            // Pass 0 to go to Max Heap
                                            arr = heapSort(arr, 0);
                                            dispArr("Max Heap[" + (i + 1) + "]", arr);
                                            
                                            // Heapify
                                            arr = heapify(arr);
                                            dispArr("Heapify[" + (i + 1) + "]", arr);
                                        }

                                        break;
                                    case 1:
                                        Console.WriteLine("You chose H-DESCENDING");
                                        Console.WriteLine("---");

                                        for (int i = 0; i < arr.Length; i++){
                                            // Pass 1 to go to Min Heap
                                            arr = heapSort(arr, 1);
                                            dispArr("Min Heap[" + (i + 1) + "]", arr);
                                            
                                            // Heapify
                                            arr = heapify(arr);
                                            dispArr("Heapify[" + (i + 1) + "]", arr);
                                        }

                                        break;
                                    default:
                                        Console.WriteLine("Invalid choice. 0 or 1 only.");
                                        break;
                                }
                            }

                            if (choiceNum1 == 0 || choiceNum1 == 1){
                                break;
                            }
                        }

                        break;

                    default:
                        Console.WriteLine("Invalid choice. 0 or 1 only.");
                        break;
                }
                
                if (choiceNum == 0 || choiceNum == 1){
                    break;
                }
            }
            else{
                Console.WriteLine("Invalid choice. 0 or 1 only.");
            }
        }
    }

    // [[1]] MERGE SORT
    static int[] mergeSort(int[] arr, int ascending)
    {
        if (ascending == 0)
        {
            dispArr("Initial Array", arr);
            arr = arrCheck(arr, ascending);
        }
        else if (ascending == 1)
        {
            dispArr("Initial Array", arr);
            arr = arrCheck(arr, ascending);
        }

        return arr;
    }

    // Checking the array
    static int[] arrCheck(int[] arr, int ascending)
    {
        if (arr.Length > 1)
        {
            int half = arr.Length / 2;

            int[] arrL = arrCutHalf(arr, 0, half, "Left");
            int[] arrR = arrCutHalf(arr, half, arr.Length, "Right");

            arrL = arrCheck(arrL, ascending);
            arrR = arrCheck(arrR, ascending);

            return arrMerge(arrL, arrR, ascending);
        }
        else
        {
            return arr;
        }
    }

    // Cutting the array in half
    static int[] arrCutHalf(int[] oldArr, int from, int to, string arrSide){
        Console.WriteLine("--- CUT HALF ---");
        int[] newArr = new int[to - from];
        Console.WriteLine("new size[" + arrSide + "]: " + newArr.Length);

        int ctr = 0;
        for (int i = from; i < to; i++){
            newArr[ctr] = oldArr[i];
            ctr++;
        }

        dispArr("newArr[" + arrSide + "]", newArr);
        return newArr;
    }

    // Merging all Arrays
    static int[] arrMerge(int[] arrL, int[] arrR, int ascending)
    {
        Console.WriteLine("--- MERGE SORT ---");
        int[] arrMerge = new int[arrL.Length + arrR.Length];

        int indMerge = 0;
        int indL = 0;
        int indR = 0;

        while (indL < arrL.Length && indR < arrR.Length)
        {
            bool condition = (ascending == 0) ? 
                             (arrL[indL] <= arrR[indR]) : 
                             (arrL[indL] >= arrR[indR]);
            
            string symbol = ascending == 0 ? " < " : " > ";

            Console.Write(arrL[indL] + symbol + arrR[indR]);
            if (condition)
            {
                arrMerge[indMerge] = arrL[indL];
                Console.WriteLine(" : true, insert " + arrL[indL]);
                indL++;
            }
            else
            {
                arrMerge[indMerge] = arrR[indR];
                Console.WriteLine(" : false, insert " + arrR[indR]);
                indR++;
            }

            indMerge++;
        }

        while (indL < arrL.Length)
        {
            arrMerge[indMerge] = arrL[indL];
            Console.WriteLine("insert " + arrL[indL]);
            indL++;
            indMerge++;
        }

        while (indR < arrR.Length)
        {
            arrMerge[indMerge] = arrR[indR];
            Console.WriteLine("insert " + arrR[indR]);
            indR++;
            indMerge++;
        }

        dispArr("arrMerge", arrMerge);
        return arrMerge;
    }

    // [[2]] HEAP SORT 
    static int[] heapSort(int[] arr, int ascending){
        for (int i = lastInd; i > 1; i--){
            // Console.WriteLine("checking arr[" + i + "]:" + arr[i-1]);
            int parent = (i / 2);
            int LChild = (2 * parent);
            int RChild = (2 * parent + 1);
            parent--;
            LChild--;
            RChild--;

            Boolean hasRChild = false;
            if (parent < lastInd){
                // Console.WriteLine("P:" + arr[parent]);
            }
            if (LChild < lastInd){
                // Console.WriteLine("LC:" + arr[LChild]);
                // hasLChild = true;
            }
            if (RChild < lastInd){
                // Console.WriteLine("RC:" + arr[RChild]);
                hasRChild = true;
            }

            // ASCENDING/MAX HEAP SORT
            if (ascending == 0){
                if (hasRChild){
                    if (arr[parent] < arr[RChild] & arr[RChild] > arr[LChild]){
                        int temp = arr[parent];
                        arr[parent] = arr[RChild];
                        arr[RChild] = temp;
                    }
                }
                if (arr[parent] < arr[LChild]){
                    int temp = arr[parent];
                    arr[parent] = arr[LChild];
                    arr[LChild] = temp;
                }
            }

            // DESCENDING/MIN HEAP SORT
            else if (ascending == 1){
                if (hasRChild){
                    if (arr[parent] > arr[RChild] && arr[RChild] < arr[LChild])
                    {
                        int temp = arr[parent];
                        arr[parent] = arr[RChild];
                        arr[RChild] = temp;
                    }
                }
                if (arr[parent] > arr[LChild]){
                    int temp = arr[parent];
                    arr[parent] = arr[LChild];
                    arr[LChild] = temp;
                }
            }
        }

        return arr;
    }

    // Heapify
    static int[] heapify(int[] arr){
        int temp = arr[0];
        arr[0] = arr[lastInd - 1];
        arr[lastInd - 1] = temp;

        lastInd--;

        return arr;
    }

    // Displays the array.
    static void dispArr(String txt, int[] arr){
        Console.Write(txt + ": ");
        for (int i = 0; i < arr.Length; i++){
            Console.Write(arr[i] + ", ");
        }
        Console.Write("\n");
    }
}
