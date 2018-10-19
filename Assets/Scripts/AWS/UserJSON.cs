using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Name:         UserJSON
 * Author:       Keagan Lidwell
 * Description:  Script to assist with JSON deserialization of a array.
 */

public class UserJSON {

    [JsonProperty("user")]
    public User User { get; set; }

}
