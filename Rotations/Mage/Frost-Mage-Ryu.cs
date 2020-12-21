﻿
namespace HyperElk.Core
{
    public class FrostMage : CombatRoutine
    {
        //Spell Strings
        private string Flurry = "Flurry";
        private string Icicles = "Icicles";
        private string BrainFreeze = "Brain Freeze";
        private string FoF = "Fingers of Frost";
        private string IF = "Ice Floes";
        private string RoP = "Rune of Power";
        private string IceBarrier = "Ice Barrier";
        private string AI = "Arcane Intellect";
        private string Frostbolt = "Frostbolt";
        private string IL = "Ice Lance";
        private string CoC = "Cone of Cold";
        private string EB = "Ebonbolt";
        private string  Blizzard = "Blizzard";
        private string  GS = "Glacial Spike";
        private string  IV = "Icy Veins";
        private string MI = "Mirror Image";
        private string  IN = "Ice Nova";
        private string  FO ="Frozen Orb";
        private string  CS = "Comet Storm";
        private string RoF = "Ray of Frost";
        private string  Freeze = "Freeze";
        private string  WE = "Water Elemental";
        private string Counterspell = "Counterspell";
        private string WC = "Winter's Chill";
        private string IB = "Ice Block";
        private string ShiftingPower = "Shifting Power";
        private string RadiantSpark = "Radiant Spark";
        private string Deathborne = "Deathborne";
        private string MirrorsofTorment = "Mirrors of Torment";
        private string AE = "Arcane Explosion";
        private string Fleshcraft = "Fleshcraft";
        private string FR = "Freezing Rain";
        private string Trinket1 = "Trinket1";
        private string Trinket2 = "Trinket2";
        private string SlickIce = "Slick Ice";
        private string TimeWarp = "Time Warp";
        private string Temp = "Temporal Displacement";
        private string Exhaustion = "Exhaustion";
        private string Fatigued = "Fatigued";
        private string BL = "Bloodlust";
        private string AH = "Ancient Hysteria";
        private string TW = "Temporal Warp";
        private string Sated = "Sated";
        private string PhialofSerenity = "Phial of Serenity";
        private string SpiritualHealingPotion = "Spiritual Healing Potion";
        //Talents
        bool LonelyWinter => API.PlayerIsTalentSelected(1, 2);
        bool IceNova => API.PlayerIsTalentSelected(1, 3);
        bool IceFloes => API.PlayerIsTalentSelected(2, 3);
        bool RuneOfPower => API.PlayerIsTalentSelected(3, 3);
        bool Ebonbolt => API.PlayerIsTalentSelected(4, 3);
        bool Cometstorm => API.PlayerIsTalentSelected(6, 3);
        bool RayofFrost => API.PlayerIsTalentSelected(7, 2);
        bool GlacialSpike => API.PlayerIsTalentSelected(7, 3);
        bool FreezingRain => API.PlayerIsTalentSelected(6, 1);

