using Microsoft.Xna.Framework;
using System;

namespace GP_Final_Catapult.Utilities {
    class FadeItem {
        public float Xpos { get; set; }
        public float Ypos { get; set; }

        public float GetScale(bool fadeIn) {
			if (fadeIn) {
				if (Delay > 0) return 0f;
				else if (Radians > MathHelper.Pi) return 2f;
				return (float)Math.Cos(Radians) + 1f;
			} else {
				if (Delay > 0) return 2.0f;
				else if (Radians > MathHelper.Pi) return 0f;
				return (float)Math.Cos(Radians) + 1f;
			}
        }
        public float Delay { get; set; }
        public float Radians { get; set; }
		public void Update(float deltaTimeInMilliseconds, bool fadeIn) {
			if (fadeIn) {
				Delay -= deltaTimeInMilliseconds;
				if (Delay < 0) {
					Radians += deltaTimeInMilliseconds / 200.0f;
				}
			} else {
				Delay -= deltaTimeInMilliseconds;
				if (Delay < 0) {
					Radians += deltaTimeInMilliseconds / 200.0f;
				}
			}
			
		}
    }
}