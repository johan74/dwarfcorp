// Faction.cs
// 
//  Modified MIT License (MIT)
//  
//  Copyright (c) 2015 Completely Fair Games Ltd.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// The following content pieces are considered PROPRIETARY and may not be used
// in any derivative works, commercial or non commercial, without explicit 
// written permission from Completely Fair Games:
// 
// * Images (sprites, textures, etc.)
// * 3D Models
// * Sound Effects
// * Music
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using DwarfCorp.GameStates;
using LibNoise;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DwarfCorp
{
    [JsonObject(IsReference =true)]
    public class TradeEnvoy : Expedition
    {
        public TradeEnvoy()
        {

        }

        public TradeEnvoy(DateTime date) : base(date)
        {
            WaitForTradeTimer = new DateTimer(date, new TimeSpan(0, 6, 0, 0, 0));
        }

        public DwarfBux TradeMoney { get; set; }
        public List<ResourceAmount> TradeGoods { get; set; }
        public DateTimer WaitForTradeTimer = null;
        public DwarfBux TributeDemanded = 0m;
        [JsonIgnore]
        public WorldManager.WorldPopup TradeWidget = null;

        public bool IsTradeWidgetValid()
        {
            return TradeWidget != null && TradeWidget.BodyToTrack != null && !TradeWidget.BodyToTrack.IsDead;
        }

        public void MakeTradeWidget(WorldManager World)
        {
            var liveCreatures = Creatures.Where(creature => creature != null && !creature.IsDead);
            if (!liveCreatures.Any())
            {
                return;
            }

            TradeWidget = World.MakeWorldPopup(new Goals.TimedIndicatorWidget()
            {
                Text = String.Format("Click here to trade with the {0}!", OwnerFaction.Race.Name),
                OnClick = (gui, sender) =>
                {
                    World.Paused = true;
                    if (String.IsNullOrEmpty(this.OwnerFaction.Race.DiplomacyConversation))
                        GameState.Game.StateManager.PushState(new Dialogue.DialogueState(
                            GameState.Game,
                            GameState.Game.StateManager,
                            this,
                            World.PlayerFaction,
                            World));
                    else
                    {
                        // Prepare conversation memory for an envoy conversation.
                        var cMem = World.ConversationMemory;
                        cMem.SetValue("$world", new Yarn.Value(World));
                        cMem.SetValue("$envoy", new Yarn.Value(this));
                        cMem.SetValue("$envoy-demands-tribute", new Yarn.Value(this.TributeDemanded != 0));
                        cMem.SetValue("$envoy-tribute-demanded", new Yarn.Value((float)this.TributeDemanded.Value));
                        cMem.SetValue("$envoy-name", new Yarn.Value(TextGenerator.GenerateRandom(Datastructures.SelectRandom(OwnerFaction.Race.NameTemplates).ToArray())));
                        cMem.SetValue("$envoy-faction", new Yarn.Value(OwnerFaction.Name));

                        GameState.Game.StateManager.PushState(new YarnState(OwnerFaction.Race.DiplomacyConversation, "Start", cMem));
                    }
                },
                ShouldKeep = () => { return this.ExpiditionState == Expedition.State.Trading && !this.ShouldRemove; }
            }, liveCreatures.First().Physics, new Vector2(0, -10));
            SoundManager.PlaySound(ContentPaths.Audio.Oscar.sfx_gui_positive_generic, 0.15f);
        }

        public bool UpdateWaitTimer(DateTime now)
        {
            WaitForTradeTimer.Update(now);
            return WaitForTradeTimer.HasTriggered;
        }

        public void DistributeGoods()
        {
            if (Creatures.Count == 0) return;
            int goodsPerCreature = TradeGoods.Count / Creatures.Count;
            int currentGood = 0;
            foreach (CreatureAI creature in Creatures)
            {
                ResourcePack pack = creature.GetRoot().GetComponent<ResourcePack>();
                if (pack != null)
                {
                    pack.Contents.Resources.Clear();
                    for (int i = currentGood; i < System.Math.Min(currentGood + goodsPerCreature, TradeGoods.Count); i++)
                    {
                        pack.Contents.AddResource(TradeGoods[i]);
                    }
                    currentGood += goodsPerCreature;
                }
            }
        }
    }
}