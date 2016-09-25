using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerControll : MonoBehaviour {
    public float currentSpeed;
    public float speed;
    public float rotationSpeed;

    public GameObject uiHolder;
    public GameObject uiYesNoHolder;
    public GameObject uiAnswersHolder;
    public GameObject uiAnswerButtonPref;
    public GameObject splashHolder;
    private GameObject btnStart;
    private int splashCounterMax = 12;
    private int splashCounter = 0;
    private RectTransform imgLoader;
    private float loaderWidthFull;
    private float loaderWidth;

    public Text uiQuestionText;
    private List<int> wrongAnswers;
    private static string DEFAULT_QUESTION = "Tacan odgovor na pitanje donosi 1 bod.\n (za svaki netacan se oduzima 1 bod)\nDa li zelite da odgovorite na pitanje?\nMozete koristiti misa ili brojeve od 1 do 9";
    private static string END_MESSAGE = "Cestitamo!!! Pronasli ste izlaz i osvojili ";

    private List<GameObject> walls;
    public float wallShowRadius = 3f;

    /**
     * LayerMask je layer koji se prikazuje na kameri. MiniMap kamera prikazuje samo Wall i Player layer,
     * dok kamera koja se nalazi u player objektu prikazuje sve layere.
     * Ako je layer objekta setovan na layerWall taj objekat ce biti prikazan na MiniMap kameri
     */
    private LayerMask layerWall;
    private LayerMask layerDefault;

    /**
     * Kad keepTrail true zidovi u miniMap view se ne brisu
     **/
    public bool keepTrail;

    /**
     * characterAnimator je GameObject koji sadrzi komponentu Animator,
     * a setovan je iz Editor-a, drag&drop >> MaleCharacterAnimation << objekta
     */
    public Animator characterAnimator;
    private int anim_var_walk = Animator.StringToHash("should_walk");
    private int anim_var_speed = Animator.StringToHash("speed");
    private int anim_trig_camera = Animator.StringToHash("toggleCamera");
    private AnimatorStateInfo animState;

    public Animator cameraAnimator;

    private int _movingDirection = 0;
    public bool canMove = true;
    
    private QuestionItem _currentQuestion;

    // Use this for initialization
    void Start (){
        wrongAnswers = new List<int>();
        walls = new List<GameObject>();
        layerWall = LayerMask.NameToLayer("Wall");
        layerDefault = LayerMask.NameToLayer("Default");
        /** Pozovi showWalls funkciju za 1s i ponavljaj je na 0.5s */
        InvokeRepeating("showWalls", 1f, 0.5f);
        uiQuestionText.text = DEFAULT_QUESTION;

        btnStart = splashHolder.transform.FindChild("btnStart").gameObject;
        btnStart.SetActive(false);
        imgLoader = splashHolder.transform.FindChild("loader").gameObject.GetComponent<RectTransform>();
        loaderWidthFull = imgLoader.sizeDelta.x;
        imgLoader.sizeDelta = new Vector2(0, 15);
        InvokeRepeating("splashWait", 1, 0);
    }
    
    void splashWait() {
        if ( splashCounter > splashCounterMax) {
            imgLoader.sizeDelta = new Vector2(loaderWidthFull, 15);
            imgLoader.gameObject.SetActive(false);
            btnStart.SetActive(true);
            return;
        }
        splashCounter++;
        loaderWidth += loaderWidthFull / splashCounterMax;
        imgLoader.sizeDelta = new Vector2( loaderWidth, 15);
        InvokeRepeating("splashWait", 0.2f, 0);
    }

    void startGame() {
        splashHolder.SetActive(false);

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow)) {
            // Prekida animaciju kretanja
            characterAnimator.SetBool(anim_var_walk, false);
            characterAnimator.SetFloat(anim_var_speed, currentSpeed*86);
        }
        if (!uiHolder.activeSelf) {
            if (Input.GetKey(KeyCode.UpArrow)) {
                if (!canMove && _movingDirection < 0) {
                    canMove = true;
                }
                _movingDirection = 1;
                if (canMove) {
                    currentSpeed = Input.GetAxis("Vertical") * Time.deltaTime * speed;
                    transform.Translate(new Vector3(0, 0, currentSpeed));
                    // Ukljucuje animaciju kretanja
                    characterAnimator.SetBool(anim_var_walk, true);
                    characterAnimator.SetFloat(anim_var_speed, currentSpeed * 86);
                }
            }
            if (Input.GetKey(KeyCode.DownArrow)) {
                if (!canMove && _movingDirection > 0) {
                    canMove = true;
                }
                _movingDirection = -1;
                if (canMove) {
                    currentSpeed = Input.GetAxis("Vertical") * Time.deltaTime * speed;
                    transform.Translate(new Vector3(0, 0, currentSpeed));
                    /** 
                     * Ukljucuje animaciju kretanja
                     * Ovo moze da se uradi i ovako (gde je KEY string):
                            characterAnimator.SetBool("should_walk", true);
                        ali zbog brzeg pronalazenja promenljive koristimo INT (anim_var_walk) id umesto STRING-a
                    */
                    characterAnimator.SetBool(anim_var_walk, true);
                    characterAnimator.SetFloat(anim_var_speed, currentSpeed * 86);
                }
            }
        }
        if (!uiAnswersHolder.activeSelf && !uiHolder.activeSelf) {
            if (Input.GetKey(KeyCode.LeftArrow)) {
                transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.RightArrow)) {
                transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
            }
        }else {
            int pressedKey = 0;
            if (Input.GetKeyUp(KeyCode.Keypad1)) {
                pressedKey = 1;
            } else if (Input.GetKeyUp(KeyCode.Keypad2)) {
                pressedKey = 2;
            } else if (Input.GetKeyUp(KeyCode.Keypad3)) {
                pressedKey = 3;
            } else if (Input.GetKeyUp(KeyCode.Keypad4)) {
                pressedKey = 4;
            } else if (Input.GetKeyUp(KeyCode.Keypad5)) {
                pressedKey = 5;
            }

            if (pressedKey > 0) {
                if (uiAnswersHolder.activeSelf) {
                    answerSelected(pressedKey-1);
                } else {
                    if( pressedKey == 1) {
                        showQuestion();
                    }else {
                        hideQuestion();
                    }
                }
            }
        }
    }

    void showWalls() {
        if (!keepTrail) {
            resetWalls();
        }
        /** 
         * malo t(ransform) i malo g(ameObject)... - nakacene skripte; a veliki su klase Transform i GameObject
         */
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, wallShowRadius);
        int i = 0;
        while (i < hitColliders.Length) {
            if( hitColliders[i].transform.tag == Waller.TAG || hitColliders[i].transform.tag == Waller.TAG_QUESTION) {
                //hitColliders[i].gameObject.layer = layerWall;
                hitColliders[i].gameObject.GetComponent<Waller>().updateLayers(layerWall);
                if (!keepTrail) {
                    walls.Add(hitColliders[i].gameObject);
                }
            }
            i++;
        }
    }

    void offerQuestion(QuestionItem item) {
        if (uiHolder.activeSelf) {
            return;
        }
        uiQuestionText.text = item.isEnd 
            ? ( END_MESSAGE + GetComponent<Score>().points + " poena.\nDa li zelite da pokusate ponovo?" )
            : DEFAULT_QUESTION;
        
        uiHolder.SetActive(true);
        _currentQuestion = item;
        cameraAnimator.SetTrigger(anim_trig_camera);
    }

    void hideQuestion() {
        Application.ExternalCall("testera", GetComponent<Score>().points);

        if( !uiHolder.activeSelf ) {
            Debug.Log("HIDE QUESTION");
            return;
        }

        if (_currentQuestion.isEnd) {
            Debug.Log("HIDE QUESTION");
            Application.Quit();
            return;
        }
        Debug.Log("HIDE QUESTION");

        _currentQuestion.gameObject.SetActive(false);
        canMove = true;

        uiHolder.SetActive(false);
        uiAnswersHolder.SetActive(false);
        uiYesNoHolder.SetActive(true);
        uiQuestionText.text = DEFAULT_QUESTION;
        cameraAnimator.SetTrigger(anim_trig_camera);

        InvokeRepeating("toggleQuestionItem", 5, 0);
    }

    void toggleQuestionItem() {
        if (_currentQuestion.isAnswered) {
            return;
        }

        _currentQuestion.gameObject.SetActive(true);
    }

    void showQuestion() {
        if( _currentQuestion.isEnd ) {
            _currentQuestion.isEnd = false;
            hideQuestion();
            var mazer = GameObject.FindGameObjectWithTag("Mazer").GetComponent<MazeController>();
            StartCoroutine(mazer.createMaze());
            return;
        }

        uiYesNoHolder.SetActive(false);
        uiQuestionText.text = _currentQuestion.question.question;
        
        // Obrisemo sve preduiAnswersHolder.transform
        for(var c = 0; c < uiAnswersHolder.transform.childCount; c++) {
            Destroy(uiAnswersHolder.transform.GetChild(c).gameObject);
        }
        wrongAnswers.Clear();
        uiAnswersHolder.SetActive(true);

        for (var i = 0; i < _currentQuestion.question.answers.Count; i++) {
            Debug.Log(" _currentQuestion.question.answers[i] => " + _currentQuestion.question.answers[i]);
            GameObject answerItem = Instantiate(uiAnswerButtonPref, uiAnswersHolder.transform.position, uiAnswersHolder.transform.rotation, uiAnswersHolder.transform) as GameObject;
            answerItem.GetComponent<QButton>().index = i;
            answerItem.GetComponent<QButton>().onClickListener = gameObject;
            answerItem.transform.GetChild(0).GetComponent<Text>().text = _currentQuestion.question.answers[i] + " ( " + (i+1) + " )";
        }

    }

    void answerSelected(int index) {
        if( wrongAnswers.Contains(index)) {
            Debug.Log("SELECTED ANSWER at INDEX => " + index + " WAS NOT CORRECT ANSWER");
            return;
        }

        if( _currentQuestion.question.answers.Count <= index) {
            Debug.Log("SELECTED ANSWER at INDEX => " + index + " DOES NOT EXISTS" );
            return;
        }
        Debug.Log("SELECTED ANSWER  at INDEX => " + index + " => " + _currentQuestion.question.answers[index].Trim().ToLower());
        Debug.Log("CORRENT ONE => " + _currentQuestion.question.correct.Trim().ToLower());
        if ( _currentQuestion.question.correct.Trim().ToLower() == _currentQuestion.question.answers[index].Trim().ToLower() ) {
            _currentQuestion.isAnswered = true;

            /** 
             * Saljemo signal updateScore sa parametrom 1.
             * Vidi ispod za objasnjenje SendMessage-a
            */
            SendMessage("updateScore", 1);
            hideQuestion();
            return;
        }
        /** Saljemo signal updateScore sa parametrom -1.
         * U svim script-ama (komponentama) koje su nakacene na isti gameObject, 
         * na koji je nakacena ova script-a (PlayerControll.cs) ce biti pozvan method "updateScore"
         * ako ga imaju
         * Trenutno "updateScore" method ima samo Score.cs
         *  */
        SendMessage("updateScore", -1);
        /** Sklanjamo pogresan odgovor iz liste ponudjenjih odgovora */
        for( int i = 0; i < uiAnswersHolder.transform.childCount; i++) {
            if(uiAnswersHolder.transform.GetChild(i).GetComponent<QButton>().index == index) {
                Destroy(uiAnswersHolder.transform.GetChild(i).gameObject);
                break;
            }
        }

        /** Dodajemo index pogresnog odgovora u listu wrongAnswers kako bi sprecili smanjivanje bodova ako korisnik greskom pritisne isti broj */
        wrongAnswers.Add(index);
    }

    void resetWalls() {
        foreach (GameObject wall in walls) {
            /** wall moze da bude obrisan (OnSpaceKey Pressed -> createMaze() -> Destroy u MazeController.cs)
             * pa zbog toga provera za null.
             * Kako MazeController.cs radi destroy svih wall-ova, mozemo da prekinemo FOR cim naidjemo na wall koji je NULL
             *  */
            if (wall == null) {
                break;
            }
            wall.GetComponent<Waller>().updateLayers(layerDefault);
        }
        walls.Clear();
    }
    
    void OnCollisionEnter(Collision collision) {
        if ( ( collision.collider.tag == Waller.TAG || collision.collider.tag == Waller.TAG_QUESTION )
            && _movingDirection != 0 ) {
            if( collision.collider.tag == Waller.TAG_QUESTION) {
                offerQuestion(collision.collider.GetComponent<QuestionItem>());
            }
            canMove = false;
        }
    }

    void OnCollisionExit(Collision collisionInfo) {
        if (collisionInfo.collider.tag == Waller.TAG || collisionInfo.collider.tag == Waller.TAG_QUESTION) {
            hideQuestion();
            canMove = true;
        }
    }


}
