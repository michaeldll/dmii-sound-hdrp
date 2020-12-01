using UnityEngine;
using UnityEngine.Events;

public class Example : MonoBehaviour
{
    public FloatEvent floatEvent;

    public void Log(float f){
        Debug.Log(f);
    }

    public void Log2(float f){
        Debug.Log(f);
    }

    void Start(){
        floatEvent.e.AddListener(Log); // add callback (with the same argument types as referenced event)
        floatEvent.e.AddListener(Log2); // add callback (with the same argument types as referenced event)
    }

    void Update(){
        if(Input.GetMouseButtonDown(0)) floatEvent.e.Invoke(10f); // call event
    }
}