        //CBProperties
        public string[] LegendaryList = new string[] { "None", "Temporal Warp" };
        int[] numbList = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100 };
        private int IceBarrierPercentProc => numbList[CombatRoutine.GetPropertyInt(IceBarrier)];
        private int IBPercentProc => numbList[CombatRoutine.GetPropertyInt(IB)];
        private int MIPercentProc => numbList[CombatRoutine.GetPropertyInt(MI)];
        private int PhialofSerenityLifePercent => numbList[CombatRoutine.GetPropertyInt(PhialofSerenity)];
        private int SpiritualHealingPotionLifePercent => numbList[CombatRoutine.GetPropertyInt(SpiritualHealingPotion)];
        private int Trinket1Usage => CombatRoutine.GetPropertyInt("Trinket1");
        private int Trinket2Usage => CombatRoutine.GetPropertyInt("Trinket2");
        private string UseCovenant => CDUsageWithAOE[CombatRoutine.GetPropertyInt("Use Covenant")];
        private string UseROP => CDUsage[CombatRoutine.GetPropertyInt(RoP)];
        private string UseIV => CDUsage[CombatRoutine.GetPropertyInt(IV)];
        private string UseLeg => LegendaryList[CombatRoutine.GetPropertyInt("Legendary")];
        private int FleshcraftPercentProc => numbList[CombatRoutine.GetPropertyInt(Fleshcraft)];
        //General
        private string UseTrinket1 => CDUsageWithAOE[CombatRoutine.GetPropertyInt("Trinket1")];
        private string UseTrinket2 => CDUsageWithAOE[CombatRoutine.GetPropertyInt("Trinket2")];
        private bool IsTimeWarp => API.ToggleIsEnabled("TimeWarp");
        bool CastFlurry => API.PlayerLastSpell == Flurry;
        bool CastShifting => API.PlayerLastSpell == ShiftingPower;
        bool CastIV => API.PlayerLastSpell == IV;
        bool CastFB => API.PlayerLastSpell == Frostbolt;
        bool CastIL => API.PlayerLastSpell == IL;
        bool CastRune => API.PlayerLastSpell == RoP;
        bool CastTW => API.PlayerLastSpell == TimeWarp;
        bool IsTrinkets1 => (UseTrinket1 == "with Cooldowns" && IsCooldowns || UseTrinket1 == "always" || UseTrinket1 == "on AOE" && API.TargetUnitInRangeCount >= 2 && IsAOE);
        bool IsTrinkets2 => (UseTrinket2 == "with Cooldowns" && IsCooldowns || UseTrinket2 == "always" || UseTrinket2 == "on AOE" && API.TargetUnitInRangeCount >= 2 && IsAOE);
        private int Level => API.PlayerLevel;
        //private bool NotCasting => !API.PlayerIsCasting;
        private bool NotChanneling => !API.PlayerIsChanneling;
        private bool BLDebuffs => (!API.PlayerHasDebuff(Temp) || !API.PlayerHasDebuff(Exhaustion) || !API.PlayerHasDebuff(Fatigued));
        private bool BLBuFfs => (!API.PlayerHasBuff(BL) || !API.PlayerHasBuff(AH) || !API.PlayerHasBuff(TimeWarp) || !API.PlayerHasBuff(TW));

        bool ChannelingShift => API.CurrentCastSpellID("player") == 314791;
        private bool InRange => API.TargetRange <= 40;



