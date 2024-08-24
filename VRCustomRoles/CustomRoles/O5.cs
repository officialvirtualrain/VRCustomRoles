using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features.Items;
using Exiled.API.Features.Spawn;
using Exiled.Events.EventArgs.Warhead;
using Exiled.Permissions.Commands.Permissions;
using MapGeneration;
using PlayerRoles;
using PlHandlers = Exiled.Events.Handlers.Player;
using WarheadHandlers = Exiled.Events.Handlers.Warhead;

using MEC;
using UnityEngine;

namespace VRCustomRoles
{
    public class O5 : CustomRole
    {
        public override uint Id { get; set; } = 6;
        public override RoleTypeId Role { get; set; } = RoleTypeId.NtfCaptain;
        public override int MaxHealth { get; set; } = 200;
        public override string Name { get; set; } = "Советник O5";
        public override string Description { get; set; } = "";
        public override string CustomInfo { get; set; } = "Член совета O5";
        public override float SpawnChance { get; set; } = 5f;

        public Vector3 PlayerDeathLocation;

        public Exiled.API.Features.Player Pl;

        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties
        {
            Limit = 1,
            RoleSpawnPoints = new List<RoleSpawnPoint>
            {
                new()
                {
                    Role = RoleTypeId.FacilityGuard,
                    Chance = 100,

                }
            }
        };


        public override List<string> Inventory { get; set; } = new()
        {
            $"{ItemType.KeycardO5}",
            $"{ItemType.GunFRMG0}",
            $"{ItemType.SCP500}",
            $"{ItemType.SCP268}",
            $"{ItemType.Coin}",
            $"{ItemType.ArmorHeavy}",
        };

        protected override void SubscribeEvents()
        {
            PlHandlers.Spawned += OnSpawned;
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            PlHandlers.Spawned -= OnSpawned;


            base.UnsubscribeEvents();
        }

        private void OnSpawned(SpawnedEventArgs ev)
        {
            if (!Check(ev.Player)) return;
            Pl = ev.Player;
        }


    }
}