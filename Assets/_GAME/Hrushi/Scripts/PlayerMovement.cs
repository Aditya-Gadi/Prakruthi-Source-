using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
	public enum Directions {TowardsX, TowardsZ, TowardsNegX, TowardsNegZ };


	[SerializeField]
    private  float MaxDistance = .6f;

    public static bool _levelOver = false;
	public float moveDuration;
	public float inputThreshold;
	public Animator animator;
	public float Health;
	public Slider healthBar;
	public GameObject DamageEffect;
	public GameObject DeadMenu;
	public GameObject pausedMenu;
	public Directions directions;
	public int collected;
	public Text InfoText;

	Rigidbody rb;
	bool _isMoving = false;
	Vector3 dir = Vector3.zero;
	GameObject rock;
	bool isAlive = true;

	private void Start()
    {
		collected = 0;
		rb = GetComponent<Rigidbody>();
		Health = 100f;
    }

    private void Update()
	{
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");
		healthBar.value = Health / 100f;

		if (!_isMoving && ((x > inputThreshold || x < -inputThreshold) || (y > inputThreshold || y < -inputThreshold)) && isAlive)
		{
			switch(directions)
            {
				case Directions.TowardsX:
					if (x != 0)
						dir = x > 0 ? Vector3.back : Vector3.forward;
					else if (y != 0)
						dir = y > 0 ? Vector3.right : Vector3.left;
					break;
				case Directions.TowardsZ:
					if (x != 0)
						dir = x > 0 ? Vector3.right : Vector3.left;
					else if (y != 0)
						dir = y > 0 ? Vector3.forward : Vector3.back;
					break;
				case Directions.TowardsNegX:
					if (x != 0)
						dir = x > 0 ? Vector3.forward : Vector3.back;
					else if (y != 0)
						dir = y > 0 ? Vector3.left : Vector3.right;
					break;
				case Directions.TowardsNegZ:
					if (x != 0)
						dir = x > 0 ? Vector3.left : Vector3.right;
					else if (y != 0)
						dir = y > 0 ? Vector3.back : Vector3.forward;
					break;
				default:
					if (x != 0)
						dir = x > 0 ? Vector3.right : Vector3.left;
					else if (y != 0)
						dir = y > 0 ? Vector3.forward : Vector3.back;
					break;
			}

			

			animator.SetBool("IsMoving", true);
			transform.rotation = Quaternion.LookRotation(dir);

			bool moveCan = CanMove();
			if (!moveCan)
				return;

            bool checkRock = CheckForRock();
            if (checkRock)
            {
                animator.SetBool("IsMoving", false);
                animator.SetBool("ShouldPush", true);
                StartCoroutine(MoveRock(rock.transform));
                StartCoroutine(MovePlayer());
                return;
            }
            else
            {
				animator.SetBool("ShouldPush", false);
			}
            StartCoroutine(MovePlayer());
		}
		else if(!_isMoving)
        {
			animator.SetBool("IsMoving", false);
			animator.SetBool("ShouldPush", false);
		}

		if(healthBar.value <= 0)
        {
			StartCoroutine(ToggleDeadMenu());
        }

		if(Input.GetKeyDown(KeyCode.Escape))
        {
			PauseGame();
        }
	}

	void IsNotMoving()
	{
		_isMoving = false;
	}

	IEnumerator MovePlayer()
	{
		float count = 0.0f;
		_isMoving = true;
		Vector3 originPos = transform.position;
		Vector3 targetPos = originPos + dir;
		while (count < moveDuration)
        {
			transform.position = Vector3.Lerp(originPos, targetPos, (count / moveDuration));
			count += Time.deltaTime;
			yield return null;
		}
		float posX = transform.position.x;
		float posZ = transform.position.z;
		transform.position = new Vector3(Mathf.RoundToInt(posX), 0.75f, Mathf.RoundToInt(posZ));
		IsNotMoving();
		
	}

	IEnumerator MoveRock(Transform obj)
	{
		float count = 0.0f;
		_isMoving = true;
		Vector3 originPos = obj.position;
		Vector3 targetPos = originPos + dir;
		while (count < moveDuration)
		{
			obj.position = Vector3.Lerp(originPos, targetPos, (count / moveDuration));
			count += Time.deltaTime;
			yield return null;
		}
		obj.position = targetPos;
		//IsNotMoving();
	}

	bool CanMove()
    {
		bool move = true;
		RaycastHit hitFront;
		if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), 
			transform.forward, out hitFront, MaxDistance))
		{
			Debug.Log("Hit: " + hitFront.collider.name);
			if (hitFront.collider.gameObject.tag == "Wall")
            {
				move = false;
				Debug.Log("Wall");
			}
			else
				move = true;
		}
		return move;
	}

	bool CheckForRock()
    {
		bool move = false;
		RaycastHit hitFront;

		if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), 
			transform.forward, out hitFront, MaxDistance))
		{
			if (hitFront.collider.gameObject.tag == "Rock")
			{
				move = true;
				Debug.Log("Rock");
				rock = hitFront.collider.gameObject;
			}
			else
				move = false;
		}
		return move;
	}

	public void ShowDamage()
    {
		StartCoroutine(ToggleDamage());
    }

	public void Die()
    {
		animator.SetBool("Dead", true);
		isAlive = false;
    }

	public void RestartLevel()
    {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

	public IEnumerator ToggleDeadMenu()
	{
		Die();
		yield return new WaitForSeconds(2.2f);
		DeadMenu.SetActive(true);
	}

	IEnumerator ToggleDamage()
    {
		DamageEffect.SetActive(true);
		yield return new WaitForSeconds(0.4f);
		DamageEffect.SetActive(false);
	}

	public void PauseGame()
    {
		Time.timeScale = 0;
		pausedMenu.SetActive(true);
    }

	public void ResumeGame()
	{
		Time.timeScale = 1;
		pausedMenu.SetActive(false);
	}

	public void RestartGame()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void ExitGame()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(0);
	}
}
