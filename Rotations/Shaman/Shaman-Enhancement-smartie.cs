﻿// Changelog
// v1.0 First release
// v1.1 covenants and cd managment
// v1.2 vesper totem fix
// v1.3 legendary preperation
// v1.4 Racials and Trinkets
// v1.5 Doomwind Leggy fixed
// v1.6 updated to latest simc apl
// v1.7 spell ids and alot of other stuff
// v1.8 Racials and other small fixes
// v1.9 DoomWinds legendary fix
// v2.0 Windfury Totem adjustments
// v2.1 Auto weapon Enchantments
// v2.2 added option to use Windfury totem while moving
// v2.3 ghost wolf while moving and nothing else to do
// v2.35 small ghostwolf change
// v2.4 earthshield fix
// v2.5 new simc apl and new settings options
// v2.6 explosive protection
// v2.7 hotfix
// v2.8 trinkets ignore
// v2.9 auto dps potion added

using System.Diagnostics;
namespace HyperElk.Core
{
    public class EnhancementShaman : CombatRoutine
    {
        private bool WindfuryToggle => API.ToggleIsEnabled("Windfury Totem");
        private bool SunderingToggle => API.ToggleIsEnabled("Sundering");
        private bool IsFocus => API.ToggleIsEnabled("Focus ES");
        //Spell,Auras
        private string LavaLash = "Lava Lash";
        private string CrashLightning = "Crash Lightning";
        private string Ascendance = "Ascendance";
        private string Windstrike = "Windstrike";
        private string LightningShield = "Lightning Shield";
        private string FeralSpirit = "Feral Spirit";
        private string Stormstrike = "Stormstrike";
        private string Sundering = "Sundering";
        private string GhostWolf = "Ghost Wolf";
        private string HealingSurge = "Healing Surge";
        private string LightningBolt = "Lightning Bolt";
        private string EarthenSpike = "Earthen Spike";
        private string WindShear = "Wind Shear";
        private string AstralShift = "Astral Shift";
        private string ChainLightning = "Chain Lightning";
        private string ElementalBlast = "Elemental Blast";
        private string FrostShock = "Frost Shock";
        private string IceStrike = "Ice Strike";
        private string FireNova = "Fire Nova";
        private string StormKeeper = "Stormkeeper";
        private string FlameShock = "Flame Shock";
        private string EarthElemental = "Earth Elemental";
        private string EarthShield = "Earth Shield";
        private string WindfuryTotem = "Windfury Totem";
        private string PrimalStrike = "Primal Strike";
        private string Stormbringer = "Stormbringer";
        private string HotHand = "Hot Hand";
        private string Hailstorm = "Hailstorm";
        private string MaelstromWeapon = "Maelstrom Weapon";
        private string HealingStreamTotem = "Healing Stream Totem";
        private string LashingFlames = "Lashing Flames";
        private string PrimordialWave = "Primordial Wave";
        private string VesperTotem = "Vesper Totem";
        private string FaeTransfusion = "Fae Transfusion";
        private string ChainHarvest = "Chain Harvest";
        private string DoomWinds = "Doom Winds";
        private string PhialofSerenity = "Phial of Serenity";
        private string SpiritualHealingPotion = "Spiritual Healing Potion";
        private string WindfuryWeapon = "Windfury Weapon";
        private string FlametongueWeapon = "Flametongue Weapon";
        private string PrimalLavaActuators = "Primal Lava Actuators";
        private string PotionofSpectralAgility = "Potion of Spectral Agility";

        //Talents
        bool TalentLashingFlames => API.PlayerIsTalentSelected(1, 1);
        bool TalentElementalBlast => API.PlayerIsTalentSelected(1, 3);
        bool TalentIceStrike => API.PlayerIsTalentSelected(2, 3);
        bool TalentEarthShield => API.PlayerIsTalentSelected(3, 2);
        bool TalentFireNova => API.PlayerIsTalentSelected(4, 3);
        bool TalentStormkeeper => API.PlayerIsTalentSelected(6, 2);
        bool TalentCrashingStorm => API.PlayerIsTalentSelected(6, 1);
        bool TalentSundering => API.PlayerIsTalentSelected(6, 3);
        bool TalentEarthenSpike => API.PlayerIsTalentSelected(7, 2);
        bool TalentAscendance => API.PlayerIsTalentSelected(7, 3);

        //General
        private bool isExplosive => API.TargetMaxHealth <= 600 && API.TargetMaxHealth != 0 && PlayerLevel == 60;
        private int PlayerLevel => API.PlayerLevel;
        private bool isMelee => API.TargetRange < 6;
        private bool isinrange => API.TargetRange < 41;
        private bool iskickrange => API.TargetRange < 31;
        bool IsAscendance => (UseAscendance == "with Cooldowns" || UseAscendance == "with Cooldowns or AoE" || UseAscendance == "on mobcount or Cooldowns") && IsCooldowns || UseAscendance == "always" || (UseAscendance == "on AOE" || UseAscendance == "with Cooldowns or AoE") && API.PlayerUnitInMeleeRangeCount >= AOEUnitNumber || (UseAscendance == "on mobcount or Cooldowns" || UseAscendance == "on mobcount") && API.PlayerUnitInMeleeRangeCount >= MobCount;
        bool IsFeralSpirit => (UseFeralSpirit == "with Cooldowns" || UseFeralSpirit == "with Cooldowns or AoE" || UseFeralSpirit == "on mobcount or Cooldowns") && IsCooldowns || UseFeralSpirit  == "always" || (UseFeralSpirit == "on AOE" || UseFeralSpirit == "with Cooldowns or AoE") && API.PlayerUnitInMeleeRangeCount >= AOEUnitNumber || (UseFeralSpirit == "on mobcount or Cooldowns" || UseFeralSpirit == "on mobcount") && API.PlayerUnitInMeleeRangeCount >= MobCount;
        bool IsEarthElemental => (UseEarthElemental == "with Cooldowns" || UseEarthElemental == "with Cooldowns or AoE" || UseEarthElemental == "on mobcount or Cooldowns") && IsCooldowns || UseEarthElemental == "always" || (UseEarthElemental == "on AOE" || UseEarthElemental == "with Cooldowns or AoE") && API.PlayerUnitInMeleeRangeCount >= AOEUnitNumber || (UseEarthElemental == "on mobcount or Cooldowns" || UseEarthElemental == "on mobcount") && API.PlayerUnitInMeleeRangeCount >= MobCount;
        bool IsSundering => (UseSundering == "with Cooldowns" || UseSundering == "with Cooldowns or AoE" || UseSundering == "on mobcount or Cooldowns") && IsCooldowns || UseSundering == "always" || (UseSundering == "on AOE" || UseSundering == "with Cooldowns or AoE") && API.PlayerUnitInMeleeRangeCount >= AOEUnitNumber || (UseSundering == "on mobcount or Cooldowns" || UseSundering == "on mobcount") && API.PlayerUnitInMeleeRangeCount >= MobCount;
        bool IsFireNova => (UseFireNova == "with Cooldowns" || UseFireNova == "with Cooldowns or AoE" || UseFireNova == "on mobcount or Cooldowns") && IsCooldowns || UseFireNova == "always" || (UseFireNova == "on AOE" || UseFireNova == "with Cooldowns or AoE") && API.PlayerUnitInMeleeRangeCount >= AOEUnitNumber || (UseFireNova == "on mobcount or Cooldowns" || UseFireNova == "on mobcount") && API.PlayerUnitInMeleeRangeCount >= MobCount;
        bool IsStormKeeper => (UseStormKeeper == "with Cooldowns" || UseStormKeeper == "with Cooldowns or AoE" || UseStormKeeper == "on mobcount or Cooldowns") && IsCooldowns || UseStormKeeper == "always" || (UseStormKeeper == "on AOE" || UseStormKeeper == "with Cooldowns or AoE") && API.PlayerUnitInMeleeRangeCount >= AOEUnitNumber || (UseStormKeeper == "on mobcount or Cooldowns" || UseStormKeeper == "on mobcount") && API.PlayerUnitInMeleeRangeCount >= MobCount;

