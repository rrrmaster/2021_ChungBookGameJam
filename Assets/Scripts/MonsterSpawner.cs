using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public MonsterScriptableObject[] monsterScriptableObject;

    private Dictionary<int, MonsterScriptableObject> cahceMonsterDic = new Dictionary<int, MonsterScriptableObject>();

    private void Awake()
    {
        CacheMonsterDic();
        MakeMonster(1, 20, new Vector3(1.5f, -6), new Vector3(18, 1.5f));
    }

    private Monster[] MakeMonster(int id, int count, Vector3 minXY, Vector3 maxXY)
    {
        Monster[] monsters = new Monster[count];

        for (int i = 0; i < count; i++)
        {
            Monster monster = Instantiate(monsterPrefab, Vector3.zero, Quaternion.identity).GetComponent<Monster>();
            monster.InitializeScriptableObject(GetMonsterScriptableObject(id));
            monster.SetMinMaxXY(minXY, maxXY);

            float x = Random.Range(minXY.x, maxXY.x);
            float y = Random.Range(minXY.y, maxXY.y);

            monster.SetPosition(new Vector3(x, y));

            monsters[i] = monster;
        }
        return monsters;
    }

    private void CacheMonsterDic()
    {
        int i = 0;
        foreach (var item in monsterScriptableObject)
        {
            cahceMonsterDic.Add(i, item);
            i++;
        }
    }

    private MonsterScriptableObject GetMonsterScriptableObject(int id)
    {
        MonsterScriptableObject result;
        if (!cahceMonsterDic.TryGetValue(id, out result))
        {
            Debug.LogWarning(id + "Not Found");
        }
        return result;
    }
}
