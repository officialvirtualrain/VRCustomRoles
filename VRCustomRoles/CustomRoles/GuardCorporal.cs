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
    public class GuardCorporal : CustomRole
    {
        public override uint Id { get; set; } = 4;
        public override RoleTypeId Role { get; set; } = RoleTypeId.FacilityGuard;
        public override int MaxHealth { get; set; } = 120;
        public override string Name { get; set; } = "Офицер Охраны";
        public override string Description { get; set; } = "";
        public override string CustomInfo { get; set; } = "Офицер Охраны";
        public override float SpawnChance { get; set; } = 25f;
        public override Exiled.API.Features.Broadcast Broadcast { get; set; } = new Exiled.API.Features.Broadcast("<size=50><b><color=#03fc90>Ты стал </color><color=#4b4f4c>Офицером Охраны</b></color></size>\\n<size=25></size>", 10);

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
            $"{ItemType.ArmorCombat}",
            $"{ItemType.Radio}",
            $"{ItemType.GunCOM18}",
            $"{ItemType.Medkit}",
            $"{ItemType.Medkit}",
            $"{ItemType.GunE11SR}",
            $"{ItemType.KeycardMTFPrivate}",

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