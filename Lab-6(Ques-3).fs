open System
open System.Text.RegularExpressions

let reverseString (s: string) : string = // string -> string
    String(s |> Seq.rev |> Seq.toArray)

let isPalindrome (s: string) : bool = // string -> bool
    let lower : string = s.ToLower()
    lower = reverseString lower

let (|AllLetters|AllDigits|Mixed|) (s: string) = // string -> Choice<unit,unit,unit>
    if s <> "" && Seq.forall Char.IsLetter s then AllLetters
    elif s <> "" && Seq.forall Char.IsDigit s then AllDigits
    else Mixed

let transform (s: string) : string = // string -> string
    match s with
    | AllLetters -> s.ToUpper()
    | AllDigits -> string (bigint.Parse s * 2I)
    | Mixed -> s

let emailPattern : string = // string
    @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9-]+(\.[A-Za-z0-9-]+)*\.[A-Za-z]{2,}$"

let (|ValidEmail|InvalidEmail|) (s: string) = // string -> Choice<unit,unit>
    if Regex.IsMatch(input = s, pattern = emailPattern) then ValidEmail else InvalidEmail

let validateEmail (s: string) : string = // string -> string
    match s with
    | ValidEmail -> "Valid"
    | InvalidEmail -> "Invalid"

[<EntryPoint>]
let main _ = // []<string> -> int
    printfn "=== Task 1: Reverse and Palindrome ==="
    [ "madam"; "Racecar"; "hello"; "level"; "fsharp" ] // string list
    |> List.iter (fun s : string -> // unit
        printfn "%-10s -> reversed: %-10s palindrome: %b" s (reverseString s) (isPalindrome s))

    printfn ""
    printfn "=== Task 2: Transform String ==="
    [ "hello"; "FSharp"; "12345"; "007"; "abc123"; "hi there" ] // string list
    |> List.iter (fun s : string -> // unit
        printfn "%-10s -> %s" s (transform s))

    printfn ""
    printfn "=== Task 3: Email Validation ==="
    [ "user@example.com"
      "john.doe@mail.co.in"
      "invalid.email"
      "@nouser.com"
      "name@domain" ] // string list
    |> List.iter (fun s : string -> // unit
        printfn "%-22s -> %s" s (validateEmail s))

    0
