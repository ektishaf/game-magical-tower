using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicalTower : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Image healthFill;
    public int health = 100;
    public GameObject fireballPrefab;
    public GameObject barragePrefab;
    public float fireballCooldown = 2f;
    public float barrageCooldown = 5f;
    private float fireballTimer, barrageTimer;

    void Update()
    {
        fireballTimer += Time.deltaTime;
        barrageTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.F) && fireballTimer >= fireballCooldown)
        {
            CastFireball();
            fireballTimer = 0f;
        }

        if (Input.GetKeyDown(KeyCode.B) && barrageTimer >= barrageCooldown)
        {
            CastBarrage();
            barrageTimer = 0f;
        }
    }

    void CastFireball()
    {
        GameObject enemy = FindClosestEnemy();
        if (enemy != null)
        {
            GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
            fireball.GetComponent<Projectile>().SetTarget(enemy.transform);
        }
    }

    void CastBarrage()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            GameObject barrage = Instantiate(barragePrefab, transform.position, Quaternion.identity);
            barrage.GetComponent<Projectile>().SetTarget(enemy.transform);
        }
    }

    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float minDist = Mathf.Infinity;
        GameObject closest = null;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = enemy;
            }
        }
        return closest;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            Debug.Log("Main enemy hit me");
            Enemy enemy = collision.transform.GetComponent<Enemy>();
            health -= enemy.damage;
            healthFill.fillAmount = (float)health / 100.0f;
            Destroy(enemy.gameObject);

            if (health <= 0)
            {
                gameOverPanel.SetActive(true);
            }
        }
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}