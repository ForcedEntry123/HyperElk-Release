﻿//Change Log
//V2.0 - Racial Add, Healing Stream totem fix, Sprirtwalkers Add. Devouring Fix

using System.Linq;
using System.Diagnostics;

namespace HyperElk.Core
{
    public class RestoShaman : CombatRoutine
    {
        //Spell Strings
        private string ManaTideTotem = "Mana Tide Totem";
        private string WaterShield = "Water Shield";
        private string LightningShield = "Lightning Shield";
        private string PrimalStrike = "Primal Strike";
        private string FlametongueWeapon = "Flametongue Weapon";
        private string FrostShock = "Frost Shock";
        private string EarthShield = "Earth Shield";
        private string Riptide = "Riptide";
        private string HealingSurge = "Healing Surge";
        private string HealingWave = "Healing Wave";
        private string ChainHeal = "Chain Heal";
        private string HealingRain = "Healing Rain";
        private string HealingStreamTotem = "Healing Stream Totem";
        private string FlameShock = "Flame Shock";
        private string LavaBurst = "Lave Burst";
        private string LightningBolt = "Lightning Bolt";
        private string ChainLightning = "Chain Lightning";
        private string HealingTideTotem = "Healing Tide Totem";
        private string SpiritLinkTotem = "Spirit Link Totem";
        private string SpiritWalkersGrace = "Spiritwalker's Grace";
        private string AstralShift = "Astral Shift";
        private string Hex = "Hex";
        private string Purge = "Purge";
        private string WindShear = "Wind Shear";
        private string GhostWolf = "Ghost Wolf";
        private string TremorTotem = "Tremor Totem";
        private string EarthElemental = "Earth Elemental";
        private string VesperTotem = "Vesper Totem";
        private string ChainHarvest = "Chain Harvest";
        private string PrimordialWave = "Primordial Wave";
        private string FaeTransfusion = "Fae Transfusion";
        private string UnleashLife = "Unleash Life";
        private string SurgeofEarth = "Surge of Earth";
        private string EarthgrabTotem = "Earthgrab Totem";
        private string EarthenWallTotem = "Earthen Wall Totem";
        private string AncestralProtectionTotem = "Ancestral Protection Totem";
        private string WindRushTotem = "Wind Rush Totem";
        private string Downpour = "Downpour";
        private string CloudburstTotem = "Cloudburst Totem";
        private string HighTide = "High Tide";
        private string Wellspring = "Wellspring";
        private string Ascendance = "Ascendance";
        private string AscendanceUse = "Ascendance Usage";
        private string AoE = "AOE";
        private string AoEP = "AOE Party";
        private string AoER = "AOE Raid";
        private string AoEDPS = "AOEDPS";
        private string AoEDPSRaid = "AOEDPS Raid";
        private string AoEDPSHRaid = "AOEDPS Health Raid";
        private string AoEDPSH = "AOEDPS Health";
        private string Soulshape = "Soulshape";
        private string PhialofSerenity = "Phial of Serenity";
        private string SpiritualHealingPotion = "Spiritual Healing Potion";
        private string Trinket1 = "Trinket1";
        private string Trinket2 = "Trinket2";
        private string Mana = "Mana";
        private string Trinket = "Trinket";
        private string Party1 = "party1";
        private string Party2 = "party2";
        private string Party3 = "party3";
        private string Party4 = "party4";
        private string Player = "player";
        private string AoERaid = "AOE Healing Raid";
        private string Fleshcraft = "Fleshcraft";
        private string LavaSurge = "Lava Surge";
        private string DungeonCD = "Dungeon CD AOE";
        private string RaidCD = "Raid CD AoE";
        private string MO = "MO";
        private string Quake = "Quake";
        private string PurifySpirit = "Purify Spirit";
        private string SwapSpeed = "Target Swap Speed";
        private string SpiritWalkersTidalTotem = "Spiritwalker's Tidal Totem";
        private string Innervate = "Innervate";


        //Talents
        bool TorrentTalent => API.PlayerIsTalentSelected(1, 1);
        bool UndulationTalent => API.PlayerIsTalentSelected(1, 2);
        bool UnsleashLifeTalent => API.PlayerIsTalentSelected(1, 3);
        bool EchooftheElementsTalent => API.PlayerIsTalentSelected(2, 1);
        bool DelugeTalent => API.PlayerIsTalentSelected(2, 2);
        bool SurgeofEarthTalent => API.PlayerIsTalentSelected(2, 3);
        bool SpiritWolfTalent => API.PlayerIsTalentSelected(3, 1);
        bool EarthGrabTotemTalent => API.PlayerIsTalentSelected(3, 2);
        bool StaticChargeTalent => API.PlayerIsTalentSelected(3, 3);
        bool AncestralVigorTalent => API.PlayerIsTalentSelected(4, 1);
        bool EarthenWallTotemTalent => API.PlayerIsTalentSelected(4, 2);
        bool AncestralProtectionTotemTalent => API.PlayerIsTalentSelected(4, 3);
        bool NaturesGuardianTalent => API.PlayerIsTalentSelected(5, 1);
        bool GracefulSpiritTalent => API.PlayerIsTalentSelected(5, 2);
        bool WindRushTotemTalent => API.PlayerIsTalentSelected(5, 3);
        bool FlashFloodTalent => API.PlayerIsTalentSelected(6, 1);
        bool DownpourTalent => API.PlayerIsTalentSelected(6, 2);
        bool CloudburstTotemTalent => API.PlayerIsTalentSelected(6, 3);
        bool HighTideTalent => API.PlayerIsTalentSelected(7, 1);
        bool WellspringTalent => API.PlayerIsTalentSelected(7, 2);
        bool AscendanceTalent => API.PlayerIsTalentSelected(7, 3);

