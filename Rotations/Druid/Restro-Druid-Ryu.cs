﻿//V1.5 - NPC Healing Updated -- Fix for Hungering - Small Adjustments
using System.Linq;
using System.Diagnostics;

namespace HyperElk.Core
{
    public class RestoDruid : CombatRoutine
    {
        //Spell Strings
        private string Rejuvenation = "Rejuvenation";
        private string Regrowth = "Regrowth";
        private string Lifebloom = "Lifebloom";
        private string WildGrowth = "Wild Growth";
        private string Swiftmend = "Swiftmend";
        private string Tranquility = "Tranquility";
        private string Moonfire = "Moonfire";
        private string Sunfire = "Sunfire";
        private string Wrath = "Wrath";
        private string Innervate = "Innervate";
        private string Ironbark = "Ironbark";
        private string Natureswiftness = "Nature's Swiftness";
        private string Barkskin = "Barkskin";
        private string Bearform = "Bear Form";
        private string Catform = "Cat Form";
        private string NaturesCure = "Nature's Cure";
        private string EntanglingRoots = "Entanngling Roots";
        private string Soothe = "Soothe";
      //  private string KindredSprirts = "Kindred Spirits";
        private string AdaptiveSwarm = "Adaptive Swarm";
        private string Fleshcraft = "Fleshcraft";
        private string Convoke = "Convoke the Spirits";
        private string RavenousFrenzy = "Ravenous Frenzy";
        private string Nourish = "Nourish";
        private string CenarionWard = "Cenarion Ward";
        private string TreeofLife = "Incarnation: Tree of Life";
        private string Overgrowth = "Overgrowth";
        private string Flourish = "Flourish";
        private string Renewal = "Renewal";
        private string AoE = "AOE";
     //   private string AoEP = "AOE Party";
    //    private string AoER = "AOE Raid";
        private string AoEDPS = "AOEDPS";
        private string AoEDPSRaid = "AOEDPS Raid";
        private string AoEDPSH = "AOEDPS Health";
        private string AoEDPSHRaid = "AOEDPS Health Raid";
        private string GerminationHoT = "Rejuvenation (Germination)";
        private string Clear = "Clearcasting";
        private string HeartoftheWild = "Heart of the Wild";
        private string TravelForm = "Travel Form";
        private string Soulshape = "Soulshape";
        private string CatForm = "Cat Form";
        private string BearForm = "Bear Form";
        private string MoonkinForm = "Moonkin Form";
        private string PhialofSerenity = "Phial of Serenity";
        private string SpiritualHealingPotion = "Spiritual Healing Potion";
        private string Trinket1 = "Trinket1";
        private string Trinket2 = "Trinket2";
       // private string Mana = "Mana";
        private string FrenziedRegeneration = "Frenzied Regeneration";
        private string Ironfur = "Ironfur";
        private string Trinket = "Trinket";
        private string SouloftheForest = "Soul of the Forest";
        private string CenarionWardPlayer = "Cenarian Ward Player";
        private string Party1 = "party1";
        private string Party2 = "party2";
        private string Party3 = "party3";
        private string Party4 = "party4";
        private string Player = "player";
        private string PartySwap = "Target Swap";
        private string LifebloomL = "LifebloomL";
        private string TargetChange = "Target Change";
        private string AoERaid = "AOE Healing Raid";
        private string EclipseLunar = "Eclispe (Lunar)";
        private string EclipseSolar = "Eclispe (Solar)";
        private string Starfire = "Starfire";
        private string Starsurge = "Starsurge";
        private string Efflor = "Effloresence";
        private string Thrashbear = "Thrash Bear";
        private string Swipebear = "Swipe Bear";
        private string Mangle = "Mangle";
        private string Rip = "Rip";
        private string Rake = "Rake";
        private string Shred = "Shred";
        private string FerociousBite = "Ferocious Bite";
        private string Swipekitty = "Swipe Cat";
        private string Thrashkitty = "Thrash Cat";
        private string Quake = "Quake";
     //   private string Wake = "The Necrotic Wake";
     //   private string OtherSide = "De Other Side";
     //   private string Halls = "Halls of Atonement";
     //   private string Mists = "Mists of Tirna Scithe";
     //   private string Depths = "Sanguine Depths";
     //   private string Plague = "Plaguefall";
    //    private string Spires = "Spires of Ascension";
    //    private string ToP = "Theater of Pain";
        private string SwapSpeed = "Target Swap Speed";
        private string MO = "MO";
        private string Verdant = "Verdant Infusion";
        private string CenarionWardHoT = "Cenarion Ward HoT";

        //Talents
        bool AbundanceTalent => API.PlayerIsTalentSelected(1, 1);
        bool NourishTalent => API.PlayerIsTalentSelected(1, 2);
        bool CenarionWardTalent => API.PlayerIsTalentSelected(1, 3);
        bool TigerDashTalent => API.PlayerIsTalentSelected(2, 1);
        bool RenewalTalent => API.PlayerIsTalentSelected(2, 2);
        bool WildChargeTalent => API.PlayerIsTalentSelected(2, 3);
        bool BalanceAffinity => API.PlayerIsTalentSelected(3, 1);
        bool FeralAffinity => API.PlayerIsTalentSelected(3, 2);
        bool GuardianAffintiy => API.PlayerIsTalentSelected(3, 3);
        bool MightyBashTalent => API.PlayerIsTalentSelected(4, 1);
        bool MassEntanglementTalent => API.PlayerIsTalentSelected(4, 2);
        bool HeartoftheWildTalent => API.PlayerIsTalentSelected(4, 3);
        bool SouloftheForestTalent => API.PlayerIsTalentSelected(5, 1);
        bool CultivationTalent => API.PlayerIsTalentSelected(5, 2);
        bool TreeofLifeTalent => API.PlayerIsTalentSelected(5, 3);
        bool InnerPeaceTalent => API.PlayerIsTalentSelected(6, 1);
        bool SpringBlossomsTalent => API.PlayerIsTalentSelected(6, 2);
        bool OvergrowthTalent => API.PlayerIsTalentSelected(6, 3);
        bool PhotosynthesisTalent => API.PlayerIsTalentSelected(7, 1);
        bool GerminationTalent => API.PlayerIsTalentSelected(7, 2);
        bool FlourishTalent => API.PlayerIsTalentSelected(7, 3);

