using Exiled.API.Features;
using System;
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
            Task.Run(() => DisplayPlayerInfoContinuously(cancellationTokenSource.Token));
        }

        public void OnRoundEnded(Exiled.Events.EventArgs.Server.RoundEndedEventArgs ev)
        {
            cancellationTokenSource?.Cancel();
        }

        private async Task DisplayPlayerInfoContinuously(CancellationToken token)
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
                        int playerId = player.Id;
                        string badge = player.Group?.BadgeText ?? " ";
                        string customInfo = player.CustomInfo ?? " ";

                        var gameTime = $"{Round.ElapsedTime.Minutes:D2}:{Round.ElapsedTime.Seconds:D2}";

                        string firstLineText = Plugin.Instance.Config.FirstLineText;
                        string secondLineText = Plugin.Instance.Config.SecondLineText;
                        string thirdLineText = Plugin.Instance.Config.ThirdLineText;

                        player.ShowHint($"\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n<size=20>{firstLineText}" +
                                        $"\n{secondLineText}" +
                                        $"\n{thirdLineText}</size>" +
                                        $"\n<size=20><color=red>NAME:</color>{playerName} | <color=green>TIME:</color>{gameTime} | <color=#42e9f5>TPS:</color>{tps} | <color=blue>ROLE:</color>{roleName} | <color=#777777>ID:</color>{playerId}</size><size=10><color=black>[1.1]</color>", Plugin.Instance.Config.UpdateInterval);

                    }
                    await Task.Delay(TimeSpan.FromSeconds(Plugin.Instance.Config.UpdateInterval), token);
                }
            }
            catch (TaskCanceledException)
            {
            }
        }
    }
}