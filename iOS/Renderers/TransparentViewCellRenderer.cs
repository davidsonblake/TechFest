using TechFest.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ViewCell), typeof(TransparentViewCellRenderer))]

namespace TechFest.iOS
{
    public class TransparentViewCellRenderer : ViewCellRenderer
    {
        public TransparentViewCellRenderer()
        {
        }

        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            if (cell != null)
            {
                cell.BackgroundColor = UIColor.Clear;
                cell.SelectionStyle = UITableViewCellSelectionStyle.None;
                cell.SeparatorInset = UIEdgeInsets.Zero;
                cell.LayoutMargins = UIEdgeInsets.Zero;
                tv.SeparatorInset = UIEdgeInsets.Zero;
                tv.SeparatorStyle = UITableViewCellSeparatorStyle.SingleLine;
                tv.LayoutMargins = UIEdgeInsets.Zero;
                tv.SeparatorColor = UIColor.FromRGBA(38, 78, 85, 200);

                tv.CellLayoutMarginsFollowReadableWidth = false;
            }

            return cell;
        }
    }
}