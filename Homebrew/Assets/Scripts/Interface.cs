using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interface : MonoBehaviour
{
    GameObject healthPip1;
    GameObject healthPip2;
    GameObject healthPip3;
    GameObject healthPip4;
    GameObject energyPip1;
    GameObject energyPip2;
    GameObject energyPip3;
    GameObject energyPip4;
    int hp = 4;
    int enrg = 4;
    // Use this for initialization
    void Start()
    {
        healthPip1 = GameObject.Find("Health Pip 1");
        healthPip2 = GameObject.Find("Health Pip 2");
        healthPip3 = GameObject.Find("Health Pip 3");
        healthPip4 = GameObject.Find("Health Pip 4");
        energyPip1 = GameObject.Find("Energy Pip 1");
        energyPip2 = GameObject.Find("Energy Pip 2");
        energyPip3 = GameObject.Find("Energy Pip 3");
        energyPip4 = GameObject.Find("Energy Pip 4");

    }

    // Update is called once per frame
    void Update()
    {
        if (hp == 3)
        {
            healthPip4.SetActive(false);
        }
        if (hp == 2)
        {
            healthPip4.SetActive(false);
            healthPip3.SetActive(false);
        }
        if (hp == 1)
        {
            healthPip4.SetActive(false);
            healthPip3.SetActive(false);
            healthPip2.SetActive(false);
        }
        if (hp == 0)
        {
            hp = 4;
            healthPip4.SetActive(true);
            healthPip3.SetActive(true);
            healthPip2.SetActive(true);
        }

        if (enrg == 3)
        {
            energyPip4.SetActive(false);
        }
        if (enrg == 2)
        {
            energyPip4.SetActive(false);
            energyPip3.SetActive(false);
        }
        if (enrg == 1)
        {
            energyPip4.SetActive(false);
            energyPip3.SetActive(false);
            energyPip2.SetActive(false);
        }
        if (enrg == 0)
        {
            enrg = 4;
            energyPip4.SetActive(true);
            energyPip3.SetActive(true);
            energyPip2.SetActive(true);
        }

    }

    public void takeDamage()
    {
        hp -= 1;
    }
    public void throwPotion()
    {
        enrg -= 1;
    }
}
