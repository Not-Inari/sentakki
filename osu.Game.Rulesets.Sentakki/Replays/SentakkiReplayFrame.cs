﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Game.Beatmaps;
using osu.Game.Replays.Legacy;
using osu.Game.Rulesets.Replays;
using osu.Game.Rulesets.Replays.Types;
using osuTK;

namespace osu.Game.Rulesets.Sentakki.Replays
{
    public class SentakkiReplayFrame : ReplayFrame, IConvertibleReplayFrame
    {
        public ReplayEvent NoteEvent = ReplayEvent.none;
        public Vector2 Position;
        public List<SentakkiAction> Actions = new List<SentakkiAction>();

        public SentakkiReplayFrame()
        {
        }

        public SentakkiReplayFrame(double time, Vector2 position, params SentakkiAction[] actions)
            : base(time)
        {
            Position = position;
            Actions.AddRange(actions);
        }

        public void FromLegacy(LegacyReplayFrame currentFrame, IBeatmap beatmap, ReplayFrame lastFrame = null)
        {
            Position = currentFrame.Position;

            if (currentFrame.MouseLeft) Actions.Add(SentakkiAction.Button1);
            if (currentFrame.MouseRight) Actions.Add(SentakkiAction.Button2);
        }

        public LegacyReplayFrame ToLegacy(IBeatmap beatmap)
        {
            ReplayButtonState state = ReplayButtonState.None;

            if (Actions.Contains(SentakkiAction.Button1)) state |= ReplayButtonState.Left1;
            if (Actions.Contains(SentakkiAction.Button2)) state |= ReplayButtonState.Right1;

            return new LegacyReplayFrame(Time, Position.X, Position.Y, state);
        }
    }

    public enum ReplayEvent
    {
        none,
        TapDown,
        TapUp,
        TouchHoldDown,
        TouchHoldUp,
        HoldDown,
        HoldUp
    }
}
