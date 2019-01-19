using R = AltCodeKneeboard.Properties.Resources;
using AltCodeKneeboard.Models;

namespace AltCodeKneeboard
{
    public enum SortMode
    {
        [DescriptionRes(nameof(R.ByGroup))]
        Grouped,

        [DescriptionRes(nameof(R.ByUnicode))]
        UnicodeOrder,

        [DescriptionRes(nameof(R.ByAlpha))]
        Alphabetically,

        [DescriptionRes(nameof(R.ByFavoritesOnly))]
        FavoritesOnly
    };
}
