using CardSpace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumberCardView : CardView
{
    [SerializeField] private GameObject Substate1;
    [SerializeField] private GameObject Substate2;
    [SerializeField] private GameObject Substate3;
    [SerializeField] private TextMeshProUGUI Sub1Sta1;
    [SerializeField] private TextMeshProUGUI Sub2Sta1;
    [SerializeField] private TextMeshProUGUI Sub2Sta2;
    [SerializeField] private TextMeshProUGUI Sub3Sta1;
    [SerializeField] private TextMeshProUGUI Sub3Sta2;
    [SerializeField] private TextMeshProUGUI Sub3Sta3;
    private NumberCard _numberCard;
    private int[] _squadValues = new int[3];

    public override void OnValidate()
    {
        base.OnValidate();

        _numberCard = _cardData as NumberCard;

        if (_numberCard == null)
        {
            Debug.LogWarning($"CardData is not NumberCard");
            return;
        }

        _squadValues[0] = _numberCard.Warriors;
        _squadValues[1] = _numberCard.Assasin;
        _squadValues[2] = _numberCard.Mage;

        Substate1.SetActive(false);
        Substate2.SetActive(false);
        Substate3.SetActive(false);
        
        switch (CheckCountOfSquads())
        {
            case 1:
                SetSubstance1();
                break;
            case 2:
                SetSubstance2();
                break;
            case 3:
                SetSubstance3();
                break;
        }
    }
    
    private int CheckCountOfSquads()
    {
         int a = _numberCard.Warriors == 0 ? 0 : 1;
         int b = _numberCard.Assasin == 0 ? 0 : 1;
         int c = _numberCard.Mage == 0 ? 0 : 1;

         return a + b + c;
    }

    private void SetSubstance1()
    {
        Substate1.SetActive(true);

        foreach (var value in _squadValues)
        {
            if (value != 0)
            {
                Sub1Sta1.text = $"{value}";
                return;
            }
        }
    }

    private void SetSubstance2()
    {
        int k = 1;
        Substate2.SetActive(true);

        foreach (var value in _squadValues)
        {
            if (value == 0) continue;

            if (k == 1)
            {
                k++;
                Sub2Sta1.text = $"{value}";
                continue;
            }

            if (k == 2)
            {
                Sub2Sta2.text = $"{value}";
            }
        }
    }

    private void SetSubstance3()
    {
        Substate3.SetActive(true);
        Sub3Sta1.text = $"{_squadValues[0]}";
        Sub3Sta2.text = $"{_squadValues[1]}";
        Sub3Sta3.text = $"{_squadValues[2]}";
    }
}