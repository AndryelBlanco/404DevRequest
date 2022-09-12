using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _404DevRequest.Data;
using _404DevRequest.Models;
using MongoDB.Driver;
using _404DevRequest.Repositories;
using _404DevRequest.Repositories.Interfaces;

namespace _404DevRequest.Controllers
{
    public class TodoItemsController : Controller
    {

        private readonly MongoRepository _mongoRepo;

        public TodoItemsController(MongoRepository mongoRepo)
        {
            _mongoRepo = mongoRepo;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _mongoRepo.GetAsync());
        }
        // GET: TodoItems
        //public async Task<IActionResult> Index()
        //{
        //    Console.WriteLine(await _mongoRepository.GetAll());
        //    return View(await _mongoRepository.GetAll());
        //}



        // GET: TodoItems/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("TaskTitle,CreatedAt,Description, Priority")] TodoItem newTodo)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    DateTime dateTime = DateTime.Today;
                    newTodo.CreatedAt = dateTime.ToString("MM/dd/yyyy");
                    await _mongoRepo.CreateAsync(newTodo);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    throw new Exception($"Erro: {ex}");
                }
            }
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var todoToEdit = await _mongoRepo.GetAsync(id);

            if (todoToEdit == null)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
            //    try
            //    {

            //        await _mongoRepo.UpdateAsync(id, todoItem);

            //    }
            //    catch (DbUpdateConcurrencyException ex)
            //    {
            //        throw new Exception($"Erro {ex}");
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            return View(todoToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, [Bind("TaskTitle,CreatedAt,Description, Priority")] TodoItem newTodo)
        {
            var todoToEdit = await _mongoRepo.GetAsync(id);
            newTodo.Id = todoToEdit.Id;
            newTodo.CreatedAt = todoToEdit.CreatedAt;
            if (todoToEdit == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _mongoRepo.UpdateAsync(id, newTodo);
                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw new Exception($"Erro {ex}");
                }
            }
            return View(todoToEdit);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            var todoToExclude = await _mongoRepo.GetAsync(id);

            if (id == null || todoToExclude == null)
            {
                return NotFound();
            }

            try
            {
                await _mongoRepo.RemoveAsync(id);
                TempData["StatusMessage"] = "The item has been successfully removed!";
            }
            catch (DbUpdateConcurrencyException ex)
            {
                TempData["StatusMessage"] = "An error occurred while deleting the item: " + ex;
                throw new Exception($"Erro {ex}");
            }

            return RedirectToAction("Index");
        }

        // POST: TodoItems/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.TodoItem == null)
        //    {
        //        return Problem("Entity set '_404DevRequestContext.TodoItem'  is null.");
        //    }
        //    var todoItem = await _context.TodoItem.FindAsync(id);
        //    if (todoItem != null)
        //    {
        //        _context.TodoItem.Remove(todoItem);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
