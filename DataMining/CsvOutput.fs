module CsvOutput

open System
open System.IO
open System.Reflection
open ConsoleCommands
open DataStructures

let directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\"
let resultsFilename = directory + "output.csv"

// Write to CSV
let writeToCsv (results: ResultData list) =
    let writer = new StreamWriter(resultsFilename)

    results
    |> resultsToCsv
    |> String.concat "\n"
    |> writer.Write

    writer.Close()

    writeInfo $"Results have been saved to {resultsFilename}"