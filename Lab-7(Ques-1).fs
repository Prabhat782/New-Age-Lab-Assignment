open System
open System.Diagnostics

let stopwatch : Stopwatch = Stopwatch.StartNew() // Stopwatch

let log (msg: string) = // string -> unit
    printfn "[%5d ms] %s" stopwatch.ElapsedMilliseconds msg

let ioTask (name: string) (delayMs: int) : Async<string> = // string -> int -> Async<string>
    async {
        log (sprintf "%s started (I/O, %d ms)" name delayMs)
        do! Async.Sleep delayMs
        log (sprintf "%s finished" name)
        return sprintf "%s -> completed after %d ms" name delayMs
    }

let computeTask (name: string) (n: int) : Async<string> = // string -> int -> Async<string>
    async {
        log (sprintf "%s started (compute, n = %d)" name n)
        let mutable total : float = 0.0
        for i in 1 .. n do
            total <- total + Math.Sqrt(float i)
        log (sprintf "%s finished" name)
        return sprintf "%s -> sum of square roots = %.2f" name total
    }

let runWithParallel () = // unit -> array<string>
    log "=== Async.Parallel: starting 4 tasks ==="
    let results : string array =
        [ ioTask "Task-A (slow)" 1500
          ioTask "Task-B (medium)" 1000
          ioTask "Task-C (fast)" 500
          computeTask "Task-D (compute)" 5_000_000 ] // Async<string> list
        |> Async.Parallel // Async<string array>
        |> Async.RunSynchronously // string array
    log "=== All parallel tasks completed ==="
    results

let runWithStartChild () = // unit -> []<string>
    async {
        log "=== Async.StartChild: starting 3 child tasks ==="
        let! c1 : Async<string> = Async.StartChild(ioTask "Child-1" 1200)
        let! c2 : Async<string> = Async.StartChild(ioTask "Child-2" 800)
        let! c3 : Async<string> = Async.StartChild(ioTask "Child-3" 400)
        log "All children launched; main workflow is not blocked yet"
        let! r1 : string = c1
        let! r2 : string = c2
        let! r3 : string = c3
        return [| r1; r2; r3 |]
    } // Async<string array>
    |> Async.RunSynchronously // string array

[<EntryPoint>]
let main _ = // []<string> -> int
    let sw : Stopwatch = Stopwatch.StartNew()
    let parallelResults : string array = runWithParallel ()
    let parallelTime : int64 = sw.ElapsedMilliseconds

    printfn "\nResults (Async.Parallel):"
    parallelResults |> Array.iter (printfn "  %s")
    printfn "Total parallel time: %d ms\n" parallelTime

    sw.Restart()
    let childResults : string array = runWithStartChild ()
    let childTime : int64 = sw.ElapsedMilliseconds

    printfn "\nResults (Async.StartChild):"
    childResults |> Array.iter (printfn "  %s")
    printfn "Total StartChild time: %d ms\n" childTime

    printfn "Sequential would take ~3000+ ms for the I/O tasks alone,"
    printfn "but parallel finished in ~%d ms -> execution was NOT sequential." parallelTime
    0
