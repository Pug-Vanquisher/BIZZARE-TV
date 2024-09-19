using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class saveResult : MonoBehaviour
{
    [SerializeField] commentOn CommentOn;
    public Camera finalShotCamera;
    public GameObject panelStars, starsComment, starsUI;
    float score, stars;
    Entry newEntry = new();
    string screenname, scoreboardContents, newscoreboard, scoreboardPath;
    byte[] byteArray;

    string[] commentsPancakes = {
        "Первый блин комом",
        "Блины с заправки",
        "Завтрак студента",
        "Золотая середина",
        "Мамины блинчики",
        "Бабушкин шедевр"
    };

    string[] commentsTacos = {
        "Кухонное бедствие",
        "Звёздочка за попытку",
        "Приличное начало",
        "Приемлемый результат",
        "Домашние тако",
        "Шедевр шеф-повара"
    };

    // string[] comments;
    string comment;

    enum commentOn { pancakes, tacos }

    // костыль для json utility, которая не может в сериализацию списков напрямую
    [System.Serializable]
    public class scoreboard {
        public List<Entry> list;
    }
    scoreboard scores = new();

    void Start()
    {
        scoreboardPath = Application.dataPath + "/score_" + SceneManager.GetActiveScene().name + ".json";

        using (FileStream f = File.Open(scoreboardPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None)) f.Dispose();       
        
        scoreboardContents = File.ReadAllText(scoreboardPath);
        if (scoreboardContents != "") scores = JsonUtility.FromJson<scoreboard>(scoreboardContents);
        else scores.list = new();
        
        score = Cook.GetScore();
        stars = Cook.ScoreToStars();

        // if (CommentOn == commentOn.pancakes) comments = commentsPancakes;
        // else if (CommentOn == commentOn.tacos) comments = commentsTacos;
        if (CommentOn == commentOn.pancakes) comment = "CommentPancake";
        else if (CommentOn == commentOn.tacos) comment = "CommentTaco";
    }

    public void save() {
        // сохранение скриншота

        RenderTexture screenTexture = new RenderTexture(Screen.width, Screen.height, 16);
        finalShotCamera.targetTexture = screenTexture;
        RenderTexture.active = screenTexture;
        finalShotCamera.Render();

        Texture2D renderedTexture = new Texture2D(Screen.width, Screen.height);
        renderedTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        RenderTexture.active = null;
        byteArray = renderedTexture.EncodeToPNG();

        if (!Directory.Exists(Application.dataPath + "/screenshots")) {
            Directory.CreateDirectory(Application.dataPath + "/screenshots");
        }

        screenname = Application.dataPath + "/screenshots/screenshot " + System.DateTime.Now.ToString("yy-MM-dd HH-mm-ss") + ".png";
        File.WriteAllBytes(screenname, byteArray);

        // запись сохранения
        newEntry = new Entry(score, stars, screenname);
        scores.list.Add(newEntry);

        newscoreboard = JsonUtility.ToJson(scores, true);
        File.WriteAllText(scoreboardPath, newscoreboard);
        
        panelStars.SetActive(true);

        // starsComment.GetComponent<TextMeshProUGUI>().text = comments[(int)stars];
        starsComment.GetComponent<TextMeshProUGUI>().text = LocalizationSettings.StringDatabase.GetLocalizedString("DDLocalization", comment + (int)stars);


        for (int i = 1; i < 6; i++) {
            if (i <= stars) starsUI.GetComponentsInChildren<Image>()[i - 1].color = new Color(1, 222f / 255f, 0);
            else break;
        }

        gameObject.SetActive(false);
    }

    public void toMainMenu() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name + "Menu");
    }
}
