using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dojodachi.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;


namespace Dojodachi.Controllers
{
    public static class SessionExtensions
    {
        // We can call ".SetObjectAsJson" just like our other session set methods, by passing a key and a value
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            // This helper function simply serializes theobject to JSON and stores it as a string in session
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        // generic type T is a stand-in indicating that we need to specify the type on retrieval
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            string value = session.GetString(key);
            // Upon retrieval the object is deserialized based on the type we specified
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            Tamagochi Guygar = new Tamagochi(20, 20, 3, 50);
            game game = new game();
            HttpContext.Session.Clear();
            HttpContext.Session.SetObjectAsJson("tamagachi", Guygar);
            HttpContext.Session.SetObjectAsJson("gameover", game);
            return View();
        }

        [HttpGet("dojodachi")]
        public ViewResult dojodachi()
        {
            Tamagochi Retrievegochi = HttpContext.Session.GetObjectFromJson<Tamagochi>("tamagachi");
            game Retrievegame = HttpContext.Session.GetObjectFromJson<game>("gameover");
            ViewBag.game = Retrievegame;
            ViewBag.tamagachi = Retrievegochi;
            return View();
        }

        [HttpPost("dojodachi/feed")]
        public IActionResult Feed()
        {
            Tamagochi Retrievegochi = HttpContext.Session.GetObjectFromJson<Tamagochi>("tamagachi");
            Random rand = new Random();
            bool Likes = true;
            if(rand.Next(0,4) == 0){
                Likes = false;
            }
            if (Retrievegochi.Meals > 0)
            {
                Retrievegochi.Meals--;
                if (Likes == true){
                    Retrievegochi.Fullness += rand.Next(5, 11);
                }
                HttpContext.Session.SetObjectAsJson("tamagachi", Retrievegochi);
                ViewBag.tamagachi = Retrievegochi;
            }
            if (Retrievegochi.Fullness >= 100 && Retrievegochi.Happiness >= 100 && Retrievegochi.Energy >= 100)
            {
                game Retrievegame = HttpContext.Session.GetObjectFromJson<game>("gameover");
                Retrievegame.over = true;
                HttpContext.Session.SetObjectAsJson("gameover", Retrievegame);
                ViewBag.game = Retrievegame;
            }
            return RedirectToAction("dojodachi");
        }
        [HttpPost("dojodachi/play")]
        public IActionResult Play()
        {
            Tamagochi Retrievegochi = HttpContext.Session.GetObjectFromJson<Tamagochi>("tamagachi");
            Random rand = new Random();
            bool Likes = true;
            if(rand.Next(0,4) == 0){
                Likes = false;
            }
                Retrievegochi.Energy -= 5;
                if (Likes == true){
                    Retrievegochi.Happiness += rand.Next(5, 11);
                }
                HttpContext.Session.SetObjectAsJson("tamagachi", Retrievegochi);
                ViewBag.tamagachi = Retrievegochi;
            if (Retrievegochi.Fullness <= 0 || Retrievegochi.Happiness <= 0 || Retrievegochi.Energy <= 0)
            {
                game Retrievegame = HttpContext.Session.GetObjectFromJson<game>("gameover");
                Retrievegame.over = true;
                HttpContext.Session.SetObjectAsJson("gameover", Retrievegame);
                ViewBag.game = Retrievegame;
            }
            if (Retrievegochi.Fullness >= 100 && Retrievegochi.Happiness >= 100 && Retrievegochi.Energy >= 100)
            {
                game Retrievegame = HttpContext.Session.GetObjectFromJson<game>("gameover");
                Retrievegame.over = true;
                HttpContext.Session.SetObjectAsJson("gameover", Retrievegame);
                ViewBag.game = Retrievegame;
            }
            return RedirectToAction("dojodachi");
        }
        [HttpPost("dojodachi/work")]
        public IActionResult Work()
        {
            Tamagochi Retrievegochi = HttpContext.Session.GetObjectFromJson<Tamagochi>("tamagachi");
            Retrievegochi.Energy -= 5;
            Random rand = new Random();
            Retrievegochi.Meals += rand.Next(1, 4);
            HttpContext.Session.SetObjectAsJson("tamagachi", Retrievegochi);
            ViewBag.tamagachi = Retrievegochi;
            if (Retrievegochi.Fullness <= 0 || Retrievegochi.Happiness <= 0 || Retrievegochi.Energy <= 0)
            {
                game Retrievegame = HttpContext.Session.GetObjectFromJson<game>("gameover");
                Retrievegame.over = true;
                HttpContext.Session.SetObjectAsJson("gameover", Retrievegame);
                ViewBag.game = Retrievegame;
            }
            return RedirectToAction("dojodachi");
        }
        [HttpPost("dojodachi/sleep")]
        public IActionResult Sleep()
        {
            Tamagochi Retrievegochi = HttpContext.Session.GetObjectFromJson<Tamagochi>("tamagachi");
                Retrievegochi.Fullness -= 5;
                Retrievegochi.Happiness -= 5;
                Retrievegochi.Energy += 15;
                HttpContext.Session.SetObjectAsJson("tamagachi", Retrievegochi);
                ViewBag.tamagachi = Retrievegochi;
            if (Retrievegochi.Fullness <= 0 || Retrievegochi.Happiness <= 0 || Retrievegochi.Energy <= 0)
            {
                game Retrievegame = HttpContext.Session.GetObjectFromJson<game>("gameover");
                Retrievegame.over = true;
                HttpContext.Session.SetObjectAsJson("gameover", Retrievegame);
                ViewBag.game = Retrievegame;
            }
            if (Retrievegochi.Fullness >= 100 && Retrievegochi.Happiness >= 100 && Retrievegochi.Energy >= 100)
            {
                game Retrievegame = HttpContext.Session.GetObjectFromJson<game>("gameover");
                Retrievegame.over = true;
                HttpContext.Session.SetObjectAsJson("gameover", Retrievegame);
                ViewBag.game = Retrievegame;
            }
            return RedirectToAction("dojodachi");
        }

        [HttpPost("restart")]
        public IActionResult actionRestart()
        {
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}