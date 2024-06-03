open System
open System.IO
open TCPSession.FSM.TCPSessionFSMModule
open TCPSession.FSM.States.FSMStateEnum 

printfn "Введите имя файла c набором событий"
let fileName = Console.ReadLine()


let readFile(fileName : string) =
   try
    File.ReadLines(fileName)
    |> Seq.filter(fun x -> x |> System.String.IsNullOrWhiteSpace |> not)
    |> Seq.map(fun x -> Enum.Parse(x))
    |> Seq.toArray
    with | _ -> raise (Exception("Ошибка чтения файла :"))

let fsm = TCPSession()
let events = readFile fileName

let mutable counter = 0
let mutable endMessage = ""
let mutable handlerResult = "CLOSED"

while(counter < events.Length && counter <> -1) do
   handlerResult <- fsm.HandleEvent(Array.get events counter)

   if handlerResult = "ERROR" then
        counter <- -1
   else
        counter <- counter + 1
done

Console.WriteLine handlerResult