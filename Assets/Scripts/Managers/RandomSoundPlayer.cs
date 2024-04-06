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
        while (true)
        {
            // Espera un tiempo aleatorio entre 1 y 10 segundos
            yield return new WaitForSeconds(Random.Range(1f, 10f));

            // Reproduce un sonido aleatorio
            AudioManager.Instance.PlayRandomAudioClip();
        }
    }
}