        public override void Initialize()
        {
            CombatRoutine.Name = "Frost Mage by Ryu";
            API.WriteLog("Welcome to Frost Mage by Ryu");
            API.WriteLog("Create the following cursor macro for Blizzard");
            API.WriteLog("Blizzard -- /cast [@cursor] Blizzard");
            API.WriteLog("Create Macro /cast [@Player] Arcane Intellect to buff Arcane Intellect so you don't require a target");
            API.WriteLog("All Talents expect Ring of Frost supported. All Cooldowns are associated with Cooldown toggle.");
            API.WriteLog("Legendary Support for Temporal Warp added. If you have it please select it in the settings.");
            //Buff

            CombatRoutine.AddBuff(Icicles, 205473);
            CombatRoutine.AddBuff(BrainFreeze, 190446);
            CombatRoutine.AddBuff(FoF, 44544);
            CombatRoutine.AddBuff(IF, 108839);
            CombatRoutine.AddBuff(RoP, 116014);
            CombatRoutine.AddBuff(IceBarrier, 11426);
            CombatRoutine.AddBuff(AI, 1459);
            CombatRoutine.AddBuff(IV, 12472);
            CombatRoutine.AddBuff(FR, 240555);
            CombatRoutine.AddBuff(SlickIce, 327511);
            CombatRoutine.AddBuff(TimeWarp, 80353);
            CombatRoutine.AddBuff(BL, 2825);
            CombatRoutine.AddBuff(AH, 90355);
            CombatRoutine.AddBuff(TW, 327351);

            //Debuff
            CombatRoutine.AddDebuff(WC, 228358);
            CombatRoutine.AddDebuff(Temp, 80354);
            CombatRoutine.AddDebuff(Fatigued, 264689);
            CombatRoutine.AddDebuff(Exhaustion, 57723);
            CombatRoutine.AddDebuff(Sated, 57724);

            //Spell
            CombatRoutine.AddSpell(RoP, 116011, "None");
            CombatRoutine.AddSpell(IB, 45438);
            CombatRoutine.AddSpell(Frostbolt, 116);
            CombatRoutine.AddSpell(IL, 30455);
            CombatRoutine.AddSpell(Flurry, 44614);
            CombatRoutine.AddSpell(CoC, 120);
            CombatRoutine.AddSpell(EB, 257537);
            CombatRoutine.AddSpell(Blizzard, 190356);
            CombatRoutine.AddSpell(GS, 199786, "C");
            CombatRoutine.AddSpell(IV, 12472, "C");
            CombatRoutine.AddSpell(IceBarrier, 11426, "C");
            CombatRoutine.AddSpell(MI, 55342, "C");
            CombatRoutine.AddSpell(IN, 157997, "None");
            CombatRoutine.AddSpell(FO, 84714, "None");
            CombatRoutine.AddSpell(CS, 153595, "None");
            CombatRoutine.AddSpell(RoF, 205021, "None");
            CombatRoutine.AddSpell(Freeze, 33395, "None");
            CombatRoutine.AddSpell(WE, 31687, "None");
            CombatRoutine.AddSpell(IF, 108839, "None");
            CombatRoutine.AddSpell(AI, 1459, "None");
            CombatRoutine.AddSpell(Counterspell, "None");
            CombatRoutine.AddSpell(ShiftingPower, 314791);
            CombatRoutine.AddSpell(RadiantSpark, 307443);
            CombatRoutine.AddSpell(Deathborne, 324220);
            CombatRoutine.AddSpell(MirrorsofTorment, 314793);
            CombatRoutine.AddSpell(Fleshcraft, 324631);
            CombatRoutine.AddSpell(TimeWarp, 80353);
            CombatRoutine.AddSpell(AE, 1449);

            //Item
            CombatRoutine.AddItem(PhialofSerenity, 177278);
            CombatRoutine.AddItem(SpiritualHealingPotion, 171267);

            //Macro
            CombatRoutine.AddMacro(Trinket1);
            CombatRoutine.AddMacro(Trinket2);

            //Toggle
            CombatRoutine.AddToggle("TimeWarp");

            //Prop
            CombatRoutine.AddProp(IceBarrier, IceBarrier, numbList, "Life percent at which " + IceBarrier + " is used, set to 0 to disable", "Defense", 5);
            CombatRoutine.AddProp(IB, IB, numbList, "Life percent at which " + IB + " is used, set to 0 to disable", "Defense", 6);
            CombatRoutine.AddProp(MI, MI, numbList, "Life percent at which " + MI + " is used, set to 0 to disable", "Defense", 7);
            CombatRoutine.AddProp(PhialofSerenity, PhialofSerenity + " Life Percent", numbList, " Life percent at which" + PhialofSerenity + " is used, set to 0 to disable", "Defense", 40);
            CombatRoutine.AddProp(SpiritualHealingPotion, SpiritualHealingPotion + " Life Percent", numbList, " Life percent at which" + SpiritualHealingPotion + " is used, set to 0 to disable", "Defense", 40);
            CombatRoutine.AddProp(Fleshcraft, "Fleshcraft", numbList, "Life percent at which " + Fleshcraft + " is used, set to 0 to disable set 100 to use it everytime", "Defense", 8);
            CombatRoutine.AddProp("Use Covenant", "Use " + "Covenant Ability", CDUsageWithAOE, "Use " + "Covenant" + " On Cooldown, with Cooldowns, On AOE, Not Used", "Cooldowns", 1);
            CombatRoutine.AddProp(RoP, "Use " + RoP, CDUsage, "Use " + RoP + "On Cooldown, With Cooldowns or Not Used", "Cooldowns", 0);
            CombatRoutine.AddProp(IV, "Use " + IV, CDUsage, "Use " + IV + "On Cooldown, With Cooldowns or Not Used", "Cooldowns", 0);
            CombatRoutine.AddProp("Trinket1", "Trinket1 usage", CDUsageWithAOE, "When should trinket1 be used", "Trinket", 0);
            CombatRoutine.AddProp("Trinket2", "Trinket2 usage", CDUsageWithAOE, "When should trinket1 be used", "Trinket", 0);
            CombatRoutine.AddProp("Legendary", "Select your Legendary", LegendaryList, "Select Your Legendary", "Legendary");

        }

