using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonQuest1.Script
{
    class InputHandler
    {
        private Dictionary<Keys, Action> _myKeys;
        private Dictionary<Keys, bool> _keyPressedState;
        public InputHandler(Dictionary<Keys, Action> keys)
        {
            _myKeys = keys;
            _keyPressedState = new Dictionary<Keys, bool>();
            foreach(Keys key in _myKeys.Keys)
                _keyPressedState.Add(key, false);
        }
        public void CheckForKeyPresses()
        {
            foreach (Keys key in _myKeys.Keys)
                if(Keyboard.GetState().IsKeyDown(key) && !_keyPressedState[key])
                {
                    _keyPressedState[key] = true;
                    HandleKeyPressed(key);
                }
                else if(!Keyboard.GetState().IsKeyDown(key) && _keyPressedState[key])
                {
                    _keyPressedState[key] = false;
                    HandleKeyReleased(key);
                }
        }
        private void HandleKeyPressed(Keys key)
        {
            _myKeys[key].Invoke();
        }
        private void HandleKeyReleased(Keys key)
        {

        }
    }
}
