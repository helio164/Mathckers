using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mathckers_.Models;

namespace Mathckers_.Controllers
{
    public class AttacksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private static readonly Random rand = new Random();

        // GET: Attacks
        public ActionResult Index(string id)
        {
            IQueryable<Attack> attacks;
            if (!string.IsNullOrWhiteSpace(id))
            {
                attacks = db.Attacks.Where(a => a.AttackerID == id).Include(a => a.Attacker).Include(a => a.Defender);
            }
            else
            {
                attacks = db.Attacks.Include(a => a.Attacker).Include(a => a.Defender);
            }
            return View(attacks.ToList());
        }

        // GET: Attacks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attack attack = db.Attacks.Find(id);
            if (attack == null)
            {
                return HttpNotFound();
            }
            return View(attack);
        }

        //// GET: Attacks/Create
        #region Create
        //public ActionResult Create()
        //{
        //    ViewBag.AttackerID = new SelectList(db.Players, "UserID", "Alias");
        //    ViewBag.DefenderID = new SelectList(db.Players, "UserID", "Alias");
        //    return View();
        //}

        //// POST: Attacks/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,AttackerID,DefenderID")] Attack attack)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Attacks.Add(attack);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.AttackerID = new SelectList(db.Players, "UserID", "Alias", attack.AttackerID);
        //    ViewBag.DefenderID = new SelectList(db.Players, "UserID", "Alias", attack.DefenderID);
        //    return View(attack);
        //}
        #endregion

        public ActionResult Create(string attacker, string defender)
        {
            AttackViewModel battle = new AttackViewModel()
            //{
            //    ID = Guid.NewGuid(),
            //    AttackerID = attacker,
            //    DefenderID = defender,
            //    Attacker = db.Players.Find(attacker),
            //    Defender = db.Players.Find(defender)
            //}
            ;

            battle.ID = Guid.NewGuid();
            battle.AttackerID = attacker;
            battle.DefenderID = defender;
            battle.Attacker = db.Players.Find(attacker);
            battle.Defender = db.Players.Find(defender);
            battle.Attacker.Coins -= 10;

            int xpAttk = battle.Attacker.XP, xpDefd = battle.Defender.XP;

            battle.Points = GetPoints(xpAttk, xpDefd);
            //battle.Defended = GetDefended(xpAttk, xpDefd);
            //battle.Quest = GetQuest(battle.Defended);

            battle.Problem = Utilities.Utilities._GetJSONData(Utilities.Utilities.MATHLY);
            battle.ProblemID = battle.Problem.id;
            //db.Attacks.Add(battle);
            //if (!battle.Defended)
            //{
            //    battle.Attacker.XP += battle.Points;
            //}
            //else
            //{
            //    battle.Defender.Coins += 10;
            //}
            db.SaveChanges();

            return View(battle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AttackViewModel attack)
        {
            //attack.ID = Guid.NewGuid();
            //attack.AttackerID = attacker;
            //attack.DefenderID = defender;
            attack.Attacker = db.Players.Find(attack.AttackerID);
            attack.Defender = db.Players.Find(attack.DefenderID);
            //attack.Attacker.Coins -= 10;

            int xpAttk = attack.Attacker.XP, xpDefd = attack.Defender.XP;

            attack.Points = GetPoints(xpAttk, xpDefd);
            attack.Defended = GetDefended(attack.Answer.GetValueOrDefault(), attack.Problem.correct_choice);
            attack.Quest = GetQuest(attack.Defended);

            db.Attacks.Add(new Attack() {
                ID = attack.ID,
                AttackerID = attack.AttackerID,
                Attacker = attack.Attacker,
                DefenderID = attack.DefenderID,
                Defender = attack.Defender,
                Defended = attack.Defended,
                Points = attack.Points,
                Quest = attack.Quest
            });
            if (!attack.Defended)
            {
                attack.Attacker.XP += attack.Points;
            }
            else
            {
                attack.Defender.Coins += 10;
            }
            db.SaveChanges();

            return View("AttackDetails", attack);
        }

        // GET: Attacks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attack attack = db.Attacks.Find(id);
            if (attack == null)
            {
                return HttpNotFound();
            }
            ViewBag.AttackerID = new SelectList(db.Players, "UserID", "Alias", attack.AttackerID);
            ViewBag.DefenderID = new SelectList(db.Players, "UserID", "Alias", attack.DefenderID);
            return View(attack);
        }

        // POST: Attacks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AttackerID,DefenderID")] Attack attack)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attack).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AttackerID = new SelectList(db.Players, "UserID", "Alias", attack.AttackerID);
            ViewBag.DefenderID = new SelectList(db.Players, "UserID", "Alias", attack.DefenderID);
            return View(attack);
        }

        // GET: Attacks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attack attack = db.Attacks.Find(id);
            if (attack == null)
            {
                return HttpNotFound();
            }
            return View(attack);
        }

        // POST: Attacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attack attack = db.Attacks.Find(id);
            db.Attacks.Remove(attack);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #region Helpers

        public int GetPoints(int xpAttk,int xpDefd)
        {
            int point = 0;

            point = 3 * (xpAttk / xpDefd);

            return point;
        }

        public string GetQuest(bool _result)
        {
            string result;
            if (_result == false)
                result = "Hacking Succeded";
            else
                result = "Hacking Failed";

            return result;

        }
        
        public bool GetDefended(int answer, int correct)
        {
            bool result = false;

            result = answer == correct ? false : true;

            return result;
        }

        #endregion

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
