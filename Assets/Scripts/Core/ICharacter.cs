using Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Core
{
    public interface ICharacter
    {
        void GetName();
        void AddItem(Item item);
    }
}
