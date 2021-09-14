using Microsoft.Xna.Framework.Graphics;

namespace DragonQuest1
{
    public class Animation
    {
        public int CurrentFrame;
        public int FrameCount;
        public int FrameHeight;
        public int FrameWidth;
        public int StartPosition;
        public float FrameSpeed;
        public bool isLooping;
        public Texture2D Texture;
        public Animation(Texture2D texture, int frameCount, int frameHeight, int frameWidth, int startPosition)
        {
            Texture = texture;
            FrameCount = frameCount;
            FrameHeight = frameHeight;
            FrameWidth = frameWidth;
            StartPosition = startPosition;
            isLooping = true;
            FrameSpeed = 0.2f;
        }
    }
}
