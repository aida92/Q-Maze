using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Questioner : MonoBehaviour, NetResponse {

    const string SERVER = "http://localhost/qmaze/";
    public List<Question> questions;

    public List<Question> selected;
    public List<int> selectedIndexes;

    // Use this for initialization
    void Start() {
        questions = new List<Question>();
        selected = new List<Question>();
        selectedIndexes = new List<int>();

        StartCoroutine(NetHelper.get(SERVER + "pitanja.txt", this));
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void onResponse(string data) {
        Debug.Log("onResponse => DATA fetched -> Length => " + data.Length );
        StartCoroutine("parseQuestions", data);
    }

    private IEnumerator parseQuestions(String data) {
        Debug.Log("parseQuestions => DATA -> Length => " + data.Length);
        /** 
         * Pitanja su ucitana, sad mogu da se proslede MazeController-u i da se ubace u lavirint
         * 
         * GetComponent<T>() se koristi kako bi dobili ref. ka nekoj od komponenti koje su zakacene za trenutni gameObject
         * u ovom slucaju T -> MazeController
         * */
        var mazeController = GetComponent<MazeController>();
        questions.Clear();
        int notParsed = 0;
        string lastError = "";
        /** Split() funkcija ne radi sa string, vec sa char i zato koristimo => "\n"[0], tj. uzimamo 1. char iz string-a "\n" */
        foreach (var q in data.Split("\n"[0])) {
            try {
                questions.Add(new Question(q));
            } catch (Exception e) {
                lastError = e.Message;
                // U slucaju da pitanje nije lepo formatirano desice se Exception 
                // To pitanje se ne dodaje u listu
                //Debug.Log("ERROR => " + e.Message + ", data => " + q);
                notParsed++;
            }
        }
        Debug.Log("QUESTIONS PARSED => SIZE => " + questions.Count + ", NOT PARSED => " + notParsed );
        
        yield return null;
        // Sad imamo pitanja i mozemo da napravimo lavirint
        StartCoroutine(mazeController.createMaze());
    }

    public void setRandom(int maxQuestions) {
        if (maxQuestions <= 0 || maxQuestions >= questions.Count) {
            return;
        }
        selected.Clear();
        selectedIndexes.Clear();
        int cIndex = 0;
        for( var i = 0; i <= maxQuestions; i++) {
            cIndex = UnityEngine.Random.Range(0, questions.Count);
            while (selectedIndexes.Contains(cIndex)) {
                cIndex = UnityEngine.Random.Range(0, questions.Count);
            }
            selectedIndexes.Add(cIndex);
            selected.Add(questions[cIndex]);
        }
    }
}
