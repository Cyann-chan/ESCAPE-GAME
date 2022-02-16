using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;


public class HandRayCastManager : MonoBehaviour
{
    bool[] m_drawerStatus = new bool[4];

    List<string> m_ingredients = new List<string>();
    List<string> m_goodIngredients = new List<string>();
    

    int m_temperatureLevel = 0;
    bool m_keyIsInPlace = false;
    [SerializeField] Animator m_CauldronAnimator;
    [SerializeField] Transform m_PotionSpawnPlace;
    [SerializeField] GameObject m_PotionWrong;
    [SerializeField] GameObject m_PotionCorrect;
    [SerializeField] float timeLeft = 6*60.0f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < m_drawerStatus.Length; i++)
        {
            m_drawerStatus[i] = false;
        }
        m_goodIngredients.Add("MeatSlice");
        m_goodIngredients.Add("MeatSlice");
        m_goodIngredients.Add("Feather");
        m_goodIngredients.Add("Pumpking");
    }


    void Update()
    {
        timeLeft -= Time.deltaTime;
        Debug.Log(timeLeft);
        if (timeLeft < 0)
        {
            GameOver();
        }
    }


    

    public void Clic(GameObject gameObject)
    {
        if(gameObject.name.Contains("Closet"))
        {
            int i = int.Parse(gameObject.name.Substring(gameObject.name.Length - 1)) - 1;
            if(i!=2 || m_keyIsInPlace)
            {
                int direction = 1; 
                if (m_drawerStatus[i])
                {
                    direction = -1;
                }
                m_drawerStatus[i] = !m_drawerStatus[i];
                StartCoroutine(OpenCloset(gameObject, 2, gameObject.transform.position, gameObject.transform.position + direction*new Vector3(-0.4f,0,0), 1));
            }
        }
        if(gameObject.name == "CauldronTarget")
        {
            m_CauldronAnimator.SetTrigger("Open");
        }
        if(gameObject.name == "BrewButton")
        {
            m_CauldronAnimator.SetTrigger("Brew");
            
            if(CorrectIngredients() && m_temperatureLevel == 6)
            {
                Debug.Log("oki");
                SpawnPotion(m_PotionCorrect);
            } else
            {
                Debug.Log("pas bon");
                SpawnPotion(m_PotionWrong);
            }

            m_ingredients.Clear();
        }
        if(gameObject.name == "TemperatureLever")
        {
            m_temperatureLevel++;
            if(m_temperatureLevel == 8)
            {
                m_temperatureLevel = 0; 
            }
            gameObject.transform.RotateAround(gameObject.transform.position, gameObject.transform.up, 360f/8f);

        }
    }

    IEnumerator OpenCloset(GameObject gameObject, float duration, Vector3 source, Vector3 target, float overTime)
    {
        float startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            gameObject.transform.position = Vector3.Lerp(source, target, (Time.time - startTime) / overTime);
            yield return null;
        }
        gameObject.transform.position = target;
    }

    public void UpdateKeyHole(SelectEnterEventArgs args)
    {
        if(args.interactableObject.transform.name == "KeyForDesk")
        {
            m_keyIsInPlace = true;
        }
    }

    public void NewIngredient(string ingredientName)
    {
        m_ingredients.Add(ingredientName);
    }

    bool CorrectIngredients()
    {
  
        m_ingredients.Sort();
        m_goodIngredients.Sort();


        for (int i = 0; i < m_goodIngredients.Count; i++)
        {
            if (m_goodIngredients[i] != m_ingredients[i])
                return false;
        }
        return true;
    }

    void SpawnPotion(GameObject objectToSpawn)
    {
        GameObject spawnInstance = Instantiate(objectToSpawn, m_PotionSpawnPlace.position, Quaternion.identity);
    }

    public void DoorPotion(SelectEnterEventArgs args)
    {
        if (args.interactableObject.transform.name.Contains("PotionCorrect"))
        {
            StartCoroutine(Win());
            
        }
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(4);
    }

    void GameOver()
    {
        SceneManager.LoadScene(5);
    }
}