        public override void Pulse()
        {
            if (!API.PlayerIsMounted)
            {
                if (API.CanCast(AI) && Level >= 8 && !API.PlayerHasBuff(AI))
                {
                    API.CastSpell(AI);
                    return;
                }
                if (!API.PlayerHasPet && !LonelyWinter && API.CanCast(WE) && !API.PlayerIsMoving && Level >= 12)
                {
                    API.CastSpell(WE);
                    return;
                }
            }
        }
        public override void CombatPulse()
        {
            if (isInterrupt && API.CanCast(Counterspell) && Level >= 7 && API.PlayerIsCasting(false))
            {
                API.CastSpell(Counterspell);
                return;
            }
            if (API.CanCast(IB) && API.PlayerHealthPercent <= IBPercentProc && API.PlayerHealthPercent != 0 && Level >= 22  && !ChannelingShift)
            {
                API.CastSpell(IB);
                return;
            }
            if (API.CanCast(MI) && API.PlayerHealthPercent <= MIPercentProc && API.PlayerHealthPercent !=0 && Level >= 44  && !ChannelingShift)
            {
                API.CastSpell(MI);
                return;
            }
            if (API.CanCast(Fleshcraft) && PlayerCovenantSettings == "Necrolord" && API.PlayerHealthPercent <= FleshcraftPercentProc  && !ChannelingShift)
            {
                API.CastSpell(Fleshcraft);
                return;
            }
            if (API.CanCast(IceBarrier) && Level >= 21 && !API.PlayerHasBuff(IceBarrier) && API.PlayerHealthPercent <= IceBarrierPercentProc && API.PlayerHealthPercent != 0  && !ChannelingShift)
            {
                API.CastSpell(IceBarrier);
                return;
            }
            if (API.PlayerItemCanUse("Healthstone") && API.PlayerItemRemainingCD("Healthstone") == 0 && API.PlayerHealthPercent <= HealthStonePercent)
            {
                API.CastSpell("Healthstone");
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
            if (IsTimeWarp && API.CanCast(TimeWarp) && (!API.PlayerHasDebuff(Temp) || !API.PlayerHasDebuff(Fatigued) || !API.PlayerHasDebuff(Exhaustion) || UseLeg == "Temporal Warp") && (!API.PlayerHasBuff(TW) || !API.PlayerHasBuff(AH) || !API.PlayerHasBuff(BL)))
            {
                API.CastSpell(TimeWarp);
                return;
            }
            if (API.PlayerTrinketIsUsable(1) && API.PlayerTrinketRemainingCD(1) == 0 && IsTrinkets1)
            {
                API.CastSpell("Trinket1");
            }
            if (API.PlayerTrinketIsUsable(2) && API.PlayerTrinketRemainingCD(2) == 0 && IsTrinkets2)
            {
                API.CastSpell("Trinket2");
            }
            if (Level <= 60)
            {
                rotation();
                return;
            }
        }

        public override void OutOfCombatPulse()
        {

        }

        private void rotation()
        {
            if (API.CanCast(Deathborne) && InRange && PlayerCovenantSettings == "Necrolord" && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" || UseCovenant == "on AOE" && IsAOE)  && !ChannelingShift)
            {
                API.CastSpell(Deathborne);
                return;
            }
            if (API.CanCast(IV) && Level >= 29 && !API.PlayerIsMoving && API.TargetRange <= 40 && (IsCooldowns && UseIV == "With Cooldowns" || UseIV == "On Cooldown") && !ChannelingShift)
            {
                API.CastSpell(IV);
                return;
            }
            if (RuneOfPower && API.CanCast(RoP) && API.TargetRange <= 40 && !CastIV && !API.PlayerHasBuff(RoP) && !API.PlayerHasBuff(BrainFreeze) && !API.PlayerHasBuff(FoF) && !API.TargetHasDebuff(WC) && !API.PlayerIsMoving && (IsCooldowns && UseROP == "With Cooldowns" || UseROP == "On Cooldown") && API.SpellCDDuration(IV) >= 1200 && !ChannelingShift)
            {
                API.CastSpell(RoP);
                return;
            }
            //actions.st=flurry,if=(remaining_winters_chill=0|debuff.winters_chill.down)&(prev_gcd.1.ebonbolt|buff.brain_freeze.react&(prev_gcd.1.glacial_spike|prev_gcd.1.frostbolt&(!conduit.ire_of_the_ascended|cooldown.radiant_spark.remains|runeforge.freezing_winds)|prev_gcd.1.radiant_spark|buff.fingers_of_frost.react=0&(debuff.mirrors_of_torment.up|buff.freezing_winds.up|buff.expanded_potential.react)))
            if (API.CanCast(Flurry) && Level >= 19 && (API.PlayerHasBuff(BrainFreeze) || !CastRune) && !CastTW && (API.PlayerLastSpell == GS && GlacialSpike && API.PlayerHasBuff(BrainFreeze) || API.PlayerHasBuff(BrainFreeze) && !GlacialSpike) && API.TargetRange <= 40 && !API.PlayerHasBuff(FoF) && !API.TargetHasDebuff(WC) && (API.PlayerLastSpell == EB && Ebonbolt || !Ebonbolt))
            {
                API.CastSpell(Flurry);
                return;
            }
            //actions.st +=/ frozen_orb
            if (API.CanCast(FO) && Level >= 38 && API.TargetRange <= 40 && !ChannelingShift)
            {
                API.CastSpell(FO);
                return;
            }
            // actions.st+=/blizzard,if=buff.freezing_rain.up|active_enemies>=2 // actions.aoe+=/blizzard
            if (API.CanCast(Blizzard) && Level >= 14 && API.TargetRange <= 40 && !API.PlayerIsMoving && (API.TargetUnitInRangeCount >= 3 && IsAOE || FreezingRain && API.PlayerHasBuff(FR)) && !ChannelingShift)
            {
                API.CastSpell(Blizzard);
                return;
            }
            //actions.st+=/ray_of_frost,if=remaining_winters_chill=1&debuff.winters_chill.remains
            if (RayofFrost && API.CanCast(RoF) && (API.TargetHasDebuff(WC) && API.TargetDebuffStacks(WC) <= 1) && API.PlayerHasBuff(IF) && API.PlayerIsMoving && !ChannelingShift)
            {
                API.CastSpell(RoF);
                return;
            }
            if (RayofFrost && API.CanCast(RoF) && API.TargetHasDebuff(WC) && !API.PlayerIsMoving && !ChannelingShift)
            {
                API.CastSpell(RoF);
                return;
            }
            //actions.st+=/glacial_spike,if=remaining_winters_chill&debuff.winters_chill.remains>cast_time+travel_time //             //actions.st+=/glacial_spike,if=buff.brain_freeze.react
            if (GlacialSpike && API.CanCast(GS) && API.PlayerHasBuff(BrainFreeze) && API.TargetRange <= 40 && API.PlayerBuffStacks(Icicles) > 4 && !API.PlayerIsMoving && !ChannelingShift)
            {
                API.CastSpell(GS);
                return;
            }
            if (GlacialSpike && API.CanCast(GS) && API.TargetHasDebuff(WC) && API.TargetRange <= 40 && API.PlayerBuffStacks(Icicles) > 4 && API.PlayerHasBuff(IF) && API.PlayerIsMoving && !ChannelingShift)
            {
                API.CastSpell(GS);
                return;
            }
            // actions.st+=/ice_lance,if=buff.fingers_of_frost.react|debuff.frozen.remains>travel_time
            if (API.CanCast(IL) && Level >= 10 && API.TargetRange <= 40 && API.PlayerHasBuff(FoF) && !ChannelingShift)
            {
                API.CastSpell(IL);
                API.WriteLog("Winters Debuff" + API.TargetHasDebuff(WC) + "Stacks " + API.TargetDebuffStacks(WC) + "Fingers of Frost " + API.PlayerHasBuff(FoF));
                return;
            }
            // actions.st+=/ice_lance,if=remaining_winters_chill&remaining_winters_chill>buff.fingers_of_frost.react&debuff.winters_chill.remains>travel_time
            if (API.CanCast(IL) && Level >= 10 && API.TargetRange <= 40 && API.TargetDebuffStacks(WC) == 2 && API.TargetDebuffPlayerSrc(WC) && !ChannelingShift)
            {
                API.CastSpell(IL);
                API.WriteLog("Winters Debuff" + API.TargetHasDebuff(WC) + "Stacks " + API.TargetDebuffStacks(WC) + "Fingers of Frost " + API.PlayerHasBuff(FoF));
                return;
            }
            if (API.CanCast(IL) && Level >= 10 && API.TargetRange <= 40 && API.TargetDebuffStacks(WC) == 1 && API.TargetDebuffPlayerSrc(WC) && !ChannelingShift)
            {
                API.CastSpell(IL);
                API.WriteLog("Winters Debuff" + API.TargetHasDebuff(WC) + "Stacks " + API.TargetDebuffStacks(WC) + "Fingers of Frost " + API.PlayerHasBuff(FoF));
                return;
            }
            if (API.CanCast(IF) && IceFloes && API.PlayerIsMoving && !API.PlayerHasBuff(IF)  && !ChannelingShift)
            {
                API.CastSpell(IF);
                return;
            }
            if (API.CanCast(Blizzard) && Level >= 14 && API.TargetRange <= 40 && API.PlayerHasBuff(IF) && API.PlayerIsMoving && API.TargetUnitInRangeCount >= 3 && IsAOE && !ChannelingShift)
            {
                API.CastSpell(Blizzard);
                return;
            }
            //actions.aoe+=/comet_storm // actions.st+=/comet_storm
            if (Cometstorm && API.CanCast(CS) && API.TargetRange <= 40 && !API.PlayerIsMoving  && !ChannelingShift)
            {
                API.CastSpell(CS);
                return;
            }
            //actions.st +=/ ice_nova
            if (IceNova && API.CanCast(IN) && API.TargetRange <= 40 && (API.PlayerIsMoving || !API.PlayerIsMoving) && !ChannelingShift)
            {
                API.CastSpell(IN);
                return;
            }
            //actions.st+=/radiant_spark,if=buff.freezing_winds.up&active_enemies=1
            //actions.st+=/ebonbolt
            if (Ebonbolt && API.CanCast(EB) && API.TargetRange <= 40 && !API.PlayerHasBuff(BrainFreeze) && API.PlayerBuffStacks(Icicles) > 4 && !API.PlayerIsMoving && !ChannelingShift)
            {
                API.CastSpell(EB);
                return;
            }
            if (Ebonbolt && API.CanCast(EB) && API.TargetRange <= 40 && !API.PlayerHasBuff(BrainFreeze) && API.PlayerBuffStacks(Icicles) > 4 && API.PlayerHasBuff(IF) && API.PlayerIsMoving && !ChannelingShift)
            {
                API.CastSpell(EB);
                return;
            }
            //actions.st+=/radiant_spark,if=(!runeforge.freezing_winds|active_enemies>=2)&buff.brain_freeze.react
            if (API.CanCast(RadiantSpark) && API.PlayerHasBuff(BrainFreeze) && InRange && PlayerCovenantSettings == "Kyrian" && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" || UseCovenant == "on AOE" && IsAOE))
            {
                API.CastSpell(RadiantSpark);
                return;
            }
            //actions.st+=/mirrors_of_torment
            if (API.CanCast(MirrorsofTorment) && InRange && PlayerCovenantSettings == "Venthyr" && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" || UseCovenant == "on AOE" && IsAOE))
            {
                API.CastSpell(MirrorsofTorment);
                return;
            }
            //actions.st+=/shifting_power,if=buff.rune_of_power.down&(soulbind.grove_invigoration|soulbind.field_of_blossoms|active_enemies>=2)
            if (API.CanCast(ShiftingPower) && InRange && PlayerCovenantSettings == "Night Fae" && (API.SpellISOnCooldown(RoP) || !RuneOfPower) && API.SpellISOnCooldown(IV) && !API.PlayerHasBuff(IV) && !API.PlayerHasBuff(RoP) && !API.PlayerHasBuff(BrainFreeze) && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" || UseCovenant == "on AOE" && IsAOE))
            {
                API.CastSpell(ShiftingPower);
                return;
            }
            //actions.st+=/arcane_explosion,if=runeforge.disciplinary_command&cooldown.buff_disciplinary_command.ready&buff.disciplinary_command_arcane.down
            if (API.CanCast(AE) && API.TargetRange <= 3 && IsAOE && API.PlayerUnitInMeleeRangeCount >= 3 && !ChannelingShift)
            {
                API.CastSpell(AE);
                return;
            }
            //actions.st+=/frostbolt
            if (API.CanCast(Frostbolt) && Level >= 1 && API.TargetRange <= 40 && !API.PlayerIsMoving && (!API.PlayerHasBuff(FoF) && !API.PlayerHasBuff(BrainFreeze) && !API.TargetHasDebuff(WC) || API.PlayerHasBuff(IV) && API.PlayerBuffStacks(SlickIce) < 10))
            {
                API.CastSpell(Frostbolt);
                return;
            }
            if (API.CanCast(CoC) && Level >= 18 && API.TargetRange <= 10 && API.TargetUnitInRangeCount >= 3 && IsAOE  && !ChannelingShift)
            {
                API.CastSpell(CoC);
                return;
            }
            if (API.CanCast(Frostbolt) && Level >= 1 && API.TargetRange <= 40 && API.PlayerIsMoving && API.PlayerHasBuff(IF)  && !API.PlayerHasBuff(FoF) && !API.PlayerHasBuff(BrainFreeze) && !API.TargetHasDebuff(WC))
            {
                API.CastSpell(Frostbolt);
                return;
            }
            if (API.PlayerIsMoving && API.CanCast(IL) && Level >= 10 && !API.PlayerHasBuff(IF) && API.TargetRange <= 40 )
            {
                API.CastSpell(IL);
                return;
            }

        }

    }
}



