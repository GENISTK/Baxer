using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Name:        Connect
 * Author:      Keagan Lidwell
 * 
 * Description: Script to interact with AWS database through PHP files. At the moment functionality is taking priority over security. This script is attached the
 *              main camera so that it loads when the game starts.
 *              
 * Notes:       This will be refactored/replaced by Google/Apple authentication in the future.
 */

public class connect : MonoBehaviour {

    public string baseURL = "http://localhost/AWS/Baxter/Connect.php?"; //be sure to add a ? to your url
    public Boolean loggedIn = false;
    public User currentUser = null;
    public string returnData = "";
    public IList<User> users = null;

    // Use this for initialization
    void Start () {
        
        // Unity absorbs the return values so you must store them manually.
        StartCoroutine(Login());
    }

    IEnumerator Login()
    {
        // Hard coded for now.
        string loginURL = baseURL + "username=\"FiveTies\"";
        WWW hs_get = new WWW(loginURL);

        // Response was returned.
        yield return hs_get;

        // locally store the return string
        returnData = hs_get.text;
        
        // Deserialize JSON data and create user from it. We will use JsonConvert to accomplish this. 
        // Doing it this way will allow us to take advantage of object orientation. Use list because the PHP JSON returns as array everytime.
        try
        {
            users = JsonConvert.DeserializeObject<List<User>>(returnData);
            Debug.Log(users.ToString());
            loggedIn = true;
            currentUser = users[0];
            Debug.Log(String.Format("Userid: {0}, Username: {1}, User Money: ${2:C2}, Premium Currency: {3}",  currentUser.user_id, currentUser.uname, currentUser.user_mon, currentUser.prem_curr));
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message.ToString());
        }

    }

    // Get the scores from the MySQL DB to display in a GUIText.
    // remember to use StartCoroutine when calling this function!
    IEnumerator TestConnect()
    {
        WWW hs_get = new WWW(baseURL);
        yield return hs_get;

        if (hs_get.error != null)
        {
            Debug.Log("There was an error connecting: " + hs_get.error);
        }
    }

    // Update is called once per frame. At the moment there is no reason to use this. Will probably start using it once we start updating stocks every 20 seconds. 
    void Update () {
		
	}
}
