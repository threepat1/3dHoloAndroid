using prometheus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputTextManager : MonoBehaviour
{
    public GameObject panel;
    public MeshPlayerPRM meshPlugin;
    public InputField input;
    // Start is called before the first frame update
    void Start()
    {
        meshPlugin = FindObjectOfType<MeshPlayerPRM>();
        input = FindObjectOfType<InputField>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
    public void InsertText()
    {
        meshPlugin.sourceUrl = input.text;
        print(input.text);
    }
    public void ToggleButton()
    {
        panel.SetActive(!panel.activeSelf);
    }
}
