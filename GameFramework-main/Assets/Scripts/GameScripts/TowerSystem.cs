using UnityEngine;

public class TowerSystem : MonoBehaviour
{
    [Header("타워의 공격력, HP")]
    public int attackDamage;
    public int towerHP;

    [Tooltip("타워가 죽었는가?")]
    public bool towerDie;

    [Header("타워 돈 관련 변수")]
    public float towerMoney;
    public float towerMoneySpeed;

    void Start()
    {
        towerDie = false;
        towerMoney = 0;

        //변경될 수 있으니, 내부 변수로 받아옵니다. (기본 체력과 공격력은 건들지 않기 위함)
        int currentTowerHP = towerHP;
        int currentAttackDamage = attackDamage;     
    }

    void Update()
    {
        if(towerHP >= 0)            //타워의 피가 0 이하면
        {
            towerDie = true;        //타워 죽음
        }

        towerMoney += Time.deltaTime * towerMoneySpeed;       //타워에서 돈을 계속 생성합니다.

    }

    public void UnitGenerate(UnitData data)
    {
        //data.
    }

}
