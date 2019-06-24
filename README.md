# minesweeper-api

MineSweeper game as an api.
Still in dev mode, mostly just some outlines to what it is supposed to do.

## How to play

### Start a new game

`POST /game`

```json
{
    "boardSizeX": 10,
    "boardSizeY": 10,
    "mines": 10
}
```

Returns `HTTP/1.1 201 Created` with the newly created Game Id.

```json
{
    "gameId": "29e6cf60-adb4-4e96-a634-9ff5adb675e2"
}
```

This creates a board of the size {boardSizeX} X {boardSizeY} and randomly places {mines} on the board.

Default board size is 10x10 with 10 mines, which you get if there is no body in the request.
Game timer starts when you try the first square.

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

`PUT /game/{gameId}/{x,y}`

If there is no mine, it returns `HTTP/1.1 200 OK` with the result.
If it is outside the board, you will receive `HTTP/1.1 404 NOT FOUND`.

```json
{
    "gameStatus": "running",
    "mines": 1,
    "isMine": false
}
```

If there is a mine, the body will contain

```json
{
    "gameStatus": "ended",
    "mines": 9,
    "isMine": true
}
```

### Get game stats

`GET /game/{gameId}/stats`

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
