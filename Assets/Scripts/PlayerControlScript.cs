using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControlScript : MonoBehaviour
{
    CharacterController _cc;
    //GameDirector _gd;
    Animator _anim;
    Transform _firstPerson;
    Transform _fpWeapon;
    GameObject _hud;

    private float _gravity = 9.8f;

    public float _moveSpdX;
    public float _moveSpdY;
    public float _mouseSpdX;
    public float _mouseSpdY;

    public int _gunDmg;

    private bool _canControl;
    private float _pitch;


    private void Awake()
    {
        _cc = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();
        //_gd = GameObject.FindGameObjectWithTag("GameDirector").GetComponent<GameDirector>();
        _firstPerson = transform.Find("FirstPerson");
        _fpWeapon = _firstPerson.Find("FPWeapon");
        _hud = GameObject.FindGameObjectWithTag("HUD");
    }
    // Start is called before the first frame update
    void Start()
    {
        _canControl = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(!_canControl)
        {
            return;
        }
        Move();
        Turn();
        LookUpDown();
        RaycastShootTest();
    }

    public void Die()
    {
        //disable control
        _canControl = false;
        //tell game director this player has died
        //_gd.PlayerDie(this);
    }

    private void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        _anim.SetFloat("MoveX", inputX);
        _anim.SetFloat("MoveY", inputY);
        Vector3 movement = inputX * transform.right * Time.deltaTime + inputY * transform.forward * Time.deltaTime;
        if(_cc.isGrounded)
        {
            _cc.Move(movement);
        }
        else
        {
            _cc.Move(new Vector3(0, -_gravity * Time.deltaTime, 0));
        }
    }

    private void LookUpDown()
    {
        float input = Input.GetAxis("Mouse Y");
        _pitch -= input * _mouseSpdY * Time.deltaTime;
        //limit pitch
        if(_pitch > 90)
        {
            _pitch = 90;
        }
        else if(_pitch < -90)
        {
            _pitch = -90;
        }
        _anim.SetFloat("Pitch", _pitch);
        _firstPerson.localEulerAngles = new Vector3(_pitch, 0, 0);
    }

    private void Turn()
    {
        float input = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, input * _mouseSpdX * Time.deltaTime);
    }

    private void RaycastShootTest()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(_firstPerson.transform.position, _firstPerson.transform.forward, out hit, 50))
            {
                Debug.DrawRay(_firstPerson.transform.position, _firstPerson.transform.forward, Color.red, 3, false);
                BodyPartScript bodyPart = hit.transform.GetComponent<BodyPartScript>();
                if (bodyPart != null)
                {
                    bodyPart.Hit(_gunDmg);
                }
            }
        }
    }
}
