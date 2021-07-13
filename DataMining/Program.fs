open System
open ConsoleCommands
open SqlQueries
open VMS.TPS.Common.Model.API
open UserInputFunctions
open EclipseModule
open CsvOutput
        
[<EntryPoint; STAThread>]
let main _ =
    try
        // Login to Eclipse
        writeInfo "Logging into Eclipse..."
        let app = Application.CreateApplication()

        // Get user input
        let hospitalToSearch = getDepartmentFromUser()
        let daysToSearch = getNumberOfDays()

        // Get data from Eclipse and analyze
        writeInfo "Running Evaluation..."
        let results =
            match hospitalToSearch with
            | "All" -> sqlGetRecentPatientsFromAllHospitals daysToSearch |> getEclipseInfoFromAllPatients app
            | hosp -> sqlGetRecentPatientsFromHospital hosp daysToSearch |> getEclipseInfoFromAllPatients app

        // Print to CSV
        results |> writeToCsv
        
        // Exit
        Console.ReadKey() |> ignore
        app.Dispose()
    with ex -> 
        // Display any error messages
        writeError $"{ex.Message}\n\n{ex.InnerException}"
        Console.ReadKey() |> ignore
    0
