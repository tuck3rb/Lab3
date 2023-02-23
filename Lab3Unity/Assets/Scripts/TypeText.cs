using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeText : MonoBehaviour
{
    public string text1;
    public string text2;
    public string text3;
    public string text4;
    public string text5;

    void Awake()
    {
        GameManager.Instance.TypeBigShow(text1 + "\n" + "\n" + text2 + "\n" + "\n" + text3 + "\n" + "\n" + text4 + "\n" + "\n" + text5);
        print("yooooo");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
