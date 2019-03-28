using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmCtrl : MonoBehaviour
{
    [SerializeField] GameCtrl m_theCenter;
    [SerializeField] bool m_isLeft;
    [Range(0, 2)] float m_upThresHold;
    [Range(0, 2)] float m_downThresHold;

    private float m_armLength;
    // Start is called before the first frame update
    void Start()
    {
        if (m_theCenter != null)
            m_theCenter.GameEvent.AddListener(OnStatusChanged);
    }

    // Update is called once per frame
    void Update()
    {
        m_armLength = Mathf.Sqrt(transform.localPosition.x * transform.localPosition.x + transform.localPosition.z * transform.localPosition.z);
    }
    void Calibrate()
    {
        m_isLeft = transform.localPosition.x < 0;
    }
    void OnStatusChanged(GameEvent.GameStatus incoming)
    {
        switch(incoming)
        {
            case GameEvent.GameStatus.CALIBRATING:
                Calibrate();
                break;
            default:
                break;
        }
    }
}
