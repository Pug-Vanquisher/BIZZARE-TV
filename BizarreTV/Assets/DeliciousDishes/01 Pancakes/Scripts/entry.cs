[System.Serializable]
public class Entry
{
    public float score, stars;
    public string pic;

    public Entry() {
        score = 0;
        stars = 0;
        pic = "";
    }

    public Entry(float Score, float Stars, string Pic) {
        score = Score;
        stars = Stars;
        pic = Pic;
    }
}
