using UnityEngine;

namespace Assets.Scripts { 
    public class PowerUpsLoader
    {
        static System.Random rand = new System.Random();
        static ResourceLoader resourceLoader = new ResourceLoader();
        public static void GenerateRandomPowerUp(Vector3 position){
            int procent = rand.Next(100);
            if (procent >= 70) {
                if (procent < 75)
                {
                    GameObject.Instantiate(resourceLoader.LoadWallWalkerPowerUp(),position,new Quaternion(0,0,0,0));
                    return;
                }
                if (procent < 85)
                {
                    GameObject.Instantiate(resourceLoader.LoadBombsPowerUp(), position, new Quaternion(0, 0, 0, 0));
                    return;
                }
                if (procent < 90)
                {
                    GameObject.Instantiate(resourceLoader.LoadSpeedPowerUp(), position, new Quaternion(0, 0, 0, 0));
                    return;
                }
                if (procent < 100)
                {
                    GameObject.Instantiate(resourceLoader.LoadExplodeRadiusPowerUp(), position, new Quaternion(0, 0, 0, 0));
                    return;
                }
            }
        }
    }
}
