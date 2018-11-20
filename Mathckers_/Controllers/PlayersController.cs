using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mathckers_.Models;
using Microsoft.AspNet.Identity;

namespace Mathckers_.Controllers
{
    public class PlayersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Players
        public ActionResult Index()
        {
            string loggedUser = User.Identity.GetUserId();
            var players = db.Players.Where(p => p.UserID != loggedUser).Include(p => p.User).Include(p => p.Avatar);
            return View(players.ToList());
        }
        public ActionResult Rank()
        {
            string loggedUser = User.Identity.GetUserId();
            var players = db.Players.Include(p => p.User).Include(p => p.Avatar).OrderByDescending(p => p.XP).Take(100);
            return View(players.ToList());
        }

        // GET: Players/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // GET: Players/Create
        #region Create
        //public ActionResult Create()
        //{
        //    ViewBag.UserID = new SelectList(db.ApplicationUsers, "Id", "Email");
        //    return View();
        //}

        //// POST: Players/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "UserID,Alias,XP,Level,Coins")] Player player)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Players.Add(player);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.UserID = new SelectList(db.ApplicationUsers, "Id", "Email", player.UserID);
        //    return View(player);
        //} 
        #endregion

        public static void CreatePlayer(ApplicationUser User)
        {
            try
            {
                var player = new Player()
                {
                    UserID = User.Id,
                    Alias = Guid.NewGuid().ToString(),
                    Level = 1,
                    XP = 1,
                    Coins = 150,
                    AvatarID = 2
                };

                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    db.Players.Add(player);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }


        }

        // GET: Players/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "Id", "Email", player.UserID);
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Alias,XP,Level,Coins")] Player player)
        {
            if (ModelState.IsValid)
            {
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "Id", "Email", player.UserID);
            return View(player);
        }

        // GET: Players/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Player player = db.Players.Find(id);
            db.Players.Remove(player);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult _LoginPartial()
        {
            string id = User.Identity.GetUserId();

            Player player = null;

            try
            {
                player = db.Players.Find(id);
            }
            catch (Exception ex)
            {

                throw;
            }

            return PartialView("_LoginPartial",player);
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
