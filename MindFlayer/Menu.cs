using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MindFlayer
{
    class Menu
    {
        List<String> options;
        public bool activated;
        int highlighted;

        public Menu()
        {
            options = new List<string>();
            highlighted = -1;
            activated = false;
        }

        public Menu(List<string> initOptions)
        {
            options = initOptions;
            highlighted = 0;
            activated = false;
        }

        public void AddItem (String item)
        {
            options.Add(item);
            if (options.Count == 1)
            {
                highlighted = 0;
            }
        }

        public void SelectNext()
        {
            if (highlighted == -1)
            {
                throw new NotImplementedException();
            }
            else if (highlighted == options.Count - 1)
            {
                highlighted = 0;
            }
            else
            {
                highlighted += 1;
            }
        }

        public void SelectPrevious()
        {
            if (highlighted == -1)
            {
                throw new NotImplementedException();
            }
            else if (highlighted == 0)
            {
                highlighted = options.Count - 1;
            }
            else
            {
                highlighted -= 1;
            }
        }

    }
}
