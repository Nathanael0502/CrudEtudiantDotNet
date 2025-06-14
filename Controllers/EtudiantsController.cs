namespace MonProjetMVC.Controllers;

using System.IO;
using System.Linq;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using MonProjetMVC.Models;
using Microsoft.EntityFrameworkCore;
using MonProjetMVC.Data;

public class EtudiantsController : Controller
{
    private readonly AppDbContext _context;

    public EtudiantsController(AppDbContext context)
    {
        _context = context;
    }
    
public IActionResult Create()
{
    return View();
}
    public IActionResult Index()
    {
        var etudiants = _context.Etudiants.ToList();
        return View(etudiants);
    }

   [HttpPost]
public async Task<IActionResult> Create(Etudiant etudiant, IFormFile Photo)
{
    if (Photo != null && Photo.Length > 0)
    {
        var fileName = Path.GetFileName(Photo.FileName);
        var filePath = Path.Combine("wwwroot/images", fileName);

        // Créer le dossier si nécessaire
        Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await Photo.CopyToAsync(stream);
        }

        etudiant.PhotoPath = "/images/" + fileName;
    }

    if (ModelState.IsValid)
    {
        _context.Etudiants.Add(etudiant);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    

    return View(etudiant);
}


 

// GET: Etudiants/Details/5
public IActionResult Details(int id)
{
    var etudiant = _context.Etudiants.FirstOrDefault(e => e.Id == id);
    if (etudiant == null)
    {
        return NotFound();
    }
    return View(etudiant);
}

// GET: Etudiants/Edit/5
public IActionResult Edit(int id)
{
    var etudiant = _context.Etudiants.Find(id);
    if (etudiant == null)
    {
        return NotFound();
    }
    return View(etudiant);
}

// POST: Etudiants/Edit/5
[HttpPost]
public async Task<IActionResult> Edit(int id, Etudiant etudiant, IFormFile? Photo)
{
    if (id != etudiant.Id)
    {
        return NotFound();
    }

    if (Photo != null && Photo.Length > 0)
    {
        var fileName = Path.GetFileName(Photo.FileName);
        var filePath = Path.Combine("wwwroot/images", fileName);

        Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await Photo.CopyToAsync(stream);
        }

        etudiant.PhotoPath = "/images/" + fileName;
    }

    if (ModelState.IsValid)
    {
        var etudiantInDb = await _context.Etudiants.FindAsync(etudiant.Id);
        if (etudiantInDb == null)
            return NotFound();

       
        etudiantInDb.Nom = etudiant.Nom;
        etudiantInDb.Prenom = etudiant.Prenom;
        etudiantInDb.Email = etudiant.Email;

       
        if (!string.IsNullOrEmpty(etudiant.PhotoPath))
        {
            etudiantInDb.PhotoPath = etudiant.PhotoPath;
        }

        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    return View(etudiant);
}


// GET: Etudiants/Delete/5
public IActionResult Delete(int id)
{
    var etudiant = _context.Etudiants.FirstOrDefault(e => e.Id == id);
    if (etudiant == null)
    {
        return NotFound();
    }
    return View(etudiant);
}

// POST: Etudiants/Delete/5
[HttpPost, ActionName("Delete")]
public IActionResult DeleteConfirmed(int id)
{
    var etudiant = _context.Etudiants.Find(id);
    if (etudiant != null)
    {
        _context.Etudiants.Remove(etudiant);
        _context.SaveChanges();
    }
     return RedirectToAction("Index");
}

}
