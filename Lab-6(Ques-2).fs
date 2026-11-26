open System

let readMarks (subject: string) : float = // string -> float
    printf "Enter marks for %s (out of 100): " subject
    Console.ReadLine() |> float

let classify (percentage: float) : string = // float -> string
    match percentage with
    | p when p >= 60.0 -> "First Division"
    | p when p >= 50.0 -> "Second Division"
    | p when p >= 40.0 -> "Third Division"
    | _ -> "Fail"

[<EntryPoint>]
let main _ = // []<string> -> int
    let subjects : string list = [ "Subject 1"; "Subject 2"; "Subject 3"; "Subject 4" ]
    let total : float = subjects |> List.sumBy readMarks
    let percentage : float = total / 4.0
    printfn "Total marks: %.2f / 400" total
    printfn "Percentage: %.2f%%" percentage
    printfn "Division: %s" (classify percentage)
    0
