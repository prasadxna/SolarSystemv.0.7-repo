using UnityEngine;
using System.Collections;

public class MovingCamera : MonoBehaviour
{




    private Vector3 set, playerPosition;
    public float smoothTime = 2.5F;
    public float smoothTimetop = 2.5F;
    private Vector3 velocity = Vector3.zero;

    private string plaName;
    private GameObject playBack;
    private Transform targetPlanet;
   private bool changeCam,topCam,moveBack;
    public livingRoomManager roomScript;
  

    void start()
    {
        moveBack = false;
        changeCam = false;
        topCam = false;
        playBack = null;
    }
   
    public void inspectMode(GameObject backFly, Vector3 currentPlayerPosition)
    {
        changeCam = false;
        playBack = backFly;
        playerPosition = new Vector3(0f, 0.8f, 0f);
        moveBack = true;
        
    }
   

    public void moveCamOnTop(GameObject planet)
    {

        //get the object name when (clicked/gazed) once
        plaName = planet.name;

        
        if(plaName == "item")
        {
          
            roomScript.menuState();
        }



        //if object tag is not solarsystem then return or do nothing
        if (planet.tag != "SolarSystemObjects")
            {
            
            return;
            }

        //if object tag is solarsystem and current game state is planethopping then hop the camera
            if ((planet.tag == "SolarSystemObjects" && roomScript.currentGameState == GameState.PLANETHOPPING))
        {

           
           // lets chage the cam transformation 
                changeCam = true;
            moveBack = false;
        //get the target object transform
        targetPlanet = planet.transform;

           



           }
          

        
    }




        
    void OnTriggerStay(Collider collidedPlanet)
    {

        if (collidedPlanet.name == plaName)
        {
          
            topCam = true;
         
           
        }

    }




    // Update is called once per frame
    void LateUpdate()
    {

       


        if (moveBack == true)
        {

            
            transform.position = Vector3.SmoothDamp(transform.position,playBack.gameObject.transform.position + playerPosition , ref velocity, smoothTime);
            changeCam = false;
            topCam = false;
        }

        
      //change cam is true and object tag is solarsystem move the cam
        if (changeCam && targetPlanet.tag == "SolarSystemObjects")
        {
          
            //if cam collides to the target object , move the cam to the top of the object with some offet
            if (topCam == true)
            {
                //With respect to the planet name, we ned to set the offset
                // set vector3 is used to offset
                if (plaName == "Mercury" || plaName == "Venus" || plaName == "Earth" || plaName == "Mars")
                {
                    set = new Vector3(0f, 0.2f, 0f);

                }
                else if (plaName == "The Sun" || plaName == "Jupiter" || plaName == "Saturn" || plaName == "Uranus" || plaName == "Neptune")
                {
                    
                    set = new Vector3(0f, 0.3f, 0f);
                }
                else if(plaName == "Pluto")
                {
                    set = new Vector3(0f, 0.2f, 0f);
                }
               
             
           }
            
           //smooth damp the camera to the target planet transform position and follow the planet 
            transform.position = Vector3.SmoothDamp(transform.position, targetPlanet.transform.position + set, ref velocity, smoothTimetop);
            moveBack = false;
        }

    }

}