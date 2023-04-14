using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Damageable
{
    public override void Damage_Implementation()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().path);
    }
}
