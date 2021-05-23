using System;
using System.Collections.Generic;
using System.Text;

namespace Constructor
{
    class license
    {
        
        public string keyData;
        public string[] keys= {"YOUR LICENSE XXXXX"};
        int say = 0;
        public bool used = false;
        List<string> key;
        
        public bool KeyCheck(string keyData)
        {

            for (int i = 0; i < keys.Length; i++)
            {
                if (keyData == keys[i])
                {
                    say++;
                    if (say == 2)
                    {
                        keys[i] = "blinblinski";
                        say = 0;
                    }
                    return true;
                }
                if (keys[i]== "blinblinski" &&keyData== "YOUR LICENSE XXXX")
                {
                    used = true;
                }
               

            }
            return false;
            

        }


    }
}
