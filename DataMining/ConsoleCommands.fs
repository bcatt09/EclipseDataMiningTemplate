module ConsoleCommands

open System

let HeaderColor = ConsoleColor.DarkMagenta
let ProgressBarColor = ConsoleColor.DarkGreen
let ErrorColor = ConsoleColor.Red
let PromptColor = ConsoleColor.Yellow
let InfoColor = ConsoleColor.Cyan


let write (text: string) =
    Console.ResetColor()
    Console.WriteLine text

let writeOver (text: string) =
    let newText = sprintf "\r%-150s" text
    Console.ResetColor()
    Console.Write newText

let writeOverWithNewLine text =
    writeOver text
    write ""


let writeWithColor color (text: string) =
    Console.ForegroundColor <- color
    Console.WriteLine text

let writeOverWithColor color text =
    let newText = sprintf "\r%-150s" text
    Console.ForegroundColor <- color
    Console.Write newText

let writeOverWithColorAndNewLine color text =
    writeOverWithColor color text
    write ""


let writeWithTempColor color text =
    let prevColor = Console.ForegroundColor
    writeWithColor color text
    Console.ForegroundColor <- prevColor

let writeOverWithTempColor color text =
    let prevColor = Console.ForegroundColor
    writeOverWithColor color text
    Console.ForegroundColor <- prevColor

let writeOverWithTempColorAndNewLine color text =
    writeOverWithTempColor color text
    write ""


let writeSectionHeader text =
    writeWithTempColor HeaderColor $"-----------{text}-----------"

let writeError text =
    writeWithTempColor ErrorColor text

let writePrompt text =
    writeWithTempColor PromptColor text

let writeInfo text =
    writeWithTempColor InfoColor text

let block = "■"
let private progressBarWrite color (text: string) =
    let prevColor = Console.ForegroundColor
    Console.ForegroundColor <- color
    Console.Write text
    Console.ForegroundColor <- prevColor

/// percent 0.0 - 1.0
let writeProgressBarWithText percent (text: string) =
    Console.Write('\r')
    Console.ResetColor()
    Console.Write "["
    let numBlocks = int (percent * 100.0 / 10.0) + 1
    progressBarWrite ProgressBarColor (String.replicate numBlocks block + String.replicate (10 - numBlocks) " ")
    Console.Write $"] %0.1f{percent * 100.0}%%"
    if text <> ""
    then Console.Write($" - %-75s{text}")
    
/// percent 0.0 - 1.0
let writeProgressBar percent =
    writeProgressBarWithText percent ""
