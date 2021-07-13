module SqlQueries

open FSharp.Data

let [<Literal>] private connectionString = "Data Source=10.71.248.60;Initial Catalog=VARIAN;User ID=reports;Password=R3p0rtsUs3r"

let sqlGetRecentPatientsFromHospital hospitalName daysToSearch =
    let cmd = new SqlCommandProvider<const(SqlFile<"SQL Queries\GetRecentPatientsFromHospital.sql">.Text), connectionString>(connectionString)
    cmd.Execute(hospitalName, daysToSearch + 30)
    |> Seq.map(fun x -> x.PatientId)
    |> Seq.toList

let sqlGetRecentPatientsFromAllHospitals daysToSearch =
    let cmd = new SqlCommandProvider<const(SqlFile<"SQL Queries\GetRecentPatientsFromAllHospitals.sql">.Text), connectionString>(connectionString)
    cmd.Execute(daysToSearch + 30)
    |> Seq.map(fun x -> x.PatientId)
    |> Seq.toList
