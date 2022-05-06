namespace Traffic_ManagerHW2.Controllers.WebAppAppServices
{
    public class LoadBalancerClass
    {
        public LoadBalancerClass()
        {

        }
       public string LoadMachineUrl()
        {
            string randomUrl;
            Random rand = new Random();
            
            if (rand.Next(0, 2) == 0)
            {
                randomUrl = WebappConstants.FirstMachine;
            }
            else
            {
                randomUrl = WebappConstants.SecondMachine;
            }

            return randomUrl;
        }
    }
}
