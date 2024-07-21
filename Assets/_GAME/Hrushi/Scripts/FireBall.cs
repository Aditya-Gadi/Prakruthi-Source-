using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed;
    public float Limit;

    float count = 0.0f;

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        count += Time.deltaTime;
        if (count >= Limit)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMovement>().ShowDamage();
            other.gameObject.GetComponent<PlayerMovement>().Health = 0;
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}