open System
open MongoDB.Bson
open MongoDB.Driver

[<EntryPoint>]
let main _ = // []<string> -> int
    let client : MongoClient = MongoClient("mongodb://localhost:27017")
    let db : IMongoDatabase = client.GetDatabase("labdb")
    db.DropCollection("students")
    let students : IMongoCollection<BsonDocument> = db.GetCollection<BsonDocument>("students")

    let makeStudent (name: string) (age: int) (course: string) =
        BsonDocument()
            .Add("name", BsonString name)
            .Add("age", BsonInt32 age)
            .Add("course", BsonString course)

    let printAll (title: string) =
        printfn "--- %s ---" title
        let docs : Collections.Generic.List<BsonDocument> = students.Find(Builders<BsonDocument>.Filter.Empty).ToList()
        for doc : BsonDocument in docs do
            printfn "Name=%s | Age=%d | Course=%s"
                (doc.["name"].AsString) (doc.["age"].AsInt32) (doc.["course"].AsString)
        printfn ""

    students.InsertMany([ makeStudent "Asha" 20 "CS"
                          makeStudent "Ravi" 21 "IT"
                          makeStudent "Meera" 22 "ECE" ])
    printAll "After CREATE (3 students inserted)"

    let csStudents : Collections.Generic.List<BsonDocument> = students.Find(Builders<BsonDocument>.Filter.Eq("course", "CS")).ToList()
    printfn "--- READ with filter (course = CS) ---"
    for doc : BsonDocument in csStudents do
        printfn "Name=%s | Age=%d" (doc.["name"].AsString) (doc.["age"].AsInt32)
    printfn ""

    let filter : FilterDefinition<BsonDocument> = Builders<BsonDocument>.Filter.Eq("name", "Ravi")
    let update : UpdateDefinition<BsonDocument> = Builders<BsonDocument>.Update.Set("age", 22).Set("course", "AI")
    students.UpdateOne(filter, update) |> ignore
    printAll "After UPDATE (Ravi -> age 22, course AI)"

    students.DeleteOne(Builders<BsonDocument>.Filter.Eq("name", "Meera")) |> ignore
    printAll "After DELETE (Meera removed)"

    0
