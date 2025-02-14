﻿
using System.Diagnostics;

namespace HyperElk.Core
{
    public class MMHunter : CombatRoutine
    {
        private readonly Stopwatch Trueshot_active = new Stopwatch();
        private readonly Stopwatch VolleyWindow = new Stopwatch();
        private readonly Stopwatch CallPetTimer = new Stopwatch();

        private bool IsMouseover => API.ToggleIsEnabled("Mouseover");
        private bool IsFocus => API.ToggleIsEnabled("Focus");
        //Spells,Buffs,Debuffs
        private string Steady_Shot = "Steady Shot";
        private string Arcane_Shot = "Arcane Shot";

        private string Aimed_Shot = "Aimed Shot";
        private string Trueshot = "Trueshot";
        private string Rapid_Fire = "Rapid Fire";
        private string Bursting_Shot = "Bursting Shot";


        private string Mend_Pet = "Mend Pet";
        private string Kill_Shot = "Kill Shot";
        private string Multi_Shot = "Multi-Shot";
        private string Misdirection = "Misdirection";


        private string Exhilaration = "Exhilaration";
        private string Survival_of_the_Fittest = "Survival of the Fittest";
        private string Feign_Death = "Feign Death";
        private string Counter_Shot = "Counter Shot";

        private string Double_Tap = "Double Tap";
        private string Chimaera_Shot = "Chimaera Shot";
        private string A_Murder_of_Crows = "A Murder of Crows";
        private string Barrage = "Barrage";
        private string Volley = "Volley";
        private string Explosive_Shot = "Explosive Shot";
        private string Serpent_Sting = "Serpent Sting";
        private string Wild_Spirits = "Wild Spirits";
        private string Resonating_Arrow = "Resonating Arrow";
        private string Flayed_Shot = "Flayed Shot";
        private string Death_Chakram = "Death Chakram";
        private string Revive_Pet = "Revive Pet";

        private string Aspect_of_the_Turtle = "Aspect of the Turtle";
        private string Precise_Shots = "Precise Shots";
        private string Trick_Shots = "Trick Shots";
        private string Steady_Focus = "Steady Focus";
        private string Lock_and_Load = "Lock and Load";
        private string Dead_Eye = "Dead Eye";
        private string FlayersMark = "Flayer's Mark";
        private string Wild_Mark = "Wild Mark";
        private string HuntersMark = "Hunter's Mark";
        private string TranquilizingShot = "Tranquilizing Shot";
        private string ConcussiveShot = "Concussive Shot";

        private string PhialofSerenity = "Phial of Serenity";
        private string SpiritualHealingPotion = "Spiritual Healing Potion";
        //Misc
        private int PlayerLevel => API.PlayerLevel;
        private bool InRange => API.TargetRange <= 43;
        private bool isMOinRange => API.MouseoverRange <= 40;
        public bool isMouseoverInCombat => CombatRoutine.GetPropertyBool("MouseoverInCombat");
        public bool NoCovReady => (API.SpellCDDuration(Wild_Spirits) >= gcd || API.SpellCDDuration(Wild_Spirits) == 0) && (API.SpellCDDuration(Flayed_Shot) >= gcd || API.SpellCDDuration(Flayed_Shot) == 0) && (API.SpellCDDuration(Death_Chakram) >= gcd || API.SpellCDDuration(Death_Chakram) == 0) && (API.SpellCDDuration(Resonating_Arrow) >= gcd || API.SpellCDDuration(Death_Chakram) == 0);
        public bool DispellList => API.TargetHasBuff("Enrage") || API.TargetHasBuff("Undying Rage") || API.TargetHasBuff("Raging") || API.TargetHasBuff("Unholy Frenzy") || API.TargetHasBuff("Renew") || API.TargetHasBuff("Additional Treads") || API.TargetHasBuff("Slime Coated") || API.TargetHasBuff("Stimulate Resistance") || API.TargetHasBuff("Unholy Fervor") || API.TargetHasBuff("Raging Tantrum") || API.TargetHasBuff("Loyal Beasts") || API.TargetHasBuff("Motivational Clubbing") || API.TargetHasBuff("Forsworn Doctrine") || API.TargetHasBuff("Seething Rage") || API.TargetHasBuff("Dark Shroud");
        //Talents
        private bool Talent_A_Murder_of_Crows => API.PlayerIsTalentSelected(1, 3);
        private bool Talent_Serpent_Sting => API.PlayerIsTalentSelected(1, 2);
        private bool Talent_CarefulAim => API.PlayerIsTalentSelected(2, 1);
        private bool Talent_Barrage => API.PlayerIsTalentSelected(2, 2);
        private bool Talent_Explosive_Shot => API.PlayerIsTalentSelected(2, 3);
        private bool Talent_Streamline => API.PlayerIsTalentSelected(4, 2);
        private bool Talent_Chimaera_Shot => API.PlayerIsTalentSelected(4, 3);
        private bool Talent_Steady_Focus => API.PlayerIsTalentSelected(4, 1);
        private bool Talent_Dead_Eye => API.PlayerIsTalentSelected(6, 2);
        private bool Talent_Double_Tap => API.PlayerIsTalentSelected(6, 3);
        private bool Talent_Volley => API.PlayerIsTalentSelected(7, 3);



        //CBProperties
        string[] MisdirectionList = new string[] { "Off", "On AOE", "On" };
        string[] TrueshotList = new string[] { "always", "with Cooldowns" };
        string[] DoubleTapList = new string[] { "always", "with Cooldowns" };
        string[] AMurderofCrowsList = new string[] { "always", "with Cooldowns" };
        string[] BloodshedList = new string[] { "always", "with Cooldowns" };
        string[] TrinketList = new string[] { "Something Else", "Dreadfire Vessel" };
        int[] numbList = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100 };

        string[] LegendaryList = new string[] { "always", "with Cooldowns" };



        private int Survival_of_the_FittestLifePercent => percentListProp[CombatRoutine.GetPropertyInt(Survival_of_the_Fittest)];
        private int ExhilarationLifePercent => percentListProp[CombatRoutine.GetPropertyInt(Exhilaration)];
        private int PetExhilarationLifePercent => percentListProp[CombatRoutine.GetPropertyInt(Exhilaration + "PET")];
        private int AspectoftheTurtleLifePercent => percentListProp[CombatRoutine.GetPropertyInt(Aspect_of_the_Turtle)];
        private int FeignDeathLifePercent => percentListProp[CombatRoutine.GetPropertyInt(Feign_Death)];
        private int MendPetLifePercent => percentListProp[CombatRoutine.GetPropertyInt(Mend_Pet)];
        private int FocusCounterShotPercent => numbList[CombatRoutine.GetPropertyInt("CounterShotFocus")];
        private string UseMisdirection => MisdirectionList[CombatRoutine.GetPropertyInt(Misdirection)];
        private string UseDoubleTap => DoubleTapList[CombatRoutine.GetPropertyInt(Double_Tap)];
        private string UseTrueshot => TrueshotList[CombatRoutine.GetPropertyInt(Trueshot)];
        private string UseExplosiveShot => CDUsageWithAOE[CombatRoutine.GetPropertyInt(Explosive_Shot)];
        private string UseAMurderofCrows => AMurderofCrowsList[CombatRoutine.GetPropertyInt(A_Murder_of_Crows)];
        private string UseVolley => CDUsageWithAOE[CombatRoutine.GetPropertyInt(Volley)];
        private string EquippedTrinket1 => TrinketList[CombatRoutine.GetPropertyInt("EquippedTrinket1")];
        private string EquippedTrinket2 => TrinketList[CombatRoutine.GetPropertyInt("EquippedTrinket2")];
        private bool UseCallPet => CombatRoutine.GetPropertyBool("CallPet");
        private bool UseCleaveRotation => CombatRoutine.GetPropertyBool("Cleave");
        private bool Use_HuntersMark => CombatRoutine.GetPropertyBool("huntersmark");
        private bool SurgingShots_enabled => CombatRoutine.GetPropertyBool("SurgingShots");
        private bool UseTranqShot => CombatRoutine.GetPropertyBool("TranquilizingShot");
        private bool eagletalons_true_focus_enabled => CombatRoutine.GetPropertyBool("eagletalons_true_focus");
        private bool AOESwitch_enabled => CombatRoutine.GetPropertyBool("AOE_Switch");
        private string UseTrinket1 => CDUsageWithAOE[CombatRoutine.GetPropertyInt("Trinket1")];
        private string UseTrinket2 => CDUsageWithAOE[CombatRoutine.GetPropertyInt("Trinket2")];
        private string UseCovenant => CDUsageWithAOE[CombatRoutine.GetPropertyInt("UseCovenant")];
        private int PhialofSerenityLifePercent => numbList[CombatRoutine.GetPropertyInt(PhialofSerenity)];
        private int SpiritualHealingPotionLifePercent => numbList[CombatRoutine.GetPropertyInt(SpiritualHealingPotion)];
        private bool ConcussiveShot_enabled => CombatRoutine.GetPropertyBool(ConcussiveShot);
        private bool KickAlways => CombatRoutine.GetPropertyBool("alwayskick");


