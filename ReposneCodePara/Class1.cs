using fr34kyn01535.Uconomy;
using Rocket.API;
using Rocket.Core;
using Rocket.Core.Commands;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ReposneCodePara
{
    public class Class1 : RocketPlugin<Config>
    {
        protected override void Load()
        {
            base.Load();
            Console.WriteLine("ReponseCode ParaGöstergesi - Aktif");
            Console.WriteLine("ReponseCode ParaGöstergesi - Aktif");
            Console.WriteLine("ReponseCode ParaGöstergesi - Aktif");
            Console.WriteLine("ReponseCode ParaGöstergesi - Aktif");
            Console.WriteLine("ReponseCode ParaGöstergesi - Aktif");
            Console.WriteLine("ReponseCode ParaGöstergesi - Aktif");
            Console.WriteLine("ReponseCode ParaGöstergesi - Aktif");
            Console.WriteLine("ReponseCode ParaGöstergesi - Aktif");
            Console.WriteLine("ReponseCode ParaGöstergesi - Aktif");
            Console.WriteLine("ReponseCode ParaGöstergesi - Aktif");

            U.Events.OnPlayerConnected += Join;

            if (Configuration.Instance.Uconomy)
            {
                R.Plugins.OnPluginsLoaded += OnPluginsLoaded;

            }
            else
            {
                UnturnedPlayerEvents.OnPlayerUpdateExperience += ExpUpdate;

            }



        }

        private void ExpUpdate(UnturnedPlayer player, uint experience)
        {
            EffectManager.sendUIEffectText(75, player.CSteamID, true, "MONEY", $" <B><color=#9884E2>{player.Experience}");
        }

        private void OnPluginsLoaded()
        {
            Uconomy.Instance.OnBalanceUpdate += BalanceUpdate;
        }

        private void BalanceUpdate(UnturnedPlayer player, decimal amt)
        {
            EffectManager.sendUIEffectText(75, player.CSteamID, true, "MONEY", $" <B><color=#9884E2>{Uconomy.Instance.Database.GetBalance(player.Id)}");
        }

        private void Join(UnturnedPlayer player)
        {
            EffectManager.sendUIEffect(5643, 75, player.CSteamID, true);

            if (Configuration.Instance.Uconomy)
            {
                EffectManager.sendUIEffectText(75, player.CSteamID, true, "MONEY", $"<B><color=#9884E2>{Uconomy.Instance.Database.GetBalance(player.Id)}");
            }
            else
            {
                EffectManager.sendUIEffectText(75, player.CSteamID, true, "MONEY", $"<B><color=#9884E2>{player.Experience}");
            }


        }

        [RocketCommand("paraspawn", "paraspawn", "/paraspawn <Oyuncu> <Miktar1", AllowedCaller.Both)]
        [RocketCommandPermission("r.paraspawn")]
        public void ExecuteNrpCommand(IRocketPlayer caller, string[] command)
        {
            string logo = Configuration.Instance.Logo;
            UnturnedPlayer pl = caller as UnturnedPlayer;
            UnturnedPlayer pls = UnturnedPlayer.FromName(command[0]);
            var fiyat = Convert.ToUInt64(command[1]);
            if (command.Count() != 0)
            {
                if (pls == null)
                {
                    ChatManager.serverSendMessage($"<color=red>Dikkat |</color> Oyuncu Bulunamadı.", Color.white, null, pl.SteamPlayer(), EChatMode.SAY, logo, true);
                    return;
                }
                else
                {
                    if (Configuration.Instance.Uconomy)
                    {
                        ChatManager.serverSendMessage($"<color=green>Başarılı |</color> Başarılı Şekilde Oyuncuya Para Gönderildi.", Color.white, null, pl.SteamPlayer(), EChatMode.SAY, logo, true);
                        ChatManager.serverSendMessage($"<color=green>Başarılı |</color> {pl.CharacterName} Adlı Oyuncu Tarafından Para Geldi Sana.", Color.white, null, pls.SteamPlayer(), EChatMode.SAY, logo, true);
                        Uconomy.Instance.Database.IncreaseBalance(pls.Id, fiyat);
                        EffectManager.sendUIEffectText(75, pl.CSteamID, true, "MONEY", $"<B><color=#9884E2>{Uconomy.Instance.Database.GetBalance(pl.Id)}");
                        EffectManager.sendUIEffectText(75, pls.CSteamID, true, "MONEY", $"<B><color=#9884E2>{Uconomy.Instance.Database.GetBalance(pls.Id)}");

                    }
                    else
                    {
                        ChatManager.serverSendMessage($"<color=green>Başarılı |</color> Başarılı Şekilde Oyuncuya Para Gönderildi.", Color.white, null, pl.SteamPlayer(), EChatMode.SAY, logo, true);
                        ChatManager.serverSendMessage($"<color=green>Başarılı |</color> {pl.CharacterName} Adlı Oyuncu Tarafından Para Geldi Sana.", Color.white, null, pls.SteamPlayer(), EChatMode.SAY, logo, true);

                        pls.Experience += (uint)Math.Min(fiyat, uint.MaxValue);
                        EffectManager.sendUIEffectText(75, pl.CSteamID, true, "MONEY", $" <B><color=#9884E2>{pl.Experience}");
                        EffectManager.sendUIEffectText(75, pls.CSteamID, true, "MONEY", $" <B><color=#9884E2>{pls.Experience}");

                    }
                }


          

            }
            else
            {
                ChatManager.serverSendMessage($"<color=red>Dikkat |</color> Yanlış Kullanım.", Color.white, null, pl.SteamPlayer(), EChatMode.SAY, logo, true);

            }

        }
        [RocketCommand("paragönder", "para gönderirsiniz", "/paragönder <isim> <sebeb>", AllowedCaller.Both)]
        [RocketCommandPermission("r.paragönder")]
        public void ParaYolla(IRocketPlayer Caller, String[] Command)
        {
            string logo = Configuration.Instance.Logo;
            UnturnedPlayer pl = Caller as UnturnedPlayer;
            UnturnedPlayer pls = UnturnedPlayer.FromName(Command[0]);
            var fiyat = Convert.ToUInt32(Command[1]);


            if (Command.Count() != 0)
            {
                if (fiyat == 0)
                {
                    ChatManager.serverSendMessage($"<color=red>Dikkat |</color> Geçersiz Miktar.", Color.white, null, pl.SteamPlayer(), EChatMode.SAY, logo, true);
                    return;
                }
                if (pls == null)
                {
                    ChatManager.serverSendMessage($"<color=red>Dikkat |</color> Oyuncu Bulunamadı.", Color.white, null, pl.SteamPlayer(), EChatMode.SAY, logo, true);
                    return;
                }


                if (pl.Experience >= fiyat)
                {
                    if (Configuration.Instance.Uconomy)
                    {
                        ChatManager.serverSendMessage($"<color=green>Başarılı |</color> <color=orange>{pls.CharacterName}</color> Adlı Oyuncuya <color=orange>{fiyat}</color> Para Yolladın.", Color.white, null, pl.SteamPlayer(), EChatMode.SAY, logo, true);
                        ChatManager.serverSendMessage($"<color=green>Başarılı |</color> <color=orange>{pls.CharacterName}</color> Tarafından Size <color=orange>{fiyat}</color> Para Geldi.", Color.white, null, pl.SteamPlayer(), EChatMode.SAY, logo, true);

                        Uconomy.Instance.Database.IncreaseBalance(pl.Id, -fiyat);
                        Uconomy.Instance.Database.IncreaseBalance(pls.Id, fiyat);

                        EffectManager.sendUIEffectText(75, pl.CSteamID, true, "MONEY", $"<B><color=#9884E2>{Uconomy.Instance.Database.GetBalance(pl.Id)}");
                        EffectManager.sendUIEffectText(75, pls.CSteamID, true, "MONEY", $"<B><color=#9884E2>{Uconomy.Instance.Database.GetBalance(pls.Id)}");

                    }
                    else
                    {
                        ChatManager.serverSendMessage($"<color=green>Başarılı |</color> <color=orange>{pls.CharacterName}</color> Adlı Oyuncuya  <color=orange>{fiyat}</color> Para Yolladın.", Color.white, null, pl.SteamPlayer(), EChatMode.SAY, logo, true);
                        ChatManager.serverSendMessage($"<color=green>Başarılı |</color> <color=orange>{pls.CharacterName}</color> Tarafından Size <color=orange>{fiyat}</color> Para Geldi.", Color.white, null, pl.SteamPlayer(), EChatMode.SAY, logo, true);

                        pl.Experience  -= fiyat;
                        pls.Experience += fiyat;

                        EffectManager.sendUIEffectText(75, pl.CSteamID, true, "MONEY", $" <B><color=#9884E2>{pl.Experience}");
                        EffectManager.sendUIEffectText(75, pls.CSteamID, true, "MONEY", $" <B><color=#9884E2>{pls.Experience}");
                    }

                }
                else
                {
                    ChatManager.serverSendMessage($"<color=red>Dikkat |</color> O Kadar Paran Bulunmamakta.", Color.white, null, pl.SteamPlayer(), EChatMode.SAY, logo, true);
                }

            }
            else
            {
                ChatManager.serverSendMessage($"<color=red>Dikkat |</color> Yanlış Kullanım.", Color.white, null, pl.SteamPlayer(), EChatMode.SAY, logo, true);

            }

        }
    }
}




