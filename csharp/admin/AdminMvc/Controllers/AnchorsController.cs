/* 
 Copyright (c) 2010, Direct Project
 All rights reserved.

 Authors:
    John Theisen
  
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
Neither the name of The Direct Project (directproject.org) nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 
*/
using System;
using System.Linq;
using System.Web.Mvc;

using Health.Direct.Admin.Console.Models;
using Health.Direct.Admin.Console.Models.Repositories;

using AutoMapper;

using Health.Direct.Config.Store;

using MvcContrib.Pagination;

namespace Health.Direct.Admin.Console.Controllers
{
    public class AnchorsController : ControllerBase<Anchor, AnchorModel, IAnchorRepository>
    {
        public AnchorsController(IAnchorRepository repository) : base(repository)
        {
        }

        protected override void SetStatus(Anchor item, EntityStatus status)
        {
            item.Status = status;
        }

        public ActionResult Index(long? domainID, int? page)
        {
            ViewData["DateTimeFormat"] = "M/d/yyyy h:mm:ss tt";

            Func<Anchor, bool> filter = anchor => true;
            if (domainID.HasValue)
            {
                var domain = Mapper.Map<Domain, DomainModel>(new DomainRepository().Get(domainID.Value));
                ViewData["Domain"] = domain;
                filter = anchor => anchor.Owner.Equals(domain.Name, StringComparison.OrdinalIgnoreCase);
            }

            return View(Repository.FindAll()
                            .Where(filter)
                            .Select(anchor => Mapper.Map<Anchor, AnchorModel>(anchor))
                            .AsPagination(page ?? 1, DefaultPageSize));
        }

        public ActionResult Details(long id)
        {
            var anchor = Repository.Get(id);
            if (anchor == null) return View("NotFound");

            return Json(Mapper.Map<Anchor, AnchorModel>(anchor), "text/json", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add(string owner)
        {
            return View(new AnchorUploadModel { Owner = owner });
        }

        [HttpPost]
        public ActionResult Add(FormCollection formValues)
        {
            var model = new AnchorUploadModel();

            if (TryUpdateModel(model))
            {
                var bytes = GetFileFromRequest("certificateFile");
                var anchor = new Anchor(model.Owner, bytes, model.Password)
                                 {
                                     ForIncoming = (model.Purpose & PurposeType.Incoming) == PurposeType.Incoming,
                                     ForOutgoing = (model.Purpose & PurposeType.Outgoing) == PurposeType.Outgoing
                                 };
                Repository.Add(anchor);

                var domain = new DomainRepository().GetByDomainName(model.Owner);
                if (domain == null) return View("NotFound");

                return RedirectToAction("Index", new { domainID = domain.ID });
            }

            return View(model);
        }

        protected override ActionResult EnableDisable(long id, EntityStatus status)
        {
            var anchor = Repository.Get(id);
            if (anchor == null) return View("NotFound");

            anchor = Repository.ChangeStatus(anchor, status);

            return Json(Mapper.Map<Anchor, AnchorModel>(anchor), "text/json");
        }
    }
}