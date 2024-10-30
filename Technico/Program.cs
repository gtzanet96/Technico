// See https://aka.ms/new-console-template for more information
using Azure;
using Technico.Models;
using Technico.Repositories;
using Technico.Responses;
using Technico.Services;

Console.WriteLine("Hello, Mr. Iracleous!");

var owner1 = new PropertyOwner()
{
    Id = 10,
    FirstName = "Albert",
    LastName = "cms",
    PhoneNumber = "2102334367890",
    VAT = "777",
    Address = "L K 17-19",
    Email = "",
    Password = "Password123"
};
//Console.WriteLine($"Property owner name: {owner1.FirstName} {owner1.LastName} with id = {owner1.Id}");
//τα παραπάνω είναι στη RAM. Για να τα σώσω δουλεύω στη βάση.

TechnicoDbContext db = new TechnicoDbContext();
PropertyOwnerService ownerService = new PropertyOwnerService(db);

/* για τον Owner: */

//1)Create
//Σημείωση: Σε περίπτωση που αφήσετε και κενό κάποιο πεδίο και ίδιο το vat με κάποιο προηγούμενο vat, το vat error χτυπάει πριν από το null field error,
//παρότι το validation του είναι δεύτερο σε σειρά στο service create method! Από ότι καταλαβαίνω αυτό συμβαίνει γιατί έχοντας βάλει unique constraint
//για το VAT στη βάση, γίνεται trigger σε πρώτη προτεραιότητα το database-level validation error.
//PropertyOwnerResponse response = ownerService.CreatePropertyOwner(owner1);
//PropertyOwnerResponse response = ownerService.DeletePropertyOwner(9);
//PropertyCustomResponse response = ownerService.UpdatePropertyOwner(owner1);
//Console.WriteLine(response.Message);


//2)Get personal details
//var owner = ownerService.GetPropertyOwnerDetails(4);
//if (owner != null)
//{
//    Console.WriteLine($"Property owner details: Id = {owner.Id}, VAT = {owner.VAT}, Name = {owner.FirstName} {owner.LastName}, Address = {owner.Address}, Phone Number = {owner.PhoneNumber}, Email = {owner.Email}, User Type = {owner.UserType}");
//}
//else
//{
//    Console.WriteLine("Property owner not found.");
//}
//ownerService.GetPropertyOwnerDetails(4).VAT = "123224"; βγάζει error και βλέπουμε πως είναι όντως immutable τα get methods


/* για το Service: */
PropertyItemService itemService = new PropertyItemService(db);

var item1 = new PropertyItem()
{
    Id = 1,
    PropertyIdentificationNumber = "yyy66",
    PropertyAddress = "P K 11",
    YearOfConstruction = 1990,
    PropertyType = PropertyType.DetachedHouse
};
// Create
//PropertyCustomResponse response = itemService.CreatePropertyItem(item1, "1221442");
//PropertyCustomResponse response = itemService.UpdatePropertyItem(item1);
//PropertyCustomResponse response = itemService.AddCoOwnerToPropertyItem(2, "12335456");
//PropertyCustomResponse response = itemService.RemoveCoOwnerFromPropertyItem(2, "12335456");
PropertyCustomResponse response = itemService.DeletePropertyItem(1, false);
Console.WriteLine(response.Message);

//var item = itemService.GetPropertyItemDetails(1);
//if (item != null)
//{
//    Console.WriteLine($"Property item details: " +
//        $"Id = {item.Id}, " +
//        $"PropertyIdentificationNumber = {item.PropertyIdentificationNumber}, " +
//        $"Property Address = {item.PropertyAddress}, " +
//        $"Year Of Construction = {item.YearOfConstruction}, " +
//        $"Property Type = {item.PropertyType}, " +
//        $"Property Owners = {string.Join(", ", item.CoOwners.Select(co => $"{co.FirstName} {co.LastName} (VAT: {co.VAT})"))}"
//    );
//}
//else
//{
//    Console.WriteLine("Property item not found.");
//}
