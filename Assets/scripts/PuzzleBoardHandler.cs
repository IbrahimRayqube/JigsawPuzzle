using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PuzzleBoardHandler : MonoBehaviour
{
    public PieceHandler[] allBoardBoxes;
    public Sprite currentImage;
    public GameObject selectedPuzzleBlock;
    public float rMinX, rMaxX, lMinX, lMaxX, yMin, yMax;
    public float timer;
    public int score;
    public GameObject BGVideo;

    // Start is called before the first frame update
    void Start()
    {
        BGVideo.GetComponent<VideoPlayer>().url = "C:\\Users\\ibrah\\Downloads\\Video\\Landscape.mp4";
        //resetPuzzle();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && SceneHandler.Instance.isGamePlay)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            Debug.Log("object clicked: " + hit.collider.name);
            if (hit.transform.CompareTag("PuzzleBlock"))
            {
                if (!hit.transform.GetComponent<PieceHandler>().isOnRightPosition && !hit.transform.GetComponent<PieceHandler>().replacing)
                {
                    selectedPuzzleBlock = hit.transform.gameObject;
                }
            }
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (selectedPuzzleBlock != null)
            {
                selectedPuzzleBlock.GetComponent<PieceHandler>().checkCurrentPosition();
                selectedPuzzleBlock = null;
            }
        }
    }

    private void FixedUpdate()
    {
        if (selectedPuzzleBlock != null)
        {
            Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectedPuzzleBlock.transform.position = new Vector3(mPos.x, mPos.y, 0);
        }
    }

    public void changePuzzle()
    {
        foreach (PieceHandler p in allBoardBoxes)
        {
            p.gameObject.GetComponent<SpriteRenderer>().sprite = currentImage;
        }
    }

    public void onClickChangePic()
    {
        changePuzzle();
    }

    public void resetPuzzle()
    {
        score = 0;
        StartCoroutine(resetPuzzleWithDelay());
    }

    IEnumerator resetPuzzleWithDelay()
    {
        yield return new WaitForSeconds(3);
        int temp;
        foreach (PieceHandler p in allBoardBoxes)
        {
            temp = Random.Range(0, 2);
            if (temp == 0)
            {
                Vector3 newPosition = new Vector3(Random.Range(rMinX, rMaxX), Random.Range(yMin, yMax), 0);
                p.replacePiece(newPosition);
            }
            else if (temp == 1)
            {
                Vector3 newPosition = new Vector3(Random.Range(lMinX, lMaxX), Random.Range(yMin, yMax), 0);
                p.replacePiece(newPosition);
            }
        }
        SceneHandler.Instance.menuManager.gamePlayHandler.gameObject.SetActive(true);
        SceneHandler.Instance.menuManager.gamePlayHandler.startTimer(10);
    }

    public void incrementScore()
    {
        score++;
        if (score >= allBoardBoxes.Length)
        {
            SceneHandler.Instance.isGamePlay = false;
            Debug.Log("You win");
            SceneHandler.Instance.menuManager.gamePlayHandler.gameEndText.gameObject.SetActive(true);
            SceneHandler.Instance.menuManager.gamePlayHandler.leaderBoard.SetActive(true);
            //SceneHandler.Instance.playerData.score
        }
    }
}
