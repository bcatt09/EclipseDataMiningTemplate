module EclipseModule

open ConsoleCommands
open DataStructures
open VMS.TPS.Common.Model.API
open System

let analyzeEclipseData (data: EclipseData) : ResultData =
    let tempStringOption = 
        match data.DataPoint1 with
        | "" -> None
        | someString -> Some someString
    
    {
        PatientName = data.PatientName
        PatientId = data.PatientId
        Hospital = data.Hospital
        DataPoint1 = tempStringOption
        newDataPoint2 = data.DataPoint2 |> Option.fold (fun _ x -> x.Dose) 0.0
    }

let getEclipseInfoFromPatient (pat: Patient) =
    pat.Courses
    |> Seq.filter(fun x -> not (x.Id.ToUpper().Contains("QA")))
    |> Seq.map(fun x -> x.PlanSetups)
    |> Seq.concat
    |> Seq.filter(fun x ->
        match (DateTime.TryParse(x.TreatmentApprovalDate)) with
        | true, date -> date > (DateTime.Today - (new TimeSpan(30, 0, 0, 0)))
        | false, _ -> false)
    |> Seq.map(fun x -> 
        {
            PatientName = $"{pat.LastName}, {pat.FirstName}"
            PatientId = pat.Id.ToString()
            Hospital = pat.Hospital.Id
            DataPoint1 = x.Id
            DataPoint2 = Some x.TotalDose
        })
    |> Seq.map(fun x -> analyzeEclipseData x)
    |> Seq.toList

let getEclipseInfoFromAllPatients (app: Application) (patientIds: string list) =
    let results = 
        patientIds
        |> List.mapi(fun i x ->
            let pat = app.OpenPatientById(x)
            //writeOver (pat.Name)
            writeProgressBarWithText (float i / float (Seq.length patientIds)) pat.Name
            let dataList = getEclipseInfoFromPatient pat
            app.ClosePatient()
            match Seq.length dataList with
            | 0 -> None
            | _ -> Some dataList)
        |> List.choose id
        |> List.concat
    writeOverWithNewLine "Done"
    results