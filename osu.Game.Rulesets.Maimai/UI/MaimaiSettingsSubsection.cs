﻿using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Graphics.UserInterface;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Maimai.Configuration;

namespace osu.Game.Rulesets.Maimai.UI
{
    public class MaimaiSettingsSubsection : RulesetSettingsSubsection
    {
        protected override string Header => "maimai";

        public MaimaiSettingsSubsection(Ruleset ruleset)
            : base(ruleset)
        {
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            var config = (MaimaiRulesetConfigManager)Config;

            // for an odd reason, Config seems to be passed as null when creating it. doesnt even get called...
            if (config == null)
                return;

            Children = new Drawable[]
            {
                new SettingsCheckbox
                {
                    LabelText = "Use Maimai style judgement text (In-game only)",
                    Bindable = config.GetBindable<bool>(MaimaiRulesetSettings.MaimaiJudgements)
                },
                new SettingsCheckbox
                {
                    LabelText = "Show Kiai effects",
                    Bindable = config.GetBindable<bool>(MaimaiRulesetSettings.KiaiEffects)
                },
                new SettingsCheckbox
                {
                    LabelText = "Show note start indicators",
                    Bindable = config.GetBindable<bool>(MaimaiRulesetSettings.ShowNoteStartIndicators)
                },
                new SettingsCheckbox
                {
                    LabelText = "Change ring color based on difficulty rating",
                    Bindable = config.GetBindable<bool>(MaimaiRulesetSettings.DiffBasedRingColor)
                },
                new SettingsSlider<double, TimeSlider>
                {
                    LabelText = "Note speed",
                    Bindable = config.GetBindable<double>(MaimaiRulesetSettings.AnimationDuration),
                },
                new SettingsSlider<float>
                {
                    LabelText = "Ring Opacity",
                    Bindable = config.GetBindable<float>(MaimaiRulesetSettings.RingOpacity),
                    KeyboardStep = 0.01f,
                    DisplayAsPercentage = true
                },
            };
        }

        private class TimeSlider : OsuSliderBar<double>
        {
            private string speedRating()
            {
                double speed = (1100 - Current.Value) / 100;

                if (speed == 10.5)
                    return "Sonic";

                return speed.ToString();
            }
            public override string TooltipText => Current.Value.ToString("N0") + "ms (" + speedRating() + ")";
        }
    }
}