        //Stopwatchs/Ints/Strings
        private static readonly Stopwatch HealingStreamWatch = new Stopwatch();
        private static readonly Stopwatch Vesperwatch = new Stopwatch();
        private static readonly Stopwatch TransfuionWatch = new Stopwatch();
        private static readonly Stopwatch CloudburstWatch = new Stopwatch();
        private static readonly Stopwatch FaeWatch = new Stopwatch();
        private static readonly Stopwatch SwapWatch = new Stopwatch();
        private static readonly Stopwatch DPSWatch = new Stopwatch();
        private static readonly Stopwatch DispelWatch = new Stopwatch();
        int[] numbList = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100 };
        int[] numbPartyList = new int[] { 0, 1, 2, 3, 4, 5, };
        int[] numbRaidList = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 33, 35, 36, 37, 38, 39, 40 };
        int[] SwapSpeedList = new int[] { 1000, 1250, 1500, 1750, 2000, 2250, 2500, 2750, 3000 };
        int PlayerHealth => API.TargetHealthPercent;
        private int Level => API.PlayerLevel;
        private int RoleSpec
        {
            get
            {
                if (UseEarth == "Tank")
                    return 999;
                else if (UseEarth == "DPS")
                    return 997;
                else if (UseEarth == "Healer")
                    return 998;
                return 999;
            }
        }
        private int LowestTankInRaid()
        {
            int lowest = 100;

            for (int i = 0; i < raidunits.Length; i++)
            {
                if (API.UnitHealthPercent(raidunits[i]) < lowest && API.UnitHealthPercent(raidunits[i]) > 0)
                    lowest = API.UnitHealthPercent(raidunits[i]);
            }
            return lowest;
        }
        private void LowestTargetRaid()
        {
            for (int j = 0; j < raidunits.Length; j++)
            {
                if (API.UnitHealthPercent(raidunits[j]) < API.UnitHealthPercent(raidunits[j]) && (PlayerHealth >= 15 || API.PlayerCanAttackTarget) && API.UnitHealthPercent(units[j]) > 0 && API.UnitRange(raidunits[j]) <= 40 && (!SwapWatch.IsRunning || SwapWatch.ElapsedMilliseconds >= 1250))
                {
                    API.CastSpell(raidunits[j]);
                    SwapWatch.Restart();
                }
            }
        }
        private string LowestParty(string[] units)
        {
            string lowest = units[0];
            int health = 100;
            foreach (string unit in units)
            {
                if (API.UnitHealthPercent(unit) < health && API.UnitRange(unit) <= 40 && API.UnitHealthPercent(unit) > 0 && API.UnitHealthPercent(unit) != 100)
                {
                    lowest = unit;
                    health = API.UnitHealthPercent(unit);
                }
            }
            return lowest;
        }
        private string LowestRaid(string[] raidunits)
        {
            string lowest = raidunits[0];
            int health = 100;
            foreach (string raidunit in raidunits)
            {
                if (API.UnitHealthPercent(raidunit) < health && API.UnitRange(raidunit) <= 40 && API.UnitHealthPercent(raidunit) > 0 && API.UnitHealthPercent(raidunit) != 100)
                {
                    lowest = raidunit;
                    health = API.UnitHealthPercent(raidunit);
                }
            }
            return lowest;
        }
        private string GetTankParty(string[] units)
        {
            string tank = units[0];
            foreach (string unit in units)
            {
                if (API.UnitRoleSpec(unit) == API.TankRole)
                {
                    tank = unit;
                }
            }
            return tank;
        }
        private string GetTankRaid(string[] raidunits)
        {
            string tank = raidunits[0];
            foreach (string raidunit in raidunits)
            {
                if (API.UnitRoleSpec(raidunit) == API.TankRole)
                {
                    tank = raidunit;
                }
            }
            return tank;
        }
        private string LowestRaidTank(string[] raidunits)
        {
            string lowest = raidunits[0];
            int health = 100;
            foreach (string raidunit in raidunits)
            {
                if (API.UnitHealthPercent(raidunit) < health && API.UnitRange(raidunit) <= 40 && API.UnitHealthPercent(raidunit) > 0 && API.UnitHealthPercent(raidunit) != 100 && API.UnitRoleSpec(raidunit) == API.TankRole)
                {
                    lowest = raidunit;
                    health = API.UnitHealthPercent(raidunit);
                }
            }
            return lowest;
        }

        string[] PlayerTargetArray = { "player", "party1", "party2", "party3", "party4" };
        string[] RaidTargetArray = { "raid1", "raid2", "raid3", "raid4", "raid5", "raid6", "raid7", "raid8", "raid9", "raid8", "raid9", "raid10", "raid11", "raid12", "raid13", "raid14", "raid16", "raid17", "raid18", "raid19", "raid20", "raid21", "raid22", "raid23", "raid24", "raid25", "raid26", "raid27", "raid28", "raid29", "raid30", "raid31", "raid32", "raid33", "raid34", "raid35", "raid36", "raid37", "raid38", "raid39", "raid40" };
        string[] DispellList = { "Chilled", "Frozen Binds", "Clinging Darkness", "Rasping Scream", "Slime Injection", "Gripping Infection", "Repulsive Visage", "Soul Split", "Anima Injection", "Bewildering Pollen", "Bramblethorn Entanglement", "Dying Breath", "Sinlight Visions", "Siphon Life", "Turn to Stone", "Stony Veins", "Curse of Stone", "Turned to Stone", "Curse of Obliteration", "Cosmic Artifice", "Wailing Grief", "Shadow Word:  Pain", "Soporific Shimmerdust", "Soporific Shimmerdust 2", "Anguished Cries", "Wrack Soul", "Sintouched Anima", "Curse of Suppression", "Explosive Anger", "Dark Lance", "Insidious Venom", "Charged Anima", "Lost Confidence", "Burden of Knowledge", "Internal Strife", "Forced Confession", "Insidious Venom 2", "Soul Corruption", "Spectral Reach", "Death Grasp", "Shadow Vulnerability", "Curse of Desolation", "Hex", "Burst" };
        public string[] LegendaryList = new string[] { "None", "Spiritwalker's Tidal Totem" };
        public string[] EarthTarget = new string[] { "Tank", "DPS", "Healer" };
        string[] FightSelection = { "Shade on Barghast", "Sun King" };
        private string FightNPC => FightSelection[CombatRoutine.GetPropertyInt("Fight Selection")];
        string[] units = { "player", "party1", "party2", "party3", "party4" };
        string[] raidunits = { "raid1", "raid2", "raid3", "raid4", "raid5", "raid6", "raid7", "raid8", "raid9", "raid8", "raid9", "raid10", "raid11", "raid12", "raid13", "raid14", "raid16", "raid17", "raid18", "raid19", "raid20", "raid21", "raid22", "raid23", "raid24", "raid25", "raid26", "raid27", "raid28", "raid29", "raid30", "raid31", "raid32", "raid33", "raid34", "raid35", "raid36", "raid37", "raid38", "raid39", "raid40" };
        private string UseEarth => EarthTarget[CombatRoutine.GetPropertyInt("Use Earth Shield")];
        private string UseLeg => LegendaryList[CombatRoutine.GetPropertyInt("Legendary")];
        //AoE Systems
        private int UnitBelowHealthPercentRaid(int HealthPercent) => raidunits.Count(p => API.UnitHealthPercent(p) <= HealthPercent && API.UnitHealthPercent(p) > 0);
        private int UnitBelowHealthPercentParty(int HealthPercent) => units.Count(p => API.UnitHealthPercent(p) <= HealthPercent && API.UnitHealthPercent(p) > 0);
        private int UnitBelowHealthPercent(int HealthPercent) => API.PlayerIsInRaid ? UnitBelowHealthPercentRaid(HealthPercent) : UnitBelowHealthPercentParty(HealthPercent);
        private int UnitBelowManaPercentRaid(int ManaPercent) => raidunits.Count(p => API.UnitManaPercent(p) <= ManaPercent && API.UnitManaPercent(p) > 0 && API.UnitRoleSpec(p) == API.HealerRole);
        private int UnitBelowManaPercentParty(int ManaPercent) => units.Count(p => API.UnitManaPercent(p) <= ManaPercent && API.UnitManaPercent(p) > 0);
        private int UnitBelowManaPercent(int ManaPercent) => API.PlayerIsInRaid ? UnitBelowManaPercentRaid(ManaPercent) : UnitBelowManaPercentParty(ManaPercent);
        private int UnitAboveHealthPercentRaid(int HealthPercent) => raidunits.Count(p => API.UnitHealthPercent(p) >= HealthPercent && API.UnitHealthPercent(p) > 0);
        private int UnitAboveHealthPercentParty(int HealthPercent) => units.Count(p => API.UnitHealthPercent(p) >= HealthPercent && API.UnitHealthPercent(p) > 0);
        private int UnitAboveHealthPercent(int HealthPercent) => API.PlayerIsInRaid ? UnitAboveHealthPercentRaid(HealthPercent) : UnitAboveHealthPercentParty(HealthPercent);
        private int RangePartyTracking(int Range) => units.Count(p => API.UnitRange(p) <= Range);
        private int RangeRaidTracking(int Range) => raidunits.Count(p => API.UnitRange(p) <= Range);
        private int RangeTracking(int Range) => API.PlayerIsInRaid ? RangeRaidTracking(Range) : RangePartyTracking(Range);
        private int BuffRaidTracking(string buff) => raidunits.Count(p => API.UnitHasBuff(buff, p) && API.UnitBuffPlayerSrc(buff, p));
        private int BuffPartyTracking(string buff) => units.Count(p => API.UnitHasBuff(buff, p) && API.UnitBuffPlayerSrc(buff, p));
        private int BuffTracking(string buff) => API.PlayerIsInRaid ? BuffRaidTracking(buff) : BuffPartyTracking(buff);
        private int RiptideRaidTracking(string buff) => raidunits.Count(p => API.UnitHasBuff(buff, p));
        private int RiptidePartyTracking(string buff) => units.Count(p => API.UnitHasBuff(buff, p));
        private int EarthShieldRaidTracking(string buff) => raidunits.Count(p => API.UnitHasBuff(buff, p) && API.UnitBuffPlayerSrc(buff, p));
        private int EarthShieldPartyTracking(string buff) => units.Count(p => API.UnitHasBuff(buff, p));
        //Bools and Checks
        private static bool TargetHasDispellAble(string debuff)
        {
            return API.TargetHasDebuff(debuff, false, true);
        }
        private static bool MouseouverHasDispellAble(string debuff)
        {
            return API.MouseoverHasDebuff(debuff, false, true);
        }
        private static bool UnitHasDispellAble(string debuff, string unit)
        {
            return API.UnitHasDebuff(debuff, unit, false, true);
        }
        private static bool UnitHasBuff(string buff, string unit)
        {
            return API.UnitHasBuff(buff, unit, true, true);
        }
        private static bool PlayerHasDebuff(string buff)
        {
            return API.PlayerHasDebuff(buff, false, false);
        }
        private static bool TargetHasDebuff(string buff)
        {
            return API.TargetHasDebuff(buff, false, true);
        }
        private static bool MouseoverHasDebuff(string buff)
        {
            return API.MouseoverHasDebuff(buff, false, false);
        }
        private static bool UnitHasDebuff(string buff, string unit)
        {
            return API.UnitHasDebuff(buff, unit, false, true);
        }
        bool ChannelingFae => API.CurrentCastSpellID("player") == 328923;
        bool ChannelingFlesh => API.CurrentCastSpellID("player") == 324631;
        private bool EarthShieldTracking => API.PlayerIsInRaid ? EarthShieldRaidTracking(EarthShield) < 1 : EarthShieldPartyTracking(EarthShield) < 1;
        private bool ManaAoE => API.PlayerIsInRaid ? UnitBelowManaPercentRaid(ManaPercent) >= 3 : API.PlayerMana <= ManaPercent;
        private bool AscendAoE => API.PlayerIsInRaid ? UnitBelowHealthPercentRaid(AscendanceLifePercent) >= AoERaidNumber : UnitBelowHealthPercentParty(AscendanceLifePercent) >= AoENumber;
        private bool TrinketAoE => UnitBelowHealthPercent(TrinketLifePercent) >= AoENumber;
        private bool VesperAoE => UnitBelowHealthPercent(VesperTotemLifePercent) >= AoENumber;
        private bool DownpourAoE => UnitBelowHealthPercent(DownpourLifePercent) >= AoENumber;
        private bool WellSpringAoE => UnitBelowHealthPercent(WellspringLifePercent) >= AoENumber;
        private bool ChainHealAoE => UnitBelowHealthPercent(ChainHealLifePercent) >= AoENumber;
        private bool ChainHarvestAoE => UnitBelowHealthPercent(ChainHarvestLifePercent) >= AoENumber;
        private bool SpiritLinkAoE => API.PlayerIsInRaid ? UnitBelowHealthPercentRaid(SpiritLinkTotemLifePercent) >= RaidCDNumber : UnitBelowHealthPercentParty(SpiritLinkTotemLifePercent) >= DungeonCDNumber;
        private bool EarthenWallAoE => UnitBelowHealthPercent(EarthenWallTotemLifePercent) >= AoENumber;
        private bool HealingRainAoE => UnitBelowHealthPercent(HealingRainLifePercent) >= AoENumber;
        private bool HealingStreamAoE => UnitBelowHealthPercent(HealingStreamTotemLifePercent) >= AoENumber;
        private bool HealingTideAoE => API.PlayerIsInRaid ? UnitBelowHealthPercentRaid(HealingTideTotemLifePercent) >= RaidCDNumber : UnitBelowHealthPercentParty(HealingTideTotemLifePercent) >= DungeonCDNumber;
        private bool SpirtWalkersTotemAoE => API.PlayerIsInRaid ? UnitBelowHealthPercentRaid(SpiritWalkersTidalTotemLifePercent) >= RaidCDNumber : UnitBelowHealthPercentParty(SpiritWalkersTidalTotemLifePercent) >= DungeonCDNumber;
        private bool FaeAoE => UnitBelowHealthPercent(FaeLifePercent) >= AoENumber;
        private bool RiptideTracking => API.PlayerIsInRaid ? RiptideRaidTracking(Riptide) >= 3 : RiptidePartyTracking(Riptide) >= 2;
        private bool KyrianCheck => API.CanCast(VesperTotem) && PlayerCovenantSettings == "Kyrian" && VesperAoE && NotChanneling && !API.PlayerCanAttackTarget && (!API.PlayerIsMoving || API.PlayerIsMoving);
        private bool NightFaeCheck =>API.CanCast(FaeTransfusion) && PlayerCovenantSettings == "Night Fae" && FaeAoE && NotChanneling && !API.PlayerCanAttackTarget && (!API.PlayerIsMoving || API.PlayerIsMoving && API.PlayerHasBuff(SpiritWalkersGrace));
        private bool NecrolordCheck => API.CanCast(PrimordialWave) && PlayerCovenantSettings == "Necrolord" && NotChanneling && !API.PlayerCanAttackTarget && (!API.PlayerIsMoving || API.PlayerIsMoving) && API.TargetHealthPercent <= PrimordialWaveLifePercent && RiptideTracking;
        private bool NecrolordMOCheck => API.CanCast(PrimordialWave) && PlayerCovenantSettings == "Necrolord" && NotChanneling && !API.PlayerCanAttackMouseover && (!API.PlayerIsMoving || API.PlayerIsMoving) && API.MouseoverHealthPercent <= PrimordialWaveLifePercent && RiptideTracking;
        private bool VenthyrCheck => API.CanCast(ChainHarvest) && ChainHarvestAoE && PlayerCovenantSettings == "Venthyr" && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" || UseCovenant == "on AOE" && IsAOE) && NotChanneling && (!API.PlayerCanAttackTarget || API.PlayerCanAttackTarget) && (!API.PlayerIsMoving || API.PlayerIsMoving && API.PlayerHasBuff(SpiritWalkersGrace));
        private bool VenthyrMOCheck => API.CanCast(ChainHarvest) && ChainHarvestAoE && PlayerCovenantSettings == "Venthyr" && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" || UseCovenant == "on AOE" && IsAOE) && NotChanneling && (!API.PlayerCanAttackMouseover || API.PlayerCanAttackMouseover) && (!API.PlayerIsMoving || API.PlayerIsMoving && API.PlayerHasBuff(SpiritWalkersGrace));
        private bool RiptideCheck => API.CanCast(Riptide) && !API.PlayerCanAttackTarget && (!API.PlayerIsMoving || API.PlayerIsMoving) && !API.PlayerHasBuff(Riptide) && API.TargetHealthPercent <= RiptideLifePercent;
        private bool RiptideMOCheck => API.CanCast(Riptide) && !API.PlayerCanAttackMouseover && (!API.PlayerIsMoving || API.PlayerIsMoving) && !API.MouseoverHasBuff(Riptide) && API.MouseoverHealthPercent <= RiptideLifePercent;
        private bool SpiritLinkCheck => API.CanCast(SpiritLinkTotem) && SpiritLinkAoE;
        public bool isMouseoverInCombat => CombatRoutine.GetPropertyBool("MouseoverInCombat");
        private bool IsAutoSwap => API.ToggleIsEnabled("Auto Target");
        private bool isExplosive => API.TargetMaxHealth <= 600 && API.TargetMaxHealth != 0;
        private bool IsOOC => API.ToggleIsEnabled("OOC");
        private bool IsDPS => API.ToggleIsEnabled("DPS Auto Target");
        private bool InRange => IsMouseover ? API.MouseoverRange <= 40 : API.TargetRange <= 40;
        private bool IsMelee => API.TargetRange < 6;
        private bool IsInKickRange => API.FocusCanInterrupted ? API.FocusRange < 31 : API.TargetRange < 31;
        private bool NotChanneling => !API.PlayerIsChanneling;
        private bool IsMouseover => API.ToggleIsEnabled("Mouseover");
        private bool IsDispell => API.ToggleIsEnabled("Dispel");
        private bool IsHealingRain => API.ToggleIsEnabled("Healing Rain");
        private bool IsNpC => API.ToggleIsEnabled("NPC");
        bool IsTrinkets1 => (UseTrinket1 == "With Cooldowns" && IsCooldowns  || UseTrinket1 == "On Cooldown" && API.TargetHealthPercent <= TrinketLifePercent || UseTrinket1 == "on AOE" && TrinketAoE);
        bool IsTrinkets2 => (UseTrinket2 == "With Cooldowns" && IsCooldowns  || UseTrinket2 == "On Cooldown" && API.TargetHealthPercent <= TrinketLifePercent || UseTrinket2 == "on AOE" && TrinketAoE);
        private bool AutoWolf => CombatRoutine.GetPropertyBool("AutoWolf");
        //Quaking
        private bool QuakingHelper => CombatRoutine.GetPropertyBool("QuakingHelper");
        private bool SpiritWalker => CombatRoutine.GetPropertyBool(SpiritWalkersGrace);

        private bool Quaking => (API.PlayerIsCasting(false) || API.PlayerIsChanneling) && API.PlayerDebuffRemainingTime(Quake) < 110 && PlayerHasDebuff(Quake);
        private bool SaveQuake => (PlayerHasDebuff(Quake) && API.PlayerDebuffRemainingTime(Quake) > 200 && QuakingHelper || !PlayerHasDebuff(Quake) || !QuakingHelper);
        private bool QuakingHS => (API.PlayerDebuffRemainingTime(Quake) > HealingSurgeCastTime && PlayerHasDebuff(Quake) || !PlayerHasDebuff(Quake));
        private bool QuakingHW => (API.PlayerDebuffRemainingTime(Quake) > HealingWaveCastTime && PlayerHasDebuff(Quake) || !PlayerHasDebuff(Quake));
        private bool QuakingCH => (API.PlayerDebuffRemainingTime(Quake) > ChainHealCastTime && PlayerHasDebuff(Quake) || !PlayerHasDebuff(Quake));
        private bool QuakingHR => (API.PlayerDebuffRemainingTime(Quake) > HealingRainCastTime && PlayerHasDebuff(Quake) || !PlayerHasDebuff(Quake));
        private bool QuakingDownpour => (API.PlayerDebuffRemainingTime(Quake) > DownpourCastTime && PlayerHasDebuff(Quake) || !PlayerHasDebuff(Quake));
        private bool QuakingWellspring => (API.PlayerDebuffRemainingTime(Quake) > WellspringCastTime && PlayerHasDebuff(Quake) || !PlayerHasDebuff(Quake));
        private bool QuakingChainHarvest => (API.PlayerDebuffRemainingTime(Quake) > ChainHarvestCastTime && PlayerHasDebuff(Quake) || !PlayerHasDebuff(Quake)); 
        private bool QuakingFae => (API.PlayerDebuffRemainingTime(Quake) > FaeCastTime && PlayerHasDebuff(Quake) || !PlayerHasDebuff(Quake));
        private bool QuakingLightning => (API.PlayerDebuffRemainingTime(Quake) > LightningCastTime && PlayerHasDebuff(Quake) || !PlayerHasDebuff(Quake));
        private bool QuakingChainLight => (API.PlayerDebuffRemainingTime(Quake) > ChainLightningCastTime && PlayerHasDebuff(Quake) || !PlayerHasDebuff(Quake));
        private bool QuakingLavaburst => (API.PlayerDebuffRemainingTime(Quake) > LavaburstCastTime && PlayerHasDebuff(Quake) || !PlayerHasDebuff(Quake));
        float HealingSurgeCastTime => 150f / (1f + API.PlayerGetHaste);
        float HealingWaveCastTime => 250f / (1f + API.PlayerGetHaste);
        float ChainHealCastTime => 250f / (1f + API.PlayerGetHaste);
        float HealingRainCastTime => 200f / (1f + API.PlayerGetHaste);

        float DownpourCastTime => 150f / (1f + API.PlayerGetHaste);
        float WellspringCastTime => 150f / (1f + API.PlayerGetHaste);
        float ChainHarvestCastTime => 250f / (1f + API.PlayerGetHaste);
        float FaeCastTime => 300f / (1f + API.PlayerGetHaste);
        float LavaburstCastTime => 200f / (1f + API.PlayerGetHaste);
        float LightningCastTime => 250f / (1f + API.PlayerGetHaste);
        float ChainLightningCastTime => 200f / (1f + API.PlayerGetHaste);
        //Settings and Percents
        private int TrinketLifePercent => numbList[CombatRoutine.GetPropertyInt(Trinket)];
        private int FaeLifePercent => numbList[CombatRoutine.GetPropertyInt(FaeTransfusion)];
        private int RiptideLifePercent => numbList[CombatRoutine.GetPropertyInt(Riptide)];
        private int WellspringLifePercent => numbList[CombatRoutine.GetPropertyInt(Wellspring)];
        private int DownpourLifePercent => numbList[CombatRoutine.GetPropertyInt(Downpour)];
        private int UnleashLifePercent => numbList[CombatRoutine.GetPropertyInt(UnleashLife)];
        private int HealingSurgeLifePercent => numbList[CombatRoutine.GetPropertyInt(HealingSurge)];
        private int HealingWaveLifePercent => numbList[CombatRoutine.GetPropertyInt(HealingWave)];
        private int PrimordialWaveLifePercent => numbList[CombatRoutine.GetPropertyInt(PrimordialWave)];
        private int SpiritLinkTotemLifePercent => numbList[CombatRoutine.GetPropertyInt(SpiritLinkTotem)];
        private int VesperTotemLifePercent => numbList[CombatRoutine.GetPropertyInt(VesperTotem)];
        private int ChainHealLifePercent => numbList[CombatRoutine.GetPropertyInt(ChainHeal)];
        private int ChainHarvestLifePercent => numbList[CombatRoutine.GetPropertyInt(ChainHarvest)];
        private int HealingRainLifePercent => numbList[CombatRoutine.GetPropertyInt(HealingRain)];
        private int HealingStreamTotemLifePercent => numbList[CombatRoutine.GetPropertyInt(HealingStreamTotem)];
        private int HealingTideTotemLifePercent => numbList[CombatRoutine.GetPropertyInt(HealingTideTotem)];
     //   private int AncestralProtectionTotemLifePercent => numbList[CombatRoutine.GetPropertyInt(AncestralProtectionTotem)];
        private int EarthenWallTotemLifePercent => numbList[CombatRoutine.GetPropertyInt(EarthenWallTotem)];
        private int AscendanceLifePercent => numbList[CombatRoutine.GetPropertyInt(Ascendance)];
        private int SurgeofEarthLifePercent => numbList[CombatRoutine.GetPropertyInt(SurgeofEarth)];
        private int ManaPercent => numbList[CombatRoutine.GetPropertyInt(ManaTideTotem)];
        private int SpiritWalkersTidalTotemLifePercent => numbList[CombatRoutine.GetPropertyInt(SpiritWalkersTidalTotem)];
        private int PhialofSerenityLifePercent => numbList[CombatRoutine.GetPropertyInt(PhialofSerenity)];
        private int SpiritualHealingPotionLifePercent => numbList[CombatRoutine.GetPropertyInt(SpiritualHealingPotion)];
        private string UseCovenant => CDUsageWithAOE[CombatRoutine.GetPropertyInt("Use Covenant")];
        private string UseAscend => CDUsage[CombatRoutine.GetPropertyInt(AscendanceUse)];
        private int AoENumber => numbPartyList[CombatRoutine.GetPropertyInt(AoE)];
        private int AoERaidNumber => numbRaidList[CombatRoutine.GetPropertyInt(AoERaid)];
        private int AoEDPSHLifePercent => numbList[CombatRoutine.GetPropertyInt(AoEDPSH)];
        private int AoEDPSNumber => numbPartyList[CombatRoutine.GetPropertyInt(AoEDPS)];
        private int TankHealth => numbList[CombatRoutine.GetPropertyInt("Tank Health")];
        private int UnitHealth => numbList[CombatRoutine.GetPropertyInt("Other Members Health")];
        private int PlayerHP => numbList[CombatRoutine.GetPropertyInt("Player Health")];

        private int AoEDPSRaidNumber => numbRaidList[CombatRoutine.GetPropertyInt(AoEDPSRaid)];
        private int AoEDPSHRaidLifePercent => numbList[CombatRoutine.GetPropertyInt(AoEDPSHRaid)];
        private int FleshcraftPercentProc => numbList[CombatRoutine.GetPropertyInt(Fleshcraft)];
        private string UseTrinket1 => CDUsageWithAOE[CombatRoutine.GetPropertyInt("Trinket1")];
        private string UseTrinket2 => CDUsageWithAOE[CombatRoutine.GetPropertyInt("Trinket2")];
        private int DungeonCDNumber => numbPartyList[CombatRoutine.GetPropertyInt(DungeonCD)];
        private int SwapSpeedSetting => SwapSpeedList[CombatRoutine.GetPropertyInt(SwapSpeed)];
        private int RaidCDNumber => numbRaidList[CombatRoutine.GetPropertyInt(RaidCD)];
        //private int AoERaidNumber => numbRaidList[CombatRoutine.GetPropertyInt(AoER)];



        //  public bool isInterrupt => CombatRoutine.GetPropertyBool("KICK") && API.TargetCanInterrupted && API.TargetIsCasting && (API.TargetIsChanneling ? API.TargetElapsedCastTime >= interruptDelay : API.TargetCurrentCastTimeRemaining <= interruptDelay);
        //  public int interruptDelay => random.Next((int)(CombatRoutine.GetPropertyInt("KICKTime") * 0.9), (int)(CombatRoutine.GetPropertyInt("KICKTime") * 1.1));
        public override void Initialize()
        {
            CombatRoutine.Name = "Resto Shaman by Ryu";
            API.WriteLog("Welcome to Resto Shaman v2.0 by Ryu");
            API.WriteLog("If you wish to use my AoE Logic with the Health Percent Setting for Ascendance, please use On Cooldown. If not, With Cooldowns will use the toggle.");
            API.WriteLog("Mouseover Support is added. Please create /cast [@mouseover] xx whereas xx is your spell and assign it the binds with MO on it in keybinds.");
            API.WriteLog("For all ground spells, either use @Cursor or when it is time to place it, the Bot will pause until you've placed it. If you'd perfer to use your own logic for them, please place them on ignore in the spellbook.");
            API.WriteLog("For the Quaking helper you just need to create an ingame macro with /stopcasting and bind it under the Macros Tab in Elk :-)");
            API.WriteLog("There are two different settings for AoE Numbers. AoE Cooldowns referes to Sprirt Link Totem and Healing Tide Totem. All others use the AoE Healing Number or AoE Raid Healing Number.");
            API.WriteLog("If you wish to use Auto Target, please set your WoW keybinds in the keybinds => Targeting for Self, Party, and Assist Target and then match them to the Macro's's in the spell book. Enable it the Toggles. You must at least have a target for it to swap, friendly or enemy. Please watch video in the Discord");
            API.WriteLog("The settings in the Targeting Section have been tested to work well. Change them at your risk and ONLY if you understand them.");
            API.WriteLog("Racial's are controled via the Cooldown Toggle and True/False Setting");
            API.WriteLog("IF YOU USE THE NPC TOGGLE, IT WILL CHANGE THE ROTATION THE NPC HEALING LOGIC (For Shade and Sun King) IT WILL IGNORE ALL OTHER THINGS EXPECT COOLDOWNS, PLEASE TURN IT OFF ONCE YOU HAVE FINISHED HEALING THE NPC - TARGETING ONLY");
            API.WriteLog("Special Thanks to Jom for testing");

            //Buff
            CombatRoutine.AddBuff(GhostWolf, 2645);
            CombatRoutine.AddBuff(Soulshape, 310143);
            CombatRoutine.AddBuff(LavaSurge, 77762);
            CombatRoutine.AddBuff(Ascendance, 114052);
            CombatRoutine.AddBuff(Riptide, 61295);
            CombatRoutine.AddBuff(EarthShield, 974);
            CombatRoutine.AddBuff(WaterShield, 52127);
            CombatRoutine.AddBuff(SpiritWalkersGrace, 79206);
            CombatRoutine.AddBuff(PrimordialWave, 327164);
            CombatRoutine.AddBuff(VesperTotem, 324386);
            CombatRoutine.AddBuff(UnleashLife, 73685);
            CombatRoutine.AddBuff(Quake, 240447);
            CombatRoutine.AddBuff(SpiritWalkersTidalTotem, 335891);
            CombatRoutine.AddBuff("Gluttonous Miasma", 329298);
            CombatRoutine.AddBuff(Innervate, 29166);


            //Debuff
            CombatRoutine.AddDebuff(FlameShock, 188389);
            CombatRoutine.AddDebuff(Quake, 240447);

            //Dispell Debuff
            CombatRoutine.AddDebuff("Chilled", 328664);
            CombatRoutine.AddDebuff("Frozen Binds", 320788);
            CombatRoutine.AddDebuff("Clinging Darkness", 323347);
            CombatRoutine.AddDebuff("Rasping Scream", 324293);
            CombatRoutine.AddDebuff("Slime Injection", 329110);
            CombatRoutine.AddDebuff("Gripping Infection", 328180);
            CombatRoutine.AddDebuff("Repulsive Visage", 328756);
            CombatRoutine.AddDebuff("Soul Split", 322557);
            CombatRoutine.AddDebuff("Anima Injection", 325224);
            CombatRoutine.AddDebuff("Bewildering Pollen", 321968);
            CombatRoutine.AddDebuff("Bramblethorn Entanglement", 324859);
            CombatRoutine.AddDebuff("Sinlight Visions", 339237);
            CombatRoutine.AddDebuff("Siphon Life", 325701);
            CombatRoutine.AddDebuff("Turn to Stone", 326607);
            CombatRoutine.AddDebuff("Stony Veins", 326632);
            CombatRoutine.AddDebuff("Dying Breath", 322968);
            CombatRoutine.AddDebuff("Curse of Stone", 319603);
            CombatRoutine.AddDebuff("Turned to Stone", 319611);
            CombatRoutine.AddDebuff("Curse of Obliteration", 325876);
            CombatRoutine.AddDebuff("Cosmic Artifice", 325725);
            CombatRoutine.AddDebuff("Wailing Grief", 340026);
            CombatRoutine.AddDebuff("Shadow Word:  Pain", 332707);
            CombatRoutine.AddDebuff("Soporific Shimmerdust", 334493);
            CombatRoutine.AddDebuff("Soporific Shimmerdust 2", 334496);
            CombatRoutine.AddDebuff("Hex", 332605);
            CombatRoutine.AddDebuff("Anguished Cries", 325885);
            CombatRoutine.AddDebuff("Wrack Soul", 321038);
            CombatRoutine.AddDebuff("Sintouched Anima", 328494);
            CombatRoutine.AddDebuff("Curse of Suppression", 326836);
            CombatRoutine.AddDebuff("Explosive Anger", 336277);
            CombatRoutine.AddDebuff("Insidious Venom", 323636);
            CombatRoutine.AddDebuff("Charged Anima", 338731);
            CombatRoutine.AddDebuff("Lost Confidence", 322818);
            CombatRoutine.AddDebuff("Burden of Knowledge", 317963);
            CombatRoutine.AddDebuff("Internal Strife", 327648);
            CombatRoutine.AddDebuff("Forced Confession", 328331);
            CombatRoutine.AddDebuff("Insidious Venom 2", 317661);
            CombatRoutine.AddDebuff("Dark Lance", 327481);
            CombatRoutine.AddDebuff("Soul Corruption", 333708);
            CombatRoutine.AddDebuff("Spectral Reach", 319669);
            CombatRoutine.AddDebuff("Death Grasp", 323831);
            CombatRoutine.AddDebuff("Shadow Vulnerability", 330725);
            CombatRoutine.AddDebuff("Curse of Desolation", 333299);
            CombatRoutine.AddDebuff("Gluttonous Miasma", 329298);
            CombatRoutine.AddDebuff("Burst", 240443);


            //Spell
            CombatRoutine.AddSpell(Fleshcraft, 324631);
            CombatRoutine.AddSpell(GhostWolf, 2645);
            CombatRoutine.AddSpell(Riptide, 61295);
            CombatRoutine.AddSpell(HealingSurge, 8004);
            CombatRoutine.AddSpell(HealingWave, 77472);
            CombatRoutine.AddSpell(ChainHeal, 1064);
            CombatRoutine.AddSpell(HealingRain, 73920);
            CombatRoutine.AddSpell(HealingStreamTotem, 5394);
            CombatRoutine.AddSpell(WaterShield, 52127);
            CombatRoutine.AddSpell(EarthShield, 974);
            CombatRoutine.AddSpell(FlameShock, 188389);
            CombatRoutine.AddSpell(FrostShock, 196840);
            CombatRoutine.AddSpell(LavaBurst, 51505);
            CombatRoutine.AddSpell(LightningBolt, 188196);
            CombatRoutine.AddSpell(ChainLightning, 188443);
            CombatRoutine.AddSpell(LightningShield, 192106);
            CombatRoutine.AddSpell(HealingTideTotem, 108280);
            CombatRoutine.AddSpell(SpiritLinkTotem, 98008);
            CombatRoutine.AddSpell(SpiritWalkersGrace, 79206);
            CombatRoutine.AddSpell(ManaTideTotem, 16191);
            CombatRoutine.AddSpell(AstralShift, 108271);
            CombatRoutine.AddSpell(WindShear, 57994);
            CombatRoutine.AddSpell(PrimordialWave, 326059, "D1");
            CombatRoutine.AddSpell(VesperTotem, 324386, "D1");
            CombatRoutine.AddSpell(FaeTransfusion, 328923, "D1");
            CombatRoutine.AddSpell(ChainHarvest, 320674, "D1");
            CombatRoutine.AddSpell(Ascendance, 114052, "D1");
            CombatRoutine.AddSpell(UnleashLife, 73685);
            CombatRoutine.AddSpell(SurgeofEarth, 320746);
            CombatRoutine.AddSpell(EarthenWallTotem, 198838);
            CombatRoutine.AddSpell(AncestralProtectionTotem, 207399);
            CombatRoutine.AddSpell(WindRushTotem, 192077);
            CombatRoutine.AddSpell(Downpour, 207778);
            CombatRoutine.AddSpell(CloudburstTotem, 157153);
            CombatRoutine.AddSpell(Wellspring, 197995);
            CombatRoutine.AddSpell(PurifySpirit, 77130);

           //Toggle
           CombatRoutine.AddToggle("Auto Target");
            CombatRoutine.AddToggle("DPS Auto Target");
            CombatRoutine.AddToggle("Mouseover");
            CombatRoutine.AddToggle("OOC");
            CombatRoutine.AddToggle("Dispel");
            CombatRoutine.AddToggle("Healing Rain");
            CombatRoutine.AddToggle("NPC");

            //Item
            CombatRoutine.AddItem(PhialofSerenity, 177278);
            CombatRoutine.AddItem(SpiritualHealingPotion, 171267);

            //Macro
            CombatRoutine.AddMacro(Trinket1);
            CombatRoutine.AddMacro(Trinket2);
            CombatRoutine.AddMacro("Stopcast", "F10");
            CombatRoutine.AddMacro("Assist");
            CombatRoutine.AddMacro(Player);
            CombatRoutine.AddMacro(Party1);
            CombatRoutine.AddMacro(Party2);
            CombatRoutine.AddMacro(Party3);
            CombatRoutine.AddMacro(Party4);
            CombatRoutine.AddMacro(WindShear + "Focus");
            CombatRoutine.AddMacro(FlameShock + MO);
            CombatRoutine.AddMacro(LightningBolt + MO);
            CombatRoutine.AddMacro(ChainLightning + MO);
            CombatRoutine.AddMacro(ChainHeal + MO);
            CombatRoutine.AddMacro(ChainHarvest + MO);
            CombatRoutine.AddMacro(HealingSurge + MO);
            CombatRoutine.AddMacro(HealingWave + MO);
            CombatRoutine.AddMacro(Riptide + MO);
            CombatRoutine.AddMacro(EarthShield + MO);
            CombatRoutine.AddMacro(PrimordialWave + MO);
            CombatRoutine.AddMacro(UnleashLife + MO);
            CombatRoutine.AddMacro(Downpour + MO);
            CombatRoutine.AddMacro(LavaBurst + MO);
            CombatRoutine.AddMacro(SurgeofEarth + MO);
            CombatRoutine.AddMacro(PurifySpirit + MO);
            CombatRoutine.AddMacro("raid1");
            CombatRoutine.AddMacro("raid2");
            CombatRoutine.AddMacro("raid3");
            CombatRoutine.AddMacro("raid4");
            CombatRoutine.AddMacro("raid5");
            CombatRoutine.AddMacro("raid6");
            CombatRoutine.AddMacro("raid7");
            CombatRoutine.AddMacro("raid8");
            CombatRoutine.AddMacro("raid9");
            CombatRoutine.AddMacro("raid10");
            CombatRoutine.AddMacro("raid11");
            CombatRoutine.AddMacro("raid12");
            CombatRoutine.AddMacro("raid13");
            CombatRoutine.AddMacro("raid14");
            CombatRoutine.AddMacro("raid15");
            CombatRoutine.AddMacro("raid16");
            CombatRoutine.AddMacro("raid17");
            CombatRoutine.AddMacro("raid18");
            CombatRoutine.AddMacro("raid19");
            CombatRoutine.AddMacro("raid20");
            CombatRoutine.AddMacro("raid21");
            CombatRoutine.AddMacro("raid22");
            CombatRoutine.AddMacro("raid23");
            CombatRoutine.AddMacro("raid24");
            CombatRoutine.AddMacro("raid25");
            CombatRoutine.AddMacro("raid26");
            CombatRoutine.AddMacro("raid27");
            CombatRoutine.AddMacro("raid28");
            CombatRoutine.AddMacro("raid29");
            CombatRoutine.AddMacro("raid30");
            CombatRoutine.AddMacro("raid31");
            CombatRoutine.AddMacro("raid32");
            CombatRoutine.AddMacro("raid33");
            CombatRoutine.AddMacro("raid34");
            CombatRoutine.AddMacro("raid35");
            CombatRoutine.AddMacro("raid36");
            CombatRoutine.AddMacro("raid37");
            CombatRoutine.AddMacro("raid38");
            CombatRoutine.AddMacro("raid39");
            CombatRoutine.AddMacro("raid40");

            //Prop
            CombatRoutine.AddProp("AutoWolf", "AutoWolf", true, "Will auto switch forms out of Fight", "Generic");
            CombatRoutine.AddProp(AstralShift, AstralShift + " Life Percent", numbList, "Life percent at which" + AstralShift + "is used, set to 0 to disable", "Defense", 40);
            CombatRoutine.AddProp(Fleshcraft, "Fleshcraft", numbList, "Life percent at which " + Fleshcraft + " is used, set to 0 to disable set 100 to use it everytime", "Defense", 100);
            CombatRoutine.AddProp(PhialofSerenity, PhialofSerenity + " Life Percent", numbList, " Life percent at which" + PhialofSerenity + " is used, set to 0 to disable", "Defense", 40);
            CombatRoutine.AddProp(SpiritualHealingPotion, SpiritualHealingPotion + " Life Percent", numbList, " Life percent at which" + SpiritualHealingPotion + " is used, set to 0 to disable", "Defense", 40);

            CombatRoutine.AddProp("Use Covenant", "Use " + "Covenant Ability", CDUsageWithAOE, "Use " + "Covenant" + "On Cooldown, with Cooldowns, On AOE, Not Used", "Cooldowns", 1);
            CombatRoutine.AddProp(EarthElemental, "Use " + EarthElemental, CDUsage, "Use " + EarthElemental + "On Cooldown, with Cooldowns, Not Used", "Cooldowns", 0);
            CombatRoutine.AddProp(AscendanceUse, "Use " + Ascendance, CDUsage, "Use " + Ascendance + "On Cooldown, with Cooldowns, Not Used", "Cooldowns", 0);
            CombatRoutine.AddProp(Ascendance, Ascendance + " Life Percent", numbList, "Life percent at which " + Ascendance + " is used when AoE Number of members are at life percent, set to 0 to disable", "Cooldowns", 65);

            AddProp("MouseoverInCombat", "Only Mouseover in combat", false, "Only Attack mouseover in combat to avoid stupid pulls", "Generic");
            CombatRoutine.AddProp("QuakingHelper", "Quaking Helper", false, "Will cancel casts on Quaking", "Generic");

            CombatRoutine.AddProp(SpiritWalkersGrace, SpiritWalkersGrace, true, "Will use Spirit Walker's Grace while moving", "Movement");

            CombatRoutine.AddProp("Tank Health", "Tank Health", numbList, "Life percent at which " + "Tank Health" + "needs to be at to targeted during DPS Targeting", "Targeting", 75);
            CombatRoutine.AddProp("Other Members Health", "Other Members Health", numbList, "Life percent at which " + "Other Members Health" + "needs to be at to targeted during DPS Targeting", "Targeting", 35);
            CombatRoutine.AddProp("Player Health", "Player Health", numbList, "Life percent at which " + "Player Health" + "needs to be at to targeted above all else", "Targeting", 35);
            CombatRoutine.AddProp(AoEDPS, "Number of units needed to be above DPS Health Percent to DPS in party ", numbPartyList, " Units above for DPS ", "Targeting", 2);
            CombatRoutine.AddProp(AoEDPSRaid, "Number of units needed to be above DPS Health Percent to DPS in Raid ", numbRaidList, " Units above for DPS ", "Targeting", 7);
            CombatRoutine.AddProp(AoEDPSH, "Life Percent for units to be above for DPS and below to return back to Healing", numbList, "Health percent at which DPS in party" + "is used,", "Targeting", 75);
            CombatRoutine.AddProp(AoEDPSHRaid, "Life Percent for units to be above for DPS and below to return back to Healing in raid", numbList, "Health percent at which DPS" + "is used,", "Targeting", 75);

            CombatRoutine.AddProp(ChainHeal, ChainHeal + " Life Percent", numbList, "Life percent at which " + ChainHeal + " is used when AoE Number of members are at life percent, set to 0 to disable", "Healing", 65);
            CombatRoutine.AddProp("Use Earth Shield", "Select your Earth Shield Target Role", EarthTarget, "Select Your Earth Shield Target Role", "Earth Shield", 1);
            CombatRoutine.AddProp(Riptide, Riptide + " Life Percent", numbList, "Life percent at which " + Riptide + " is used, set to 0 to disable", "Healing", 90);
            CombatRoutine.AddProp(UnleashLife, UnleashLife + " Life Percent", numbList, "Life percent at which " + UnleashLife + " is used if talented, set to 0 to disable", "Healing", 80);
            CombatRoutine.AddProp(HealingSurge, HealingSurge + " Life Percent", numbList, "Life percent at which " + HealingSurge + " is used, set to 0 to disable", "Healing", 70);
            CombatRoutine.AddProp(HealingWave, HealingWave + " Life Percent", numbList, "Life percent at which " + HealingWave + " is used, set to 0 to disable", "Healing", 91);
            CombatRoutine.AddProp(PrimordialWave, PrimordialWave + " Life Percent", numbList, "Life percent at which " + PrimordialWave + " is used, set to 0 to disable", "Healing", 78);
            CombatRoutine.AddProp(ManaTideTotem, ManaTideTotem + " Mana Percent", numbList, "Mana percent at which " + ManaTideTotem + " is used, set to 0 to disable", "Healing", 65);
            CombatRoutine.AddProp(SpiritLinkTotem, SpiritLinkTotem + " Life Percent", numbList, "Life percent at which " + SpiritLinkTotem + " is used when AoE Number of members are at, set to 0 to disable", "Healing", 15);
            CombatRoutine.AddProp(HealingTideTotem, HealingTideTotem + " Life Percent", numbList, "Life percent at which " + HealingTideTotem + " is used when AoE Number of members are at, set to 0 to disable", "Healing", 45);
            CombatRoutine.AddProp(VesperTotem, VesperTotem + " Life Percent", numbList, "Life percent at which " + VesperTotem + " is used when AoE Number of members are at and Cov is Kyrian, set to 0 to disable", "Healing", 55);
            CombatRoutine.AddProp(FaeTransfusion, FaeTransfusion + " Life Percent", numbList, "Life percent at which " + FaeTransfusion + " is used when AoE Number of members are at and Cov is Kyrian, set to 0 to disable", "Healing", 55);
            CombatRoutine.AddProp(SpiritWalkersTidalTotem, SpiritWalkersTidalTotem + " Life Percent", numbList, "Life percent at which " + SpiritWalkersTidalTotem + " is used when AoE Number of members are at life percent if you have that legendary, set to 0 to disable", "Healing", 65);
            CombatRoutine.AddProp(ChainHarvest, ChainHarvest + " Life Percent", numbList, "Life percent at which " + ChainHarvest + " is used when AoE Number of members are at life percent, set to 0 to disable", "Healing", 65);
            CombatRoutine.AddProp(HealingRain, HealingRain + " Life Percent", numbList, "Life percent at which " + HealingRain + " is used when AoE Number of members are at life percent, set to 0 to disable", "Healing", 95);
            CombatRoutine.AddProp(HealingStreamTotem, HealingStreamTotem + " Life Percent", numbList, "Life percent at which " + HealingStreamTotem + " Or Cloudburst Totem is used when AoE Number of members are at life percent, set to 0 to disable", "Healing", 92);
         //   CombatRoutine.AddProp(AncestralProtectionTotem, AncestralProtectionTotem + " Life Percent", numbList, "Life percent at which " + AncestralProtectionTotem + " is used when AoE Number of members are at life percent if talented, set to 0 to disable", "Healing", 65);
            CombatRoutine.AddProp(EarthenWallTotem, EarthenWallTotem + " Life Percent", numbList, "Life percent at which " + EarthenWallTotem + " is used when AoE Number of members are at life percent if talented, set to 0 to disable", "Healing", 95);
            CombatRoutine.AddProp(Downpour, Downpour + " Life Percent", numbList, "Life percent at which " + Downpour + " is used when AoE Number of members are at life percent if talented, set to 0 to disable", "Healing", 65);
            CombatRoutine.AddProp(Wellspring, Wellspring + " Life Percent", numbList, "Life percent at which " + Wellspring + " is used when AoE Number of members are at life percent if talented, set to 0 to disable", "Healing", 70);
            CombatRoutine.AddProp(SurgeofEarth, SurgeofEarth + " Life Percent", numbList, "Life percent at which " + SurgeofEarth + " is used when AoE Number of members are at life percent if talented, set to 0 to disable", "Healing", 65);
            CombatRoutine.AddProp(AoE, "Number of units for AoE Healing ", numbPartyList, " Units for AoE Healing", "Healing", 3);
            CombatRoutine.AddProp(AoERaid, "Number of units for AoE Healing in raid ", numbRaidList, " Units for AoE Healing in raid", "Healing", 7);
     
            CombatRoutine.AddProp(DungeonCD, "Number of units for Cooldowns Healing in 5-man ", numbPartyList, " Units for Cooldowns Healing", "Healing", 2);
            CombatRoutine.AddProp(RaidCD, "Number of units for Cooldowns Healing in raid ", numbRaidList, " Units for Cooldowns Healing in raid", "Healing", 6);
            CombatRoutine.AddProp("Legendary", "Select your Legendary", LegendaryList, "Select Your Legendary", "Legendary");

            CombatRoutine.AddProp("Fight Selection", "Select your NPC Fight", FightSelection, "Select Your NPC Fight", "NPC Rotation");

            CombatRoutine.AddProp(Trinket, Trinket + " Life Percent", numbList, "Life percent at which " + "Trinkets" + " should be used, set to 0 to disable", "Healing", 55);
            CombatRoutine.AddProp("Trinket1", "Trinket1 usage", CDUsageWithAOE, "When should trinket1 be used", "Trinket", 0);
            CombatRoutine.AddProp("Trinket2", "Trinket2 usage", CDUsageWithAOE, "When should trinket1 be used", "Trinket", 0);

        }

        public override void Pulse()
        {
            if (API.PlayerCurrentCastTimeRemaining > 40 && QuakingHelper && Quaking)
            {
                API.CastSpell("Stopcast");
                API.WriteLog("Debuff Time Remaining for Quake : " + API.PlayerDebuffRemainingTime(Quake));
                return;
            }
            if (API.PlayerLastSpell == HealingStreamTotem || API.LastSpellCastInGame == HealingStreamTotem)
            {
                HealingStreamWatch.Restart();
            }
            if (HealingStreamWatch.ElapsedMilliseconds >= 16000)
            {
                HealingStreamWatch.Stop();
            }
            for (int i = 0; i < units.Length; i++)
            {
                if (IsDispell && API.PlayerIsInGroup && !API.PlayerIsInRaid && UnitHasDispellAble("Frozen Binds", units[i]))
                {
                    DispelWatch.Restart();
                }
            }
            if (!API.PlayerIsMounted && !API.PlayerSpellonCursor && (IsOOC || API.PlayerIsInCombat) && !ChannelingFae && NotChanneling && !ChannelingFlesh && (!TargetHasDebuff("Gluttonous Miasma") || !MouseoverHasDebuff("Gluttonous Miasma") && IsMouseover))
            {
                if (isExplosive)
                {
                    if (API.CanCast(FrostShock) && InRange)
                    {
                        API.CastSpell(FrostShock);
                        API.WriteLog("Explosive killer");
                        return;
                    }
                }
                if (SpiritWalker && API.CanCast(SpiritWalkersGrace) && API.PlayerIsMoving)
                {
                    API.CastSpell(SpiritWalkersGrace);
                    return;
                }
                if (isInterrupt && API.CanCast(WindShear) && Level >= 12 && IsInKickRange)
                {
                    API.CastSpell(WindShear);
                    return;
                }
                if (API.CanCast(WindShear) && Level >= 12 && IsInKickRange && CombatRoutine.GetPropertyBool("KICK") && API.FocusIsCasting() && (API.FocusIsChanneling ? API.FocusElapsedCastTimePercent >= interruptDelay : API.FocusCurrentCastTimeRemaining <= interruptDelay))
                {
                    API.CastSpell(WindShear + "Focus");
                    return;
                }
                if (API.CanCast(Ascendance) && AscendanceTalent && (UseAscend == "With Cooldowns" && IsCooldowns || UseAscend == "On Cooldown" && AscendAoE))
                {
                    API.CastSpell(Ascendance);
                    return;
                }
                if (IsNpC && FightNPC == "Sun King" && InRange)
                {
                    if (!API.TargetHasBuff(EarthShield, true, true) && API.CanCast(EarthShield) && API.TargetHealthPercent > 0)
                    {
                        API.CastSpell(EarthShield);
                        return;
                    }
                    if (PlayerCovenantSettings == "Necrolord" && API.CanCast(PrimordialWave)  && API.TargetHealthPercent > 0)
                    {
                        API.CastSpell(PrimordialWave);
                        return;
                    }
                    if (API.CanCast(Riptide) && !API.TargetHasBuff(Riptide, true, true) && API.TargetHealthPercent > 0)
                    {
                        API.CastSpell(Riptide);
                        return;
                    }
                    if (API.CanCast(HealingSurge) && API.TargetHealthPercent > 0 && API.PlayerHasBuff(Innervate))
                    {
                        API.CastSpell(HealingSurge);
                        return;
                    }
                    if (API.CanCast(HealingWave) && API.TargetHealthPercent > 0)
                    {
                        API.CastSpell(HealingWave);
                        return;
                    }
                    
                }
                if (IsNpC && FightNPC == "Shade on Barghast" && InRange)
                {
                    if (API.CanCast(Riptide) && !API.TargetHasBuff(Riptide, true, true) && InRange && API.TargetHealthPercent > 0)
                    {
                        API.CastSpell(Riptide);
                        return;
                    }
                    if (API.CanCast(HealingSurge) && API.TargetHealthPercent > 0 && API.PlayerHasBuff(Innervate))
                    {
                        API.CastSpell(HealingSurge);
                        return;
                    }
                    if (API.CanCast(HealingWave) && API.TargetHealthPercent > 0)
                    {
                        API.CastSpell(HealingWave);
                        return;
                    }
                }
                    #region Dispell
                    if (IsDispell)
                {
                    if (API.CanCast(PurifySpirit) && !ChannelingFae && NotChanneling)
                    {
                        for (int i = 0; i < DispellList.Length; i++)
                        {
                            if (TargetHasDispellAble(DispellList[i]) && (!TargetHasDispellAble("Frozen Binds") || TargetHasDispellAble("Frozen Binds") && DispelWatch.ElapsedMilliseconds >= 2000))
                            {
                                API.CastSpell(PurifySpirit);
                                return;
                            }
                        }
                    }
                    if (API.CanCast(PurifySpirit) && IsMouseover && !ChannelingFae && NotChanneling)
                    {
                        for (int i = 0; i < DispellList.Length; i++)
                        {
                            if (MouseouverHasDispellAble(DispellList[i]) && (!MouseouverHasDispellAble("Frozen Binds") || MouseouverHasDispellAble("Frozen Binds") && DispelWatch.ElapsedMilliseconds >= 2000))
                            {
                                API.CastSpell(PurifySpirit + "MO");
                                return;
                            }
                        }
                    }
                }
                #endregion
                if (API.CanCast(ManaTideTotem) && ManaAoE && InRange && API.PlayerIsInCombat || API.CanCast(ManaTideTotem) && SpirtWalkersTotemAoE && UseLeg == SpiritWalkersTidalTotem)
                {
                    API.CastSpell(ManaTideTotem);
                    return;
                }
                if (API.CanCast(EarthShield) && API.TargetRoleSpec == RoleSpec && !API.TargetHasBuff(EarthShield) && InRange && !API.PlayerCanAttackTarget && API.TargetHealthPercent <= 100 && EarthShieldTracking && API.TargetHealthPercent > 0)
                {
                    API.CastSpell(EarthShield);
                    return;
                }
                if (API.CanCast(EarthShield) && !API.MacroIsIgnored(EarthShield + MO) && IsMouseover && API.MouseoverRoleSpec == RoleSpec && !API.MouseoverHasBuff(EarthShield) && InRange && !API.PlayerCanAttackMouseover && API.MouseoverHealthPercent <= 100 && EarthShieldTracking && API.MouseoverHealthPercent > 0)
                {
                    API.CastSpell(EarthShield + "MO");
                    return;
                }
                if (RiptideCheck && InRange && API.TargetHealthPercent > 0)
                {
                    API.CastSpell(Riptide);
                    return;
                }
                if (RiptideMOCheck && !API.MacroIsIgnored(Riptide + MO) && IsMouseover && InRange && API.MouseoverHealthPercent > 0)
                {
                    API.CastSpell(Riptide + "MO");
                    return;
                }
                if (NecrolordCheck && InRange && API.TargetHealthPercent > 0)
                {
                    API.CastSpell(PrimordialWave);
                    return;
                }
                if (NecrolordMOCheck && !API.MacroIsIgnored(PrimordialWave + MO) && IsMouseover && InRange && API.MouseoverHealthPercent > 0)
                {
                    API.CastSpell(PrimordialWave + "MO");
                    return;
                }
                if (API.PlayerHasBuff(PrimordialWave) && RiptideTracking && API.CanCast(HealingWave) && InRange && !API.PlayerCanAttackTarget && (!QuakingHelper || QuakingHW && QuakingHelper) && API.TargetHealthPercent > 0 && (!API.PlayerIsMoving || API.PlayerIsMoving && API.PlayerHasBuff(SpiritWalkersGrace)))
                {
                    API.CastSpell(HealingWave);
                    return;
                }
                if (API.PlayerHasBuff(PrimordialWave) && RiptideTracking && API.CanCast(HealingWave) && !API.MacroIsIgnored(HealingWave + MO) && IsMouseover && InRange && !API.PlayerCanAttackMouseover && API.MouseoverHealthPercent > 0 && (!QuakingHelper || QuakingHW && QuakingHelper) && (!API.PlayerIsMoving || API.PlayerIsMoving && API.PlayerHasBuff(SpiritWalkersGrace)))
                {
                    API.CastSpell(HealingWave + MO);
                    return;
                }
                if (API.CanCast(SurgeofEarth) && SurgeofEarthTalent && PlayerHealth <= SurgeofEarthLifePercent && API.TargetHasBuff(EarthShield) && API.TargetBuffStacks(EarthShield) >= 3 && API.TargetHealthPercent > 0)
                {
                    API.CastSpell(SurgeofEarth);
                    return;
                }
                if (API.CanCast(SurgeofEarth) && !API.MacroIsIgnored(SurgeofEarth + MO) && SurgeofEarthTalent && API.MouseoverHealthPercent <= SurgeofEarthLifePercent && API.MouseoverHasBuff(EarthShield) && API.MouseoverBuffStacks(EarthShield) >= 3 && API.MouseoverHealthPercent > 0)
                {
                    API.CastSpell(SurgeofEarth + "MO");
                    return;
                }
                if (API.CanCast(WaterShield) && !API.PlayerHasBuff(WaterShield) && API.PlayerHealthPercent > 0)
                {
                    API.CastSpell(WaterShield);
                    return;
                }
                if (API.CanCast(HealingRain) && IsHealingRain && HealingRainAoE && InRange && API.TargetHealthPercent > 0 && (!QuakingHelper || QuakingHR && QuakingHelper) && (!API.PlayerIsMoving || API.PlayerIsMoving && API.PlayerHasBuff(SpiritWalkersGrace)))
                {
                    API.CastSpell(HealingRain);
                    return;
                }
                if (SpiritLinkCheck && InRange && API.TargetHealthPercent > 0)
                {
                    API.CastSpell(SpiritLinkTotem);
                    return;
                }
                if (API.CanCast(HealingStreamTotem) && HealingStreamAoE && InRange && API.TargetHealthPercent > 0 && !CloudburstTotemTalent && (!HealingStreamWatch.IsRunning || HealingStreamWatch.ElapsedMilliseconds >= 15000))
                {
                    API.CastSpell(HealingStreamTotem);
                    return;
                }
                if (API.CanCast(EarthenWallTotem) && EarthenWallTotemTalent && InRange && EarthenWallAoE && API.TargetHealthPercent > 0)
                {
                    API.CastSpell(EarthenWallTotem);
                    return;
                }
                if (API.CanCast(CloudburstTotem) && InRange && CloudburstTotemTalent && HealingStreamAoE && (!CloudburstWatch.IsRunning || CloudburstWatch.ElapsedMilliseconds >= 10000) && API.TargetHealthPercent > 0)
                {
                    API.CastSpell(CloudburstTotem);
                    CloudburstWatch.Reset();
                    CloudburstWatch.Start();
                    return;
                }
                if (API.CanCast(HealingTideTotem) && HealingTideAoE && InRange && API.TargetHealthPercent > 0)
                {
                    API.CastSpell(HealingTideTotem);
                    return;
                }
                if (KyrianCheck && InRange && API.TargetHealthPercent > 0)
                {
                    API.CastSpell(VesperTotem);
                    return;
                }
                if (NightFaeCheck && InRange && API.TargetHealthPercent > 0 && (!QuakingHelper || QuakingFae && QuakingHelper) && (!API.PlayerIsMoving || API.PlayerIsMoving && API.PlayerHasBuff(SpiritWalkersGrace)))
                {
                    API.CastSpell(FaeTransfusion);
                    return;
                }
                if (API.CanCast(UnleashLife) && API.TargetHealthPercent > 0 && UnsleashLifeTalent && API.TargetHealthPercent <= UnleashLifePercent && InRange && !API.PlayerCanAttackTarget)
                {
                    API.CastSpell(UnleashLife);
                    return;
                }
                if (API.CanCast(UnleashLife) && !API.MacroIsIgnored(UnleashLife + MO) && IsMouseover && API.MouseoverHealthPercent > 0 && UnsleashLifeTalent && API.MouseoverHealthPercent <= UnleashLifePercent && InRange && !API.PlayerCanAttackMouseover)
                {
                    API.CastSpell(UnleashLife + MO);
                    return;
                }
                if (API.CanCast(Downpour) && API.TargetHealthPercent > 0 && DownpourAoE && DownpourTalent && InRange && !API.PlayerCanAttackTarget && (!QuakingHelper || QuakingDownpour && QuakingHelper) && (!API.PlayerIsMoving || API.PlayerIsMoving && API.PlayerHasBuff(SpiritWalkersGrace)))
                {
                    API.CastSpell(Downpour);
                    return;
                }
                if (API.CanCast(Downpour) && !API.MacroIsIgnored(Downpour + MO) && IsMouseover && API.MouseoverHealthPercent > 0 && DownpourAoE && DownpourTalent && InRange && !API.PlayerCanAttackMouseover && (!QuakingHelper || QuakingDownpour && QuakingHelper) && (!API.PlayerIsMoving || API.PlayerIsMoving && API.PlayerHasBuff(SpiritWalkersGrace)))
                {
                    API.CastSpell(Downpour + MO);
                    return;
                }
                if (VenthyrCheck && InRange && API.TargetHealthPercent > 0 && (!QuakingHelper || QuakingChainHarvest && QuakingHelper) && (!API.PlayerIsMoving || API.PlayerIsMoving && API.PlayerHasBuff(SpiritWalkersGrace)))
                {
                    API.CastSpell(ChainHarvest);
                    return;
                }
                if (VenthyrMOCheck && !API.MacroIsIgnored(ChainHarvest + MO) && IsMouseover && InRange && API.MouseoverHealthPercent > 0 && (!QuakingHelper || QuakingChainHarvest && QuakingHelper) && (!API.PlayerIsMoving || API.PlayerIsMoving && API.PlayerHasBuff(SpiritWalkersGrace)))
                {
                    API.CastSpell(ChainHarvest + MO);
                    return;
                }
                if (API.CanCast(ChainHeal) && API.TargetHealthPercent > 0 && (ChainHealAoE || API.PlayerHasBuff(UnleashLife) && UnitBelowHealthPercent(85) >= 2 && API.TargetHasBuff(Riptide)) && (!QuakingHelper || QuakingCH && QuakingHelper) && (!API.PlayerIsMoving || API.PlayerIsMoving && API.PlayerHasBuff(SpiritWalkersGrace)))
                {
                    API.CastSpell(ChainHeal);
                    return;
                }
                if (API.CanCast(ChainHeal) && !API.MacroIsIgnored(ChainHeal + MO) && IsMouseover && API.MouseoverHealthPercent > 0 && (ChainHealAoE || API.PlayerHasBuff(UnleashLife) && UnitBelowHealthPercent(85) >= 2 && API.MouseoverHasBuff(Riptide)) && (!QuakingHelper || QuakingCH && QuakingHelper) && (!API.PlayerIsMoving || API.PlayerIsMoving && API.PlayerHasBuff(SpiritWalkersGrace)))
                {
                    API.CastSpell(ChainHeal + MO);
                    return;
                }
                if (API.CanCast(Wellspring) && WellspringTalent && WellSpringAoE && (!QuakingHelper || QuakingWellspring && QuakingHelper) && API.TargetHealthPercent > 0 && (!API.PlayerIsMoving || API.PlayerIsMoving && API.PlayerHasBuff(SpiritWalkersGrace)))
                {
                    API.CastSpell(Wellspring);
                    return;
                }
                if (API.CanCast(HealingSurge) && API.TargetHealthPercent <= HealingSurgeLifePercent && InRange && !API.PlayerCanAttackTarget && (!QuakingHelper || QuakingHS && QuakingHelper) && API.TargetHealthPercent > 0 && (!API.PlayerIsMoving || API.PlayerIsMoving && API.PlayerHasBuff(SpiritWalkersGrace)))
                {
                    API.CastSpell(HealingSurge);
                    return;
                }
                if (API.CanCast(HealingSurge) && !API.MacroIsIgnored(HealingSurge + MO) && IsMouseover && API.MouseoverHealthPercent <= HealingSurgeLifePercent && InRange && !API.PlayerCanAttackMouseover && (!QuakingHelper || QuakingHS && QuakingHelper) && API.MouseoverHealthPercent > 0 && (!API.PlayerIsMoving || API.PlayerIsMoving && API.PlayerHasBuff(SpiritWalkersGrace)))
                {
                    API.CastSpell(HealingSurge + MO);
                    return;
                }
                if (API.CanCast(HealingWave) && API.TargetHealthPercent <= HealingWaveLifePercent && InRange && !API.PlayerCanAttackTarget && (!QuakingHelper || QuakingHW && QuakingHelper) && API.TargetHealthPercent > 0 && (!API.PlayerIsMoving || API.PlayerIsMoving && API.PlayerHasBuff(SpiritWalkersGrace)))
                {
                    API.CastSpell(HealingWave);
                    return;
                }
                if (API.CanCast(HealingWave) && !API.MacroIsIgnored(HealingWave + MO) && IsMouseover && API.MouseoverHealthPercent <= HealingWaveLifePercent && InRange && !API.PlayerCanAttackMouseover && (!QuakingHelper || QuakingHW && QuakingHelper) && API.MouseoverHealthPercent > 0 && (!API.PlayerIsMoving || API.PlayerIsMoving && API.PlayerHasBuff(SpiritWalkersGrace)))
                {
                    API.CastSpell(HealingWave + MO);
                    return;
                }
                //DPS
                if (API.PlayerIsInCombat && !isExplosive)
                {
                    if (API.CanCast(FlameShock) && InRange && (!API.TargetHasDebuff(FlameShock) || API.TargetDebuffRemainingTime(FlameShock) < 600) && API.PlayerCanAttackTarget && API.TargetHealthPercent > 0)
                    {
                        API.CastSpell(FlameShock);
                        return;
                    }
                    if (API.CanCast(FlameShock) && IsMouseover && isMouseoverInCombat && InRange && (!API.MouseoverHasDebuff(FlameShock) || API.MouseoverDebuffRemainingTime(FlameShock) < 600) && API.PlayerCanAttackMouseover && API.MouseoverHealthPercent > 0)
                    {
                        API.CastSpell(FlameShock + "MO");
                        return;
                    }
                    if (API.CanCast(LavaBurst) && InRange && API.PlayerCanAttackTarget && (!API.PlayerHasBuff(LavaSurge) || API.PlayerHasBuff(LavaSurge)) && (!QuakingHelper || QuakingLavaburst && QuakingHelper) && API.TargetHealthPercent > 0 && (!API.PlayerIsMoving || API.PlayerIsMoving && API.PlayerHasBuff(SpiritWalkersGrace)))
                    {
                        API.CastSpell(LavaBurst);
                        return;
                    }
                    if (API.CanCast(LavaBurst) && IsMouseover && isMouseoverInCombat && InRange && API.PlayerCanAttackMouseover && (!API.PlayerHasBuff(LavaSurge) || API.PlayerHasBuff(LavaSurge)) && (!QuakingHelper || QuakingLavaburst && QuakingHelper) && API.MouseoverHealthPercent > 0 && (!API.PlayerIsMoving || API.PlayerIsMoving && API.PlayerHasBuff(SpiritWalkersGrace)))
                    {
                        API.CastSpell(LavaBurst + "MO");
                        return;
                    }
                    if (API.CanCast(ChainLightning) && InRange && API.PlayerCanAttackTarget && API.TargetUnitInRangeCount >= 3 && (!QuakingHelper || QuakingChainLight && QuakingHelper) && API.TargetHealthPercent > 0 && (!API.PlayerIsMoving || API.PlayerIsMoving && API.PlayerHasBuff(SpiritWalkersGrace)))
                    {
                        API.CastSpell(ChainLightning);
                        return;
                    }
                    if (API.CanCast(ChainLightning) && IsMouseover && isMouseoverInCombat && InRange && API.PlayerCanAttackMouseover && API.TargetUnitInRangeCount >= 3 && (!QuakingHelper || QuakingChainLight && QuakingHelper) && API.MouseoverHealthPercent > 0 && (!API.PlayerIsMoving || API.PlayerIsMoving && API.PlayerHasBuff(SpiritWalkersGrace)))
                    {
                        API.CastSpell(ChainLightning + "MO");
                        return;
                    }
                    if (API.CanCast(LightningBolt) && InRange && API.PlayerCanAttackTarget && (!QuakingHelper || QuakingLightning && QuakingHelper) && API.TargetHealthPercent > 0 && (!API.PlayerIsMoving || API.PlayerIsMoving && API.PlayerHasBuff(SpiritWalkersGrace)))
                    {
                        API.CastSpell(LightningBolt);
                        return;
                    }
                    if (API.CanCast(LightningBolt) && IsMouseover && isMouseoverInCombat && InRange && API.PlayerCanAttackMouseover && (!QuakingHelper || QuakingLightning && QuakingHelper) && API.MouseoverHealthPercent > 0 && (!API.PlayerIsMoving || API.PlayerIsMoving && API.PlayerHasBuff(SpiritWalkersGrace)))
                    {
                        API.CastSpell(LightningBolt + "MO");
                        return;
                    }
                }
                //Auto Target
                if (IsAutoSwap && (IsOOC || API.PlayerIsInCombat))
                {
                    if (!API.PlayerIsInGroup && !API.PlayerIsInRaid)
                    {
                        if (API.PlayerHealthPercent <= PlayerHP)
                        {
                            API.CastSpell(Player);
                            return;
                        }
                    }
                    if (API.PlayerIsInGroup && !API.PlayerIsInRaid)
                        {
                        if (API.PlayerHealthPercent <= PlayerHP && API.TargetIsUnit() != "player")
                        {
                            API.CastSpell(Player);
                            return;
                        }
                            for (int j = 0; j < DispellList.Length; j++)
                            for (int i = 0; i < units.Length; i++)
                            {
                                if (UnitHasDispellAble(DispellList[j], units[i]) && IsDispell && !API.SpellISOnCooldown(PurifySpirit) && API.TargetIsUnit() != units[i])
                                {
                                    API.CastSpell(PlayerTargetArray[i]);
                                    return;
                                }
                                if (API.UnitHealthPercent(units[i]) <= 20 && API.UnitHealthPercent(units[i]) > 0 && API.UnitRange(units[i]) <= 40 && API.TargetIsUnit() != units[i])
                                {
                                    API.CastSpell(PlayerTargetArray[i]);
                                    return;
                                }
                                if (API.UnitRoleSpec(units[i]) == RoleSpec && !UnitHasBuff(EarthShield, units[i]) && EarthShieldTracking && API.UnitRange(units[i]) <= 40 && API.UnitHealthPercent(units[i]) > 0 && API.TargetIsUnit() != units[i])
                                {
                                    API.CastSpell(PlayerTargetArray[i]);
                                    return;
                                }
                                if (API.UnitRoleSpec(units[i]) == API.TankRole && (!SwapWatch.IsRunning || SwapWatch.ElapsedMilliseconds >= API.SpellGCDTotalDuration * 10) && API.UnitHealthPercent(units[i]) <= TankHealth && API.UnitHealthPercent(units[i]) > 0 && API.TargetIsUnit() != units[i])
                                {
                                    API.CastSpell(PlayerTargetArray[i]);
                                    SwapWatch.Restart();
                                    return;
                                }
                                if (LowestParty(units) == units[i] && (!SwapWatch.IsRunning || SwapWatch.ElapsedMilliseconds >= API.SpellGCDTotalDuration * 10) && API.UnitHealthPercent(units[i]) <= UnitHealth && API.TargetIsUnit() != units[i])
                                {
                                    API.CastSpell(PlayerTargetArray[i]);
                                    SwapWatch.Restart();
                                    return;
                                }
                                if (IsDPS && !API.PlayerCanAttackTarget && API.UnitRoleSpec(units[i]) == API.TankRole && !API.MacroIsIgnored("Assist") && UnitAboveHealthPercentParty(AoEDPSHLifePercent) >= AoEDPSNumber && API.UnitRange(units[i]) <= 40 && API.UnitHealthPercent(units[i]) > 0 && API.PlayerIsInCombat && API.TargetIsUnit() != units[i])
                                {
                                    API.CastSpell(PlayerTargetArray[i]);
                                    API.CastSpell("Assist");
                                    return;
                                }
                            }
                        }
                    if (API.PlayerIsInRaid)
                    {
                        if (API.PlayerHealthPercent <= PlayerHP)
                        {
                            API.CastSpell(Player);
                            return;
                        }
                        for (int i = 0; i < raidunits.Length; i++)
                        {
                            if (API.UnitHealthPercent(raidunits[i]) <= 10 && API.UnitHealthPercent(raidunits[i]) > 0 && API.UnitRange(raidunits[i]) <= 40 && !UnitHasDebuff("Gluttonous Miasma", raidunits[i]) && API.TargetIsUnit() != raidunits[i])
                            {
                                API.CastSpell(RaidTargetArray[i]);
                                return;
                            }
                            if (API.UnitRoleSpec(raidunits[i]) == RoleSpec && !UnitHasBuff(EarthShield, raidunits[i]) && EarthShieldTracking && API.UnitRange(raidunits[i]) <= 40 && API.UnitHealthPercent(raidunits[i]) > 0 && API.TargetIsUnit() != raidunits[i])
                            {
                                API.CastSpell(RaidTargetArray[i]);
                                return;
                            }
                            if (API.UnitRoleSpec(raidunits[i]) == API.TankRole && (!SwapWatch.IsRunning || SwapWatch.ElapsedMilliseconds >= API.SpellGCDTotalDuration * 10) && API.UnitHealthPercent(raidunits[i]) <= TankHealth && API.UnitHealthPercent(raidunits[i]) > 0 & !UnitHasDebuff("Gluttonous Miasma", raidunits[i]) && API.TargetIsUnit() != raidunits[i])
                            {
                                API.CastSpell(RaidTargetArray[i]);
                                SwapWatch.Restart();
                                return;
                            }
                            if (LowestRaid(raidunits) == raidunits[i] && (!SwapWatch.IsRunning || SwapWatch.ElapsedMilliseconds >= API.SpellGCDTotalDuration * 10) && API.UnitHealthPercent(raidunits[i]) <= UnitHealth && !UnitHasDebuff("Gluttonous Miasma", raidunits[i]) && API.TargetIsUnit() != raidunits[i])
                            {
                                API.CastSpell(RaidTargetArray[i]);
                                SwapWatch.Restart();
                                return;
                            }
                            if (IsDPS && !API.PlayerCanAttackTarget && API.UnitRange(raidunits[i]) <= 40 && API.UnitRoleSpec(raidunits[i]) == API.TankRole && !API.MacroIsIgnored("Assist") && UnitAboveHealthPercentRaid(AoEDPSHRaidLifePercent) >= AoEDPSRaidNumber && (!SwapWatch.IsRunning || SwapWatch.ElapsedMilliseconds >= API.SpellGCDTotalDuration * 10) && API.UnitHealthPercent(raidunits[i]) > 0 && API.PlayerIsInCombat && API.TargetIsUnit() != raidunits[i])
                            {
                                API.CastSpell(RaidTargetArray[i]);
                                SwapWatch.Restart();
                                API.CastSpell("Assist");
                                return;
                            }

                        }
                    }
                }
            }
        }
        public override void CombatPulse()
        {
            if (API.CanCast(Fleshcraft) && PlayerCovenantSettings == "Necrolord" && API.PlayerHealthPercent <= FleshcraftPercentProc && NotChanneling && !API.PlayerIsMoving)
            {
                API.CastSpell(Fleshcraft);
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
            if (PlayerRaceSettings == "Orc" && API.CanCast(RacialSpell1) && isRacial && IsCooldowns)
            {
                API.CastSpell(RacialSpell1);
                return;
            }
            if (PlayerRaceSettings == "Troll" && API.CanCast(RacialSpell1) && isRacial && IsCooldowns)
            {
                API.CastSpell(RacialSpell1);
                return;
            }
            if (PlayerRaceSettings == "Lightforged" && API.CanCast(RacialSpell1) && isRacial && IsCooldowns)
            {
                API.CastSpell(RacialSpell1);
                return;
            }
            if (PlayerRaceSettings == "Dark Iron Dwarf" && API.CanCast(RacialSpell1) && isRacial && IsCooldowns)
            {
                API.CastSpell(RacialSpell1);
                return;
            }
            if (PlayerRaceSettings == "Mag'har Orc" && API.CanCast(RacialSpell1) && isRacial && IsCooldowns)
            {
                API.CastSpell(RacialSpell1);
                return;
            }
            if (PlayerRaceSettings == "Vulpera" && API.CanCast(RacialSpell1) && isRacial && IsCooldowns)
            {
                API.CastSpell(RacialSpell1);
                return;
            }
            if (API.PlayerTrinketIsUsable(1) && API.PlayerTrinketRemainingCD(1) == 0 && IsTrinkets1 && NotChanneling && InRange)
            {
                API.CastSpell("Trinket1");
            }
            if (API.PlayerTrinketIsUsable(2) && API.PlayerTrinketRemainingCD(2) == 0 && IsTrinkets2 && NotChanneling && InRange) 
            {
                API.CastSpell("Trinket2");
            }
        }
        private void AutoTarget2()
        {
  
        }
        public override void OutOfCombatPulse()
        {
            {
                if (API.PlayerCurrentCastTimeRemaining > 40)
                    return;
                if (AutoWolf && API.CanCast(GhostWolf) && !API.PlayerHasBuff(GhostWolf) && !API.PlayerIsMounted && API.PlayerIsMoving)
                {
                    API.CastSpell(GhostWolf);
                    return;
                }
            }

        }

    }
}



