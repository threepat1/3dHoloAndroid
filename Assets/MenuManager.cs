using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject player;
    public MeshRenderer meshRend;
    public GameObject panel1;
    public GameObject colorPicker;
    public RotateModel rotate;
    [Header("Menu")]
    public GameObject environmentMenu;
    public GameObject materialMenu;
    [Header("Environments")]
    public GameObject football;
    public GameObject weapon;
    public GameObject exhibition;
    public GameObject space2;
    public GameObject concert;
    public GameObject cube;
    [Header("Materials")]
    public Material normal;
    public Material holo;
    public Material iron;
    public Material rim;
    public Material def;
    [Header("Effect")]
    public GameObject heartFloat;
    public GameObject starLight;
    public GameObject groundAura;
    public GameObject dustRain;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<GameObject>();
        meshRend = player.GetComponent<MeshRenderer>();
        rotate = GetComponent<RotateModel>();

    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_STANDALONE_WIN
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            panel1.SetActive(true);
        }
        if (!colorPicker.activeSelf)
        {
            rotate.enabled = true;
        }
        else
        {
            rotate.enabled = false;
        }
#endif
    }

    public void BackToLoadScene()
    {
        SceneManager.LoadScene("main");
    }
    public void OpenEnvironMenu()
    {
        environmentMenu.SetActive(true);
    }

    public void OpenMatMenu()
    {
        materialMenu.SetActive(true);
    }
    public void NormalMat()
    {
        meshRend.material = normal;
        materialMenu.SetActive(false);
    }
    public void DefaultMat()
    {
        meshRend.material = def;
        materialMenu.SetActive(false);
    }
    public void HoloMat()
    {
        meshRend.material = holo;
        materialMenu.SetActive(false);
    }
    public void IronMat()
    {
        meshRend.material = iron;
        materialMenu.SetActive(false);
    }
    public void RimMat()
    {
        meshRend.material = rim;
        materialMenu.SetActive(false);
    }

    public void FootballScene()
    {
        football.SetActive(true);
        weapon.SetActive(false);
        exhibition.SetActive(false);
        space2.SetActive(false);
        environmentMenu.SetActive(false);
        concert.SetActive(false);
        cube.SetActive(false);
    }
    public void WeaponScene()
    {

        football.SetActive(false);
        weapon.SetActive(true);
        exhibition.SetActive(false);
        space2.SetActive(false);
        environmentMenu.SetActive(false);
        concert.SetActive(false);
        cube.SetActive(false);
    }
    public void ExhibScene()
    {
        football.SetActive(false);
        weapon.SetActive(false);
        exhibition.SetActive(true);
        space2.SetActive(false);
        environmentMenu.SetActive(false);
        concert.SetActive(false);
        cube.SetActive(false);
    }


    public void SpaceScene2()
    {
        football.SetActive(false);
        weapon.SetActive(false);
        exhibition.SetActive(false);
        space2.SetActive(true);
        environmentMenu.SetActive(false);
        concert.SetActive(false);
        cube.SetActive(false);
    }
    public void ConcertScene()
    {
        football.SetActive(false);
        weapon.SetActive(false);
        exhibition.SetActive(false);
        space2.SetActive(false);
        environmentMenu.SetActive(false);
        concert.SetActive(true);
        cube.SetActive(false);
    }
    public void HeartEffect()
    {
        heartFloat.SetActive(!heartFloat.activeSelf);
    }
    public void StarLightEffect()
    {
        starLight.SetActive(!starLight.activeSelf);
    }
    public void GroundMagicEffect()
    {
        groundAura.SetActive(!groundAura.activeSelf);
    }
    public void DustRainEffect()
    {
        dustRain.SetActive(!dustRain.activeSelf);
    }

    public void TogglePanel()
    {
        panel1.SetActive(false);
    }
}


