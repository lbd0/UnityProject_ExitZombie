using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    CharacterController character;
    Ray ray;
    public Camera getCamera;
    private RaycastHit hit;
    private Vector3 move;
    float mouseX = 0;
    float speed = 4.0f;
    float jump = 10.0f;
 
    public float hp = 1;

    public Transform bullet;
    public Transform bullet_spawn;
    public GameObject gun;
    public float bullet_power = 2000.0f;
    public ParticleSystem muzzleFlash;

    public AudioSource audioSource;
    public AudioClip shoot_Sound;
    public AudioClip walk_Sound;


    private crosshair theCrosshair;
    private bool isMoving = false;
    private bool isShoot = false;

    string objectName;



    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        theCrosshair = FindObjectOfType<crosshair>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
            speed = 7.0f;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            speed = 4.0f;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerJump();
        }

        playerMove();

        mouseX += Input.GetAxis("Mouse X") * 7;
        transform.eulerAngles = new Vector3(0, mouseX, 0);

        if (Input.GetMouseButtonDown(0))
        { 
            ray = getCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            if (Physics.Raycast(ray, out hit))
            {
               objectName = hit.collider.gameObject.name;

                Debug.Log(objectName);
            }

            //if (objectName == "zombi")
                Shoot();
        }
        
    }

    void playerMove()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");


        move = new Vector3(moveX, 0, moveZ);

        if (moveX != 0 || moveZ != 0)
        {
            isMoving = true;
            Debug.Log(isMoving);
        }
        else
            isMoving = false;

        if (isMoving)
        {
            if (!audioSource.isPlaying)
            {
                PlaySE(walk_Sound);
             
            }
        }
        else
            audioSource.Stop();

        character.Move(transform.TransformDirection(move) * Time.deltaTime * speed);
        

    }

    void playerJump()
    {

         ;
         move.y += jump;
        character.Move(transform.TransformDirection(move) * Time.deltaTime);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "zombi")
        {
            hp -= 0.5f;

            if (hp == 0)
                GetComponent<HPControl>().Die();
        }

    }

    private void Shoot()
    {
        theCrosshair.ShootAnimaition();
        muzzleFlash.Play();
        PlaySE(this.shoot_Sound);
        Transform prefab_bullet = Instantiate(bullet, bullet_spawn.transform.position, getCamera.transform.rotation);
        Vector3 shooting_ray = ray.direction;
        prefab_bullet.GetComponent<Rigidbody>().AddForce(shooting_ray * bullet_power);
        
      
                
         
        
    }

    private void PlaySE(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();
    }

}
