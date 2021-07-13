module DataStructures

open VMS.TPS.Common.Model.Types

type EclipseData =
    {
        PatientName: string
        PatientId: string
        Hospital: string
        DataPoint1: string
        DataPoint2: DoseValue option
    }

type ResultData =
    {
        PatientName: string
        PatientId: string
        Hospital: string
        DataPoint1: string option
        newDataPoint2: double
    }

let resultsToCsv data =
    data
    |> List.map(fun x -> $"{x.PatientId},{x.Hospital},{x.DataPoint1 |> Option.map id},{x.newDataPoint2}")
    |> List.append(["Patient ID,Hospital,DataPoint1,newDataPoint2"])