using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; // Required to load a different scene  
using UnityEngine.UI;


//Enumeration to move a planet closer to the player   --> bringPlanetsCloser() is making use of this enumeration
public enum ViewState
{	
	Orbiting,
	rotating,
	arrange
};



public enum GameState
{
    INSPECT,
    PLANETHOPPING,
    
};


public class livingRoomManager : MonoBehaviour
{
    //Game objects used 
    public GameObject menuItems, Player, MovingCamObject, menuShowPos,sunSolid;

    public Light LightningPlanet;

  
    private Vector3 currentPlayerPosition, sunInitailPos,offsetMenu,sunSoidInitialScale;
    public GameState currentGameState;
    private float startTime, journeyLength, campos;
    
    private bool setState, MenuActive, SunOut, isOutFlag;

    [SerializeField]
    GameObject placeHolder; // objects out of its orbit comes to this position.

    [SerializeField]
    string currentPlanet; // current planet that is out of its orbit.

    [SerializeField]
    Vector3 initialScale; // Holds inital scale of the object which is pulled out of its orbit. 


    GameObject planetOutOfOrbit; // will hold the refrernce of the game object which will be passed out as an argument 
                                 //	during the first click of the mouse and using this game object the second click --> rotates the object
                                 //	retains the game object collected during the first click and sends it back to orbit (3rd click)					

    [SerializeField]
    Camera camera;


    //user defined datatypes
    [SerializeField]
    ViewState currentState;


    [SerializeField]
    GameObject exploreCanvas;

    [SerializeField]
    Text planetDataDisplay;

    [SerializeField]
    GameObject m_camera;

    [SerializeField]
    Text terrain_name_display;

    [SerializeField]
    GameObject planet_placeHolder;


    
   
  
    

    void Start()
    {
        //default game state is Inspect
        currentGameState = GameState.INSPECT;
        setState = false;
        currentState = ViewState.Orbiting;
        exploreCanvas.SetActive(false);
        //default the menu and menu items are is hidden 
        MenuActive = false;
        menuItems.gameObject.transform.FindChild("InspectUI").FindChild("Inspect").GetComponent<Button>().interactable = false;
        SunOut = false;
       

    }

    //Used to toggle the Menu. 
    public void  menuState()
    {

       //if false then true (vice-versa)
        MenuActive =!MenuActive;


        
        if (MenuActive == true)
        {
            //show the menu
           // offsetMenu = new Vector3(0f, 0.5f, 0f);
            //menuItems.transform.position = menuShowPos.transform.position - offsetMenu;
            //menuItems.transform.LookAt(MovingCamObject.transform);
            menuItems.gameObject.SetActive(true);
           
        }
        if (MenuActive == false)
        {
            //hide the menu
            menuItems.gameObject.SetActive(false);

        }

    }





    public void setGameState(int changeStatestr)
    {
           //Assign the Game State(INSPECT or HOPPING)
        GameState changeState = (GameState)changeStatestr;

      

        //if game state is planethop then 
        // 1. hide the menu
        // 2. Disable the PlanetHop and enable Inspect control menu
        // 3. call the PlanetHopMode function 
        // 4. find the button, if active then make it inactive 
        if (changeState == GameState.PLANETHOPPING)
        {
          
            planetHopMode();
            menuItems.gameObject.transform.FindChild("PlanetHopUI").FindChild("PlanetHop").GetComponent<Button>().interactable = false;
            menuItems.gameObject.transform.FindChild("InspectUI").FindChild("Inspect").GetComponent<Button>().interactable =true;
           

        }
        
        if (changeState == GameState.INSPECT)
        {
            
            planetInspectMode();
            menuItems.gameObject.transform.FindChild("InspectUI").FindChild("Inspect").GetComponent<Button>().interactable = false;
            menuItems.gameObject.transform.FindChild("PlanetHopUI").FindChild("PlanetHop").GetComponent<Button>().interactable = true;
    
        }

       

        // Hiding the Menu
        menuItems.gameObject.SetActive(false);

        //setting the current game state
        currentGameState = changeState;
        MenuActive = false;
    }


    private void planetInspectMode()
    {

        currentPlayerPosition = Player.transform.position;
        
      
        MovingCamObject.gameObject.GetComponent<BoxCollider>().enabled = false;

        MovingCamObject.gameObject.GetComponent<MovingCamera>().inspectMode(Player, currentPlayerPosition);
        Player.gameObject.GetComponent<MeshRenderer>().enabled = true;
        Player.gameObject.GetComponent<RigidbodyAutowalk>().enabled = true;
        Player.gameObject.GetComponent<CapsuleCollider>().enabled = true;
        Player.gameObject.GetComponent<Rigidbody>().isKinematic = false;
   

    }

    private void planetHopMode()
    {

        
        
        MovingCamObject.gameObject.GetComponent<BoxCollider>().enabled = true;

      
        Player.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        Player.gameObject.GetComponent<MeshRenderer>().enabled = false;
        Player.gameObject.GetComponent<RigidbodyAutowalk>().enabled = false;
      
        Player.gameObject.GetComponent<Rigidbody>().isKinematic = true;
  

    }


   

