namespace VMS.TPS

open System.Diagnostics
open System.Reflection
open VMS.TPS.Common.Model.API

type Script () =

    member this.Execute (context:ScriptContext) =

        let fullAssemblyName = Assembly.GetExecutingAssembly().Location
        let minusExtension = fullAssemblyName.[0..fullAssemblyName.Length-11]
        let exePath = minusExtension + ".exe"
                
        Process.Start(exePath) |> ignore
