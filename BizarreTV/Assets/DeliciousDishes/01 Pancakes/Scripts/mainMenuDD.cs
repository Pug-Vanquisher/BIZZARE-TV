using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenuDD : MonoBehaviour
{
    public string gameSceneName;
    public GameObject dishesPanel, deletePanel, numDishes, cardsParent, cardPrefab;
    GameObject pref;
    string scoreboardPath, scoreboardContents, newscoreboard;
    saveResult.scoreboard scores = new();
    float score, stars;
    byte[] picData;
    Texture2D picTexture;
    GameObject cardToRemove;
    Entry entryToRemove;

    public void GetTable()
    {
        scoreboardPath = Application.dataPath + "/score_" + gameSceneName + ".json";

        using (FileStream f = File.Open(scoreboardPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None)) f.Dispose();

        scoreboardContents = File.ReadAllText(scoreboardPath);
        if (scoreboardContents != "") scores = JsonUtility.FromJson<saveResult.scoreboard>(scoreboardContents);
        else scores.list = new();

        foreach (Transform child in cardsParent.transform) Destroy(child.gameObject);

        UpdateNumDishes();

        foreach (Entry entry in scores.list)
        {
            score = entry.score;
            stars = entry.stars;
            pref = Instantiate(cardPrefab, cardsParent.transform);
            foreach (Transform child in pref.transform)
            {
                if (child.name == "подложка карточки")
                {
                    if (File.Exists(entry.pic))
                    {
                        StartCoroutine(loadingSprite(child.transform.GetChild(0).GetComponent<Image>(), entry.pic));
                    }
                }
                else if (child.name == "stars")
                {
                    for (int i = 1; i < 6; i++)
                    {
                        if (i <= stars) child.GetComponentsInChildren<Image>()[i - 1].color = new Color(1, 222f / 255f, 0);
                        else break;
                    }
                }
                else if (child.name == "score") child.GetComponent<TextMeshProUGUI>().text = score.ToString();
                else if (child.name == "btnDelete") child.GetComponent<Button>().onClick.AddListener(() => ConfirmRemovingEntry(child.GetComponent<Button>().transform.parent.gameObject, entry));

            }
        }
    }

    IEnumerator loadingSprite(Image image, string pic)
    {
        yield return new WaitForSeconds(0);
        picData = File.ReadAllBytes(pic);
        picTexture = new Texture2D(1920, 1080); // пыталась сделать это в async-await, но он не может в объявления
        if (picTexture.LoadImage(picData))
        { // превращение текстуры в спрайт для отображения в ui
            image.sprite = Sprite.Create(picTexture, new Rect(0, 0, 1920, 1080), new Vector2(0.5f, 0.5f), 100, 0, SpriteMeshType.FullRect);
        } // есть вариант напрямую применить текстуру к raw image mapImage.GetComponent<RawImage>().texture = mapTexture;
    }

    void ConfirmRemovingEntry(GameObject card, Entry en)
    {
        deletePanel.SetActive(true);
        cardToRemove = card;
        entryToRemove = en;
    }

    public void RemoveEntry()
    {
        File.Delete(entryToRemove.pic);
        if (File.Exists(entryToRemove.pic + ".meta")) File.Delete(entryToRemove.pic + ".meta");
        scores.list.Remove(entryToRemove);
        newscoreboard = JsonUtility.ToJson(scores, true);
        File.WriteAllText(scoreboardPath, newscoreboard);
        UpdateNumDishes();
        Destroy(cardToRemove);
        CancelRemovingEntry();
    }

    public void CancelRemovingEntry()
    {
        deletePanel.SetActive(false);
        cardToRemove = null;
        entryToRemove = null;
    }

    void UpdateNumDishes()
    {
        numDishes.GetComponent<TextMeshProUGUI>().text = scores.list.Count.ToString();
    }

    public void ShowHideDishes()
    {
        dishesPanel.SetActive(!dishesPanel.activeSelf);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}
