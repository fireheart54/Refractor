#load "SpirographFun.fsx"
open SpirographFun
open System.Drawing
open System.IO


let bitmap= new Bitmap(2000, 2000) 


let initialPlotter= {
    position= (1000,1000) 
    color= Color.Red 
    direction= 90.0
    bitmap= bitmap 

    }

let cmdStripe = [
    changecolor Color.Bisque
    move 45
    turn 45.0
    quartCircle 45 45 
    changecolor Color.Black
    turn 75.0 
    move 50 
    changecolor Color.DarkCyan
    thirdCircle 45 45 
    polygons 4 10  
            ]



generate cmdStripe 100 initialPlotter |> saveAs "Experiment.png"




