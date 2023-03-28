using System.Collections.Generic;
using UnityEngine;


public class Board{

    private string _gameName;
    private List<List<CellTypes>> _board;
    private int _playerType;
    private int _boardType;
    private Vector3 _spawnStartPoint;

    public Board(){
        _board = new List<List<CellTypes>>();
        _spawnStartPoint = new Vector3(0,0,0);
    }

    public Board(List<List<CellTypes>> board , Vector3 spawnStartPoint){
        this._board = board;
        this._spawnStartPoint = spawnStartPoint;
    }
    
    //Copy Constructor
    public Board(Board gameData){
        _gameName = gameData.getGameName();
        _board = gameData.getBoard();
        _playerType = gameData.getPlayerType();
        _boardType = gameData.getBoardType();
        _spawnStartPoint = gameData.getSpawnStartPoint();
    }

    public void setGameName(string gameName){
        this._gameName = gameName;
    }

    public string getGameName(){
        return this._gameName;
    }

    public void setBoard(List<CellTypes>[] board){
        this._board.AddRange(board);
    }

    public List<List<CellTypes>> getBoard(){
        return this._board;
    }

    public CellTypes getItem(int x,int y){
        return _board[x][y];
    }

    public void setItem(int x ,int y ,CellTypes type){
        _board[x][y] = type;
    }

    public int getPlayerType(){
        return _playerType;
    }

    public void setPlayerType(int playerType){
        this._playerType = playerType;
    }

    public int getBoardType(){
        return _boardType;
    }

    public void setBoardType(int boardType){
        this._boardType = boardType;
    }

    public Vector3 getSpawnStartPoint(){
        return this._spawnStartPoint;
    }
    
    public void setSpawnStartPoint(Vector3 spawnPoint){
        this._spawnStartPoint = spawnPoint;
    }

    public void clearBoard(){
        _board.Clear();
        _spawnStartPoint = new Vector3(0,0,0);
    }
}