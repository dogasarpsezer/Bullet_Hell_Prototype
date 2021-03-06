
    public class Timer
    {
        private float time;
        private float currentTime;
        public Timer(float time)
        {
            this.time = time;
            currentTime = 0f;
        }

        public void UpdateTimer(float additionTime)
        {
            currentTime += additionTime;
        }

        public float GetCurrentTime()
        {
            return currentTime;
        }
        
        public bool TimerDone()
        {
            if (currentTime >= time)
            {
                return true;
            }
            return false;
        }

        public void RestartTimer()
        {
            currentTime = 0f;
        }

        public float NormalizeTime()
        {
            return currentTime / time;
        }
        
        public void ForceComplete()
        {
            currentTime += time;
        }
    }