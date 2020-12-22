using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePanel : MonoBehaviour
{
    public GameObject panel1;

    // Start is called before the first frame update
    void Start()
    {
        panel1 = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        panel1.SetActive(false);
    }
}
