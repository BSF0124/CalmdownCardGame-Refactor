using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stage", menuName = "Stage")]
public class StageData : ScriptableObject
{
    // 스테이지 번호
    public int stageNumber;

    // 스테이지에서 사용할 듀얼 모드
    public Enums.DuelMode mode;

    // 스테이지에 적용할 특수 규칙
    public List<Enums.StageRule> specialRules = new List<Enums.StageRule>();

    // AI가 사용하는 카드 목록
    public List<int> aiDeckIds = new List<int>();

    // 플레이어가 고정 덱으로 플레이 할 카드 목록
    public List<int> fixedDeckIds = new List<int>();
}
