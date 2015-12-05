using UnityEngine;
using System.Collections;

public class PlayerHealthController : MonoBehaviour
{
    public GameplayScreenController GSC;
    public Cub.View.Character Who;
    public UITexture FG;
    public UILabel Name;
    public UILabel Value;
    int ValueDisplay;
    public bool TeamOne;
    public int ScoreDownRate;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Setup(Cub.View.Character who, Cub.Model.Team t, bool teamOne, GameplayScreenController gsc)
    {
        Who = who;
        GSC = gsc;
        //FG.color = t.Colour_Primary;
        FG.SetDimensions(60, 10);
        Name.text = who.Stat.Name;
        TeamOne = teamOne;
        ValueDisplay = Who.Stat.Value;
        Value.text = ValueDisplay.ToString();
        if (!teamOne)
        {
            Vector3 where = Value.transform.localPosition;
            where.x *= -1;
            Value.transform.localPosition = where;
        }
        Value.gameObject.SetActive(false);
    }

    public void SetHealth(int health, int maxHealth)
    {
        int width = 60 * health / maxHealth;
        if (width <= 0)
        {
            width = 60;
            FG.color = Color.gray;
            Value.gameObject.SetActive(true);
            StartCoroutine("ScoreDown");
        }
        FG.SetDimensions(width, 10);
    }

    IEnumerator ScoreDown()
    {
        yield return new WaitForSeconds(0.6f);
        while (ValueDisplay > 0)
        {
            ValueDisplay -= ScoreDownRate;
            Value.text = ValueDisplay.ToString();
            GSC.SetScore(!TeamOne, ScoreDownRate);
            yield return new WaitForSeconds(0.005f);
        }
        Value.gameObject.SetActive(false);
    }
}
