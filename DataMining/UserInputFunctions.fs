module UserInputFunctions

open System
open ConsoleCommands

/// List departments and get selection from user
let rec getDepartmentFromUser() =
    let departmentList = 
        [
            "All"
            "KCI Bay"
            "KCI Central"
            "KCI Clarkston"
            "KCI Detroit"
            "KCI Farmington"
            "KCI Flint"
            "KCI Lapeer"
            "KCI Lansing"
            "KCI Macomb"
            "KCI Northern"
            "KCI Owosso"
            "KCI Port Huron"
        ]
    writePrompt "Select a department to generate the report for:"
    write
        (departmentList 
        |> List.mapi(fun i x -> $"{i+1} - {x}")
        |> String.concat "\n")
    let input = Console.ReadLine()
    match Int32.TryParse input with
    | (true, num) when num > 0 && num < 14 -> departmentList.[num - 1]
    | _ -> getDepartmentFromUser()

/// Prompt user for date range selection
let rec getNumberOfDays() =
    writePrompt "Enter number of days to search for plans in:"
    let input = Console.ReadLine()
    match Int32.TryParse input with
    | (true, num) when num > 0 -> num
    | _ -> getNumberOfDays()