using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlaneMatManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Material[] planeMat;
    Material CurrMat;
    Renderer renderer;
    void Start()
    {
        // foreach(var b in planeColButton)
        // {
        //     b.onClick.AddListener(OnClickButton);
        // }
        renderer=this.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void YellowColor()
    {
        renderer.material= planeMat[0];
        CurrMat = renderer.material;
    }
    public void BlueColor()
    {
        renderer.material= planeMat[1];
        CurrMat = renderer.material;
        Debug.Log("blue");
    }
    public void RedColor()
    {
        renderer.material= planeMat[2];
        CurrMat = renderer.material;
    }
    
    public void PurpleColor()
    {
        renderer.material= planeMat[3];
        CurrMat = renderer.material;
    }
}
