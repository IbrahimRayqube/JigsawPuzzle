using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHandler : Singleton<SceneHandler>
{
    public PuzzleBoardHandler board;
    public PuzzleBoardHandler boardLandScape;
    public PuzzleBoardHandler boardPortrait;
    public MenuManager menuManager;
    public MenuManager menuManagerPortrait;
    public bool isGamePlay = false;
    public PuzzleBoardHandler puzzleBoard;
    public string baseAssetsURL;
    public string baseAssetsURLPortrait;
    public Credentials playerData;
    public bool isPortrait = false;
    // Start is called before the first frame update
    void Start()
    {
        //puzzleBoard = boardLandScape;
        puzzleBoard = boardPortrait;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startGameWithDelay()
    {
        puzzleBoard.resetPuzzle();
        isGamePlay = true;
    }

    public void timeUp()
    {
        if (!isGamePlay)
            return;
        isGamePlay = false;
        if (board.score < board.allBoardBoxes.Length)
        {
            board.gameObject.SetActive(false);
            // Fail Situation
            menuManager.gamePlayHandler.gameEndText.text = "Time's up, Better luck next time, ";
            menuManager.gamePlayHandler.gameEndText.gameObject.SetActive(true);
            menuManager.gamePlayHandler.leaderBoard.SetActive(true);
            menuManager.gamePlayHandler.putStats(APIHandler.Instance.);
        }
    }


}
