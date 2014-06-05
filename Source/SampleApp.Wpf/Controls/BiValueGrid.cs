using System.Windows;
using System.Windows.Controls;

namespace StyleMVVM.DotNet.TestApp.Controls
{
    public class BiValueGrid : Grid
    {
        public BiValueGrid()
        {
            this.ColumnDefinitions.Add(new ColumnDefinition { SharedSizeGroup = "LeftSide" });
            this.ColumnDefinitions.Add(new ColumnDefinition());
        }

        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);
            
            if (visualAdded == null)
            {
                return;
            }

            if (visualAdded == this.GetVisualChild(0))
            {
                visualAdded.SetValue(Grid.ColumnProperty, 0);
            }
            else
            {
                visualAdded.SetValue(Grid.ColumnProperty, 1);
            }
        }        
    }
}