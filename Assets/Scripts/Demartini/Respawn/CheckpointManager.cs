using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }

    [SerializeField] Checkpoints[] checkpoints;
    Transform player;
    int currentIndex = 0;
    public static int savedIndex = 0;



    private void Awake()
    {
        Instance = this;
        currentIndex = savedIndex;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        player.position = checkpoints[currentIndex].transform.position;
        player.forward = checkpoints[currentIndex].transform.forward;

        for (int i = 0; i <= currentIndex; i++)
        {
            if(checkpoints.Length > i)
            {
                checkpoints[i].LoadCheckpoint();
            }
        }
    }

    public void passCheck(int newIndex)
    {
        if (newIndex <= currentIndex)return;

        currentIndex = newIndex;
        savedIndex = currentIndex;
        checkText.Instance.showText();
    }

    public Vector3 checkpointPOS()
    {
        return checkpoints[currentIndex].transform.position;
    }
}
