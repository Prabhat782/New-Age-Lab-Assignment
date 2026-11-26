open System
open Microsoft.Data.Sqlite

let execute (conn: SqliteConnection) (sql: string) (parameters: (string * obj) list) =
    use cmd = conn.CreateCommand()
    cmd.CommandText <- sql
    for (name, value) in parameters do
        cmd.Parameters.AddWithValue(name, value) |> ignore
    cmd.ExecuteNonQuery() |> ignore

let printStudents (conn: SqliteConnection) (title: string) =
    printfn "--- %s ---" title
    use cmd = conn.CreateCommand()
    cmd.CommandText <- "SELECT Id, Name, Age, Course FROM Students ORDER BY Id"
    use reader = cmd.ExecuteReader()
    while reader.Read() do
        printfn "Id=%d | Name=%s | Age=%d | Course=%s"
            (reader.GetInt32 0) (reader.GetString 1) (reader.GetInt32 2) (reader.GetString 3)
    printfn ""

[<EntryPoint>]
let main _ =
    use conn = new SqliteConnection("Data Source=students.db")
    conn.Open()

    execute conn "DROP TABLE IF EXISTS Students" []
    execute conn "CREATE TABLE Students (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL, Age INTEGER NOT NULL, Course TEXT NOT NULL)" []

    let insertSql = "INSERT INTO Students (Name, Age, Course) VALUES (@name, @age, @course)"
    execute conn insertSql [ "@name", box "Asha"; "@age", box 20; "@course", box "CS" ]
    execute conn insertSql [ "@name", box "Ravi"; "@age", box 21; "@course", box "IT" ]
    execute conn insertSql [ "@name", box "Meera"; "@age", box 22; "@course", box "ECE" ]
    printStudents conn "After CREATE (3 students inserted)"

    execute conn "UPDATE Students SET Age = @age, Course = @course WHERE Name = @name"
        [ "@age", box 22; "@course", box "AI"; "@name", box "Ravi" ]
    printStudents conn "After UPDATE (Ravi -> age 22, course AI)"

    execute conn "DELETE FROM Students WHERE Name = @name" [ "@name", box "Meera" ]
    printStudents conn "After DELETE (Meera removed)"

    0
