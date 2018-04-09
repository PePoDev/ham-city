using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GP_Final_Catapult.GameObjects;
namespace GP_Final_Catapult.Screens {
    class GamePlayScreen : IScreen{
        Catapult Catapult;
        Enemy Enemy;
        Bullet Bullet;
        public override void Initial() {

        }
        public override void LoadContent() {
            base.LoadContent();

        }

        
        void LoadLevel(string fileName) {
            FileStream fs = new FileStream(fileName, FileMode.CreateNew);
            BinaryWriter w = new BinaryWriter(fs);
            for (int i = 0; i < 11; i++) {
                w.Write(i);
            }
            w.Close();
            fs.Close();
            
            fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader r = new BinaryReader(fs);
            for (int i = 0; i < 11; i++) {
                Console.WriteLine(r.ReadInt32());

            }
            r.Close();
            fs.Close();
        }
    }
}
