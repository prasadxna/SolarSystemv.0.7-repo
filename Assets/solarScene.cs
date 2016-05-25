using UnityEngine;
using System.Collections;

public class solarScene : MonoBehaviour {

   
    public Transform[] planetT;
   

   
	// Use this for initialization
	void Start () {


	}
	

    public void gameScene()
    {

       
        for (int a = 1; a < planetT.Length - 1; a++)
        {
            if (a == 3)
                continue;
            planetT[a].gameObject.SetActive(false);
        }

         planetT[3].GetComponent<Planet_Y_Rot>().enabled = true;
        planetT[3].GetComponent<LineRenderer>().enabled = true;
        planetT[3].GetChild(4).gameObject.GetComponent<LineRenderer>().enabled = true;
        planetT[3].GetComponent<SgtSimpleOrbit>().DegreesPerSecond = 0;
        planetT[3].GetComponent<SgtSimpleOrbit>().Angle = 158;
        planetT[3].GetComponent<SgtSimpleOrbit>().Radius = 8;
        planetT[3].GetChild(4).gameObject.AddComponent<Planet_Y_Rot>();
        planetT[3].GetChild(4).gameObject.AddComponent<SgtSimpleOrbit>().DegreesPerSecond = 0.5f;
        planetT[3].GetChild(4).gameObject.GetComponent<SgtSimpleOrbit>().Radius =4;
        planetT[3].GetChild(4).gameObject.GetComponent<SgtSimpleOrbit>().Oblateness = 0.4f;
       
    }

	// Update is called once per frame
	void Update () {

    }
}
