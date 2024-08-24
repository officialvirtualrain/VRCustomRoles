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
    public class Shahid : CustomRole
    {
        public override uint Id { get; set; } = 7;
        public override RoleTypeId Role { get; set; } = RoleTypeId.ClassD;
        public override int MaxHealth { get; set; } = 100;
        public override string Name { get; set; } = "Shahed-36";
        public override string Description { get; set; } = "";
        public override string CustomInfo { get; set; } = "";
        public override float SpawnChance { get; set; } = 10f;

        public Vector3 PlayerDeathLocation;

        public Exiled.API.Features.Player Pl;

        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties
        {
            Limit = 1,
            DynamicSpawnPoints = new List<DynamicSpawnPoint>
            {
                new()
                {
                    Location = SpawnLocationType.InsideGr18,
                    Chance = 100,
                }
            }
        };



        public override List<string> Inventory { get; set; } = new()
        {
            $"{ItemType.GrenadeHE}",
            $"{ItemType.GrenadeHE}",
            $"{ItemType.GrenadeHE}",
            $"{ItemType.SCP018}",
            $"{ItemType.GrenadeFlash}",
            $"{ItemType.GrenadeFlash}"
        };

        protected override void SubscribeEvents()
        {
            PlHandlers.Spawning += OnSpawning;
            PlHandlers.Died += OnDied;
            PlHandlers.Dying += OnDying;
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            PlHandlers.Spawning -= OnSpawning;
            PlHandlers.Died -= OnDied;
            PlHandlers.Dying -= OnDying;

            base.UnsubscribeEvents();
        }

        public void OnDying(DyingEventArgs ev)
        {
            if (!Check(ev.Player)) return;

            PlayerDeathLocation = ev.Player.Position;
        }


        public void OnDied(DiedEventArgs ev)
        {
            if (ev.Player == Pl) {
                ExplosiveGrenade gr = (ExplosiveGrenade)Item.Create(ItemType.GrenadeHE);
                gr.FuseTime = 4f;
                gr.SpawnActive(PlayerDeathLocation, ev.Player);
            }
        }

        public void OnSpawning(SpawningEventArgs ev)
        {
            if (!Check(ev.Player)) return;
            Pl = ev.Player;
        }

    }
}