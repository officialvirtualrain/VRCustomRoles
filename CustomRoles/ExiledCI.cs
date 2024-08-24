using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features.Items;
using Exiled.API.Features.Spawn;
using PlayerRoles;
using PlHandlers = Exiled.Events.Handlers.Player;
using MEC;
using UnityEngine;

namespace VRCustomRoles
{
    public class ExiledCI : CustomRole
    {
        public override uint Id { get; set; } = 1;
        public override RoleTypeId Role { get; set; } = RoleTypeId.ChaosRepressor;
        public override int MaxHealth { get; set; } = 175;
        public override string Name { get; set; } = "Изгой";
        public override string Description { get; set; } = "";
        public override string CustomInfo { get; set; } = "Ч\u0338\u030c\u0305\u032c\u031dт\u0336\u0310\u0359\u033b\u0332о\u0334\u0307\u0350\u032a\u034d \u0338\u0352\u0310\u0320о\u0335\u034b\u0315\u0360\u034e\u034e\u0317н\u0334\u0358\u033f\u031a\u0324\u032a\u0359 \u0334\u0344\u0341\u0327з\u0338\u0342\u030d\u0346\u0323\u0319\u034eд\u0337\u0303\u0314\u031c\u0325е\u0336\u033f\u0300\u0320\u035c\u0349с\u0338\u0310\u030c\u034d\u0348\u0339ь\u0338\u030e\u031b\u031e \u0338\u0309\u034c\u0312\u0325\u0316д\u0335\u0350\u031fе\u0335\u0344\u0350\u034a\u0331\u035c\u032dл\u0336\u0340\u035d\u032f\u033c\u034eа\u0336\u035d\u0345е\u0334\u0352\u0343\u0302\u0356т\u0338\u030f\u0344\u033c\u0316\u033c?\u0335\u0340\u0357\u0351\u034d\u0323?\u0338\u0315\u033b?\u0336\u0344\u0352\u032d\u035a\u034e";
        public override float SpawnChance { get; set; } = 5f;

        public Vector3 PlayerDeathLocation;

        public Exiled.API.Features.Player Pl;

        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties
        {
            Limit = 1,
            DynamicSpawnPoints = new List<DynamicSpawnPoint>
            {
                new()
                {
                    Location = SpawnLocationType.InsideGateA,
                    Chance = 100,
                }
            }
        };


        public override List<string> Inventory { get; set; } = new()
        {
            $"{ItemType.GunE11SR}",
            $"{ItemType.KeycardChaosInsurgency}",
            $"{ItemType.GrenadeFlash}",
            $"{ItemType.Radio}",
            $"{ItemType.Medkit}",
            $"{ItemType.ArmorCombat}"
        };

        protected override void SubscribeEvents()
        {
            PlHandlers.Spawned += OnSpawned;
            PlHandlers.Died += OnDied;
            PlHandlers.Hurting += OnHurting;
            PlHandlers.Dying += OnDying;
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            PlHandlers.Spawned -= OnSpawned;
            PlHandlers.Died -= OnDied;
            PlHandlers.Hurting -= OnHurting;
            PlHandlers.Dying -= OnDying;

            base.UnsubscribeEvents();
        }

        public void OnDying(DyingEventArgs ev)
        {
            PlayerDeathLocation = ev.Player.Position;
        }

        public void OnHurting(HurtingEventArgs ev)
        {
            if (!Check(ev.Player)) return;
            if (ev.Player.Health - ev.Amount <= 0 && ev.Amount < 100)
            {
                ev.Amount = 0;
                ev.Player.IsGodModeEnabled = true;
                Timing.CallDelayed(5f, () =>
                {
                    ev.Player.IsGodModeEnabled = false;
                    ev.Player.Kill(ev.DamageHandler);
                });

            }
            else if (ev.Amount >= 100)
            {
                ev.Player.Kill(ev.DamageHandler);
            }
                
                
        }

        public void OnDied(DiedEventArgs ev)
        {
            if (ev.Player == Pl) {
                ExplosiveGrenade gr = (ExplosiveGrenade)Item.Create(ItemType.GrenadeHE);
                gr.FuseTime = 0.4f;
                gr.SpawnActive(PlayerDeathLocation, ev.Player);
            }
        }

        public void OnSpawned(SpawnedEventArgs ev)
        {
            if (!Check(ev.Player)) return;
            Pl = ev.Player;
            Timing.RunCoroutine(Regenerate(ev.Player, 2, 4.5f));
        }

        public IEnumerator<float> Regenerate(Exiled.API.Features.Player player, ushort amount, float interval)
        {
            while (true)
            {
                player.Heal(amount, false);
                yield return Timing.WaitForSeconds(interval);
            }
        }
    }
}