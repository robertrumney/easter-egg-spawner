using UnityEngine;

// Basic example egg class
public class EasterEgg : MonoBehaviour
{
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
