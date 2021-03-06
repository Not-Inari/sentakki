// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Scoring;
using osu.Game.Skinning;
using osu.Game.Audio;
using osu.Game.Configuration;
using osu.Game.Rulesets.Sentakki.Configuration;
using osu.Framework.Audio;
using osu.Framework.Bindables;

namespace osu.Game.Rulesets.Sentakki.Objects.Drawables
{
    public class DrawableBreak : DrawableTap
    {
        private readonly Bindable<bool> userPositionalHitSounds = new Bindable<bool>(false);
        private readonly BindableDouble balanceAdjust = new BindableDouble();
        private readonly SkinnableSound breakSound;
        public DrawableBreak(SentakkiHitObject hitObject) : base(hitObject)
        {
            AddRangeInternal(new Drawable[]{
                breakSound = new SkinnableSound(new SampleInfo("Break"))
            });
            breakSound.AddAdjustment(AdjustableProperty.Balance, balanceAdjust);
        }

        [BackgroundDependencyLoader(true)]
        private void load(OsuConfigManager osuConfig, SentakkiRulesetConfigManager sentakkiConfig)
        {
            osuConfig.BindWith(OsuSetting.PositionalHitSounds, userPositionalHitSounds);
            sentakkiConfig?.BindWith(SentakkiRulesetSettings.BreakSounds, breakEnabled);
        }

        private readonly Bindable<bool> breakEnabled = new Bindable<bool>(true);
        public override void PlaySamples()
        {
            base.PlaySamples();
            if (Result.Type == HitResult.Perfect && breakEnabled.Value)
            {
                const float balance_adjust_amount = 0.4f;
                balanceAdjust.Value = balance_adjust_amount * (userPositionalHitSounds.Value ? SamplePlaybackPosition - 0.5f : 0);
                breakSound?.Play();
            }
        }
    }
}
