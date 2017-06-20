open System.Drawing
open System.IO 
open System

type Plotter = {
    position : int*int
    direction:float
    color: Color
    bitmap: Bitmap 
}
let linedrawer (x1,y1) (plotter:Plotter) =
    let (x0,y0)= plotter.position 
    let updatedPlotter = {plotter with position = (x1,y1)} 
    let xLen = float (x1-x0) 
    let yLen = float (y1-y0)
 
    
    let x0,y0,x1,y1 = if x0>x1 then x1,y1,x0,y0 else x0,y0,x1,y1
    if xLen <> 0.0 then  
        for x in x0..x1 do 
        let proportion = float (x- x0) / xLen
        let y= (int (Math.Round(yLen * proportion))) + y0
        plotter.bitmap.SetPixel(x,y,plotter.color) 
    
    let x0,y0,x1,y1 = if y0>y1 then x1,y1,x0,y0 else x0,y0,x1,y1    
    if yLen <> 0.0 then  
        for y in y0..y1 do 
        let proportion = float (y-y0) / yLen
        let x= (int (Math.Round(xLen * proportion))) + x0
        plotter.bitmap.SetPixel(x, y, plotter.color )
    
    updatedPlotter


let turn amt plotter= 
    let newDir = plotter.direction + amt 
    let newAngle = {plotter with direction = newDir }
    newAngle 
   

let move dist plotter= 
    let curPosition = plotter.position 
    let angle       = plotter.direction
    let startX      = fst plotter.position
    let startY      = snd plotter.position
    let rads        = (angle - 90.0) * Math.PI / 180.0
    let roundX      = (float startX) + (float dist) * cos rads 
    let roundY      = (float startY) + (float dist) * sin rads
    let endX        = int (Math.Round(roundX)) 
    let endY        = int (Math.Round(roundY))  
    let plotted     = linedrawer (int endX, int endY) plotter 
    printfn "%A" plotted 
    plotted 


let polygons (sides:int) length plotter = 
    let angle = Math.Round (360.0/ float sides)
    Seq.fold (fun s i -> turn angle (move length s)) plotter [1.0..(float sides)]  

let semiCircle (sides:int) length plotter = 
    let angle = Math.Round (360.0/ float sides)
    Seq.fold (fun s i -> turn angle (move length s)) plotter [1.0..(float sides/2.0)]  


let quartCircle (sides:int) length plotter = 
    let angle = Math.Round (360.0/ float sides)
    Seq.fold (fun s i -> turn angle (move length s)) plotter [1.0..(float sides/4.0)]  

 
let thirdCircle (sides:int) length plotter = 
    let angle = Math.Round (360.0/ float sides)
    Seq.fold (fun s i -> turn angle (move length s)) plotter [1.0..(float sides/3.0)]

let fifteenthCircle (sides:int) length plotter = 
    let angle = Math.Round (360.0/ float sides)
    Seq.fold (fun s i -> turn angle (move length s)) plotter [1.0..(float sides/15.0)]  

let changecolor (color:Color) plotter = 
    {plotter with color=color}   

let saveAs name plotter =
    let path = Path.Combine(__SOURCE_DIRECTORY__, name)
    plotter.bitmap.Save(path) 

let moveto (x1,y1) plotter = 
    { plotter with position= (x1,y1) }


let generate cmdStripe times fromPlotter = 
    let cmdsGen = 
        seq {
            while true do 
            yield! cmdStripe }
    let cmds = cmdsGen |> Seq.take (times * (List.length cmdStripe))
    
    cmds |> Seq.fold (fun plot cmd -> cmd plot) fromPlotter 





 







 











