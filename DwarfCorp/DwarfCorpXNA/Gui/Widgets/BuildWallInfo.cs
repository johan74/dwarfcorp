using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DwarfCorp.Gui;
using Microsoft.Xna.Framework;

namespace DwarfCorp.Gui.Widgets
{
    /// <summary>
    /// A properly framed Icon for use in an icon tray.
    /// </summary>
    public class BuildWallInfo : Widget
    {
        public VoxelType Data;
        public GameMaster Master;

        public override void Construct()
        {
            Border = "border-fancy";
            var builder = new StringBuilder();
            builder.AppendLine(String.Format("Place {0}", Data.Name));
            builder.AppendLine(String.Format("Strength: {0}", Data.StartingHealth));
            builder.AppendLine(String.Format("Requires: {0}", Data.GetBuildRequirementsString()));
            builder.Append("Click to build.");
            
            Font = "font8";
            Text = builder.ToString();
            TextColor = Color.Black.ToVector4();
        }

        public bool CanBuild()
        {
            foreach (var resource in Master.Faction.ListResourcesInStockpilesPlusMinions().Where(r => Data.CanBuildWith(ResourceLibrary.GetResourceByName(r.Key))))
                if (resource.Value.First.NumResources > 0 || resource.Value.Second.NumResources > 0)
                    return true;
            return false;
        }

    }
}
