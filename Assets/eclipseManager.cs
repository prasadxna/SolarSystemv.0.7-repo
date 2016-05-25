using UnityEngine;
using System.Collections;

public class eclipseManager : MonoBehaviour {
    private string objName;
    private bool  MenuActive;
    public GameObject menuItems, Earth;
    // Use this for initialization
    void Start () {
	
	}
	

    public void getObject(GameObject objects)
    {

      objName = objects.name;


        if (objName == "item")
        {
            MenuActive = !MenuActive;



            if (MenuActive == true)
            {
                //show the menu
                menuItems.gameObject.SetActive(true);

            }
            if (MenuActive == false)
            {
                //hide the menu
                menuItems.gameObject.SetActive(false);

            }

        }


    }

    // Update is called once per frame
    void Update () {
	
	}
}