        bool IsCovenant => (UseCovenant == "with Cooldowns" || UseCovenant == "with Cooldowns or AoE" || UseCovenant == "on mobcount or Cooldowns") && IsCooldowns || UseCovenant == "always" || (UseCovenant == "on AOE" || UseCovenant == "with Cooldowns or AoE") && API.PlayerUnitInMeleeRangeCount >= AOEUnitNumber || (UseCovenant == "on mobcount or Cooldowns" || UseCovenant == "on mobcount") && API.PlayerUnitInMeleeRangeCount >= MobCount;
        bool IsTrinkets1 => ((UseTrinket1 == "with Cooldowns" || UseTrinket1 == "with Cooldowns or AoE" || UseTrinket1 == "on mobcount or Cooldowns") && IsCooldowns || UseTrinket1 == "always" || (UseTrinket1 == "on AOE" || UseTrinket1 == "with Cooldowns or AoE") && API.PlayerUnitInMeleeRangeCount >= AOEUnitNumber || (UseTrinket1 == "on mobcount or Cooldowns" || UseTrinket1 == "on mobcount") && API.PlayerUnitInMeleeRangeCount >= MobCount) && isMelee;
        bool IsTrinkets2 => ((UseTrinket2 == "with Cooldowns" || UseTrinket2 == "with Cooldowns or AoE" || UseTrinket2 == "on mobcount or Cooldowns") && IsCooldowns || UseTrinket2 == "always" || (UseTrinket2 == "on AOE" || UseTrinket2 == "with Cooldowns or AoE") && API.PlayerUnitInMeleeRangeCount >= AOEUnitNumber || (UseTrinket2 == "on mobcount or Cooldowns" || UseTrinket2 == "on mobcount") && API.PlayerUnitInMeleeRangeCount >= MobCount) && isMelee;

