using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
  [SerializeField] public List<moleGenerate> moles;
  [SerializeField] public List<plantGenerate> plants;

  [SerializeField] private TMPro.TextMeshProUGUI scoreText;
    [SerializeField] private GameObject winUI;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioSource audioSource;

    private HashSet<moleGenerate> currentMoles = new HashSet<moleGenerate>();
  private HashSet<plantGenerate> currentPlants = new HashSet<plantGenerate>();
  public int score;
  private bool playing = false;

  private float timer = 3f;
  private float currentTime = 0f;


    private HashSet<moleGenerate> timesOutMoles = new HashSet<moleGenerate>();
    int diff;
    double time1;
    public void StartGame() 
  {
    for (int i = 0; i < moles.Count; i++) 
    {
      moles[i].Hide();
      moles[i].SetIndex(i);
      plants[i].SetIndex(i);
    }
    
    currentMoles.Clear();
    currentPlants.Clear();
    score = 0;
    scoreText.text = "0";
    playing = true;

        diff = moles.Count;

  }



void Update() 
{
    if (playing) 
    {
        bool isWon = true;
        foreach (plantGenerate plant in plants)
        {
            if (plant.GetGrowState() != 3)
            {
                    isWon = false;
                break;
            }
        }
        if (isWon)
        {
            playing = false;
            audioSource.clip = winSound;
            audioSource.Play();
            currentMoles.Clear();
            currentPlants.Clear();
            winUI.SetActive(true);
        }
        
        currentTime += Time.deltaTime;

        bool allPlantsPlaced = true;
        foreach (plantGenerate plant in plants)
        {
            if (!plant.GetPlacedState())
            {
                allPlantsPlaced = false;
                break;
            }
        }

        if (currentTime >= timer && !allPlantsPlaced)
        {
            timer = Random.Range(0.5f, 3f);
            currentTime = 0f;
            int index = Random.Range(0, moles.Count);
            while (currentPlants.Contains(plants[index]))
            {
                index = Random.Range(0, moles.Count);
            }    
        
            if (!currentMoles.Contains(moles[index]) && !currentPlants.Contains(plants[index])) 
            {
                    if (!timesOutMoles.Contains(moles[index]))
                    {
                        currentMoles.Add(moles[index]);
                        timesOutMoles.Add(moles[index]);
                        Debug.Log("if "+index);
                        moles[index].Activate();
                        plants[index].ChangeBoxCollider2DState(false);
                    }
                    else
                    {
                        timesOutMoles.Remove(moles[index]);
                        Debug.Log("else " + index); 
                    }

            }

        }
    }
}



  public void AddScore(int moleIndex, int moleType) 
  {
        switch (moleType)
        {
            case 0: score += 10; break;
            case 1: score += 25; break;
            case 2: score += 50; break;
        }
    scoreText.text = $"{score}";
    currentMoles.Remove(moles[moleIndex]);

  }

  public void SubtractScore(int plantIndex, bool fromPlacement)
  {
        score -= 100;
        scoreText.text = $"{score}";
        if (fromPlacement)
        {
            currentMoles.Remove(moles[plantIndex]);
            currentPlants.Add(plants[plantIndex]);
        }
        else
        {
            currentPlants.Remove(plants[plantIndex]);
        }
        
  }

  public void Missed(int moleIndex) 
  {
    currentMoles.Remove(moles[moleIndex]);
  }

  public bool CheckIsMoleCurrent(int plantIndex)
  {
        return currentMoles.Contains(moles[plantIndex]);
  }
}
