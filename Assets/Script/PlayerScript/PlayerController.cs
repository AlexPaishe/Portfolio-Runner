using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AnimationClip _ani;
    private Animator _animator;
    private CharacterController _controller;
    private BoxCollider _box;
    private Vector3 _moveVec;
    private Vector3 _gravity;

    [SerializeField] private float _distance = 3.0f;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _crawlingTime;
    [SerializeField] private float _height;

    public bool go;

    public bool BackDeath = false;
    public bool LeftDeath = false;
    public bool RightDeath = false;
    public bool RunParticle = false;
    public bool DeadVariation;

    private float _sideSpeed;
    private float _time;
    private float _currenDir = 0f;
    private float _heightControl;
    private float _heightBox;
    private float _boxcol;
    private float _heightCenter;

    private bool _isInMovement = false;
    private bool _craw = false;
    private bool _jump;
    private bool _killzone = false;
    private bool _roll = false;

    private int _position = 1;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
        _box = GetComponent<BoxCollider>();
        _moveVec = new Vector3(1, 0, 0);
        _gravity = Vector3.zero;
        _heightBox = _box.size.y;
        _boxcol = _box.center.y;
        _heightCenter = _controller.center.y;
        _heightControl = _controller.height;
        _jump = false;
        _time = _ani.length / 8f;
    }

    void Update()
    {
        Anima();
        OnParticleSys();
        if(transform.position.y < 0.45f)
        {
            WorldController.Instance.speed = 0;
            PhoneController.Instance.speed = 0f;
        }
    }

    private void FixedUpdate()
    {
        if (go == true)
        {
            Run();
            Gravity();
            if (_isInMovement)
            {
                Move();
            }
        }
        else
        {
            Gravity();
        }
    }

    #region Система передвижения

    /// <summary>
    /// Реализация передвижения
    /// </summary>
    private void Move()
    {
        if (_roll == true)
        {
            StartCoroutine(Roll(_currenDir, _time));
            _roll = false;
        }
    }

    /// <summary>
    /// Реализация передвижения в стороны
    /// </summary>
    /// <param name="currenDir"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator Roll(float currenDir, float time)
    {
        int cur = 0;
        float elapsed = 0;
        while(elapsed<=time)
        {
            elapsed += Time.fixedDeltaTime;
            if (currenDir > 0)
            {
                cur = -1;
            }
            else if (currenDir < 0)
            {
                cur = 1;
            }
            _sideSpeed = _distance / time;
            float Dist = _sideSpeed * Time.fixedDeltaTime;
            _controller.Move(Vector3.forward * cur * Dist);
            yield return null;
            if(go == false)
            {
                break;
            }
        }
            _isInMovement = false;
    }

    /// <summary>
    /// Реализация бега
    /// </summary>
    private void Run()
    {
        _moveVec.x = _speed;
        _moveVec += _gravity;
        _moveVec *= Time.fixedDeltaTime;
        _controller.Move(_moveVec);
    }

    /// <summary>
    /// РЕализация гравитации
    /// </summary>
    private void Gravity()
    {
        if(_controller.isGrounded)
        {
            _gravity = Vector3.zero;
            if (Input.GetAxisRaw("Vertical")>0)
            {
                if (_animator.GetBool("Crawling") == false)
                {
                    _gravity.y = _jumpForce;
                }
            }
        }
        else
        {
            _gravity += Physics.gravity * 3 * Time.fixedDeltaTime;
        }
    }
    #endregion

    #region Реакция на соприкосновение с опасными препятствиями

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Danger") && other.transform.position.x - other.transform.localScale.x / 2 < transform.position.x
    && other.transform.position.x + other.transform.localScale.x / 2 > transform.position.x
    && other.transform.position.y + other.transform.localScale.y / 2 > transform.position.y
    && other.transform.position.z + other.transform.localScale.z / 2 < transform.position.z)
        {
            if (DeadVariation == false)
            {
                _animator.SetTrigger("LeftWall");
            }
            else if (DeadVariation == true)
            {
                _animator.enabled = false;
            }
            go = false;
            LeftDeath = true;
            WorldController.Instance.speed = 0;
            PhoneController.Instance.speed = 0f;         
        }
        else if (other.CompareTag("Danger") && other.transform.position.x - other.transform.localScale.x / 2 < transform.position.x
            && other.transform.position.x + other.transform.localScale.x / 2 > transform.position.x
            && other.transform.position.y + other.transform.localScale.y / 2 > transform.position.y
            && other.transform.position.z - other.transform.localScale.z / 2 > transform.position.z)
             {
                if (DeadVariation == false)
                {
                   _animator.SetTrigger("RightWall");
                }
                else if (DeadVariation == true)
                {
                _animator.enabled = false;
                }
                 go = false;
                 RightDeath = true;
                 WorldController.Instance.speed = 0;
                 PhoneController.Instance.speed = 0f;              
             }
        else if (other.CompareTag("Danger") && other.transform.position.y + other.transform.localScale.y / 2 > transform.position.y 
            && other.transform.position.x + other.transform.localScale.x / 2 > transform.position.x)
             {
                if (_killzone == false)
                {
                   if (DeadVariation == false)
                   {
                      _animator.SetTrigger("Death");
                   }
                   else if (DeadVariation == true)
                   {
                      _animator.enabled = false;
                   }
                   go = false;
                   BackDeath = true;
                   WorldController.Instance.speed = 0;
                   PhoneController.Instance.speed = 0f;                  
                }
             }
        else if(other.CompareTag("Danger") && other.transform.position.y + other.transform.localScale.y / 2 < transform.position.y 
            && other.transform.position.x + other.transform.localScale.x / 2 < transform.position.x)
             {
                _killzone = true;
             }
        if(other.CompareTag("KillZone"))
        {
            if (DeadVariation == true)
            {
                _animator.enabled = false;
            } 
            go = false;
            WorldController.Instance.speed = 0;
            PhoneController.Instance.speed = 0f;
        }
        if (other.CompareTag("DangerRight"))
        {
            if (DeadVariation == false)
            {
                _animator.SetTrigger("LeftWall");
            }
            if (DeadVariation == true)
            {
                _animator.enabled = false;
            }
            go = false;
            LeftDeath = true;
            WorldController.Instance.speed = 0;
            PhoneController.Instance.speed = 0f;           
        }
        if (other.CompareTag("DangerLeft"))
        {
            if (DeadVariation == false)
            {
                _animator.SetTrigger("RightWall");
            }
            if (DeadVariation == true)
            {
                _animator.enabled = false;
            }
            go = false;
            RightDeath = true;
            WorldController.Instance.speed = 0;
            PhoneController.Instance.speed = 0f;            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Danger"))
        {
            if (_killzone == true)
            {
                _killzone = false;
            }
        }
    }
    #endregion

    #region Анимации персонажа

    /// <summary>
    /// Реализация анимации
    /// </summary>
    private void Anima()
    {
        AnimaRoll();
        AnimaCrawl();
        AnimaJump();
    }

    /// <summary>
    /// Анимация перекатов
    /// </summary>
    private void AnimaRoll()
    {
        float dir = Input.GetAxis("Horizontal");
        if (dir != 0)
        {
            if (_isInMovement == false && dir < 0 && _position != 0 ||
                _isInMovement == false && dir > 0 && _position != 2)
            {
                _isInMovement = true;
                _roll = true;
                _currenDir = dir;
                if(dir > 0)
                {
                    _position++;
                }
                else if (dir < 0)
                {
                    _position--;
                }
                if (_animator.GetBool("Crawling") == false)
                {
                    if (dir > 0)
                    {
                        _animator.SetTrigger("Right");
                    }
                    if (dir < 0)
                    {
                        _animator.SetTrigger("Left");
                    }
                }
            }
        }
    }

    /// <summary>
    /// Анимация ползка
    /// </summary>
    private void AnimaCrawl()
    {
        if(_craw == true)
        {
            StartCoroutine(Craw(_crawlingTime));
            _craw = false;
        }
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            if (_controller.isGrounded)
            {
                _animator.SetBool("Crawling", true);
                _craw = true;
            }
        }
    }

    /// <summary>
    /// Изменение размеров и расположения солайдеров при ползке
    /// </summary>
    /// <param name="CrawlingTime"></param>
    /// <returns></returns>
    IEnumerator Craw(float CrawlingTime)
    {
        float elapsed = 0;
        _controller.center = new Vector3(_controller.center.x, _heightCenter / 3, _controller.center.z);
        _controller.height = _heightControl / 3;
        _box.size = new Vector3(_box.size.x, _heightBox / 3, _box.size.z);
        _box.center = new Vector3(_box.center.x, _boxcol / 3 + 10, _boxcol / 3);
        while(elapsed<CrawlingTime)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        _animator.SetBool("Crawling", false);
        _controller.center = new Vector3(_controller.center.x, _heightCenter, _controller.center.z);
        _controller.height = _heightControl;
        _box.size = new Vector3(_box.size.x, _heightBox, _box.size.z);
        _box.center = new Vector3(_box.center.x, _boxcol, _box.center.x);
    }

    /// <summary>
    /// Анимация прыжка
    /// </summary>
    private void AnimaJump()
    {
        if (_controller.isGrounded == false)
        {
            StartCoroutine(Jump(_height));
        }
        float input = Input.GetAxis("Vertical");
        if (input != 0)
        {
            if (_jump == false)
            {
                _jump = true;
                if (_animator.GetBool("Crawling") == false)
                {
                    if (input > 0.1f)
                    {
                        _animator.SetTrigger("Jump");
                    }
                }
            }
        }
        if(_controller.isGrounded == true)
        {
            _jump = false;
            _animator.SetInteger("Height", 2);
        }
    }

    /// <summary>
    /// Реализация анимации свободного падения
    /// </summary>
    /// <param name="Height"></param>
    /// <returns></returns>
    IEnumerator Jump(float Height)
    {
        float elapsed = 0;
        while (elapsed<Height && _controller.isGrounded == false)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        if (_controller.isGrounded == false)
        {
            _animator.SetInteger("Height", 1);
        }
    }
    #endregion

    #region Вывод переменной для ParticleSystem

    /// <summary>
    /// Реализация появления партикла при беге
    /// </summary>
    private void OnParticleSys()
    {
        if(_isInMovement == false && _craw == false && _jump == false&& go == true)
        {
            RunParticle = true;
        }
        else
        {
            RunParticle = false;
        }
    }
    #endregion
}
