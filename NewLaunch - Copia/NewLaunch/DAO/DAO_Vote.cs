using NewLaunch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewLaunch.DAO
{
    public class DAO_Vote
    {
        dbEntities entity;
        public DAO_Vote(dbEntities db)
        {
            entity = db;
        }


        public DAO_Vote()
        {
            entity = new dbEntities();
        }
        /// <summary>
        /// Verify if the vote is possible for the employee
        /// </summary>
        /// <param name="MyVoted"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public IEnumerable<object> VerifyPossibleVote (Voted MyVoted)
        {
            var nextDay = DateTime.Today.AddDays(1);
            IEnumerable<object> query = from sel in entity.Voted
                                        where sel.EmployeeID == MyVoted.EmployeeID
                                        where sel.DateVoted >= DateTime.Today && sel.DateVoted < nextDay 
                                        select new { sel.RestaurantID, sel.EmployeeID };

            return query;
        }


        static public bool IsTimeOfDayBetween(DateTime time,
                                      TimeSpan startTime, TimeSpan endTime)
        {
            if (endTime == startTime)
            {
                return true;
            }
            else if (endTime < startTime)
            {
                return time.TimeOfDay <= endTime ||
                    time.TimeOfDay >= startTime;
            }
            else
            {
                return time.TimeOfDay >= startTime &&
                    time.TimeOfDay <= endTime;
            }

        }

        public Restaurant GetTheMostVotedbyDay()
        {
            var nextDay = DateTime.Today.AddDays(1);
            List<Restaurant> listofRestaurants = (from voted in entity.Voted
                         join rest in entity.Restaurant on voted.RestaurantID equals rest.Id
                         where voted.DateVoted >= DateTime.Today && voted.DateVoted < nextDay 
                         select rest).ToList();

            List<Restaurant> freqAux = new List<Restaurant>();

            for (int i = 0; i < listofRestaurants.Count(); i++ )
            {
                if (!freqAux.Any(currentElement => currentElement.Id== listofRestaurants[i].Id))
                    freqAux.Add((Restaurant)listofRestaurants[i]);
            }

            List<Restaurant> freqAux2=freqAux;
            for (int i = 0; i < freqAux.Count; i++ )
            {
                for(int j=0; j<listofRestaurants.Count;j++)
                {
                    if (freqAux[i].Id == listofRestaurants[j].Id)
                     freqAux2[i].CountVoted++;
                }
            }

            
            var Greatest = freqAux2.OrderByDescending(i => i.CountVoted).FirstOrDefault();
            return (Restaurant)Greatest;
        }
    }
}