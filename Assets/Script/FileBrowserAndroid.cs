using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FileBrowserAndroid : MonoBehaviour
{
    private string modelFile;
    public string scene;
 
    // Start is called before the first frame update
    void awake()
    {
        using (var ajc = new AndroidJavaClass("com.yasirkula.unity.NativeFilePickerPickFragment"))
            ajc.SetStatic("pickerMode", 2);
    }
    void Start()
    {
        modelFile = NativeFilePicker.ConvertExtensionToFileType("mp4");
        NativeFilePicker.RequestPermission();
    }

    // Update is called once per frame
 
    public void PickModelFile()
    {
        

        if (NativeFilePicker.IsFilePickerBusy())
            return;

        NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
        {
            if (path == null)
            {
                Debug.Log("Operation cancelled");
            }
                
            else
            {
                
                PlayerPrefs.SetString("model", path);
                SceneManager.LoadScene(scene);
            }
                Debug.Log("Picked file: " + path);
        }, new string[] { modelFile });

        
    }
}
