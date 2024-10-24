using UnityEngine;

public class Interfaces : MonoBehaviour
{
    public interface IFollowPoints
    {
        public void GetNextPoint() {}  
        void MoveToNextPoint(Transform nextPoint) { }
    }
}
