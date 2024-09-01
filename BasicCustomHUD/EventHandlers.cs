using Exiled.API.Features;
using RueI.Displays.Scheduling;
using RueI.Displays;
using RueI.Elements;
using RueI.Extensions.HintBuilding;
using RueI.Parsing.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;
using static PlayerList;
using RueI.Extensions;
using System.Linq;
using Exiled.API.Features.Roles;

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
                        var gameTime = $"{Round.ElapsedTime.Minutes:D2}:{Round.ElapsedTime.Seconds:D2}";
                        string playerName = player.Nickname;
                        string roleName = player.Role.Type.ToString();
                        float tps = (float)Server.Tps;
                        float playerHealth = player.Health;
                        int playerCount = Player.List.Count();
                        string currentTime = DateTime.Now.ToString("HH:mm");
                        int playerId = player.Id;
                        string badge = player.Group?.BadgeText ?? " ";
                        string customInfo = player.CustomInfo ?? " ";
                        string formattedMessage = $"<size=20><color=red>NAME:</color>{playerName} | <color=green>TIME:</color>{gameTime} | <color=#42e9f5>TPS:</color>{tps} | <color=blue>ROLE:</color>{roleName} | <color=#777777>ID:</color>{playerId}</size><size=8><color=black>[1.1.2]</color>";

                        DisplayCore core = DisplayCore.Get(player.ReferenceHub);

                        var elementReference = new TimedElemRef<SetElement>();

                        var displayTime = TimeSpan.FromSeconds(1);

                        core.SetElemTemp(formattedMessage, 5, displayTime, elementReference);
                    }
                    await Task.Delay(TimeSpan.FromMilliseconds(999), token);
                }
            }
            catch (TaskCanceledException)
            {
                Log.Info("Display task canceled.");
            }
            catch (Exception ex)
            {
                Log.Error($"Error displaying hint: {ex.Message}");
            }
        }
    }
}