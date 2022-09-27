using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private Image _wallHealthBar;
    [SerializeField] private Transform _healthContainer;
    [SerializeField] private Hitable _player;
    [SerializeField] private Hitable _base;

    private void Update()
    {
        int damage = _healthContainer.childCount - _player.GetHealth();
        for (int i = 0; i < _healthContainer.childCount; i++)
        {
            if (i < damage)
                _healthContainer.GetChild(i).GetChild(0).gameObject.SetActive(false);
            else
                _healthContainer.GetChild(i).GetChild(0).gameObject.SetActive(true);
        }

        _wallHealthBar.fillAmount = _base.GetPercentHealth();

    }
}
