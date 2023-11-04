open System

// Define the number of rows and columns for the grid
let numRows = 20 // Number of rows
let numCols = 40 // Number of columns

// Function to create an empty grid
let createGrid() =
    Array2D.init numRows numCols (fun _ _ -> false)

// Function to randomize the initial state of the grid
let randomizeGrid(grid : bool [,]) =
    let rand = Random()
    for row in 0 .. numRows - 1 do
        for col in 0 .. numCols - 1 do
            grid.[row, col] <- rand.Next(2) = 1

// Define an array of available colors for grid cell visualization
let availableColors =
    [|
        ConsoleColor.Red
        ConsoleColor.Green
        ConsoleColor.Blue
        ConsoleColor.Cyan
        ConsoleColor.Magenta
        ConsoleColor.Yellow
    |]

// Function to print the grid to the console with colorful cell representations
let printGrid(grid : bool [,]) =
    Console.Clear()
    for row in 0 .. numRows - 1 do
        for col in 0 .. numCols - 1 do
            if grid.[row, col] then
                // Choose a random color for live cells
                let randomColor = availableColors.[Random().Next(availableColors.Length)]
                Console.ForegroundColor <- randomColor
                Console.Write("O ")
            else
                // Set the color for dead cells to black
                Console.ForegroundColor <- ConsoleColor.Black
                Console.Write("* ")
        Console.WriteLine()
    Console.ResetColor()

// Function to count the number of live neighbors for a given cell
let countAliveNeighbors(grid : bool [,]) row col =
    let mutable count = 0
    for r in row - 1 .. row + 1 do
        for c in col - 1 .. col + 1 do
            if r >= 0 && r < numRows && c >= 0 && c < numCols && (r <> row || c <> col) then
                if grid.[r, c] then
                    count <- count + 1
    count

// Function to update the grid based on Conway's Game of Life rules
let updateGrid(grid : bool [,]) =
    let newGrid = createGrid()
    for row in 0 .. numRows - 1 do
        for col in 0 .. numCols - 1 do
            let neighbors = countAliveNeighbors grid row col
            if grid.[row, col] then
                // Apply rules for live cells
                newGrid.[row, col] <- neighbors = 2 || neighbors = 3
            else
                // Apply rules for dead cells
                newGrid.[row, col] <- neighbors = 3
    newGrid

// Main program entry point
let main() =
    let mutable grid = createGrid()
    randomizeGrid grid
    while true do
        printGrid grid
        grid <- updateGrid grid
        System.Threading.Thread.Sleep(400) // you can make it faster or slower 

main()
