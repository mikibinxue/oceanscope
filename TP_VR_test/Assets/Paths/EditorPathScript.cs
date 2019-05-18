using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorPathScript : MonoBehaviour
{
    public Color rayColor = Color.white; //color of the path in editor
    public List<Transform> path_objs = new List<Transform>();
    Transform[] theArray; 

    void OnDrawGizmos ()
    {
        Gizmos.color = rayColor;
        theArray = GetComponentsInChildren<Transform>();
        path_objs.Clear(); //I clear the list because it has to be updated upon adding new points to the path 

        foreach (Transform path_obj in theArray)
        {
            if(path_obj!= this.transform)
            {
                path_objs.Add(path_obj); //I'm adding new objs to the path that I'm creating 
            }
        }

        for(int i = 0; i<path_objs.Count; i++)
        {
            Vector3 position = path_objs[i].position;
            if (i > 0) //meaning that I have something in theArray
            {
                Vector3 previous = path_objs[i - 1].position; //cause I need to start drawing the line from somewhere 
                Gizmos.DrawLine(previous, position);
                Gizmos.DrawWireSphere(position, 0.3f); //drawing a new spere on a current position with a radius of 0.3 
            }
        }
    }
}
