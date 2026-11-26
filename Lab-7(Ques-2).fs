let list1 : int list = [1; 2; 3; 4; 5; 6; 7; 8; 9; 10] // list<int>
let list2 : int list = [-3; 7; 12; 0; 5; -8; 20] // list<int>

printfn "=== List.map: square each element ==="
let squares1 : int list = List.map (fun x -> x * x) list1 // list<int>
let squares2 : int list = List.map (fun x -> x * x) list2 // list<int>
printfn "Input 1 : %A" list1
printfn "Squares : %A" squares1
printfn "Input 2 : %A" list2
printfn "Squares : %A" squares2

printfn "\n=== List.filter: keep only even numbers ==="
let evens1 : int list = List.filter (fun x -> x % 2 = 0) list1 // list<int>
let evens2 : int list = List.filter (fun x -> x % 2 = 0) list2 // list<int>
printfn "Input 1 : %A" list1
printfn "Evens   : %A" evens1
printfn "Input 2 : %A" list2
printfn "Evens   : %A" evens2

printfn "\n=== List.fold: sum, product and maximum ==="
let sum1 : int = List.fold (fun acc x -> acc + x) 0 list1 // int
let sum2 : int = List.fold (fun acc x -> acc + x) 0 list2 // int
let product1 : int = List.fold (fun acc x -> acc * x) 1 list1 // int
let product2 : int = List.fold (fun acc x -> acc * x) 1 list2 // int
let max1 : int = List.fold (fun acc x -> if x > acc then x else acc) (List.head list1) list1 // int
let max2 : int = List.fold (fun acc x -> if x > acc then x else acc) (List.head list2) list2 // int
printfn "List 1 -> sum = %d, product = %d, max = %d" sum1 product1 max1
printfn "List 2 -> sum = %d, product = %d, max = %d" sum2 product2 max2

printfn "\n=== List.reduce: sum of each list ==="
printfn "List 1 sum (reduce): %d" (List.reduce (fun a b -> a + b) list1)
printfn "List 2 sum (reduce): %d" (List.reduce (fun a b -> a + b) list2)