        //CBProperties
        private string UseTrinket1 => CDUsageWithAOE[CombatRoutine.GetPropertyInt("Trinket1")];
        private string UseTrinket2 => CDUsageWithAOE[CombatRoutine.GetPropertyInt("Trinket2")];
        public new string[] CDUsage = new string[] { "Not Used", "with Cooldowns", "always" };
        public string[] Wolfoptions = new string[] { "only out of Fight", "only in Fight", "both" };
        int[] numbRaidList = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 33, 35, 36, 37, 38, 39, 40 };
        public new string[] CDUsageWithAOE = new string[] { "Not Used", "with Cooldowns", "on AOE", "with Cooldowns or AoE", "on mobcount", "on mobcount or Cooldowns", "always" };
        int[] numbList = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100 };
        private int MobCount => numbRaidList[CombatRoutine.GetPropertyInt("MobCount")];
        private string UseCovenant => CDUsageWithAOE[CombatRoutine.GetPropertyInt("UseCovenant")];
        private string UseAscendance => CDUsageWithAOE[CombatRoutine.GetPropertyInt(Ascendance)];
        private string UseFeralSpirit => CDUsageWithAOE[CombatRoutine.GetPropertyInt(FeralSpirit)];
        private string UseEarthElemental => CDUsageWithAOE[CombatRoutine.GetPropertyInt(EarthElemental)];
        private string UseSundering => CDUsageWithAOE[CombatRoutine.GetPropertyInt(Sundering)];
        private string UseFireNova => CDUsageWithAOE[CombatRoutine.GetPropertyInt(FireNova)];
        private string UseStormKeeper => CDUsageWithAOE[CombatRoutine.GetPropertyInt(StormKeeper)];
        private bool AutoWolf => CombatRoutine.GetPropertyBool("AutoWolf");
        private bool UsePotion => CombatRoutine.GetPropertyBool("Potion");
        private string UseAutoWolf => Wolfoptions[CombatRoutine.GetPropertyInt("UseAutoWolf")];
        private bool Windfury => CombatRoutine.GetPropertyBool(WindfuryTotem);
        private bool WeaponEnchant => CombatRoutine.GetPropertyBool("WeaponEnchant");
        private bool DoomWindLeggy => CombatRoutine.GetPropertyBool("Doom Winds");
        private bool SelfLightningShield => CombatRoutine.GetPropertyBool("LightningShield");
        private bool SelfEarthShield => CombatRoutine.GetPropertyBool("EarthShield");
        private int AstralShiftLifePercent => numbList[CombatRoutine.GetPropertyInt(AstralShift)];
        private int HealingStreamTotemLifePercent => numbList[CombatRoutine.GetPropertyInt(HealingStreamTotem)];
        private int HealingSurgeLifePercent => numbList[CombatRoutine.GetPropertyInt(HealingSurge)];
        private int HealingSurgeFreeLifePercent => numbList[CombatRoutine.GetPropertyInt("InstantHealingSurge")];
        private int PhialofSerenityLifePercent => numbList[CombatRoutine.GetPropertyInt(PhialofSerenity)];
        private int SpiritualHealingPotionLifePercent => numbList[CombatRoutine.GetPropertyInt(SpiritualHealingPotion)];


        private static readonly Stopwatch vesperwatch = new Stopwatch();
        private static readonly Stopwatch movingwatch = new Stopwatch();
        public override void Initialize()
        {
            CombatRoutine.Name = "Enhancement Shaman by smartie";
            API.WriteLog("Welcome to smartie`s Enhancement Shaman v2.9");
            API.WriteLog("For this rota you need to following macros");
            API.WriteLog("For Earthshield on Focus: /cast [@focus,help] Earth shield");

            //Spells
            CombatRoutine.AddSpell(LavaLash, 60103, "D3");
            CombatRoutine.AddSpell(CrashLightning, 187874, "D4");
            CombatRoutine.AddSpell(Ascendance, 114051, "D1");
            CombatRoutine.AddSpell(Windstrike, 115356, "D1");
            CombatRoutine.AddSpell(LightningShield, 192106, "F2");
            CombatRoutine.AddSpell(FeralSpirit, 51533, "D9");
            CombatRoutine.AddSpell(Stormstrike, 17364, "D1");
            CombatRoutine.AddSpell(Sundering, 197214, "D8");
            CombatRoutine.AddSpell(GhostWolf, 2645, "NumPad1");
            CombatRoutine.AddSpell(HealingSurge, 8004, "F5");
            CombatRoutine.AddSpell(LightningBolt, 188196, "D6");
            CombatRoutine.AddSpell(EarthenSpike, 188089, "NumPad8");
            CombatRoutine.AddSpell(WindShear, 57994, "F12");
            CombatRoutine.AddSpell(AstralShift, 108271, "F1");
            CombatRoutine.AddSpell(ChainLightning, 188443, "D5");
            CombatRoutine.AddSpell(ElementalBlast, 117014, "NumPad9");
            CombatRoutine.AddSpell(FrostShock, 196840, "F9");
            CombatRoutine.AddSpell(IceStrike, 342240, "D7");
            CombatRoutine.AddSpell(FireNova, 333974, "NumPad6");
            CombatRoutine.AddSpell(StormKeeper, 320137, "NumPad7");
            CombatRoutine.AddSpell(FlameShock, 188389, "D2");
            CombatRoutine.AddSpell(EarthElemental, 198103, "D0");
            CombatRoutine.AddSpell(EarthShield, 974, "F7");
            CombatRoutine.AddSpell(WindfuryTotem, 8512, "NumPad2");
            CombatRoutine.AddSpell(PrimalStrike, 73899, "D1");
            CombatRoutine.AddSpell(HealingStreamTotem,5394, "F4");
            CombatRoutine.AddSpell(PrimordialWave,326059, "D1");
            CombatRoutine.AddSpell(VesperTotem,324386, "D1");
            CombatRoutine.AddSpell(FaeTransfusion,328923, "D1");
            CombatRoutine.AddSpell(ChainHarvest, 320674, "D1");
            CombatRoutine.AddSpell(WindfuryWeapon, 33757);
            CombatRoutine.AddSpell(FlametongueWeapon, 318038);

            //Macros
            CombatRoutine.AddMacro(EarthShield + "Focus", "NumPad7");
            CombatRoutine.AddMacro("Trinket1", "F9");
            CombatRoutine.AddMacro("Trinket2", "F10");

            //Buffs
            CombatRoutine.AddBuff(CrashLightning, 187878);
            CombatRoutine.AddBuff(Ascendance, 114051);
            CombatRoutine.AddBuff(Stormbringer, 201846);
            CombatRoutine.AddBuff(HotHand, 215785);
            CombatRoutine.AddBuff(LightningShield, 192106);
            CombatRoutine.AddBuff(GhostWolf, 2645);
            CombatRoutine.AddBuff(EarthShield, 974);
            CombatRoutine.AddBuff(StormKeeper, 320137);
            CombatRoutine.AddBuff(Hailstorm, 334196);
            CombatRoutine.AddBuff(MaelstromWeapon, 344179);
            CombatRoutine.AddBuff(WindfuryTotem, 327942);
            CombatRoutine.AddBuff(PrimordialWave, 326059);
            CombatRoutine.AddBuff(VesperTotem, 324386);
            CombatRoutine.AddBuff(DoomWinds, 335903);
            CombatRoutine.AddBuff(PrimalLavaActuators, 335896);

            //Debuff
            CombatRoutine.AddDebuff(FlameShock, 188389);
            CombatRoutine.AddDebuff(LashingFlames, 334168);
            CombatRoutine.AddDebuff(DoomWinds,335904);

            //Toggle
            CombatRoutine.AddToggle("Windfury Totem");
            CombatRoutine.AddToggle("Sundering");
            CombatRoutine.AddToggle("Focus ES");

            //Item
            CombatRoutine.AddItem(PhialofSerenity, 177278);
            CombatRoutine.AddItem(SpiritualHealingPotion, 171267);
            CombatRoutine.AddItem(PotionofSpectralAgility, 171270);

            //Prop
            CombatRoutine.AddProp("MobCount", "Mobcount to use Cooldowns ", numbRaidList, " Mobcount to use Cooldowns", "Cooldowns", 3);
            CombatRoutine.AddProp("Trinket1", "Use " + "Trinket 1", CDUsageWithAOE, "Use " + "Trinket 1" + " always, with Cooldowns", "Trinkets", 0);
            CombatRoutine.AddProp("Trinket2", "Use " + "Trinket 2", CDUsageWithAOE, "Use " + "Trinket 2" + " always, with Cooldowns", "Trinkets", 0);
            CombatRoutine.AddProp("UseCovenant", "Use " + "Covenant Ability", CDUsageWithAOE, "Use " + "Covenant" + " always, with Cooldowns", "Covenant", 0);
            CombatRoutine.AddProp(Ascendance, "Use " + Ascendance, CDUsageWithAOE, "Use " + Ascendance + " always, with Cooldowns", "Cooldowns", 0);
            CombatRoutine.AddProp(FeralSpirit, "Use " + FeralSpirit, CDUsageWithAOE, "Use " + FeralSpirit + " always, with Cooldowns", "Cooldowns", 0);
            CombatRoutine.AddProp(EarthElemental, "Use " + EarthElemental, CDUsageWithAOE, "Use " + EarthElemental + " always, with Cooldowns", "Cooldowns", 0);
            CombatRoutine.AddProp(Sundering, "Use " + Sundering, CDUsageWithAOE, "Use " + Sundering + " always, with Cooldowns", "Cooldowns", 0);
            CombatRoutine.AddProp(FireNova, "Use " + FireNova, CDUsageWithAOE, "Use " + FireNova + " always, with Cooldowns", "Cooldowns", 0);
            CombatRoutine.AddProp(StormKeeper, "Use " + StormKeeper, CDUsageWithAOE, "Use " + StormKeeper + " always, with Cooldowns", "Cooldowns", 0);
            CombatRoutine.AddProp("LightningShield", "LightningShield", true, "Put" + LightningShield + " on ourselfs", "Generic");
            CombatRoutine.AddProp("EarthShield", "EarthShield", true, "Put" + EarthShield + " on ourselfs", "Generic");
            CombatRoutine.AddProp("UseAutoWolf", "Use " + GhostWolf, Wolfoptions, "Use " + GhostWolf + " only in Fight, out of Fight or both", "Generic", 0);
            CombatRoutine.AddProp("AutoWolf", "AutoWolf", true, "Will auto switch forms out of Fight", "Generic");
            CombatRoutine.AddProp("Potion", "Use DPS Potion", false, "Will auto use DPS Potion", "Cooldowns");
            CombatRoutine.AddProp(WindfuryTotem, "Windfury Totem only when not moving", false, "Rota will use Windfury Totem only when not moving", "Generic");
            CombatRoutine.AddProp("WeaponEnchant", "WeaponEnchant", true, "Will auto enchant your Weapons", "Generic");
            CombatRoutine.AddProp("Doom Winds", "Doom Winds Legendary", false, "Pls enable if you have that Legendary", "Generic");
            CombatRoutine.AddProp(PhialofSerenity, PhialofSerenity + " Life Percent", numbList, " Life percent at which" + PhialofSerenity + " is used, set to 0 to disable", "Defense", 40);
            CombatRoutine.AddProp(SpiritualHealingPotion, SpiritualHealingPotion + " Life Percent", numbList, " Life percent at which" + SpiritualHealingPotion + " is used, set to 0 to disable", "Defense", 40);
            CombatRoutine.AddProp(AstralShift, AstralShift + " Life Percent", numbList, "Life percent at which" + AstralShift + " is used, set to 0 to disable", "Defense", 40);
            CombatRoutine.AddProp(HealingStreamTotem, HealingStreamTotem + " Life Percent", numbList, "Life percent at which" + HealingStreamTotem + " is used, set to 0 to disable", "Defense", 20);
            CombatRoutine.AddProp("InstantHealingSurge", "Instant HealingSurge" + "Life Percent", numbList, "Life percent at which" + HealingSurge + " is used with Maelstorm Weapon Stacks, set to 0 to disable", "Defense", 60);
            CombatRoutine.AddProp(HealingSurge, HealingSurge + " Life Percent", numbList, "Life percent at which" + HealingSurge + " is used, set to 0 to disable", "Defense", 0);
        }
        public override void Pulse()
        {
            //API.WriteLog("check it all: "+ (API.CanCast(GhostWolf) && AutoWolf && (UseAutoWolf == "only in Fight" || UseAutoWolf == "both") && (API.TargetRange > 6 || API.TargetRange == 1) && !API.PlayerHasBuff(GhostWolf) && !API.PlayerIsMounted && API.PlayerIsMoving));
            if (!vesperwatch.IsRunning && API.LastSpellCastInGame == VesperTotem)
            {
                vesperwatch.Restart();
                API.WriteLog("Starting Vespermwatch.");
            }
            if (!movingwatch.IsRunning && API.PlayerIsMoving && API.PlayerIsInCombat)
            {
                movingwatch.Restart();
            }
            if (movingwatch.IsRunning && !API.PlayerIsMoving)
            {
                movingwatch.Stop();
                movingwatch.Reset();
            }
            if (movingwatch.IsRunning && !API.PlayerIsInCombat)
            {
                movingwatch.Stop();
                movingwatch.Reset();
            }
            if (vesperwatch.IsRunning && vesperwatch.ElapsedMilliseconds > 30000)
            {
                vesperwatch.Stop();
                vesperwatch.Reset();
                API.WriteLog("Resetting Vespermwatch.");
            }
            if (API.CanCast(GhostWolf) && movingwatch.ElapsedMilliseconds > 500 && API.PlayerIsInCombat && !API.PlayerIsMounted && API.PlayerCurrentCastTimeRemaining == 0 && AutoWolf && (UseAutoWolf == "only in Fight" || UseAutoWolf == "both") && !API.PlayerCanAttackTarget && PlayerLevel > 10 && !API.PlayerHasBuff(GhostWolf) && !API.PlayerIsMounted && API.PlayerIsMoving)
            {
                API.CastSpell(GhostWolf);
                return;
            }
        }
        public override void CombatPulse()
        {
            if (API.PlayerCurrentCastTimeRemaining > 40 || API.PlayerSpellonCursor)
                return;
            if (!API.PlayerIsMounted)
            {
                if (isInterrupt && API.CanCast(WindShear) && PlayerLevel >= 12 && iskickrange)
                {
                    API.CastSpell(WindShear);
                    return;
                }
                if (PlayerRaceSettings == "Tauren" && API.CanCast(RacialSpell1) && isInterrupt && !API.PlayerIsMoving && isRacial && isMelee && API.SpellISOnCooldown(WindShear))
                {
                    API.CastSpell(RacialSpell1);
                    return;
                }
                if (API.CanCast(AstralShift) && PlayerLevel >= 42 && API.PlayerHealthPercent <= AstralShiftLifePercent)
                {
                    API.CastSpell(AstralShift);
                    return;
                }
                if (API.PlayerItemCanUse(PhialofSerenity) && !API.MacroIsIgnored(PhialofSerenity) && API.PlayerItemRemainingCD(PhialofSerenity) == 0 && API.PlayerHealthPercent <= PhialofSerenityLifePercent)
                {
                    API.CastSpell(PhialofSerenity);
                    return;
                }
                if (API.PlayerItemCanUse(SpiritualHealingPotion) && !API.MacroIsIgnored(SpiritualHealingPotion) && API.PlayerItemRemainingCD(SpiritualHealingPotion) == 0 && API.PlayerHealthPercent <= SpiritualHealingPotionLifePercent)
                {
                    API.CastSpell(SpiritualHealingPotion);
                    return;
                }
                if (API.CanCast(HealingSurge) && PlayerLevel >= 4 && API.PlayerMana >= 24 && API.PlayerBuffStacks(MaelstromWeapon) >= 5 && API.PlayerHealthPercent <= HealingSurgeFreeLifePercent)
                {
                    API.CastSpell(HealingSurge);
                    return;
                }
                if (API.CanCast(HealingSurge) && PlayerLevel >= 4 && API.PlayerMana >= 24 && !API.PlayerIsMoving && API.PlayerHealthPercent <= HealingSurgeLifePercent)
                {
                    API.CastSpell(HealingSurge);
                    return;
                }
                if (API.CanCast(HealingStreamTotem) && PlayerLevel >= 28 && API.PlayerMana >= 9 && !API.PlayerIsMoving && API.PlayerHealthPercent <= HealingStreamTotemLifePercent)
                {
                    API.CastSpell(HealingStreamTotem);
                    return;
                }
                if (API.CanCast(EarthShield) && API.PlayerMana >= 10 && SelfEarthShield && !IsFocus && !API.PlayerHasBuff(LightningShield) && !API.PlayerHasBuff(EarthShield) && TalentEarthShield)
                {
                    API.CastSpell(EarthShield);
                    return;
                }
                if (API.CanCast(LightningShield) && PlayerLevel >= 9 && API.PlayerMana >= 2 && SelfLightningShield && !API.PlayerHasBuff(EarthShield) && !API.PlayerHasBuff(LightningShield) && API.PlayerHealthPercent > 0)
                {
                    API.CastSpell(LightningShield);
                    return;
                }
                //Focus
                if (API.CanCast(EarthShield) && IsFocus && API.FocusRange < 40 && API.FocusHealthPercent != 0 && !API.FocusHasBuff(EarthShield) && API.PlayerMana >= 10 && TalentEarthShield)
                {
                    API.CastSpell(EarthShield + "Focus");
                    return;
                }
                rotation();
                return;
            }
        }
        public override void OutOfCombatPulse()
        {
            if (API.PlayerCurrentCastTimeRemaining > 40 || API.PlayerSpellonCursor)
                return;
            if (API.CanCast(HealingSurge) && PlayerLevel >= 4 && API.PlayerMana >= 24 && API.PlayerBuffStacks(MaelstromWeapon) >= 5 && API.PlayerHealthPercent <= HealingSurgeFreeLifePercent)
            {
                API.CastSpell(HealingSurge);
                return;
            }
            if (API.CanCast(GhostWolf) && AutoWolf && (UseAutoWolf == "only out of Fight" || UseAutoWolf == "both") && PlayerLevel > 10 && !API.PlayerHasBuff(GhostWolf) && !API.PlayerIsMounted && API.PlayerIsMoving)
            {
                API.CastSpell(GhostWolf);
                return;
            }
            if (API.CanCast(EarthShield) && API.PlayerMana >= 10 && SelfEarthShield && !IsFocus && !API.PlayerHasBuff(LightningShield) && !API.PlayerHasBuff(EarthShield) && TalentEarthShield)
            {
                API.CastSpell(EarthShield);
                return;
            }
            if (API.CanCast(LightningShield) && PlayerLevel >= 9 && API.PlayerMana >= 2 && SelfLightningShield && !API.PlayerHasBuff(EarthShield) && !API.PlayerHasBuff(LightningShield) && API.PlayerHealthPercent > 0)
            {
                API.CastSpell(LightningShield);
                return;
            }
            //Focus
            if (API.CanCast(EarthShield) && IsFocus && API.FocusRange < 40 && API.FocusHealthPercent != 0 && !API.FocusHasBuff(EarthShield) && API.PlayerMana >= 10 && TalentEarthShield)
            {
                API.CastSpell(EarthShield + "Focus");
                return;
            }
            if (API.CanCast(WindfuryWeapon) && WeaponEnchant && API.LastSpellCastInGame != (WindfuryWeapon) && API.PlayerWeaponBuffDuration(true) < 30000)
            {
                API.CastSpell(WindfuryWeapon);
                return;
            }
            if (API.CanCast(FlametongueWeapon) && WeaponEnchant && API.LastSpellCastInGame != (FlametongueWeapon) && API.PlayerWeaponBuffDuration(false) < 30000)
            {
                API.CastSpell(FlametongueWeapon);
                return;
            }
        }
        private void rotation()
        {
            //Weapon Buffs
            if (API.CanCast(WindfuryWeapon) && WeaponEnchant && API.LastSpellCastInGame != (WindfuryWeapon) && API.PlayerWeaponBuffDuration(true) < 3000)
            {
                API.CastSpell(WindfuryWeapon);
                return;
            }
            if (API.CanCast(FlametongueWeapon) && WeaponEnchant && API.LastSpellCastInGame != (FlametongueWeapon) && API.PlayerWeaponBuffDuration(false) < 3000)
            {
                API.CastSpell(FlametongueWeapon);
                return;
            }
            //Potion
            if (API.PlayerItemCanUse(PotionofSpectralAgility) && !API.MacroIsIgnored(PotionofSpectralAgility) && API.PlayerItemRemainingCD(PotionofSpectralAgility) == 0 && IsCooldowns && API.PlayerHasBuff(Ascendance))
            {
                API.CastSpell(PotionofSpectralAgility);
                return;
            }
            //actions +=/ blood_fury,if= !talent.ascendance.enabled | buff.ascendance.up | cooldown.ascendance.remains > 50
            if (PlayerRaceSettings == "Orc" && API.CanCast(RacialSpell1) && isRacial && IsCooldowns && isMelee && (!TalentAscendance || API.PlayerHasBuff(Ascendance) || TalentAscendance && API.SpellCDDuration(Ascendance) > 5000))
            {
                API.CastSpell(RacialSpell1);
                return;
            }
            //actions +=/ berserking,if= !talent.ascendance.enabled | buff.ascendance.up
            if (PlayerRaceSettings == "Troll" && API.CanCast(RacialSpell1) && isRacial && IsCooldowns && isMelee && (!TalentAscendance || API.PlayerHasBuff(Ascendance)))
            {
                API.CastSpell(RacialSpell1);
                return;
            }
            //actions +=/ fireblood,if= !talent.ascendance.enabled | buff.ascendance.up | cooldown.ascendance.remains > 50
            if (PlayerRaceSettings == "Dark Iron Dwarf" && API.CanCast(RacialSpell1) && isRacial && IsCooldowns && isMelee && (!TalentAscendance || API.PlayerHasBuff(Ascendance) || TalentAscendance && API.SpellCDDuration(Ascendance) > 5000))
            {
                API.CastSpell(RacialSpell1);
                return;
            }
            //actions +=/ ancestral_call,if= !talent.ascendance.enabled | buff.ascendance.up | cooldown.ascendance.remains > 50
            if (PlayerRaceSettings == "Mag'har Orc" && API.CanCast(RacialSpell1) && isRacial && IsCooldowns && isMelee && (!TalentAscendance || API.PlayerHasBuff(Ascendance) || TalentAscendance && API.SpellCDDuration(Ascendance) > 5000))
            {
                API.CastSpell(RacialSpell1);
                return;
            }
            //actions +=/ bag_of_tricks,if= !talent.ascendance.enabled | !buff.ascendance.up
            if (PlayerRaceSettings == "Vulpera" && API.CanCast(RacialSpell1) && isRacial && IsCooldowns && isMelee && (!TalentAscendance || API.PlayerHasBuff(Ascendance) || !IsAscendance || TalentAscendance && API.SpellCDDuration(Ascendance) > 9000))
            {
                API.CastSpell(RacialSpell1);
                return;
            }
            if (API.PlayerTrinketIsUsable(1) && API.PlayerTrinketRemainingCD(1) == 0 && !API.MacroIsIgnored("Trinket1") && !isExplosive && IsTrinkets1)
            {
                API.CastSpell("Trinket1");
                return;
            }
            if (API.PlayerTrinketIsUsable(2) && API.PlayerTrinketRemainingCD(2) == 0 && !API.MacroIsIgnored("Trinket2") && !isExplosive && IsTrinkets2)
            {
                API.CastSpell("Trinket2");
                return;
            }
            if (API.CanCast(WindfuryTotem) && PlayerLevel >= 49 && WindfuryToggle && !DoomWindLeggy && API.PlayerMana >= 12 && API.LastSpellCastInGame != (WindfuryTotem) && API.PlayerBuffTimeRemaining(WindfuryTotem) < 100 && isMelee && (!API.PlayerIsMoving && Windfury || !Windfury))
            {
                API.CastSpell(WindfuryTotem);
                return;
            }
            if (API.CanCast(WindfuryTotem) && WindfuryToggle && DoomWindLeggy && API.LastSpellCastInGame != (WindfuryTotem) && (API.PlayerDebuffRemainingTime(DoomWinds) == 0 || API.PlayerBuffTimeRemaining(WindfuryTotem) < 100) && API.PlayerMana >= 12 && isMelee && (!API.PlayerIsMoving && Windfury || !Windfury))
            {
                API.CastSpell(WindfuryTotem);
                return;
            }
            //actions+=/feral_spirit
            if (API.CanCast(FeralSpirit) && PlayerLevel >= 34 && isMelee && !isExplosive && IsFeralSpirit)
            {
                API.CastSpell(FeralSpirit);
                return;
            }
            //actions+=/ascendance,if=raid_event.adds.in>=90|active_enemies>1
            if (API.CanCast(Ascendance) && isMelee && TalentAscendance && !API.PlayerHasBuff(Ascendance) && !isExplosive && IsAscendance)
            {
                API.CastSpell(Ascendance);
                return;
            }
            // Single Target rota
            if (API.PlayerUnitInMeleeRangeCount < AOEUnitNumber || !IsAOE)
            {
                //actions.single=windstrike
                if (API.CanCast(Windstrike) && PlayerLevel >= 15 && API.PlayerHasBuff(Ascendance) && isMelee)
                {
                    API.CastSpell(Windstrike);
                    return;
                }
                //actions.single+=/lava_lash,if=buff.hot_hand.up|(runeforge.primal_lava_actuators.equipped&buff.primal_lava_actuators.stack>6)
                if (API.CanCast(LavaLash) && PlayerLevel >= 11 && API.PlayerMana >= 4 && isMelee && (API.PlayerHasBuff(HotHand) || API.PlayerBuffStacks(PrimalLavaActuators) > 6))
                {
                    API.CastSpell(LavaLash);
                    return;
                }
                //actions.single+=/primordial_wave,if=!buff.primordial_wave.up
                if (API.CanCast(PrimordialWave) && IsCovenant && !isExplosive && PlayerCovenantSettings == "Necrolord" && API.PlayerMana >= 3 && !API.PlayerHasBuff(PrimordialWave) && isinrange)
                {
                    API.CastSpell(PrimordialWave);
                    return;
                }
                //actions.single+=/stormstrike,if=runeforge.doom_winds.equipped&buff.doom_winds.up
                if (API.CanCast(Stormstrike) && PlayerLevel >= 20 && API.PlayerMana >= 2 && isMelee && API.PlayerBuffTimeRemaining(DoomWinds) > 0)
                {
                    API.CastSpell(Stormstrike);
                    return;
                }
                //actions.single +=/ crash_lightning,if= runeforge.doom_winds.equipped & buff.doom_winds.up
                if (API.CanCast(CrashLightning) && PlayerLevel >= 38 && API.PlayerMana >= 6 && isMelee && API.PlayerBuffTimeRemaining(DoomWinds) > 0)
                {
                    API.CastSpell(CrashLightning);
                    return;
                }
                //actions.single +=/ ice_strike,if= runeforge.doom_winds.equipped & buff.doom_winds.up
                if (API.CanCast(IceStrike) && isMelee && API.PlayerMana >= 4 && TalentIceStrike && API.PlayerBuffTimeRemaining(DoomWinds) > 0)
                {
                    API.CastSpell(IceStrike);
                    return;
                }
                //actions.single+=/flame_shock,if=!ticking
                if (API.CanCast(FlameShock) && PlayerLevel >= 3 && API.PlayerMana >= 2 && !API.TargetHasDebuff(FlameShock) && isinrange)
                {
                    API.CastSpell(FlameShock);
                    return;
                }
                //actions.single+=/vesper_totem
                if (API.CanCast(VesperTotem) && !isExplosive && PlayerCovenantSettings == "Kyrian" && IsCovenant && !API.PlayerIsMoving && API.PlayerMana >= 10 && isMelee && !vesperwatch.IsRunning)
                {
                    API.CastSpell(VesperTotem);
                    return;
                }
                //actions.single+=/frost_shock,if=buff.hailstorm.up
                if (API.CanCast(FrostShock) && PlayerLevel >= 17 && API.PlayerMana >= 1 && isinrange && API.PlayerHasBuff(Hailstorm))
                {
                    API.CastSpell(FrostShock);
                    return;
                }
                //actions.single+=/earthen_spike
                if (API.CanCast(EarthenSpike) && isMelee && API.PlayerMana >= 4 && TalentEarthenSpike)
                {
                    API.CastSpell(EarthenSpike);
                    return;
                }
                //actions.single+=/fae_transfusion
                if (API.CanCast(FaeTransfusion) && !isExplosive && isMelee && !API.PlayerIsMoving && API.PlayerMana >= 8 && PlayerCovenantSettings == "Night Fae" && IsCovenant)
                {
                    API.CastSpell(FaeTransfusion);
                    return;
                }
                //actions.single+=/chain_lightning,if=buff.stormkeeper.up
                if (API.CanCast(ChainLightning) && API.PlayerHasBuff(StormKeeper) && API.PlayerMana >= 1 && isinrange)
                {
                    API.CastSpell(ChainLightning);
                    return;
                }
                //actions.single+=/elemental_blast,if=buff.maelstrom_weapon.stack>=5
                if (API.CanCast(ElementalBlast) && !isExplosive && API.PlayerBuffStacks(MaelstromWeapon) >= 5 && isinrange && API.PlayerMana >= 3 && TalentElementalBlast)
                {
                    API.CastSpell(ElementalBlast);
                    return;
                }
                //actions.single+=/chain_harvest,if=buff.maelstrom_weapon.stack>=5
                if (API.CanCast(ChainHarvest) && !isExplosive && API.PlayerBuffStacks(MaelstromWeapon) >= 5 && isinrange && API.PlayerMana >= 10 && PlayerCovenantSettings == "Venthyr" && IsCovenant)
                {
                    API.CastSpell(ChainHarvest);
                    return;
                }
                //actions.single+=/lightning_bolt,if=buff.maelstrom_weapon.stack=10
                if (API.CanCast(LightningBolt) && API.PlayerBuffStacks(MaelstromWeapon) == 10 && API.PlayerMana >= 1 && isinrange)
                {
                    API.CastSpell(LightningBolt);
                    return;
                }
                //actions.single+=/stormstrike low level
                if (API.CanCast(PrimalStrike) && PlayerLevel < 20 && API.PlayerMana >= 10 && isMelee)
                {
                    API.CastSpell(PrimalStrike);
                    return;
                }
                //actions.single+=/stormstrike
                if (API.CanCast(Stormstrike) && PlayerLevel >= 20 && API.PlayerMana >= 2 && isMelee)
                {
                    API.CastSpell(Stormstrike);
                    return;
                }
                //actions.single+=/lava_lash
                if (API.CanCast(LavaLash) && PlayerLevel >= 11 && API.PlayerMana >= 4 && isMelee)
                {
                    API.CastSpell(LavaLash);
                    return;
                }
                //actions.single+=/crash_lightning
                if (API.CanCast(CrashLightning) && PlayerLevel >= 38 && API.PlayerMana >= 6 && isMelee)
                {
                    API.CastSpell(CrashLightning);
                    return;
                }
                //actions.single+=/flame_shock,target_if=refreshable
                if (API.CanCast(FlameShock) && !isExplosive && PlayerLevel >= 3 && API.PlayerMana >= 2 && API.TargetDebuffRemainingTime(FlameShock) < 500 && isinrange)
                {
                    API.CastSpell(FlameShock);
                    return;
                }
                //actions.single+=/lava_lash
                if (API.CanCast(LavaLash) && PlayerLevel >= 11 && API.PlayerMana >= 4 && isMelee)
                {
                    API.CastSpell(LavaLash);
                    return;
                }
                //actions.single+=/frost_shock
                if (API.CanCast(FrostShock) && PlayerLevel >= 17 && API.PlayerMana >= 1 && isinrange)
                {
                    API.CastSpell(FrostShock);
                    return;
                }
                //actions.single+=/stormkeeper,if=buff.maelstrom_weapon.stack>=5
                if (API.CanCast(StormKeeper) && API.PlayerBuffStacks(MaelstromWeapon) >= 5 && !API.PlayerIsMoving && isinrange && TalentStormkeeper && IsStormKeeper)
                {
                    API.CastSpell(StormKeeper);
                    return;
                }
                //actions.single+=/ice_strike
                if (API.CanCast(IceStrike) && isMelee && API.PlayerMana >= 4 && TalentIceStrike)
                {
                    API.CastSpell(IceStrike);
                    return;
                }
                //actions.single+=/sundering,if=raid_event.adds.in>=40
                if (API.CanCast(Sundering) && !isExplosive && API.PlayerMana >= 6 && SunderingToggle && isMelee && TalentSundering && IsSundering)
                {
                    API.CastSpell(Sundering);
                    return;
                }
                //actions.single+=/fire_nova,if=active_dot.flame_shock
                if (API.CanCast(FireNova) && !isExplosive && isMelee && API.PlayerMana >= 6 && TalentFireNova && API.TargetHasDebuff(FlameShock) && IsFireNova)
                {
                    API.CastSpell(FireNova);
                    return;
                }
                //actions.single+=/lightning_bolt,if=buff.maelstrom_weapon.stack>=5
                if (API.CanCast(LightningBolt) && API.PlayerBuffStacks(MaelstromWeapon) >= 5 && API.PlayerMana >= 1 && isinrange)
                {
                    API.CastSpell(LightningBolt);
                    return;
                }
                //actions.single+=/earth_elemental
                if (API.CanCast(EarthElemental) && !isExplosive && isMelee && PlayerLevel >= 37 && IsEarthElemental)
                {
                    API.CastSpell(EarthElemental);
                    return;
                }
                //actions.single+=/windfury_totem,if=buff.windfury_totem.remains<30
                if (API.CanCast(WindfuryTotem) && PlayerLevel >= 49 && WindfuryToggle && !DoomWindLeggy && API.PlayerMana >= 12 && API.PlayerBuffTimeRemaining(WindfuryTotem) < 3000 && isMelee && (!API.PlayerIsMoving && Windfury || !Windfury))
                {
                    API.CastSpell(WindfuryTotem);
                    return;
                }
                if (API.CanCast(GhostWolf) && movingwatch.ElapsedMilliseconds > 500 && AutoWolf && (UseAutoWolf == "only in Fight" || UseAutoWolf == "both") && !isMelee && PlayerLevel > 10 && !API.PlayerHasBuff(GhostWolf) && !API.PlayerIsMounted && API.PlayerIsMoving)
                {
                    API.CastSpell(GhostWolf);
                    return;
                }
            }
            //AoE rota
            if (API.PlayerUnitInMeleeRangeCount >= AOEUnitNumber && IsAOE)
            {
                //actions.aoe=windstrike,if=buff.crash_lightning.up
                if (API.CanCast(Windstrike) && PlayerLevel >= 15 && API.PlayerHasBuff(Ascendance) && isMelee && API.PlayerHasBuff(CrashLightning))
                {
                    API.CastSpell(Windstrike);
                    return;
                }
                //actions.aoe+=/crash_lightning,if=runeforge.doom_winds.equipped&buff.doom_winds.up
                if (API.CanCast(CrashLightning) && PlayerLevel >= 38 && API.PlayerMana >= 6 && isMelee && API.PlayerBuffTimeRemaining(DoomWinds) > 0)
                {
                    API.CastSpell(CrashLightning);
                    return;
                }
                //actions.aoe+=/frost_shock,if=buff.hailstorm.up
                if (API.CanCast(FrostShock) && PlayerLevel >= 17 && API.PlayerMana >= 1 && isinrange && API.PlayerHasBuff(Hailstorm))
                {
                    API.CastSpell(FrostShock);
                    return;
                }
                //actions.aoe+=/sundering
                if (API.CanCast(Sundering) && !isExplosive && API.PlayerMana >= 6 && SunderingToggle && isMelee && TalentSundering && IsSundering)
                {
                    API.CastSpell(Sundering);
                    return;
                }
                //actions.aoe+=/flame_shock,target_if=refreshable,cycle_targets=1,if=talent.fire_nova.enabled|talent.lashing_flames.enabled|covenant.necrolord
                if (API.CanCast(FlameShock) && !isExplosive && PlayerLevel >= 3 && API.PlayerMana >= 2 && !API.TargetHasDebuff(FlameShock) && isinrange && (TalentFireNova || TalentLashingFlames || PlayerCovenantSettings == "Necrolord"))
                {
                    API.CastSpell(FlameShock);
                    return;
                }
                //actions.aoe+=/primordial_wave,target_if=min:dot.flame_shock.remains,cycle_targets=1,if=!buff.primordial_wave.up
                if (API.CanCast(PrimordialWave) && !isExplosive && IsCovenant && PlayerCovenantSettings == "Necrolord" && API.PlayerMana >= 3 && API.TargetHasDebuff(FlameShock) && !API.PlayerHasBuff(PrimordialWave) && isinrange)
                {
                    API.CastSpell(PrimordialWave);
                    return;
                }
                //actions.aoe+=/fire_nova,if=active_dot.flame_shock>=3
                if (API.CanCast(FireNova) && !isExplosive && isMelee && API.PlayerMana >= 6 && TalentFireNova && API.TargetHasDebuff(FlameShock) && IsFireNova)
                {
                    API.CastSpell(FireNova);
                    return;
                }
                //actions.aoe+=/vesper_totem
                if (API.CanCast(VesperTotem) && !isExplosive && PlayerCovenantSettings == "Kyrian" && IsCovenant && !API.PlayerIsMoving && API.PlayerMana >= 10 && isMelee && !vesperwatch.IsRunning)
                {
                    API.CastSpell(VesperTotem);
                    return;
                }
                //actions.aoe+=/lightning_bolt,if=buff.primordial_wave.up&buff.maelstrom_weapon.stack>=5
                if (API.CanCast(LightningBolt) && API.PlayerHasBuff(PrimordialWave) && API.PlayerBuffStacks(MaelstromWeapon) >= 5 && API.PlayerMana >= 1 && isinrange)
                {
                    API.CastSpell(LightningBolt);
                    return;
                }
                //actions.aoe+=/chain_lightning,if=buff.stormkeeper.up
                if (API.CanCast(ChainLightning) && PlayerLevel >= 24 && API.PlayerHasBuff(StormKeeper) && API.PlayerMana >= 1 && isinrange)
                {
                    API.CastSpell(ChainLightning);
                    return;
                }
                //actions.aoe+=/crash_lightning,if=talent.crashing_storm.enabled|buff.crash_lightning.down
                if (API.CanCast(CrashLightning) && PlayerLevel >= 38 && (TalentCrashingStorm || !API.PlayerHasBuff(CrashLightning)) && API.PlayerMana >= 6 && isMelee)
                {
                    API.CastSpell(CrashLightning);
                    return;
                }
                //actions.aoe+=/lava_lash,target_if=min:debuff.lashing_flames.remains,cycle_targets=1,if=talent.lashing_flames.enabled
                if (API.CanCast(LavaLash) && PlayerLevel >= 11 && API.PlayerMana >= 4 && isMelee && TalentLashingFlames && API.TargetDebuffRemainingTime(LashingFlames) < 150)
                {
                    API.CastSpell(LavaLash);
                    return;
                }
                //actions.aoe+=/lava_lash,if=buff.crash_lightning.up&(buff.hot_hand.up|(runeforge.primal_lava_actuators.equipped&buff.primal_lava_actuators.stack>6))
                if (API.CanCast(LavaLash) && PlayerLevel >= 11 && API.PlayerMana >= 4 && isMelee && API.PlayerHasBuff(CrashLightning) && (API.PlayerHasBuff(HotHand) || API.PlayerBuffStacks(PrimalLavaActuators) > 6))
                {
                    API.CastSpell(LavaLash);
                    return;
                }
                //actions.aoe+=/stormstrike,if=buff.crash_lightning.up
                if (API.CanCast(Stormstrike) && PlayerLevel >= 20 && API.PlayerMana >= 2 && isMelee && API.PlayerHasBuff(CrashLightning))
                {
                    API.CastSpell(Stormstrike);
                    return;
                }
                //actions.aoe+=/crash_lightning
                if (API.CanCast(CrashLightning) && PlayerLevel >= 38 && API.PlayerMana >= 6 && isMelee)
                {
                    API.CastSpell(CrashLightning);
                    return;
                }
                //actions.aoe+=/chain_harvest,if=buff.maelstrom_weapon.stack>=5
                if (API.CanCast(ChainHarvest) && !isExplosive && API.PlayerBuffStacks(MaelstromWeapon) >= 5 && isinrange && API.PlayerMana >= 10 && PlayerCovenantSettings == "Venthyr" && IsCovenant)
                {
                    API.CastSpell(ChainHarvest);
                    return;
                }
                //actions.aoe+=/elemental_blast,if=buff.maelstrom_weapon.stack>=5
                if (API.CanCast(ElementalBlast) && API.PlayerUnitInMeleeRangeCount < 3 && API.PlayerBuffStacks(MaelstromWeapon) >= 5 && isinrange && API.PlayerMana >= 3 && TalentElementalBlast)
                {
                    API.CastSpell(ElementalBlast);
                    return;
                }
                //actions.aoe+=/stormkeeper,if=buff.maelstrom_weapon.stack>=5
                if (API.CanCast(StormKeeper) && !isExplosive && !API.PlayerIsMoving && IsStormKeeper && API.PlayerBuffStacks(MaelstromWeapon) >= 5 && isinrange && TalentStormkeeper)
                {
                    API.CastSpell(StormKeeper);
                    return;
                }
                //actions.aoe+=/chain_lightning,if=buff.maelstrom_weapon.stack=10
                if (API.CanCast(ChainLightning) && PlayerLevel >= 24 && API.PlayerBuffStacks(MaelstromWeapon) == 10 && API.PlayerMana >= 1 && isinrange)
                {
                    API.CastSpell(ChainLightning);
                    return;
                }
                //actions.aoe+=/windstrike
                if (API.CanCast(Windstrike) && PlayerLevel >= 15 && API.PlayerHasBuff(Ascendance) && isMelee)
                {
                    API.CastSpell(Windstrike);
                    return;
                }
                //actions.aoe+=/stormstrike
                if (API.CanCast(Stormstrike) && PlayerLevel >= 20 && API.PlayerMana >= 2 && isMelee)
                {
                    API.CastSpell(Stormstrike);
                    return;
                }
                //actions.aoe+=/lava_lash
                if (API.CanCast(LavaLash) && PlayerLevel >= 11 && API.PlayerMana >= 4 && isMelee)
                {
                    API.CastSpell(LavaLash);
                    return;
                }
                //actions.aoe+=/flame_shock,target_if=refreshable,cycle_targets=1,if=talent.fire_nova.enabled
                if (API.CanCast(FlameShock) && PlayerLevel >= 3 && API.PlayerMana >= 2 && API.TargetDebuffRemainingTime(FlameShock) < 100 && isinrange)
                {
                    API.CastSpell(FlameShock);
                    return;
                }
                //actions.aoe+=/fae_transfusion
                if (API.CanCast(FaeTransfusion) && !isExplosive && isMelee && !API.PlayerIsMoving && API.PlayerMana >= 8 && PlayerCovenantSettings == "Night Fae" && IsCovenant)
                {
                    API.CastSpell(FaeTransfusion);
                    return;
                }
                //actions.aoe+=/frost_shock
                if (API.CanCast(FrostShock) && PlayerLevel >= 17 && API.PlayerMana >= 1 && isinrange)
                {
                    API.CastSpell(FrostShock);
                    return;
                }
                //actions.aoe+=/ice_strike
                if (API.CanCast(IceStrike) && isMelee && API.PlayerMana >= 4 && TalentIceStrike)
                {
                    API.CastSpell(IceStrike);
                    return;
                }
                //actions.aoe+=/chain_lightning,if=buff.maelstrom_weapon.stack>=5
                if (API.CanCast(ChainLightning) && PlayerLevel >= 24 && API.PlayerBuffStacks(MaelstromWeapon) >= 5 && API.PlayerMana >= 1 && isinrange)
                {
                    API.CastSpell(ChainLightning);
                    return;
                }
                //actions.aoe+=/earthen_spike
                if (API.CanCast(EarthenSpike) && isMelee && API.PlayerMana >= 4 && TalentEarthenSpike)
                {
                    API.CastSpell(EarthenSpike);
                    return;
                }
                //actions.aoe+=/stormstrike low level
                if (API.CanCast(PrimalStrike) && PlayerLevel < 20 && API.PlayerMana >= 10 && isMelee)
                {
                    API.CastSpell(PrimalStrike);
                    return;
                }
                //actions.aoe+=/earth_elemental
                if (API.CanCast(EarthElemental) && isMelee && PlayerLevel >= 37 && IsEarthElemental)
                {
                    API.CastSpell(EarthElemental);
                    return;
                }
                //actions.aoe+=/windfury_totem,if=buff.windfury_totem.remains<30
                if (API.CanCast(WindfuryTotem) && PlayerLevel >= 49 && WindfuryToggle && !DoomWindLeggy && API.PlayerMana >= 12 && API.PlayerBuffTimeRemaining(WindfuryTotem) < 3000 && isMelee && (!API.PlayerIsMoving && Windfury || !Windfury))
                {
                    API.CastSpell(WindfuryTotem);
                    return;
                }
                if (API.CanCast(GhostWolf) && movingwatch.ElapsedMilliseconds > 500 && AutoWolf && (UseAutoWolf == "only in Fight" || UseAutoWolf == "both") && !isMelee && !API.PlayerHasBuff(GhostWolf) && !API.PlayerIsMounted && API.PlayerIsMoving)
                {
                    API.CastSpell(GhostWolf);
                    return;
                }
            }
        }
    }
}