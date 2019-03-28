using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameCtrl : MonoBehaviour
{
    private bool m_Start = false;
    [Header("**** Speed Settings ****")]
    [SerializeField] float m_force;
    [SerializeField] float m_fAccelerateSpeed = 0.1f;
    [SerializeField] float m_fRotateAccelerateSpeed = -0.1f;
    float m_fSpeed = 0;
    float m_fRotateSpeed = 0.1f;
    [Header("**** Animation Settings ****")]
    [SerializeField] Animator m_theAnimator;
    [SerializeField] ParticleSystem m_speedVFX;
    ParticleSystem.EmissionModule m_emits;
    [Header("**** For Debug ****")]
    [SerializeField] TextMesh m_txtDebug;
    [SerializeField] bool m_isDebugging = false;

    //Hides.
    public GameEvent GameEvent { get; private set; } = new GameEvent();
    public WingsEvent WingsEvent { get; private set; } = new WingsEvent();
    WingsEvent.WingStatus[] m_theComboList = null;
    public FlyDirection FlyingDirection { get; private set; } = new FlyDirection();
    [HideInInspector]
    public int m_nLeftForward = 0;
    [HideInInspector]
    public int m_nRightForward = 0;

    IEnumerator Calibration()
    {

        m_txtDebug.text = "Read to Fly?";
        while (m_Start == false) {
            if (Input.GetKey(KeyCode.Space)) m_Start = true;
            yield return null;
        }
        yield return new WaitForSeconds(0.8f);
        for (int i = 3; i >= 0; i--)
        {
            m_txtDebug.text = "Over the Forbidden City...." + i;
            yield return new WaitForSeconds(0.8f);
            //m_txtDebug.text = "请保持视线水平";
            //yield return new WaitForSeconds(0.2f);
        }
        m_txtDebug.text = "Go!";
        GetComponent<AvatarFlying>().StartFlying();
        GameEvent.Invoke(GameEvent.GameStatus.CALIBRATING);
        yield return new WaitForSeconds(0.8f);
        
        m_txtDebug.text = "";
        FlyingDirection.AddListener(OnDirectionChanged);
        WingsEvent.AddListener(OnWingsChanged);
        GameEvent.Invoke(GameEvent.GameStatus.PLAY);
    }
    IEnumerator AddForce()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * m_force);
        yield return new WaitForSeconds(1f);
    }

    // Start is called before the first frame update
    void Awake()
    {
        SceneManager.sceneLoaded += OnEnvLoaded;
    }
    private void Start()
    {
        if (m_speedVFX != null)
            m_emits = m_speedVFX.emission;
    }
    void OnEnvLoaded(Scene theScene, LoadSceneMode theMode)
    {
        if(theScene.name == "Env" && theMode == LoadSceneMode.Additive && isActiveAndEnabled)
        {
            GameEvent.AddListener(OnStatusChanged);
            StartCoroutine(Calibration());
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (GameEvent.Current == GameEvent.GameStatus.PLAY)
        {
            if (m_nLeftForward == -1 && m_nRightForward == -1)
            {
                m_fSpeed += m_fAccelerateSpeed;
                m_fSpeed = m_fSpeed < 0.5f ? 0.5f : m_fSpeed;
            }
            else if (m_nLeftForward == 1 && m_nRightForward == 1)
            {
                //m_fSpeed -= 2 * m_fAccelerateSpeed;
            }
            if (m_nLeftForward == -1 && m_nRightForward == 1)
            {
                m_fRotateSpeed -= m_fRotateAccelerateSpeed;
                m_fRotateSpeed = m_fRotateSpeed < -0.5f ? -0.5f : m_fRotateSpeed;
                //m_fSpeed = m_fSpeed < 0.5f ? 0.5f : m_fSpeed;
            }
            else if (m_nLeftForward == 1 && m_nRightForward == -1)
            {
                m_fRotateSpeed += m_fRotateAccelerateSpeed;
                m_fRotateSpeed = m_fRotateSpeed > 0.5f ? 0.5f : m_fRotateSpeed;
            }
            else
            {
                //m_fSpeed  /= 1.01f ;
                //m_fSpeed = m_fSpeed > 0.1f ? m_fSpeed : 0;
            }
            //Yan: Release to 0 
            m_fRotateSpeed /= 1.01f;
            if (m_fRotateSpeed < 0.01f && m_fRotateSpeed > -0.01f)
                m_fRotateSpeed = 0;
            m_fSpeed /= 1.01f;

            if (transform.position.y < 0)
                m_fSpeed = m_fSpeed < 4 ? m_fSpeed : 4;
            m_fSpeed = m_fSpeed > 0.1f ? m_fSpeed : 0;
            //m_txtDebug.text = string.Format("Rotate:{0:F2}\nSpeed:{1:F2}",m_fRotateSpeed, m_fSpeed);
            //Yan: Sync Animation Parameter.
            m_theAnimator.SetFloat("fY", transform.position.y);
            m_theAnimator.SetFloat("fSpeed", m_fSpeed);
            m_theAnimator.SetFloat("fRotateSpeed", m_fRotateSpeed);
            m_emits.rateOverTime = m_fSpeed > 4 ? m_fSpeed : 0;

            if(m_isDebugging)
                m_txtDebug.text = string.Format("Height:{0:F2}\nSpeed:{1:F2}", m_theAnimator.GetFloat("fY"), m_theAnimator.GetFloat("fSpeed"));
            //Yan: Do it!
            // transform.Rotate(Vector3.up * m_fRotateSpeed);
            float fYFactor = 1;// transform.position.y > 0 ? transform.position.y : 0;
            // transform.Translate(Vector3.forward * m_fSpeed * fYFactor * Time.deltaTime);

            /*
            switch (FlyingDirection.Current)
            {
                case FlyDirection.Direction.FORWARD:
                    //m_fSpeed++;
                    break;
                case FlyDirection.Direction.LEFT:
                    //transform.Rotate(Vector3.down);
                    break;
                case FlyDirection.Direction.RIGHT:
                    //transform.Rotate(Vector3.up);
                    break;
                case FlyDirection.Direction.IDLE:
                    //m_fSpeed = 0;
                    break;
            }*/
            //Fly!
            //transform.Translate(transform.rotation * Vector3.forward * m_fSpeed * transform.position.y * Time.deltaTime);
        }
    }
    void OnStatusChanged(GameEvent.GameStatus gameStatus)
    {
        switch(gameStatus)
        {
            case GameEvent.GameStatus.PLAY:
                // GetComponent<Rigidbody>().useGravity = true;
                break;

        }

    }
    void OnWingsChanged(bool isLeft, WingsEvent.WingStatus wingStatus)
    {
        //Force
        m_theComboList = isLeft ? WingsEvent.LeftQueue.ToArray() : WingsEvent.RightQueue.ToArray();
        if(m_theComboList.Length >= 3)
        {
            if(m_theComboList[0] == WingsEvent.WingStatus.UP && m_theComboList[1] == WingsEvent.WingStatus.IDLE && m_theComboList[2] == WingsEvent.WingStatus.DOWN)
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * m_force);
                m_emits.SetBurst(0, new ParticleSystem.Burst(0.25f, 20));

                //Dequeue!
                if (isLeft)
                    WingsEvent.LeftQueue.Clear();
                else
                    WingsEvent.RightQueue.Clear();
            }
        }
        else
        {
            // m_emits.SetBurst(0, new ParticleSystem.Burst(0, 0));

        }

    }
    void OnDirectionChanged(bool isLeft, FlyDirection.Direction newDirection)
    {
        if(isLeft)
        {

        }
        else
        {

        }
        //if(newDirection == FlyDirection.Direction.FORWARD)
        //    GetComponent<Rigidbody>().AddForce(transform.rotation * Vector3.forward * m_force);
    }
}
