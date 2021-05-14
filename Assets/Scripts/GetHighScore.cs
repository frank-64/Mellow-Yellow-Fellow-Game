using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.UI;

public class GetHighScore : MonoBehaviour
{

    [SerializeField]
    string highscoreFile = "scores.txt";
    
    struct HighScoreEntry
    {
        public int score;
        public string name;

    }

    List<HighScoreEntry> allScores = new List<HighScoreEntry>();


    // Start is called before the first frame update
    void Start()
    {
        LoadHighScoreTable();
        SortHighScoreEntries();
        GetComponent<Text>().text = "Highscore\n" + allScores[0].score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadHighScoreTable()
    {
        using (TextReader file = File.OpenText(highscoreFile))
        {
            string text = null;
            while ((text = file.ReadLine()) != null)
            {
                string[] splits = text.Split(' ');
                HighScoreEntry entry;
                entry.name = splits[0];
                entry.score = int.Parse(splits[1]);
                allScores.Add(entry);
            }
        }
    }

    public void SortHighScoreEntries()
    {
        allScores.Sort((x, y) => y.score.CompareTo(x.score));
    }
}