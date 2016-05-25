using UnityEngine;
using System.Collections;



public class SpaceShipManager : MonoBehaviour {

    public GameObject camCenterItem;
   
    public GameObject captureCentreScreen;
    private float campos;
  
    public float smoothTime = 0.6F;
    private bool isOutFlag;
    private Vector3 velocity = Vector3.zero;
    private float startTime;
    private float journeyLength;


    [SerializeField]
    GameObject m_camera;
    // Use this for initialization
    void Start () {
        campos = 0f;
    }


    IEnumerator moveItemToCenter()
    {
        float distCovered, fracJourney;
        while (Vector3.Distance(camCenterItem.transform.position, captureCentreScreen.transform.position) > 0f)
        {
          
            distCovered = (Time.time - startTime) * smoothTime;
            fracJourney = distCovered / journeyLength;
            camCenterItem.transform.position = Vector3.Lerp(camCenterItem.transform.position, captureCentreScreen.transform.position, fracJourney);
            // camCenterItem.transform.position = Vector3.SmoothDamp(camCenterItem.transform.position, captureCentreScreen.transform.position, ref velocity, smoothTime);
            // camCenterItem.transform.position = captureCentreScreen.transform.position;
            yield return new WaitForFixedUpdate();
        }
        isOutFlag = false;
    }

    // Update is called once per frame
    void Update () {


        Vector3 viewPos = m_camera.gameObject.GetComponent<Camera>().WorldToViewportPoint(camCenterItem.transform.position);

        if (!(viewPos.x > 0.2F && viewPos.x < 0.6F) && isOutFlag == false)
        {
            isOutFlag = true;
            startTime = Time.time;
            journeyLength = Vector3.Distance(camCenterItem.transform.position, captureCentreScreen.transform.position);
            StartCoroutine(moveItemToCenter());
        }

    }
}
