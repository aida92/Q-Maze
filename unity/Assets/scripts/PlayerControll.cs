using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControll : MonoBehaviour {
    public float currentSpeed;
    public float speed;
    public float rotationSpeed;

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
    private AnimatorStateInfo animState;

    // Use this for initialization
    void Start (){
        walls = new List<GameObject>();
        layerWall = LayerMask.NameToLayer("Wall");
        layerDefault = LayerMask.NameToLayer("Default");

        /** Pozovi showWalls funkciju za 1s i ponavljaj je na 0.5s */
        InvokeRepeating("showWalls", 1f, 0.5f);
    }
    
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow)) {
            // Prekida animaciju kretanja
            characterAnimator.SetBool(anim_var_walk, false);
            characterAnimator.SetFloat(anim_var_speed, currentSpeed*86);
        }

        if ( Input.GetKey(KeyCode.UpArrow)) {
            currentSpeed = Input.GetAxis("Vertical") * Time.deltaTime * speed;
            transform.Translate(new Vector3(0, 0, currentSpeed ) );
            // Ukljucuje animaciju kretanja
            characterAnimator.SetBool(anim_var_walk, true);
            characterAnimator.SetFloat(anim_var_speed, currentSpeed * 86);
        }
        if (Input.GetKey(KeyCode.DownArrow)){
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
        if (Input.GetKey(KeyCode.LeftArrow)){
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow)){
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
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
            if( hitColliders[i].transform.tag == Waller.TAG ) {
                hitColliders[i].gameObject.layer = layerWall;
                if (!keepTrail) {
                    walls.Add(hitColliders[i].gameObject);
                }
            }
            i++;
        }
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
            wall.layer = layerDefault;
        }
        walls.Clear();
    }

}
