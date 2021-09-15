using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonQuest1.Script
{
    class CommandList
    {
        public event EventHandler SwitchBuilder;
        Dictionary<Keys, Action> _commands;
        InputHandler _inputHandler;
        public CommandList()
        {
            _commands = new Dictionary<Keys, Action>();
            _commands.Add(Keys.F, delegate() { OnSwitchBuilder(); });
            _inputHandler = new InputHandler(_commands);
        }
        public void Update()
        {
            _inputHandler.CheckForKeyPresses();
        }
        protected virtual void OnSwitchBuilder()
        {
            EventHandler handler = SwitchBuilder;
            handler?.Invoke(this, null);
        }
    }
}