	void Update () 
	{
        //m_camera is the main camera
       
        
    
	}

	//Method Invoked by CardBoardReticle script while Trigger is Down
	public void bringPlanetsCloser(GameObject GO)
	{
        //if current game state is Inspect then only run this
		if (currentState == ViewState.Orbiting && GO.tag == "SolarSystemObjects" && currentGameState == GameState.INSPECT) 
		{

            //InspectCanvas.SetActive(true);
            currentPlanet = GO.name;


            menuItems.gameObject.SetActive(false);

            LightningPlanet.gameObject.SetActive(true);
                terrain_name_display.text = planetDataDisplay.text = "";
				exploreCanvas.SetActive (true);
				//exploreCanvas.transform.position = new Vector3 (placeHolder.transform.position.x, placeHolder.transform.position.y + 0.3f, placeHolder.transform.position.z);
				//exploreCanvas.transform.position = new Vector3 (GO.transform.position.x, GO.transform.position.y + 0.3f, GO.transform.position.z);
				switch (currentPlanet) 
				{

                case "The Sun":
                   
                    planetDataDisplay.text = "The Sun is the star at the center of the Solar System and is by far the most important source of energy for life on Earth";
                    break;

                case "Mercury":
                        planetDataDisplay.text = "Mercury is the smallest planet in the Solar System and the one closest to the Sun";
                        break;

                    case "Venus":
                        planetDataDisplay.text = "Venus is the second planet from the Sun, orbiting it every 224.7 Earth days";
                        break;

                    case "Earth":
						terrain_name_display.text = "Explore Moon";
                        planetDataDisplay.text = "Earth is the third planet from the Sun, the densest planet in the Solar System";
						break;

					case "Mars":
						terrain_name_display.text = "Explore Mars";
                        planetDataDisplay.text = "Mars is the fourth planet from the Sun and the second-smallest planet in the Solar System, after Mercury";
                        break;

					case "Jupiter":
						terrain_name_display.text = "Explore Europa";
                        planetDataDisplay.text = "Jupiter is the fifth planet from the Sun and the largest in the Solar System";
                        break;

                    case "Saturn":
                            planetDataDisplay.text = "Saturn is the sixth planet from the Sun and the second-largest in the Solar System, after Jupiter";
                    break;

                    case "Uranus":
                        planetDataDisplay.text = "Uranus is the seventh planet from the Sun. It has the third-largest planetary radius";
                        break;

                    case "Neptune":
                        planetDataDisplay.text = "Neptune is the eighth and farthest known planet from the Sun in the Solar System";
                        break;

                    case "Pluto":
                        planetDataDisplay.text = "Pluto was known as the smallest planet in the solar system and the ninth planet from the sun";
                        break;
                }

        if(currentPlanet!="The Sun")
            {

               
                GO.transform.GetComponent<SgtSimpleOrbit>().enabled = false;

            }
        
              
            


            sunInitailPos = GO.transform.position;

            initialScale = GO.transform.localScale;
            if (currentPlanet == "The Sun") { 
                sunSoidInitialScale = sunSolid.transform.localScale;
               sunSolid.transform.localScale = new Vector3(1, 1, 1);
                GO.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                GO.transform.localScale = new Vector3(1, 1, 1);
            }
           
            GO.transform.position = placeHolder.transform.position;
			planetOutOfOrbit = GO;
			currentState = ViewState.arrange;
            if (currentPlanet != "The Sun")
                planetOutOfOrbit.transform.GetComponent<Planet_Y_Rot>().enabled = true;

           
        }
			
		else if ((currentState == ViewState.arrange && planetOutOfOrbit != null) ) 
		{
            //currentPlanet = null;
            LightningPlanet.gameObject.SetActive(false);
            planetOutOfOrbit.transform.localScale = initialScale;
            if (currentPlanet != "The Sun") {
             
                planetOutOfOrbit.transform.GetComponent<Planet_Y_Rot>().enabled = false;
                planetOutOfOrbit.transform.GetComponent<SgtSimpleOrbit>().enabled = true;

            }
            else
            {
                sunSolid.transform.localScale = sunSoidInitialScale;
                planetOutOfOrbit.transform.position = sunInitailPos;
            }
            currentState = ViewState.Orbiting;
           // InspectCanvas.SetActive(false);
            exploreCanvas.SetActive (false);
       
            
        }
		
	}


	//Mehod Invoked by Canvas button elements 
	public void exploreTerrain(GameObject m_terrain)
	{

		string terrain_name = currentPlanet;
		switch(terrain_name)
		{

		case "Mars":
			SceneManager.LoadScene ("Mars");
			break;

		case "Earth":
			SceneManager.LoadScene ("Moon");
			break;

		case "Jupiter":
			SceneManager.LoadScene ("Europa");
			break;

		}
	}
		
}