        private bool CanuseASinST => API.CanCast(Aimed_Shot) && InRange && (PlayerHasBuff(Lock_and_Load) || !API.PlayerIsMoving) && API.PlayerFocus >= (PlayerHasBuff(Lock_and_Load) ? 0 : 35) && API.PlayerCurrentCastSpellID != 19434 && (API.TargetDebuffRemainingTime(Serpent_Sting) > 200 || !Talent_Serpent_Sting) && (!PlayerHasBuff(Precise_Shots) || (PlayerHasBuff(Trueshot) || FullRechargeTime(Aimed_Shot, AimedShotCooldown) < gcd + AimedShotCastTime) && (!Talent_Chimaera_Shot || (!UseCleaveRotation && API.TargetUnitInRangeCount < 2)) || API.PlayerBuffTimeRemaining(Trick_Shots) > AimedShotCastTime && (UseCleaveRotation || API.TargetUnitInRangeCount > 1));
        private bool CanuseArcaneinST => (API.CanCast(Arcane_Shot) && (API.SpellCDDuration(Rapid_Fire) > gcd || PlayerHasBuff(Double_Tap)) && InRange && (PlayerHasBuff(Precise_Shots) || API.PlayerFocus > 20 + (PlayerHasBuff(Lock_and_Load) ? 0 : 35)));

        private bool LastSpell(string spellname, int spellid)
        {
            return API.LastSpellCastInGame == spellname || API.PlayerCurrentCastSpellID == spellid;
        }
        private float FocusRegen => (10f * (1f + API.PlayerGetHaste)) * (PlayerHasBuff(Trueshot) ? 15 / 10 : 1);
        private float FocusTimeToMax => (API.PlayerMaxFocus - API.PlayerFocus) * 100f / FocusRegen;
        private float AimedShotCastTime => ((250f / (1f + (API.PlayerGetHaste))) / (PlayerHasBuff(Trueshot) ? 2 : 1) * (PlayerHasBuff(Lock_and_Load) ? 0 : 1));
        private float RapidFireChannelTime => 200f / (1f + (API.PlayerGetHaste));
        private float SteadyShot_CastTime => 175f / (1f + (API.PlayerGetHaste));
        private float gcd => API.SpellGCDTotalDuration;
        private bool Playeriscasting => API.PlayerCurrentCastTimeRemaining > 40;
        private bool VolleyTrickShots => (Talent_Volley && VolleyWindow.IsRunning);
        private static bool PlayerHasBuff(string buff)
        {
            return API.PlayerHasBuff(buff, false, false);
        }
        private bool Race(string race)
        {
            return API.PlayerRaceName == race && PlayerRaceSettings == race;
        }
        private bool ca_active => (Talent_CarefulAim ? true : false) && (API.TargetHealthPercent > 70 ? true : false);

        private float AimedShotCooldown => (1200f / (1f + (API.PlayerGetHaste))) / (PlayerHasBuff(Trueshot) ? 22 / 10 : 1);
        private float FullRechargeTime(string spellname, float spellcooldown_max)
        {
            return (API.SpellMaxCharges(spellname) - API.SpellCharges(spellname)) * spellcooldown_max + API.SpellCDDuration(spellname);
        }

        public override void Initialize()
        {
            CombatRoutine.Name = "Marksman Hunter by Vec";
            API.WriteLog("Welcome to Marksman Hunter Rotation");
            API.WriteLog("Misdirection Macro : /cast [@focus,help][help][@pet,exists] Misdirection");
            API.WriteLog("Mend Pet Macro (revive/call): /cast [mod]Revive Pet; [@pet,dead]Revive Pet; [nopet]Call Pet 1; Mend Pet");
            API.WriteLog("Kill Shot Mouseover - /cast [@mouseover] Kill Shot");


            //Spells
            CombatRoutine.AddSpell(Steady_Shot, 56641, "D1");
            CombatRoutine.AddSpell(Arcane_Shot, 185358, "D2");
            CombatRoutine.AddSpell(Aimed_Shot, 19434, "D3");
            CombatRoutine.AddSpell(Trueshot, 288613, "Q");
            CombatRoutine.AddSpell(Rapid_Fire, 257044, "Q");
            CombatRoutine.AddSpell(Bursting_Shot, 186387, "D7");

            CombatRoutine.AddSpell(Kill_Shot, 53351, "D5");
            CombatRoutine.AddSpell(Multi_Shot, 257620, "D6");

            CombatRoutine.AddSpell(Counter_Shot, 147362, "F");
            CombatRoutine.AddSpell(Exhilaration, 109304, "F9");
            CombatRoutine.AddSpell(Survival_of_the_Fittest, 281195, "F8");
            CombatRoutine.AddSpell(Misdirection, 34477, "D4");

            CombatRoutine.AddSpell(Double_Tap, 260402, "D1");
            CombatRoutine.AddSpell(Chimaera_Shot, 342049, "D1");
            CombatRoutine.AddSpell(A_Murder_of_Crows, 131894, "D1");
            CombatRoutine.AddSpell(Barrage, 120360, "D1");
            CombatRoutine.AddSpell(Volley, 260243, "D1");
            CombatRoutine.AddSpell(Explosive_Shot, 212431, "D1");
            CombatRoutine.AddSpell(Serpent_Sting, 271788, "D1");
            CombatRoutine.AddSpell(Feign_Death, 5384, "F2");
            CombatRoutine.AddSpell(Aspect_of_the_Turtle, 186265, "G");
            CombatRoutine.AddSpell(TranquilizingShot, 19801, "C");
            CombatRoutine.AddSpell(Mend_Pet, 136, "F5");
            CombatRoutine.AddSpell(Revive_Pet, 982, "F5");

            CombatRoutine.AddSpell(Wild_Spirits, 328231, "F10");
            CombatRoutine.AddSpell(Resonating_Arrow, 308491, "F10");
            CombatRoutine.AddSpell(Flayed_Shot, 324149, "F10");
            CombatRoutine.AddSpell(Death_Chakram, 325028, "F10");
            CombatRoutine.AddSpell(HuntersMark, 257284, "F11");
            CombatRoutine.AddSpell(ConcussiveShot, 5116, "F12");

            CombatRoutine.AddMacro("Trinket1", "F9");
            CombatRoutine.AddMacro("Trinket2", "F10");
            //Buffs

            CombatRoutine.AddBuff(Aspect_of_the_Turtle, 186265);
            CombatRoutine.AddBuff(Feign_Death, 5384);
            CombatRoutine.AddBuff(Misdirection, 34477);
            CombatRoutine.AddBuff(Precise_Shots, 260242);
            CombatRoutine.AddBuff(Trick_Shots, 257622);
            CombatRoutine.AddBuff(Steady_Focus, 193534);
            CombatRoutine.AddBuff(Trueshot, 288613);
            CombatRoutine.AddBuff(Double_Tap, 260402);
            CombatRoutine.AddBuff(Lock_and_Load, 194594);
            CombatRoutine.AddBuff(Dead_Eye, 321460);
            CombatRoutine.AddBuff(FlayersMark, 324156);
            CombatRoutine.AddBuff(Volley, 260243);
            //dispell
            CombatRoutine.AddBuff("Raging", 228318);
            CombatRoutine.AddBuff("Unholy Frenzy", 320012);
            CombatRoutine.AddBuff("Renew", 135953);
            CombatRoutine.AddBuff("Additional Treads", 965900);
            CombatRoutine.AddBuff("Slime Coated", 3459153);
            CombatRoutine.AddBuff("Stimulate Resistance", 1769069);
            CombatRoutine.AddBuff("Stimulate Regeneration", 136079);
            CombatRoutine.AddBuff("Unholy Fervor", 2576093);
            CombatRoutine.AddBuff("Loyal Beasts", 326450);
            CombatRoutine.AddBuff("Motivational Clubbing", 3554193);
            CombatRoutine.AddBuff("Forsworn Doctrine", 3528444);
            CombatRoutine.AddBuff("Dark Shroud", 2576096);
            CombatRoutine.AddBuff("Undying Rage", 333227);
            CombatRoutine.AddBuff("Enrage", 324085);
            CombatRoutine.AddBuff("Raging Tantrum", 333241);
            CombatRoutine.AddBuff("Seething Rage", 320703);
            //Debuffs

            CombatRoutine.AddDebuff(Serpent_Sting, 271788);
            CombatRoutine.AddDebuff(ConcussiveShot, 5116);
            CombatRoutine.AddDebuff(Wild_Mark, 328275);
            CombatRoutine.AddDebuff(Resonating_Arrow, 308491);
            CombatRoutine.AddDebuff(HuntersMark, 257284);

            //Macros
            CombatRoutine.AddMacro(Kill_Shot + "MO", "NumPad7");
            CombatRoutine.AddMacro("Call Pet", "H");
            CombatRoutine.AddMacro(Counter_Shot + " Focus", "NumPad8");
            //items
            CombatRoutine.AddItem(PhialofSerenity, 177278);
            CombatRoutine.AddItem(SpiritualHealingPotion, 171267);

            //Toggle
            CombatRoutine.AddToggle("Mouseover");
            CombatRoutine.AddToggle("Focus");
            AddProp("MouseoverInCombat", "Only Mouseover in combat", true, "Only Attack mouseover in combat to avoid stupid pulls", "Generic");

            //Settings
            CombatRoutine.AddProp(Survival_of_the_Fittest, "Use " + Survival_of_the_Fittest + " below:", percentListProp, "Life percent at which " + Survival_of_the_Fittest + " is used, set to 0 to disable", "Defense", 7);
            CombatRoutine.AddProp(Misdirection, "Use Misdirection", MisdirectionList, "Use " + Misdirection + " Off, On AOE, On", "Generic", 0);
            CombatRoutine.AddProp(Trueshot, "Use " + Trueshot, TrueshotList, "Use " + Trueshot + " always, with Cooldowns", "Cooldowns", 0);
            CombatRoutine.AddProp(Double_Tap, "Use " + Double_Tap, DoubleTapList, "Use " + Double_Tap + " always, with Cooldowns", "Cooldowns", 0);
            CombatRoutine.AddProp(A_Murder_of_Crows, "Use " + A_Murder_of_Crows, AMurderofCrowsList, "Use " + A_Murder_of_Crows + " always, with Cooldowns", "Cooldowns", 0);
            CombatRoutine.AddProp(Volley, "Use " + Volley, CDUsageWithAOE, "Use " + Volley + " always, with Cooldowns, On AOE, never", "Cooldowns", 0);
            CombatRoutine.AddProp(Explosive_Shot, "Use " + Explosive_Shot, CDUsageWithAOE, "Use " + Explosive_Shot + " always, with Cooldowns, On AOE, never", "Cooldowns", 0);
            CombatRoutine.AddProp("EquippedTrinket1", "Equipped Trinket 1", TrinketList, "Select your Equipped Trinket", "Trinkets", 0);
            CombatRoutine.AddProp("EquippedTrinket2", "Equipped Trinket 2", TrinketList, "Select your Equipped Trinket", "Trinkets", 0);
            CombatRoutine.AddProp("AOE_Switch", "AoE Switch", true, "Enable if you want to let the rotation switch ST/AOE", "Generic");
            CombatRoutine.AddProp("Cleave", "Cleave rotation", false, "Enable if you want to test the cleave rotation", "Generic");
            CombatRoutine.AddProp("huntersmark", "Hunter's Mark", false, "Enable if you want to let the rotation use Hunter's Mark", "Generic");
            CombatRoutine.AddProp(PhialofSerenity, PhialofSerenity + " Life Percent", numbList, " Life percent at which" + PhialofSerenity + " is used, set to 0 to disable", "Defense", 40);
            CombatRoutine.AddProp(SpiritualHealingPotion, SpiritualHealingPotion + " Life Percent", numbList, " Life percent at which" + SpiritualHealingPotion + " is used, set to 0 to disable", "Defense", 40);
            CombatRoutine.AddProp("SurgingShots", "Surging Shots", false, "Enable if you have Surging Shots", "Legendary");
            CombatRoutine.AddProp(ConcussiveShot, ConcussiveShot, false, "Enable if you want to use ConcussiveShot", "Misc");
            CombatRoutine.AddProp("CounterShotFocus", "Use " + Counter_Shot + " at Focus:", numbList, "% cast at which " + Counter_Shot + " is used on focus", "Interrupt", 90);
            CombatRoutine.AddProp("eagletalons_true_focus", "eagletalons true focus", false, "Enable if you have eagletalons true focus", "Legendary");

            CombatRoutine.AddProp("TranquilizingShot", "Tranquilizing Shot", false, "Enable if you want to use Tranquilizing Shot", "Generic");
            CombatRoutine.AddProp("CallPet", "Call/Ressurect Pet", false, "Should the rotation try to ressurect/call your Pet", "Pet");
            CombatRoutine.AddProp("Trinket1", "Use " + "Trinket 1", CDUsageWithAOE, "Use " + "Trinket 1" + " always, with Cooldowns", "Trinkets", 0);
            CombatRoutine.AddProp("Trinket2", "Use " + "Trinket 2", CDUsageWithAOE, "Use " + "Trinket 2" + " always, with Cooldowns", "Trinkets", 0);
            CombatRoutine.AddProp(Exhilaration, "Use " + Exhilaration + " below:", percentListProp, "Life percent at which " + Exhilaration + " is used, set to 0 to disable", "Defense", 6);
            CombatRoutine.AddProp(Exhilaration + "PET", "Use " + Exhilaration + " below:", percentListProp, "Life percent at which " + Exhilaration + " is used to heal your pet, set to 0 to disable", "Pet", 2);
            CombatRoutine.AddProp(Aspect_of_the_Turtle, "Use " + Aspect_of_the_Turtle + " below:", percentListProp, "Life percent at which " + Aspect_of_the_Turtle + " is used, set to 0 to disable", "Defense", 6);
            CombatRoutine.AddProp(Feign_Death, "Use " + Feign_Death + " below:", percentListProp, "Life percent at which " + Feign_Death + " is used, set to 0 to disable", "Defense", 2);
            CombatRoutine.AddProp(Mend_Pet, "Use " + Mend_Pet + " below:", percentListProp, "Life percent at which " + Mend_Pet + " is used, set to 0 to disable", "Pet", 6);
            CombatRoutine.AddProp("UseCovenant", "Use " + "Covenant Ability", CDUsageWithAOE, "Use " + "Covenant" + " always, with Cooldowns", "Covenant", 0);
            CombatRoutine.AddProp("alwayskick", "Kick always", false, "Enable if you want to kick even if casting", "Interrupt");

        }

