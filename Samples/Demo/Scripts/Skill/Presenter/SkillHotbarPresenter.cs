using Elysium.Core;
using System.Linq;

namespace Elysium.Hotbar.Samples
{
    public class SkillHotbarPresenter : HotbarPresenter<IUsableSkill, SkillHotbarViewData>
    {
        public SkillHotbarPresenter(IUnityLogger _logger, IHotbar<IUsableSkill> _hotbar, IHotbarView<SkillHotbarViewData> _view) : base(_logger, _hotbar, _view)
        {

        }

        protected override SkillHotbarViewData GetViewData(IHotbarRow<IUsableSkill> _row)
        {
            return new SkillHotbarViewData
            {
                CurrentRow = currentRow,
                Slots = _row.Slots.Select(x => new SkillHotbarViewSlotData
                {
                    Index = x.Index,
                    CanUse = x != null ? x.Usable.CanUse : false,
                    CanDrag = x != null,
                    Icon = x != null ? x.Usable.Icon : null,
                }),
            };
        }
    }
}