using Amazon;
using Amazon.CognitoIdentity.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    public void btnClickSearchStocks()
    {
        Debug.Log("Testing Search Button");
    }
    // Use this for initialization
    void Start()
    {
        UnityInitializer.AttachToGameObject(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
