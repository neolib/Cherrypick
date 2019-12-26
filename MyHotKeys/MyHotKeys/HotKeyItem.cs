using System.Windows.Forms;

namespace MyHotKeys
{
    using MyHotKeys.Library;

    class HotKeyItem: HotKeyEntity
    {
        public Keys KeyStrokes { get; set; }
        public int RegisteredId { get; set; }
    }
}
