﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MazeGeneration;

public class MazeController : MonoBehaviour {
	/** Kada script-a extend-uje/implementira MonoBehaviour intf UnityEngine (il nesto trece) 
	 * 	poziva Start, Stop, Update, FixedUpdate & etc evente u script-i.
	 *  Za bolje objasnjenje pitaj Google :p :D
	*/
	public GameObject prefTopBottom;
	public GameObject prefTopLeft;
	public GameObject prefRightLeft;
	public GameObject prefBottomLeft;
	public GameObject prefBottomRigth;
	public GameObject prefTopRight;
	public GameObject prefLeft;
	public GameObject prefRight;
	public GameObject prefBottom;
	public GameObject prefTop;
	public GameObject prefTopRightBottom;
	public GameObject prefTopLeftBottom;
	public GameObject prefTopRightLeft;
    public GameObject prefRightBottomLeft;

    public GameObject prefQuestion;
    public GameObject prefEnd;

    public GameObject player;

	public Maze maze;

	public Vector3 startPosition = Vector3.zero;
	public Vector2 mazeSize = Vector2.zero;

	public Transform mazeHolder;
	public List<GameObject> cells = new List<GameObject>();

    private Questioner _questioner;

    [Range(1, 20)]
    public int questionDensity = 7;

    // Use this for initialization
    void Start() {
        _questioner = GetComponent<Questioner>();

    }

	// Update is called once per frame
	void Update() {
		if (Input.GetKeyUp (KeyCode.Space)) {
			createMaze();
		}
	}

	public IEnumerator createMaze() {
        yield return new WaitForSeconds(2);
        foreach (GameObject obj in cells) { // cells are 1x1 objects, like topRight
			Destroy (obj);
		}
        mazeHolder.position = Vector3.zero;
        cells.Clear ();

        _questioner.setRandom( Mathf.FloorToInt( mazeSize.x * mazeSize.y / questionDensity ) );

		maze = new Maze((int)mazeSize.x, (int)mazeSize.y, startPosition);
		Debug.Log ("START POINT => " + maze.startPoint);
		displayMaze();
        player.transform.position = new Vector3(maze.startPoint.X, 0, maze.startPoint.Y + 0.5f);

        GameObject eCell = Instantiate(prefEnd, new Vector3(mazeSize.x-1, 0, mazeSize.y - 0.5f), Quaternion.identity, mazeHolder) as GameObject;
        var endQ = new Question("Gotovo je. Nema Vise,Bla|Test|Kraj,Kraj");
        eCell.GetComponent<QuestionItem>().question = endQ;
        eCell.GetComponent<QuestionItem>().isEnd = true;
        eCell.GetComponent<QuestionItem>().setPlayer(player.transform);

        //mazeHolder.position = new Vector3(0.5f,0,0);
    }

	public void displayMaze() {
		int revY = 0;
        int insertQuestion = 0;
        int questionIndex = 0;
		for (var y = 0; y < maze._height; y++) {
			revY = maze._height - 1 - y;
            for (var x = 0; x < maze._width; x++) {
                GameObject cellPrefab = null;
				if (maze [x, y] == (CellState.Top | CellState.Bottom | CellState.Visited)) {
					cellPrefab = prefTopBottom;
				} else if (maze [x, y] == (CellState.Top | CellState.Left | CellState.Visited)) {
					cellPrefab = prefTopLeft;
				} else if (maze [x, y] == (CellState.Top | CellState.Left | CellState.Bottom | CellState.Visited)) {
					cellPrefab = prefTopLeftBottom;
				} else if (maze [x, y] == (CellState.Top | CellState.Right | CellState.Bottom | CellState.Visited)) {
					cellPrefab = prefTopRightBottom;
				} else if (maze [x, y] == (CellState.Right | CellState.Left | CellState.Visited)) {
					cellPrefab = prefRightLeft;
				} else if (maze [x, y] == (CellState.Bottom | CellState.Left | CellState.Visited)) {
					cellPrefab = prefBottomLeft;
				} else if (maze [x, y] == (CellState.Bottom | CellState.Right | CellState.Visited)) {
					cellPrefab = prefBottomRigth;
				} else if (maze [x, y] == (CellState.Top | CellState.Right | CellState.Visited)) {
					cellPrefab = prefTopRight;
				} else if (maze [x, y] == (CellState.Left | CellState.Visited)) {
					cellPrefab = prefLeft;
				} else if (maze [x, y] == (CellState.Right | CellState.Visited)) {
					cellPrefab = prefRight;
				} else if (maze [x, y] == (CellState.Bottom | CellState.Visited)) {
					cellPrefab = prefBottom;
				} else if (maze [x, y] == (CellState.Top | CellState.Visited)) {
					cellPrefab = prefTop;
				} else if (maze [x, y] == (CellState.Top | CellState.Right | CellState.Left | CellState.Visited)) {
					cellPrefab = prefTopRightLeft;
				} else if (maze [x, y] == (CellState.Right | CellState.Bottom | CellState.Left | CellState.Visited)) {
					cellPrefab = prefRightBottomLeft;
				} else {
					Debug.Log ("OVO NEMA, A TREBA => maze[x,y] => " + maze[x,y]);
				}

				if( cellPrefab != null ){
					cells.Add (Instantiate (cellPrefab, new Vector3 (x, 0, revY), Quaternion.identity, mazeHolder) as GameObject);
                }

                if (insertQuestion == questionDensity) {
                    GameObject qCell = Instantiate(prefQuestion, new Vector3(x, 0, revY+0.5f), Quaternion.identity, mazeHolder) as GameObject;
                    qCell.GetComponent<QuestionItem>().question = _questioner.selected[questionIndex];
                    qCell.GetComponent<QuestionItem>().setPlayer(player.transform);
                    cells.Add(qCell);
                    questionIndex++;
                    insertQuestion = 0;
                } else {
                    insertQuestion++;
                }
            }
		}
	}

	/** 
	* Checks if startPosition is valid
	*/
	[ExecuteInEditMode]
	void OnValidate(){
		startPosition.x = startPosition.x < 0 ? 0 : startPosition.x;
		startPosition.y = startPosition.y < 0 ? 0 : startPosition.y;

		if (startPosition.x > 0 && startPosition.x >= mazeSize.x) {
			startPosition.x = mazeSize.x - 1;
		}
		if (startPosition.y > 0 && startPosition.y >= mazeSize.y ) {
			startPosition.y = mazeSize.y - 1;
		}
	}
}
