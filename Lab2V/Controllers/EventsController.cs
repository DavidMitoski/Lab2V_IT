using Lab2V.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab2V.Controllers
{
    public class EventsController : Controller
    {
        public static List<EventModel> events = new List<EventModel>()
        {
           new EventModel() { Id = 1, Ime="Event1", Lokacija="Skopje"},
           new EventModel() { Id = 2, Ime="Event2", Lokacija="Bitola"},
           new EventModel() { Id = 3, Ime="Event3", Lokacija="Prilep"}
        };

        // GET: Events
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowAllEvents()
        {
            return View(events);
        }

        public EventModel getEventWithId(int id)
        {
            return events.Find(e => e.Id == id);
        }
        public ActionResult ShowEvents(int id)
        {
            EventModel model = getEventWithId(id);
            return View(model);
        }

        public ActionResult AddEvent() 
        {
            EventModel model = new EventModel();
            return View(model); 
        }

        public ActionResult InsertNewEvent(EventModel model) 
        {
            if(!ModelState.IsValid)
            {
                return View("AddEvent",model);
            }
            EventModel newEvent = model;
            if(events.Count == 0)
            {
                newEvent.Id = 1;
            }
            else
            {
                newEvent.Id=events.Max( e=> e.Id) +1 ;
            }


            events.Add(newEvent);
            return View("ShowEvents",newEvent);
        }

        public ActionResult EditEvent(int id)
        {
            EventModel model = getEventWithId(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditEventInList(EventModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View("EditEvent", model);
            }
            var forUpdate = getEventWithId(model.Id);
            forUpdate.Id = model.Id;
            forUpdate.Ime = model.Ime;
            forUpdate.Lokacija = model.Lokacija;

            return View("ShowAllEvents", events);

        }

        public ActionResult DeleteEvent(int id)
        {
            events.Remove(getEventWithId(id));

            return View("ShowAllEvents",events);
        }
    }
}