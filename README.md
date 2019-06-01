# minesweeper-api

MineSweeper game as an api

## How to play

### Start a new game

POST /game
{
    "boardSizeX": 10,
    "boardSizeY": 10,
    "mines": 10
}

Returns
{
    "gameId": "29e6cf60-adb4-4e96-a634-9ff5adb675e2"
}

This creates a board of the size {boardSizeX} X {boardSizeY} and randomly places {mines} on the board.

Default board size is 10x10 with 10 mines, which you get if there is no body in the request.
Game timer starts when you try the first square.

### Test a position on the board

PUT /game/{gameId}/{x,y}

If there is no mine, returns
{
    "gameStatus": "running",
    "mines": 1,
    "isMine": false
}

If there is a mine, returns
{
    "gameStatus": "ended",
    "mines": 9,
    "isMine": true
}
