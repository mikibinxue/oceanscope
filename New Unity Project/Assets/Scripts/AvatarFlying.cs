using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarFlying : MonoBehaviour
{
    private bool m_Start = false;
    [SerializeField] Transform m_trLeft;
    [SerializeField] Transform m_trRight;
    [SerializeField] TextMesh m_txtDebug;
    [SerializeField] float m_fSpeed = 5;
    bool m_hasInited = false;
    [SerializeField] bool m_moveByHMD = true;
    [SerializeField] bool m_isDebugging = false;
    // Start is called before the first frame update
    private GameObject m_StartPoint;

    void Start()
    {
        StartCoroutine(Calibration());
        m_StartPoint = GameObject.FindObjectOfType<StartMarker>().gameObject;
        transform.position = m_StartPoint.transform.position;
        transform.rotation = m_StartPoint.transform.rotation;
    }
    IEnumerator Calibration()
    {
        m_txtDebug.text = "Ready to Fly?";
        while (!m_Start)
        {
            if (Input.GetKey(KeyCode.Space)) m_Start = true;
            yield return null;
        }

        yield return new WaitForSeconds(0.8f);
        for (int i = 3; i >= 0; i--)
        {
            m_txtDebug.text = "Over the Forbidden City..." + i;
            yield return new WaitForSeconds(0.8f);
            m_txtDebug.text = "";
            yield return new WaitForSeconds(0.2f);
        }
        m_txtDebug.text = "Go!";
        yield return new WaitForSeconds(0.8f);
        m_txtDebug.text = "";
        //Do Calibration,
        //if (m_trLeft.localPosition.x > 0 && m_trRight.localPosition.x < 0)
        //{
        //    //Swap
        //    GameObject tmp = m_trRight.gameObject;
        //    m_trRight = m_trLeft.gameObject.transform;
        //    m_trLeft = tmp.transform;
        //}
        m_hasInited = true;
    }
    // Update is called once per frame
    public void StartFlying() { m_hasInited = true; }

    void Update()
    {
        if (m_hasInited)
        {
            //Yan: Move By Head.
            if(m_moveByHMD)
                transform.Translate((Camera.main.transform.rotation * Vector3.forward) * m_fSpeed * Time.deltaTime, Space.World);
            else
                transform.Translate(m_trRight.rotation * Vector3.forward * m_fSpeed * Time.deltaTime);
            m_txtDebug.text = m_isDebugging? string.Format("Left X:{0:F2} Y:{1:F2}\nRight X: {2:F2} Y: {3:F2}", m_trLeft.localPosition.x, m_trLeft.localPosition.y, m_trRight.localPosition.x,m_trRight.localPosition.y) : "";
        }
    }
}
