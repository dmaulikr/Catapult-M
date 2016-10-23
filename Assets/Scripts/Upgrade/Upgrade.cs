using UnityEngine;
using System.Collections;

public interface UpgradeReceiver
{
    void receive(Upgrade upgrade);
}

public class Upgrade : MonoBehaviour
{
    public Sprite icon;

    private UpgradeReceiver receiver {
        get {
            return receiverrr.GetComponent<UpgradeReceiver>();
        }
    }
    [SerializeField]
    private Transform receiverrr; //awkward: using b/c interfaces don't seem to appear in Unity's inspector (??)

    public virtual int level() {
        throw new System.Exception("please only use subclasses of Upgrade.");
    }

    public void giveUpgrade() {
        receiver.receive(this);
    }
}

