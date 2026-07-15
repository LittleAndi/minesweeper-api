# minesweeper-api

MineSweeper game as an api.
Still in dev mode, mostly just some outlines to what it is supposed to do.

## How to play

### Start a new game

`POST /api/game`

```json
{
    "boardSizeX": 10,
    "boardSizeY": 10,
    "mines": 10
}
```

Returns `HTTP/1.1 201 Created` with the newly created game and a `Location` header pointing at it.

```json
{
    "gameId": "29e6cf60-adb4-4e96-a634-9ff5adb675e2",
    "boardSizeX": 10,
    "boardSizeY": 10,
    "mines": 10
}
```

This creates a board of the size {boardSizeX} X {boardSizeY} and randomly places {mines} on the board.

Default board size is 10x10 with 10 mines, which you get if there is no body in the request.
Board dimensions must be at least 1, and the mine count must leave at least one safe square,
otherwise you receive `HTTP/1.1 400 Bad Request`.

Sample board

```
╔══════════╗
║*    *    ║
║          ║
║  *       ║
║   *  *   ║
║          ║
║*         ║
║          ║
║  *       ║
║   *   ** ║
║          ║
╚══════════╝
```

### Test a position on the board

`PUT /api/game/{gameId}/{x},{y}`

If there is no mine, it returns `HTTP/1.1 200 OK` with the result.
If the square is outside the board, or the game id is unknown, you will receive `HTTP/1.1 404 Not Found`.
If the game is already won or lost, you will receive `HTTP/1.1 400 Bad Request`.

```json
{
    "gameStatus": "inProgress",
    "isMine": false,
    "adjacentMines": 1
}
```

If there is a mine, the game is lost and the body will contain

```json
{
    "gameStatus": "lost",
    "isMine": true,
    "adjacentMines": 0
}
```

Reveal all safe squares to win; the final reveal returns `"gameStatus": "won"`.

### Get game stats (not implemented yet)

`GET /api/game/{gameId}/stats`

Returns `HTTP/1.1 200 OK`

```json
{
    "gameId": "29e6cf60-adb4-4e96-a634-9ff5adb675e2",
    "status": "ended",
    "missionStatus": "success",
    "created": "2019-08-15T10:00:00.000Z",
    "started": "2019-08-10:05:00.000Z",
    "ended": "2019-08-15T10:10:10.000Z",
    "boardSizeX": 10,
    "boardSizeY": 10,
    "mines": 10,
    "minesCleared": 10,
    "boardId": "a211b24c-1427-42e4-8e7e-8369eaaf32bb"
}
```

`boardId` is only included if the game has ended.

## Support hosting

If you find this "game" or project fun and want to support the hosting you can donate a Coffe.

[![ko-fi](https://www.ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/N4N6W9C7)
