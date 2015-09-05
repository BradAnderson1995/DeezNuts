using Assets.Scripts.Player;

namespace Assets.Scripts.Level
{
    public interface IItem
    {
        void ReceiveItem(PlayerController player);
    }
}