        public override void Pulse()
        {
               // API.WriteLog("debug: " + "mend pet: " + API.CanCast(Mend_Pet));
            if (CallPetTimer.ElapsedMilliseconds > 10000)
            {
                CallPetTimer.Stop();
                CallPetTimer.Reset();
            }
        }
        public override void CombatPulse()
        {
            if ((!API.PlayerHasPet || API.PetHealthPercent < 1) && CallPetTimer.ElapsedMilliseconds > gcd * 20 && UseCallPet && API.CanCast(Revive_Pet))
            {
                API.CastSpell(Revive_Pet);
                return;
            }

            if ((!API.PlayerHasPet || API.PetHealthPercent < 1) && (CallPetTimer.ElapsedMilliseconds <= gcd * 20 || !CallPetTimer.IsRunning) && UseCallPet)
            {
                API.CastSpell("Call Pet");
                CallPetTimer.Start();
                return;
            }
            if (API.CanCast(Mend_Pet) && API.PlayerHasPet && API.PetHealthPercent <= MendPetLifePercent && API.PetHealthPercent >= 1)
            {
                API.CastSpell(Mend_Pet);
                return;
            }
            if (API.CanCast(HuntersMark) && Use_HuntersMark && !API.TargetHasDebuff(HuntersMark) && InRange)
            {
                API.CastSpell(HuntersMark);
                return;
            }
            if (API.FocusHealthPercent > 0 && API.CanCast(Misdirection) && !PlayerHasBuff(Misdirection) && PlayerLevel >= 21 && (UseMisdirection == "On" || (UseMisdirection == "On AOE" & IsAOE && API.TargetUnitInRangeCount >= AOEUnitNumber)))
            {
                API.CastSpell(Misdirection);
                return;
            }
            if (API.PlayerItemCanUse(PhialofSerenity) && API.PlayerItemRemainingCD(PhialofSerenity) == 0 && API.PlayerHealthPercent <= PhialofSerenityLifePercent)
            {
                API.CastSpell(PhialofSerenity);
                return;
            }
            if (API.PlayerItemCanUse(SpiritualHealingPotion) && API.PlayerItemRemainingCD(SpiritualHealingPotion) == 0 && API.PlayerHealthPercent <= SpiritualHealingPotionLifePercent)
            {
                API.CastSpell(SpiritualHealingPotion);
                return;
            }
            if (API.CanCast(Exhilaration) && API.PlayerHealthPercent <= ExhilarationLifePercent && PlayerLevel >= 9)
            {
                API.CastSpell(Exhilaration);
                return;
            }
            if (API.CanCast(Survival_of_the_Fittest) && API.PlayerHealthPercent <= Survival_of_the_FittestLifePercent && PlayerLevel >= 9)
            {
                API.CastSpell(Survival_of_the_Fittest);
                return;
            }
            if (API.CanCast(Aspect_of_the_Turtle) && API.PlayerHealthPercent <= AspectoftheTurtleLifePercent && PlayerLevel >= 8)
            {
                API.CastSpell(Aspect_of_the_Turtle);
                return;
            }
            if (API.CanCast(Feign_Death) && API.PlayerHealthPercent <= FeignDeathLifePercent && PlayerLevel >= 6)
            {
                API.CastSpell(Feign_Death);
                return;
            }
            if (isInterrupt && KickAlways && API.CanCast(Counter_Shot) && InRange && PlayerLevel >= 18)
            {
                API.CastSpell(Counter_Shot);
                return;
            }
            if (!API.MacroIsIgnored(Counter_Shot + " Focus") && KickAlways && IsFocus && API.FocusCanInterrupted && API.FocusElapsedCastTimePercent >= FocusCounterShotPercent && API.FocusRange <= 40 && API.CanCast(Counter_Shot))
            {
                API.CastSpell(Counter_Shot + " Focus");
                return;
            }
            if (!Playeriscasting && !API.PlayerIsChanneling && !API.PlayerIsMounted && !PlayerHasBuff(Aspect_of_the_Turtle) && !PlayerHasBuff(Feign_Death) && !API.PlayerSpellonCursor)
            {
                if (isInterrupt && API.CanCast(Counter_Shot) && InRange && PlayerLevel >= 18)
                {
                    API.CastSpell(Counter_Shot);
                    return;
                }
                if (!API.MacroIsIgnored(Counter_Shot + " Focus") && IsFocus && API.FocusCanInterrupted && API.FocusElapsedCastTimePercent >= FocusCounterShotPercent && API.FocusRange <= 40 && API.CanCast(Counter_Shot))
                {
                    API.CastSpell(Counter_Shot + " Focus");
                    return;
                }
                if (API.CanCast(TranquilizingShot) && DispellList && UseTranqShot && InRange && PlayerLevel >= 18)
                {
                    API.CastSpell(TranquilizingShot);
                    return;
                }
                if (API.CanCast(ConcussiveShot) && ConcussiveShot_enabled && InRange && !API.TargetHasDebuff(ConcussiveShot))
                {
                    API.CastSpell(ConcussiveShot);
                    return;
                }
                rotation();
                return;
            }
        }

