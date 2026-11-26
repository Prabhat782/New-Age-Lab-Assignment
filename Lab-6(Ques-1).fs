open System

let rec hasDivisor (n: int) (d: int) : bool = // int -> int -> bool
    if d * d > n then false
    elif n % d = 0 then true
    else hasDivisor n (d + 1)

let isPrime (n: int) : bool = // int -> bool
    n > 1 && not (hasDivisor n 2)

[<EntryPoint>]
let main _ = // []<string> -> int
    let testSets : (string * int list) list =
        [ ("Set 1 (small numbers)", [ 0; 1; 2; 3; 4; 5 ])
          ("Set 2 (primes)",        [ 7; 11; 13; 29; 97 ])
          ("Set 3 (composites)",    [ 9; 15; 49; 100; 121 ]) ]

    testSets // (string * int list) list
    |> List.iter (fun (name: string, numbers: int list) -> // unit
        printfn "%s" name
        numbers // int list
        |> List.iter (fun (n: int) -> // unit
            let result : string = if isPrime n then "Prime" else "Not prime"
            printfn "  %d -> %s" n result)
        printfn "")

    printf "Enter an integer: "
    let input : int = Console.ReadLine() |> int
    printfn "%d is %s" input (if isPrime input then "prime" else "not prime")
    0
