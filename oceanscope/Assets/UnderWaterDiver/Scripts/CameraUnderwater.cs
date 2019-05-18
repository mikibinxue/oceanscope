using UnityEngine;
using System.Collections;
using UnityStandardAssets;
//using UnityStandardAssets.ImageEffects;
using UnityStandardAssets.Characters.ThirdPerson;

public class CameraUnderwater : MonoBehaviour {

    public GameObject causticLight;
    public Color underwaterFogColor = new Color(0, 0.619f, 0.780f);

    private float waterLevel = -999;
    private MeshRenderer aboveWaterMesh;
    private MeshRenderer belowWaterMesh;

    private bool cameraUnderwater = false;
    private CameraManager camManager;
    //private DepthOfField depthOfField; //uncomment
    private Color startingFogColor;

    void Start()
    {
        camManager = FindObjectOfType<CameraManager>();
        //depthOfField = Camera.main.GetComponent<DepthOfField>(); //uncomment
        startingFogColor = RenderSettings.fogColor;
    }

    void Update()
    {
        if(transform.position.y < waterLevel)
        {
            if (cameraUnderwater == false)
            {
                ResetToBelowWater();
            }
        }
        else
        {
            if (cameraUnderwater == true)
            {
                ResetToAboveWater();
            }
        }
    }

    public void ResetCamera()
    {
        ResetToAboveWater();
        waterLevel = -999;
    }

    public void ResetToBelowWater()
    {
        if (aboveWaterMesh != null && belowWaterMesh != null)
        {
            cameraUnderwater = true;
            RenderSettings.fogStartDistance = 5;
            RenderSettings.fogEndDistance = 20;
            Camera.main.farClipPlane = 100;
            Camera.main.clearFlags = CameraClearFlags.SolidColor;
            RenderSettings.fogColor = underwaterFogColor;

            aboveWaterMesh.enabled = false;
            belowWaterMesh.enabled = true;

            //uncomment
            if (camManager != null)
            {
                //if (camManager.activeCamera.GetComponent<DepthOfField>() != null)
              //  {
               //     camManager.activeCamera.GetComponent<DepthOfField>().enabled = true;
              //  }
            }
          //  else if (depthOfField != null)
           // {
          //      depthOfField.enabled = true;
          //  }

            if (causticLight != null)
            {
                causticLight.SetActive(true);
            }
        }
    }

    public void ResetToAboveWater()
    {
        if (aboveWaterMesh != null && belowWaterMesh != null)
        {
            cameraUnderwater = false;
            RenderSettings.fogStartDistance = 200;
            RenderSettings.fogEndDistance = 300;
            Camera.main.farClipPlane = 400;
            Camera.main.clearFlags = CameraClearFlags.Skybox;
            RenderSettings.fogColor = startingFogColor;

            aboveWaterMesh.enabled = true;
            belowWaterMesh.enabled = false;

            //uncomment
            if (camManager != null)
            {
              //  if (camManager.activeCamera.GetComponent<DepthOfField>() != null)
              //  {
             //       camManager.activeCamera.GetComponent<DepthOfField>().enabled = false;
             //   }
            }
          //  else if (depthOfField != null)
          //  {
         //       depthOfField.enabled = false;
         //   }

            if (causticLight != null)
            {
                causticLight.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            waterLevel = other.transform.position.y;
            aboveWaterMesh = other.transform.GetComponent<MeshRenderer>();
            belowWaterMesh = other.transform.GetChild(0).GetComponent<MeshRenderer>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            waterLevel = -999;
        }
    }


}
