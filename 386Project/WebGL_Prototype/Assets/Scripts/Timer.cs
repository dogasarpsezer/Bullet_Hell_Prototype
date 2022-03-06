
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
    }