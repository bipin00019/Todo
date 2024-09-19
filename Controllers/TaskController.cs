using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoTask.Web.Data;
using ToDoTask.Web.Models;
using ToDoTask.Web.Models.Enitities;

namespace ToDoTask.Web.Controllers
{
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public TaskController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTaskViewModel viewModel)
        {
            var task = new ToDoIdentity
            {
                Title = viewModel.Title,
                AddedOn=DateTime.Now,


            };
            await dbContext.Tasks.AddAsync(task);
            await dbContext.SaveChangesAsync();
            ModelState.Clear();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var task = await dbContext.Tasks.ToListAsync();
            return View(task);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var task = await dbContext.Tasks.FindAsync(id);
            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ToDoIdentity viewModel)
        {
            var task = await dbContext.Tasks.FindAsync(viewModel.ID);
            if (task != null)
            {
                task.Title = viewModel.Title;
                task.UpdatedOn = DateTime.Now;
                await dbContext.SaveChangesAsync();
            }
            
            return RedirectToAction("List","Task");


        }
        public async Task<IActionResult> Delete(ToDoIdentity viewModel)
        {
            var task = await dbContext.Tasks
                .AsNoTracking()
                .FirstOrDefaultAsync(x=>x.ID==viewModel.ID);
            if (task != null)
            {
                dbContext.Tasks.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List","Task");
        }

        [HttpPost]
        public async Task<IActionResult> Status(Guid ID, int Status)
        {
            var task= await dbContext.Tasks.FindAsync(ID);
            if (task!= null)
            {
                task.Status = Status;
                if (Status == 2)
                {
                    task.CompletedOn = DateTime.Now;
                }
                else
                {
                    task.CompletedOn = null;
                }
                //UpdatedOn= DateTime.Now;
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List");

        }

    }
}
