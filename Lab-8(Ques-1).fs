open System

type Shape(name: string) =
    member this.Name = name // string
    abstract Area : unit -> float
    default this.Area() = 0.0 // unit -> float
    abstract Describe : unit -> string
    default this.Describe() = sprintf "%s with area %.2f" name (this.Area()) // unit -> string

type Circle(radius: float) =
    inherit Shape("Circle")
    override this.Area() = Math.PI * radius * radius // unit -> float

type Rectangle(width: float, height: float) =
    inherit Shape("Rectangle")
    override this.Area() = width * height // unit -> float

type Triangle(baseLen: float, height: float) =
    inherit Shape("Triangle")
    override this.Area() = 0.5 * baseLen * height // unit -> float

let baseShape : Shape = Shape("Generic Shape") // Shape
let circle : Circle = Circle(5.0) // Circle
let rectangle : Rectangle = Rectangle(4.0, 6.0) // Rectangle
let triangle : Triangle = Triangle(3.0, 4.0) // Triangle

printfn "=== Calling methods on each object directly ==="
printfn "%s area: %.2f" baseShape.Name (baseShape.Area())
printfn "%s area: %.2f" circle.Name (circle.Area())
printfn "%s area: %.2f" rectangle.Name (rectangle.Area())
printfn "%s area: %.2f" triangle.Name (triangle.Area())

printfn "\n=== Polymorphism: one list of Shape, different behaviour ==="
let shapes : Shape list = // list<Shape>
    [ baseShape
      circle :> Shape
      rectangle :> Shape
      triangle :> Shape
      Circle(2.5) :> Shape
      Rectangle(3.0, 3.0) :> Shape ]

for s : Shape in shapes do
    printfn "%s" (s.Describe())

let totalArea : float = shapes |> List.sumBy (fun s : Shape -> s.Area()) // float
printfn "\nTotal area of all shapes: %.2f" totalArea
