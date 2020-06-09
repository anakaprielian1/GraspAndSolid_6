using System;
using System.Threading;

namespace Full_GRASP_And_SOLID
{
    public class Adapter : TimerClient
    {
        private readonly CountdownTimer _adapteeCount = new CountdownTimer();

        public Adapter(CountdownTimer adapteeCount)
        {
            this._adapteeCount = adapteeCount;
        }

        public void RecipeTime(int time)
        {
            //adapteeCount = new CountdownTimer();
            _adapteeCount.Register(time, this);
        }

        public void TimeOut()
        {
            throw new NotImplementedException();
        }
    }
}

