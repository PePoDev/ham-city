using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace GP_Final_Catapult.Managers {
    static class AudioManager {
		public static Dictionary<string, SoundEffectInstance> audios = new Dictionary<string, SoundEffectInstance>();

		public static void AddAudioEffect(string nameAudio,SoundEffectInstance audio) {
			audios.Add(nameAudio, audio);
		}
		public static void PlayAudio(string nameAudio) {
			audios[nameAudio].Play();
		}
    }
}