        //CBProperties
        int[] numbList = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100 };
        int[] numbPartyList = new int[] { 0, 1, 2, 3, 4, 5, };
        int[] numbRaidList = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 33, 35, 36, 37, 38, 39, 40 };
        int[] SwapSpeedList = new int[] { 1000, 1250, 1500, 1750, 2000, 2250, 2500, 2750, 3000 };
        public string[] LegendaryList = new string[] { "None", "Verdant Infusion", "The Dark Titan's Lesson" };
        public string[] LifeTarget = new string[] { "Tank", "DPS", "Healer" };
        private string UseLife => LifeTarget[CombatRoutine.GetPropertyInt("Use Lifebloom")];
        private string UseLifeTorghast => PlayerTargetArray[CombatRoutine.GetPropertyInt("Use Lifebloom in Torghast")];
        private string TankinTorghast => PlayerTargetArray[CombatRoutine.GetPropertyInt("Tank in Torghast")];


        private int RoleSpec
        {
            get
            {
                if (UseLife == "Tank")
                    return 999;
                else if (UseLife == "DPS")
                    return 997;
                else if (UseLife == "Healer")
                    return 998;
                return 999;
            }
        }

        private int LowestHpPartyUnit()
        {
            int lowest = 100;

            for (int i = 0; i < units.Length; i++)
            {
                if (API.UnitHealthPercent(units[i]) < lowest && API.UnitHealthPercent(units[i]) > 0)
                    lowest = API.UnitHealthPercent(units[i]);
            }
            return lowest;
        }
        private int LowestHpRaidUnit()
        {
            int lowest = 100;

            for (int i = 0; i < raidunits.Length; i++)
            {
                if (API.UnitHealthPercent(raidunits[i]) < lowest && API.UnitHealthPercent(raidunits[i]) > 0)
                    lowest = API.UnitHealthPercent(raidunits[i]);
            }
            return lowest;
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
        private string LowestRaidTank(string[] raidunits)
        {
            string lowest = raidunits[0];
            int health = 100;
            foreach (string raidunit in raidunits)
            {
                if (API.UnitRoleSpec(raidunit) == API.TankRole && API.UnitHealthPercent(raidunit) < health && API.UnitRange(raidunit) <= 40 && API.UnitHealthPercent(raidunit) > 0 && API.UnitHealthPercent(raidunit) != 100)
                {
                    lowest = raidunit;
                    health = API.UnitHealthPercent(raidunit);
                }
            }
            return lowest;
        }
        int PlayerHealth => API.TargetHealthPercent;
        string[] PlayerTargetArray = { "player", "party1", "party2", "party3", "party4" };
        string[] RaidTargetArray = { "raid1", "raid2", "raid3", "raid4", "raid5", "raid6", "raid7", "raid8", "raid9", "raid8", "raid9", "raid10", "raid11", "raid12", "raid13", "raid14", "raid16", "raid17", "raid18", "raid19", "raid20", "raid21", "raid22", "raid23", "raid24", "raid25", "raid26", "raid27", "raid28", "raid29", "raid30", "raid31", "raid32", "raid33", "raid34", "raid35", "raid36", "raid37", "raid38", "raid39", "raid40" };
        string[] NecoritcWakeDispell = { "Chilled", "Frozen Binds", "Clinging Darkness", "Rasping Scream", "Heaving Retch", "Goresplatter" };
        string[] FightSelection = { "Shade on Barghast", "Sun King" };
        string[] PlaugeFallDispell = { "Slime Injection", "Gripping Infection", "Cytotoxic Slash", "Venompiercer", "Wretched Phlegm" };
        string[] MistsofTirnaScitheDispell = { "Repulsive Visage", "Soul Split", "Anima Injection", "Bewildering Pollen", "Bramblethorn Entanglement", "Dying Breath", "Debilitating Poison", };
        string[] HallofAtonementDispell = { "Sinlight Visions", "Siphon Life", "Turn to Stone", "Stony Veins", "Curse of Stone", "Turned to Stone", "Curse of Obliteration" };
        string[] SanguineDepthsDispell = { "Anguished Cries", "Wrack Soul", "Sintouched Anima", "Curse of Suppression", "Explosive Anger" };
        string[] TheaterofPainDispell = { "Soul Corruption", "Spectral Reach", "Death Grasp", "Shadow Vulnerability", "Curse of Desolation" };
        string[] DeOtherSideDispell = { "Cosmic Artifice", "Wailing Grief", "Shadow Word:  Pain", "Soporific Shimmerdust", "Soporific Shimmerdust 2", "Hex" };
        string[] SpireofAscensionDispell = { "Dark Lance", "Insidious Venom", "Charged Anima", "Lost Confidence", "Burden of Knowledge", "Internal Strife", "Forced Confession", "Insidious Venom 2" };
        string[] DispellList = { "Chilled", "Frozen Binds", "Clinging Darkness", "Rasping Scream", "Slime Injection", "Gripping Infection", "Cytotoxic Slash", "Venompiercer", "Wretched Phlegm", "Repulsive Visage", "Soul Split", "Anima Injection", "Bewildering Pollen", "Bramblethorn Entanglement", "Dying Breath", "Debilitating Poison", "Sinlight Visions", "Siphon Life", "Turn to Stone", "Stony Veins", "Curse of Stone", "Turned to Stone", "Curse of Obliteration", "Anguished Cries", "Wrack Soul", "Sintouched Anima", "Curse of Suppression", "Explosive Anger", "Soul Corruption", "Spectral Reach", "Death Grasp", "Shadow Vulnerability", "Curse of Desolation", "Cosmic Artifice", "Wailing Grief", "Shadow Word:  Pain", "Soporific Shimmerdust", "Soporific Shimmerdust 2", "Hex", "Dark Lance", "Insidious Venom", "Charged Anima", "Lost Confidence", "Burden of Knowledge", "Internal Strife", "Forced Confession", "Insidious Venom 2", "Burst" };
        //  public string[] InstanceList = { "The Necrotic Wake", "De Other Side", "Halls of Atonement", "Mists of Tirna Scithe", "Plaguefall", "Sanguine Depths", "Spires of Ascension", "Theater of Pain" };
        private static readonly Stopwatch LifeBloomwatch = new Stopwatch();
        private static readonly Stopwatch EfflorWatch = new Stopwatch();
        private static readonly Stopwatch SwapWatch = new Stopwatch();
        private static readonly Stopwatch DispelWatch = new Stopwatch();
        private static readonly Stopwatch Solarwatch = new Stopwatch();
        private static readonly Stopwatch Lunarwatch = new Stopwatch();

        private string UseLeg => LegendaryList[CombatRoutine.GetPropertyInt("Legendary")];
        private string FightNPC => FightSelection[CombatRoutine.GetPropertyInt("Fight Selection")];
        // private string UseDispell => InstanceList[CombatRoutine.GetPropertyInt("Instance List")];
        private string[] units = { "player", "party1", "party2", "party3", "party4" };
        private string[] TargetingUnits = { "party1", "party2", "party3", "party4" };
        private string[] raidunits = { "raid1", "raid2", "raid3", "raid4", "raid5", "raid6", "raid7", "raid8", "raid9", "raid8", "raid9", "raid10", "raid11", "raid12", "raid13", "raid14", "raid16", "raid17", "raid18", "raid19", "raid20", "raid21", "raid22", "raid23", "raid24", "raid25", "raid26", "raid27", "raid28", "raid29", "raid30", "raid31", "raid32", "raid33", "raid34", "raid35", "raid36", "raid37", "raid38", "raid39", "raid40" };
        private int UnitBelowHealthPercentRaid(int HealthPercent) => raidunits.Count(p => API.UnitHealthPercent(p) <= HealthPercent && API.UnitHealthPercent(p) > 0);
        private int UnitBelowHealthPercentParty(int HealthPercent) => units.Count(p => API.UnitHealthPercent(p) <= HealthPercent && API.UnitHealthPercent(p) > 0);
        private int UnitBelowHealthPercent(int HealthPercent) => API.PlayerIsInRaid ? UnitBelowHealthPercentRaid(HealthPercent) : UnitBelowHealthPercentParty(HealthPercent);
        private int UnitAboveHealthPercentRaid(int HealthPercent) => raidunits.Count(p => API.UnitHealthPercent(p) >= HealthPercent && API.UnitHealthPercent(p) > 0);
        private int UnitAboveHealthPercentParty(int HealthPercent) => units.Count(p => API.UnitHealthPercent(p) >= HealthPercent && API.UnitHealthPercent(p) > 0);
        private int UnitAboveHealthPercent(int HealthPercent) => API.PlayerIsInRaid ? UnitAboveHealthPercentRaid(HealthPercent) : UnitAboveHealthPercentParty(HealthPercent);

        private int FlourishRaidTracking(string buff) => raidunits.Count(p => API.UnitHasBuff(buff, p) && API.UnitBuffPlayerSrc(buff, p));
        private int FlourishPartyTracking(string buff) => units.Count(p => API.UnitHasBuff(buff, p) && API.UnitBuffPlayerSrc(buff, p));
        private int BuffRaidTracking(string buff) => raidunits.Count(p => API.UnitHasBuff(buff, p) && API.UnitBuffPlayerSrc(buff, p));
        private int BuffPartyTracking(string buff) => units.Count(p => API.UnitHasBuff(buff, p) && API.UnitBuffPlayerSrc(buff, p));
        private int BuffTracking(string buff) => API.PlayerIsInRaid ? BuffRaidTracking(buff) : BuffPartyTracking(buff);
        private int RangePartyTracking(int Range) => units.Count(p => API.UnitRange(p) <= Range);
        private int RangeRaidTracking(int Range) => raidunits.Count(p => API.UnitRange(p) <= Range);
        private int RangeTracking(int Range) => API.PlayerIsInRaid ? RangeRaidTracking(Range) : RangePartyTracking(Range);


        // private int FlourishTracking(string buff) => API.PlayerIsInRaid ? FlourishRaidTracking(Rejuvenation) : FlourishPartyTracking(Rejuvenation);

        private bool QuakingHelper => CombatRoutine.GetPropertyBool("QuakingHelper");
        private bool TorghastHelper => CombatRoutine.GetPropertyBool("In Torghast");

        bool ChannelingCov => API.CurrentCastSpellID("player") == 323764;
        bool ChannelingTranq => API.CurrentCastSpellID("player") == 740;
        private bool LifebloomPartyLTracking => BuffPartyTracking(LifebloomL) < 2;
        private bool LifebloomRaidLTracking => BuffRaidTracking(LifebloomL) < 2;
        private bool LifeBloomLTracking => API.PlayerIsInRaid ? LifebloomRaidLTracking : LifebloomPartyLTracking;
        private bool LifeBloomTracking => API.PlayerIsInRaid ? BuffRaidTracking(Lifebloom) < 1 : BuffPartyTracking(Lifebloom) < 1;
        private bool TrinketAoE => UnitBelowHealthPercent(TrinketLifePercent) >= AoENumber;
        private bool ConvokeAoE => UnitBelowHealthPercent(ConvLifePercent) >= AoENumber && (!API.PlayerCanAttackTarget || !API.PlayerCanAttackMouseover) && NotChanneling && !API.PlayerIsMoving && !ChannelingTranq;
        private bool WGAoE => UnitBelowHealthPercent(WGLifePercent) >= AoENumber && (!API.PlayerCanAttackTarget || !API.PlayerCanAttackMouseover) && NotChanneling && !API.PlayerIsMoving && !ChannelingCov && !ChannelingTranq;
        private bool ToLAoE => UnitBelowHealthPercent(ToLLifePercent) >= AoENumber && !API.PlayerCanAttackTarget && NotChanneling && !ChannelingCov && !ChannelingTranq;
        private bool TranqAoE => API.PlayerIsInRaid ? UnitBelowHealthPercentRaid(TranqLifePercent) >= AoERaidNumber : UnitBelowHealthPercentParty(TranqLifePercent) >= AoENumber && !API.PlayerIsMoving && !ChannelingCov;
        private bool FloruishRejTracking => API.PlayerIsInRaid ? FlourishRaidTracking(Rejuvenation) >= AoERaidNumber : FlourishPartyTracking(Rejuvenation) >= AoENumber;
        private bool FlourishRegTracking => API.PlayerIsInRaid ? FlourishRaidTracking(Regrowth) >= 1 : FlourishPartyTracking(Regrowth) >= 1;
        private bool FlourishLifeTracking => API.PlayerIsInRaid ? FlourishRaidTracking(Lifebloom) >= 1 : FlourishPartyTracking(Lifebloom) >= 1;
        private bool FlourishWGTracking => API.PlayerIsInRaid ? FlourishRaidTracking(WildGrowth) >= AoENumber : FlourishPartyTracking(WildGrowth) >= AoENumber;
        private bool FlourishTranqTracking => API.PlayerIsInRaid ? FlourishRaidTracking(Tranquility) >= AoERaidNumber : FlourishPartyTracking(Tranquility) >= AoENumber;
        private bool FlourishCWTracking => API.PlayerIsInRaid ? FlourishRaidTracking(CenarionWardHoT) == 1 : FlourishPartyTracking(CenarionWardHoT) == 1;
        private bool FloruishAoE => UnitBelowHealthPercent(FloruishLifePercent) >= AoENumber && !API.PlayerCanAttackTarget && NotChanneling && !ChannelingCov && !ChannelingTranq && (!API.PlayerIsMoving || API.PlayerIsMoving);
        private bool CWCheck => CenarionWardTalent && (API.TargetRoleSpec == API.TankRole && API.TargetHealthPercent <= CWTankLifePercent || API.TargetHealthPercent <= CWPlayerLifePercent) && !API.PlayerCanAttackTarget && NotChanneling && !ChannelingCov && !ChannelingTranq && (!API.PlayerIsMoving || API.PlayerIsMoving);
        private bool CWMOCheck => CenarionWardTalent && (API.MouseoverRoleSpec == API.TankRole && API.MouseoverHealthPercent <= CWTankLifePercent || API.MouseoverHealthPercent <= CWPlayerLifePercent) && !API.PlayerCanAttackMouseover && NotChanneling && !ChannelingCov && !ChannelingTranq && (!API.PlayerIsMoving || API.PlayerIsMoving);
        private bool NatureSwiftCheck => (API.TargetHealthPercent <= NSLifePercent || IsMouseover && API.MouseoverHealthPercent <= NSLifePercent) && NotChanneling && !ChannelingCov && !ChannelingTranq && (!API.PlayerIsMoving || API.PlayerIsMoving) && (!API.PlayerCanAttackTarget || !API.PlayerCanAttackMouseover && IsMouseover);
        private bool OvergrowthCheck => OvergrowthTalent && API.TargetHealthPercent <= OvergrowthLifePercent && NotChanneling && !ChannelingCov && !ChannelingTranq && (!API.PlayerIsMoving || API.PlayerIsMoving) && (!TargetHasBuff(Lifebloom) || !TargetHasBuff(WildGrowth) || !TargetHasBuff(Rejuvenation) || !TargetHasBuff(Regrowth) && !API.PlayerCanAttackTarget);
        private bool OvergrowthMOCheck => OvergrowthTalent && API.MouseoverHealthPercent <= OvergrowthLifePercent && NotChanneling && !ChannelingCov && !ChannelingTranq && (!API.PlayerIsMoving || API.PlayerIsMoving) && (!MouseoverHasBuff(Lifebloom) || !MouseoverHasBuff(WildGrowth) || !MouseoverHasBuff(Rejuvenation) || !MouseoverHasBuff(Regrowth) && !API.PlayerCanAttackMouseover);
        private bool InnervateCheck => API.PlayerMana <= ManaPercent && NotChanneling && !ChannelingCov && !ChannelingTranq && (!API.PlayerIsMoving || API.PlayerIsMoving);
        private bool IBCheck => API.TargetHealthPercent <= IronBarkLifePercent && !API.PlayerCanAttackTarget && NotChanneling && !ChannelingCov && !ChannelingTranq && (!API.PlayerIsMoving || API.PlayerIsMoving);
        private bool IBMOCheck => API.MouseoverHealthPercent <= IronBarkLifePercent && !API.PlayerCanAttackTarget && NotChanneling && !ChannelingCov && !ChannelingTranq && (!API.PlayerIsMoving || API.PlayerIsMoving);
        private bool NourishCheck => NourishTalent && API.TargetHealthPercent <= NourishLifePercent && !API.PlayerCanAttackTarget && NotChanneling && !ChannelingCov && !ChannelingTranq && !API.PlayerIsMoving;
        private bool NourishMOCheck => NourishTalent && API.MouseoverHealthPercent <= NourishLifePercent && !API.PlayerCanAttackMouseover && NotChanneling && !ChannelingCov && !ChannelingTranq && !API.PlayerIsMoving;
        private bool RegrowthCheck => (API.PlayerHasBuff(Clear) && API.TargetHealthPercent <= 90 || API.TargetHealthPercent <= RegrowthLifePercent) && !API.PlayerCanAttackTarget && NotChanneling && !ChannelingCov && !ChannelingTranq && !API.PlayerIsMoving;
        private bool RegrowthMOCheck => (API.PlayerHasBuff(Clear) && API.MouseoverHealthPercent <= 90 || API.MouseoverHealthPercent <= RegrowthLifePercent) && !API.PlayerCanAttackMouseover && NotChanneling && !ChannelingCov && !ChannelingTranq && !API.PlayerIsMoving;
        private bool RejCheck => API.TargetHealthPercent <= RejLifePercent && !API.PlayerCanAttackTarget && !TargetHasBuff(Rejuvenation) && !ChannelingCov && !ChannelingTranq && NotChanneling && (!API.PlayerIsMoving || API.PlayerIsMoving);
        private bool RejMOCheck => API.MouseoverHealthPercent <= RejLifePercent && !API.PlayerCanAttackMouseover && !API.MouseoverHasBuff(Rejuvenation) && !ChannelingCov && !ChannelingTranq && NotChanneling && (!API.PlayerIsMoving || API.PlayerIsMoving);
        private bool RejGermCheck => !API.PlayerCanAttackTarget && GerminationTalent && !TargetHasBuff(GerminationHoT) && API.TargetHealthPercent <= RejGermLifePercent && !ChannelingCov && !ChannelingTranq && NotChanneling && (!API.PlayerIsMoving || API.PlayerIsMoving);
        private bool RejGermMOCheck => !API.PlayerCanAttackMouseover && GerminationTalent && !MouseoverHasBuff(GerminationHoT) && API.MouseoverHealthPercent <= RejGermLifePercent && !ChannelingCov && !ChannelingTranq && NotChanneling && (!API.PlayerIsMoving || API.PlayerIsMoving);
        private bool SwiftCheck => !API.PlayerCanAttackTarget && API.SpellCharges(Swiftmend) > 0 && (API.TargetHealthPercent <= SwiftmendLifePercent && UseLeg != Verdant && (TargetHasBuff(Rejuvenation) || TargetHasBuff(Regrowth) || TargetHasBuff(WildGrowth)) || UseLeg == Verdant && TargetHasBuff(Rejuvenation) && TargetHasBuff(Lifebloom) && (TargetHasBuff(CenarionWardHoT) && CenarionWardTalent || CenarionWardTalent && API.SpellISOnCooldown(CenarionWard) && API.TargetHealthPercent <= SwiftmendLifePercent || !CenarionWardTalent) && API.TargetRoleSpec == API.TankRole || UseLeg == Verdant && TargetHasBuff(Rejuvenation) && TargetHasBuff(Lifebloom) && API.TargetHealthPercent <= 15) && (!API.PlayerIsMoving || API.PlayerIsMoving) && NotChanneling && !ChannelingCov && !ChannelingTranq;
       private bool SwiftMOCheck => !API.PlayerCanAttackMouseover && API.SpellCharges(Swiftmend) > 0 && (API.MouseoverHealthPercent <= SwiftmendLifePercent && UseLeg != Verdant && (MouseoverHasBuff(Rejuvenation) || MouseoverHasBuff(Regrowth) || MouseoverHasBuff(WildGrowth)) || UseLeg == Verdant && MouseoverHasBuff(Rejuvenation) && MouseoverHasBuff(Lifebloom) && (MouseoverHasBuff(CenarionWardHoT) && CenarionWardTalent || CenarionWardTalent && API.SpellISOnCooldown(CenarionWard) && API.MouseoverHealthPercent <= SwiftmendLifePercent || !CenarionWardTalent) && API.MouseoverRoleSpec == API.TankRole || UseLeg == Verdant && MouseoverHasBuff(Rejuvenation) && MouseoverHasBuff(Lifebloom) && API.MouseoverHealthPercent <= 15) && (!API.PlayerIsMoving || API.PlayerIsMoving) && NotChanneling && !ChannelingCov && !ChannelingTranq;
        private bool LifeBloomCheck => (API.TargetHealthPercent <= LifebloomLifePercent || IsAutoSwap && API.TargetHealthPercent <= 100) && UseLeg != "The Dark Titan's Lesson" && !API.PlayerCanAttackTarget && !TargetHasBuff(Lifebloom) && (!PhotosynthesisTalent && (API.TargetRoleSpec == RoleSpec || TorghastHelper && API.TargetIsUnit() == UseLifeTorghast) || PhotosynthesisTalent && (API.TargetRoleSpec == API.HealerRole || API.TargetRoleSpec == RoleSpec || TorghastHelper && API.TargetIsUnit() == UseLifeTorghast)) && LifeBloomTracking && NotChanneling && !ChannelingCov && !ChannelingTranq && (!API.PlayerIsMoving || API.PlayerIsMoving);
       private bool LifeBloomMOCheck => API.MouseoverHealthPercent <= LifebloomLifePercent && UseLeg != "The Dark Titan's Lesson" && !API.PlayerCanAttackMouseover && !TargetHasBuff(Lifebloom) && (!PhotosynthesisTalent && (API.MouseoverRoleSpec == RoleSpec || TorghastHelper && API.MouseoverIsUnit() == UseLifeTorghast) || PhotosynthesisTalent && (API.MouseoverRoleSpec == API.HealerRole || API.MouseoverRoleSpec == RoleSpec || TorghastHelper && API.MouseoverIsUnit() == UseLifeTorghast)) && LifeBloomTracking && NotChanneling && !ChannelingCov && !ChannelingTranq && (!API.PlayerIsMoving || API.PlayerIsMoving);

        private bool LifeBloom2Check => API.TargetHealthPercent <= LifebloomLifePercent && !API.PlayerCanAttackTarget && !TargetHasBuff(Lifebloom) && (UseLeg == "The Dark Titan's Lesson" && (API.TargetRoleSpec == API.HealerRole || API.TargetRoleSpec == API.TankRole) || PhotosynthesisTalent && (!LifeBloomwatch.IsRunning || LifeBloomwatch.ElapsedMilliseconds >= 15000) && API.TargetRoleSpec == API.HealerRole || API.TargetRoleSpec == API.TankRole && !TargetHasBuff(Lifebloom)) && NotChanneling && !ChannelingCov && !ChannelingTranq && (!API.PlayerIsMoving || API.PlayerIsMoving);
        private bool LifeBloomLegCheck => (API.TargetHealthPercent <= LifebloomLifePercent || IsAutoSwap && API.TargetHealthPercent <= 100) && !API.PlayerCanAttackTarget && !TargetHasBuff(LifebloomL) && LifeBloomLTracking && UseLeg == "The Dark Titan's Lesson" && (API.TargetRoleSpec == API.HealerRole && API.PlayerIsTargetTarget || API.TargetRoleSpec == RoleSpec || TorghastHelper && API.TargetIsUnit() == UseLifeTorghast || TorghastHelper && API.TargetIsUnit() == Player) && !ChannelingCov;
        private bool LifeBloomLegMOCheck => API.MouseoverHealthPercent <= LifebloomLifePercent && !API.PlayerCanAttackTarget && !MouseoverHasBuff(LifebloomL) && LifeBloomLTracking && UseLeg == "The Dark Titan's Lesson" && (API.MouseoverRoleSpec == API.HealerRole || API.MouseoverRoleSpec == RoleSpec || TorghastHelper && API.MouseoverIsUnit() == UseLifeTorghast) && !ChannelingCov;
        private bool FloruishCheck => (FloruishRejTracking && FlourishLifeTracking && FlourishRegTracking || FlourishWGTracking && FlourishTranqTracking || FloruishRejTracking && FlourishLifeTracking && UseLeg == Verdant && FlourishCWTracking) && !API.PlayerCanAttackTarget && FloruishAoE && FlourishTalent;
        private bool KyrianCheck => PlayerCovenantSettings == "Kyrian" && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" || UseCovenant == "on AOE" && IsAOE) && NotChanneling && !API.PlayerCanAttackTarget && !API.PlayerIsMoving && !ChannelingTranq;
        private bool NightFaeCheck => PlayerCovenantSettings == "Night Fae" && ConvokeAoE;
        private bool NecrolordCheck => PlayerCovenantSettings == "Necrolord" && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" || UseCovenant == "on AOE" && IsAOE) && NotChanneling && !API.PlayerCanAttackTarget && (!API.PlayerIsMoving || API.PlayerIsMoving) && !ChannelingCov && !ChannelingTranq;
        private bool NecrolordMOCheck => PlayerCovenantSettings == "Necrolord" && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" || UseCovenant == "on AOE" && IsAOE) && NotChanneling && !API.PlayerCanAttackMouseover && (!API.PlayerIsMoving || API.PlayerIsMoving) && !ChannelingCov && !ChannelingTranq;
        private bool VenthyrCheck => PlayerCovenantSettings == "Venthyr" && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" || UseCovenant == "on AOE" && IsAOE) && NotChanneling && !API.PlayerCanAttackTarget && (!API.PlayerIsMoving || API.PlayerIsMoving) && !ChannelingCov && !ChannelingTranq;
        private bool Forms => !API.PlayerHasBuff(BearForm) || !API.PlayerHasBuff(CatForm) || !API.PlayerHasBuff(MoonkinForm) || !API.PlayerHasBuff(Soulshape);
        private bool AutoForm => CombatRoutine.GetPropertyBool("AutoForm");
        public bool isMouseoverInCombat => CombatRoutine.GetPropertyBool("MouseoverInCombat");
        private bool IsAutoSwap => API.ToggleIsEnabled("Auto Target");
        private bool IsOOC => API.ToggleIsEnabled("OOC");
        private int FrenziedRegenerationLifePercent => numbList[CombatRoutine.GetPropertyInt(FrenziedRegeneration)];
        private int IronfurLifePercent => numbList[CombatRoutine.GetPropertyInt(Ironfur)];
        private int TankHealth => numbList[CombatRoutine.GetPropertyInt("Tank Health")];
        private int UnitHealth => numbList[CombatRoutine.GetPropertyInt("Other Members Health")];
        private int PlayerHP => numbList[CombatRoutine.GetPropertyInt("Player Health")];
        private int BarkskinLifePercent => numbList[CombatRoutine.GetPropertyInt(Barkskin)];
        private int BearFormLifePercent => numbList[CombatRoutine.GetPropertyInt(BearForm)];
        private int RenewalLifePercent => numbList[CombatRoutine.GetPropertyInt(Renewal)];
        private int RejLifePercent => numbList[CombatRoutine.GetPropertyInt(Rejuvenation)];
        private int RejGermLifePercent => numbList[CombatRoutine.GetPropertyInt(GerminationHoT)];
        private int RegrowthLifePercent => numbList[CombatRoutine.GetPropertyInt(Regrowth)];
        private int LifebloomLifePercent => numbList[CombatRoutine.GetPropertyInt(Lifebloom)];
        private int CWTankLifePercent => numbList[CombatRoutine.GetPropertyInt(CenarionWard)];
        private int CWPlayerLifePercent => numbList[CombatRoutine.GetPropertyInt(CenarionWardPlayer)];
        private int IronBarkLifePercent => numbList[CombatRoutine.GetPropertyInt(Ironbark)];
        private int NourishLifePercent => numbList[CombatRoutine.GetPropertyInt(Nourish)];
        private int SwiftmendLifePercent => numbList[CombatRoutine.GetPropertyInt(Swiftmend)];
        private int FloruishLifePercent => numbList[CombatRoutine.GetPropertyInt(Flourish)];
        private int OvergrowthLifePercent => numbList[CombatRoutine.GetPropertyInt(Overgrowth)];
        private int WGLifePercent => numbList[CombatRoutine.GetPropertyInt(WildGrowth)];
        private int TranqLifePercent => numbList[CombatRoutine.GetPropertyInt(Tranquility)];
        private int ToLLifePercent => numbList[CombatRoutine.GetPropertyInt(TreeofLife)];
        private int NSLifePercent => numbList[CombatRoutine.GetPropertyInt(Natureswiftness)];
        private int ConvLifePercent => numbList[CombatRoutine.GetPropertyInt(Convoke)];
        private int TrinketLifePercent => numbList[CombatRoutine.GetPropertyInt(Trinket)];
        private int ManaPercent => numbList[CombatRoutine.GetPropertyInt(Innervate)];
        private int PhialofSerenityLifePercent => numbList[CombatRoutine.GetPropertyInt(PhialofSerenity)];
        private int SpiritualHealingPotionLifePercent => numbList[CombatRoutine.GetPropertyInt(SpiritualHealingPotion)];
        private string UseCovenant => CDUsageWithAOE[CombatRoutine.GetPropertyInt("Use Covenant")];
        private bool AutoTravelForm => CombatRoutine.GetPropertyBool("AutoTravelForm");
        private int AoENumber => numbPartyList[CombatRoutine.GetPropertyInt(AoE)];
        private int AoERaidNumber => numbRaidList[CombatRoutine.GetPropertyInt(AoERaid)];
        private int AoEDPSHLifePercent => numbList[CombatRoutine.GetPropertyInt(AoEDPSH)];
        private int AoEDPSNumber => numbPartyList[CombatRoutine.GetPropertyInt(AoEDPS)];
        private int AoEDPSRaidNumber => numbRaidList[CombatRoutine.GetPropertyInt(AoEDPSRaid)];
        private int AoEDPSHRaidLifePercent => numbList[CombatRoutine.GetPropertyInt(AoEDPSHRaid)];
        private int FleshcraftPercentProc => numbList[CombatRoutine.GetPropertyInt(Fleshcraft)];
        private int PartySwapPercent => numbList[CombatRoutine.GetPropertyInt(PartySwap)];
        private int TargetChangePercent => numbList[CombatRoutine.GetPropertyInt(TargetChange)];
        private int SwapSpeedSetting => SwapSpeedList[CombatRoutine.GetPropertyInt(SwapSpeed)];
        private string UseTrinket1 => CDUsageWithAOE[CombatRoutine.GetPropertyInt("Trinket1")];
        private string UseTrinket2 => CDUsageWithAOE[CombatRoutine.GetPropertyInt("Trinket2")];
        private string UseHeart => CDUsage[CombatRoutine.GetPropertyInt(HeartoftheWild)];

        //private int AoERaidNumber => numbRaidList[CombatRoutine.GetPropertyInt(AoER)];
        private bool Quaking => (API.PlayerIsCasting(false) || API.PlayerIsChanneling) && API.PlayerDebuffRemainingTime(Quake) < 110 && PlayerHasDebuff(Quake);
        private bool SaveQuake => (PlayerHasDebuff(Quake) && API.PlayerDebuffRemainingTime(Quake) > 200 && QuakingHelper || !PlayerHasDebuff(Quake) || !QuakingHelper);
        private bool QuakingWG => (API.PlayerDebuffRemainingTime(Quake) > WGCastTime && PlayerHasDebuff(Quake) || !PlayerHasDebuff(Quake));
        private bool QuakingRegrowth => (API.PlayerDebuffRemainingTime(Quake) > RegrowthCastTime && PlayerHasDebuff(Quake) || !PlayerHasDebuff(Quake));
        private bool QuakingConvoke => (API.PlayerDebuffRemainingTime(Quake) > ConvokeCastTime && PlayerHasDebuff(Quake) || !PlayerHasDebuff(Quake));
        private bool QuakingTranq => (API.PlayerDebuffRemainingTime(Quake) > TranqCastTime && PlayerHasDebuff(Quake) || !PlayerHasDebuff(Quake));
        private bool QuakingNourish => (API.PlayerDebuffRemainingTime(Quake) > NourishCastTime && PlayerHasDebuff(Quake) || !PlayerHasDebuff(Quake));
        private bool QuakingWrath => (API.PlayerDebuffRemainingTime(Quake) > WrathCastTime && PlayerHasDebuff(Quake) || !PlayerHasDebuff(Quake));
        private bool QuakingStar => (API.PlayerDebuffRemainingTime(Quake) > StarfireCastTime && PlayerHasDebuff(Quake) || !PlayerHasDebuff(Quake));
        float WGCastTime => 150f / (1f + API.PlayerGetHaste);
        float RegrowthCastTime => 150f / (1f + API.PlayerGetHaste);
        float ConvokeCastTime => 400f / (1f + API.PlayerGetHaste);
        float TranqCastTime => 800f / (1f + API.PlayerGetHaste);
        float NourishCastTime => 200f / (1f + API.PlayerGetHaste);
        float WrathCastTime => 150f / (1f + API.PlayerGetHaste);
        float StarfireCastTime => 225f / (1f + API.PlayerGetHaste);
        //General
        bool IsTrinkets1 => (UseTrinket1 == "With Cooldowns" && IsCooldowns  || UseTrinket1 == "On Cooldown" && API.TargetHealthPercent <= TrinketLifePercent || UseTrinket1 == "on AOE" && TrinketAoE);
        bool IsTrinkets2 => (UseTrinket2 == "With Cooldowns" && IsCooldowns  || UseTrinket2 == "On Cooldown" && API.TargetHealthPercent <= TrinketLifePercent || UseTrinket2 == "on AOE" && TrinketAoE);
        private int Level => API.PlayerLevel;
        private bool InRange => API.TargetRange <= 40;
        private bool InMORange => API.MouseoverRange <= 40;
        private bool IsMelee => API.TargetRange < 6;
        // private bool NotCasting => !API.PlayerIsCasting;
        private bool NotChanneling => !API.PlayerIsChanneling;
        private bool IsMouseover => API.ToggleIsEnabled("Mouseover");
        private bool IsDispell => API.ToggleIsEnabled("Dispel");
        private bool IsDPS => API.ToggleIsEnabled("DPS Auto Target");
        private bool IsNpC => API.ToggleIsEnabled("NPC");
        private bool IsAutoForm => API.ToggleIsEnabled("Auto Form");

        public bool SootheList => API.TargetHasBuff("Enrage") || API.TargetHasBuff("Undying Rage") || API.TargetHasBuff("Raging") || API.TargetHasBuff("Unholy Frenzy") || API.TargetHasBuff("Renew") || API.TargetHasBuff("Additional Treads") || API.TargetHasBuff("Slime Coated") || API.TargetHasBuff("Stimulate Resistance") || API.TargetHasBuff("Unholy Fervor") || API.TargetHasBuff("Raging Tantrum") || API.TargetHasBuff("Loyal Beasts") || API.TargetHasBuff("Motivational Clubbing") || API.TargetHasBuff("Forsworn Doctrine") || API.TargetHasBuff("Seething Rage") || API.TargetHasBuff("Dark Shroud");
        public bool SootheMOList => API.MouseoverHasBuff("Enrage") || API.MouseoverHasBuff("Undying Rage") || API.MouseoverHasBuff("Raging") || API.MouseoverHasBuff("Unholy Frenzy") || API.MouseoverHasBuff("Renew") || API.MouseoverHasBuff("Additional Treads") || API.MouseoverHasBuff("Slime Coated") || API.MouseoverHasBuff("Stimulate Resistance") || API.MouseoverHasBuff("Unholy Fervor") || API.MouseoverHasBuff("Raging Tantrum") || API.MouseoverHasBuff("Loyal Beasts") || API.MouseoverHasBuff("Motivational Clubbing") || API.MouseoverHasBuff("Forsworn Doctrine") || API.MouseoverHasBuff("Seething Rage") || API.MouseoverHasBuff("Dark Shroud");
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
        private static bool UnitHasDebuff(string buff, string unit)
        {
            return API.UnitHasDebuff(buff, unit, false, true);
        }
        private static bool PlayerHasDebuff(string buff)
        {
            return API.PlayerHasDebuff(buff, false, false);
        }
        private static bool TargetHasBuff(string buff)
        {
            return API.TargetHasBuff(buff, true, true);
        }
        private static bool MouseoverHasBuff(string buff)
        {
           return API.MouseoverHasBuff(buff, true, false);
        }
        private static bool TargetHasDebuff(string buff)
        {
            return API.TargetHasDebuff(buff, false, true);
        }
        private static bool MouseoverHasDebuff(string buff)
        {
            return API.MouseoverHasDebuff(buff, false, false);
        }
        //  public bool isInterrupt => CombatRoutine.GetPropertyBool("KICK") && API.TargetCanInterrupted && API.TargetIsCasting && (API.TargetIsChanneling ? API.TargetElapsedCastTime >= interruptDelay : API.TargetCurrentCastTimeRemaining <= interruptDelay);
        //  public int interruptDelay => random.Next((int)(CombatRoutine.GetPropertyInt("KICKTime") * 0.9), (int)(CombatRoutine.GetPropertyInt("KICKTime") * 1.1));
        public override void Initialize()
        {
            CombatRoutine.Name = "Resto Druid by Ryu";
            API.WriteLog("Welcome to Resto Druid v1.6 by Ryu");
            API.WriteLog("Mouseover Support is added for healing, dispel and Ranged DPS (Moonkin). Please create /cast [@mouseover] xx whereas xx is your Dispell and assign it the bind with MO on it in keybinds.");
            API.WriteLog("For all ground spells, either use @Cursor or when it is time to place it, the Bot will pause until you've placed it. If you'd perfer to use your own logic for them, please place them on ignore in the spellbook.");
            API.WriteLog("For using Dark Titan's Lesson, please BIND LifebloomL in your bindings to YOUR Lifebloom and select in the Legendary select. It changes the ID of the spell and that will mess the rotation if these things aren't done.");
            API.WriteLog("For the Quaking helper you just need to create an ingame macro with /stopcasting and bind it under the Macros Tab in Elk :-)");
            API.WriteLog("Please us a /cast [target=player] macro for Innervate to work properly or it will cast on your current target");
            API.WriteLog("If you wish to use Auto Target, please set your WoW keybinds in the keybinds => Targeting for Self, Party, and Assist Target and then match them to the Macro's's in the spell book. Enable it the Toggles. You must at least have a target for it to swap, friendly or enemy. UNDER TESTING : It can swap back to an enemy, but YOU WILL NEED TO ASSIGN YOUR ASSIST TARGET KEY IT WILL NOT WORK IF YOU DONT DO THIS. If you DO NOT want it to do target enemy swapping, please IGNORE Assist Macro in the Spellbook. This works for both raid and party, however, you must set up the binds. Please watch video in the Discord");
            API.WriteLog("The settings in the Targeting Section have been tested to work well. Change them at your risk and ONLY if you understand them.");
            API.WriteLog("IF YOU USE THE NPC TOGGLE, IT WILL CHANGE THE ROTATION THE NPC HEALING LOGIC (For Shade and Sun King) IT WILL IGNORE ALL OTHER THINGS EXPECT COOLDOWNS, PLEASE TURN IT OFF ONCE YOU HAVE FINISHED HEALING THE NPC - TARGETING ONLY -- IF you have Convoke, it WILL USE IT ONLY IF YOU TURN ON COOLDOWNS DURING THE NPC HEALING BOTH TOGGLES MUST BE ON FOR IT TO CAST IT, unless the AOE Logic is met, then it wil cast it regardless");
            API.WriteLog("Special Thanks to Ajax and Goose/Zero for testing and Smartie for the DPS rotations.");

            //Buff
            CombatRoutine.AddBuff(Rejuvenation, 774);
            CombatRoutine.AddBuff(Regrowth, 8936);
            CombatRoutine.AddBuff(Lifebloom, 33763);
            CombatRoutine.AddBuff(WildGrowth, 48438);
            CombatRoutine.AddBuff(GerminationHoT, 155777);
            CombatRoutine.AddBuff(Clear, 16870);
            CombatRoutine.AddBuff(BearForm, 5487);
            CombatRoutine.AddBuff(Catform, 768);
            CombatRoutine.AddBuff(MoonkinForm, 197625);
            CombatRoutine.AddBuff(TravelForm, 783);
            CombatRoutine.AddBuff(Soulshape, 310143);
            CombatRoutine.AddBuff(FrenziedRegeneration, 22842);
            CombatRoutine.AddBuff(HeartoftheWild, 108291);
            CombatRoutine.AddBuff(Tranquility, 157982);
            CombatRoutine.AddBuff(SouloftheForest, 114108);
            CombatRoutine.AddBuff(LifebloomL, 188550);
            CombatRoutine.AddBuff(EclipseLunar, 48518);
            CombatRoutine.AddBuff(EclipseSolar, 48517);
            CombatRoutine.AddBuff(Quake, 240447);
            CombatRoutine.AddBuff("Gluttonous Miasma", 329298);
            CombatRoutine.AddBuff(TreeofLife, 33891);
            CombatRoutine.AddBuff(CenarionWardHoT, 102352);
            CombatRoutine.AddBuff(Innervate, 29166);

            //Debuff
            CombatRoutine.AddDebuff(Sunfire, 164815);
            CombatRoutine.AddDebuff(Moonfire, 164812);
            CombatRoutine.AddDebuff(Thrashbear, 192090);
            CombatRoutine.AddDebuff(Thrashkitty, 106830);
            CombatRoutine.AddDebuff(Rip, 1079);
            CombatRoutine.AddDebuff(Rake, 155722);
            CombatRoutine.AddDebuff(Quake, 240447);
            //Soothe
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

            //Dispels
            CombatRoutine.AddDebuff("Chilled", 328664);
            CombatRoutine.AddDebuff("Frozen Binds", 320788);
            CombatRoutine.AddDebuff("Clinging Darkness", 323347);
            CombatRoutine.AddDebuff("Rasping Scream", 324293);
            CombatRoutine.AddDebuff("Heaving Retch", 320596);
            CombatRoutine.AddDebuff("Goresplatter", 338353);
            CombatRoutine.AddDebuff("Slime Injection", 329110);
            CombatRoutine.AddDebuff("Gripping Infection", 328180);
            CombatRoutine.AddDebuff("Cytotoxic Slash", 325552);
            CombatRoutine.AddDebuff("Venompiercer", 328395);
            CombatRoutine.AddDebuff("Wretched Phlegm", 334926);
            CombatRoutine.AddDebuff("Repulsive Visage", 328756);
            CombatRoutine.AddDebuff("Soul Split", 322557);
            CombatRoutine.AddDebuff("Anima Injection", 325224);
            CombatRoutine.AddDebuff("Bewildering Pollen", 321968);
            CombatRoutine.AddDebuff("Bramblethorn Entanglement", 324859);
            CombatRoutine.AddDebuff("Dying Breath", 322968);
            CombatRoutine.AddDebuff("Debilitating Poison", 326092);
            CombatRoutine.AddDebuff("Sinlight Visions", 339237);
            CombatRoutine.AddDebuff("Siphon Life", 325701);
            CombatRoutine.AddDebuff("Turn to Stone", 326607);
            CombatRoutine.AddDebuff("Stony Veins", 326632);
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
            CombatRoutine.AddDebuff("Dark Lance", 327481);
            CombatRoutine.AddDebuff("Insidious Venom", 323636);
            CombatRoutine.AddDebuff("Charged Anima", 338731);
            CombatRoutine.AddDebuff("Lost Confidence", 322818);
            CombatRoutine.AddDebuff("Burden of Knowledge", 317963);
            CombatRoutine.AddDebuff("Internal Strife", 327648);
            CombatRoutine.AddDebuff("Forced Confession", 328331);
            CombatRoutine.AddDebuff("Insidious Venom 2", 317661);
            CombatRoutine.AddDebuff("Soul Corruption", 333708);
            CombatRoutine.AddDebuff("Spectral Reach", 319669);
            CombatRoutine.AddDebuff("Death Grasp", 323831);
            CombatRoutine.AddDebuff("Shadow Vulnerability", 330725);
            CombatRoutine.AddDebuff("Curse of Desolation", 333299);
            CombatRoutine.AddDebuff("Gluttonous Miasma", 329298);
            CombatRoutine.AddDebuff("Burst", 240443);


            //Spell
            CombatRoutine.AddSpell(Rejuvenation, 774);
            CombatRoutine.AddSpell(Regrowth, 8936);
            CombatRoutine.AddSpell(Lifebloom, 33763);
            CombatRoutine.AddSpell(WildGrowth, 48438);
            CombatRoutine.AddSpell(Swiftmend, 18562);
            CombatRoutine.AddSpell(Tranquility, 740);
            CombatRoutine.AddSpell(Innervate, 29166);
            CombatRoutine.AddSpell(Ironbark, 102342);
            CombatRoutine.AddSpell(Natureswiftness, 132158);
            CombatRoutine.AddSpell(Barkskin, 22812);
            CombatRoutine.AddSpell(NaturesCure, 88423);
            CombatRoutine.AddSpell(EntanglingRoots, 339);
            CombatRoutine.AddSpell(Soothe, 2908);
            CombatRoutine.AddSpell(AdaptiveSwarm, 325727);
            CombatRoutine.AddSpell(Fleshcraft, 324631);
            CombatRoutine.AddSpell(Convoke, 323764);
            CombatRoutine.AddSpell(RavenousFrenzy, 323546);
            CombatRoutine.AddSpell(Nourish, 50464);
            CombatRoutine.AddSpell(CenarionWard, 102351);
            CombatRoutine.AddSpell(TreeofLife, 33891);
            CombatRoutine.AddSpell(Overgrowth, 203651);
            CombatRoutine.AddSpell(Flourish, 197721);
            CombatRoutine.AddSpell(Renewal, 108238);
            CombatRoutine.AddSpell(TravelForm, 783);
            CombatRoutine.AddSpell(HeartoftheWild, 319454);
            CombatRoutine.AddSpell(LifebloomL, 188550);
            CombatRoutine.AddSpell(Efflor, 145205);

            //OWL
            CombatRoutine.AddSpell(MoonkinForm, 197625);
            CombatRoutine.AddSpell(Moonfire, 8921);
            CombatRoutine.AddSpell(Sunfire, 93402);
            CombatRoutine.AddSpell(Wrath, 5176);
            CombatRoutine.AddSpell(Starfire, 197628);
            CombatRoutine.AddSpell(Starsurge, 197626);

            //Kitty
            CombatRoutine.AddSpell(Catform, 768);
            CombatRoutine.AddSpell(Rip, 1079);
            CombatRoutine.AddSpell(Rake, 1822);
            CombatRoutine.AddSpell(Shred, 5221);
            CombatRoutine.AddSpell(FerociousBite, 22568);
            CombatRoutine.AddSpell(Thrashkitty, 106830);
            CombatRoutine.AddSpell(Swipekitty, 106785);

            //Bear
            CombatRoutine.AddSpell(Bearform, 5487);
            CombatRoutine.AddSpell(FrenziedRegeneration, 22842);
            CombatRoutine.AddSpell(Ironfur, 192081);
            CombatRoutine.AddSpell(Thrashbear, 77758);
            CombatRoutine.AddSpell(Mangle, 33917);
            CombatRoutine.AddSpell(Swipebear, 213771);

            //Toggle
            CombatRoutine.AddToggle("Auto Target");
            CombatRoutine.AddToggle("DPS Auto Target");
            CombatRoutine.AddToggle("Auto Form");
            CombatRoutine.AddToggle("NPC");
            CombatRoutine.AddToggle("OOC");
            CombatRoutine.AddToggle("Mouseover");
            CombatRoutine.AddToggle("Dispel");

            //Item
            CombatRoutine.AddItem(PhialofSerenity, 177278);
            CombatRoutine.AddItem(SpiritualHealingPotion, 171267);

            //Macro
            CombatRoutine.AddMacro(NaturesCure + "MO");
            CombatRoutine.AddMacro(Soothe + MO);
            CombatRoutine.AddMacro(Rejuvenation + MO);
            CombatRoutine.AddMacro(Regrowth + MO);
            CombatRoutine.AddMacro(Lifebloom + MO);
            CombatRoutine.AddMacro(WildGrowth + MO);
            CombatRoutine.AddMacro(Swiftmend + MO);
            CombatRoutine.AddMacro(Ironbark + MO);
            CombatRoutine.AddMacro(Nourish + MO);
            CombatRoutine.AddMacro(CenarionWard + MO);
            CombatRoutine.AddMacro(Overgrowth + MO);
            CombatRoutine.AddMacro(LifebloomL + MO);
            CombatRoutine.AddMacro(Moonfire + MO);
            CombatRoutine.AddMacro(Sunfire + MO);
            CombatRoutine.AddMacro(Wrath + MO);
            CombatRoutine.AddMacro(Starfire + MO);
            CombatRoutine.AddMacro(Starsurge + MO);
            CombatRoutine.AddMacro(AdaptiveSwarm + MO);
            CombatRoutine.AddMacro(Trinket1);
            CombatRoutine.AddMacro(Trinket2);
            CombatRoutine.AddMacro("Stopcast", "F10");
            CombatRoutine.AddMacro("Assist");
            CombatRoutine.AddMacro(Player);
            CombatRoutine.AddMacro(Party1);
            CombatRoutine.AddMacro(Party2);
            CombatRoutine.AddMacro(Party3);
            CombatRoutine.AddMacro(Party4);
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
            CombatRoutine.AddProp("AutoTravelForm", "AutoTravelForm", false, "Will auto switch to Travel Form Out of Fight and outside", "Generic");
           // CombatRoutine.AddProp("AutoForm", "AutoForm", false, "Will auto switch forms", "Generic");
            CombatRoutine.AddProp("QuakingHelper", "Quaking Helper", false, "Will cancel casts on Quaking", "Generic");

            CombatRoutine.AddProp(Barkskin, Barkskin + " Life Percent", numbList, "Life percent at which" + Barkskin + "is used, set to 0 to disable", "Defense", 25);
            CombatRoutine.AddProp(Renewal, Renewal + " Life Percent", numbList, "Life percent at which" + Renewal + "is used, if talented, set to 0 to disable", "Defense", 45);
            CombatRoutine.AddProp(FrenziedRegeneration, FrenziedRegeneration + " Life Percent", numbList, "Life percent at which" + FrenziedRegeneration + "is used, set to 0 to disable", "Defense", 60);
            CombatRoutine.AddProp(Ironfur, Ironfur + " Life Percent", numbList, "Life percent at which" + Ironfur + "is used, set to 0 to disable", "Defense", 90);
            CombatRoutine.AddProp(BearForm, BearForm + " Life Percent", numbList, "Life percent at which rota will go into" + BearForm + "set to 0 to disable", "Defense", 10);
            CombatRoutine.AddProp(Fleshcraft, "Fleshcraft", numbList, "Life percent at which " + Fleshcraft + " is used, set to 0 to disable set 100 to use it everytime", "Defense", 8);
            CombatRoutine.AddProp(PhialofSerenity, PhialofSerenity + " Life Percent", numbList, " Life percent at which" + PhialofSerenity + " is used, set to 0 to disable", "Defense", 40);
            CombatRoutine.AddProp(SpiritualHealingPotion, SpiritualHealingPotion + " Life Percent", numbList, " Life percent at which" + SpiritualHealingPotion + " is used, set to 0 to disable", "Defense", 40);

            CombatRoutine.AddProp("Use Covenant", "Use " + "Covenant Ability", CDUsageWithAOE, "Use " + "Covenant" + "On Cooldown, with Cooldowns, On AOE, Not Used", "Cooldowns", 1);
            CombatRoutine.AddProp(HeartoftheWild, "Use " + HeartoftheWild, CDUsage, "Use " + HeartoftheWild + "On Cooldown, with Cooldowns, Not Used", "Cooldowns", 1);

            CombatRoutine.AddProp("Tank Health", "Tank Health", numbList, "Life percent at which " + "Tank Health" + "needs to be at to target during DPS Targeting", "Targeting", 75);
            CombatRoutine.AddProp("Other Members Health", "Other Members Health", numbList, "Life percent at which " + "Other Members Health" + "needs to be at to targeted during DPS Targeting", "Targeting", 35);
            CombatRoutine.AddProp("Player Health", "Player Health", numbList, "Life percent at which " + "Player Health" + "needs to be at to targeted above all else", "Targeting", 35);
            CombatRoutine.AddProp(AoEDPS, "Number of units needed to be above DPS Health Percent to DPS in party ", numbPartyList, " Units above for DPS ", "Targeting", 2);
            CombatRoutine.AddProp(AoEDPSRaid, "Number of units needed to be above DPS Health Percent to DPS in Raid ", numbRaidList, " Units above for DPS ", "Targeting", 4);
            CombatRoutine.AddProp(AoEDPSH, "Life Percent for units to be above for DPS and below to return back to Healing", numbList, "Health percent at which DPS in party" + "is used,", "Targeting", 75);
            CombatRoutine.AddProp(AoEDPSHRaid, "Life Percent for units to be above for DPS and below to return back to Healing in raid", numbList, "Health percent at which DPS" + "is used,", "Targeting", 75);


            CombatRoutine.AddProp("Use Lifebloom", "Select your Lifebloom Target Role", LifeTarget, "Select Your Lifebloom Target Role", "Lifebloom", 0);

            CombatRoutine.AddProp("Legendary", "Select your Legendary", LegendaryList, "Select Your Legendary", "Legendary");

            CombatRoutine.AddProp("Fight Selection", "Select your NPC Fight", FightSelection, "Select Your NPC Fight", "NPC Rotation");

            CombatRoutine.AddProp("In Torghast", "Torghast Helper", false, "Are you in Torghast?", "Torghast");
            CombatRoutine.AddProp("Use Lifebloom in Torghast", "Use Lifebloom in Torghast", PlayerTargetArray, "Will set Lifebloom target in Torghast", "Torghast");
            CombatRoutine.AddProp("Tank in Torghast", "Tank in Torghast", PlayerTargetArray, "Will set Tank target in Torghast", "Torghast");


            CombatRoutine.AddProp(Rejuvenation, Rejuvenation + " Life Percent", numbList, "Life percent at which " + Rejuvenation + " is used, set to 0 to disable", "Healing", 95);
            CombatRoutine.AddProp(GerminationHoT, GerminationHoT + " Life Percent", numbList, "Life percent at which " + GerminationHoT + " is used, set to 0 to disable", "Healing", 85);
            CombatRoutine.AddProp(Regrowth, Regrowth + " Life Percent", numbList, "Life percent at which " + Regrowth + " is used, set to 0 to disable", "Healing", 85);
            CombatRoutine.AddProp(Lifebloom, Lifebloom + " Life Percent", numbList, "Life percent at which " + Lifebloom + " is used, set to 0 to disable", "Healing", 100);
            CombatRoutine.AddProp(CenarionWard, CenarionWard + " Life Percent", numbList, "Life percent at which " + CenarionWard + " is used for the tank, if talented, set to 0 to disable", "Healing", 95);
            CombatRoutine.AddProp(CenarionWardPlayer, CenarionWard + " Life Percent", numbList, "Life percent at which " + CenarionWard + " is used for other players, if talented, set to 0 to disable", "Healing", 95);
            CombatRoutine.AddProp(Ironbark, Ironbark + " Life Percent", numbList, "Life percent at which " + Ironbark + " is used, set to 0 to disable", "Healing", 25);
            CombatRoutine.AddProp(Nourish, Nourish + " Life Percent", numbList, "Life percent at which " + Nourish + " is used, set to 0 to disable", "Healing", 55);
            CombatRoutine.AddProp(Swiftmend, Swiftmend + " Life Percent", numbList, "Life percent at which " + Swiftmend + " is used, set to 0 to disable", "Healing", 65);
            CombatRoutine.AddProp(Natureswiftness, Natureswiftness + " Life Percent", numbList, "Life percent at which " + Natureswiftness + " is used, set to 0 to disable", "Healing", 45);
            CombatRoutine.AddProp(Overgrowth, Overgrowth + " Life Percent", numbList, "Life percent at which " + Overgrowth + " is used, set to 0 to disable", "Healing", 75);
            CombatRoutine.AddProp(Innervate, Innervate + " Mana Percent", numbList, "Mana percent at which " + Innervate + " is used, set to 0 to disable", "Healing", 85);
            CombatRoutine.AddProp(Flourish, Flourish + " Life Percent", numbList, "Life percent at which " + Flourish + " is used when AoE Number of members are at and have the HoTs needed, set to 0 to disable", "Healing", 45);
            CombatRoutine.AddProp(Convoke, Convoke + " Life Percent", numbList, "Life percent at which " + Convoke + " is used when AoE Number of members are at and have the HoTs needed, set to 0 to disable", "Healing", 55);
            CombatRoutine.AddProp(WildGrowth, WildGrowth + " Life Percent", numbList, "Life percent at which " + WildGrowth + " is used when AoE Number of members are at life percent, set to 0 to disable", "Healing", 65);
            CombatRoutine.AddProp(Tranquility, Tranquility + " Life Percent", numbList, "Life percent at which " + Tranquility + " is used when AoE Number of members are at life percent, set to 0 to disable", "Healing", 20);
            CombatRoutine.AddProp(TreeofLife, TreeofLife + " Life Percent", numbList, "Life percent at which " + TreeofLife + " is used when AoE Number of members are at life percent, set to 0 to disable", "Healing", 45);
            CombatRoutine.AddProp(AoE, "Number of units for AoE Healing ", numbPartyList, " Units for AoE Healing", "Healing", 3);
            CombatRoutine.AddProp(AoERaid, "Number of units for AoE Healing in raid ", numbRaidList, " Units for AoE Healing in raid", "Healing", 7);
            CombatRoutine.AddProp(Trinket, Trinket + " Life Percent", numbList, "Life percent at which " + "Trinkets" + " should be used, set to 0 to disable", "Healing", 55);

            CombatRoutine.AddProp("Trinket1", "Trinket1 usage", CDUsageWithAOE, "When should trinket 1 be used", "Trinket", 0);
            CombatRoutine.AddProp("Trinket2", "Trinket2 usage", CDUsageWithAOE, "When should trinket 2 be used", "Trinket", 0);

        }

        public override void Pulse()
        {
            if (!Lunarwatch.IsRunning && API.PlayerHasBuff(EclipseLunar) && !API.PlayerHasBuff(EclipseSolar))
            {
                Solarwatch.Stop();
                Solarwatch.Reset();
                Lunarwatch.Restart();
                API.WriteLog("Starting Lunarwatch.");
            }
            if (!Solarwatch.IsRunning && API.PlayerHasBuff(EclipseSolar) && !API.PlayerHasBuff(EclipseLunar))
            {
                Lunarwatch.Stop();
                Lunarwatch.Reset();
                Solarwatch.Restart();
                API.WriteLog("Starting Solarwatch.");
            }
            for (int i = 0; i < units.Length; i++)
            {
                if (IsDispell && API.PlayerIsInGroup && !API.PlayerIsInRaid && UnitHasDispellAble("Frozen Binds", units[i]))
                {
                    DispelWatch.Restart();
                }
            }
            if (API.PlayerCurrentCastTimeRemaining > 40 && QuakingHelper && Quaking)
            {
                API.CastSpell("Stopcast");
                API.WriteLog("Debuff Time Remaining for Quake : " + API.PlayerDebuffRemainingTime(Quake));
                return;
            }
            if (API.PlayerHasBuff(MoonkinForm) && IsAutoForm && BalanceAffinity && (API.PlayerIsInGroup && !API.PlayerIsInRaid && UnitBelowHealthPercentParty(AoEDPSHLifePercent) >= AoEDPSNumber || API.PlayerIsInRaid && UnitBelowHealthPercentRaid(AoEDPSHRaidLifePercent) >= AoEDPSRaidNumber) || API.PlayerHasBuff(MoonkinForm) && BalanceAffinity && !API.PlayerCanAttackTarget)
            {
                API.CastSpell(MoonkinForm);
                return;
            }
            if (!ChannelingCov && !ChannelingTranq && NotChanneling && !API.PlayerIsMounted && !API.PlayerSpellonCursor && !API.PlayerHasBuff(TravelForm) && !API.PlayerHasBuff(BearForm) && !API.PlayerHasBuff(CatForm) && !API.PlayerHasBuff(Soulshape) && (IsOOC || API.PlayerIsInCombat) && (!TargetHasDebuff("Gluttonous Miasma") || IsMouseover && !MouseoverHasDebuff("Gluttonous Miasma")))
            {

                #region Dispell
                if (IsDispell)
                {
                    if (API.CanCast(NaturesCure) && !ChannelingTranq && !ChannelingCov && NotChanneling)
                    {
                        for (int i = 0; i < DispellList.Length; i++)
                        {
                            if (TargetHasDispellAble(DispellList[i]) && (!TargetHasDispellAble("Frozen Binds") || TargetHasDispellAble("Frozen Binds") && DispelWatch.ElapsedMilliseconds >= 2000))
                            {
                                API.CastSpell(NaturesCure);
                                return;
                            }
                        }
                    }
                    if (API.CanCast(NaturesCure) && IsMouseover && !ChannelingTranq && !ChannelingCov && NotChanneling)
                    {
                        for (int i = 0; i < DispellList.Length; i++)
                        {
                            if (MouseouverHasDispellAble(DispellList[i]) && (!MouseouverHasDispellAble("Frozen Binds") || MouseouverHasDispellAble("Frozen Binds") && DispelWatch.ElapsedMilliseconds >= 2000))
                            {
                                API.CastSpell(NaturesCure + "MO");
                                return;
                            }
                        }
                    }
                }
                #endregion
                if (API.CanCast(TreeofLife) && TreeofLifeTalent && InRange && ToLAoE && !API.PlayerHasBuff(TreeofLife))
                {
                    API.CastSpell(TreeofLife); ;
                    return;
                }
                if (IsNpC && FightNPC == "Sun King" && InRange)
                {
                    if (API.CanCast(Innervate) && InRange && API.PlayerMana < 90)
                    {
                        API.CastSpell(Innervate);
                        return;
                    }
                    if (OvergrowthTalent && API.CanCast(Overgrowth) && (!TargetHasBuff(Lifebloom) || !TargetHasBuff(WildGrowth) || !TargetHasBuff(Rejuvenation) || !TargetHasBuff(Regrowth)) && !ChannelingCov)
                    {
                        API.CastSpell(Overgrowth);
                        return;
                    }
                    if (API.CanCast(TreeofLife) && TreeofLifeTalent && !API.PlayerHasBuff(TreeofLife) && !ChannelingCov)
                    {
                        API.CastSpell(TreeofLife);
                        return;
                    }
                    if (API.CanCast(Lifebloom) && !TargetHasBuff(Lifebloom) && UseLeg != "The Dark Titan's Lesson" && !ChannelingCov)
                    {
                        API.CastSpell(Lifebloom);
                        return;
                    }
                    if (API.CanCast(LifebloomL) && !TargetHasBuff(LifebloomL) && UseLeg == "The Dark Titan's Lesson" && !ChannelingCov)
                    {
                        API.CastSpell(LifebloomL);
                        return;
                    }
                    if (API.CanCast(Rejuvenation) && !TargetHasBuff(Rejuvenation) && !ChannelingCov)
                    {
                        API.CastSpell(Rejuvenation);
                        return;
                    }
                    if (API.CanCast(Regrowth) && !TargetHasBuff(Regrowth) && !ChannelingCov)
                    {
                        API.CastSpell(Regrowth);
                        return;
                    }
                    if (GerminationTalent && API.CanCast(Rejuvenation) && !TargetHasBuff(GerminationHoT) && !ChannelingCov)
                    {
                        API.CastSpell(Rejuvenation);
                        return;
                    }
                    if (API.CanCast(WildGrowth) && !ChannelingCov && !TargetHasBuff(WildGrowth))
                    {
                        API.CastSpell(WildGrowth);
                        return;
                    }
                    if (API.CanCast(Ironbark) && !ChannelingCov)
                    {
                        API.CastSpell(Ironbark);
                        return;
                    }
                    if (API.CanCast(Flourish) && FlourishTalent && TargetHasBuff(Rejuvenation) && TargetHasBuff(Regrowth) && TargetHasBuff(WildGrowth) && !ChannelingCov)
                    {
                        API.CastSpell(Flourish);
                        return;
                    }
                    if (API.CanCast(Convoke) && PlayerCovenantSettings == "Night Fae")
                    {
                        API.CastSpell(Convoke);
                        return;
                    }
                    if (API.CanCast(Swiftmend) && UseLeg == Verdant && API.SpellCharges(Swiftmend) > 0 && (TargetHasBuff(Rejuvenation) || TargetHasBuff(Regrowth)) && !ChannelingCov)
                    {
                        API.CastSpell(Swiftmend);
                        return;
                    }
                    if (API.CanCast(Regrowth) && (!NourishTalent || !TargetHasBuff(Regrowth) && NourishTalent) && !ChannelingCov)
                    {
                        API.CastSpell(Regrowth);
                        return;
                    }
                    if (NourishTalent && API.CanCast(Nourish) && !ChannelingCov)
                    {
                        API.CastSpell(Nourish);
                        return;
                    }
                }
                if (IsNpC && FightNPC == "Shade on Barghast" && InRange)
                {
                    if (API.CanCast(Innervate) && InnervateCheck && InRange)
                    {
                        API.CastSpell(Innervate);
                        return;
                    }
                    if (OvergrowthTalent && API.CanCast(Overgrowth) && (!TargetHasBuff(Lifebloom) || !TargetHasBuff(WildGrowth) || !TargetHasBuff(Rejuvenation) || !TargetHasBuff(Regrowth)) && !ChannelingCov)
                    {
                        API.CastSpell(Overgrowth);
                        return;
                    }
                    if (API.CanCast(Lifebloom) && !TargetHasBuff(Lifebloom) && UseLeg != "The Dark Titan's Lesson" && !ChannelingCov)
                    {
                        API.CastSpell(Lifebloom);
                        return;
                    }
                    if (API.CanCast(LifebloomL) && !TargetHasBuff(LifebloomL) && UseLeg == "The Dark Titan's Lesson" && !ChannelingCov)
                    {
                        API.CastSpell(LifebloomL);
                        return;
                    }
                    if (API.CanCast(Rejuvenation) && !TargetHasBuff(Rejuvenation) && !ChannelingCov)
                    {
                        API.CastSpell(Rejuvenation);
                        return;
                    }
                    if (GerminationTalent && API.CanCast(Rejuvenation) && !TargetHasBuff(GerminationHoT) && !ChannelingCov)
                    {
                        API.CastSpell(Rejuvenation);
                        return;
                    }
                    if (API.CanCast(Swiftmend) && API.SpellCharges(Swiftmend) > 0 && (TargetHasBuff(Rejuvenation) || TargetHasBuff(Regrowth)) && !ChannelingCov)
                    {
                        API.CastSpell(Swiftmend);
                        return;
                    }
                    if (API.CanCast(Convoke) && PlayerCovenantSettings == "Night Fae" && IsCooldowns)
                    {
                        API.CastSpell(Convoke);
                        return;
                    }
                    if (API.CanCast(Regrowth) && (!NourishTalent || !TargetHasBuff(Regrowth) && NourishTalent) && !ChannelingCov)
                    {
                        API.CastSpell(Regrowth);
                        return;
                    }
                    if (NourishTalent && API.CanCast(Nourish) && !ChannelingCov)
                    {
                        API.CastSpell(Nourish);
                        return;
                    }

                }
                if (API.CanCast(Soothe) && InRange && (SootheList))
                {
                    API.CastSpell(Soothe);
                    return;
                }
                if (API.CanCast(Soothe) && InMORange && IsMouseover && (SootheMOList))
                {
                    API.CastSpell(Soothe + MO);
                    return;
                }
                if (API.CanCast(Efflor) && API.PlayerIsInCombat && API.PlayerTotemPetDuration == 0 && !ChannelingCov && !API.PlayerHasBuff(MoonkinForm))
                {
                    API.CastSpell(Efflor);
                    return;
                }

                if (API.CanCast(Convoke) && NightFaeCheck && InRange && (!QuakingHelper || QuakingConvoke && QuakingHelper))
                {
                    API.CastSpell(Convoke);
                    return;
                }
                if (API.CanCast(AdaptiveSwarm) && NecrolordCheck && InRange)
                {
                    API.CastSpell(AdaptiveSwarm);
                    return;
                }
                if (API.CanCast(AdaptiveSwarm) && NecrolordMOCheck && InMORange && IsMouseover)
                {
                    API.CastSpell(AdaptiveSwarm + MO);
                    return;
                }
                if (API.CanCast(RavenousFrenzy) && VenthyrCheck && InRange)
                {
                    API.CastSpell(RavenousFrenzy);
                    return;
                }
                if (API.CanCast(Innervate) && InnervateCheck && InRange && !ChannelingCov)
                {
                    API.CastSpell(Innervate);
                    return;
                }
                if (API.CanCast(Overgrowth) && OvergrowthCheck && InRange)
                {
                    API.CastSpell(Overgrowth);
                    return;
                }
                if (API.CanCast(Overgrowth) && OvergrowthMOCheck && InMORange && IsMouseover)
                {
                    API.CastSpell(Overgrowth + MO);
                    return;
                }
                if (API.CanCast(Natureswiftness) && NatureSwiftCheck && InRange)
                {
                    API.CastSpell(Natureswiftness);
                    return;
                }
                if (API.CanCast(Ironbark) && InRange && IBCheck)
                {
                    API.CastSpell(Ironbark);
                    return;
                }
                if (API.CanCast(Ironbark) && InMORange && IBMOCheck && IsMouseover)
                {
                    API.CastSpell(Ironbark + MO);
                    return;
                }
                if (API.CanCast(Lifebloom) && InRange && LifeBloomCheck)
                {
                    API.CastSpell(Lifebloom);
                    return;
                }
                if (API.CanCast(Lifebloom) && InMORange && LifeBloomMOCheck && IsMouseover)
                {
                    API.CastSpell(Lifebloom + MO);
                    return;
                }
                if (API.CanCast(LifebloomL) && InRange && LifeBloomLegCheck)
                {
                    API.CastSpell(LifebloomL);
                    return;
                }
                if (API.CanCast(LifebloomL) && InMORange && LifeBloomLegMOCheck && IsMouseover)
                {
                    API.CastSpell(LifebloomL + MO);
                    return;
                }
                if (API.CanCast(WildGrowth) && InRange && WGAoE || SouloftheForestTalent && API.CanCast(WildGrowth) && API.PlayerHasBuff(SouloftheForest) && UnitBelowHealthPercent(65) >= 3 && InRange && (!QuakingHelper || QuakingWG && QuakingHelper))
                {
                    API.CastSpell(WildGrowth);
                    return;
                }
                if (API.CanCast(WildGrowth) && InMORange && IsMouseover && WGAoE && (!QuakingHelper || QuakingWG && QuakingHelper) || SouloftheForestTalent && API.CanCast(WildGrowth) && API.PlayerHasBuff(SouloftheForest) && UnitBelowHealthPercent(65) >= 3 && InMORange && IsMouseover && (!QuakingHelper || QuakingWG && QuakingHelper))
                {
                    API.CastSpell(WildGrowth + MO);
                    return;
                }
                if (API.CanCast(Tranquility) && InRange && TranqAoE && (!QuakingHelper || QuakingTranq && QuakingHelper))
                {
                    API.CastSpell(Tranquility);
                    return;
                }
                if (API.CanCast(Flourish) && InRange && FloruishCheck)
                {
                    API.CastSpell(Flourish);
                    return;
                }
                if (API.CanCast(CenarionWard) && InRange && CWCheck)
                {
                    API.CastSpell(CenarionWard);
                    return;
                }
                if (API.CanCast(CenarionWard) && InMORange && CWMOCheck && IsMouseover)
                {
                    API.CastSpell(CenarionWard + MO);
                    return;
                }
                if (API.CanCast(Swiftmend) && InRange && SwiftCheck)
                {
                    API.CastSpell(Swiftmend);
                    return;
                }
                if (API.CanCast(Swiftmend) && InMORange && SwiftMOCheck && IsMouseover)
                {
                    API.CastSpell(Swiftmend + MO);
                    return;
                }
                if (API.CanCast(Rejuvenation) && InRange && RejCheck)
                {
                    API.CastSpell(Rejuvenation);
                    return;
                }
                if (API.CanCast(Rejuvenation) && InMORange && RejMOCheck && IsMouseover)
                {
                    API.CastSpell(Rejuvenation + MO);
                    return;
                }
                if (API.CanCast(Rejuvenation) && InRange && RejGermCheck)
                {
                    API.CastSpell(Rejuvenation);
                    return;
                }
                if (API.CanCast(Rejuvenation) && InMORange && RejGermMOCheck && IsMouseover)
                {
                    API.CastSpell(Rejuvenation + MO);
                    return;
                }
                if (API.CanCast(Regrowth) && InRange && RegrowthCheck && (!QuakingHelper || QuakingRegrowth && QuakingHelper))
                {
                    API.CastSpell(Regrowth);
                    return;
                }
                if (API.CanCast(Regrowth) && InMORange && RegrowthMOCheck && (!QuakingHelper || QuakingRegrowth && QuakingHelper) && IsMouseover)
                {
                    API.CastSpell(Regrowth + MO);
                    return;
                }
                if (API.CanCast(Nourish) && InRange && NourishCheck && (!QuakingHelper || QuakingNourish && QuakingHelper))
                {
                    API.CastSpell(Nourish);
                    return;
                }
                if (API.CanCast(Nourish) && InMORange && NourishMOCheck && (!QuakingHelper || QuakingNourish && QuakingHelper) && IsMouseover)
                {
                    API.CastSpell(Nourish + MO);
                    return;
                }
                //DPS
                if (API.PlayerIsInCombat)
                {
                    if (!API.PlayerIsCasting(false) && API.PlayerHasBuff(MoonkinForm) && !(API.PlayerHasBuff(EclipseLunar) || API.PlayerHasBuff(EclipseSolar)) || !API.PlayerIsCasting(true) && (API.PlayerHasBuff(EclipseLunar) || API.PlayerHasBuff(EclipseSolar) || !API.PlayerHasBuff(MoonkinForm)))
                    {
                        if (API.PlayerCanAttackTarget && IsAutoForm && API.CanCast(MoonkinForm) && BalanceAffinity && !API.PlayerHasBuff(MoonkinForm) && !ChannelingCov && !ChannelingTranq && InRange && API.TargetHealthPercent > 0)
                        {
                            API.CastSpell(MoonkinForm);
                            return;
                        }
                        if (API.PlayerHasBuff(MoonkinForm))
                        {
                            if (API.CanCast(HeartoftheWild) && HeartoftheWildTalent && !ChannelingCov && !ChannelingTranq && (UseHeart == "With Cooldowns" && IsCooldowns || UseHeart == "On Cooldown"))
                            {
                                API.CastSpell(HeartoftheWild);
                                return;
                            }
                            if (API.CanCast(Starsurge) && BalanceAffinity && API.PlayerCanAttackTarget && !ChannelingCov && !ChannelingTranq && !API.PlayerIsMoving && (API.PlayerHasBuff(EclipseLunar) || API.PlayerHasBuff(EclipseSolar)) && InRange && API.TargetHealthPercent > 0)
                            {
                                API.CastSpell(Starsurge);
                                return;
                            }
                            if (API.CanCast(Starsurge) && BalanceAffinity && API.PlayerCanAttackMouseover && !ChannelingCov && !ChannelingTranq && !API.PlayerIsMoving && (API.PlayerHasBuff(EclipseLunar) || API.PlayerHasBuff(EclipseSolar)) && InMORange && IsMouseover && API.MouseoverHealthPercent > 0)
                            {
                                API.CastSpell(Starsurge + MO);
                                return;
                            }
                            if (API.CanCast(Moonfire) && (API.PlayerHasBuff(EclipseLunar) || API.PlayerHasBuff(EclipseSolar)) && API.TargetUnitInRangeCount < 5 && API.TargetDebuffRemainingTime(Moonfire) < 300 && InRange && API.PlayerCanAttackTarget && NotChanneling && !ChannelingTranq && !ChannelingCov && API.TargetHealthPercent > 0)
                            {
                                API.CastSpell(Moonfire);
                                return;
                            }
                            if (API.CanCast(Moonfire) && (API.PlayerHasBuff(EclipseLunar) || API.PlayerHasBuff(EclipseSolar)) && API.TargetUnitInRangeCount < 5 && API.MouseoverDebuffRemainingTime(Moonfire) < 300 && InMORange && IsMouseover && API.PlayerCanAttackMouseover && NotChanneling && !ChannelingTranq && !ChannelingCov && API.MouseoverHealthPercent > 0)
                            {
                                API.CastSpell(Moonfire + MO);
                                return;
                            }
                            if (API.CanCast(Sunfire) && (API.PlayerHasBuff(EclipseLunar) || API.PlayerHasBuff(EclipseSolar) || API.TargetUnitInRangeCount >= 5) && InRange && API.TargetDebuffRemainingTime(Sunfire) < 300 && API.PlayerCanAttackTarget && NotChanneling && !ChannelingTranq && !ChannelingCov && API.TargetHealthPercent > 0)
                            {
                                API.CastSpell(Sunfire);
                                return;
                            }
                            if (API.CanCast(Sunfire) && (API.PlayerHasBuff(EclipseLunar) || API.PlayerHasBuff(EclipseSolar) || API.TargetUnitInRangeCount >= 5) && InMORange && IsMouseover && API.MouseoverDebuffRemainingTime(Sunfire) < 300 && API.PlayerCanAttackMouseover && NotChanneling && !ChannelingTranq && !ChannelingCov && API.MouseoverHealthPercent > 0)
                            {
                                API.CastSpell(Sunfire + MO);
                                return;
                            }
                            if (API.CanCast(Starfire) && BalanceAffinity && InRange && API.PlayerCanAttackTarget && !ChannelingCov && !ChannelingTranq && !API.PlayerIsMoving && API.TargetHealthPercent > 0 && (Lunarwatch.IsRunning || API.TargetUnitInRangeCount >= 5) && (!QuakingHelper || QuakingStar && QuakingHelper))
                            {
                                API.CastSpell(Starfire);
                                return;
                            }
                            if (API.CanCast(Starfire) && BalanceAffinity && InMORange && IsMouseover && API.PlayerCanAttackMouseover && !ChannelingCov && !ChannelingTranq && !API.PlayerIsMoving && API.MouseoverHealthPercent > 0 && (Lunarwatch.IsRunning || API.TargetUnitInRangeCount >= 5) && (!QuakingHelper || QuakingStar && QuakingHelper))
                            {
                                API.CastSpell(Starfire + MO);
                                return;
                            }
                            if (API.CanCast(Wrath) && InRange && API.PlayerCanAttackTarget && !ChannelingCov && !ChannelingTranq && !API.PlayerIsMoving && Solarwatch.IsRunning && API.TargetHealthPercent > 0 && (!QuakingHelper || QuakingWrath && QuakingHelper))
                            {
                                API.CastSpell(Wrath);
                                return;
                            }
                            if (API.CanCast(Wrath) && InMORange && IsMouseover && API.PlayerCanAttackMouseover && !ChannelingCov && !ChannelingTranq && !API.PlayerIsMoving && Solarwatch.IsRunning && API.MouseoverHealthPercent > 0 && (!QuakingHelper || QuakingWrath && QuakingHelper))
                            {
                                API.CastSpell(Wrath + MO);
                                return;
                            }
                            if (API.CanCast(Wrath) && InRange && !Lunarwatch.IsRunning && !Solarwatch.IsRunning && API.TargetUnitInRangeCount >= 2 && API.PlayerCanAttackTarget && !ChannelingCov && !ChannelingTranq && !API.PlayerIsMoving && API.TargetHealthPercent > 0 && (!QuakingHelper || QuakingWrath && QuakingHelper))
                            {
                                API.CastSpell(Wrath);
                                return;
                            }
                            if (API.CanCast(Wrath) && InMORange && IsMouseover && !Lunarwatch.IsRunning && !Solarwatch.IsRunning && API.TargetUnitInRangeCount >= 2 && API.PlayerCanAttackMouseover && !ChannelingCov && !ChannelingTranq && !API.PlayerIsMoving && API.MouseoverHealthPercent > 0 && (!QuakingHelper || QuakingWrath && QuakingHelper))
                            {
                                API.CastSpell(Wrath + MO);
                                return;
                            }
                            if (API.CanCast(Starfire) && API.PlayerHasBuff(MoonkinForm) && !Lunarwatch.IsRunning && !Solarwatch.IsRunning && API.TargetUnitInRangeCount < 2 && BalanceAffinity && InRange && API.PlayerCanAttackTarget && !ChannelingCov && !ChannelingTranq && !API.PlayerIsMoving && API.TargetHealthPercent > 0 && (!QuakingHelper || QuakingStar && QuakingHelper))
                            {
                                API.CastSpell(Starfire);
                                return;
                            }
                            if (API.CanCast(Starfire) && API.PlayerHasBuff(MoonkinForm) && !Lunarwatch.IsRunning && !Solarwatch.IsRunning && API.TargetUnitInRangeCount < 2 && BalanceAffinity && InMORange && IsMouseover && API.PlayerCanAttackMouseover && !ChannelingCov && !ChannelingTranq && !API.PlayerIsMoving && API.MouseoverHealthPercent > 0 && (!QuakingHelper || QuakingStar && QuakingHelper))
                            {
                                API.CastSpell(Starfire + MO);
                                return;
                            }
                        }
                        if (!API.PlayerHasBuff(MoonkinForm))
                        {
                            if (API.CanCast(Moonfire) && API.TargetUnitInRangeCount < 5 && API.TargetDebuffRemainingTime(Moonfire) < 300 && InRange && API.PlayerCanAttackTarget && NotChanneling && !ChannelingTranq && !ChannelingCov && API.TargetHealthPercent > 0)
                            {
                                API.CastSpell(Moonfire);
                                return;
                            }
                            if (API.CanCast(Moonfire) && API.TargetUnitInRangeCount < 5 && API.MouseoverDebuffRemainingTime(Moonfire) < 300 && InMORange && IsMouseover && API.PlayerCanAttackMouseover && NotChanneling && !ChannelingTranq && !ChannelingCov && API.MouseoverHealthPercent > 0)
                            {
                                API.CastSpell(Moonfire + MO);
                                return;
                            }
                            if (API.CanCast(Sunfire) && InRange && API.TargetDebuffRemainingTime(Sunfire) < 300 && API.PlayerCanAttackTarget && NotChanneling && !ChannelingTranq && !ChannelingCov && API.TargetHealthPercent > 0)
                            {
                                API.CastSpell(Sunfire);
                                return;
                            }
                            if (API.CanCast(Sunfire) && InMORange && IsMouseover && API.MouseoverDebuffRemainingTime(Sunfire) < 300 && API.PlayerCanAttackMouseover && NotChanneling && !ChannelingTranq && !ChannelingCov && API.MouseoverHealthPercent > 0)
                            {
                                API.CastSpell(Sunfire + MO);
                                return;
                            }
                            if (API.CanCast(Wrath) && InRange && API.PlayerCanAttackTarget && !ChannelingCov && !ChannelingTranq && !API.PlayerIsMoving && API.TargetHealthPercent > 0 && (!QuakingHelper || QuakingWrath && QuakingHelper))
                            {
                                API.CastSpell(Wrath);
                                return;
                            }
                            if (API.CanCast(Wrath) && InMORange && IsMouseover && API.PlayerCanAttackMouseover && !ChannelingCov && !ChannelingTranq && !API.PlayerIsMoving && API.MouseoverHealthPercent > 0 && (!QuakingHelper || QuakingWrath && QuakingHelper))
                            {
                                API.CastSpell(Wrath + MO);
                                return;
                            }
                        }
                    }
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
                        for (int i = 0; i < units.Length; i++)
                            for (int j = 0; j < DispellList.Length; j++)
                                for (int t = 0; t < TargetingUnits.Length; t++)
                                {
                                if (API.PlayerHealthPercent <= PlayerHP && API.TargetIsUnit() != "player")
                                {
                                    API.CastSpell(Player);
                                    return;
                                }
                                if (API.UnitRoleSpec(units[i]) == API.TankRole && UseLeg == Verdant && API.UnitRange(units[i]) <= 40 && API.SpellCharges(Swiftmend) > 0 && API.CanCast(Swiftmend) && (!SwapWatch.IsRunning || SwapWatch.ElapsedMilliseconds >= API.SpellGCDTotalDuration * 10) && UnitHasBuff(Rejuvenation, units[i]) && UnitHasBuff(Lifebloom, units[i]) && UnitHasBuff(CenarionWardHoT, units[i]) && API.TargetIsUnit() != units[i])
                                {
                                    API.CastSpell(PlayerTargetArray[i]);
                                    SwapWatch.Restart();
                                    return;
                                }
                                if (API.UnitHealthPercent(units[i]) <= 10 && API.UnitHealthPercent(units[i]) > 0  && API.UnitRange(units[i]) <= 40 && API.TargetIsUnit() != units[i])
                                    {
                                        API.CastSpell(PlayerTargetArray[i]);
                                        SwapWatch.Restart();
                                        return;
                                    }
                                    if (UnitHasDispellAble(DispellList[j], units[i]) && IsDispell && !API.SpellISOnCooldown(NaturesCure) && API.TargetIsUnit() != units[i])
                                    {
                                        API.CastSpell(PlayerTargetArray[i]);
                                        return;
                                    }
                                    if ((UseLife == "Healer" || TorghastHelper && UseLifeTorghast == "player") && PhotosynthesisTalent && !API.PlayerHasBuff(Lifebloom) && LifeBloomTracking && UseLeg != "The Dark Titan's Lesson" && API.PlayerHealthPercent > 0 && API.TargetIsUnit() != units[i])
                                    {
                                        API.CastSpell(Player);
                                        return;
                                    }
                                if ((API.UnitRoleSpec(units[i]) == RoleSpec || TorghastHelper && UseLifeTorghast == units[i]) && !API.UnitHasBuff(Lifebloom, units[i]) && LifeBloomTracking && UseLeg != "The Dark Titan's Lesson" && API.UnitRange(units[i]) <= 40 && API.UnitHealthPercent(units[i]) > 0 && API.TargetIsUnit() != units[i])
                                    {
                                        API.CastSpell(PlayerTargetArray[i]);
                                        return;
                                    }
                                    if ((API.UnitRoleSpec(units[i]) == RoleSpec && UseLeg == "The Dark Titan's Lesson" || TorghastHelper && UseLifeTorghast == units[i]) && !UnitHasBuff(LifebloomL, units[i]) && API.UnitRange(units[i]) <= 40 && API.UnitHealthPercent(units[i]) > 0 && API.TargetIsUnit() != units[i])
                                    {
                                        API.CastSpell(PlayerTargetArray[i]);
                                        return;
                                    }
                                    if (!API.PlayerHasBuff(LifebloomL) && UseLeg == "The Dark Titan's Lesson" && API.UnitRange(units[i]) <= 40 && API.UnitHealthPercent(units[i]) > 0 && API.TargetIsUnit() != units[i])
                                    {
                                        API.CastSpell(Player);
                                        return;
                                    }
                                    if ((API.UnitRoleSpec(units[i]) == API.TankRole || TorghastHelper && TankinTorghast == units[i]) && (!SwapWatch.IsRunning || SwapWatch.ElapsedMilliseconds >= API.SpellGCDTotalDuration * 10) && API.UnitHealthPercent(units[i]) <= TankHealth && API.UnitHealthPercent(units[i]) > 0 && API.TargetIsUnit() != units[i])
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
                                    if (IsDPS && !API.PlayerCanAttackTarget && (API.UnitRoleSpec(units[i]) == API.TankRole || TorghastHelper && TankinTorghast == units[i]) && !API.MacroIsIgnored("Assist") && UnitAboveHealthPercentParty(AoEDPSHLifePercent) >= AoEDPSNumber && API.UnitRange(units[i]) <= 40 && API.UnitHealthPercent(units[i]) > 0 && API.PlayerIsInCombat && API.TargetIsUnit() != units[i])
                                    {
                                        API.CastSpell(PlayerTargetArray[i]);
                                        API.CastSpell("Assist");
                                        SwapWatch.Restart();
                                        return;
                                    }
                                }
                        
                    }
                    if (API.PlayerIsInRaid)
                    {
                        for (int i = 0; i < raidunits.Length; i++)
                        {
                            if (API.PlayerHealthPercent <= PlayerHP && API.TargetIsUnit() != "player")
                            {
                                API.CastSpell(Player);
                                return;
                            }
                            if (API.UnitRoleSpec(raidunits[i]) == API.TankRole && UseLeg == Verdant && API.UnitRange(raidunits[i]) <= 40 && API.SpellCharges(Swiftmend) > 0 && (!SwapWatch.IsRunning || SwapWatch.ElapsedMilliseconds >= API.SpellGCDTotalDuration * 10) && UnitHasBuff(Rejuvenation, raidunits[i]) && UnitHasBuff(Lifebloom, raidunits[i]) && UnitHasBuff(CenarionWardHoT, raidunits[i]) && API.TargetIsUnit() != raidunits[i])
                            {
                                API.CastSpell(RaidTargetArray[i]);
                                SwapWatch.Restart();
                                return;
                            }
                            if (API.UnitHealthPercent(raidunits[i]) <= 10 && API.UnitHealthPercent(raidunits[i]) > 0 && API.UnitRange(raidunits[i]) <= 40 && !UnitHasDebuff("Gluttonous Miasma", raidunits[i]) && API.TargetIsUnit() != raidunits[i])
                            {
                                API.CastSpell(RaidTargetArray[i]);
                                return;
                            }
                            if (API.UnitRoleSpec(raidunits[i]) == API.TankRole && !UnitHasBuff(Lifebloom, raidunits[i]) && LifeBloomTracking && UseLeg != "The Dark Titan's Lesson" && API.UnitRange(raidunits[i]) <= 40 && API.UnitHealthPercent(raidunits[i]) > 0 && !UnitHasDebuff("Gluttonous Miasma", raidunits[i]) && API.TargetIsUnit() != raidunits[i])
                            {
                                API.CastSpell(RaidTargetArray[i]);
                                SwapWatch.Restart();
                                return;
                            }
                            if (API.UnitRoleSpec(raidunits[i]) == API.TankRole && UseLeg == "The Dark Titan's Lesson" && !UnitHasBuff(LifebloomL, raidunits[i]) && LifebloomRaidLTracking && API.UnitRange(raidunits[i]) <= 40 && API.UnitHealthPercent(raidunits[i]) > 0 && !UnitHasDebuff("Gluttonous Miasma", raidunits[i]) && API.TargetIsUnit() != raidunits[i])
                            {
                                API.CastSpell(RaidTargetArray[i]);
                                return;
                            }
                            if (!API.PlayerHasBuff(LifebloomL) && UseLeg == "The Dark Titan's Lesson" && LifebloomRaidLTracking && API.UnitRange(raidunits[i]) <= 40 && API.UnitHealthPercent(raidunits[i]) > 0 && PlayerHasDebuff("Gluttonous Miasma") && API.TargetIsUnit() != raidunits[i])
                            {
                                API.CastSpell(Player);
                                return;
                            }
                            if (API.UnitRoleSpec(raidunits[i]) == API.TankRole && (!SwapWatch.IsRunning || SwapWatch.ElapsedMilliseconds >= API.SpellGCDTotalDuration * 10) && API.UnitHealthPercent(raidunits[i]) <= TankHealth && API.UnitHealthPercent(raidunits[i]) > 0 && API.TargetIsUnit() != raidunits[i])
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
                            if (IsDPS && !API.PlayerCanAttackTarget && API.UnitRange(raidunits[i]) <= 40 && API.UnitRoleSpec(raidunits[i]) == API.TankRole && !API.MacroIsIgnored("Assist") && UnitAboveHealthPercentRaid(AoEDPSHRaidLifePercent) >= AoEDPSRaidNumber && API.UnitHealthPercent(raidunits[i]) > 0 && API.PlayerIsInCombat && API.TargetIsUnit() != raidunits[i])
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



        public override void CombatPulse()
        {
            if (API.CanCast(Fleshcraft) && PlayerCovenantSettings == "Necrolord" && API.PlayerHealthPercent <= FleshcraftPercentProc && NotChanneling && !ChannelingTranq && !ChannelingCov && !API.PlayerIsMoving)
            {
                API.CastSpell(Fleshcraft);
                return;
            }
            if (API.PlayerHealthPercent <= BarkskinLifePercent && Level >= 24 && API.CanCast(Barkskin))
            {
                API.CastSpell(Barkskin);
                return;
            }
            if (API.CanCast(Renewal) && API.PlayerHealthPercent <= RenewalLifePercent && RenewalTalent)
            {
                API.CastSpell(Renewal);
                return;
            }
            if (API.PlayerHealthPercent <= FrenziedRegenerationLifePercent && API.PlayerRage >= 10 && API.CanCast(FrenziedRegeneration) && API.PlayerHasBuff(BearForm) && GuardianAffintiy)
            {
                API.CastSpell(FrenziedRegeneration);
                return;
            }
            if (API.PlayerHealthPercent <= IronfurLifePercent && API.PlayerRage >= 40 && API.CanCast(Ironfur) && GuardianAffintiy && API.PlayerHasBuff(BearForm))
            {
                API.CastSpell(Ironfur);
                return;
            }
            if (API.PlayerHealthPercent <= BearFormLifePercent && Level >= 8 && AutoForm && API.CanCast(BearForm) && !API.PlayerHasBuff(BearForm))
            {
                API.CastSpell(BearForm);
                return;
            }
            if (API.PlayerHealthPercent > BearFormLifePercent && BearFormLifePercent != 0 && API.CanCast(BearForm) && API.PlayerHasBuff(BearForm) && AutoForm)
            {
                API.CastSpell(BearForm);
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
            if (API.PlayerTrinketIsUsable(1) && API.PlayerTrinketRemainingCD(1) == 0 && IsTrinkets1 && !ChannelingCov && !ChannelingTranq && NotChanneling && InRange)
            {
                API.CastSpell("Trinket1");
            }
            if (API.PlayerTrinketIsUsable(2) && API.PlayerTrinketRemainingCD(2) == 0 && IsTrinkets2 && !ChannelingCov && !ChannelingTranq && NotChanneling && InRange)
            {
                API.CastSpell("Trinket2");
            }
            if (API.PlayerHasBuff(BearForm))
            {
                if (API.CanCast(Thrashbear) && API.TargetRange < 9 && GuardianAffintiy && API.PlayerCanAttackTarget)
                {
                    API.CastSpell(Thrashbear);
                    return;
                }
                if (API.CanCast(Mangle) && API.TargetRange < 6 && API.PlayerCanAttackTarget)
                {
                    API.CastSpell(Mangle);
                    return;
                }
                if (API.CanCast(Swipebear) && FeralAffinity && API.TargetRange < 9 && API.PlayerCanAttackTarget)
                {
                    API.CastSpell(Swipebear);
                    return;
                }
            }
            if (API.PlayerHasBuff(CatForm))
            {
                if (API.PlayerComboPoints == 5)
                {
                    if (API.CanCast(Rip) && FeralAffinity && API.TargetRange < 6 && API.PlayerEnergy >= 20 && (!API.TargetHasDebuff(Rip) || API.TargetDebuffRemainingTime(Rip) < 600) && API.PlayerCanAttackTarget)
                    {
                        API.CastSpell(Rip);
                        return;
                    }
                    if (API.CanCast(FerociousBite) && API.TargetRange < 6 && API.PlayerEnergy >= 50 && API.PlayerCanAttackTarget)
                    {
                        API.CastSpell(FerociousBite);
                        return;
                    }
                }
                if (API.PlayerComboPoints < 5)
                {
                    if (API.CanCast(Rake) && FeralAffinity && API.TargetRange < 6 && API.PlayerEnergy >= 35 && API.TargetDebuffRemainingTime(Rake) <= 360 && API.PlayerCanAttackTarget)
                    {
                        API.CastSpell(Rake);
                        return;
                    }
                    if (API.CanCast(Thrashkitty) && API.TargetRange < 9 && API.PlayerEnergy >= 40 && GuardianAffintiy && API.TargetDebuffRemainingTime(Thrashkitty) <= 200 && API.PlayerCanAttackTarget)
                    {
                        API.CastSpell(Thrashkitty);
                        return;
                    }
                    if (API.CanCast(Swipekitty) && FeralAffinity && API.PlayerEnergy >= 35 && API.TargetRange < 9 && API.PlayerUnitInMeleeRangeCount >= 2 && API.PlayerCanAttackTarget)
                    {
                        API.CastSpell(Swipekitty);
                        return;
                    }
                    if (API.CanCast(Shred) && API.TargetRange < 6 && API.PlayerEnergy >= 40 && API.PlayerCanAttackTarget)
                    {
                        API.CastSpell(Shred);
                        return;
                    }
                }
            }
        }

        public override void OutOfCombatPulse()
        {
            {
                if (API.PlayerCurrentCastTimeRemaining > 40)
                    return;
                if (API.CanCast(TravelForm) && AutoTravelForm && API.PlayerIsOutdoor && !API.PlayerHasBuff(TravelForm))
                {
                    API.CastSpell(TravelForm);
                    return;
                }
            }

        }

    }
}