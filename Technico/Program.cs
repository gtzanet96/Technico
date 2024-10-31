// See https://aka.ms/new-console-template for more information
using Azure;
using Technico.Models;
using Technico.Repositories;
using Technico.Responses;
using Technico.Services;

Console.WriteLine("Hello, Mr. Iracleous!");

var owner1 = new PropertyOwner()
{
    FirstName = "Albert",
    LastName = "C",
    PhoneNumber = "210232367890",
    VAT = "777",
    Address = "L K 17-19",
    Email = "stranger@gmail.com",
    Password = "Password123"
};
//τα παραπάνω είναι στη RAM. Για να τα σώσω δουλεύω στη βάση.

TechnicoDbContext db = new TechnicoDbContext();
PropertyOwnerService ownerService = new PropertyOwnerService(db);



/* για τον Owner: */

// ***** Create-Update-Delete Owner *****
//Σημείωση: Σε περίπτωση που αφήσετε και κενό κάποιο πεδίο και ίδιο το vat με κάποιο προηγούμενο vat, το vat error χτυπάει πριν από το null field error,
//παρότι το validation του είναι δεύτερο σε σειρά στο service create method! Από ότι καταλαβαίνω αυτό συμβαίνει γιατί έχοντας βάλει unique constraint
//για το VAT στη βάση, γίνεται trigger σε πρώτη προτεραιότητα το database-level validation error.
//PropertyCustomResponse response = ownerService.CreatePropertyOwner(owner1);
//PropertyCustomResponse response = ownerService.UpdatePropertyOwner(2, owner1);
//PropertyCustomResponse response = ownerService.DeletePropertyOwner(3);
//Console.WriteLine(response.Message);
//2)

// ***** Get owner personal details *****
/*var owner = ownerService.GetPropertyOwnerDetails(2); //parameters: (owner id)
if (owner != null) 
    { Console.WriteLine($"Property owner details: Id = {owner.Id}, Full Name = {owner.FirstName} {owner.LastName}, VAT = {owner.VAT}, Address = {owner.Address}, Phone Number = {owner.PhoneNumber}, Email = {owner.Email}, User Type = {owner.UserType}"); }
else 
    { Console.WriteLine("Property owner not found."); } */

//Σημείωση: Η παρακάτω δοκιμαστική σειρά βγάζει compile error και διαπιστώνουμε πως είναι όντως immutable τα get methods
//ownerService.GetPropertyOwnerDetails(4).VAT = "123224";

// ***** Get owner property items *****
//!Για να φαίνονται στο console (line51) τα στοιχεία του χρήστη του οποίου θέλουμε τα property items, πρώτα καλούμε την αντίστοιχη μέθοδο (line48) για να πάρουμε
//τα στοιχεία του και αμέσως μετά την επίμαχη μέθοδο που δίνει τα property items του (line55).
//var ownerDetails = ownerService.GetPropertyOwnerDetails(2); //parameters: (owner id)
//if (ownerDetails != null)
//{
//    Console.WriteLine($"Property items for owner {ownerDetails.FirstName} {ownerDetails.LastName} with ID = {ownerDetails.Id}");
//    var OwnerItems = ownerService.GetPropertyOwnerItems(ownerDetails.Id);
//    if (OwnerItems?.Count > 0)
//    {
//        foreach (var propertyItem in OwnerItems)
//        {
//            Console.WriteLine($"  Property ID: {propertyItem.Id}");
//            Console.WriteLine($"  Identification Number: {propertyItem.PropertyIdentificationNumber}");
//            Console.WriteLine($"  Address: {propertyItem.PropertyAddress}");
//            Console.WriteLine($"  Year of Construction: {propertyItem.YearOfConstruction}");
//            Console.WriteLine($"  Property Type: {propertyItem.PropertyType}");

//            Console.WriteLine("  Co-Owners:");
//            foreach (var coOwner in propertyItem.CoOwners)
//            {
//                Console.WriteLine($"    - Co-Owner ID: {coOwner.Id}");
//                Console.WriteLine($"      Name: {coOwner.FirstName} {coOwner.LastName}");
//                Console.WriteLine($"      VAT: {coOwner.VAT}");
//            }
//            Console.WriteLine();
//        }
//    }
//    else { Console.WriteLine("No property items found for the specified owner."); }
//}
//else { Console.WriteLine("Owner not found."); }

