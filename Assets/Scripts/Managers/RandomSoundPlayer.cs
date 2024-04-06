using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(PlayRandomSound());
    }

    private IEnumerator PlayRandomSound()
    {
        yield return new WaitForSeconds(2 * 60);
        while (true)
        {
            // Espera un tiempo aleatorio entre 1 y 10 segundos
            

            // Reproduce un sonido aleatorio
            AudioManager.Instance.PlayRandomAudioClip();

            yield return new WaitForSeconds(Random.Range(60f, 100f));
        }
    }
}
