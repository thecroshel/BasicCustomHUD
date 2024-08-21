using Exiled.API.Features;
using System;

namespace BasicCustomHUD
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "BasicCustomHUD";
        public override string Author => "thecroshel";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(8, 9, 11);

        public static Plugin Instance { get; private set; }

        private EventHandlers eventHandlers;

        public override void OnEnabled()
        {
            Instance = this;
            eventHandlers = new EventHandlers();
            Exiled.Events.Handlers.Server.RoundStarted += eventHandlers.OnRoundStarted;
            Exiled.Events.Handlers.Server.RoundEnded += eventHandlers.OnRoundEnded;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= eventHandlers.OnRoundStarted;
            Exiled.Events.Handlers.Server.RoundEnded -= eventHandlers.OnRoundEnded;

            eventHandlers = null;
            Instance = null;

            base.OnDisabled();
        }
    }
}