// ***** Get owner repairs ***** 
//var ownerDetails = ownerService.GetPropertyOwnerDetails(3); //parameters: (owner id)
//if (ownerDetails != null)
//{
//    Console.WriteLine($"Repairs for owner {ownerDetails.FirstName} {ownerDetails.LastName} with ID = {ownerDetails.Id}");
//    var ownerRepairs = ownerService.GetPropertyOwnerRepairs(ownerDetails.Id);
//    if (ownerRepairs?.Count > 0)
//    {
//        foreach (var repair in ownerRepairs)
//        {
//            Console.WriteLine($"  - Repair ID: {repair.Id}");
//            Console.WriteLine($"    Scheduled Date: {repair.ScheduledDate}");
//            Console.WriteLine($"    Type: {repair.RepairType}");
//            Console.WriteLine($"    Description: {repair.RepairDescription}");
//            Console.WriteLine($"    Status: {repair.RepairStatus}");
//            Console.WriteLine($"    Cost: {repair.Cost}");
//            Console.WriteLine();
//        }
//    }
//    else { Console.WriteLine("No repairs found for the specified owner."); }
//}
//else { Console.WriteLine("Owner not found."); }





/* για το Service: */
PropertyItemService itemService = new PropertyItemService(db);

var item1 = new PropertyItem()
{
    PropertyIdentificationNumber = 1209887723,
    PropertyAddress = "hj K 11",
    YearOfConstruction = 1990,
    PropertyType = PropertyType.DetachedHouse
};
// ***** Create-Update-Add & Remove Coowner-Delete ***** 
//PropertyCustomResponse response = itemService.CreatePropertyItem(item1, "1010"); //parameters: (new item, owner vat)
//PropertyCustomResponse response = itemService.UpdatePropertyItem(3, item1); //parameters: (item id, new item)
//PropertyCustomResponse response = itemService.AddCoOwnerToPropertyItem(1, "777"); //parameters: (property id, owner vat)
//PropertyCustomResponse response = itemService.RemoveCoOwnerFromPropertyItem(1, "777");parameters:  //(property id, owner vat)
//PropertyCustomResponse response = itemService.DeletePropertyItem(1, false); //parameters: (item id, false=hard delete και true=soft delete)
//Console.WriteLine(response.Message);

// ***** Get PropertyItem details ***** 
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
//else { Console.WriteLine("Property item not found."); }





/* για το Repair: */
PropertyRepairService repairService = new PropertyRepairService(db);

var repair1 = new PropertyRepair()
{
    ScheduledDate = new DateTime(2025, 12, 21, 15, 30, 00),
    RepairType = RepairType.ElectricalWork,
    RepairDescription = "good house",
    Cost = 10M,
    RepairStatus = RepairStatus.InProgress
};

// ***** Create-Update-Delete ***** 
//PropertyCustomResponse response = repairService.CreateRepair(repair1, 4); //parameters: (new repair, property item id)
//PropertyCustomResponse response = repairService.UpdateRepair(1, repair1); //parameters: (repair id, new repair)
//PropertyCustomResponse response = repairService.DeleteRepair(2, false); //parameters: (repair id, false=hard delete και true=soft delete)
//Console.WriteLine(response.Message);

// ***** Search by Repair Type ***** 
//var searchType = RepairType.Insulation; // parameters: (Painting, Insulation, Frames, Plumbing, ElectricalWork)
//var repairTypeResults = repairService.SearchRepairsByType(searchType);
//Console.WriteLine($"Repairs of type {searchType}:");
//if (repairTypeResults.Any())
//{
//    foreach (var repair in repairTypeResults)
//    {
//        Console.WriteLine($"- Repair ID: {repair.Id}, Description: {repair.RepairDescription}, Cost: {repair.Cost}, Status: {repair.RepairStatus}");
//    }
//}
//else { Console.WriteLine($"No repairs found for type {searchType}."); }

// ***** Search by Repair Status ***** 
//var searchStatus = RepairStatus.Pending; // parameters: (Pending, InProgress, Complete)
//var repairStatusResults = repairService.SearchRepairsByStatus(searchStatus);
//Console.WriteLine($"Repairs with status {searchStatus}:");
//if (repairStatusResults.Any())
//{
//    foreach (var repair in repairStatusResults)
//    {
//        Console.WriteLine($"- Repair ID: {repair.Id}, Description: {repair.RepairDescription}, Cost: {repair.Cost}, Type: {repair.RepairType}");
//    }
//}
//else { Console.WriteLine($"No repairs found with status {searchStatus}."); }

// ***** Search by Minimun Cost ***** 
//decimal minCost = 5M; // βάζουμε ένα μινιμουμ κόστος που θέλουμε να έχουν τα repairs που θα μας εμφανίσει
//var minCostResults = repairService.SearchRepairsByMinCost(minCost);
//Console.WriteLine($"Repairs with a minimum cost of {minCost}:");
//if (minCostResults.Any())
//{
//    foreach (var repair in minCostResults)
//    {
//        Console.WriteLine($"- Repair ID: {repair.Id}, Description: {repair.RepairDescription}, Cost: {repair.Cost}");
//    }
//}
//else { Console.WriteLine($"No repairs found with the minimum cost of {minCost}."); }