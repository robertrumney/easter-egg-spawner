using UnityEngine;

// Basic example egg class
public class EasterEgg : MonoBehaviour
{
    // Init
    private void Start()
	{
		rb = GetComponent<Rigidbody>();
		coroutine = CheckMovement();
		StartCoroutine(coroutine);
	}

    // Check if spawner has spawned egg in such a way that it is falling into space - if so destroy the egg
	private IEnumerator CheckMovement()
	{
		while (true)
		{
			if (rb.velocity.magnitude < 0.01f && Time.timeScale != 0f)
			{
				yield return new WaitForSeconds(2f);
				if (rb.velocity.magnitude < 0.01f)
				{
					Debug.Log("Object is stationary.");
					StopCoroutine(coroutine);
				}
			}
			else
			{
				Debug.Log("Object is moving, destroying...");
				Destroy(gameObject);
			}
			yield return null;
		}
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Game.instance.audioSource.clip = Game.instance.pickupItem;
            Game.instance.audioSource.Play();
            
            Game.instance.Say("Egg Found");

            SaveGame.instance.save.eggs++;

            if (SaveGame.instance.save.eggs == 100) 
                Game.instance.Achieve("easter100");
            if (SaveGame.instance.save.eggs == 250) 
                Game.instance.Achieve("easter250");
            if (SaveGame.instance.save.eggs == 500) 
                Game.instance.Achieve("easter500");

            Destroy(gameObject);
        }
    }
}
