using Exiled.API.Features;
using Exiled.CustomRoles.API;
using Exiled.CustomRoles.API.Features;

namespace VRCustomRoles
{
    public sealed class Plugin : Plugin<Config>
    {
        public override string Author => "VR";

        public override string Name => "VRCustomRoles";

        public override string Prefix => Name;
        
        public ExiledCI ExiledCI { get; set; } = new();
        
        public SeniorResearcher SeniorResearcher { get; set; } = new();

        public GuardCorporal GuardCorporal { get; set; } = new();
        
        public HeadOfTheScienceDepartment HeadOfTheScienceDepartment { get; set; } = new();
        
        public O5 O5 { get; set; } = new();

        public Shahid Shahid { get; set; } = new();
        
        public Janitor Janitor { get; set; } = new();
        
        public static Plugin Instance;
        
        public override void OnEnabled()
        {
            // RegisterEvents();

            Instance = this;
            
            ExiledCI.Register();
            SeniorResearcher.Register();
            GuardCorporal.Register();
            HeadOfTheScienceDepartment.Register();
            O5.Register();
            Janitor.Register();
            Shahid.Register();


            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            // UnregisterEvents();

            Instance = null;
            
            CustomRole.UnregisterRoles();

            base.OnDisabled();
        }

       /* private void RegisterEvents()
        {
            _handlers = new EventHandlers();
        }

        private void UnregisterEvents()
        {
            _handlers = null;
        }*/
    }
}