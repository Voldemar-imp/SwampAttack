using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private List <Weapon> _weapons;
    [SerializeField] private List<Weapon> _tempWeapons;
    [SerializeField] private Transform _shootPoint;

    private Weapon _currentWeapon;
    private int _currentWeaponNumber = 0;
    private Animator _animator;
    private int _currenrHealth;

    public int Money { get; private set; }
    public event UnityAction <int, int> HealthChanged;
    public event UnityAction<int> MoneyChanged;

    private void Start()
    {
        _animator= GetComponent<Animator>();
        _currenrHealth = _health;
        MoneyChanged?.Invoke(Money);

        foreach (var weapon in _weapons)
        {
           Weapon weaponNew =  Instantiate(weapon, gameObject.transform);
            weaponNew.gameObject.SetActive(false);
            _tempWeapons.Add(weaponNew);
        }
        _weapons = _tempWeapons;

        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            _currentWeapon.Shoot(_shootPoint);
            _animator.Play("Shoot");
        }
    }

    public void ApplyDamage(int damage)
    {
        _currenrHealth -= damage;
        HealthChanged?.Invoke(_currenrHealth,_health);

        if (_currenrHealth <= 0)
        {
            Die();
        }
    }

    public void BuyWeapon(Weapon weapon)
    {
        Weapon weaponNew = Instantiate(weapon, gameObject.transform);
        weaponNew.gameObject.SetActive(false);
        _weapons.Add(weaponNew);
        Money -= weaponNew.Prise;
        MoneyChanged?.Invoke(Money);
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void AddMoney(int money)
    {
        Money+= money;
        MoneyChanged?.Invoke(Money);
    }

    public void NextWeapon() 
    {     
        if (_currentWeaponNumber == _weapons.Count - 1)
            _currentWeaponNumber = 0;
        else
            _currentWeaponNumber++;

        _currentWeapon.gameObject.SetActive(false);
        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    public void PreviousWeapon()
    {
        if (_currentWeaponNumber == 0)
            _currentWeaponNumber = _weapons.Count - 1;
        else
            _currentWeaponNumber--;

        _currentWeapon.gameObject.SetActive(false);
        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    private void ChangeWeapon(Weapon weapon)
    {        
        _currentWeapon = weapon;
        _currentWeapon.gameObject.SetActive(true);
    }
}
