using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[RequireComponent(typeof(MonsterSpawner))]
public class DungeonManager : MonoBehaviour
{
    [SerializeField] private Image dimmed;

    private PlayerController playerController;
    private MonsterSpawner monsterSpawner;

    public List<Monster> monsterList = new List<Monster>();

    public bool onStage = false;

    private void Start()
    {
        monsterSpawner = GetComponent<MonsterSpawner>();
    }

    public void OnClickDungeonButton()
    {
        Debug.Log("sadas");
        StartCoroutine(StartStage());
    }

    public IEnumerator StartStage()
    {
        if (onStage) yield break;

        var seq = DOTween.Sequence();

        seq.OnStart(() => { dimmed.gameObject.SetActive(true); });
        seq.OnComplete(() => { dimmed.gameObject.SetActive(false); }).Play();
        seq.Append(dimmed.DOFade(1, 0.4f).From(0));
        seq.Append(dimmed.DOFade(0, 0.4f).From(1).SetDelay(1));
        seq.Play();

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController.transform.position = new Vector3(-32.5f, -51.6f, 0);

        onStage = true;

        int randnum = Random.Range(0, monsterSpawner.monsterScriptableObject.Length);
        foreach (var item in monsterSpawner.MakeMonster(randnum, 5, new Vector3(-31.51f, -54.17f, 0), new Vector3(-18.7f, -49.98f, 0)))
        {
            item.deathEvent.AddListener(() => monsterList.Remove(item));
            monsterList.Add(item);
        }

        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (monsterList.Count <= 0)
            {
                Debug.Log("Clear");
                onStage = false;
                break;
            }
        }
    }

    public void QuitDungeon()
    {
        monsterList.ForEach(p=>Destroy(p.gameObject));
        monsterList.Clear();

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController.transform.position = new Vector3(0, 0, 0);
    }
}
