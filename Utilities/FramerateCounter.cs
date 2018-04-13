using System.Collections.Generic;
using System.Linq;

namespace GP_Final_Catapult.Utilities {
    static class FramerateCounter {
        public static float AverageFramesPerSecond;

        private static long TotalFrames;
        private static float TotalSeconds;
        private static float CurrentFramesPerSecond;

        private const int MAXIMUM_SAMPLES = 100;

        private static Queue<float> _sampleBuffer = new Queue<float>();

        public static void Update(float deltaTime) {
            CurrentFramesPerSecond = 1.0f / deltaTime;
            _sampleBuffer.Enqueue(CurrentFramesPerSecond);
            if (_sampleBuffer.Count > MAXIMUM_SAMPLES) {
                _sampleBuffer.Dequeue();
                AverageFramesPerSecond = _sampleBuffer.Average(i => i);
            } else {
                AverageFramesPerSecond = CurrentFramesPerSecond;
            }
            TotalFrames++;
            TotalSeconds += deltaTime;
        }
    }
}