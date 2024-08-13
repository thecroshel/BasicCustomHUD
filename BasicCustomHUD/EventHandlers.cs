using BasicCustomHUD;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Exiled.Events.EventArgs.Server;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BasicCustomHUD
{
    public class EventHandlers
    {
        private CancellationTokenSource cancellationTokenSource;

        public void OnRoundStarted()
        {
            cancellationTokenSource = new CancellationTokenSource();
            Task.Run(() => DisplayGameTime(cancellationTokenSource.Token));
        }

        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            cancellationTokenSource?.Cancel();
        }

        private async Task DisplayGameTime(CancellationToken token)
        {
            try
            {
                while (Round.IsStarted)
                {
                    if (token.IsCancellationRequested)
                        break;

                    foreach (var player in Player.List)
                    {
                        string playerName = player.Nickname;
                        string roleName = player.Role.Type.ToString();
                        float tps = (float)Server.Tps;

                        var gameTime = $"{Round.ElapsedTime.Minutes:D2}:{Round.ElapsedTime.Seconds:D2}";
                        player.ShowHint($"\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n<size=20><color=#a18f1b>Saint SCP</color>|<color=red>NAME:</color>{playerName}|<color=green>TIME:</color>{gameTime}|<color=#42e9f5>TPS:{tps}</color>|<color=blue>ROLE:</color>{roleName}</size><size=10><color=black>[1.3]</color></size>", Plugin.Instance.Config.UpdateInterval);
                    }

                    await Task.Delay(TimeSpan.FromSeconds(Plugin.Instance.Config.UpdateInterval), token);
                }
            }
            catch (TaskCanceledException)
            {
                // wasd
            }
        }
    }
}