        public override void OutOfCombatPulse()
        {
        }


        private void rotation()
        {


            //API.WriteLog("opener time: " + API.PlayerTimeInCombat);
            if (!VolleyWindow.IsRunning && API.LastSpellCastInGame == Volley)
            {
                API.WriteLog("Volley window open" + " AS ready? " + API.CanCast(Aimed_Shot) + " RF ready? " + API.CanCast(Rapid_Fire));
                VolleyWindow.Start();
            }
            if (VolleyWindow.ElapsedMilliseconds >= 6000)
            {
                API.WriteLog("Volley window closed");
                VolleyWindow.Stop();
                VolleyWindow.Reset();
            }
            if (!Trueshot_active.IsRunning && API.LastSpellCastInGame == Trueshot)
            {
                API.WriteLog("Trueshot window open");
                Trueshot_active.Start();
            }
            if (Trueshot_active.ElapsedMilliseconds >= 15000)
            {
                API.WriteLog("Trueshot window closed");
                Trueshot_active.Stop();
                Trueshot_active.Reset();
            }







            // actions.cds = berserking,if= buff.trueshot.up | target.time_to_die < 13
            if (PlayerRaceSettings == "Troll" && API.CanCast(RacialSpell1) && isRacial && (PlayerHasBuff(Trueshot) || API.TargetTimeToDie < 1300))
            {
                API.CastSpell(RacialSpell1);
                return;
            }
            // actions.cds +=/ blood_fury,if= buff.trueshot.up | target.time_to_die < 16
            if (PlayerRaceSettings == "Orc" && API.CanCast(RacialSpell1) && isRacial && (PlayerHasBuff(Trueshot) || API.TargetTimeToDie < 1600))
            {
                API.CastSpell(RacialSpell1);
                return;
            }
            // actions.cds +=/ ancestral_call,if= buff.trueshot.up | target.time_to_die < 16
            if (PlayerRaceSettings == "Mag'har Orc" && API.CanCast(RacialSpell1) && (PlayerHasBuff(Trueshot) || API.TargetTimeToDie < 1600))
            {
                API.CastSpell(RacialSpell1);
                return;
            }
            // actions.cds +=/ fireblood,if= buff.trueshot.up | target.time_to_die < 9
            if (PlayerRaceSettings == "Dark Iron Dwarf" && API.CanCast(RacialSpell1) && (PlayerHasBuff(Trueshot) || API.TargetTimeToDie < 900))
            {
                API.CastSpell(RacialSpell1);
                return;
            }
            // actions.cds +=/ lights_judgment,if= buff.trueshot.down
            if (PlayerRaceSettings == "Lightforged" && API.CanCast(RacialSpell1) && !PlayerHasBuff(Trueshot))
            {
                API.CastSpell(RacialSpell1);
                return;
            }
            // actions.cds +=/ bag_of_tricks,if= buff.trueshot.down
            if (PlayerRaceSettings == "Vulpera" && API.CanCast(RacialSpell1) && !PlayerHasBuff(Trueshot))
            {
                API.CastSpell(RacialSpell1);
                return;
            }

            // actions.cds +=/ potion,if= buff.trueshot.up & buff.bloodlust.up | buff.trueshot.up & target.health.pct < 20 | target.time_to_die < 26


            //API.WriteLog("cov not ready: " + NoCovReady);
            if (!IsAOE || (AOESwitch_enabled && API.TargetUnitInRangeCount < AOEUnitNumber && IsAOE))
            {
                #region opener

                if (IsCooldowns && !VolleyTrickShots && API.PlayerTimeInCombat <= 17000 && IsCooldowns && (!IsAOE || (AOESwitch_enabled && API.TargetUnitInRangeCount < AOEUnitNumber && IsAOE)))
                {
                    if (API.CanCast(Double_Tap))
                    {
                        API.CastSpell(Double_Tap);
                        return;
                    }
                    if (API.SpellCDDuration(Double_Tap) > gcd)
                    {
                        if (Talent_Steady_Focus && API.CanCast(Steady_Shot) && API.LastSpellCastInGame != Steady_Shot && API.PlayerCurrentCastSpellID != 56641 && !PlayerHasBuff(Steady_Focus) && InRange)
                        {
                            API.CastSpell(Steady_Shot);
                            API.WriteLog("opener: SS:1 ");
                            return;
                        }
                        if (Talent_Steady_Focus && API.CanCast(Steady_Shot) && API.LastSpellCastInGame != Steady_Shot && API.PlayerCurrentCastSpellID == 56641 && API.PlayerBuffTimeRemaining(Steady_Focus) < 500 && InRange)
                        {
                            API.CastSpell(Steady_Shot);
                            API.WriteLog("opener: SS:2 ");
                            return;
                        }
                        if (API.CanCast(Explosive_Shot) && API.TargetTimeToDie > 300 && (UseExplosiveShot == "With Cooldowns" && IsCooldowns || UseExplosiveShot == "On Cooldown" || UseExplosiveShot == "on AOE" && ((IsAOE && (API.TargetUnitInRangeCount >= AOEUnitNumber || !AOESwitch_enabled)) || (UseCleaveRotation || API.TargetUnitInRangeCount > 1))) && API.PlayerFocus >= 20 && InRange && Talent_Explosive_Shot)
                        {
                            API.CastSpell(Explosive_Shot);
                            return;
                        }
                        if (API.CanCast(Wild_Spirits) && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" || UseCovenant == "on AOE" && API.TargetUnitInRangeCount >= AOEUnitNumber && IsAOE) && InRange)
                        {
                            API.CastSpell(Wild_Spirits);
                            return;
                        }
                        if (API.CanCast(Resonating_Arrow) && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" || UseCovenant == "on AOE" && API.TargetUnitInRangeCount >= AOEUnitNumber && IsAOE) && InRange)
                        {
                            API.CastSpell(Resonating_Arrow);
                            return;
                        }
                        if (API.CanCast(Volley) && (UseVolley == "With Cooldowns" && IsCooldowns || UseVolley == "On Cooldown" || UseVolley == "on AOE" && ((IsAOE && (API.TargetUnitInRangeCount >= AOEUnitNumber || !AOESwitch_enabled)) || (UseCleaveRotation || API.TargetUnitInRangeCount > 1))) && InRange)
                        {
                            API.CastSpell(Volley);
                            return;
                        }
                        if (API.CanCast(Trueshot) && InRange && (VolleyTrickShots || !Talent_Volley) && (API.TargetHasDebuff(Resonating_Arrow) || API.TargetHasDebuff(Wild_Mark) || (API.SpellCDDuration(Resonating_Arrow) > gcd || API.SpellCDDuration(Wild_Spirits) > gcd)))
                        {
                            API.CastSpell(Trueshot);
                            return;
                        }
                        if (PlayerHasBuff(Trueshot))
                        {
                            if (API.CanCast(Aimed_Shot) && InRange && (PlayerHasBuff(Lock_and_Load) || !API.PlayerIsMoving) && API.PlayerFocus > 35)
                            {
                                API.CastSpell(Aimed_Shot);
                                return;
                            }
                            if (API.CanCast(Aimed_Shot) && InRange && (PlayerHasBuff(Lock_and_Load) || !API.PlayerIsMoving) && API.PlayerFocus >= (PlayerHasBuff(Lock_and_Load) ? 0 : 35) && FullRechargeTime(Aimed_Shot, AimedShotCooldown) < gcd + AimedShotCastTime)
                            {
                                API.CastSpell(Aimed_Shot);
                                return;
                            }
                            if (API.CanCast(Rapid_Fire) && !eagletalons_true_focus_enabled && InRange && API.PlayerFocus + FocusRegen * (gcd / 100) + 7 < API.PlayerMaxFocus && FullRechargeTime(Aimed_Shot, AimedShotCooldown) > RapidFireChannelTime)
                            {
                                API.CastSpell(Rapid_Fire);
                                return;
                            }
                            if (API.CanCast(Arcane_Shot) && InRange && API.PlayerFocus > 50 && FullRechargeTime(Aimed_Shot, AimedShotCooldown) > gcd && !API.CanCast(Rapid_Fire))
                            {
                                API.CastSpell(Arcane_Shot);
                                API.WriteLog("Arcane: " + "rechargetime: " + FullRechargeTime(Aimed_Shot, AimedShotCooldown));
                                return;
                            }
                            if (API.CanCast(Steady_Shot) && InRange && API.PlayerFocus + (10 + (SteadyShot_CastTime / 100) * FocusRegen) < 120 && (!API.CanCast(Rapid_Fire) || PlayerHasBuff(Double_Tap)) && (!PlayerHasBuff(Precise_Shots) || PlayerHasBuff(Precise_Shots) && API.PlayerFocus < 20) && (FullRechargeTime(Aimed_Shot, AimedShotCooldown) > SteadyShot_CastTime || API.PlayerFocus < (PlayerHasBuff(Lock_and_Load) ? 0 : 35) || API.PlayerIsMoving))
                            {
                                API.CastSpell(Steady_Shot);
                                API.WriteLog("opener: SS:3 ");
                                return;
                            }
                        }
                    }
                }
                #endregion
                #region cooldowns
                // trinket1,if=buff.trueshot.up&(trinket.1.cooldown.duration>=trinket.2.cooldown.duration            |trinket.2.cooldown.remains)|                                 buff.trueshot.down&cooldown.trueshot.remains>20&trinket.2.cooldown.duration>=trinket.1.cooldown.duration&                    trinket.2.cooldown.remains-5<cooldown.trueshot.remains              &!trinket.2.is.dreadfire_vessel|(trinket.1.cooldown.duration-5<cooldown.trueshot.remains                          &(trinket.1.cooldown.duration>=trinket.2.cooldown.duration|trinket.2.cooldown.remains))|target.time_to_die<cooldown.trueshot.remains
                if (API.PlayerTrinketIsUsable(1) && (PlayerHasBuff(Trueshot) && (API.PlayerTrinketRemainingCD(1) >= API.PlayerTrinketRemainingCD(2) || API.PlayerTrinketRemainingCD(2) > 0) || !PlayerHasBuff(Trueshot) && API.SpellCDDuration(Trueshot) > 2000 && API.PlayerTrinketRemainingCD(2) >= API.PlayerTrinketRemainingCD(1) && API.PlayerTrinketRemainingCD(2) - 500 < API.SpellCDDuration(Trueshot) && EquippedTrinket2 != "Dreadfire Vessel" || (API.PlayerTrinketRemainingCD(1) - 500 < API.SpellCDDuration(Trueshot) && (API.PlayerTrinketRemainingCD(1) >= API.PlayerTrinketRemainingCD(2) || API.PlayerTrinketRemainingCD(2) > 0)) || API.TargetTimeToDie < API.SpellCDDuration(Trueshot)) && API.PlayerTrinketRemainingCD(1) == 0 && (UseTrinket1 == "With Cooldowns" && IsCooldowns || UseTrinket1 == "On Cooldown" || UseTrinket1 == "on AOE" && API.TargetUnitInRangeCount >= AOEUnitNumber && IsAOE) && InRange)
                {
                    API.CastSpell("Trinket1");
                }
                //trinket2,if=buff.trueshot.up&(trinket.2.cooldown.duration>=trinket.1.cooldown.duration|trinket.1.cooldown.remains)|buff.trueshot.down&cooldown.trueshot.remains>20&trinket.1.cooldown.duration>=trinket.2.cooldown.duration&trinket.1.cooldown.remains-5<cooldown.trueshot.remains&!trinket.1.is.dreadfire_vessel|(trinket.2.cooldown.duration-5<cooldown.trueshot.remains&(trinket.2.cooldown.duration>=trinket.1.cooldown.duration|trinket.1.cooldown.remains))|target.time_to_die<cooldown.trueshot.remains
                if (API.PlayerTrinketIsUsable(2) && (PlayerHasBuff(Trueshot) && (API.PlayerTrinketRemainingCD(2) >= API.PlayerTrinketRemainingCD(1) || API.PlayerTrinketRemainingCD(1) > 0) || !PlayerHasBuff(Trueshot) && API.SpellCDDuration(Trueshot) > 2000 && API.PlayerTrinketRemainingCD(1) >= API.PlayerTrinketRemainingCD(2) && API.PlayerTrinketRemainingCD(1) - 500 < API.SpellCDDuration(Trueshot) && EquippedTrinket1 != "Dreadfire Vessel" || (API.PlayerTrinketRemainingCD(2) - 500 < API.SpellCDDuration(Trueshot) && (API.PlayerTrinketRemainingCD(2) >= API.PlayerTrinketRemainingCD(1) || API.PlayerTrinketRemainingCD(1) > 0)) || API.TargetTimeToDie < API.SpellCDDuration(Trueshot)) && API.PlayerTrinketRemainingCD(2) == 0 && (UseTrinket2 == "With Cooldowns" && IsCooldowns || UseTrinket2 == "On Cooldown" || UseTrinket2 == "on AOE" && API.TargetUnitInRangeCount >= AOEUnitNumber && IsAOE) && InRange)
                {
                    API.CastSpell("Trinket2");
                }
                #endregion
                #region ST

                if (PlayerHasBuff(Trueshot))
                {
                    if (Talent_Steady_Focus && API.CanCast(Steady_Shot) && API.LastSpellCastInGame != Steady_Shot && API.PlayerCurrentCastSpellID == 56641 && API.PlayerBuffTimeRemaining(Steady_Focus) < 500 && InRange)
                    {
                        API.CastSpell(Steady_Shot);
                        //API.WriteLog("ST: SS:1 ");
                        return;
                    }
                    else if ((API.TargetHealthPercent <= 20 || PlayerHasBuff(FlayersMark)) && API.CanCast(Kill_Shot) && InRange && PlayerLevel >= 42 && API.PlayerFocus >= 10)
                    {
                        API.CastSpell(Kill_Shot);
                        return;
                    }
                    else if (API.CanCast(Kill_Shot) && (IsMouseover && (!isMouseoverInCombat || API.MouseoverIsIncombat) && API.PlayerCanAttackMouseover && (API.MouseoverHealthPercent <= 20 || PlayerHasBuff(FlayersMark))) && API.PlayerFocus >= 10 && PlayerLevel >= 42)
                    {
                        API.CastSpell(Kill_Shot + "MO");
                        return;
                    }
                    else if (API.CanCast(Aimed_Shot) && InRange && (PlayerHasBuff(Lock_and_Load) || !API.PlayerIsMoving) && API.PlayerFocus > 35)
                    {
                        API.CastSpell(Aimed_Shot);
                        return;
                    }
                    else if (API.CanCast(Aimed_Shot) && InRange && (PlayerHasBuff(Lock_and_Load) || !API.PlayerIsMoving) && API.PlayerFocus >= (PlayerHasBuff(Lock_and_Load) ? 0 : 35) && FullRechargeTime(Aimed_Shot, AimedShotCooldown) < gcd + AimedShotCastTime)
                    {
                        API.CastSpell(Aimed_Shot);
                        return;
                    }
                    else if (API.CanCast(Rapid_Fire) && !eagletalons_true_focus_enabled && InRange && API.PlayerFocus + FocusRegen * (gcd / 100) + 7 < API.PlayerMaxFocus && FullRechargeTime(Aimed_Shot, AimedShotCooldown) > RapidFireChannelTime)
                    {
                        API.CastSpell(Rapid_Fire);
                        return;
                    }
                    else if (API.CanCast(Arcane_Shot) && InRange && API.PlayerFocus > 50 && FullRechargeTime(Aimed_Shot, AimedShotCooldown) > gcd && !API.CanCast(Rapid_Fire))
                    {
                        API.CastSpell(Arcane_Shot);
                        return;
                    }
                    else if (API.CanCast(Steady_Shot) && InRange && API.PlayerFocus + (10 + (SteadyShot_CastTime / 100) * FocusRegen) < 120 && (!API.CanCast(Rapid_Fire) || PlayerHasBuff(Double_Tap)) && (!PlayerHasBuff(Precise_Shots) || PlayerHasBuff(Precise_Shots) && API.PlayerFocus < 20) && (FullRechargeTime(Aimed_Shot, AimedShotCooldown) > SteadyShot_CastTime || API.PlayerFocus < (PlayerHasBuff(Lock_and_Load) ? 0 : 35) || API.PlayerIsMoving))
                    {
                        API.CastSpell(Steady_Shot);
                       // API.WriteLog("st: SS:2 ");
                        return;
                    }
                }
                if (VolleyTrickShots && (UseCleaveRotation || API.TargetUnitInRangeCount > 1))
                {
                    if (API.CanCast(Trueshot) && NoCovReady && (UseTrueshot == "always" || (UseTrueshot == "with Cooldowns" && IsCooldowns)) && InRange &&
    (!PlayerHasBuff(Precise_Shots) || API.TargetHasDebuff(Resonating_Arrow) || API.TargetHasDebuff(Wild_Mark) || VolleyTrickShots && (UseCleaveRotation || API.TargetUnitInRangeCount > 1)))
                    {
                        API.CastSpell(Trueshot);
                        return;
                    }
                    else if (API.CanCast(Aimed_Shot) && (!API.PlayerIsMoving || PlayerHasBuff(Lock_and_Load)) && (API.SpellCharges(Aimed_Shot) >= 2 || !API.CanCast(Rapid_Fire)) && InRange && API.PlayerCurrentCastSpellID != 19434)
                    {
                        API.CastSpell(Aimed_Shot);
                        return;
                    }
                    else if (API.CanCast(Rapid_Fire) && InRange)
                    {
                        API.CastSpell(Rapid_Fire);
                        return;
                    }
                }
                if (!PlayerHasBuff(Trueshot))
                {
                    if (Talent_Steady_Focus && API.CanCast(Steady_Shot) && API.LastSpellCastInGame != Steady_Shot && API.PlayerCurrentCastSpellID == 56641 && API.PlayerBuffTimeRemaining(Steady_Focus) < 500 && InRange)
                    {
                        API.CastSpell(Steady_Shot);
                        //API.WriteLog("st: SS:3 ");
                        return;
                    }
                    //actions.st +=/ kill_shot
                    else if ((API.TargetHealthPercent <= 20 || PlayerHasBuff(FlayersMark)) && API.CanCast(Kill_Shot) && InRange && PlayerLevel >= 42 && API.PlayerFocus >= 10)
                    {
                        API.CastSpell(Kill_Shot);
                        return;
                    }
                    else if (API.CanCast(Kill_Shot) && (IsMouseover && (!isMouseoverInCombat || API.MouseoverIsIncombat) && API.PlayerCanAttackMouseover && (API.MouseoverHealthPercent <= 20 || PlayerHasBuff(FlayersMark))) && API.PlayerFocus >= 10 && PlayerLevel >= 42)
                    {
                        API.CastSpell(Kill_Shot + "MO");
                        return;
                    }
                    //actions.st +=/ double_tap,if= covenant.kyrian & cooldown.resonating_arrow.remains < gcd | !covenant.kyrian & !covenant.night_fae | covenant.night_fae & (cooldown.wild_spirits.remains < gcd | cooldown.trueshot.remains > 55) | target.time_to_die < 15
                    else if (API.CanCast(Double_Tap) && !VolleyTrickShots && (UseDoubleTap == "always" || (UseDoubleTap == "with Cooldowns" && IsCooldowns)) && InRange && Talent_Double_Tap && (API.SpellCDDuration(Aimed_Shot) < API.SpellCDDuration(Rapid_Fire))
         && (PlayerCovenantSettings == "Kyrian" && API.SpellCDDuration(Resonating_Arrow) < gcd || PlayerCovenantSettings != "Kyrian" && PlayerCovenantSettings != "Night Fae" || PlayerCovenantSettings == "Night Fae" && (API.SpellCDDuration(Wild_Spirits) < gcd || API.SpellCDDuration(Trueshot) > 5500) || API.TargetTimeToDie < 1500))
                    {
                        API.CastSpell(Double_Tap);
                        return;
                    }
                    //actions.st = steady_shot,if= talent.steady_focus & (prev_gcd.1.steady_shot & buff.steady_focus.remains < 5 | buff.steady_focus.down)
                    else if (Talent_Steady_Focus && API.CanCast(Steady_Shot) && API.LastSpellCastInGame != Steady_Shot && API.PlayerCurrentCastSpellID != 56641 && !PlayerHasBuff(Steady_Focus) && InRange)
                    {
                        API.CastSpell(Steady_Shot);
                        //API.WriteLog("st: SS:4 ");
                        return;
                    }
                    //actions.st +=/ flare,if= tar_trap.up & runeforge.soulforge_embers
                    //actions.st +=/ tar_trap,if= runeforge.soulforge_embers & tar_trap.remains < gcd & cooldown.flare.remains < gcd
                    //actions.st +=/ explosive_shot
                    else if (API.CanCast(Explosive_Shot) && API.TargetTimeToDie > 300 && (UseExplosiveShot == "With Cooldowns" && IsCooldowns || UseExplosiveShot == "On Cooldown" || UseExplosiveShot == "on AOE" && ((IsAOE && (API.TargetUnitInRangeCount >= AOEUnitNumber || !AOESwitch_enabled)) || (UseCleaveRotation || API.TargetUnitInRangeCount > 1))) && API.PlayerFocus >= 20 && InRange && Talent_Explosive_Shot)
                    {
                        API.CastSpell(Explosive_Shot);
                        return;
                    }
                    //actions.st +=/ wild_spirits
                    else if (API.CanCast(Wild_Spirits) && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" || UseCovenant == "on AOE" && API.TargetUnitInRangeCount >= AOEUnitNumber && IsAOE) && InRange)
                    {
                        API.CastSpell(Wild_Spirits);
                        return;
                    }
                    //actions.st +=/ flayed_shot
                    else if (API.CanCast(Flayed_Shot) && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" || UseCovenant == "on AOE" && API.TargetUnitInRangeCount >= AOEUnitNumber && IsAOE) && InRange)
                    {
                        API.CastSpell(Flayed_Shot);
                        return;
                    }
                    //actions.st +=/ death_chakram,if= focus + cast_regen < focus.max
                    else if (API.CanCast(Death_Chakram) && API.PlayerFocus + FocusRegen * gcd / 100 < API.PlayerMaxFocus && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" || UseCovenant == "on AOE" && API.TargetUnitInRangeCount >= AOEUnitNumber && IsAOE) && InRange)
                    {
                        API.CastSpell(Death_Chakram);
                        return;
                    }
                    //actions.st +=/ a_murder_of_crows
                    else if (Talent_A_Murder_of_Crows && (UseAMurderofCrows == "always" || (UseAMurderofCrows == "with Cooldowns" && IsCooldowns)) && API.CanCast(A_Murder_of_Crows) && InRange && API.PlayerFocus >= 20)
                    {
                        API.CastSpell(A_Murder_of_Crows);
                        return;
                    }
                    //actions.st +=/ resonating_arrow
                    else if (API.CanCast(Resonating_Arrow) && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" || UseCovenant == "on AOE" && API.TargetUnitInRangeCount >= AOEUnitNumber && IsAOE) && InRange)
                    {
                        API.CastSpell(Resonating_Arrow);
                        return;
                    }
                    //actions.st +=/ volley,if= buff.precise_shots.down | !talent.chimaera_shot | active_enemies < 2
                    else if (API.CanCast(Volley) && (!PlayerHasBuff(Precise_Shots) || !Talent_Chimaera_Shot || (!UseCleaveRotation && API.TargetUnitInRangeCount < 2)) && (UseVolley == "With Cooldowns" && IsCooldowns || UseVolley == "On Cooldown" || UseVolley == "on AOE" && ((IsAOE && (API.TargetUnitInRangeCount >= AOEUnitNumber || !AOESwitch_enabled)) || (UseCleaveRotation || API.TargetUnitInRangeCount > 1))) && InRange && Talent_Volley)
                    {
                        API.CastSpell(Volley);
                        return;
                    }
                    //actions.st +=/ trueshot,if= buff.precise_shots.down | buff.resonating_arrow.up | buff.wild_spirits.up | buff.volley.up & active_enemies > 1
                    else if (API.CanCast(Trueshot) && NoCovReady && (UseTrueshot == "always" || (UseTrueshot == "with Cooldowns" && IsCooldowns)) && InRange && (!PlayerHasBuff(Precise_Shots) || API.TargetHasDebuff(Resonating_Arrow) || API.TargetHasDebuff(Wild_Mark) || VolleyTrickShots && (UseCleaveRotation || API.TargetUnitInRangeCount > 1)))
                    {
                        API.CastSpell(Trueshot);
                        return;
                    }

                    else if (API.CanCast(Rapid_Fire) && !PlayerHasBuff(Double_Tap) && InRange && (API.PlayerFocus + FocusRegen * (gcd / 100) + 7 < API.PlayerMaxFocus && FullRechargeTime(Aimed_Shot, AimedShotCooldown) >= RapidFireChannelTime || API.PlayerIsMoving))
                    {
                        API.CastSpell(Rapid_Fire);
                        return;
                    }
                    //actions.st +=/ aimed_shot,target_if = min:dot.serpent_sting.remains + action.serpent_sting.in_flight_to_target * 99,if= 
                    //buff.precise_shots.down | (buff.trueshot.up | full_recharge_time < gcd + cast_time) & (!talent.chimaera_shot | active_enemies < 2) | buff.trick_shots.remains > execute_time & active_enemies > 1
                    else if (API.CanCast(Aimed_Shot) && InRange && (PlayerHasBuff(Lock_and_Load) || !API.PlayerIsMoving) && API.PlayerFocus >= (PlayerHasBuff(Lock_and_Load) ? 0 : 35) && API.PlayerCurrentCastSpellID != 19434 &&
        (API.TargetDebuffRemainingTime(Serpent_Sting) > 200 || !Talent_Serpent_Sting) &&
        (!PlayerHasBuff(Precise_Shots) || (PlayerHasBuff(Trueshot) || FullRechargeTime(Aimed_Shot, AimedShotCooldown) < gcd + AimedShotCastTime) && (!Talent_Chimaera_Shot || (!UseCleaveRotation && API.TargetUnitInRangeCount < 2)) || API.PlayerBuffTimeRemaining(Trick_Shots) > AimedShotCastTime && (UseCleaveRotation || API.TargetUnitInRangeCount > 1)))
                    {
                        API.CastSpell(Aimed_Shot);
                        return;
                    }
                    //actions.st +=/ rapid_fire,if= focus + cast_regen < focus.max & (buff.trueshot.down | !runeforge.eagletalons_true_focus) & (buff.double_tap.down | talent.streamline)
                    else if (API.CanCast(Rapid_Fire) && InRange && ((API.PlayerFocus + FocusRegen * (gcd / 100) + 7 < API.PlayerMaxFocus || API.PlayerIsMoving) && (!PlayerHasBuff(Trueshot) || !eagletalons_true_focus_enabled) && (!PlayerHasBuff(Double_Tap) || Talent_Streamline)))
                    {
                        API.CastSpell(Rapid_Fire);
                        return;
                    }
                    //actions.st +=/ chimaera_shot,if= buff.precise_shots.up | focus > cost + action.aimed_shot.cost
                    else if (Talent_Chimaera_Shot && API.CanCast(Chimaera_Shot) && InRange && (PlayerHasBuff(Precise_Shots) || API.PlayerFocus > 10 + (PlayerHasBuff(Lock_and_Load) ? 0 : 35)))
                    {
                        API.CastSpell(Chimaera_Shot);
                        return;
                    }
                    //actions.st +=/ arcane_shot,if= buff.precise_shots.up | focus > cost + action.aimed_shot.cost
                    else if (API.CanCast(Arcane_Shot) && (API.SpellCDDuration(Rapid_Fire) > gcd || PlayerHasBuff(Double_Tap)) && InRange && (PlayerHasBuff(Precise_Shots) || API.PlayerFocus > 20 + (PlayerHasBuff(Lock_and_Load) ? 0 : 35)))
                    {
                        API.CastSpell(Arcane_Shot);
                        return;
                    }
                    //actions.st +=/ serpent_sting,target_if = min:remains,if= refreshable & target.time_to_die > duration
                    else if (Talent_Serpent_Sting && API.CanCast(Serpent_Sting) && API.PlayerFocus > 10 && InRange && (!API.TargetHasDebuff(Serpent_Sting) || API.PlayerDebuffRemainingTime(Serpent_Sting) < 200) && API.TargetTimeToDie >= 1800)
                    {
                        API.CastSpell(Serpent_Sting);
                        return;
                    }
                    //actions.st +=/ barrage,if= active_enemies > 1
                    else if (Talent_Barrage && API.CanCast(Barrage) && InRange && IsAOE && (UseCleaveRotation || API.TargetUnitInRangeCount > 1) && API.PlayerFocus >= 30)
                    {
                        API.CastSpell(Barrage);
                        return;
                    }
                    //actions.st +=/ rapid_fire,if= focus + cast_regen < focus.max & (buff.double_tap.down | talent.streamline)
                    else if (API.CanCast(Rapid_Fire) && InRange && (FocusRegen * (gcd / 100) + 7 < API.PlayerMaxFocus && (!PlayerHasBuff(Double_Tap) || Talent_Streamline)))
                    {
                        API.CastSpell(Rapid_Fire);
                        return;
                    }
                    //actions.st +=/ steady_shot
                    /*    if (API.CanCast(Steady_Shot) && InRange && (API.PlayerFocus + (10 + ((SteadyShot_CastTime*FocusRegen) / 100) * FocusRegen) < 120 || API.PlayerIsMoving) && (!API.CanCast(Rapid_Fire) || PlayerHasBuff(Double_Tap)) && (!PlayerHasBuff(Precise_Shots) || PlayerHasBuff(Precise_Shots) && API.PlayerFocus < 20) && (FullRechargeTime(Aimed_Shot, AimedShotCooldown) > SteadyShot_CastTime || API.PlayerFocus < (PlayerHasBuff(Lock_and_Load) ? 0 : 35) || API.PlayerIsMoving))
                        {
                            API.CastSpell(Steady_Shot);
                            API.WriteLog("st: SS:5 ");
                            return;
                        }*/
                    else if (API.CanCast(Steady_Shot) && InRange && !CanuseASinST && !API.CanCast(Rapid_Fire) && !CanuseArcaneinST)
                    {
                        API.CastSpell(Steady_Shot);
                        //API.WriteLog("st: SS:5 ");
                        return;
                    }
                }
            }


            #endregion
            else
            {
                //actions.trickshots = steady_shot,if= talent.steady_focus & in_flight & buff.steady_focus.remains < 5
                /* if (Talent_Steady_Focus && API.CanCast(Steady_Shot) && API.LastSpellCastInGame != Steady_Shot && API.PlayerCurrentCastSpellID != 56641 && !PlayerHasBuff(Steady_Focus) && InRange)
                 {
                     API.CastSpell(Steady_Shot);
                     return;
                 }*/
                if (Talent_Steady_Focus && !PlayerHasBuff(Trueshot) && !VolleyTrickShots && API.CanCast(Steady_Shot) && API.LastSpellCastInGame != Steady_Shot && API.PlayerCurrentCastSpellID == 56641 && API.PlayerBuffTimeRemaining(Steady_Focus) < 500 && InRange)
                {
                    API.CastSpell(Steady_Shot);
                   // API.WriteLog("AOE: SS:1 ");
                    return;
                }
                //actions.trickshots +=/ double_tap,if= covenant.kyrian & cooldown.resonating_arrow.remains < gcd | !covenant.kyrian & !covenant.night_fae | covenant.night_fae & (cooldown.wild_spirits.remains < gcd | cooldown.trueshot.remains > 55) | target.time_to_die < 10
                else if (API.CanCast(Double_Tap) && !VolleyTrickShots && (UseDoubleTap == "always" || (UseDoubleTap == "with Cooldowns" && IsCooldowns)) && InRange && Talent_Double_Tap && (API.SpellCDDuration(Aimed_Shot) - 300 < API.SpellCDDuration(Rapid_Fire))
&& (PlayerCovenantSettings == "Kyrian" && API.SpellCDDuration(Resonating_Arrow) < gcd || PlayerCovenantSettings != "Kyrian" && PlayerCovenantSettings != "Night Fae" || PlayerCovenantSettings == "Night Fae" && (API.SpellCDDuration(Wild_Spirits) < gcd || API.SpellCDDuration(Trueshot) > 5500) || API.TargetTimeToDie < 1000))
                {
                    API.CastSpell(Double_Tap);
                    return;
                }
                else if (API.CanCast(Double_Tap) && !VolleyTrickShots && (UseDoubleTap == "always" || (UseDoubleTap == "with Cooldowns" && IsCooldowns)) && InRange && Talent_Double_Tap && API.SpellCDDuration(Aimed_Shot) - 300 < API.SpellCDDuration(Rapid_Fire) && (!(UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" || UseCovenant == "on AOE" && API.TargetUnitInRangeCount >= AOEUnitNumber && IsAOE) || API.TargetTimeToDie < 1000))
                {
                    API.CastSpell(Double_Tap);
                    return;
                }
                //actions.trickshots +=/ tar_trap,if= runeforge.soulforge_embers & tar_trap.remains < gcd & cooldown.flare.remains < gcd
                //actions.trickshots +=/ flare,if= tar_trap.up & runeforge.soulforge_embers
                //actions.trickshots +=/ explosive_shot
                else if (API.CanCast(Explosive_Shot) && API.TargetTimeToDie > 300 && (UseExplosiveShot == "With Cooldowns" && IsCooldowns || UseExplosiveShot == "On Cooldown" || UseExplosiveShot == "on AOE" && ((IsAOE && (API.TargetUnitInRangeCount >= AOEUnitNumber || !AOESwitch_enabled)) || (UseCleaveRotation || API.TargetUnitInRangeCount > 1))) && API.PlayerFocus >= 20 && InRange && Talent_Explosive_Shot)
                {
                    API.CastSpell(Explosive_Shot);
                    return;
                }
                //actions.trickshots +=/ wild_spirits
                else if (API.CanCast(Wild_Spirits) && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" || UseCovenant == "on AOE" && API.TargetUnitInRangeCount >= AOEUnitNumber && IsAOE) && InRange)
                {
                    API.CastSpell(Wild_Spirits);
                    return;
                }
                //actions.trickshots +=/ resonating_arrow
                else if (API.CanCast(Resonating_Arrow) && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" || UseCovenant == "on AOE" && API.TargetUnitInRangeCount >= AOEUnitNumber && IsAOE) && InRange)
                {
                    API.CastSpell(Resonating_Arrow);
                    return;
                }
                //actions.trickshots +=/ volley
                else if (API.CanCast(Volley) && (UseVolley == "With Cooldowns" && IsCooldowns || UseVolley == "On Cooldown" || UseVolley == "on AOE" && ((IsAOE && (API.TargetUnitInRangeCount >= AOEUnitNumber || !AOESwitch_enabled)) || (UseCleaveRotation || API.TargetUnitInRangeCount > 1))) && InRange && Talent_Volley)
                {
                    API.CastSpell(Volley);
                    return;
                }
                //actions.trickshots +=/ barrage
                else if (Talent_Barrage && API.CanCast(Barrage) && InRange && API.PlayerFocus >= 30)
                {
                    API.CastSpell(Barrage);
                    return;
                }
                //actions.trickshots +=/ trueshot
                else if (API.CanCast(Trueshot) && NoCovReady && (UseTrueshot == "always" || (UseTrueshot == "with Cooldowns" && IsCooldowns)) && InRange)
                {
                    API.CastSpell(Trueshot);
                    return;
                }
                //actions.trickshots +=/ rapid_fire,if= buff.trick_shots.remains >= execute_time & runeforge.surging_shots & buff.double_tap.down
                else if (API.CanCast(Rapid_Fire) && (API.PlayerCurrentCastSpellID != 19434 || VolleyTrickShots) && InRange && API.PlayerBuffTimeRemaining(Trick_Shots) >= RapidFireChannelTime && !PlayerHasBuff(Double_Tap) && SurgingShots_enabled)
                {
                    API.CastSpell(Rapid_Fire);
                    return;
                }
                //API.WriteLog("PlayerHasBuff(Lock_and_Load) " + PlayerHasBuff(Lock_and_Load) + " API.PlayerIsMoving "+ API.PlayerIsMoving +" API.PlayerBuffTimeRemaining(Trick_Shots) " + API.PlayerBuffTimeRemaining(Trick_Shots) + " AimedShotCastTime " + AimedShotCastTime);
                //actions.trickshots +=/ aimed_shot,target_if = min:dot.serpent_sting.remains + action.serpent_sting.in_flight_to_target * 99,if= buff.trick_shots.remains >= execute_time & (buff.precise_shots.down | full_recharge_time < cast_time + gcd | buff.trueshot.up)
                else if (API.CanCast(Aimed_Shot) && InRange && (PlayerHasBuff(Lock_and_Load) || !API.PlayerIsMoving) && API.PlayerFocus >= (PlayerHasBuff(Lock_and_Load) ? 0 : 35) && (API.PlayerCurrentCastSpellID != 19434 || !API.CanCast(Rapid_Fire) && VolleyTrickShots) && (API.PlayerCurrentCastSpellID != 257044 || VolleyTrickShots) &&
    (API.TargetDebuffRemainingTime(Serpent_Sting) > 200 || !Talent_Serpent_Sting) &&
    API.PlayerBuffTimeRemaining(Trick_Shots) >= AimedShotCastTime && (!PlayerHasBuff(Precise_Shots) || FullRechargeTime(Aimed_Shot, AimedShotCooldown) < AimedShotCastTime + gcd || PlayerHasBuff(Trueshot)))
                {
                    API.CastSpell(Aimed_Shot);
                    return;
                }
                //actions.trickshots +=/ death_chakram,if= focus + cast_regen < focus.max
                else if (API.CanCast(Death_Chakram) && API.PlayerFocus + FocusRegen * (gcd / 100) < API.PlayerMaxFocus && PlayerCovenantSettings == "Necrolord" && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" || UseCovenant == "on AOE" && API.TargetUnitInRangeCount >= AOEUnitNumber && IsAOE) && InRange)
                {
                    API.CastSpell(Death_Chakram);
                    return;
                }
                //actions.trickshots +=/ rapid_fire,if= buff.trick_shots.remains >= execute_time
                else if (API.CanCast(Rapid_Fire) && (API.PlayerCurrentCastSpellID != 19434 || VolleyTrickShots) && InRange && API.PlayerBuffTimeRemaining(Trick_Shots) >= RapidFireChannelTime)
                {
                    API.CastSpell(Rapid_Fire);
                    return;
                }
                //actions.trickshots +=/ multishot,if= buff.trick_shots.down | buff.precise_shots.up & focus > cost + action.aimed_shot.cost & (!talent.chimaera_shot | active_enemies > 3)
                else if (API.CanCast(Multi_Shot) && InRange && (!Talent_Chimaera_Shot || API.TargetUnitInRangeCount > 3 || !AOESwitch_enabled && IsAOE) && API.PlayerFocus >= 20 && (!VolleyTrickShots || !API.CanCast(Aimed_Shot) && !API.CanCast(Rapid_Fire)) && (!PlayerHasBuff(Trick_Shots) || PlayerHasBuff(Precise_Shots) && API.PlayerFocus > 20 + (PlayerHasBuff(Lock_and_Load) ? 0 : 35)))
                {
                    API.CastSpell(Multi_Shot);
                    return;
                }
                //actions.trickshots +=/ chimaera_shot,if= buff.precise_shots.up & focus > cost + action.aimed_shot.cost & active_enemies < 4
                else if (Talent_Chimaera_Shot && API.CanCast(Chimaera_Shot) && InRange && (PlayerHasBuff(Precise_Shots) && API.PlayerFocus > 10 + (PlayerHasBuff(Lock_and_Load) ? 0 : 35) && API.TargetUnitInRangeCount < 4))
                {
                    API.CastSpell(Chimaera_Shot);
                    return;
                }
                //actions.trickshots +=/ kill_shot,if= buff.dead_eye.down
                else if (API.CanCast(Kill_Shot) && InRange && PlayerLevel >= 42 && !PlayerHasBuff(Dead_Eye))
                {
                    API.CastSpell(Kill_Shot);
                    return;
                }
                else if (API.CanCast(Kill_Shot) && !PlayerHasBuff(Dead_Eye) && (IsMouseover && (!isMouseoverInCombat || API.MouseoverIsIncombat) && API.PlayerCanAttackMouseover && (API.MouseoverHealthPercent <= 20 || PlayerHasBuff(FlayersMark)) && API.PlayerFocus >= 10 && PlayerLevel >= 42))
                {
                    API.CastSpell(Kill_Shot + "MO");
                    return;
                }
                //actions.trickshots +=/ a_murder_of_crows
                else if (Talent_A_Murder_of_Crows && (UseAMurderofCrows == "always" || (UseAMurderofCrows == "with Cooldowns" && IsCooldowns)) && API.CanCast(A_Murder_of_Crows) && InRange && API.PlayerFocus >= 20)
                {
                    API.CastSpell(A_Murder_of_Crows);
                    return;
                }
                //actions.trickshots +=/ flayed_shot
                else if (API.CanCast(Flayed_Shot) && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" || UseCovenant == "on AOE" && API.TargetUnitInRangeCount >= AOEUnitNumber && IsAOE) && InRange)
                {
                    API.CastSpell(Flayed_Shot);
                    return;
                }
                //actions.trickshots +=/ serpent_sting,target_if = min:dot.serpent_sting.remains,if= refreshable
                else if (Talent_Serpent_Sting && API.CanCast(Serpent_Sting) && API.PlayerFocus > 10 && InRange && (!API.TargetHasDebuff(Serpent_Sting) || API.TargetDebuffRemainingTime(Serpent_Sting) < 200) && API.TargetTimeToDie > 1800)
                {
                    API.CastSpell(Serpent_Sting);
                    return;
                }
                //actions.trickshots +=/ multishot,if= focus > cost + action.aimed_shot.cost
                else if (API.CanCast(Multi_Shot) && (!API.CanCast(Aimed_Shot) && !API.CanCast(Rapid_Fire) || API.PlayerIsMoving) && InRange && API.PlayerFocus >= 20 && API.PlayerFocus > 20 + (PlayerHasBuff(Lock_and_Load) ? 0 : 35))
                {
                    API.CastSpell(Multi_Shot);
                    return;
                }
                else if (API.CanCast(Steady_Shot) && ((!API.CanCast(Aimed_Shot) || API.PlayerIsMoving) && !API.CanCast(Rapid_Fire) || (!PlayerHasBuff(Trick_Shots) && !VolleyTrickShots)) && (API.PlayerFocus < 20 + (PlayerHasBuff(Lock_and_Load) ? 0 : 35)) && InRange)
                {
                    API.CastSpell(Steady_Shot);
                    //API.WriteLog("AOE: SS:2 ");
                    return;
                }
                //actions.trickshots +=/ steady_shot
                /*     if (API.CanCast(Steady_Shot) && (API.CanCast(Aimed_Shot) && API.PlayerFocus < (PlayerHasBuff(Lock_and_Load) ? 0 : 35) || !API.CanCast(Aimed_Shot)) && !API.CanCast(Rapid_Fire) && InRange)
                     {
                         API.CastSpell(Steady_Shot);
                         API.WriteLog("AOE: SS:2 ");
                         return;
                     }*/
            }
        }
    }
}


