using Elysium.Core;
using System.Linq;

namespace Elysium.Hotbar.Samples
{
    public class ItemHotbarPresenter : HotbarPresenter<IUsableItem, ItemHotbarViewData>
    {
        public ItemHotbarPresenter(IUnityLogger _logger, IHotbar<IUsableItem> _hotbar, IHotbarView<ItemHotbarViewData> _view) : base(_logger, _hotbar, _view)
        {

        }

        protected override ItemHotbarViewData GetViewData(IHotbarRow<IUsableItem> _row)
        {
            return new ItemHotbarViewData
            {
                CurrentRow = currentRow,
                Slots = _row.Slots.Select(x => new ItemHotbarViewSlotData
                {
                    Index = x.Index,
                    CanUse = x.Usable != null ? x.Usable.CanUse : false,
                    CanDrag = x.Usable != null,
                    Icon = x.Usable != null ? x.Usable.Icon : null,
                    Amount = x.Usable != null ? x.Usable.Amount : 0,
                }),
            };
        }
    }
}