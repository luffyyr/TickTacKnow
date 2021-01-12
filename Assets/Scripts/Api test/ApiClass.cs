using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApiClass : MonoBehaviour
{

    
    public class Root
    {
        public bool status { get; set; }
        public object recordID { get; set; }
        public string message { get; set; }
        public object record { get; set; }
    }

}
