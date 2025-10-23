using System.Collections.Generic;
using TPT.Gameplay.Heroes;

namespace TPT.Gameplay.Selection
{
    public interface IHeroSelectionCallbackProvider
    {
        void OnComplete(List<Hero> heroes);
        void OnFail();
    }
}