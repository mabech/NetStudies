using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NewLaunch.Models
{
    public class VotedController : Controller
    {
        private dbEntities db = new dbEntities();

        // GET: /Voted/
        public ActionResult Index()
        {
            var voted = db.Voted.Include(v => v.Employee).Include(v => v.Restaurant);
            return View(voted.ToList());
        }

        // GET: /Voted/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voted voted = db.Voted.Find(id);
            if (voted == null)
            {
                return HttpNotFound();
            }
            return View(voted);
        }

        // GET: /Voted/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(db.Employee, "Id", "Name");
            ViewBag.RestaurantID = new SelectList(db.Restaurant, "Id", "Name");
            return View();
        }

       

        // POST: /Voted/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,EmployeeID,RestaurantID")] Voted MyVoted)
        {
            if (ModelState.IsValid)
            {

                DAO.DAO_Vote dao = new DAO.DAO_Vote(db);

                //var nextDay = DateTime.Today.AddDays(1);

                //var queryAllCustomers = from sel in db.Voted
                //                        where sel.EmployeeID == MyVoted.EmployeeID
                //                        where sel.DateVoted >= DateTime.Today && sel.DateVoted < nextDay 
                                        
                //                        select new { sel.RestaurantID, sel.EmployeeID };
                // checks if the guy already voted
                //var EmployeeExist = db.Voted.Find(MyVoted.EmployeeID);
                if (dao.VerifyPossibleVote(MyVoted).Count() ==0)
                {
                    MyVoted.DateVoted = DateTime.Now;
                    db.Voted.Add(MyVoted);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    var EmployeeName = db.Employee.Find(MyVoted.EmployeeID); //
                    Response.Write("<script>var alerttext='O " + EmployeeName.Name  + " já  votou'; alert(alerttext); </script>");
                }
            }

            ViewBag.EmployeeID = new SelectList(db.Employee, "Id", "Name", MyVoted.EmployeeID);
            ViewBag.RestaurantID = new SelectList(db.Restaurant, "Id", "Name", MyVoted.RestaurantID);
            //return View(voted);
            return View(MyVoted);
        }

        // GET: /Voted/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voted voted = db.Voted.Find(id);
            if (voted == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Employee, "Id", "Name", voted.EmployeeID);
            ViewBag.RestaurantID = new SelectList(db.Restaurant, "Id", "Name", voted.RestaurantID);
            return View(voted);
        }

        // POST: /Voted/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,EmployeeID,RestaurantID")] Voted voted)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voted).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.Employee, "Id", "Name", voted.EmployeeID);
            ViewBag.RestaurantID = new SelectList(db.Restaurant, "Id", "Name", voted.RestaurantID);
            return View(voted);
        }

        // GET: /Voted/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voted voted = db.Voted.Find(id);
            if (voted == null)
            {
                return HttpNotFound();
            }
            return View(voted);
        }

        // POST: /Voted/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Voted voted = db.Voted.Find(id);
            db.Voted.Remove(voted);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
