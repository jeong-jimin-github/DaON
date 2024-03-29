using UnityEngine;

public class NoteGen : MonoBehaviour
{
    float musicBPM = 185;
    float stdBPM = 60;
    float musicTempo = 2f;
    float stdTempo = 4f;
    float tikTime = 0f;
    float nextTime = 0f;

    public GameObject note;
    public GameObject lonen;
    public GameObject tn;
    string maptxt;
    int currentIndex = 0;

    void Start()
    {
        maptxt = Resources.Load<TextAsset>("Map/Map01").text;
    }

    void Gen(int type, int line, int end)
    {
        if (line == 0)
            return;

        for (int i = 0; i < line.ToString().Length; i++)
        {
            int lineDigit = int.Parse(line.ToString()[i].ToString());
            InstantiateNoteObject(type, lineDigit, end);
        }
    }

    void InstantiateNoteObject(int type, int lineDigit, int end)
    {
        float note_x = (lineDigit - 2.5f) * 1.0f;

        if (type == 1)
        {
            // 일반 노트 생성 및 추가 로직
            GameObject newNote = Instantiate(note, new Vector3(note_x, 30), Quaternion.identity);
            // 추가 로직...
        }
        else if (type == 2)
        {
            // 롱노트 생성 및 추가 로직
            // 여기서 endTime을 설정하거나, 다른 조작을 수행할 수 있습니다.
            GameObject newNote = Instantiate(note, new Vector3(note_x, 30), Quaternion.identity);
            GameObject longNoteTailObject = Instantiate(lonen, new Vector3(note_x, 30), Quaternion.identity);
            longNoteTailObject.transform.parent = newNote.transform;
            longNoteTailObject.transform.localPosition = new Vector3(0f, end*1.8f, 0f);
            longNoteTailObject.transform.localScale = new Vector3(0.5f, end, 1f);
            for (int i = 1; i <= end; i++) {
                GameObject longNote = Instantiate(tn, new Vector3(note_x, 30), Quaternion.identity);
                longNote.transform.parent = newNote.transform;
                longNote.transform.localPosition = new Vector3(0f, i * 3.5f, 0f);
            }
            // 추가 로직...
        }
    }

    void Update()
    {
        tikTime = (stdBPM / musicBPM) * (musicTempo / stdTempo);
        nextTime += Time.deltaTime;

        if (nextTime > tikTime)
        {
            if (currentIndex < maptxt.Split("\n").Length)
            {
                string mapLine = maptxt.Split("\n")[currentIndex];

                if (mapLine.StartsWith("!"))
                {
                    // Handle special cases (if needed)
                }
                else
                {
                    int type = int.Parse(mapLine.Replace(" ", "").Split(",")[0]);
                    int line = int.Parse(mapLine.Replace(" ", "").Split(",")[1]);
                    int end = int.Parse(mapLine.Replace(" ", "").Split(",")[2]);
                    Gen(type, line, end);
                }

                currentIndex++;
            }

            nextTime -= tikTime;
        }
    }
}
