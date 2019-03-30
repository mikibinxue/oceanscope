using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class onTriggerLoad : MonoBehaviour
{
    public GameObject enterText;
    public string levelToLoad;

    void Start()
    {
        enterText.SetActive(false);
    }

    // Update is called once per frame
    void OnTriggerStay(Collider plyr)
    {
        if (plyr.gameObject.tag == "Player")
        {
            enterText.SetActive(true);
            if ((Input.GetKeyUp(KeyCode.Return)))
            {
                SceneManager.LoadScene(levelToLoad);
            }
        }
    }
    void OnTriggerExit(Collider plyr)
    {
        if (plyr.gameObject.tag == "Player")
        {
            enterText.SetActive(false);
        }
    }
}
