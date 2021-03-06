// WizardClass.cs
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
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace DwarfCorp
{
    public class WizardClass : EmployeeClass
    {
        void InitializeLevels()
        {
            Levels = new List<Level>
            {
                new Level
                {
                    Index = 0,
                    Name = "Bachelor of Magical Studies",
                    Pay = 25,
                    XP = 0,
                    BaseStats = new CreatureStats.StatNums(),
                },
                new Level
                {
                    Index = 1,
                    Name = "Master of Magical Studies",
                    Pay = 50,
                    XP = 100,
                    BaseStats = new CreatureStats.StatNums()
                    {
                        Intelligence = 6,
                        Constitution = 6
                    }
                },
                new Level
                {
                    Index = 2,
                    Name = "PhM Candidate",
                    Pay = 100,
                    XP = 250,
                    BaseStats = new CreatureStats.StatNums()
                    {
                        Intelligence = 7,
                        Constitution = 6,
                        Charisma = 6
                    },
                    HealingPower = 1
                },
                new Level
                {
                    Index = 3,
                    Name = "Adjunct Wizard",
                    Pay = 200,
                    XP = 500,
                    BaseStats = new CreatureStats.StatNums()
                    {
                        Intelligence = 7,
                        Constitution = 7,
                        Charisma = 6,
                        Dexterity = 6
                    },
                    HealingPower = 5,
                    ExtraAttacks = new List<Attack>()
                    {
                        new Attack("Fireball", 5.0f, 3.0f, 5.0f, SoundSource.Create(ContentPaths.Audio.Oscar.sfx_env_lava_spread), ContentPaths.Effects.explode)
                        {
                            Mode = Attack.AttackMode.Area,
                            TriggerFrame = 2,
                            TriggerMode = Attack.AttackTrigger.Animation,
                            HitParticles = "flame",
                            ShootLaser = true
                        }
                    }
                },
                new Level
                {
                    Index = 4,
                    Name = "Associate Wizard",
                    Pay = 500,
                    XP = 1000,
                    BaseStats = new CreatureStats.StatNums()
                    {
                        Intelligence = 8,
                        Constitution = 7,
                        Charisma = 6,
                        Dexterity = 6
                    },
                    HealingPower = 10,
                    ExtraAttacks = new List<Attack>()
                    {
                        new Attack("Fireball", 15.0f, 3.0f, 5.0f, SoundSource.Create(ContentPaths.Audio.Oscar.sfx_env_lava_spread), ContentPaths.Effects.explode)
                        {
                            Mode = Attack.AttackMode.Area,
                            TriggerFrame = 2,
                            TriggerMode = Attack.AttackTrigger.Animation,
                            HitParticles = "flame",
                            ShootLaser = true
                        }
                    }
                },
                new Level
                {
                    Index = 5,
                    Name = "Tenured Wizard",
                    Pay = 1000,
                    XP = 5000,
                    BaseStats = new CreatureStats.StatNums()
                    {
                        Intelligence = 9,
                        Constitution = 8,
                        Charisma = 7,
                        Dexterity = 7
                    },
                    HealingPower = 15,
                    ExtraAttacks = new List<Attack>()
                    {
                        new Attack("Fireball", 20.0f, 3.0f, 5.0f, SoundSource.Create(ContentPaths.Audio.Oscar.sfx_env_lava_spread), ContentPaths.Effects.explode)
                        {
                            Mode = Attack.AttackMode.Area,
                            TriggerFrame = 2,
                            TriggerMode = Attack.AttackTrigger.Animation,
                            HitParticles = "flame",
                            ShootLaser = true
                        }
                    }
                },
                new Level
                {
                    Index = 6,
                    Name = "Wizarding Fellow",
                    Pay = 5000,
                    XP = 10000,
                    BaseStats = new CreatureStats.StatNums()
                    {
                        Intelligence = 10,
                        Constitution = 8,
                        Charisma = 8,
                        Dexterity = 8
                    },
                    HealingPower = 20,
                    ExtraAttacks = new List<Attack>()
                    {
                        new Attack("Fireball", 40.0f, 3.0f, 5.0f, SoundSource.Create(ContentPaths.Audio.Oscar.sfx_env_lava_spread), ContentPaths.Effects.explode)
                        {
                            Mode = Attack.AttackMode.Area,
                            TriggerFrame = 2,
                            TriggerMode = Attack.AttackTrigger.Animation,
                            HitParticles = "flame",
                            ShootLaser = true
                        }
                    }
                },
                new Level
                {
                    Index = 7,
                    Name = "Dean of Wizarding",
                    Pay = 10000,
                    XP = 20000,
                    BaseStats = new CreatureStats.StatNums()
                    {
                        Intelligence = 10,
                        Constitution = 9,
                        Charisma = 9,
                        Dexterity = 9,
                        Strength = 6
                    },
                    HealingPower = 20,
                    ExtraAttacks = new List<Attack>()
                    {
                        new Attack("Fireball", 40.0f, 3.0f, 5.0f, SoundSource.Create(ContentPaths.Audio.Oscar.sfx_env_lava_spread), ContentPaths.Effects.explode)
                        {
                            Mode = Attack.AttackMode.Area,
                            TriggerFrame = 2,
                            TriggerMode = Attack.AttackTrigger.Animation,
                            HitParticles = "flame",
                            ShootLaser = true
                        }
                    }

                },
                new Level
                {
                    Index = 8,
                    Name = "Chair of Wizarding",
                    Pay = 50000,
                    XP = 1000000,
                    BaseStats = new CreatureStats.StatNums()
                    {
                        Intelligence = 10,
                        Constitution = 10,
                        Charisma = 10,
                        Dexterity = 10,
                        Strength = 6
                    },
                    HealingPower = 20,
                    ExtraAttacks = new List<Attack>()
                    {
                        new Attack("Fireball", 40.0f, 3.0f, 5.0f, SoundSource.Create(ContentPaths.Audio.Oscar.sfx_env_lava_spread), ContentPaths.Effects.explode)
                        {
                            Mode = Attack.AttackMode.Area,
                            TriggerFrame = 2,
                            TriggerMode = Attack.AttackTrigger.Animation,
                            HitParticles = "flame",
                            ShootLaser = true
                        }
                    }
                },
                new Level
                {
                    Index = 9,
                    Name = "Magical Provost",
                    Pay = 100000,
                    XP = 2000000,
                    BaseStats = new CreatureStats.StatNums()
                    {
                        Intelligence = 10,
                        Constitution = 10,
                        Charisma = 10,
                        Dexterity = 10,
                        Strength = 10
                    },
                    HealingPower = 20,
                    ExtraAttacks = new List<Attack>()
                    {
                        new Attack("Fireball", 40.0f, 3.0f, 5.0f, SoundSource.Create(ContentPaths.Audio.Oscar.sfx_env_lava_spread), ContentPaths.Effects.explode)
                        {
                            Mode = Attack.AttackMode.Area,
                            TriggerFrame = 2,
                            TriggerMode = Attack.AttackTrigger.Animation,
                            HitParticles = "flame",
                            ShootLaser = true
                        }
                    }
                },
                new Level
                {
                    Index = 10,
                    Name = "Wizard Emeritus",
                    Pay = 100000,
                    XP = 5000000,
                    BaseStats = new CreatureStats.StatNums()
                    {
                        Intelligence = 10,
                        Constitution = 10,
                        Charisma = 10,
                        Dexterity = 10,
                        Strength = 10
                    },
                    HealingPower = 20,
                    ExtraAttacks = new List<Attack>()
                    {
                        new Attack("Fireball", 40.0f, 3.0f, 5.0f, SoundSource.Create(ContentPaths.Audio.Oscar.sfx_env_lava_spread), ContentPaths.Effects.explode)
                        {
                            Mode = Attack.AttackMode.Area,
                            TriggerFrame = 2,
                            TriggerMode = Attack.AttackTrigger.Animation,
                            HitParticles = "flame",
                            ShootLaser = true
                        }
                    }
                }
            };
        }

        void InitializeActions()
        {
            Actions =
                Task.TaskCategory.Gather |
                Task.TaskCategory.Research |
                Task.TaskCategory.Attack;
        }

        void InitializeAnimations()
        {
            Animations = AnimationLibrary.LoadCompositeAnimationSet(ContentPaths.Entities.Dwarf.Sprites.wizard_animation, "Dwarf");
            MinecartAnimations = ContentPaths.Entities.Dwarf.Sprites.wizard_minecart;
        }

        public void InitializeWeapons()
        {
            Attacks = new List<Attack>()
            {
                new Attack("Magic Missile", 1.0f, 0.5f, 3.0f, ContentPaths.Audio.Oscar.sfx_ic_dwarf_spell_cast_1, ContentPaths.Effects.pierce)
                {
                    Knockback = 0.5f,
                    HitParticles = "star_particle",
                    TriggerMode = Attack.AttackTrigger.Animation,
                    TriggerFrame = 2
                }
            };
        }

        protected override sealed void InitializeStatics()
        {
            Name = "Wizard";
            InitializeLevels();
            InitializeAnimations();
            InitializeWeapons();
            InitializeActions();
            AttackMode = CharacterMode.Attacking01;
            base.InitializeStatics();
        }
        public WizardClass()
        {
            if (!staticsInitiailized)
            {
                InitializeStatics();
            }
        }
    }
}
