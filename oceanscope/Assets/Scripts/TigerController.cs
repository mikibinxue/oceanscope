using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TigerController : MonoBehaviour
{
    float Speed = 4;
    float Rot = 0;
    float RotSpeed = 80;
    float Gravity = 8;

    Vector3 MoveDir = Vector3.zero;

    CharacterController Controller;
    Animator Anim;

    //for counting
    private int Count;
    public Text CountText;
    public Text WinText;

    // Start is called before the first frame update


    void Start()
    {
        Controller = GetComponent<CharacterController>();
        Anim = GetComponent<Animator>();
        SetCountText();
        WinText.text = "";



    }

    // Update is called once per frame
    void Update()
    {
        //move forward and backward
        if (Input.GetKeyDown(KeyCode.W))
        {
            Anim.SetInteger("condition",1);
            MoveDir = new Vector3(0, 0, 1);
            MoveDir *= Speed;
            MoveDir = transform.TransformDirection(MoveDir);
         
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            Anim.SetInteger("condition", 0);
            MoveDir = new Vector3(0, 0, 0);
         
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Anim.SetInteger("condition", 1);
            MoveDir = new Vector3(0, 0, -1);
            MoveDir *= Speed;
            MoveDir = transform.TransformDirection(MoveDir);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            Anim.SetInteger("condition", 0);
            MoveDir = new Vector3(0, 0, 0);
        }



        //jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Anim.SetInteger("condition", 2);
            MoveDir = new Vector3(0, 3, 1);
            MoveDir *= Speed;
            MoveDir = transform.TransformDirection(MoveDir);
            Debug.Log(Anim.GetInteger("condition"));

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Anim.SetInteger("condition", 0);
            MoveDir = new Vector3(0, 0, 0);

        }


        //rotate left and right
        if ((Input.GetKeyDown(KeyCode.A)) || (Input.GetKeyDown(KeyCode.D)))
        {
            Anim.SetInteger("condition", 1);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            Anim.SetInteger("condition", 0);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            Anim.SetInteger("condition", 0);
        }

        Rot += Input.GetAxis("Horizontal") * RotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, Rot, 0);

        //update direction
       
        Controller.Move(MoveDir * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            Count += 1;
            SetCountText();

            if (Count>=3)
            {
                WinText.text = "You Win";
            }
        } 
    }

    void SetCountText()
    {
        CountText.text = "Score: " + Count.ToString();

    }
}
